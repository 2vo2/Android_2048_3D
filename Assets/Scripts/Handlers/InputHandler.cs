using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Handlers
{
    public class InputHandler : MonoBehaviour
    {
        private TouchScreenAction _touchScreenAction;
        private InputSystem_Actions _inputSystemActions;

        private Camera _mainCamera;
        
        public event Action OnPressStarted;
        public event Action OnPerformedPointer;
        public event Action OnPressCanceled;
        public event Action OnUIClicked;
        
        public bool ClickedUIThisFrame { get; private set; }

        private void Awake()
        {
            Init();
            _mainCamera = Camera.main;
        }

        private void OnEnable()
        {
            _touchScreenAction.TouchScreen.Enable();
            _inputSystemActions.UI.Enable();
        }

        private void LateUpdate()
        {
            ClickedUIThisFrame = EventSystem.current.IsPointerOverGameObject();
        }

        private void OnDisable()
        {
            _touchScreenAction.TouchScreen.Disable();
            _inputSystemActions.UI.Disable();
        }

        private void Init()
        {
            _touchScreenAction = new TouchScreenAction();
            _inputSystemActions = new InputSystem_Actions();

            _touchScreenAction.TouchScreen.PressScreen.started += _ => OnPressStarted?.Invoke();
            _touchScreenAction.TouchScreen.TouchPosition.performed += _ => OnPerformedPointer?.Invoke();
            _touchScreenAction.TouchScreen.PressScreen.canceled += _ => OnPressCanceled?.Invoke();
            
            _inputSystemActions.UI.Click.performed += _ => OnUIClicked?.Invoke();
        }
        
        public Vector3 GetWorldPointerPosition(Transform referenceTransform)
        {
            var depth = Vector3.Distance(_mainCamera.transform.position, referenceTransform.position);
            var screenPos = _touchScreenAction.TouchScreen.TouchPosition.ReadValue<Vector2>();
            
            return _mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, depth));
        }
    }
}
