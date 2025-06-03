using UnityEngine;
using UnityEngine.SceneManagement;

namespace Handlers.Game
{
    public class Bootstraper : MonoBehaviour
    {
        [SerializeField] private GameScore _gameScore;
        [SerializeField] private Wallet _wallet;
        
        private void Awake()
        {
            _gameScore.Initialize();
            _wallet.Initialize();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}