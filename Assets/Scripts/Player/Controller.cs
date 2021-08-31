using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Controller : RaftObject
    {
        public static bool ControlsEnabled { get; private set; } = true;
        public static bool CanMove = true;
        public static bool CanInteract = true;

        [SerializeField] private string debug_state;
        [field: SerializeField] public GameObject SelectedItem { get; set; }
        [field: SerializeField] public GameObject HeldItem { get; set; }

        [SerializeField, Range(0, 10)] private float _speed = 5f;
        private float _speedMultiplier = 1f;
        public float Speed => _speed * _speedMultiplier;
        public void SetSpeedMultiplier(float multiplier) => _speedMultiplier = Mathf.Clamp(multiplier, 0.1f, 4f);

        public bool IsNearRaftEdge { get; private set; }

        public Animator Animator             { get; private set; }
        public Rigidbody2D Rigidbody2d       { get; private set; }
        public SpriteRenderer SpriteRenderer { get; private set; }
        public Hunger Hunger { get; private set; }

        public Vector2 Direction { get; set; }
        public Vector2 Velocity { get; set; }
        
        public State State { get; private set; }

        private void Awake()
        {
            Animator = GetComponent<Animator>();
            Rigidbody2d = GetComponent<Rigidbody2D>();
            SpriteRenderer = GetComponent<SpriteRenderer>();
            Hunger = GetComponent<Hunger>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Manager.GameManager.Instance.OnResume += EnableControls;
            Manager.GameManager.Instance.OnPause += DisableControls;
            Manager.GameManager.Instance.OnWin += DisableControls;
            Manager.GameManager.Instance.OnLose += DisableControls;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            Manager.GameManager.Instance.OnResume -= EnableControls;
            Manager.GameManager.Instance.OnPause -= DisableControls;
            Manager.GameManager.Instance.OnWin -= DisableControls;
            Manager.GameManager.Instance.OnLose -= DisableControls;
        }

        private void Start()
        {
            SetState(new Idle(this));
        }

        private void FixedUpdate()
        {
            State.FixedUpdate();
        }

        private void Update()
        {
            State.Update();
            State.Transitions();
            debug_state = State.GetType().Name;

            if (HeldItem != null)
            {
                GrabItem(HeldItem);
                if (Inputs.InteractAPress)
                    DropItem();
                else if (Inputs.InteractBPress)
                    UseItem();
            }
            else if (SelectedItem != null)
            {
                if (Inputs.InteractAPress || Inputs.InteractBPress)
                    SelectedItem.GetComponent<IInteractable>().Interact(gameObject);
            }
        }

        private void OnTriggerStay2D(Collider2D col)
        {
            switch (col.gameObject.tag)
            {
                case Tag.Food:
                case Tag.FoodSource:
                    SelectedItem = col.gameObject;
                    break;
                case Tag.Platform:
                    if (!(col is EdgeCollider2D)) return;
                    IsNearRaftEdge = true;
                    break;
                default:
                    return;
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            switch (col.gameObject.tag)
            {
                case Tag.Food:
                case Tag.FoodSource:
                    SelectedItem = null;
                    break;
                case Tag.Platform:
                    if (!(col is EdgeCollider2D)) return;
                    IsNearRaftEdge = false;
                    break;
                default:
                    return;
            }
        }

        public void SetState(State state)
        {
            State?.ExitState();
            State = state;
            State.EnterState();
        }

        public void GrabItem(GameObject obj)
        {
            HeldItem = obj;
            HeldItem.layer = LayerMask.NameToLayer(Layer.IgnorePlatform);
            HeldItem.transform.position = transform.position;
        }

        private void DropItem()
        {
            HeldItem.layer = LayerMask.NameToLayer(Layer.Default);
            HeldItem = null;
        }

        private void UseItem()
        {
            var item = HeldItem.GetComponent<IInteractable>();
            SetState(new UsingItem(this, item.UseDuration));
            item.Interact(gameObject);
        }

        private void EnableControls()
        {
            ControlsEnabled = true;
        }

        private void DisableControls()
        {
            ControlsEnabled = false;
        }
    }
}
