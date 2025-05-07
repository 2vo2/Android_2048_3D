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
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private GameCubeSO _gameCubeData;
        [SerializeField] private List<TMP_Text> _texts;

        private bool _isMainCube;
        
        public Rigidbody Rigidbody => _rigidbody;
        public bool IsMainCube => _isMainCube;

        public void SetMainCube(bool isMainCube)
        {
            _isMainCube = isMainCube;
        }

        public void SetCubeView()
        {
            var cubeNumber = _gameCubeData.SetCubeNumber();

            foreach (var tmpText in _texts)
            {
                tmpText.text = cubeNumber.ToString();
            }
            
            var cubeColor = _gameCubeData.SetCubeColor(cubeNumber);
            _meshRenderer.material.color = cubeColor;
        }
    }
}