﻿using UnityEngine;

namespace Bubbles
{
    public sealed class SceneContext : Singleton<SceneContext>
    {
        [SerializeField] private DifficultSettings _difficultSettings;
        [SerializeField] private RoundSettings _roundSettings;
        [SerializeField] private ScoreSettings _scoreSettings;
        [SerializeField] private GameObject _bubblePrefab;
        
        private UpdateManager _updateManager;
        private TimeManager _timeManager;
        private BubblesManager _bubblesManager;
        private ScoreManager _scoreManager;

        public UpdateManager UpdateManager => _updateManager;

        public TimeManager TimeManager => _timeManager;

        public BubblesManager BubblesManager => _bubblesManager;

        public ScoreManager ScoreManager => _scoreManager;

        public GameObject BubblePrefab => _bubblePrefab;

        public DifficultSettings DifficultSettings => _difficultSettings;

        public RoundSettings RoundSettings => _roundSettings;
        
        public ScoreSettings ScoreSettings => _scoreSettings;

        private void Awake()
        {
            _updateManager = new UpdateManager();
            _timeManager = new TimeManager();
            _bubblesManager = new BubblesManager();
            _scoreManager = new ScoreManager();
        }
    }
}