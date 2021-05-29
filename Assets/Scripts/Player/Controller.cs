using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : RaftObject
    {
        [SerializeField] private bool debug_canInteract;
        [SerializeField] private bool debug_canPaddle;
        [SerializeField] private string debug_state;

        [SerializeField, Range(0, 10)]
        private float _speed = 5f;
        private float _speedMultiplier = 1f;
        public float Speed { get => _speed * _speedMultiplier; }
        public void SetSpeedMultiplier(float multiplier) => _speedMultiplier = Mathf.Clamp(multiplier, 0.1f, 4f);
        public bool CanMove { get; set; } = true;
        public bool CanPaddle { get; private set; } = false;

        public Animator Animator             { get; private set; }
        public Rigidbody2D Rigidbody2d       { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }

        public Vector2 Direction { get; set; }
        public Vector2 Velocity { get; set; }
        
        private State _stateMachine;

        void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody2d = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void Start()
        {
            base.Start();
            SetState(new Idle(this));
        }

        public void SetState(State state)
        {
            _stateMachine?.ExitState();
            _stateMachine = state;
            _stateMachine.EnterState();
        }

        void OnTriggerStay2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                if (!(col is EdgeCollider2D)) return;
                CanPaddle = true;
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                if (!(col is EdgeCollider2D)) return;
                CanPaddle = false;
            }
        }

        void FixedUpdate()
        {
            _stateMachine.DoStateBehaviourFixedUpdate();
        }

        void Update()
        {
            _stateMachine.DoStateBehaviour();
            _stateMachine.Transitions();
            debug_canPaddle = CanPaddle;
            debug_state = _stateMachine.GetType().Name;
        }
    }
}
