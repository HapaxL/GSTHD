using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerOOT
{
    class GossipStone : PictureBox
    {
        List<string> listImage;
        bool isMouseDown = false;

        public GossipStone(List<string> images, Point location, int size)
        {
            listImage = images;

            this.BackColor = Color.Transparent;
            this.Name = listImage[0];
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImage[0]);
            this.Size = new Size(size, size);
            this.Location = location;
            this.TabStop = false;
            this.AllowDrop = true;
            this.MouseUp += this.Click_MouseUp;
            this.MouseDown += this.Click_MouseDown;
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
            var newImageName = imageName.Substring(0, imageName.Length - 2) + this.Size.Width;
            var image = (Image)Properties.Resources.ResourceManager.GetObject(newImageName);
            this.Image = image;
        }

        public void Click_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var index = listImage.FindIndex(x => x == this.Name) + 1;
                if (index <= 0 || index >= listImage.Count)
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImage[0]);
                    this.Name = listImage[0];
                }
                else
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImage[index]);
                    this.Name = listImage[index];
                }
            }
        }

        private void Click_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1)
                isMouseDown = false;
            else isMouseDown = true;
        }
        
    }
}
