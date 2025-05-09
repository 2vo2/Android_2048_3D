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
            _cubeSpawner.OnNewCubeSpawned += OnNewCubeSpawned;
            _inputHandler.OnPressStarted += OnPressStarted;
            _inputHandler.OnPressCanceled += OnPressCanceled;
        }

        private void OnDisable()
        {
            _cubeSpawner.OnNewCubeSpawned -= OnNewCubeSpawned;
            _inputHandler.OnPressStarted -= OnPressStarted;
            _inputHandler.OnPressCanceled -= OnPressCanceled;
        }

        private void OnNewCubeSpawned(CubeUnit newCube)
        {
            CubeUnit = newCube;
        }

        protected virtual void OnPressStarted()
        {
            if (CubeUnit == null) return;

            _inputHandler.OnPerformedPointer += OnPerformedPointer;
        }

        protected virtual void OnPerformedPointer()
        {
            if (CubeUnit == null) return;
            
            PointerPosition = _inputHandler.GetWorldPointerPosition(CubeUnit.transform);
        }

        protected virtual void OnPressCanceled()
        {
            _inputHandler.OnPerformedPointer -= OnPerformedPointer;
        }
    }
}