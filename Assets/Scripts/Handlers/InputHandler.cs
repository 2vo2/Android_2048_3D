using Cube;
using UnityEngine;

namespace Handlers
{
    public class InputHandler : MonoBehaviour
    {
        private Vector3 RawMousePosition(Transform cubeTransform)
        {
            var mousePosition = Input.mousePosition;
            var depth = Vector3.Distance(Camera.main.transform.position, cubeTransform.position);
            mousePosition.z = depth;
            
            return mousePosition;
        }

        public Vector3 PreparedMousePosition(Transform cubeTransform)
        {
            return Camera.main.ScreenToWorldPoint(RawMousePosition(cubeTransform));
        }
    }
}
