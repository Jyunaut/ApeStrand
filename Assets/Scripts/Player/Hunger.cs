using UnityEngine;
using Manager;

namespace Player
{
    public class Hunger : MonoBehaviour
    {
        [SerializeField] private float _maxHunger;
        [SerializeField] private float _currentHunger;
        public float CurrentHunger
        {
            get
            {
                return _currentHunger;
            }
            set
            {
                _currentHunger = value;
                if (_currentHunger <= 0f)
                    Manager.GameManager.Instance.SetGameState(Manager.GameManager.State.Lose);
            }
        }
        [SerializeField, Range(0f, 2f)] private float _hungerDecaySpeed;
        public float MaxHunger
        { 
            get
            {
                return _maxHunger;
            }
            set
            {
                _maxHunger = value;
                CurrentHunger = Mathf.Clamp(CurrentHunger, 0f, _maxHunger);
            }
        }
        void OnValidate() => CurrentHunger = Mathf.Clamp(CurrentHunger, 0f, _maxHunger);

        public void DecreaseHunger(float amount)
        {
            if (amount < 0f)
            {
                Debug.LogWarning("Negative values are not allowed", this);
                return;
            }
            CurrentHunger -= amount;
            if (CurrentHunger < 0f)
                CurrentHunger = 0f;
        }

        public void IncreaseHunger(float amount)
        {
            if (amount < 0f)
            {
                Debug.LogWarning("Negative values are not allowed", this);
                return;
            }
            CurrentHunger += amount;
            if (CurrentHunger > MaxHunger)
                CurrentHunger = MaxHunger;
        }

        void Start()
        {
            CurrentHunger = MaxHunger;
        }

        void Update()
        {
            if (GameManager.Instance.GameState == GameManager.State.Game)
                DecreaseHunger(Time.deltaTime * _hungerDecaySpeed);
        }
    }
}
