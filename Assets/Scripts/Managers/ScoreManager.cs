using System;
using UnityEngine;

namespace Bubbles
{
    public class ScoreManager : IDisposable
    {
        private float _score;
        private readonly DifficultSettings _difficultSettings;
        private readonly ScoreSettings _scoreSettings;
        private readonly TimeManager _timeManager;
        private readonly BubblesManager _bubblesManager;
        
        public ScoreManager()
        {
            _difficultSettings = SceneContext.Instance.DifficultSettings;
            _scoreSettings = SceneContext.Instance.ScoreSettings;
            _timeManager = SceneContext.Instance.TimeManager;
            _bubblesManager = SceneContext.Instance.BubblesManager;
            
            _bubblesManager.BubbleDestroyed += OnBubbleDestroyed;
            _timeManager.RoundStarted += OnTimeStarted;
        }
        
        public void Dispose()
        {
            _bubblesManager.BubbleDestroyed -= OnBubbleDestroyed;
            _timeManager.RoundStarted -= OnTimeStarted;
        }

        private void OnTimeStarted()
        {
            _score = 0;
        }

        private void OnBubbleDestroyed(float radius)
        {
            _score += _scoreSettings.AveragePoints * radius /
                      (_difficultSettings.MaxRadius - _difficultSettings.MinRadius);
        }

        public int Score => Mathf.CeilToInt(_score);
    }
}