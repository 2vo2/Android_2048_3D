using System;
using UnityEngine;

namespace Cube.SO
{
    public class CubeSound : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private AudioSource _audioSource;

        private void OnEnable()
        {
            _cubeUnit.CubeMerger.Merge += PlaySound;
        }

        private void OnDisable()
        {
            _cubeUnit.CubeMerger.Merge -= PlaySound;
        }

        private void PlaySound(int value)
        {
            _audioSource.Play();
        }
    }
}