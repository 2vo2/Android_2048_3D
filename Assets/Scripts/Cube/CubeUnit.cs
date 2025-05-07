using UnityEngine;

namespace Cube
{
    [RequireComponent(typeof(Rigidbody))]
    public class CubeUnit : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private bool _isMainCube;
        
        public Rigidbody Rigidbody => _rigidbody;
        public bool IsMainCube => _isMainCube;

        public void SetMainCube(bool isMainCube)
        {
            _isMainCube = isMainCube;
        }
    }
}