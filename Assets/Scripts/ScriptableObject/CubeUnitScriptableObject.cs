using System.Collections.Generic;
using Cube;
using UnityEngine;

namespace ScriptableObject
{
    [CreateAssetMenu(fileName = "New CubeUnit Data", menuName = "CubeUnit Data", order = 0)]
    public class CubeUnitScriptableObject : UnityEngine.ScriptableObject
    {
        [SerializeField] private List<int> _chances;
        [SerializeField] private int _mainCubeLayer;
        [SerializeField] private int _onBoardCubeLayer;

        public int MainCubeLayer => _mainCubeLayer;
        public int OnBoardCubeLayer => _onBoardCubeLayer;
        
        public int CubeNumber()
        {
            var roll = Random.Range(0, 100);
            var cumulative = 0;

            for (int i = 0; i < _chances.Count; i++)
            {
                cumulative += _chances[i];
                if (roll < cumulative)
                    return (int)Mathf.Pow(2, i + 1);
            }
            
            return (int)Mathf.Pow(2, _chances.Count);
        }

        public Color CubeColor(int cubeNumber)
        {
            var n = (int)Mathf.Log(cubeNumber, 2);

            var hue = (n * 47f) % 360f / 360f;
            var saturation = 0.7f;
            var valueBrightness = Mathf.Lerp(1f, 0.4f, n / 30f);

            return Color.HSVToRGB(hue, saturation, valueBrightness);
        }

        public void SetCubeLayer(CubeUnit cubeUnit, int layer)
        {
            if (cubeUnit == null) return;
            
            cubeUnit.gameObject.layer = layer;
        }
    }
}