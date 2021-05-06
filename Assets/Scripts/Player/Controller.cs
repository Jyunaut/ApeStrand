using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : MonoBehaviour
    {
        public readonly PlayerInput playerInput = new PlayerInput();
        public PlayerState playerState;
        [HideInInspector] public Rigidbody2D rigidbody2d;
        [HideInInspector] public Animator animator;
        [HideInInspector] public SpriteRenderer spriteRenderer;
        [HideInInspector] public Vector2 direction = new Vector2(0, 0);

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

        public void SetState(PlayerState state)
        {
            if (playerState != null)
                playerState.ExitState();

            playerState = state;
            playerState.EnterState();
        }

        public void EnableMovement() => canMove = true;
        public void DisableMovement() => canMove = false;

        void FixedUpdate()
        {
            playerState.DoStateBehaviourFixedUpdate();
        }

        void Update()
        {
            playerState.DoStateBehaviour();
            playerState.Transitions();
        }
    }
}
