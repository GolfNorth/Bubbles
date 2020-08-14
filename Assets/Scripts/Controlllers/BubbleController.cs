using UnityEngine;

namespace Bubbles
{
    public class BubbleController : IController<BubbleController>, ITickable
    {
        private bool _active;
        private BubbleModel _model;
        private GameObject _gameObject;
        private Transform _transform;
        private SphereCollider _collider;
        private Vector3 _targetPosition;
        private readonly UpdateManager _updateManager;
        private readonly BubblesManager _bubblesManager;
        private readonly DifficultSettings _difficultSettings;

        public GameObject GameObject => _gameObject;

        public float Radius => _model.Radius;

        public Vector3 Position
        {
            get => _model.Position;
            set
            {
                _transform.position = value;
                _model.Position = value;
            }
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

        public Vector3 TargetPosition
        {
            set => _targetPosition = value;
        }

        public BubbleController(GameObject gameObject)
        {
            _active = true;
            _model = new BubbleModel();
            _gameObject = gameObject;
            _transform = gameObject.transform;
            _collider = gameObject.GetComponent<SphereCollider>();
            _bubblesManager = SceneContext.Instance.BubblesManager;
            _updateManager = SceneContext.Instance.UpdateManager;
            _updateManager.Add(this);
            _difficultSettings = SceneContext.Instance.DifficultSettings;
        }

        public void Dispose()
        {
            _updateManager?.Remove(this);
        }

        public void Disable()
        {
            if (!_active) return;

            _gameObject.SetActive(false);
            _active = false;
        }

        public void Enable()
        {
            if (_active) return;

            _gameObject.SetActive(true);
            _active = true;
        }

        public void Hit()
        {
            if (!_active) return;

            _bubblesManager.Destroy(this, true);
        }

        public void Tick()
        {
            if (!_active) return;

            var position = _transform.position;

            if (Mathf.Abs(position.y - _targetPosition.y) < 0.01f) _bubblesManager.Destroy(this);

            var newPosition =
                Vector3.MoveTowards(position, _targetPosition, _model.Speed * _bubblesManager.SpeedFactor);

            _transform.position = newPosition;
        }
    }
}