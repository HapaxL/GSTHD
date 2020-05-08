using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerOOT
{
    class ItemDouble : PictureBox
    {
        List<Image> listImage;
        bool isMouseDown = false;
        bool isColoredLeft = false;
        bool isColoredRight = false;

        public ItemDouble(string name, List<Image> images, int x, int y)
        {
            listImage = images;

            this.BackColor = Color.Transparent;
            this.Name = name;
            this.Image = listImage[0];
            this.Size = new Size(32, 32);
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
                    this.Image = listImage[1];
                    isColoredLeft = true;
                }
                else if (isColoredLeft && !isColoredRight)
                {
                    this.Image = listImage[0];
                    isColoredLeft = false;
                }
                else if (!isColoredLeft && isColoredRight)
                {
                    this.Image = listImage[3];
                    isColoredLeft = true;
                }
                else if (isColoredLeft && isColoredRight)
                {
                    this.Image = listImage[2];
                    isColoredLeft = false;
                }

            }
            if (e.Button == MouseButtons.Right)
            {
                if (!isColoredLeft && !isColoredRight)
                {
                    this.Image = listImage[2];
                    isColoredRight = true;
                }
                else if (isColoredLeft && !isColoredRight)
                {
                    this.Image = listImage[3];
                    isColoredRight = true;
                }
                else if (!isColoredLeft && isColoredRight)
                {
                    this.Image = listImage[0];
                    isColoredRight = false;
                }
                else if (isColoredLeft && isColoredRight)
                {
                    this.Image = listImage[1];
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
                this.DoDragDrop(this.listImage[4], DragDropEffects.Copy);
                isMouseDown = false;
            }
            if (e.Button == MouseButtons.Right && isMouseDown)
            {
                this.DoDragDrop(this.listImage[5], DragDropEffects.Copy);
                isMouseDown = false;
            }
        }


        public string saveItem()
        {
            return '"' + this.Name + "\" : \"" + listImage.FindIndex(x => x == this.Image).ToString() + '"';
        }

        public void loadItem(string value)
        {
            var image = listImage.Find(x => x.Tag.ToString() == value);
        }
    }
}
