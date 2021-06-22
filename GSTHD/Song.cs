using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GSTHD
{
    class Song : PictureBox
    {
        Settings Settings;

        public List<string> ImageNames = new List<string>();
        public string[] SongMarkerImageNames;
        string ActiveImageName;
        string ActiveSongMarkerImageName;

        int imageIndex = 0;
        int songMarkerImageIndex = 0;
        public PictureBox SongMarker;

        Size SongSize;

        public Song(ObjectPointSong data, Settings settings)
        {
            Settings = settings;

            if (data.ImageCollection != null)
            {
                ImageNames = data.ImageCollection.ToList();
                ActiveImageName = data.ActiveSongImage;
            }

            SongSize = data.Size;
            
            this.BackColor = Color.Transparent;
            this.Location = new Point(data.X, data.Y);
            this.TabStop = false;
            this.AllowDrop = true;
            this.MouseUp += this.Mouse_MiddleClick_Up;
            this.MouseDown += this.Mouse_LeftClick_Down;
            this.MouseDown += this.Mouse_RightClick_Down;
            this.MouseMove += this.Mouse_Move;
            this.DragEnter += this.Mouse_MiddleClick_Drag;

            if (ImageNames.Count > 0)
            {
                this.Name = ImageNames[0];
                this.Image = Image.FromFile(@"Resources/" + this.Name);
                this.SizeMode = PictureBoxSizeMode.Zoom;

                if (data.DragAndDropImageName != string.Empty)
                    this.Tag = data.DragAndDropImageName;
                else
                    this.Tag = ImageNames[1];
            }

            if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourEnum.None)
            {
                this.Size = new Size(SongSize.Width, SongSize.Height);
            }
            else
            {
                if (data.TinyImageCollection == null)
                {
                    SongMarkerImageNames = Settings.DefaultSongMarkerImages;
                }
                else
                {
                    SongMarkerImageNames = data.TinyImageCollection;
                    ActiveSongMarkerImageName = data.ActiveTinySongImage;
                }

                SongMarker = new PictureBox
                {
                    BackColor = Color.Transparent,
                    TabStop = false,
                    AllowDrop = false,
                };

                this.Controls.Add(SongMarker);
                SongMarker.BringToFront();
                
                if (SongMarkerImageNames.Length > 0)
                {
                    SongMarker.Name = SongMarkerImageNames[0];
                    SongMarker.Image = Image.FromFile(@"Resources/" + SongMarkerImageNames[0]);
                    SongMarker.SizeMode = PictureBoxSizeMode.StretchImage;
                    SongMarker.Size = new Size(SongMarker.Image.Width, SongMarker.Image.Height);
                    if (data.DragAndDropImageName != string.Empty)
                        SongMarker.Tag = data.DragAndDropImageName;
                    else
                        SongMarker.Tag = ImageNames[1];
                }

                if (ImageNames.Count > 0)
                {
                    this.Size = new Size(SongSize.Width, SongSize.Height + (int)(SongMarker.Height * 5 / 6));

                    SongMarker.Location = new Point(
                        (SongSize.Width - SongMarker.Width) / 2,
                        SongSize.Height - SongMarker.Height / 6
                    );
                }

                SongMarker.MouseDown += Mouse_RightClick_Down_SongMarker;
                SongMarker.MouseUp += Mouse_MiddleClick_Up_SongMarker;
                SongMarker.DragEnter += Mouse_MiddleClick_Drag;

                if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourEnum.CheckOnly)
                {
                    SongMarker.MouseDown += Mouse_LeftClick_Down_SongMarker;
                    SongMarker.MouseMove += Mouse_Move;
                }

                if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourEnum.DropOnly)
                {
                    this.DragDrop += Mouse_MiddleClick_Drop;
                    SongMarker.MouseMove += Mouse_Move;
                }

                if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourEnum.DropAndCheck)
                {
                    this.DragDrop += Mouse_MiddleClick_Drop;
                    SongMarker.MouseDown += Mouse_LeftClick_Down_SongMarker;
                    SongMarker.MouseMove += Mouse_Move;
                }
    
                if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourEnum.DragAndDrop)
                {
                    this.DragDrop += Mouse_MiddleClick_Drop;
                    SongMarker.MouseMove += Mouse_Move_SongMarker;
                }
    
                if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourEnum.Full)
                {
                    this.DragDrop += Mouse_MiddleClick_Drop;
                    SongMarker.MouseDown += Mouse_LeftClick_Down_SongMarker;
                    SongMarker.MouseMove += Mouse_Move_SongMarker;
                }
            }
        }

        private void SetImage()
        {
            Image = Image.FromFile(@"Resources/" + ImageNames[imageIndex]);
            Name = ImageNames[imageIndex];
        }

        private void SetSongMarkerImage()
        {
            SongMarker.Image = Image.FromFile(@"Resources/" + SongMarkerImageNames[songMarkerImageIndex]);
            SongMarker.Name = SongMarkerImageNames[songMarkerImageIndex];
        }

        private void Mouse_LeftClick_Down(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (imageIndex < ImageNames.Count - 1)
                {
                    imageIndex += 1;
                }
                SetImage();
            }
        }

        private void Mouse_RightClick_Down(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (imageIndex > 0)
                {
                    imageIndex -= 1;
                }
                SetImage();
            }
        }

        private void Mouse_LeftClick_Down_SongMarker(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (songMarkerImageIndex < SongMarkerImageNames.Length - 1)
                {
                    songMarkerImageIndex += 1;
                }
                SetSongMarkerImage();
            }
        }

        private void Mouse_RightClick_Down_SongMarker(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (songMarkerImageIndex > 0)
                {
                    songMarkerImageIndex -= 1;
                }
                SetSongMarkerImage();
            }
        }

        public void Mouse_MiddleClick_Up(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                imageIndex = 0;
                SetImage();
            }
        }

        public void Mouse_MiddleClick_Up_SongMarker(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                songMarkerImageIndex = 0;
                SetSongMarkerImage();
            }
        }

        private void Mouse_MiddleClick_Drop(object sender, DragEventArgs e)
        {
            var imageName = ((string)e.Data.GetData(DataFormats.Text));
            var image = Image.FromFile(@"Resources/" + imageName);

            SongMarker.Name = imageName;
            SongMarker.Image = image;

            if (Settings.MoveLocationToSong)
            {
                if (Settings.AutoCheckSongs)
                {
                    this.Image = Image.FromFile(@"Resources/" + ImageNames[1]);
                    this.Name = imageName;
                }
            }
            else
            {
                if (Settings.AutoCheckSongs)
                {
                    var splitName = imageName.Split('_');
                    var newName = splitName[0] + "-bw_" + splitName[1];
                    var findOrigin = this.Parent.Controls.Find(newName, false);
                    if (findOrigin.Length > 0)
                    {
                        var origin = (Song)findOrigin[0];
                        origin.Mouse_LeftClick_Down(this, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                    }
                }
            }
        }

        private void Mouse_MiddleClick_Drag(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                this.DoDragDrop(this.Tag, DragDropEffects.Copy);
            }
        }

        private void Mouse_Move_SongMarker(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                var draggedImageName = SongMarker.Name;
                imageIndex = 0;
                SetSongMarkerImage();
                this.DoDragDrop(draggedImageName, DragDropEffects.Copy);
            }
        }
    }
}
