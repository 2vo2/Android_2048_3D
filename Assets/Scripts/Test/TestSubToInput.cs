using System;
using UnityEngine;

namespace Test
{
    public class TestSubToInput : MonoBehaviour
    {
        [SerializeField] private GameObject _cube;
        [SerializeField] private TestInputSystem _testInputSystem;

        private void OnEnable()
        {
            _testInputSystem.StartClick += TestInputSystemOnStartClick;
            _testInputSystem.EndClick += TestInputSystemOnEndClick;
        }

        private void OnDisable()
        {
            _testInputSystem.StartClick -= TestInputSystemOnStartClick;
            _testInputSystem.HoldClick -= TestInputSystemOnHoldClick;
            _testInputSystem.EndClick -= TestInputSystemOnEndClick;
        }

        private void TestInputSystemOnStartClick()
        {
            print("Start click");
            _cube.SetActive(true);
            
            _testInputSystem.HoldClick += TestInputSystemOnHoldClick;
        }

        private void TestInputSystemOnHoldClick()
        {
            print("Hold click");
            
            var clampMousePositionX = Mathf.Clamp(_testInputSystem.GetWorldPointerPosition(_cube.transform).x, -4f, 4f);
            var newCubePosition = new Vector3(clampMousePositionX, _cube.transform.position.z, _cube.transform.position.z);
                
            _cube.transform.position = newCubePosition;
            
            print(_testInputSystem.GetWorldPointerPosition(_cube.transform));
        }

        private void TestInputSystemOnEndClick()
        {
            print("End click");
            _cube.SetActive(false);
        }
    }
}