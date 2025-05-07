using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Test
{
    public class TestInputSystem : MonoBehaviour
    {
        private TestAction _testAction;

        public event Action StartClick;
        public event Action HoldClick;
        public event Action EndClick;
        
        private void Awake()
        {
            _testAction = new TestAction();
            
            _testAction.TestMap.TestActionClick.started += ctx => StartClick?.Invoke();
            _testAction.TestMap.TestActionPos.performed += ctx => HoldClick?.Invoke();
            _testAction.TestMap.TestActionClick.canceled += ctx => EndClick?.Invoke();
        }

        private void OnEnable() => _testAction.Enable();
        private void OnDisable() => _testAction.Disable();

        public Vector3 GetWorldPointerPosition(Transform referenceTransform)
        {
            float depth = Vector3.Distance(Camera.main.transform.position, referenceTransform.position);
            Vector2 screenPos = _testAction.TestMap.TestActionPos.ReadValue<Vector2>();
            return Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, depth));
        }
    }
}