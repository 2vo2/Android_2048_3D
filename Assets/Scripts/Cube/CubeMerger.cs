using System;
using Interface;
using UI;
using UnityEngine;

namespace Cube
{
    public abstract class CubeMerger : MonoBehaviour, ICubeMergeHandler
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private float _tossMergeCubeValue;
        [SerializeField] protected float _minImpulseValueForMerge;

        public event Action<int, Vector3> OnCubeMerged;
        public event Action<Vector3> OnCubeHitted;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out CubeUnit cubeUnit))
            {
                MergeHandle(_cubeUnit, cubeUnit);
            }
        }

        protected void InvokeCubeMerged(int value, Vector3 position)
        {
            OnCubeMerged?.Invoke(value, position);
        }

        protected void InvokeCubeHitted(Vector3 position)
        {
            OnCubeHitted?.Invoke(position);
        }
        
        protected void TossMergeCube()
        {
            var tossVector = new Vector3(0f, 1f, 1f);
            _cubeUnit.Rigidbody.AddForce(tossVector * _tossMergeCubeValue, ForceMode.Impulse);
        }

        public abstract void MergeHandle(CubeUnit self, CubeUnit other);
    }
}