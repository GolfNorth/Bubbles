using System;
using UnityEngine;

namespace Bubbles
{
    public sealed class BoundsManager : IDisposable
    {
        private Camera _camera;
        private readonly DifficultSettings _difficultSettings;
        private readonly TimeManager _timeManager;
        private float _topBound;
        private float _bottomBound;
        private float _leftBound;
        private float _rightBound;

        public BoundsManager()
        {
            _camera = SceneContext.Instance.Camera;
            _difficultSettings = SceneContext.Instance.DifficultSettings;
            _timeManager = SceneContext.Instance.TimeManager;
            _timeManager.RoundCountdown += RecalculateBounds;

            RecalculateBounds();
        }

        public void Dispose()
        {
            _timeManager.RoundCountdown -= RecalculateBounds;
        }

        public float TopBound => _topBound;

        public float BottomBound => _bottomBound;

        public float LeftBound => _leftBound;

        public float RightBound => _rightBound;

        private void RecalculateBounds()
        {
            var z = _camera.gameObject.transform.position.z;
            var topRight = _camera.ViewportToWorldPoint(new Vector3(1, 1, -z));
            var bottomLeft = _camera.ViewportToWorldPoint(new Vector3(0, 0, -z));

            _topBound = topRight.y + _difficultSettings.MaxRadius;
            _bottomBound = bottomLeft.y - _difficultSettings.MaxRadius;
            _leftBound = bottomLeft.x + _difficultSettings.MaxRadius;
            _rightBound = topRight.x - _difficultSettings.MaxRadius;
        }
    }
}