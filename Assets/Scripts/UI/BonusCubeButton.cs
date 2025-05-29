using Cube;
using Handlers.Game;
using UnityEngine;

namespace UI
{
    public class BonusCubeButton : UIButton
    {
        [SerializeField] private int _bonusCubeCost;
        [SerializeField] private Wallet _wallet;
        [SerializeField] private CubeUnit _bonusCubeUnit;
        [SerializeField] private CubeSpawner _cubeSpawner;
        
        protected override void OnButtonClick()
        {
            if (_bonusCubeCost >= _wallet.MoneyValue)
            {
                _wallet.DecreaseMoney(_bonusCubeCost);
                _wallet.InvokeMoneyChanged(_wallet.MoneyValue);
                _cubeSpawner.SpawnBonusCube(_bonusCubeUnit);
            }
        }
    }
}