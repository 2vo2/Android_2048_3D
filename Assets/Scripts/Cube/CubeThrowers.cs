using System;
using Cube;
using UnityEngine;

namespace Handlers
{
    public class CubeThrowers : CubeHandler
    {
        [SerializeField] private float _throwForce;

        public event Action<CubeUnit> Throw;

        protected override void OnPressCanceled()
        {
            if (CubeUnit == null) return;

            if (CubeUnit.IsMainCube)
            {
                ThrowCube();
            }
            
            base.OnPressCanceled();
        }
        
        private void ThrowCube()
        {
            CubeUnit.Rigidbody.linearVelocity = Vector3.forward * _throwForce;
            Throw?.Invoke(CubeUnit);

            CubeUnit.SetMainCube(false);
            CubeUnit = null;
        }
    }
}