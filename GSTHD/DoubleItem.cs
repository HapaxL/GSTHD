using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GSTHD
{
    class DoubleItem : PictureBox
    {
        List<string> ListImageName;
        bool isMouseDown = false;
        bool isColoredLeft = false;
        bool isColoredRight = false;
        Size DoubleItemSize;

        public DoubleItem(ObjectPoint data)
        {
            if(data.ImageCollection != null)
                ListImageName = data.ImageCollection.ToList();

            DoubleItemSize = data.Size;

            if (ListImageName.Count > 0)
            {
                this.Name = ListImageName[0];
                this.Image = Image.FromFile(@"Resources/" + ListImageName[0]);
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Size = DoubleItemSize;
            }

            this.BackColor = Color.Transparent;
            this.Location = new Point(data.X, data.Y);
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
                    this.Image = Image.FromFile(@"Resources/" + ListImageName[1]);
                    this.Name = ListImageName[1];
                    isColoredLeft = true;
                }
                else if (isColoredLeft && !isColoredRight)
                {
                    this.Image = Image.FromFile(@"Resources/" + ListImageName[0]);
                    this.Name = ListImageName[0];
                    isColoredLeft = false;
                }
                else if (!isColoredLeft && isColoredRight)
                {
                    this.Image = Image.FromFile(@"Resources/" + ListImageName[3]);
                    this.Name = ListImageName[3];
                    isColoredLeft = true;
                }
                else if (isColoredLeft && isColoredRight)
                {
                    this.Image = Image.FromFile(@"Resources/" + ListImageName[2]);
                    this.Name = ListImageName[2];
                    isColoredLeft = false;
                }

            }
            if (e.Button == MouseButtons.Right)
            {
                if (!isColoredLeft && !isColoredRight)
                {
                    this.Image = Image.FromFile(@"Resources/" + ListImageName[2]);
                    this.Name = ListImageName[2];
                    isColoredRight = true;
                }
                else if (isColoredLeft && !isColoredRight)
                {
                    this.Image = Image.FromFile(@"Resources/" + ListImageName[3]);
                    this.Name = ListImageName[3];
                    isColoredRight = true;
                }
                else if (!isColoredLeft && isColoredRight)
                {
                    this.Image = Image.FromFile(@"Resources/" + ListImageName[0]);
                    this.Name = ListImageName[0];
                    isColoredRight = false;
                }
                else if (isColoredLeft && isColoredRight)
                {
                    this.Image = Image.FromFile(@"Resources/" + ListImageName[1]);
                    this.Name = ListImageName[1];
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
                // TODO change that bool to DragBehaviour.AutocheckDragDrop
                var dropContent = new DragDropContent(false, ListImageName[4]);
                this.DoDragDrop(dropContent, DragDropEffects.Copy);
                isMouseDown = false;
            }
            if (e.Button == MouseButtons.Right && isMouseDown)
            {
                var dropContent = new DragDropContent(false, ListImageName[5]);
                this.DoDragDrop(dropContent, DragDropEffects.Copy);
                isMouseDown = false;
            }
        }
    }
}
