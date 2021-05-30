using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : RaftObject
    {
        [SerializeField] private string debug_state;

        [SerializeField, Range(0, 10)]
        private float _speed = 5f;
        private float _speedMultiplier = 1f;
        public float Speed { get => _speed * _speedMultiplier; }
        public void SetSpeedMultiplier(float multiplier) => _speedMultiplier = Mathf.Clamp(multiplier, 0.1f, 4f);
        public bool CanMove { get; set; } = true;
        public bool CanPaddle { get; private set; }
        public bool UsingItem { get; set; }
        public float UseDuration { get; set; }
        [field: SerializeField] public GameObject SelectedItem { get; set; }
        [field: SerializeField] public GameObject HeldItem { get; set; }

        public Animator Animator             { get; private set; }
        public Rigidbody2D Rigidbody2d       { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Hunger Hunger { get; private set; }

        public Vector2 Direction { get; set; }
        public Vector2 Velocity { get; set; }
        
        private State _stateMachine;

        void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody2d = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Hunger = GetComponent<Hunger>();
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
            switch (col.gameObject.tag)
            {
                case Tag.Food:
                case Tag.FoodSource:
                    SelectedItem = col.gameObject;
                    break;
                case Tag.Platform:
                    if (!(col is EdgeCollider2D)) return;
                    CanPaddle = true;
                    break;
                default:
                    return;
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            switch (col.gameObject.tag)
            {
                case Tag.Food:
                case Tag.FoodSource:
                    SelectedItem = null;
                    break;
                case Tag.Platform:
                    if (!(col is EdgeCollider2D)) return;
                    CanPaddle = false;
                    break;
                default:
                    return;
            }
        }

        public void GrabItem(GameObject obj)
        {
            HeldItem = obj;
            HeldItem.layer = LayerMask.NameToLayer("Ignore Platform");
            HeldItem.transform.position = transform.position;
        }

        void DropItem()
        {
            HeldItem.layer = LayerMask.NameToLayer("Default");
            HeldItem = null;
        }

        void UseItem()
        {
            HeldItem.GetComponent<IInteractable>().Interact(gameObject);
            UsingItem = true;
        }

        void FixedUpdate()
        {
            _stateMachine.DoStateBehaviourFixedUpdate();
        }

        void Update()
        {
            _stateMachine.DoStateBehaviour();
            _stateMachine.Transitions();
            debug_state = _stateMachine.GetType().Name;

            if (HeldItem != null)
            {
                GrabItem(HeldItem);
                if (PlayerInput.Interact_A)
                    DropItem();
                else if (PlayerInput.Interact_B)
                    UseItem();
            }
            else if (SelectedItem != null)
            {
                switch (SelectedItem.tag)
                {
                    case Tag.FoodSource:
                        if (PlayerInput.Interact_A)
                        {
                            GameObject food = Hunger.SpawnFood();
                            GrabItem(food);
                        }
                        break;
                    case Tag.Food:
                        if (PlayerInput.Interact_A)
                        {
                            GrabItem(SelectedItem);
                        }
                        break;
                }
            }
        }
    }
}
