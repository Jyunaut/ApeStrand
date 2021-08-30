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

        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void OnEnable()
        {
            GameManager.Instance.OnWin += ShowWinUI;
            GameManager.Instance.OnLose += ShowLoseUI;
        }

        private void OnDisable()
        {
            GameManager.Instance.OnWin -= ShowWinUI;
            GameManager.Instance.OnLose -= ShowLoseUI;
        }

        private void Start()
        {
            _gameUI?.SetActive(false);
            _titleMenuUI?.SetActive(true);
            _pauseMenuUI?.SetActive(false);
        }

        public void PlayButton()
        {
            GameManager.Instance.SetGameState(GameManager.State.Game);
            print("Game Started");
            
            _gameUI?.SetActive(true);
            _titleMenuUI?.SetActive(false);
            _pauseButton?.SetActive(true);
        }

        public void ResumeButton()
        {
            if (GameManager.Instance.GameState == GameManager.State.Game)
                return;
            GameManager.Instance.SetGameState(GameManager.State.Game);
            Time.timeScale = _previousTimeScale;
            print("Resume Game");

            _gameUI?.SetActive(true);
            _pauseMenuUI?.SetActive(false);
            _pauseButton?.SetActive(true);
        }

        public void PauseButton()
        {
            if (GameManager.Instance.GameState == GameManager.State.Menu)
                return;
            GameManager.Instance.SetGameState(GameManager.State.Menu);
            _previousTimeScale = Time.timeScale;
            Time.timeScale = 0f;
            print("Pause Game");
            
            _pauseMenuUI?.SetActive(true);
            _pauseButton?.SetActive(false);
        }

        public void ShowWinUI()
        {
            Debug.Log("ayy you win");
        }

        public void ShowLoseUI()
        {
            Debug.Log("ayy you lose");
        }
    }
}
