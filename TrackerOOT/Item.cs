using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerOOT
{
    class Item : PictureBox
    {
        List<string> listImageName;
        bool isMouseDown = false;
        public Item(List<string> images, int x, int y, int size)
        {
            listImageName = images;

            this.BackColor = Color.Transparent;
            this.Name = listImageName[0];
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[0]);
            this.Size = new Size(size, size);
            this.Location = new Point(x, y);
            this.TabStop = false;
            this.AllowDrop = false;
            this.MouseUp += this.Click_MouseUp;
            this.MouseDown += this.Click_MouseDown;
            this.MouseMove += this.Click_MouseMove;
        }

        private void Click_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
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
                this.DoDragDrop(listImageName[1], DragDropEffects.Copy);
                isMouseDown = false;
            }
        }
    }
}
