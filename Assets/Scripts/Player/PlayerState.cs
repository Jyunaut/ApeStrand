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
            if (PlayerInput.InteractHold && controller.CanPaddle)
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

        public override void Transitions()
        {
            if      (Idle()) {}
            else if (PaddleMode()) {}
        }

        private void MovePlayer()
        {
            if (Mathf.Abs(PlayerInput.Horizontal) > 0
                || Mathf.Abs(PlayerInput.Vertical) > 0)
                controller.Direction = new Vector2(PlayerInput.Horizontal, PlayerInput.Vertical).normalized;
            controller.Velocity = controller.Rigidbody2d.position + controller.Direction * controller.Speed * Time.fixedDeltaTime;
            controller.Rigidbody2d.MovePosition(controller.Velocity);
            _prevPos = controller.Rigidbody2d.position;
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
            if (PlayerInput.InteractHold) return;
            if      (Idle()) {}
            else if (Move()) {}
        }
    }

    class Paddling : PaddleMode
    {
        private Vector2 _direction;
        private bool inMiddleOfPaddle;
        private float paddleDelay = 0.25f;

        public Paddling(Controller controller) : base(controller) {}

        public override void ExitState()
        {
            Manager.RaftManager.Instance.paddleTime = 0f;
        }

        public override void DoStateBehaviour()
        {
            controller.SpriteRenderer.color = Color.blue;
        }

        public override void DoStateBehaviourFixedUpdate()
        {
            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                _direction = new Vector2(PlayerInput.Horizontal, PlayerInput.Vertical).normalized;
            
            // Keep paddling if already in mid paddle even if inputs are released
            if (Manager.RaftManager.Instance.paddleTime > paddleDelay)
                inMiddleOfPaddle = true;
            else
                inMiddleOfPaddle = false;
            Manager.RaftManager.Instance.MoveRaft(_direction);
        }

        public override void Transitions()
        {
            if ((Player.PlayerInput.Horizontal == 0
                && Player.PlayerInput.Vertical == 0
                || !Player.PlayerInput.InteractHold)
                && !inMiddleOfPaddle)
                controller.SetState(new PaddleMode(controller));
        }
    }
}