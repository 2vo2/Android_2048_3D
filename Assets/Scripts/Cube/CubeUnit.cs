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
        [SerializeField] private CubeSound _cubeSound;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private GameCubeSO _gameCubeData;
        [SerializeField] private List<TMP_Text> _texts;

        private bool _isMainCube;
        private int _cubeNumber;
        
        public Rigidbody Rigidbody => _rigidbody;
        public CubeMerger CubeMerger => _cubeMerger;
        public CubeSound CubeSound => _cubeSound;
        public bool IsMainCube => _isMainCube;
        public int CubeNumber => _cubeNumber;

        private void SetCubeNumber(int cubeNumber)
        {
            _cubeNumber = cubeNumber;
        }

        public void SetMainCube(bool isMainCube)
        {
            _isMainCube = isMainCube;
        }

        public void SetCubeView()
        {
            SetCubeNumber(_gameCubeData.CubeNumber());

            foreach (var tmpText in _texts)
            {
                tmpText.text = _cubeNumber.ToString();
            }
            
            var cubeColor = _gameCubeData.CubeColor(_cubeNumber);
            _meshRenderer.material.color = cubeColor;
        }

        public void SetCubeView(int cubeNumber)
        {
            SetCubeNumber(cubeNumber);
            
            foreach (var tmpText in _texts)
            {
                tmpText.text = cubeNumber.ToString();
            }
            
            var cubeColor = _gameCubeData.CubeColor(cubeNumber);
            _meshRenderer.material.color = cubeColor;
        }
    }
}