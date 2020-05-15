using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerOOT
{
    class ItemDouble : PictureBox
    {
        List<string> listImageName;
        bool isMouseDown = false;
        bool isColoredLeft = false;
        bool isColoredRight = false;

        public ItemDouble(List<string> names, int x, int y, int size)
        {
            listImageName = names;

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
            if (e.Button == MouseButtons.Left)
            {
                if (!isColoredLeft && !isColoredRight)
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[1]);
                    this.Name = listImageName[1];
                    isColoredLeft = true;
                }
                else if (isColoredLeft && !isColoredRight)
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[0]);
                    this.Name = listImageName[0];
                    isColoredLeft = false;
                }
                else if (!isColoredLeft && isColoredRight)
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[3]);
                    this.Name = listImageName[3];
                    isColoredLeft = true;
                }
                else if (isColoredLeft && isColoredRight)
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[2]);
                    this.Name = listImageName[2];
                    isColoredLeft = false;
                }

            }
            if (e.Button == MouseButtons.Right)
            {
                if (!isColoredLeft && !isColoredRight)
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[2]);
                    this.Name = listImageName[2];
                    isColoredRight = true;
                }
                else if (isColoredLeft && !isColoredRight)
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[3]);
                    this.Name = listImageName[3];
                    isColoredRight = true;
                }
                else if (!isColoredLeft && isColoredRight)
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[0]);
                    this.Name = listImageName[0];
                    isColoredRight = false;
                }
                else if (isColoredLeft && isColoredRight)
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(listImageName[1]);
                    this.Name = listImageName[1];
                    isColoredRight = false;
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
                this.DoDragDrop(this.listImageName[4], DragDropEffects.Copy);
                isMouseDown = false;
            }
            if (e.Button == MouseButtons.Right && isMouseDown)
            {
                this.DoDragDrop(this.listImageName[5], DragDropEffects.Copy);
                isMouseDown = false;
            }
        }
    }
}
