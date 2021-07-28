using System;
using System.Drawing;
using System.Windows.Forms;

namespace GSTHD
{
    public interface DraggableElement<T>
    {
        T GetState();
        void SetState(T state);

        void StartDragDrop();
        void SaveChanges();
        void CancelChanges();
    }

    public class DraggableElementBehaviour<T>
    {
        protected DraggableElement<T> Element;
        protected Settings Settings;

        protected bool LeftClickDown = false;
        protected bool RightClickDown = false;
        protected bool MiddleClickDown = false;

        protected bool CanDragDrop = false;
        protected Point DragStartPoint;

        protected T LastState;

        public DraggableElementBehaviour(DraggableElement<T> element, Settings settings)
        {
            Element = element;
            Settings = settings;

            LastState = Element.GetState();
        }

        public void Mouse_ClickUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    Mouse_LeftClickUp(sender, e);
                    break;
                case MouseButtons.Middle:
                    Mouse_MiddleClickUp(sender, e);
                    break;
                case MouseButtons.Right:
                    Mouse_RightClickUp(sender, e);
                    break;
            }
            if (!LeftClickDown && !MiddleClickDown && !RightClickDown)
                SaveChanges();

            UpdateDragDropPreparationStatus();
        }

        public void Mouse_LeftClickUp(object sender, MouseEventArgs e)
        {
            LeftClickDown = false;
        }

        public void Mouse_MiddleClickUp(object sender, MouseEventArgs e)
        {
            MiddleClickDown = false;
        }

        public void Mouse_RightClickUp(object sender, MouseEventArgs e)
        {
            RightClickDown = false;
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
            UpdateDragDropPreparationStatus();
        }

        public void Mouse_LeftClickDown(object sender, MouseEventArgs e)
        {
            LeftClickDown = true;
        }

        public void Mouse_MiddleClickDown(object sender, MouseEventArgs e)
        {
            MiddleClickDown = true;
        }

        public void Mouse_RightClickDown(object sender, MouseEventArgs e)
        {
            RightClickDown = true;
        }

        public virtual void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (CanDragDrop && DragOverThreshold())
            {
                LeftClickDown = false;
                MiddleClickDown = false;
                RightClickDown = false;
                CanDragDrop = false;
                CancelChanges();
                Element.StartDragDrop();
            }
        }

        protected bool DragOverThreshold()
        {
            return (System.Math.Abs(DragStartPoint.X - Cursor.Position.X) >= Settings.MinDragThreshold
                || System.Math.Abs(DragStartPoint.Y - Cursor.Position.Y) >= Settings.MinDragThreshold);
        }

        public virtual void UpdateDragDropPreparationStatus()
        {
            CanDragDrop = ((Settings.DragButton == Settings.DragButtonOption.LeftAndRight || Settings.AutocheckDragButton == Settings.DragButtonOption.LeftAndRight) && LeftClickDown && RightClickDown)
                || ((Settings.DragButton == Settings.DragButtonOption.Left || Settings.AutocheckDragButton == Settings.DragButtonOption.Left) && LeftClickDown)
                || ((Settings.DragButton == Settings.DragButtonOption.Right || Settings.AutocheckDragButton == Settings.DragButtonOption.Right) && RightClickDown)
                || ((Settings.DragButton == Settings.DragButtonOption.Middle || Settings.AutocheckDragButton == Settings.DragButtonOption.Middle) && MiddleClickDown);
            if (CanDragDrop)
                DragStartPoint = Cursor.Position;
        }

        public void SaveChanges()
        {
            Element.SaveChanges();
            LastState = Element.GetState();
        }

        public void CancelChanges()
        {
            Element.SetState(LastState);
            Element.CancelChanges();
        }
    }
}
