using System;
using Interface;
using UnityEngine;

namespace Handlers.Game
{
    public class Wallet : MonoBehaviour, IInitializable
    {
        public static Wallet Instance;
        
        [SerializeField] private GameScore _gameScore;
        private int _moneyValue;
        
        public event Action<int> OnMoneyChanged;
        
        public int MoneyValue => _moneyValue;

        public void Initialize()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance == this)
                Destroy(gameObject);
            
            _moneyValue = PlayerPrefs.GetInt("Coin", 0);
            OnMoneyChanged?.Invoke(_moneyValue);
            
            DontDestroyOnLoad(gameObject);
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