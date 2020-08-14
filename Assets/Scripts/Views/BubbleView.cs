namespace Bubbles
{
    public class BubbleView : BaseView<BubbleController>
    {
        private void OnDestroy()
        {
            Controller.Dispose();
        }

        private void OnMouseDown()
        {
            Controller.Hit();
        }
    }
}