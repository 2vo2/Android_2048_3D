using System;
using UI;
using UnityEngine;

namespace Cube
{
    public class CubeMerger : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private float _minImpulseValueForMerge;
        [SerializeField] private float _tossMergeCubeValue;
        
        public event Action<int, Vector3> Merge;
        public event Action<Vector3> Hit;
        
        private void OnCollisionEnter(Collision other)
        {
            var impulseValue = _cubeUnit.Rigidbody.linearVelocity.sqrMagnitude;
            
            if (other.gameObject.TryGetComponent(out CubeUnit cubeUnit))
            {
                if (cubeUnit.CubeNumber == _cubeUnit.CubeNumber &&
                    impulseValue > _minImpulseValueForMerge)
                {
                    cubeUnit.gameObject.SetActive(false);
                    cubeUnit.CubeMerger.enabled = false;
                
                    var mergeValue = _cubeUnit.CubeNumber / 2;
                    Score.Instance.AddScore(mergeValue);

                    Merge?.Invoke(_cubeUnit.CubeNumber * 2, other.contacts[0].point);

                    TossMergeCube();
                }
                else
                {
                    Hit?.Invoke(other.contacts[0].point);
                }
            }
        }

        private void TossMergeCube()
        {
            var tossVector = new Vector3(0f, 1f, 1f);
            _cubeUnit.Rigidbody.AddForce(tossVector * _tossMergeCubeValue, ForceMode.Impulse);
        }
    }
}