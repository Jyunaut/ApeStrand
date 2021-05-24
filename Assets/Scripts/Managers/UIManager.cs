using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private GameObject _gameUI;
        [SerializeField] private GameObject _titleMenuUI;
        [SerializeField] private GameObject _pauseMenuUI;
        [SerializeField] private GameObject _pauseButton;
        [SerializeField] private float _previousTimeScale = 1f;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void StartGame()
        {
            GameManager.SetGameState(GameManager.State.Game);
            print("Game Started");
            
            _gameUI?.SetActive(true);
            _titleMenuUI?.SetActive(false);
            _pauseButton?.SetActive(true);
        }

        public void Resume()
        {
            if (GameManager.GameState == GameManager.State.Game)
                return;
            GameManager.SetGameState(GameManager.State.Game);
            Time.timeScale = _previousTimeScale;
            print("Resume Game");

            _gameUI?.SetActive(true);
            _pauseMenuUI?.SetActive(false);
            _pauseButton?.SetActive(true);
        }

        public void Pause()
        {
            if (GameManager.GameState == GameManager.State.Menu)
                return;
            GameManager.SetGameState(GameManager.State.Menu);
            _previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            print("Pause Game");
            
            _pauseMenuUI?.SetActive(true);
            _pauseButton?.SetActive(false);
        }
    }
}
