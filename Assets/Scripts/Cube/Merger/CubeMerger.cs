using System;
using Interface;
using UI;
using UnityEngine;

namespace Cube.Merger
{
    public abstract class CubeMerger : MonoBehaviour, ICubeMergeHandler
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private float _minImpulseValueForMerge;
        [SerializeField] private float _tossForce;

        public event Action<int> OnCubeMerged;
        public event Action OnCubeHitted;
        
        private void OnCollisionEnter(Collision other)
        {
            var impulseValue = _cubeUnit.Rigidbody.linearVelocity.sqrMagnitude;
            
            if (other.gameObject.TryGetComponent(out CubeUnit cubeUnit))
            {
                if (impulseValue > _minImpulseValueForMerge)
                {
                    MergeCube(_cubeUnit, cubeUnit);
                }
                
                OnCubeHitted?.Invoke();
            }
        }

        protected void TossMergeCube(CubeUnit cubeUnit)
        {
            var toosVector = new Vector3(0f, 1f, 1f);
            cubeUnit.Rigidbody.AddForce(toosVector * _tossForce, ForceMode.Impulse);
        }

        protected void EnableMergeCube(CubeUnit cubeUnit, bool enable)
        {
            cubeUnit.gameObject.SetActive(enable);
            cubeUnit.CubeMerger.enabled = enable;
        }

        protected void AddMergeValueToScore(CubeUnit cubeUnit)
        {
            var mergeValue = cubeUnit.CubeNumber / 2;
            GameScore.Instance.AddScore(mergeValue);
        }

        public void InvokeCubeMerged(int cubeNumber)
        {
            OnCubeMerged?.Invoke(cubeNumber);
        }

        public abstract void MergeCube(CubeUnit self, CubeUnit other);
    }
}