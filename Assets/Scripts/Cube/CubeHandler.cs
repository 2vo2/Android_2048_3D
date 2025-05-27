using Handlers;
using UnityEngine;
using UnityEngine.EventSystems;

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

            StartCoroutine(DelayedPressStart());
        }

        private System.Collections.IEnumerator DelayedPressStart()
        {
            yield return null; // чекаємо один кадр

            if (EventSystem.current.IsPointerOverGameObject()) yield break;

            _inputHandler.OnPerformedPointer += OnPerformedPointer;
        }

        protected virtual void OnPerformedPointer()
        {
            if (CubeUnit == null) return;
            
            PointerPosition = _inputHandler.GetWorldPointerPosition(CubeUnit.transform);
        }

        protected virtual void OnPressCanceled()
        {
            StartCoroutine(DelayedPressCanceled());
        }

        private System.Collections.IEnumerator DelayedPressCanceled()
        {
            yield return null; // чекаємо один кадр, щоб EventSystem оновився

            if (EventSystem.current.IsPointerOverGameObject()) yield break;

            _inputHandler.OnPerformedPointer -= OnPerformedPointer;
        }
    }
}