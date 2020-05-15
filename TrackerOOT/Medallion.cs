using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerOOT
{
    class Medallion : PictureBox
    {
        List<string> ListImageName;
        List<string> ListDungeon;
        bool IsMouseDown = false;

        public Label SelectedDungeon;
        int SelectedDungeon_Y_Delta = 20;

        public Medallion(List<string> images, int x, int y, List<string> dungeons, int size)
        {
            ListImageName = images;
            ListDungeon = dungeons;

            this.BackColor = Color.Transparent;
            this.Name = ListImageName[0];
            this.Image = (Image)Properties.Resources.ResourceManager.GetObject(ListImageName[0]);
            this.Size = new Size(size, size);
            this.Location = new Point(x, y);
            this.TabStop = false;
            this.AllowDrop = false;
            this.MouseUp += this.Click_MouseUp;
            this.MouseDown += this.Click_MouseDown;

            SelectedDungeon = new Label
            {
                Font = new Font("Consolas", 8, FontStyle.Bold),
                Text = ListDungeon[0],
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                AutoSize = true
            };
        }     

        public void SetSelectedDungeonLocation()
        {
            SelectedDungeon.Location = new Point(this.Location.X + this.Width / 2 - SelectedDungeon.Width / 2, this.Location.Y + SelectedDungeon_Y_Delta);
        }

        private void Click_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var index = ListImageName.FindIndex(x => x == this.Name) + 1;
                if (index <= 0 || index >= ListImageName.Count)
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(ListImageName[0]);
                    this.Name = ListImageName[0];
                }
                else
                {
                    this.Image = (Image)Properties.Resources.ResourceManager.GetObject(ListImageName[index]);
                    this.Name = ListImageName[index];
                }
            }

            if (e.Button == MouseButtons.Right)
            {
                var index = ListDungeon.FindIndex(x => x == SelectedDungeon.Text);
                if (index == (ListDungeon.Count - 1))
                    SelectedDungeon.Text = ListDungeon[0];
                else
                    SelectedDungeon.Text = ListDungeon[index + 1];
                SetSelectedDungeonLocation();
            }
        }

        private void Click_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1)
                IsMouseDown = false;
            else IsMouseDown = true;
        }
        
    }
}
