using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Cube
{
    public class CubeSpawner : MonoBehaviour
    {
        [SerializeField] private CubeThrowers _cubeThrower;
        [SerializeField] private CubeUnit _cubePrefab;
        [SerializeField] private Transform _spawnPoint;

        private List<CubeUnit> _cubeUnits = new List<CubeUnit>();
        
        public event Action<CubeUnit> OnNewCubeSpawned;

        private void Start()
        {
            SpawnCube();
        }

        private void OnEnable()
        {
            _cubeThrower.OnCubeThrowed += OnCubeThrowed;
        }

        private void OnDisable()
        {
            _cubeThrower.OnCubeThrowed -= OnCubeThrowed;
        }

        private void SpawnCube()
        {
            var newCube = Instantiate(_cubePrefab, _spawnPoint.position, Quaternion.identity, transform);

            newCube.gameObject.layer = newCube.CubeUnitData.MainCubeLayer;
            newCube.SetMainCube(true);
            newCube.CubeViewer.SetCubeView();

            _cubeUnits.Add(newCube);
        
            OnNewCubeSpawned?.Invoke(newCube);
        }

        private void OnCubeThrowed(CubeUnit thrownCube)
        {
            StartCoroutine(WaitForCubeToStop(thrownCube));
        }

        private IEnumerator WaitForCubeToStop(CubeUnit cube)
        {
            const float threshold = 0.1f;
            const float delay = 0.1f;
            const float timeout = 3f;

            var cubeRigidbody = cube.Rigidbody;
            var timer = 0f;
            
            while (cubeRigidbody != null && cubeRigidbody.linearVelocity.sqrMagnitude > threshold)
            {
                yield return new WaitForSeconds(delay);
                
                timer += delay;

                if (timer >= timeout)
                {
                    break;
                }
            }

            cube.CubeMerger.enabled = true;
            
            TakeCubeFromPool();
        }

        private void TakeCubeFromPool()
        {
            for (int i = 0; i < _cubeUnits.Count; i++)
            { 
                var cubeUnit = _cubeUnits[i];
                
                if (!_cubeUnits[i].gameObject.activeSelf)
                {
                    ResetCube(cubeUnit);

                    cubeUnit.gameObject.SetActive(true);
                    cubeUnit.CubeMerger.enabled = false;  
                    
                    cubeUnit.SetMainCube(true);
                    cubeUnit.CubeViewer.SetCubeView();
                    
                    OnNewCubeSpawned?.Invoke(cubeUnit);
                    
                    return;
                }
            }
            
            SpawnCube();
        }

        private void ResetCube(CubeUnit cubeUnit)
        {
            cubeUnit.Rigidbody.linearVelocity = Vector3.zero;
            cubeUnit.Rigidbody.angularVelocity = Vector3.zero;
            cubeUnit.transform.position = _spawnPoint.position;
            cubeUnit.transform.rotation = Quaternion.identity;
        }
    }
}