using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrackerOOT
{
    class Song : PictureBox
    {
        public List<string> listImageName;
        public List<string> listTinyImageName;
        public Image tinyImage;
        public Image tinyImageEmpty;
        bool isMouseDown = false;
        public PictureBox elementFoundAtLocation;
        int ImageSize;
        bool SongMode;

        public Song(List<string> images, int x, int y, List<string> tinyImages, int size, bool songMode)
        {
            ImageSize = size;
            SongMode = songMode;
            listImageName = images;
            listTinyImageName = tinyImages;
            tinyImageEmpty = (Image)Properties.Resources.ResourceManager.GetObject(listTinyImageName[0]);
            tinyImage = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[1].Replace(size.ToString(), "16"));

            this.BackColor = Color.Transparent;
            this.Name = listImageName[0];
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[0]);
            this.Size = new Size(size, size + 12);
            this.Location = new Point(x, y);
            this.TabStop = false;
            this.AllowDrop = true;
            this.MouseUp += this.Click_MouseUp;
            this.MouseDown += this.Click_MouseDown;
            this.MouseMove += this.Click_MouseMove;
            this.DragEnter += this.Click_DragEnter;
            this.DragDrop += this.Click_DragDrop;
            this.Tag = listImageName[1];

            elementFoundAtLocation = new PictureBox
            {
                BackColor = Color.Transparent,
                Image = tinyImageEmpty,
                Name = listTinyImageName[0],
                Size = new Size(16, 16),
                TabStop = false,
                AllowDrop = false,
            };
            elementFoundAtLocation.MouseUp += ElementFoundAtLocation_MouseUp;
            elementFoundAtLocation.DragEnter += Click_DragEnter;
            elementFoundAtLocation.DragDrop += Click_DragDrop;
            this.Controls.Add(elementFoundAtLocation);
            elementFoundAtLocation.BringToFront();
            elementFoundAtLocation.Location = new Point((size - 16) / 2, size - size / 4);
        }

        private void ElementFoundAtLocation_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var index = listTinyImageName.FindIndex(x => x == elementFoundAtLocation.Name) + 1;
                if (index <= 0 || index >= listTinyImageName.Count)
                {
                    elementFoundAtLocation.Image = (Image)Properties.Resources.ResourceManager.GetObject(listTinyImageName[0]);
                    elementFoundAtLocation.Name = listTinyImageName[0];
                }
                else
                {
                    elementFoundAtLocation.Image = (Image)Properties.Resources.ResourceManager.GetObject(listTinyImageName[index]);
                    elementFoundAtLocation.Name = listTinyImageName[index];
                }
            }
        }

        private void Click_DragDrop(object sender, DragEventArgs e)
        {
            var imageName = ((string)e.Data.GetData(DataFormats.Text));
            var tinyImageName = imageName.Substring(0, imageName.Length-2) + "16";
            var tinyImage = (Image)Properties.Resources.ResourceManager.GetObject(tinyImageName);

            if (SongMode)
            {
                elementFoundAtLocation.Image = tinyImage;
                elementFoundAtLocation.Name = imageName;
                this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[1]);
                this.Name = imageName;
            }
            else
            {
                elementFoundAtLocation.Image = tinyImage;
                elementFoundAtLocation.Name = imageName;
                var newName = imageName.Substring(0, imageName.Length - 2) + "bw_" + imageName.Substring(imageName.Length - 2, 2);
                var findOrigin = this.Parent.Controls.Find(newName, false);
                if (findOrigin.Length > 0)
                {
                    var origin = (Song)findOrigin[0];
                    origin.Click_MouseUp(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
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
                var index = listImageName.FindIndex(x => x == this.Name) + 1;
                if (index <= 0 || index >= listImageName.Count)
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[0]);
                    this.Name = listImageName[0];
                }
                else
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[index]);
                    this.Name = listImageName[index];
                }
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
                this.DoDragDrop(this.Tag, DragDropEffects.Copy);
                isMouseDown = false;
            }
        }       
    }
}
