using UnityEngine;

namespace Bubbles
{
    public sealed class BubbleModel
    {
        private bool _active;
        private float _speed;
        private float _radius;

        public bool Active
        {
            get => _active;
            set => _active = value;
        }

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
    }
}