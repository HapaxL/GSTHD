using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerOOT
{
    class Item : PictureBox
    {
        List<Image> listImage;
        bool isMouseDown = false;

        public Item(string name, List<Image> images, int x, int y)
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
            if(e.Button == MouseButtons.Left)
            {
                var index = listImage.FindIndex(x => x == this.Image);
                if(index == (listImage.Count-1))
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
                this.DoDragDrop(listImage[1], DragDropEffects.Copy);
                isMouseDown = false;
            }
        }


        public string saveItem()
        {
            return '"'+ this.Name + "\" : \"" + listImage.FindIndex(x => x == this.Image).ToString() + '"';
        }

        public void loadItem(string value)
        {
            var image = listImage.Find(x => x.Tag.ToString() == value);
        }
    }
}
