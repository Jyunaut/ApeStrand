using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : RaftObject
    {
        [SerializeField] [Range(0, 10)]
        private float _speed = 5f;
        private float _speedMultiplier = 1f;
        public float Speed { get => _speed * _speedMultiplier; }

        public bool canMove { get; private set; } = true;
        [HideInInspector]
        public Vector2 direction;
        [HideInInspector]
        public Vector2 velocity;

        public readonly PlayerInput playerInput = new PlayerInput();
        public Animator animator             { get; private set; }
        public Rigidbody2D rigidbody2d       { get; private set; }
        public SpriteRenderer spriteRenderer { get; private set; }
        
        private State _stateMachine;

        void Awake()
        {
            animator = GetComponent<Animator>();
            rigidbody2d = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void Start()
        {
            base.Start();
            SetState(new Idle(this));
        }

        public void SetState(State state)
        {
            if (_stateMachine != null)
                _stateMachine.ExitState();

            _stateMachine = state;
            _stateMachine.EnterState();
        }

        public void SetSpeedMultiplier(float multiplier) => _speedMultiplier = Mathf.Clamp(multiplier, 0.1f, 4f);
        public void ResetSpeedMultiplier() => _speedMultiplier = 1f;
        public void EnableMovement() => canMove = true;
        public void DisableMovement() => canMove = false;

        void FixedUpdate()
        {
            _stateMachine.DoStateBehaviourFixedUpdate();
        }

        void Update()
        {
            _stateMachine.DoStateBehaviour();
            _stateMachine.Transitions();
        }
    }
}
