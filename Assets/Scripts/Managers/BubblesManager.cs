using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bubbles
{
    public sealed class BubblesManager : ITickable, IDisposable
    {
        private readonly List<BubbleController> _bubbleControllers;
        private readonly ObjectPool _objectPool;
        private readonly RoundManager _roundManager;
        private readonly UpdateManager _updateManager;
        private readonly DifficultSettings _difficultSettings;
        private float _passedAfterSpawn;
        
        public delegate void BubbleDestroyedHandler(float radius);
        public event BubbleDestroyedHandler BubbleDestroyed;

        public BubblesManager()
        {
            _bubbleControllers = new List<BubbleController>();
            _objectPool = new ObjectPool(SceneContext.Instance.BubblePrefab);

            _difficultSettings = SceneContext.Instance.DifficultSettings;
            _roundManager = SceneContext.Instance.RoundManager;
            _roundManager.RoundEnded += OnRoundEnded;
            _updateManager = SceneContext.Instance.UpdateManager;
            _updateManager.Add(this);
        }

        public void Dispose()
        {
            _roundManager.RoundEnded -= OnRoundEnded;
            _updateManager.Remove(this);
        }

        private void OnRoundEnded()
        {
            DestroyAll();
        }

        public void Tick()
        {
            if (!_roundManager.IsStarted || _bubbleControllers.Count >= _difficultSettings.MaxAmount) return;

            _passedAfterSpawn += Time.deltaTime;
            
            if (_bubbleControllers.Count == 0 || _passedAfterSpawn > _difficultSettings.SpawnDelay)
                Spawn();
        }

        public void Spawn()
        {
            var position = new Vector3();
            var bubble = _objectPool.Acquire(position: position);
            var controller = bubble.GetComponent<BubbleView>().Controller;
            
            _bubbleControllers.Add(controller);

            _passedAfterSpawn = 0;
        }

        public void Destroy(BubbleController controller)
        {
            if (!_bubbleControllers.Remove(controller)) return;
                
            _objectPool.Release(controller.GameObject);
            
            if (_roundManager.IsStarted)
                BubbleDestroyed?.Invoke(controller.Radius);
        }

        private void DestroyAll()
        {
            foreach (var controller in _bubbleControllers)
            {
                controller.Disable();
            }
        }
    }
}