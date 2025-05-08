using System;
using UI;
using UnityEngine;

namespace Cube
{
    public class CubeMerger : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private float _minImpulseValueForMerge;
        
        public event Action Merge;
        
        private void OnCollisionEnter(Collision other)
        {
            var impulseValue = _cubeUnit.Rigidbody.linearVelocity.sqrMagnitude;
            
            if (other.gameObject.TryGetComponent(out CubeUnit cubeUnit) &&
                cubeUnit.CubeNumber == _cubeUnit.CubeNumber &&
                impulseValue > _minImpulseValueForMerge)
            {
                cubeUnit.gameObject.SetActive(false);
                cubeUnit.CubeMerger.enabled = false;
                
                var mergeValue = _cubeUnit.CubeNumber / 2;
                Score.Instance.AddScore(mergeValue);
                
                Merge?.Invoke();
                
                _cubeUnit.SetCubeView(_cubeUnit.CubeNumber * 2);

                _cubeUnit.Rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
        }
    }
}