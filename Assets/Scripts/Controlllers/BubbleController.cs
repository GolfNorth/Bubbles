using System;
using UnityEngine;

namespace Bubbles
{
    public class BubbleController : ITickable, IDisposable
    {
        private bool _active;
        private BubbleModel _model;
        private GameObject _gameObject;
        private UpdateManager _updateManager;
        private BubblesManager _bubblesManager;

        public GameObject GameObject => _gameObject;

        public float Radius
        {
            get => _model.Radius;
            set => _model.Radius = value;
        }
        
        public float Speed
        {
            get => _model.Speed;
            set => _model.Speed = value;
        }
        
        public BubbleController(GameObject gameObject)
        {
            _active = true;
            _model = new BubbleModel();
            _gameObject = gameObject;
            _bubblesManager = SceneContext.Instance.BubblesManager; 
            _updateManager = SceneContext.Instance.UpdateManager;
            _updateManager.Add(this);
        }

        public void Dispose()
        {
            _updateManager?.Remove(this);
        }
        
        public void Disable()
        {
            if (!_active) return;
            
            _active = false;
        }

        public void Enable()
        {
            if (_active) return;
            
            _active = true;
        }
        
        public void Hit()
        {
            if (!_active) return;
            
            _bubblesManager.Destroy(this);
        }

        public void Tick()
        {
            if (!_active) return;
            
            Debug.Log("Bubble");
        }
    }
}