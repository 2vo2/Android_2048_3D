using System;
using UnityEngine;

namespace Handlers
{
    public class InputHandler : MonoBehaviour
    {
        private TouchScreenAction _touchScreenAction;

        private Camera _mainCamera;
        
        public event Action OnPressStarted;
        public event Action OnPerformedPointer;
        public event Action OnPressCanceled;

        private void Awake()
        {
            Init();
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _touchScreenAction.TouchScreen.Enable();
        }

        private void OnDisable()
        {
            _touchScreenAction.TouchScreen.Disable();
        }

        private void Init()
        {
            _touchScreenAction = new TouchScreenAction();

            _touchScreenAction.TouchScreen.PressScreen.started += _ => OnPressStarted?.Invoke();
            _touchScreenAction.TouchScreen.TouchPosition.performed += _ => OnPerformedPointer?.Invoke();
            _touchScreenAction.TouchScreen.PressScreen.canceled += _ => OnPressCanceled?.Invoke();
        }
        
        public Vector3 GetWorldPointerPosition(Transform referenceTransform)
        {
            var depth = Vector3.Distance(_mainCamera.transform.position, referenceTransform.position);
            var screenPos = _touchScreenAction.TouchScreen.TouchPosition.ReadValue<Vector2>();
            
            return _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, depth));
        }
    }
}
