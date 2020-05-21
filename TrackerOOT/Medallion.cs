using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrackerOOT
{
    class Medallion : PictureBox
    {
        List<string> ListImageName;
        List<string> ListDungeon;
        bool IsMouseDown = false;

        public Label SelectedDungeon;
        Size MedallionSize;

        public Medallion(ObjectPointMedallion data)
        {
            if(data.ImageCollection != null)
                ListImageName = data.ImageCollection.ToList();
            if(data.Label.TextCollection != null)
                ListDungeon = data.Label.TextCollection.ToList();

            MedallionSize = data.Size;

            if(ListImageName.Count > 0)
            {
                this.Name = ListImageName[0];
                this.Image = Image.FromFile(@"Resources/" + ListImageName[0]);
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Size = MedallionSize;
            }

            this.BackColor = Color.Transparent;
            this.Location = new Point(data.X, data.Y);
            this.TabStop = false;
            this.AllowDrop = false;
            this.MouseUp += this.Click_MouseUp;
            this.MouseDown += this.Click_MouseDown;

            SelectedDungeon = new Label
            {
                Font = new Font(new FontFamily(data.Label.FontName), data.Label.FontSize, data.Label.FontStyle),
                Text = ListDungeon[0],
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                AutoSize = true
            };
        }     

        public void SetSelectedDungeonLocation()
        {
            SelectedDungeon.Location = new Point(this.Location.X + MedallionSize.Width / 2 - SelectedDungeon.Width / 2, (int)(this.Location.Y + MedallionSize.Height * 0.75));
        }

        private void Click_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var index = ListImageName.FindIndex(x => x == this.Name) + 1;
                if (index <= 0 || index >= ListImageName.Count)
                {
                    this.Image = Image.FromFile(@"Resources/" + ListImageName[0]);
                    this.Name = ListImageName[0];
                }
                else
                {
                    this.Image = Image.FromFile(@"Resources/" + ListImageName[index]);
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
