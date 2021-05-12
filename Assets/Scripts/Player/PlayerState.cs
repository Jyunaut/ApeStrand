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
            if (Mathf.Abs(controller.playerInput.Horizontal) <= Mathf.Epsilon
                && Mathf.Abs(controller.playerInput.Vertical) <= Mathf.Epsilon)
            {
                controller.SetState(new Idle(controller)); return true;
            }
            return false;
        }

        public bool Move()
        {
            if ((controller.playerInput.Horizontal != 0
                || controller.playerInput.Vertical != 0)
                && controller.canMove)
            {
                controller.SetState(new Move(controller)); return true;
            }
            return false;
        }

        public bool Dodge()
        {
            if (controller.playerInput.Dodge
                && controller.canMove)
            {
                controller.SetState(new Dodge(controller)); return true;
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
            if      (Dodge()) {}
            else if (Move())  {}
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
            if      (Dodge()) {}
            else if (Idle())  {}
        }

        private void MovePlayer()
        {
            controller.direction = new Vector2(controller.playerInput.Horizontal, controller.playerInput.Vertical).normalized;
            controller.velocity = controller.rigidbody2d.position + controller.direction * controller.Speed * Time.fixedDeltaTime;
            controller.rigidbody2d.MovePosition(controller.velocity);
            _prevPos = controller.rigidbody2d.position;
        }
    }

    class Dodge : State
    {
        private float _dodgeSpeed = 2f;
        private float _dodgeTimer;

        public Dodge(Controller controller) : base(controller) {}

        public override void EnterState()
        {
            controller.DisableMovement();

            _dodgeTimer = 0.2f;
        }

        public override void DoStateBehaviourFixedUpdate()
        {
            controller.spriteRenderer.color = new UnityEngine.Color(0, 127, 0);
            controller.rigidbody2d.MovePosition(controller.rigidbody2d.position + controller.direction * _dodgeSpeed * _dodgeTimer);

            _dodgeTimer -= Time.fixedDeltaTime;
            if (_dodgeTimer <= 0) controller.EnableMovement();
        }

        public override void Transitions()
        {
            if (_dodgeTimer > 0) return;
            if      (Move()) {}
            else if (Idle()) {}
        }
    }
}