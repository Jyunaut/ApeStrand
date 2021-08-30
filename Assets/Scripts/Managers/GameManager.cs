using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public delegate void GameStateChange();
        public event GameStateChange OnWin, OnLose, OnPause, OnResume;

        [field: SerializeField] public float IslandDistance { get; private set; }
        [SerializeField] private float _distanceTravelled;
        [SerializeField] private float _startDistance;
        public float DistanceTravelled
        {
            get
            {
                return _distanceTravelled;
            }
            set
            {
                _distanceTravelled = value;
                if (_distanceTravelled >= IslandDistance && GameState == State.Game)
                    SetGameState(State.Win);
            }
        }

        public enum State { Game, Menu, Win, Lose }

        [SerializeField] private State _gameState;
        public State GameState
        {
            get
            {
                return _gameState;
            }
            set
            {
                State temp = _gameState;
                _gameState = value;
                switch (value)
                {
                    case State.Win when temp == State.Game:
                        OnWin?.Invoke();
                        break;
                    case State.Lose when temp == State.Game:
                        OnLose?.Invoke();
                        break;
                    case State.Menu:
                        OnPause?.Invoke();
                        break;
                    case State.Game:
                        OnResume?.Invoke();
                        break;
                }
            }
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Start()
        {
            SetGameState(State.Menu);
            DistanceTravelled = _startDistance;
        }

        public void SetGameState(State state)
        {
            GameState = state;
        }

        private void Update()
        {
            if (GameState == State.Game)
            {
                DistanceTravelled += Time.deltaTime;
            }
        }
    }
}