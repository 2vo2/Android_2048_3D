using System.Collections.Generic;
using Cube.SO;
using TMPro;
using UnityEngine;

namespace Cube
{
    [RequireComponent(typeof(Rigidbody))]
    public class CubeUnit : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CubeMerger _cubeMerger;
        [SerializeField] private CubeViewer _cubeViewer;
        [SerializeField] private CubeSound _cubeSound;

        private bool _isMainCube;
        private int _cubeNumber;
        
        public Rigidbody Rigidbody => _rigidbody;
        public CubeMerger CubeMerger => _cubeMerger;
        public CubeViewer CubeViewer => _cubeViewer;
        public bool IsMainCube => _isMainCube;
        public int CubeNumber => _cubeNumber;
        
        public void SetMainCube(bool isMainCube)
        {
            _isMainCube = isMainCube;
        }
        
        public void SetCubeNumber(int cubeNumber)
        {
            if (cubeNumber < 2) return;
            
            _cubeNumber = cubeNumber;
        }
    }
}