using System;
using System.Windows.Forms;

namespace GSTHD
{
    public interface ProgressibleElement<T>
    {
        void IncrementState();
        void DecrementState();
        void ResetState();
    }

    public class ProgressibleElementBehaviour<T>
    {
        protected ProgressibleElement<T> Element;
        protected Settings Settings;

        public ProgressibleElementBehaviour(ProgressibleElement<T> element, Settings settings)
        {
            Element = element;
            Settings = settings;
        }

        public void Mouse_ClickDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    Mouse_LeftClickDown(sender, e);
                    break;
                case MouseButtons.Middle:
                    Mouse_MiddleClickDown(sender, e);
                    break;
                case MouseButtons.Right:
                    Mouse_RightClickDown(sender, e);
                    break;
            }
        }

        public void Mouse_LeftClickDown(object sender, MouseEventArgs e)
        {
            Element.IncrementState();
        }

        public void Mouse_MiddleClickDown(object sender, MouseEventArgs e)
        {
            Element.ResetState();
        }

        public void Mouse_RightClickDown(object sender, MouseEventArgs e)
        {
            Element.DecrementState();
        }
    }
}
