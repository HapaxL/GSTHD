using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GSTHD
{
    class Item : PictureBox
    {
        private readonly Settings Settings;

        List<string> ListImageName = new List<string>();
        int imageIndex = 0;
        bool isMouseDown = false;

        Size ItemSize;
        public Item(ObjectPoint data, Settings settings)
        {
            Settings = settings;

            if(data.ImageCollection != null)
                ListImageName = data.ImageCollection.ToList();

            ItemSize = data.Size;

            this.BackColor = Color.Transparent;
            if (ListImageName.Count > 0)
            {
                this.Name = ListImageName[0];
                this.Image = Image.FromFile(@"Resources/" + ListImageName[0]);
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Size = ItemSize;
            }            
            this.Location = new Point(data.X, data.Y);
            this.TabStop = false;
            this.AllowDrop = false;
            this.MouseUp += this.Click_MouseUp;
            this.MouseDown += this.Click_MouseDown;
            this.MouseMove += this.Click_MouseMove;
            this.MouseWheel += this.Click_MouseWheel;
        }

        private void Click_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                imageIndex = 0;
                Image = Image.FromFile(@"Resources/" + ListImageName[imageIndex]);
                Name = ListImageName[imageIndex];
            }
        }

        private void Click_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1)
                isMouseDown = false;
            else isMouseDown = true;

            if (e.Button == MouseButtons.Left && imageIndex < ListImageName.Count - 1)
            {
                imageIndex += 1;
            }

            if (e.Button == MouseButtons.Right && imageIndex > 0)
            {
                imageIndex -= 1;
            }

            Image = Image.FromFile(@"Resources/" + ListImageName[imageIndex]);
            Name = ListImageName[imageIndex];
        }

        private void Click_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle && isMouseDown)
            {
                this.DoDragDrop(ListImageName[1], DragDropEffects.Copy);
                isMouseDown = false;
            }
        }

        private void Click_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                var scrolls = e.Delta / SystemInformation.MouseWheelScrollDelta;
                imageIndex += Settings.InvertScrollWheel ? scrolls : -scrolls;
                if (imageIndex < 0) imageIndex = 0;
                if (imageIndex >= ListImageName.Count) imageIndex = ListImageName.Count - 1;
                Image = Image.FromFile(@"Resources/" + ListImageName[imageIndex]);
                Name = ListImageName[imageIndex];
            }
        }
    }
}
