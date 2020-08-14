namespace Bubbles
{
    public sealed class BubbleModel
    {
        private float _speed;
        private float _radius;

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