using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GSTHD
{
    class GossipStone : PictureBox
    {
        List<string> ListImageName = new List<string>();
        string ActiveImageName;
        int imageIndex = 0;
        bool isMouseDown = false;

        Size GossipStoneSize;

        public GossipStone(ObjectPoint data)
        {
            if (data.ImageCollection != null)
                ListImageName = data.ImageCollection.ToList();

            GossipStoneSize = data.Size;

            if (ListImageName.Count > 0)
            {
                this.ActiveImageName = ListImageName[0];
                this.Image = Image.FromFile(@"Resources/" + ListImageName[0]);
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Size = GossipStoneSize;
            }

            this.Name = data.Name;
            this.BackColor = Color.Transparent;
            this.Location = new Point(data.X, data.Y);
            this.TabStop = false;
            this.AllowDrop = true;

            this.MouseUp += this.Click_MouseUp;
            this.MouseDown += this.Click_MouseDown;
            this.MouseMove += this.Click_MouseMove;
            this.DragEnter += this.Click_DragEnter;
            this.DragDrop += this.Click_DragDrop;
        }

        public GossipStone(string name, int x, int y, string[] imageCollection, Size imageSize)
        {
            if (imageCollection != null)
                ListImageName = imageCollection.ToList();

            GossipStoneSize = imageSize;

            if (ListImageName.Count > 0)
            {
                this.ActiveImageName = ListImageName[0];
                this.Image = Image.FromFile(@"Resources/" + ListImageName[0]);
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Size = GossipStoneSize;
            }

            this.Name = name;
            this.BackColor = Color.Transparent;
            this.Location = new Point(x, y);
            this.TabStop = false;
            this.AllowDrop = true;

            this.MouseUp += this.Click_MouseUp;
            this.MouseDown += this.Click_MouseDown;
            this.MouseMove += this.Click_MouseMove;
            this.DragEnter += this.Click_DragEnter;
            this.DragDrop += this.Click_DragDrop;
        }

        private void Click_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void Click_DragDrop(object sender, DragEventArgs e)
        {
            var imageName = (string)e.Data.GetData(DataFormats.Text);
            var image = Image.FromFile(@"Resources/" + imageName);
            ActiveImageName = imageName;
            this.Image = image;
        }

        public void Click_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                imageIndex = 0;
                this.Image = Image.FromFile(@"Resources/" + ListImageName[imageIndex]);
                this.ActiveImageName = ListImageName[imageIndex];
            }
        }

        private void Click_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle && isMouseDown)
            {
                var draggedImageName = ActiveImageName;
                imageIndex = 0;
                this.Image = Image.FromFile(@"Resources/" + ListImageName[imageIndex]);
                ActiveImageName = ListImageName[imageIndex];
                isMouseDown = false;
                this.DoDragDrop(draggedImageName, DragDropEffects.Copy);
            }
        }

        private void Click_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1)
                isMouseDown = false;
            else isMouseDown = true;

            var newImageIndex = imageIndex;

            if (e.Button == MouseButtons.Left && imageIndex < ListImageName.Count - 1)
            {
                newImageIndex += 1;
            }

            if (e.Button == MouseButtons.Right && imageIndex > 0)
            {
                newImageIndex -= 1;
            }

            if (newImageIndex != imageIndex)
            {
                imageIndex = newImageIndex;
                this.Image = Image.FromFile(@"Resources/" + ListImageName[newImageIndex]);
                this.ActiveImageName = ListImageName[newImageIndex];
            }
        }
    }
}
