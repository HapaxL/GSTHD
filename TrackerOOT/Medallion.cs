using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerOOT
{
    class Medallion : PictureBox
    {
        List<Image> ListImage;
        List<string> ListDungeon;
        bool IsMouseDown = false;

        public Label SelectedDungeon;
        int SelectedDungeon_Y_Delta = 24;

        public Medallion(string name, List<Image> images, int x, int y, List<string> dungeons)
        {
            ListImage = images;
            ListDungeon = dungeons;

            this.BackColor = Color.Transparent;
            this.Name = name;
            this.Image = ListImage[0];
            this.Size = new Size(32, 32);
            this.Location = new Point(x, y);
            this.TabStop = false;
            this.AllowDrop = false;
            this.MouseUp += this.Click_MouseUp;
            this.MouseDown += this.Click_MouseDown;
            this.MouseMove += this.Click_MouseMove;

            SelectedDungeon = new Label
            {
                Font = new Font("Consolas", 8, FontStyle.Bold),
                Text = ListDungeon[0],
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                AutoSize = true
            };
            SelectedDungeon.Location = new Point(x, y + SelectedDungeon_Y_Delta);
        }     

        private void Click_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var index = ListImage.FindIndex(x => x == this.Image);
                if (index == (ListImage.Count - 1))
                    this.Image = ListImage[0];
                else
                    this.Image = ListImage[index + 1];
            }

            if (e.Button == MouseButtons.Right)
            {
                var index = ListDungeon.FindIndex(x => x == SelectedDungeon.Text);
                if (index == (ListDungeon.Count - 1))
                    SelectedDungeon.Text = ListDungeon[0];
                else
                    SelectedDungeon.Text = ListDungeon[index + 1];
                SelectedDungeon.Location = new Point(this.Location.X + this.Width / 2 - SelectedDungeon.Width / 2, this.Location.Y + SelectedDungeon_Y_Delta);
            }
        }

        private void Click_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1)
                IsMouseDown = false;
            else IsMouseDown = true;
        }


        private void Click_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && IsMouseDown)
            {
                this.DoDragDrop(this.Image, DragDropEffects.Copy);
                IsMouseDown = false;
            }
        }


        public string saveItem()
        {
            return '"' + this.Name + "\" : \"" + ListImage.FindIndex(x => x == this.Image).ToString() + '"';
        }

        public void loadItem(string value)
        {
            var image = ListImage.Find(x => x.Tag.ToString() == value);
        }
    }
}
