using Cube;
using Handlers.Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GameOverScreen : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameOverArea _gameOverArea;
        [SerializeField] private CubeHandler _cubeHandler;


        [Header("CanvasGroups")]
        [SerializeField] private CanvasGroup _gameOverScreen;
        [SerializeField] private CanvasGroup _levelScreen;
        [SerializeField] private CanvasGroup _timerScreen;
        
        [Header("Texts")]
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _highScoreText;
        
        private void OnEnable()
        {
            _gameOverArea.OnGameOver += OnGameOver;
            _gameOverArea.OnTimeToLoseChanged += OnTimeToLoseChanged;
            _gameOverArea.OnTimerStarted += OnTimeStarted;
            _gameOverArea.OnTimerStopped += OnTimerStopped;
        }

        private void OnDisable()
        {
            _gameOverArea.OnGameOver -= OnGameOver;
            _gameOverArea.OnTimeToLoseChanged -= OnTimeToLoseChanged;
            _gameOverArea.OnTimerStarted -= OnTimeStarted;
            _gameOverArea.OnTimerStopped -= OnTimerStopped;
        }

        private void OnGameOver()
        {
            _cubeHandler.gameObject.SetActive(false);
            _gameOverArea.gameObject.SetActive(false);
            
            EnableCanvasGroup(_gameOverScreen, 1f, true, true);
            EnableCanvasGroup(_levelScreen, 0f, false, false);
            
            _scoreText.text = $"SCORE: {GameScore.Instance.ScoreValue}";
            _highScoreText.text = $"HIGHSCORE: {GameScore.Instance.HighScoreValue}";
        }

        private void EnableCanvasGroup(CanvasGroup canvasGroup, float alpha, bool interactable, bool blocksRaycasts)
        {
            canvasGroup.alpha = alpha;
            canvasGroup.interactable = interactable;
            canvasGroup.blocksRaycasts = blocksRaycasts;
        }

        private void OnTimeToLoseChanged(float time)
        {
            _timerText.text = $"Time to Lose: {Mathf.CeilToInt(time)}";
        }

        private void OnTimeStarted()
        {
            EnableCanvasGroup(_timerScreen, 1f, false, false);
        }

        private void OnTimerStopped()
        {
            EnableCanvasGroup(_timerScreen, 0f, false, false);
        }
    }
}