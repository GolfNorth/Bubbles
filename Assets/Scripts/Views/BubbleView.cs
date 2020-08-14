namespace Bubbles
{
    public class BubbleView : BaseView<BubbleController>
    {
        private void Awake()
        {
            controller = new BubbleController(gameObject);
        }

        private void OnDestroy()
        {
            controller.Dispose();
        }

        private void OnDisable()
        {
            controller.Disable();
        }

        private void OnEnable()
        {
            controller.Enable();
        }

        private void OnMouseDown()
        {
            controller.Hit();
        }
    }
}