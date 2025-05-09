using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Cube.SO
{
    public class CubeSfx : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private AudioSource _mergeSfx;
        [SerializeField] private AudioSource _hitSfx;

        private void OnEnable()
        {
            _cubeUnit.CubeMerger.Merge += PlayMergeSfx;
            _cubeUnit.CubeMerger.Hit += PlayHitSfx;
        }

        private void OnDisable()
        {
            _cubeUnit.CubeMerger.Merge -= PlayMergeSfx;
            _cubeUnit.CubeMerger.Hit -= PlayHitSfx;
        }

        private void PlayMergeSfx(int value, Vector3 position)
        {
            _mergeSfx.Play();
        }

        private void PlayHitSfx(Vector3 position)
        {
            _hitSfx.Play();
        }
    }
}