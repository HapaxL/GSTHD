using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GSTHD
{
    class Medallion : PictureBox
    {
        private readonly Settings Settings;

        List<string> ListImageName;
        List<string> ListDungeon;
        bool isMouseDown = false;
        int imageIndex = 0;
        int dungeonIndex = 0;

        public Label SelectedDungeon;
        Size MedallionSize;

        public Medallion(ObjectPointMedallion data, Settings settings)
        {
            Settings = settings;

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
            this.MouseMove += this.Click_MouseMove;
            this.MouseWheel += this.Click_MouseWheel;

            SelectedDungeon = new Label
            {
                Font = new Font(new FontFamily(data.Label.FontName), data.Label.FontSize, data.Label.FontStyle),
                Text = ListDungeon[0],
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                AutoSize = true
            };
            SelectedDungeon.MouseUp += this.Click_MouseUp;
            SelectedDungeon.MouseDown += this.Click_MouseDown;
            SelectedDungeon.MouseMove += this.Click_MouseMove;
            SelectedDungeon.MouseWheel += this.Click_MouseWheel;
        }     

        public void SetSelectedDungeonLocation()
        {
            SelectedDungeon.Location = new Point(this.Location.X + MedallionSize.Width / 2 - SelectedDungeon.Width / 2, (int)(this.Location.Y + MedallionSize.Height * 0.75));
        }

        private void Click_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                dungeonIndex = 0;
                SelectedDungeon.Text = ListDungeon[dungeonIndex];
                SetSelectedDungeonLocation();
                return;
            }
        }

        private void Click_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks != 1)
                isMouseDown = false;
            else isMouseDown = true;

            if (e.Button == MouseButtons.Left && imageIndex < ListImageName.Count - 1)
            {
                imageIndex += 1;
            }

            if (e.Button == MouseButtons.Right && imageIndex > 0)
            {
                imageIndex -= 1;
            }

            Image = Image.FromFile(@"Resources/" + ListImageName[imageIndex]);
            Name = ListImageName[imageIndex];
        }

        private void Click_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle && isMouseDown)
            {
                this.DoDragDrop(ListImageName[1], DragDropEffects.Copy);
                isMouseDown = false;
            }
        }

        private void Click_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                var scrolls = e.Delta / SystemInformation.MouseWheelScrollDelta;
                var newIndex = dungeonIndex + (Settings.InvertScrollWheel ? scrolls : -scrolls);
                dungeonIndex = Math.EMod(newIndex, ListDungeon.Count);
                SelectedDungeon.Text = ListDungeon[dungeonIndex];
                SetSelectedDungeonLocation();
            }
        }
    }
}
