using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Handlers.Game
{
    public class GameScore : MonoBehaviour
    {
        public static GameScore Instance;

        [SerializeField] private int _scoreThreshold = 10;
        [SerializeField] private int _moneyForScore = 1;
        
        private int _scoreValue;
        private int _highScoreValue;
        private int _nextScoreThreshold;
        
        public int ScoreValue => _scoreValue;
        public int HighScoreValue => _highScoreValue;
        
        public event Action<int> OnScoreChanged; 
        public event Action<int> OnHighScoreChanged;
        public event Action<int> OnScoreThresholdReached;

        public void Initialize()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance == this)
                Destroy(gameObject);
            
            _highScoreValue = PlayerPrefs.GetInt("HighScore", 0);
            _nextScoreThreshold = _scoreThreshold;
            OnHighScoreChanged?.Invoke(_highScoreValue);
            
            DontDestroyOnLoad(gameObject);
        }
        
        public void AddScore(int value)
        {
            if (value < 0) return;
            
            _scoreValue += value;

            if (_scoreValue >= _nextScoreThreshold)
            {
                _nextScoreThreshold += _scoreThreshold;
                OnScoreThresholdReached?.Invoke(_moneyForScore);
            }
            
            if (_scoreValue > _highScoreValue)
            {
                _highScoreValue = _scoreValue;
                PlayerPrefs.SetInt("HighScore", _highScoreValue);
                OnHighScoreChanged?.Invoke(_highScoreValue);
            }
            
            OnScoreChanged?.Invoke(_scoreValue);
        }

        public void ResetHighScore()
        {
            PlayerPrefs.DeleteKey("HighScore");
            _highScoreValue = 0;
            OnHighScoreChanged?.Invoke(_highScoreValue);
        }
    }
}