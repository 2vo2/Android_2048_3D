using System.Collections.Generic;
using Cube.SO;
using TMPro;
using UnityEngine;

namespace Cube
{
    public class CubeViewer : MonoBehaviour
    {
        [SerializeField] private GameCubeSO _gameCubeData;
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private List<TMP_Text> _texts;
        
        public void SetCubeView()
        {
            _cubeUnit.SetCubeNumber(_gameCubeData.CubeNumber());

            foreach (var tmpText in _texts)
            {
                tmpText.text = _cubeUnit.CubeNumber.ToString();
            }
            
            var cubeColor = _gameCubeData.CubeColor(_cubeUnit.CubeNumber);
            _meshRenderer.material.color = cubeColor;
        }
        
        public void SetCubeView(int cubeNumber)
        {
            _cubeUnit.SetCubeNumber(cubeNumber);
            
            foreach (var tmpText in _texts)
            {
                tmpText.text = cubeNumber.ToString();
            }
            
            var cubeColor = _gameCubeData.CubeColor(cubeNumber);
            _meshRenderer.material.color = cubeColor;
        }
    }
}