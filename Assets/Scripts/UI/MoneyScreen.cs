using System;
using Handlers.Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MoneyScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;

        private void Start()
        {
            OnMoneyChanged(Wallet.Instance.MoneyValue);
        }

        private void OnEnable()
        {
            Wallet.Instance.OnMoneyChanged += OnMoneyChanged;
        }

        private void OnDisable()
        {
            Wallet.Instance.OnMoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int value)
        {
            _moneyText.text = value.ToString();   
        }
    }
}