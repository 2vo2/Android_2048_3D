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
        private Vector3 _pointerPosition;

        public event Action<CubeUnit> Throw;
        

        private void OnEnable()
        {
            _cubeSpawner.SpawnNewCube += OnSpawnNewCube;
            _inputHandler.PressStarted += OnPressStarted;
            _inputHandler.PressCanceled += OnPressCanceled;
        }

        private void OnDisable()
        {
            _cubeSpawner.SpawnNewCube -= OnSpawnNewCube;
            _inputHandler.PressStarted -= OnPressStarted;
            _inputHandler.PressCanceled -= OnPressCanceled;
        }

        private void OnSpawnNewCube(CubeUnit newCube)
        {
            _cubeUnit = newCube;
        }

        private void OnPressStarted()
        {
            if (_cubeUnit == null) return;
            
            _inputHandler.PerformedPointer += OnPerformedPointer;
        }

        private void OnPerformedPointer()
        {
            if (_cubeUnit == null) return;
            
            _pointerPosition = _inputHandler.GetWorldPointerPosition(_cubeUnit.transform);
            
            if (_cubeUnit.IsMainCube)
            {
                MoveCube();
            }
        }
        
        private void OnPressCanceled()
        {
            if (_cubeUnit == null) return;
            
            if (_cubeUnit.IsMainCube)
            {
                ThrowCube();
            }
            
            _inputHandler.PerformedPointer -= OnPerformedPointer;
        }
        
        private void MoveCube()
        {
            var clampPointerPositionX = Mathf.Clamp(_pointerPosition.x, -4f, 4f);
            var newCubePosition = new Vector3(clampPointerPositionX, _cubeUnit.transform.position.z, _cubeUnit.transform.position.z);
                
            _cubeUnit.transform.position = newCubePosition;
        }

        private void ThrowCube()
        {
            var throwDirectionZ = _cubeUnit.transform.position.z -
                                   _pointerPosition.z;
            
            if (throwDirectionZ >= 0.5f)
            {
                var throwVector = new Vector3(0f, 0f, throwDirectionZ);
                _cubeUnit.Rigidbody.linearVelocity = throwVector * _throwForce;
                
                Throw?.Invoke(_cubeUnit);
                
                _cubeUnit.SetMainCube(false);
                _cubeUnit = null;
            }
        }
    }
}