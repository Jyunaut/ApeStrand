using UnityEngine;
using Manager;

namespace Player
{
    public class Hunger : MonoBehaviour
    {
        [SerializeField] private float _maxHunger;
        [field: SerializeField] public float CurrentHunger { get; private set; }
        [SerializeField, Range(0, 2)] private float _hungerDecaySpeed;
        public float MaxHunger
        { 
            get => _maxHunger;
            set
            {
                _maxHunger = value;
                CurrentHunger = Mathf.Clamp(CurrentHunger, 0, _maxHunger);
            }
        }
        void OnValidate() => CurrentHunger = Mathf.Clamp(CurrentHunger, 0, _maxHunger);

        private Controller _controller;

        public void DecreaseHunger(float amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Negative values are not allowed");
                return;
            }
            CurrentHunger -= amount;
            if (CurrentHunger < 0)
                CurrentHunger = 0;
        }

        public void IncreaseHunger(float amount)
        {
            if (amount < 0)
            {
                Debug.LogWarning("Negative values are not allowed");
                return;
            }
            CurrentHunger += amount;
            if (CurrentHunger > MaxHunger)
                CurrentHunger = MaxHunger;
        }

        void Awake()
        {
            _controller = GetComponent<Controller>();
        }

        void Start()
        {
            CurrentHunger = MaxHunger;
        }

        void Update()
        {
            if (CurrentHunger <= 0 && GameManager.GameState != GameManager.State.Lose)
                GameManager.SetGameState(GameManager.State.Lose);
            else if (GameManager.GameState == GameManager.State.Game)
                DecreaseHunger(Time.deltaTime * _hungerDecaySpeed);
        }
    }
}
