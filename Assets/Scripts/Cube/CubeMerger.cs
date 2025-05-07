using System;
using UI;
using UnityEngine;

namespace Cube
{
    public class CubeMerger : MonoBehaviour
    {
        private CubeUnit _cubeUnit;

        private void Awake()
        {
            _cubeUnit = GetComponent<CubeUnit>();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out CubeUnit cubeUnit) &&
                cubeUnit.CubeNumber == _cubeUnit.CubeNumber)
            {
                _cubeUnit.gameObject.SetActive(false);

                var mergeValue = cubeUnit.CubeNumber / 2;
                Score.Instance.AddScore(mergeValue);
                
                cubeUnit.SetCubeView(cubeUnit.CubeNumber * 2);

                cubeUnit.Rigidbody.AddForce(Vector3.up * 10, ForceMode.Impulse);
            }
        }
    }
}