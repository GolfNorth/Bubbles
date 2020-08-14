using System.Collections;
using UnityEngine;

namespace Bubbles
{
    public sealed class RoundManager
    {
        private bool _isStarted;
        private float _timer;
        private float _countdown;
        private GameState _gameState;
        private readonly RoundSettings _roundSettings;
        private readonly UpdateManager _updateManager;

        public delegate void RoundHandler();
        public event RoundHandler RoundCountdown;
        public event RoundHandler RoundStarted;
        public event RoundHandler RoundEnded;
        
        public RoundManager()
        {
            _roundSettings = SceneContext.Instance.RoundSettings;
            _updateManager = SceneContext.Instance.UpdateManager;

            Start();
        }

        public int Timer => Mathf.CeilToInt(_timer);
        
        public int Countdown => Mathf.CeilToInt(_countdown);

        public bool IsStarted => _isStarted;

        public GameState GameState => _gameState;

        public void Start()
        {
            if (_gameState == GameState.Countdown) return;
            
            _gameState = GameState.Countdown;
            _isStarted = false;
            _timer = 0;
            _countdown = _roundSettings.Сountdown;

            _updateManager.StartCoroutine(StartCountdown());
        }
        
        private IEnumerator StartCountdown()
        {
            RoundCountdown?.Invoke();
            
            while(_countdown > 0)
            {
                _countdown -= Time.deltaTime;
                
                yield return null;
            }

            _gameState = GameState.Started;
            _isStarted = true;

            RoundStarted?.Invoke();
            
            _updateManager.StartCoroutine(StartTimer());
        }
        
        private IEnumerator StartTimer()
        {
            var duration = _roundSettings.Duration;
            
            while(duration > _timer)
            {
                _timer += Time.deltaTime;
                
                yield return null;
            }

            _gameState = GameState.Ended;
            _isStarted = false;

            RoundEnded?.Invoke();
        }
    }
}