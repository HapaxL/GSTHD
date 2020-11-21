using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TrackerOOT.EditorObjects
{
    class JSONItem : PictureBox
    {
        ObjectPoint Item;

        private Point MouseDownLocation;

        public JSONItem(ObjectPoint item)
        {
            Item = item;
            this.Name = item.Name;
            this.Location = new Point(item.X, item.Y);
            this.Size = item.Size;
            this.Image = Image.FromFile(@"Resources/" + item.ImageCollection[0]);

            this.MouseHover += JSONItem_MouseHover;
            this.MouseLeave += JSONItem_MouseLeave;
            this.MouseMove += JSONItem_MouseMove;
            this.MouseDown += JSONItem_MouseDown;
        }

        private void JSONItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownLocation = e.Location;
            }
        }

        private void JSONItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left = e.X + this.Left - MouseDownLocation.X;
                this.Top = e.Y + this.Top - MouseDownLocation.Y;
            }

            Item.X = this.Location.X;
            Item.Y = this.Location.Y;
        }

        private void JSONItem_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.Transparent;
        }

        private void JSONItem_MouseHover(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(90, 0, 0, 0);
        }
    }
}
