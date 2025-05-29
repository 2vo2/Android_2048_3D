using System;
using Handlers.Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class MoneyScreen : MonoBehaviour
    {
        [SerializeField] private Wallet _wallet;
        [SerializeField] private TMP_Text _moneyText;

        private void OnEnable()
        {
            _wallet.OnMoneyChanged += OnMoneyChanged;
        }

        private void OnDisable()
        {
            _wallet.OnMoneyChanged -= OnMoneyChanged;
        }

        private void OnMoneyChanged(int value)
        {
            _moneyText.text = value.ToString();   
        }
    }
}