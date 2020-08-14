using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles
{
    public class UIView : MonoBehaviour
    {
        [SerializeField] private Text _scoreComponent;
        [SerializeField] private Text _timerComponent;
        [SerializeField] private Text _gameOverComponent;
        [SerializeField] private Text _countdownComponent;
        private UIController _controller;

        private void Awake()
        {
            _controller = new UIController(_scoreComponent, _timerComponent, _gameOverComponent, _countdownComponent);
        }

        private void OnDestroy()
        {
            _controller.Dispose();
        }
    }
}