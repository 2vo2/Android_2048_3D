using Cube;
using UnityEngine;

namespace Handlers
{
    public class CubeThrowers : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private InputHandler _inputHandler;
        [SerializeField] private float _throwForce;
        
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var clampMousePositionX = Mathf.Clamp(_inputHandler.PreparedMousePosition(_cubeUnit.transform).x, -4f, 4f);
                var newCubePosition = new Vector3(clampMousePositionX, _cubeUnit.transform.position.z, _cubeUnit.transform.position.z);
                
                _cubeUnit.transform.position = newCubePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                var mousedDirectionZ = _cubeUnit.transform.position.z -
                                       _inputHandler.PreparedMousePosition(_cubeUnit.transform).z;
                var throwDirection = new Vector3(0f, 0f, mousedDirectionZ);
                
                _cubeUnit.Rigidbody.linearVelocity = throwDirection * _throwForce;
            }
        }
    }
}