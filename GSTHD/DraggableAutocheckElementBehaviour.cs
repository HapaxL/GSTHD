using System;
using System.Windows.Forms;

namespace GSTHD
{
    public interface DraggableAutocheckElement<T> : DraggableElement<T>
    {
        void IncrementState();
        void DecrementState();
        void ResetState();
    }

    public class DraggableAutocheckElementBehaviour<T> : DraggableElementBehaviour<T>
    {
        protected new DraggableAutocheckElement<T> Element;

        public bool AutocheckDragDrop = false;

        public DraggableAutocheckElementBehaviour(DraggableAutocheckElement<T> element, Settings settings)
            : base(element, settings)
        {
            Element = element;
        }

        public void Mouse_Move_WithAutocheck(object sender, MouseEventArgs e)
        {
            if (CanDragDrop && DragOverThreshold())
            {
                LeftClickDown = false;
                MiddleClickDown = false;
                RightClickDown = false;
                CanDragDrop = false;
                CancelChanges();
                if (AutocheckDragDrop)
                {
                    Element.IncrementState();
                    SaveChanges();
                }
                Element.StartDragDrop();
            }
        }

        public override void UpdateDragDropPreparationStatus()
        {
            if ((Settings.AutocheckDragButton == Settings.DragButtonOption.LeftAndRight && LeftClickDown && RightClickDown)
                || (Settings.AutocheckDragButton == Settings.DragButtonOption.Middle && MiddleClickDown))
            {
                CanDragDrop = true;
                AutocheckDragDrop = true;
            }
            else if (Settings.DragButton == Settings.DragButtonOption.LeftAndRight && LeftClickDown && RightClickDown)
            {
                CanDragDrop = true;
                AutocheckDragDrop = false;
            }
            else if ((Settings.AutocheckDragButton == Settings.DragButtonOption.Left && LeftClickDown)
                || (Settings.AutocheckDragButton == Settings.DragButtonOption.Right && RightClickDown))
            {
                CanDragDrop = true;
                AutocheckDragDrop = true;
            }
            else if ((Settings.DragButton == Settings.DragButtonOption.Left && LeftClickDown)
                || (Settings.DragButton == Settings.DragButtonOption.Right && RightClickDown)
                || (Settings.DragButton == Settings.DragButtonOption.Middle && MiddleClickDown))
            {
                CanDragDrop = true;
                AutocheckDragDrop = false;
            }
            else
            {
                CanDragDrop = false;
            }
            if (CanDragDrop)
                DragStartPoint = Cursor.Position;
        }
    }
}
