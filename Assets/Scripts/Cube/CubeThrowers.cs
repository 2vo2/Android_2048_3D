using System;
using Cube;
using UnityEngine;

namespace Handlers
{
    public class CubeThrowers : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private CubeSpawner _cubeSpawner;
        [SerializeField] private float _throwForce;

        private CubeUnit _cubeUnit;
        private Vector3 _mousePosition;

        public event Action<CubeUnit> Throw;

        private void OnEnable()
        {
            _cubeSpawner.SpawnNewCube += OnSpawnNewCube;
        }

        private void OnDisable()
        {
            _cubeSpawner.SpawnNewCube -= OnSpawnNewCube;
        }

        private void OnSpawnNewCube(CubeUnit newCube)
        {
            _cubeUnit = newCube;
        }
        
        private void Update()
        {
            if (!_cubeUnit) return;
            
            MousePosition();

            if (_cubeUnit.IsMainCube)
            {
                if (Input.GetMouseButton(0))
                {
                    MoveCube();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    ThrowCube();
                }
            }
        }

        private void MousePosition()
        {
            _mousePosition = _inputHandler.PreparedMousePosition(_cubeUnit.transform);;
        }
        
        private void MoveCube()
        {
            var clampMousePositionX = Mathf.Clamp(_mousePosition.x, -4f, 4f);
            var newCubePosition = new Vector3(clampMousePositionX, _cubeUnit.transform.position.z, _cubeUnit.transform.position.z);
                
            _cubeUnit.transform.position = newCubePosition;
        }

        private void ThrowCube()
        {
            var mousedDirectionZ = _cubeUnit.transform.position.z -
                                   _mousePosition.z;
            
            if (mousedDirectionZ >= 0.5f)
            {
                var throwDirection = new Vector3(0f, 0f, mousedDirectionZ);
                _cubeUnit.Rigidbody.linearVelocity = throwDirection * _throwForce;
                
                Throw?.Invoke(_cubeUnit);
                
                _cubeUnit.SetMainCube(false);
                _cubeUnit = null;
            }
        }
    }
}