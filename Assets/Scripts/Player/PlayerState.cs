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
                && controller.canMove)
            {
                controller.SetState(new Move(controller)); return true;
            }
            return false;
        }

        public bool Paddle()
        {
            if (PlayerInput.InteractHold && controller.canPaddle)
            {
                controller.SetState(new Paddle(controller)); return true;
            }
            return false;
        }
    }

    class Idle : State
    {
        public Idle(Controller controller) : base(controller) {}

        public override void DoStateBehaviour()
        {
            controller.spriteRenderer.color = new UnityEngine.Color(255, 255, 255);
        }

        public override void Transitions()
        {
            if      (Move()) {}
            else if (Paddle()) {}
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
            controller.spriteRenderer.color = new UnityEngine.Color(127, 0, 0);
        }

        public override void Transitions()
        {
            if      (Idle()) {}
            else if (Paddle()) {}
        }

        private void MovePlayer()
        {
            if (Mathf.Abs(PlayerInput.Horizontal) > 0 || Mathf.Abs(PlayerInput.Vertical) > 0)
                controller.direction = new Vector2(PlayerInput.Horizontal, PlayerInput.Vertical).normalized;
            controller.velocity = controller.rigidbody2d.position + controller.direction * controller.Speed * Time.fixedDeltaTime;
            controller.rigidbody2d.MovePosition(controller.velocity);
            _prevPos = controller.rigidbody2d.position;
        }
    }

    class Paddle : State
    {
        public Paddle(Controller controller) : base(controller) {}

        public override void DoStateBehaviour()
        {
            controller.spriteRenderer.color = Color.green;
        }

        public override void DoStateBehaviourFixedUpdate()
        {
            Manager.RaftManager.Instance.MoveRaft(new Vector2(PlayerInput.Horizontal, PlayerInput.Vertical));
        }

        public override void Transitions()
        {
            if (PlayerInput.InteractHold) return;
            if      (Idle()) {}
            else if (Move()) {}
        }
    }
}