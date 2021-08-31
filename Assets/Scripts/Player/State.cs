using UnityEngine;

namespace Player
{
    public abstract class State : StateMachine
    {
        public Controller Controller { get; private set; }
        
        public State(Controller controller)
        {
            Controller = controller;
        }
    }

    class Idle : State
    {
        public Idle(Controller controller) : base(controller) {}

        public override void EnterState()
        {
            Controller.SpriteRenderer.color = new UnityEngine.Color(255, 255, 255);
        }

        public override void Transitions()
        {
            if (Inputs.IsPressingMovement)
                Controller.SetState(new Move(Controller));
            else if (Inputs.InteractBPress && Controller.IsNearRaftEdge)
                Controller.SetState(new PaddleMode(Controller));
        }
    }

    class Move : State
    {
        public Move(Controller controller) : base(controller) {}

        public override void EnterState()
        {
            Controller.SpriteRenderer.color = new UnityEngine.Color(127, 0, 0);
        }

        public override void FixedUpdate()
        {
            MovePlayer();
        }

        public override void Transitions()
        {
            if (!Inputs.IsPressingMovement)
                Controller.SetState(new Idle(Controller));
            else if (Inputs.InteractBPress && Controller.IsNearRaftEdge)
                Controller.SetState(new PaddleMode(Controller));
        }

        private void MovePlayer()
        {
            if (Inputs.IsPressingMovement)
                Controller.Direction = new Vector2(Inputs.Horizontal, Inputs.Vertical).normalized;
            Controller.Velocity = Controller.Rigidbody2d.position + Controller.Direction * Controller.Speed * Time.fixedDeltaTime;
            Controller.Rigidbody2d.MovePosition(Controller.Velocity);
        }
    }

    class PaddleMode : State
    {
        public PaddleMode(Controller controller) : base(controller) {}

        public override void EnterState()
        {
            Controller.SpriteRenderer.color = Color.green;
        }

        public override void Transitions()
        {
            if (Inputs.InteractBHold)
            {
                if (Inputs.IsPressingMovement)
                    Controller.SetState(new Paddling(Controller));
            }
            else if (!Inputs.InteractBHold)
            {
                if (!Inputs.IsPressingMovement)
                    Controller.SetState(new Idle(Controller));
                else if (Inputs.IsPressingMovement)
                    Controller.SetState(new Move(Controller));
            }
        }
    }
    // TODO: Make paddling move based on a grid
    class Paddling : PaddleMode
    {
        private const float _defaultPaddleDistance = 0.1f;
        private const float _paddleSpeed = 5f;
        private Vector2 _direction;
        private bool _inMiddleOfPaddle;
        private float _paddleDistance = _defaultPaddleDistance;
        private float _paddleTimer;
        private const float _initialPaddleStartupTime = 0.25f;
        private float _initialPaddleTimer;

        public Paddling(Controller controller) : base(controller)
        {
            _initialPaddleTimer = Time.time + _initialPaddleStartupTime;
        }

        public override void EnterState()
        {
            Controller.SpriteRenderer.color = Color.blue;
        }

        public override void FixedUpdate()
        {
            if (!_inMiddleOfPaddle)
            {
                if (Mathf.Abs(Inputs.Horizontal) > 0.1f)
                    _direction = new Vector2(Inputs.Horizontal, 0f).normalized;
                else if (Mathf.Abs(Inputs.Vertical) > 0.1f)
                    _direction = new Vector2(0f, Inputs.Vertical).normalized;
                else
                    _initialPaddleTimer = Time.time + _initialPaddleStartupTime;
            }
            if (Time.time >= _initialPaddleTimer)
                MoveRaft(_direction);
        }

        public override void Transitions()
        {
            if ((!Inputs.IsPressingMovement || !Inputs.InteractBHold) && !_inMiddleOfPaddle)
                Controller.SetState(new PaddleMode(Controller));
        }

        private void MoveRaft(Vector2 dir)
        {
            // Slow raft based on the size of the raft
            int numPlatforms = 0;
            foreach (GameObject obj in Manager.RaftManager.Instance.RaftObjects)
                if (obj.CompareTag(Tag.Platform)) numPlatforms++;

            if (numPlatforms > 9)
            {
                _paddleDistance = _defaultPaddleDistance - 0.01f * (numPlatforms - 9);
                if (_paddleDistance < 0) _paddleDistance = 0;
            }
            else
                _paddleDistance = _defaultPaddleDistance;

            // Paddle distance over time based on sine wave
            Vector2 e1 = _paddleDistance * Mathf.Sin(_paddleSpeed * _paddleTimer) * dir;
            _inMiddleOfPaddle = Mathf.Sin(_paddleSpeed * _paddleTimer) >= 0f;
            if (!_inMiddleOfPaddle)
            {
                _paddleTimer = 0f;
            }
            else
            {
                foreach (GameObject e in Manager.RaftManager.Instance.RaftObjects)
                    e.transform.Translate(e1, Space.World);
                _paddleTimer += Time.deltaTime;
            }
        }
    }

    class UsingItem : State
    {
        private float _useDuration;

        public UsingItem(Controller controller, float useDuration) : base(controller)
        {
            _useDuration = useDuration;
        }

        public override void EnterState()
        {
            // Play Animation
            Controller.SpriteRenderer.color = Color.cyan;
        }

        public override void Transitions()
        {
            if (_useDuration > 0)
                _useDuration -= Time.deltaTime;
            else if (Inputs.IsPressingMovement)
                Controller.SetState(new Idle(Controller));
            else if (!Inputs.IsPressingMovement)
                Controller.SetState(new Move(Controller));
        }
    }
}