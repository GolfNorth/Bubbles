using UnityEngine;
using UnityEngine.UI;

namespace Bubbles
{
    public class UIView : BaseView<UIController>
    {
        [SerializeField] private Text _scoreComponent;
        [SerializeField] private Text _timerComponent;
        [SerializeField] private Text _gameOverComponent;
        [SerializeField] private Text _countdownComponent;

        private void Awake()
        {
            Controller = new UIController(_scoreComponent, _timerComponent, _gameOverComponent, _countdownComponent);
        }

        private void OnDestroy()
        {
            Controller.Dispose();
        }
    }
}