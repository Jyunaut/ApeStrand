using UnityEngine;

namespace Player
{
    public abstract class State : StateMachine
    {
        public Controller controller;
        
        public State(Controller controller)
        {
            this.controller = controller;
        }

        public bool Idle()
        {
            if (Mathf.Abs(PlayerInput.Horizontal) <= Mathf.Epsilon
                && Mathf.Abs(PlayerInput.Vertical) <= Mathf.Epsilon)
            {
                controller.SetState(new Idle(controller)); return true;
            }
            return false;
        }

        public bool Move()
        {
            if ((PlayerInput.Horizontal != 0
                || PlayerInput.Vertical != 0)
                && controller.CanMove)
            {
                controller.SetState(new Move(controller)); return true;
            }
            return false;
        }

        public bool PaddleMode()
        {
            if (PlayerInput.InteractHold_B && controller.CanPaddle)
            {
                controller.SetState(new PaddleMode(controller)); return true;
            }
            return false;
        }

        public bool Paddling()
        {
            if (Mathf.Abs(Player.PlayerInput.Horizontal) > 0
                || Mathf.Abs(Player.PlayerInput.Vertical) > 0)
            {
                controller.SetState(new Paddling(controller)); return true;
            }
            return false;
        }

        public bool UsingItem()
        {
            if (controller.UsingItem)
            {
                controller.SetState(new UsingItem(controller)); return true;
            }
            return false;
        }
    }

    class Idle : State
    {
        public Idle(Controller controller) : base(controller) {}

        public override void DoStateBehaviour()
        {
            controller.SpriteRenderer.color = new UnityEngine.Color(255, 255, 255);
        }

        public override void Transitions()
        {
            if      (Move()) {}
            else if (UsingItem()) {}
            else if (controller.HeldItem != null) return;
            else if (PaddleMode()) {}
        }
    }

    class Move : State
    {
        private Vector2 _prevPos;
        private float _timer;

        public Move(Controller controller) : base(controller) {}

        public override void DoStateBehaviourFixedUpdate()
        {
            MovePlayer();
            controller.SpriteRenderer.color = new UnityEngine.Color(127, 0, 0);
        }

        private void MovePlayer()
        {
            if (Mathf.Abs(PlayerInput.Horizontal) > 0 || Mathf.Abs(PlayerInput.Vertical) > 0)
                controller.Direction = new Vector2(PlayerInput.Horizontal, PlayerInput.Vertical).normalized;
            controller.Velocity = controller.Rigidbody2d.position + controller.Direction * controller.Speed * Time.fixedDeltaTime;
            controller.Rigidbody2d.MovePosition(controller.Velocity);
            _prevPos = controller.Rigidbody2d.position;
        }

        public override void Transitions()
        {
            if      (Idle()) {}
            else if (UsingItem()) {}
            else if (controller.HeldItem != null) return;
            else if (PaddleMode()) {}
        }
    }

    class PaddleMode : State
    {
        public PaddleMode(Controller controller) : base(controller) {}

        public override void DoStateBehaviour()
        {
            controller.SpriteRenderer.color = Color.green;
        }

        public override void Transitions()
        {
            if (Paddling()) {}
            if (PlayerInput.InteractHold_B) return;
            if      (Idle()) {}
            else if (Move()) {}
        }
    }

    class Paddling : PaddleMode
    {
        private Vector2 _direction;
        private bool inMiddleOfPaddle;
        private const float _defaultPaddleDistance = 0.1f;
        private float _paddleDistance = _defaultPaddleDistance;
        private float _paddleSpeed = 5f;
        private float _paddleDelay = 0.1f;
        private float _paddleTimer = 0f;

        public Paddling(Controller controller) : base(controller) {}

        public void MoveRaft(Vector2 dir)
        {
            // Slow raft based on the size of the raft
            int numPlatforms = 0;
            foreach (GameObject obj in Manager.RaftManager.Instance.RaftObjects)
                if (obj.CompareTag("Platform")) numPlatforms++;

            if (numPlatforms > 9)
            {
                _paddleDistance = _defaultPaddleDistance - 0.01f * (numPlatforms - 9);
                if (_paddleDistance < 0) _paddleDistance = 0;
            }
            else
                _paddleDistance = _defaultPaddleDistance;

            // Paddle distance over time based on sine wave
            Vector2 e1 = _paddleDistance * Mathf.Sin(_paddleSpeed * _paddleTimer) * dir;
            if (Mathf.Sin(_paddleSpeed * _paddleTimer) < 0)
            {
                _paddleTimer = 0f;
                return;
            }
            _paddleTimer += Time.fixedDeltaTime;
            foreach (GameObject e in Manager.RaftManager.Instance.RaftObjects)
                e.transform.Translate(e1, Space.World);
        }

        public override void ExitState()
        {
            _paddleTimer = 0f;
        }

        public override void DoStateBehaviour()
        {
            controller.SpriteRenderer.color = Color.blue;
        }

        public override void DoStateBehaviourFixedUpdate()
        {
            if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                && !inMiddleOfPaddle)
                _direction = new Vector2(PlayerInput.Horizontal, PlayerInput.Vertical).normalized;
            
            // Keep paddling if already in mid paddle even if inputs are released
            if (_paddleTimer > _paddleDelay)
                inMiddleOfPaddle = true;
            else
                inMiddleOfPaddle = false;
            MoveRaft(_direction);
        }

        public override void Transitions()
        {
            if ((Player.PlayerInput.Horizontal == 0
                && Player.PlayerInput.Vertical == 0
                || !Player.PlayerInput.InteractHold_B)
                && !inMiddleOfPaddle)
                controller.SetState(new PaddleMode(controller));
        }
    }

    class UsingItem : State
    {
        public UsingItem(Controller controller) : base(controller) {}

        public override void EnterState()
        {
            // Play Animation
        }

        public override void DoStateBehaviour()
        {
            controller.SpriteRenderer.color = Color.cyan;
            controller.UseDuration -= Time.deltaTime;
        }

        public override void ExitState()
        {
            controller.UsingItem = false;
            controller.UseDuration = 0;
        }

        public override void Transitions()
        {
            if (controller.UseDuration >= 0) return;
            if      (Idle()) {}
            else if (Move()) {}
        }
    }
}