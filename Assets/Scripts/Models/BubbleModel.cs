using UnityEngine;

namespace Bubbles
{
    public sealed class BubbleModel
    {
        private float _speed;
        private float _radius;
        private Vector3 _position;

        public float Speed
        {
            get => _speed;
            set => _speed = value;
        }

        public float Radius
        {
            get => _radius;
            set => _radius = value;
        }

        public Vector3 Position
        {
            get => _position;
            set => _position = value;
        }
    }
}