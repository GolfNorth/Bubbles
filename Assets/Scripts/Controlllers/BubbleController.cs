using UnityEngine;

namespace Bubbles
{
    public class BubbleController : IController<BubbleController>, IPoolable, ITickable
    {
        private BubbleView _view;
        private GameObject _gameObject;
        private Transform _transform;
        private SphereCollider _collider;
        private Vector3 _targetPosition;
        private readonly BubbleModel _model;
        private readonly GameObject _prefab;
        private readonly UpdateManager _updateManager;
        private readonly BubblesManager _bubblesManager;
        private readonly DifficultSettings _difficultSettings;

        public GameObject GameObject => _gameObject;

        public float Radius => _model.Radius;

        public Vector3 Position
        {
            get => _transform.position;
            set => _transform.position = value;
        }

        public Vector3 TargetPosition
        {
            set => _targetPosition = value;
        }

        public float Speed
        {
            set
            {
                _model.Radius = _difficultSettings.MinRadius +
                                (_difficultSettings.MaxRadius - _difficultSettings.MinRadius) * (1 - value);
                _model.Speed = _difficultSettings.MinSpeed +
                               (_difficultSettings.MaxSpeed - _difficultSettings.MinSpeed) * value;
                _collider.radius = _model.Radius;

                var scale = _model.Radius * 2;
                _transform.localScale = new Vector3(scale, scale, scale);
            }
        }

        public BubbleController()
        {
            _model = new BubbleModel();
            _bubblesManager = SceneContext.Instance.BubblesManager;
            _updateManager = SceneContext.Instance.UpdateManager;
            _updateManager.Add(this);
            _difficultSettings = SceneContext.Instance.DifficultSettings;
            _prefab = SceneContext.Instance.BubblePrefab;
        }

        public void Dispose()
        {
            _updateManager?.Remove(this);
        }

        public bool IsEnabled { get; set; }

        public void Initialize()
        {
            _gameObject = Object.Instantiate(_prefab);
            _transform = _gameObject.transform;
            _collider = _gameObject.GetComponent<SphereCollider>();
            _view = _gameObject.GetComponent<BubbleView>();
            _view.Controller = this;
            _model.Active = true;
        }

        public void Enable()
        {
            if (_model.Active) return;

            _gameObject.SetActive(true);
            _model.Active = true;
        }

        public void Disable()
        {
            if (!_model.Active) return;

            _gameObject.SetActive(false);
            _model.Active = false;
        }

        public void Hit()
        {
            if (!_model.Active) return;

            _bubblesManager.Destroy(this, true);
        }

        public void Tick()
        {
            if (!_model.Active) return;

            var position = _transform.position;

            if (Mathf.Abs(position.y - _targetPosition.y) < 0.01f) _bubblesManager.Destroy(this);

            var newPosition =
                Vector3.MoveTowards(position, _targetPosition, _model.Speed * _bubblesManager.SpeedFactor);

            _transform.position = newPosition;
        }
    }
}