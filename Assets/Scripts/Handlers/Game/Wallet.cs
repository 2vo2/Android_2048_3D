using System;
using UnityEngine;

namespace Handlers.Game
{
    public class Wallet : MonoBehaviour
    {
        private GameScore _gameScore;
        private int _moneyValue = 1000;
        
        public event Action<int> OnMoneyChanged;
        
        public int MoneyValue => _moneyValue;

        private void Awake()
        {
            _gameScore = GameScore.Instance;
        }

        private void Start()
        {
            OnScoreThresholdReached();
        }

        private void OnEnable()
        {
            _gameScore.OnScoreThresholdReached += OnScoreThresholdReached;
        }

        private void OnDisable()
        {
            _gameScore.OnScoreThresholdReached -= OnScoreThresholdReached;
        }

        private void OnScoreThresholdReached()
        {
            _moneyValue++;
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
        }
    }
}