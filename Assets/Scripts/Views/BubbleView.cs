using UnityEngine;

namespace Bubbles
{
    public class BubbleView : MonoBehaviour
    {
        private BubbleController _controller;

        public BubbleController Controller => _controller;

        private void Awake()
        {
            _controller = new BubbleController(gameObject);
        }

        private void OnDestroy()
        {
            _controller.Dispose();
        }

        private void OnDisable()
        {
            _controller.Disable();
        }

        private void OnEnable()
        {
            _controller.Enable();
        }

        private void OnMouseDown()
        {
            _controller.Hit();
        }
    }
}