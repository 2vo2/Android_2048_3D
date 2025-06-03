using System;
using UnityEngine;

namespace Handlers.Game
{
    public class Wallet : MonoBehaviour
    {
        private GameScore _gameScore;
        private int _moneyValue;
        
        public event Action<int> OnMoneyChanged;
        
        public int MoneyValue => _moneyValue;

        private void Awake()
        {
            _gameScore = GameScore.Instance;
            _moneyValue = PlayerPrefs.GetInt("Coin", 0);
        }

        private void Start()
        {
            OnScoreThresholdReached(0);
        }

        private void OnEnable()
        {
            _gameScore.OnScoreThresholdReached += OnScoreThresholdReached;
        }

        private void OnDisable()
        {
            _gameScore.OnScoreThresholdReached -= OnScoreThresholdReached;
        }

        private void OnScoreThresholdReached(int value)
        {
            _moneyValue += value;
            PlayerPrefs.SetInt("Coin", _moneyValue);
            OnMoneyChanged?.Invoke(_moneyValue);
        }

        public void InvokeMoneyChanged(int moneyValue)
        {
            OnMoneyChanged?.Invoke(moneyValue);
        }
        
        public void DecreaseMoney(int amount)
        {
            if (amount < 0) return;
            
            _moneyValue -= amount;
            PlayerPrefs.SetInt("Coin", _moneyValue);
        }
    }
}