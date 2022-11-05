using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSTHD
{
    class CollectedItem : PictureBox, ProgressibleElement<int>, DraggableAutocheckElement<int>
    {
        private readonly Settings Settings;
        private readonly ProgressibleElementBehaviour<int> ProgressBehaviour;
        private readonly DraggableAutocheckElementBehaviour<int> DragBehaviour;

        private string[] ImageNames;
        private Label ItemCount;
        private Size CollectedItemSize;
        private Size CollectedItemCountPosition;
        private readonly int CollectedItemMin;
        private readonly int CollectedItemMax;
        private readonly int CollectedItemDefault;
        private int CollectedItems;
        private readonly int Step;

        public CollectedItem(ObjectPointCollectedItem data, Settings settings)
        {
            Settings = settings;

            if (data.ImageCollection == null)
                ImageNames = new string[0];
            else
                ImageNames = data.ImageCollection;

            CollectedItemMin = data.CountMin;
            CollectedItemMax = data.CountMax.HasValue ? data.CountMax.Value : 100;
            CollectedItemDefault = data.DefaultValue;
            CollectedItems = System.Math.Min(System.Math.Max(CollectedItemMin, CollectedItemDefault), CollectedItemMax);
            Step = data.Step == 0 ? 1 : data.Step;
            CollectedItemSize = data.Size;

            if (ImageNames.Length > 0)
            {
                Image = Image.FromFile(@"Resources/" + ImageNames[0]);
                Name = ImageNames[0];
                SizeMode = PictureBoxSizeMode.StretchImage;
                Size = CollectedItemSize;
            }

            ProgressBehaviour = new ProgressibleElementBehaviour<int>(this, Settings);
            DragBehaviour = new DraggableAutocheckElementBehaviour<int>(this, Settings);

            Location = new Point(data.X, data.Y);
            CollectedItemCountPosition = data.CountPosition.IsEmpty ? new Size(0, -7) : data.CountPosition;
            BackColor = Color.Transparent;
            TabStop = false;


            ItemCount = new Label
            {
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None,
                Text = CollectedItems.ToString(),
                Font = new Font(data.LabelFontName, data.LabelFontSize, data.LabelFontStyle),
                ForeColor = data.LabelColor,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Width = 40,
                Location = new Point((CollectedItemSize.Width / 2) + CollectedItemCountPosition.Width - 19, (CollectedItemSize.Height / 2) + CollectedItemCountPosition.Height - 15),
            };

            MouseDown += ProgressBehaviour.Mouse_ClickDown;
            MouseUp += DragBehaviour.Mouse_ClickUp;
            MouseDown += DragBehaviour.Mouse_ClickDown;
            MouseMove += DragBehaviour.Mouse_Move_WithAutocheck;
            MouseWheel += Mouse_Wheel;
            ItemCount.MouseDown += ProgressBehaviour.Mouse_ClickDown; // must add these lines because MouseDown/Up on PictureBox won't fire when hovering above Label
            ItemCount.MouseDown += DragBehaviour.Mouse_ClickDown;
            ItemCount.MouseUp += DragBehaviour.Mouse_ClickUp;
            ItemCount.MouseMove += DragBehaviour.Mouse_Move_WithAutocheck;
            // ItemCount.MouseWheel += Click_MouseWheel; // must NOT add this line because both MouseWheels would fire when hovering above both PictureBox and Label

            Controls.Add(ItemCount);
        }

        private void Mouse_Wheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                var scrolls = e.Delta / SystemInformation.MouseWheelScrollDelta;
                CollectedItems += Step * (Settings.InvertScrollWheel ? scrolls : -scrolls);
                if (CollectedItems < CollectedItemMin) CollectedItems = CollectedItemMin;
                else if (CollectedItems > CollectedItemMax) CollectedItems = CollectedItemMax;
                UpdateCount();
            }
        }

        private void UpdateCount()
        {
            ItemCount.Text = CollectedItems.ToString();
        }

        public int GetState()
        {
            return CollectedItems;
        }

        public void SetState(int state)
        {
            CollectedItems = state;
            UpdateCount();
        }

        public void IncrementState()
        {
            CollectedItems += Step;
            if (CollectedItems > CollectedItemMax) CollectedItems = CollectedItemMax;
            UpdateCount();
        }

        public void DecrementState()
        {
            CollectedItems -= Step;
            if (CollectedItems < CollectedItemMin) CollectedItems = CollectedItemMin;
            UpdateCount();
        }

        public void ResetState()
        {
            CollectedItems = CollectedItemDefault;
            UpdateCount();
        }

        public void StartDragDrop()
        {
            var dropContent = new DragDropContent(DragBehaviour.AutocheckDragDrop, ImageNames[0]);
            DoDragDrop(dropContent, DragDropEffects.Copy);
        }

        public void SaveChanges() { }
        public void CancelChanges() { }
    }
}
