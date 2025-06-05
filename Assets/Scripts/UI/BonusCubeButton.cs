using Cube;
using Handlers.Game;
using UnityEngine;

namespace UI
{
    public class BonusCubeButton : UIButton
    {
        [SerializeField] private int _bonusCubeCost;
        [SerializeField] private CubeUnit _bonusCubeUnit;
        [SerializeField] private CubeSpawner _cubeSpawner;
        
        private Wallet _wallet;

        private void Awake()
        {
            _wallet = Wallet.Instance;
        }

        protected override void OnButtonClick()
        {
            if (_wallet.MoneyValue >= _bonusCubeCost)
            {
                _wallet.DecreaseMoney(_bonusCubeCost);
                _wallet.InvokeMoneyChanged(_wallet.MoneyValue);
                _cubeSpawner.SpawnBonusCube(_bonusCubeUnit);
            }
        }
    }
}