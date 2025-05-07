using Handlers;
using UnityEngine;
using System;
using System.Collections;

namespace Cube
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private CubeThrowers _cubeThrower;
        [SerializeField] private CubeUnit _cubePrefab;

        public event Action<CubeUnit> SpawnNewCube;

        private void Start()
        {
            SpawnCube();
        }

        private void OnEnable()
        {
            _cubeThrower.Throw += OnCubeThrow;
        }

        private void OnDisable()
        {
            _cubeThrower.Throw -= OnCubeThrow;
        }

        private void OnCubeThrow(CubeUnit thrownCube)
        {
            StartCoroutine(WaitForCubeToStop(thrownCube));
        }

        private IEnumerator WaitForCubeToStop(CubeUnit cube)
        {
            const float threshold = 0.1f;
            const float delay = 0.1f;

            var cubeRigidbody = cube.Rigidbody;
            
            while (cubeRigidbody != null && cubeRigidbody.linearVelocity.sqrMagnitude > threshold)
            {
                yield return new WaitForSeconds(delay);
            }

            SpawnCube();
        }
        
        private void SpawnCube()
        {
            var newCube = Instantiate(_cubePrefab, transform.position, Quaternion.identity);
            newCube.SetMainCube(true);
            
            SpawnNewCube?.Invoke(newCube);
        }
    }
}