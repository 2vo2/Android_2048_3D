using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class UIButton : MonoBehaviour
    {
        [SerializeField] private Button _uiButton;
        
        private void OnEnable()
        {
            _uiButton.onClick.AddListener( () =>
            {
                OnButtonClick();
            });
        }

        private void OnDisable()
        {
            _uiButton.onClick.RemoveListener( () =>
            {
                OnButtonClick();
            });
        }

        protected abstract void OnButtonClick();
    }
}