using UnityEngine;

namespace Cube
{
    public class CubeVfx : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private ParticleSystem _mergeSfx;
        [SerializeField] private ParticleSystem _hitSfx;

        private void OnEnable()
        {
            _cubeUnit.CubeMerger.Merge += PlayMergeVfx;
            _cubeUnit.CubeMerger.Hit += PlayHitVfx;
        }

        private void OnDisable()
        {
            _cubeUnit.CubeMerger.Merge -= PlayMergeVfx;
            _cubeUnit.CubeMerger.Hit -= PlayHitVfx;
        }

        private void PlayMergeVfx(int value, Vector3 position)
        {
            _mergeSfx.transform.position = position;
            
            _mergeSfx.Play();
        }

        private void PlayHitVfx(Vector3 position)
        {
            _hitSfx.transform.position = position;
            
            _hitSfx.Play();
        }
    }
}