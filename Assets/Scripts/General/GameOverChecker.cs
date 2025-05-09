using System;
using System.Collections;
using Cube;
using UnityEngine;

namespace General
{
    public class GameOverChecker : MonoBehaviour
    {
        [SerializeField] private float _timeToLeft;
        
        private float _timer;
        private Coroutine _gameOverCoroutine;
        
        public event Action GameOver;
        public event Action<float> TimeLeftChanged;
        public event Action TimerStarted;
        public event Action TimerStopped;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CubeUnit cubeUnit) &&
                !cubeUnit.IsMainCube)
            {
                if (_gameOverCoroutine == null)
                {
                    _gameOverCoroutine = StartCoroutine(GameOverTimer());
                    TimerStarted?.Invoke();
                } 
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CubeUnit cubeUnit) &&
                !cubeUnit.IsMainCube)
            {
                if (_gameOverCoroutine != null)
                {
                    StopCoroutine(_gameOverCoroutine);
                    _gameOverCoroutine = null;
                    _timer = 0f;
                    TimeLeftChanged?.Invoke(_timeToLeft);
                    TimerStopped?.Invoke();
                }
            }
        }

        private IEnumerator GameOverTimer()
        {
            while (_timeToLeft > _timer)
            {
                TimeLeftChanged?.Invoke(_timeToLeft - _timer);
                yield return new WaitForSeconds(1f);
                _timer++;
            }
            
            GameOver?.Invoke();
            TimerStopped?.Invoke();
        }
    }
}