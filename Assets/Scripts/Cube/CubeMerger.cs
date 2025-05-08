using System;
using UI;
using UnityEngine;

namespace Cube
{
    public class CubeMerger : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out CubeUnit cubeUnit) &&
                cubeUnit.CubeNumber == _cubeUnit.CubeNumber)
            {
                cubeUnit.gameObject.SetActive(false);
                cubeUnit.CubeMerger.enabled = false;

                var mergeValue = _cubeUnit.CubeNumber / 2;
                Score.Instance.AddScore(mergeValue);
                
                _cubeUnit.SetCubeView(_cubeUnit.CubeNumber * 2);

                _cubeUnit.Rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
        }
    }
}