using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrackerOOT
{
    class Song : PictureBox
    {
        public List<string> ListImageName = new List<string>();
        public List<string> ListTinyImageName = new List<string>();
        string ActiveImageName;
        string ActiveTinyImageName;

        bool isMouseDown = false;
        public PictureBox TinyPictureBox;
        bool SongMode;
        bool AutoCheck;

        Size SongSize;

        public Song(ObjectPointSong data, bool songMode, bool autoCheck)
        {
            SongMode = songMode;
            AutoCheck = autoCheck;

            if (data.ImageCollection != null)
            {
                ListImageName = data.ImageCollection.ToList();
                ActiveImageName = data.ActiveSongImage;
            }

            if (data.TinyImageCollection != null)
            {
                ListTinyImageName = data.TinyImageCollection.ToList();
                ActiveTinyImageName = data.ActiveTinySongImage;
            }

            SongSize = data.Size;

            TinyPictureBox = new PictureBox
            {
                BackColor = Color.Transparent,
                TabStop = false,
                AllowDrop = false
            };
            TinyPictureBox.MouseUp += TinyPictureBox_MouseUp;
            TinyPictureBox.DragEnter += Click_DragEnter;
            TinyPictureBox.DragDrop += Click_DragDrop;
            TinyPictureBox.MouseDown += Click_MouseDown;
            TinyPictureBox.MouseMove += Click_MouseMove;
            this.Controls.Add(TinyPictureBox);
            TinyPictureBox.BringToFront();

            if (ListTinyImageName.Count > 0)
            {
                TinyPictureBox.Name = ListTinyImageName[0];
                TinyPictureBox.Image = Image.FromFile(@"Resources/" + ListTinyImageName[0]);
                TinyPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                TinyPictureBox.Size = new Size(TinyPictureBox.Image.Width, TinyPictureBox.Image.Height);
            }

            
            this.BackColor = Color.Transparent;
            this.Location = new Point(data.X, data.Y);
            this.TabStop = false;
            this.AllowDrop = true;
            this.MouseUp += this.Click_MouseUp;
            this.MouseDown += this.Click_MouseDown;
            this.MouseMove += this.Click_MouseMove;
            this.DragEnter += this.Click_DragEnter;
            this.DragDrop += this.Click_DragDrop;

            if (ListImageName.Count > 0)
            {
                this.Name = ListImageName[0];
                this.Image = Image.FromFile(@"Resources/" + this.Name);
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Size = new Size(SongSize.Width, SongSize.Height + (int)(TinyPictureBox.Height*5/6));
                TinyPictureBox.Location = new Point(
                    (SongSize.Width - TinyPictureBox.Width) / 2,
                    SongSize.Height - TinyPictureBox.Height / 6
                );

                if (data.DragAndDropImageName != string.Empty)
                    this.Tag = data.DragAndDropImageName;
                else
                    this.Tag = ListImageName[1];
            }
        }

        private void TinyPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var index = ListTinyImageName.FindIndex(x => x == TinyPictureBox.Name) + 1;
                if (index <= 0 || index >= ListTinyImageName.Count)
                {
                    TinyPictureBox.Image = Image.FromFile(@"Resources/" + ListTinyImageName[0]);
                    TinyPictureBox.Name = ListTinyImageName[0];
                }
                else
                {
                    TinyPictureBox.Image = Image.FromFile(@"Resources/" + ListTinyImageName[index]);
                    TinyPictureBox.Name = ListTinyImageName[index];
                }
            }
        }

        private void Click_DragDrop(object sender, DragEventArgs e)
        {
            var imageName = ((string)e.Data.GetData(DataFormats.Text));
            var tinyImage = Image.FromFile(@"Resources/" + imageName);

            TinyPictureBox.Image = tinyImage;
            TinyPictureBox.Name = imageName;

            if (SongMode)
            {
                if (AutoCheck)
                {
                    this.Image = Image.FromFile(@"Resources/" + ListImageName[1]);
                    this.Name = imageName;
                }
            }
            else
            {
                if (AutoCheck)
                {
                    var splitName = imageName.Split('_');
                    var newName = splitName[0] + "-bw_" + splitName[1];
                    var findOrigin = this.Parent.Controls.Find(newName, false);
                    if (findOrigin.Length > 0)
                    {
                        var origin = (Song)findOrigin[0];
                        origin.Click_MouseUp(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    }
                }
            }
        }

        private void Click_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        public void Click_MouseUp(object sender, MouseEventArgs e)
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
                this.DoDragDrop(this.Tag, DragDropEffects.Copy);
                isMouseDown = false;
            }
        }       
    }
}
