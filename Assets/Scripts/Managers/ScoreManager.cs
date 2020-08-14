using System;
using UnityEngine;

namespace Bubbles
{
    public class ScoreManager : IDisposable
    {
        private float _score;
        private readonly DifficultSettings _difficultSettings;
        private readonly ScoreSettings _scoreSettings;
        private readonly RoundManager _roundManager;
        private readonly BubblesManager _bubblesManager;
        
        public ScoreManager()
        {
            _difficultSettings = SceneContext.Instance.DifficultSettings;
            _scoreSettings = SceneContext.Instance.ScoreSettings;
            _roundManager = SceneContext.Instance.RoundManager;
            _bubblesManager = SceneContext.Instance.BubblesManager;
            
            _bubblesManager.BubbleDestroyed += OnBubbleDestroyed;
            _roundManager.RoundStarted += OnRoundStarted;
        }
        
        public void Dispose()
        {
            _bubblesManager.BubbleDestroyed -= OnBubbleDestroyed;
            _roundManager.RoundStarted -= OnRoundStarted;
        }

        private void OnRoundStarted()
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