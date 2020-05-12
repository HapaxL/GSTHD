using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerOOT
{
    class Song : PictureBox
    {
        public List<Image> listImage;
        public List<Image> listTinyImage;
        public Image tinyImage;
        public Image tinyImageEmpty;
        bool isMouseDown = false;
        public PictureBox elementFoundAtLocation;
        bool SongMode;

        public Song(string name, List<Image> images, int x, int y, List<Image> tinyImages, bool songMode)
        {
            SongMode = songMode;
            listImage = images;
            listTinyImage = tinyImages;
            tinyImageEmpty = listTinyImage[0];
            tinyImage = (Image)Properties.Resources.ResourceManager.GetObject(name + "_16");

            this.BackColor = Color.Transparent;
            this.Name = name;
            this.Image = listImage[0];
            this.Size = new Size(32, 40);
            this.Location = new Point(x, y);
            this.TabStop = false;
            this.AllowDrop = true;
            this.MouseUp += this.Click_MouseUp;
            this.MouseDown += this.Click_MouseDown;
            this.MouseMove += this.Click_MouseMove;
            this.DragEnter += this.Click_DragEnter;
            this.DragDrop += this.Click_DragDrop;
            
            elementFoundAtLocation = new PictureBox
            {
                BackColor = Color.Transparent,
                Image = tinyImageEmpty,
                Name = name,
                Size = new Size(16, 16),
                Location = new Point(8, 24),
                TabStop = false,
                AllowDrop = false
            };
            elementFoundAtLocation.MouseUp += ElementFoundAtLocation_MouseUp;
            elementFoundAtLocation.DragEnter += Click_DragEnter;
            elementFoundAtLocation.DragDrop += Click_DragDrop;
            this.Controls.Add(elementFoundAtLocation);
            elementFoundAtLocation.BringToFront();
        }

        private void ElementFoundAtLocation_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var index = listTinyImage.FindIndex(x => x == elementFoundAtLocation.Image);
                if (index == (listTinyImage.Count - 1))
                    elementFoundAtLocation.Image = listTinyImage[0];
                else
                    elementFoundAtLocation.Image = listTinyImage[index + 1];
            }
        }

        private void Click_DragDrop(object sender, DragEventArgs e)
        {
            var image = (Image)e.Data.GetData(DataFormats.Bitmap);
            if (image.Width == 16 && image.Height == 16)
            {
                if (SongMode)
                {
                    elementFoundAtLocation.Image = image;
                    this.Image = listImage[1];
                }
                else
                {
                    elementFoundAtLocation.Image = image;                    
                }
            }
        }

        private void Click_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        public void Click_MouseUp(object sender, MouseEventArgs e)
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

        private void Click_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && isMouseDown)
            {
                this.DoDragDrop(this.listImage[1], DragDropEffects.Copy);
                isMouseDown = false;
            }
            if (e.Button == MouseButtons.Right && isMouseDown)
            {
                this.DoDragDrop(this.tinyImage, DragDropEffects.Copy);
                isMouseDown = false;
            }

        }

        public string saveItem()
        {
            return '"' + this.Name + "\" : \"" + listImage.FindIndex(x => x == this.Image).ToString() + '"';
        }

        public void loadItem(string value)
        {

        }
    }
}
