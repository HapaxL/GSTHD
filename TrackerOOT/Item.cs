using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrackerOOT
{
    class Item : PictureBox
    {
        List<string> listImageName = new List<string>();
        bool isMouseDown = false;

        Size ItemSize;
        public Item(ObjectPoint data)
        {
            if(data.ImageCollection != null)
                listImageName = data.ImageCollection.ToList();

            ItemSize = data.Size;

            this.BackColor = Color.Transparent;
            if (listImageName.Count > 0)
            {
                this.Name = listImageName[0];
                this.Image = Image.FromFile(@"Resources/" + listImageName[0]);
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Size = ItemSize;
            }            
            this.Location = new Point(data.X, data.Y);
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
                    this.Image = Image.FromFile(@"Resources/" + listImageName[0]);
                    this.Name = listImageName[0];
                }
                else
                {
                    this.Image = Image.FromFile(@"Resources/" + listImageName[index]);
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
