using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerOOT
{
    class GossipStone : PictureBox
    {
        List<Image> listImage;
        bool isMouseDown = false;

        public GossipStone(string name, List<Image> images, int x, int y)
        {
            listImage = images;

            this.BackColor = Color.Transparent;
            this.Name = name;
            this.Image = listImage[0];
            this.Size = new Size(32, 32);
            this.Location = new Point(x, y);
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
            var image = (Image)e.Data.GetData(DataFormats.Bitmap);
            this.Image = image;
        }

        private void Click_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var index = listImage.FindIndex(x => x == this.Image);
                if (index == (listImage.Count - 1))
                    this.Image = listImage[0];
                else
                    this.Image = listImage[index + 1];
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
