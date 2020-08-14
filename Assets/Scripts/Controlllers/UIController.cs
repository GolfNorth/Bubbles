using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bubbles
{
    public sealed class UIController : IController<UIController>, ITickable
    {
        private Text _scoreComponent;
        private Text _timerComponent;
        private Text _gameOverComponent;
        private Text _countdownComponent;
        private readonly TimeManager _timeManager;
        private readonly ScoreManager _scoreManager;
        private readonly UpdateManager _updateManager;

        public UIController(Text scoreComponent, Text timerComponent, Text gameOverComponent, Text countdownComponent)
        {
            _scoreComponent = scoreComponent;
            _timerComponent = timerComponent;
            _gameOverComponent = gameOverComponent;
            _countdownComponent = countdownComponent;
            
            _timeManager = SceneContext.Instance.TimeManager;
            _timeManager.RoundCountdown += OnTimeCountdown;
            _timeManager.RoundStarted += OnTimeStarted;
            _timeManager.RoundEnded += OnTimeEnded;

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
            _timeManager.RoundCountdown -= OnTimeCountdown;
            _timeManager.RoundStarted -= OnTimeStarted;
            _timeManager.RoundEnded -= OnTimeEnded;
            _updateManager.Remove(this);
        }
        
        private void OnTimeCountdown()
        {
            _gameOverComponent.gameObject.SetActive(false);
            _countdownComponent.gameObject.SetActive(true);
        }

        private void OnTimeStarted()
        {
            _scoreComponent.gameObject.SetActive(true);
            _timerComponent.gameObject.SetActive(true);
            _countdownComponent.gameObject.SetActive(false);
        }

        private void OnTimeEnded()
        {
            _scoreComponent.gameObject.SetActive(false);
            _timerComponent.gameObject.SetActive(false);
            _gameOverComponent.gameObject.SetActive(true);
            _countdownComponent.gameObject.SetActive(false);

            _gameOverComponent.text = $"Game over!\nYour score: {_scoreManager.Score}";
        }

        public void Tick()
        {
            switch (_timeManager.GameState)
            {
                case GameState.Countdown:
                    _countdownComponent.text = _timeManager.Countdown.ToString();
                    break;
                case GameState.Started:
                    _scoreComponent.text = $"Score: {_scoreManager.Score}";
                    _timerComponent.text = $"Timer: {_timeManager.Timer}";
                    break;
                case GameState.Ended:
                    if (Input.anyKey)
                        _timeManager.Start();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}