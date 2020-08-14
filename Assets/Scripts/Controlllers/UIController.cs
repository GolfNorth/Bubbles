using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles
{
    public sealed class UIController : ITickable, IDisposable
    {
        private Text _scoreComponent;
        private Text _timerComponent;
        private Text _gameOverComponent;
        private Text _countdownComponent;
        private readonly RoundManager _roundManager;
        private readonly ScoreManager _scoreManager;
        private readonly UpdateManager _updateManager;

        public UIController(Text scoreComponent, Text timerComponent, Text gameOverComponent, Text countdownComponent)
        {
            _scoreComponent = scoreComponent;
            _timerComponent = timerComponent;
            _gameOverComponent = gameOverComponent;
            _countdownComponent = countdownComponent;

            Debug.Log(1);
            
            _roundManager = SceneContext.Instance.RoundManager;
            _roundManager.RoundCountdown += OnRoundCountdown;
            _roundManager.RoundStarted += OnRoundStarted;
            _roundManager.RoundEnded += OnRoundEnded;

            _scoreManager = SceneContext.Instance.ScoreManager;

            _updateManager = SceneContext.Instance.UpdateManager;
            _updateManager.Add(this);
            
            _scoreComponent.gameObject.SetActive(false);
            _timerComponent.gameObject.SetActive(false);
            _gameOverComponent.gameObject.SetActive(false);
            _countdownComponent.gameObject.SetActive(true);
        }

        public void Dispose()
        {
            _roundManager.RoundCountdown -= OnRoundCountdown;
            _roundManager.RoundStarted -= OnRoundStarted;
            _roundManager.RoundEnded -= OnRoundEnded;
            _updateManager.Remove(this);
        }
        
        private void OnRoundCountdown()
        {
            _gameOverComponent.gameObject.SetActive(false);
            _countdownComponent.gameObject.SetActive(true);
        }

        private void OnRoundStarted()
        {
            _scoreComponent.gameObject.SetActive(true);
            _timerComponent.gameObject.SetActive(true);
            _countdownComponent.gameObject.SetActive(false);
        }

        private void OnRoundEnded()
        {
            _scoreComponent.gameObject.SetActive(false);
            _timerComponent.gameObject.SetActive(false);
            _gameOverComponent.gameObject.SetActive(true);
            _countdownComponent.gameObject.SetActive(false);

            _gameOverComponent.text = $"Game over!\nYour score: {_scoreManager.Score}";
        }

        public void Tick()
        {
            switch (_roundManager.GameState)
            {
                case GameState.Countdown:
                    _countdownComponent.text = _roundManager.Countdown.ToString();
                    break;
                case GameState.Started:
                    _scoreComponent.text = $"Score: {_scoreManager.Score}";
                    _timerComponent.text = $"Timer: {_roundManager.Timer}";
                    break;
                case GameState.Ended:
                    if (Input.anyKey)
                        _roundManager.Start();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}