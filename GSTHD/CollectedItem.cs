using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSTHD
{
    class CollectedItem : PictureBox
    {
        private readonly Settings Settings;

        List<string> ListImageName = new List<string>();
        Label ItemCount;
        Size CollectedItemSize;
        Size CollectedItemCountPosition;
        private readonly int CollectedItemMax;
        private int CollectedItems;
        private readonly int Step;
        bool isMouseDown = false;

        public CollectedItem(ObjectPointCollectedItem data, Settings settings)
        {
            Settings = settings;

            if (data.ImageCollection != null)
                ListImageName = data.ImageCollection.ToList();

            CollectedItemSize = data.Size;
            this.CollectedItems = 0;

            if (ListImageName.Count > 0)
            {
                this.Image = Image.FromFile(@"Resources/" + ListImageName[0]);
                this.Name = ListImageName[0];
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Size = CollectedItemSize;
            }
            this.Location = new Point(data.X, data.Y);
            this.CollectedItemCountPosition = data.CountPosition.IsEmpty ? new Size(0, -7) : data.CountPosition;
            this.CollectedItemMax = data.CountMax == 0 ? 100 : data.CountMax;
            this.Step = data.Step == 0 ? 1 : data.Step;
            this.BackColor = Color.Transparent;
            this.TabStop = false;

            ItemCount = new Label
            {
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None,
                Text = "0",
                Font = new Font(data.LabelFontName, data.LabelFontSize, data.LabelFontStyle),
                ForeColor = data.LabelColor,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30,
                Width = 40,
                Location = new Point((CollectedItemSize.Width / 2) + CollectedItemCountPosition.Width - 19, (CollectedItemSize.Height / 2) + CollectedItemCountPosition.Height - 15),
            };
            this.MouseDown += this.Click_MouseDown;
            this.MouseUp += this.Click_MouseUp;
            this.MouseMove += this.Click_MouseMove;
            this.MouseWheel += this.Click_MouseWheel;
            ItemCount.MouseMove += this.Click_MouseMove;
            ItemCount.MouseUp += this.Click_MouseUp;
            ItemCount.MouseDown += this.Click_MouseDown; // must add this line because MouseDown on PictureBox won't fire when hovering above Label
            // ItemCount.MouseWheel += this.Click_MouseWheel; // must NOT add this line because both MouseWheels would fire when hovering above both PictureBox and Label

            this.Controls.Add(ItemCount);
        }

        private void Click_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1)
                isMouseDown = false;
            else isMouseDown = true;

            if (e.Button == MouseButtons.Left && CollectedItems < CollectedItemMax)
            {
                CollectedItems += Step;
                if (CollectedItems > CollectedItemMax) CollectedItems = CollectedItemMax;
            }
            if (e.Button == MouseButtons.Right && CollectedItems > 0)
            {
                CollectedItems -= Step;
                if (CollectedItems < 0) CollectedItems = 0;
            }

            ItemCount.Text = CollectedItems.ToString();
        }

        private void Click_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                CollectedItems = 0;
                ItemCount.Text = CollectedItems.ToString();
            }
        }

        private void Click_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle && isMouseDown)
            {
                this.DoDragDrop(ListImageName[0], DragDropEffects.Copy);
                isMouseDown = false;
            }
        }

        private void Click_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                var scrolls = e.Delta / SystemInformation.MouseWheelScrollDelta;
                CollectedItems += Step * (Settings.InvertScrollWheel ? scrolls : -scrolls);
                if (CollectedItems < 0) CollectedItems = 0;
                if (CollectedItems > CollectedItemMax) CollectedItems = CollectedItemMax;

                ItemCount.Text = CollectedItems.ToString();
            }
        }
    }
}
