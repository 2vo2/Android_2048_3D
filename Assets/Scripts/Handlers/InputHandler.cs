using System;
using UnityEngine;

namespace Handlers
{
    public class InputHandler : MonoBehaviour
    {
        private TouchScreenAction _touchScreenAction;

        public event Action PressStarted;
        public event Action PerformedPointer;
        public event Action PressCanceled;

        private void Awake()
        {
            Init();
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

            _touchScreenAction.TouchScreen.PressScreen.started += ctx => PressStarted?.Invoke();
            _touchScreenAction.TouchScreen.TouchPosition.performed += ctx => PerformedPointer?.Invoke();
            _touchScreenAction.TouchScreen.PressScreen.canceled += ctx => PressCanceled?.Invoke();
        }
        
        public Vector3 GetWorldPointerPosition(Transform referenceTransform)
        {
            var depth = Vector3.Distance(Camera.main.transform.position, referenceTransform.position);
            var screenPos = _touchScreenAction.TouchScreen.TouchPosition.ReadValue<Vector2>();
            
            return Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, depth));
        }
    }
}
