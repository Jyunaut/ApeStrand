using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public float IslandDistance { get; private set; }
        [SerializeField] private float _startDistance = 250f;
        [SerializeField] private float debug_islandDistance;
        [SerializeField] private State debug_gameState;

        [Serializable]
        public enum State { Game, Menu, Win, Lose }
        public enum Flag { Paused }

        public static State GameState { get; private set; } = State.Menu;
        public Dictionary<Flag, bool> GameFlags = new Dictionary<Flag, bool>();

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            foreach (Flag flag in Enum.GetValues(typeof(Flag)))
                GameFlags.Add(flag, false);
        }

        void Start()
        {
            IslandDistance = _startDistance;
        }

        public static void SetGameState(State state)
        {
            GameState = state;
        }

        void StateGame()
        {
            IslandDistance -= Time.deltaTime;
            debug_islandDistance = IslandDistance;

            if (IslandDistance <= 0)
            {
                print("Ayy you win!");
                GameState = State.Win;
            }
        }

        void StateMenu()
        {

        }

        void StateWin()
        {
            print("We're winning!");
        }

        void StateLose()
        {

        }

        void Update()
        {
            debug_gameState = GameState;
            switch (GameState)
            {
                case State.Game:
                    StateGame();
                    break;
                case State.Menu:
                    StateMenu();
                    break;
                case State.Win:
                    StateWin();
                    break;
                case State.Lose:
                    StateLose();
                    break;
            }
        }
    }
}