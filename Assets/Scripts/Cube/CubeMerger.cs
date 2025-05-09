using System;
using UI;
using UnityEngine;

namespace Cube
{
    public class CubeMerger : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private float _minImpulseValueForMerge;
        
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

                    _cubeUnit.Rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
                }
                else
                {
                    Hit?.Invoke(other.contacts[0].point);
                }
            }
        }
    }
}