using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : MonoBehaviour
    {
        public readonly PlayerInput playerInput = new PlayerInput();
        public State stateMachine;
        [HideInInspector] public Rigidbody2D rigidbody2d;
        [HideInInspector] public Animator animator;
        [HideInInspector] public SpriteRenderer spriteRenderer;
        [HideInInspector] public Vector2 direction;
        [HideInInspector] public Vector2 velocity;

        public float speed = 5f;
        public bool canMove { get; private set; } = true;

        void Awake()
        {
            rigidbody2d = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            SetState(new Idle(this));
        }

        public void SetState(State state)
        {
            if (stateMachine != null)
                stateMachine.ExitState();

            stateMachine = state;
            stateMachine.EnterState();
        }

        public void EnableMovement() => canMove = true;
        public void DisableMovement() => canMove = false;

        void FixedUpdate()
        {
            stateMachine.DoStateBehaviourFixedUpdate();
        }

        void Update()
        {
            stateMachine.DoStateBehaviour();
            stateMachine.Transitions();
        }
    }
}
