using Handlers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class UIButton : MonoBehaviour
    {
        [SerializeField] private Button _uiButton;
        [SerializeField] private InputHandler _inputHandler;

        private void OnEnable()
        {
            _inputHandler.OnUIClicked += RegisterUIListener;
        }

        private void OnDisable()
        {
            _inputHandler.OnUIClicked -= RegisterUIListener;
            _uiButton.onClick.RemoveListener(OnButtonClick);
        }

        private void RegisterUIListener()
        {
            _uiButton.onClick.RemoveListener(OnButtonClick);
            _uiButton.onClick.AddListener(OnButtonClick);
        }

        protected abstract void OnButtonClick();
    }
}