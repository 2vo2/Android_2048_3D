using Handlers;
using UnityEngine;

namespace Cube
{
    public abstract class CubeHandler : MonoBehaviour
    {
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private CubeSpawner _cubeSpawner;

        protected CubeUnit CubeUnit;
        protected Vector3 PointerPosition;

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
            CubeUnit = newCube;
        }

        protected virtual void OnPressStarted()
        {
            if (CubeUnit == null) return;

            _inputHandler.PerformedPointer += OnPerformedPointer;
        }

        protected virtual void OnPerformedPointer()
        {
            if (CubeUnit == null) return;
            
            PointerPosition = _inputHandler.GetWorldPointerPosition(CubeUnit.transform);
        }

        protected virtual void OnPressCanceled()
        {
            _inputHandler.PerformedPointer -= OnPerformedPointer;
        }
    }
}