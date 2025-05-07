using UnityEngine;

namespace Cube
{
    [RequireComponent(typeof(Rigidbody))]
    public class CubeUnit : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
        public Rigidbody Rigidbody => _rigidbody;
    }
}