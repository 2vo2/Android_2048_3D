using System.Collections;
using UnityEngine;

namespace Cube
{
    public class CubeSfx : MonoBehaviour
    {
        [SerializeField] private CubeUnit _cubeUnit;
        [SerializeField] private AudioSource _mergeSfx;
        [SerializeField] private AudioSource _hitSfx;

        private void OnEnable()
        {
            _cubeUnit.CubeMerger.OnCubeMerged += OnCubeMerged;
            _cubeUnit.CubeMerger.OnCubeHitted += OnCubeHitted;
        }

        private void OnDisable()
        {
            _cubeUnit.CubeMerger.OnCubeMerged -= OnCubeMerged;
            _cubeUnit.CubeMerger.OnCubeHitted -= OnCubeHitted;
        }

        private void OnCubeMerged(int value)
        {
            PlaySFX(_mergeSfx);
        }

        private void OnCubeHitted()
        {
            PlaySFX(_hitSfx);
        }

        private void PlaySFX(AudioSource sfx)
        {
            sfx.transform.SetParent(null);
            StartCoroutine(WaitSFX(sfx, sfx.time));
        }

        private IEnumerator WaitSFX(AudioSource sfxPrefab, float duration)
        {
            sfxPrefab.Play();
            yield return new WaitForSeconds(duration);
            sfxPrefab.transform.SetParent(transform);
            sfxPrefab.transform.localPosition = Vector3.zero;
        }
    }
}