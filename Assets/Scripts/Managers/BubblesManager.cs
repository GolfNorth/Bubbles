﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Bubbles
{
    public sealed class BubblesManager : ITickable, IDisposable
    {
        private readonly List<BubbleController> _bubbleControllers;
        private readonly ObjectPool _objectPool;
        private readonly TimeManager _timeManager;
        private readonly UpdateManager _updateManager;
        private readonly BoundsManager _boundsManager;
        private readonly DifficultSettings _difficultSettings;
        private float _speedFactor;
        private float _passedAfterSpawn;

        public delegate void BubbleHitHandler(float radius);

        public event BubbleHitHandler BubbleHit;

        public float SpeedFactor => _speedFactor;

        public BubblesManager()
        {
            _speedFactor = 1f;
            _bubbleControllers = new List<BubbleController>();
            _objectPool = new ObjectPool(SceneContext.Instance.BubblePrefab);
            _difficultSettings = SceneContext.Instance.DifficultSettings;
            _timeManager = SceneContext.Instance.TimeManager;
            _timeManager.RoundEnded += OnTimeEnded;
            _updateManager = SceneContext.Instance.UpdateManager;
            _updateManager.Add(this);
            _boundsManager = SceneContext.Instance.BoundsManager;
        }

        public void Dispose()
        {
            _timeManager.RoundEnded -= OnTimeEnded;
            _updateManager.Remove(this);
        }

        private void OnTimeEnded()
        {
            _speedFactor = 1f;
            _passedAfterSpawn = 0;
            DestroyAll();
        }

        public void Tick()
        {
            if (!_timeManager.IsStarted || _bubbleControllers.Count >= _difficultSettings.MaxAmount) return;

            _speedFactor = 1f + (_difficultSettings.FinalSpeedFactor - 1f) * _timeManager.RelativeTimer;
            _passedAfterSpawn += Time.deltaTime;

            if (_bubbleControllers.Count == 0 || _passedAfterSpawn > _difficultSettings.SpawnDelay)
                Spawn();
        }

        public void Spawn()
        {
            var position = new Vector3();
            var bubble = _objectPool.Acquire(position);
            var controller = bubble.GetComponent<BubbleView>().Controller;
            controller.Speed = Random.Range(0, 100f) / 100f;
            controller.Position = new Vector3(
                Random.Range(_boundsManager.LeftBound, _boundsManager.RightBound),
                _boundsManager.BottomBound
            );
            controller.TargetPosition = new Vector3(
                controller.Position.x,
                _boundsManager.TopBound
            );

            _bubbleControllers.Add(controller);

            _passedAfterSpawn = 0;
        }

        public void Destroy(BubbleController controller, bool hit = false)
        {
            if (!_bubbleControllers.Remove(controller)) return;

            _objectPool.Release(controller.GameObject);

            if (hit)
                BubbleHit?.Invoke(controller.Radius);
        }

        private void DestroyAll()
        {
            foreach (var controller in _bubbleControllers) controller.Disable();
        }
    }
}