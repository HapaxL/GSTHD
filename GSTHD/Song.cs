using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GSTHD
{
    public struct SongMarkerState
    {
        public bool HoldsImage;
        public string HeldImageName;
        public int ImageIndex;
    }

    public class SongMarker : PictureBox, UpdatableFromSettings, ProgressibleElement<SongMarkerState>, DraggableElement<SongMarkerState>
    {
        private readonly Settings Settings;
        private readonly ProgressibleElementBehaviour<SongMarkerState> ProgressBehaviour;
        private readonly DraggableElementBehaviour<SongMarkerState> DragBehaviour;

        private string[] ImageNames;
        private bool HoldsImage;
        private string HeldImageName;
        private int ImageIndex = 0;

        private bool RemoveImage;

        public Song Song;

        public SongMarker(Song song, Settings settings, string[] imageCollection)
        {
            Song = song;
            Settings = settings;
            ProgressBehaviour = new ProgressibleElementBehaviour<SongMarkerState>(this, settings);
            DragBehaviour = new DraggableElementBehaviour<SongMarkerState>(this, settings);

            if (imageCollection == null)
            {
                ImageNames = Settings.DefaultSongMarkerImages;
            }
            else
            {
                ImageNames = imageCollection;
            }

            Visible = true;

            if (ImageNames.Length > 0)
            {
                Name = Song.Name + "_SongMarker";
                UpdateImage();
                SizeMode = PictureBoxSizeMode.StretchImage;
                Size = new Size(Image.Width, Image.Height);

                //if (data.DragAndDropImageName != string.Empty)
                //    SongMarker.Tag = data.DragAndDropImageName;
                //else
                //    SongMarker.Tag = ImageNames[1];

            }
        }

        public void UpdateFromSettings()
        {
            MouseDown -= ProgressBehaviour.Mouse_ClickDown;
            MouseDown -= Mouse_MiddleClickDown;
            MouseUp -= DragBehaviour.Mouse_ClickUp;
            MouseDown -= DragBehaviour.Mouse_ClickDown;
            MouseMove -= Mouse_Move;
            DragEnter -= Mouse_DragEnter;
            DragDrop -= Mouse_DragDrop;

            if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourOption.None)
            {
                Visible = false;
            }
            else
            {
                Visible = true;

                if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourOption.CheckOnly)
                {
                    MouseDown += ProgressBehaviour.Mouse_ClickDown;
                }
                else if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourOption.DropOnly)
                {
                    MouseDown += Mouse_MiddleClickDown;
                    MouseUp += DragBehaviour.Mouse_ClickUp;
                    DragDrop += Mouse_DragDrop;
                }
                else if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourOption.DropAndCheck)
                {
                    MouseDown += ProgressBehaviour.Mouse_ClickDown;
                    MouseUp += DragBehaviour.Mouse_ClickUp;
                    DragDrop += Mouse_DragDrop;
                }
                else if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourOption.DragAndDrop)
                {
                    MouseDown += Mouse_MiddleClickDown;
                    MouseUp += DragBehaviour.Mouse_ClickUp;
                    MouseDown += DragBehaviour.Mouse_ClickDown;
                    MouseMove += Mouse_Move;
                    DragEnter += Mouse_DragEnter;
                    DragDrop += Mouse_DragDrop;
                }
                else if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourOption.Full)
                {
                    MouseDown += ProgressBehaviour.Mouse_ClickDown;
                    MouseUp += DragBehaviour.Mouse_ClickUp;
                    MouseDown += DragBehaviour.Mouse_ClickDown;
                    MouseMove += Mouse_Move;
                    DragEnter += Mouse_DragEnter;
                    DragDrop += Mouse_DragDrop;
                }
            }
        }

        public void UpdateImage()
        {
            if (HoldsImage)
            {
                Image = Image.FromFile(@"Resources/" + HeldImageName);
            }
            else
            {
                Image = Image.FromFile(@"Resources/" + ImageNames[ImageIndex]);
            }
        }

        public void Mouse_MiddleClickDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Middle:
                    ProgressBehaviour.Mouse_MiddleClickDown(sender, e);
                    break;
            }
        }

        private void Mouse_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            if (HoldsImage)
            {
                DragBehaviour.Mouse_Move(sender, e);
            }
        }

        public void Mouse_DragDrop(object sender, DragEventArgs e)
        {
            ImageIndex = 0;
            HoldsImage = true;
            var dropContent = (DragDropContent)e.Data.GetData(typeof(DragDropContent));
            HeldImageName = dropContent.ImageName;
            UpdateImage();
            DragBehaviour.SaveChanges();
        }

        public SongMarkerState GetState()
        {
            return new SongMarkerState()
            {
                HoldsImage = HoldsImage,
                HeldImageName = HeldImageName,
                ImageIndex = ImageIndex,
            };
        }

        public void SetState(SongMarkerState state)
        {
            HoldsImage = state.HoldsImage;
            HeldImageName = state.HeldImageName;
            ImageIndex = state.ImageIndex;
        }

        public void IncrementState()
        {
            RemoveImage = true;
            if (ImageIndex < ImageNames.Length - 1) ImageIndex += 1;
            UpdateImage();
        }

        public void DecrementState()
        {
            RemoveImage = true;
            if (ImageIndex > 0) ImageIndex -= 1;
            UpdateImage();
        }

        public void ResetState()
        {
            RemoveImage = true;
            ImageIndex = 0;
            UpdateImage();
        }

        public void StartDragDrop()
        {
            HoldsImage = false;
            UpdateImage();
            var dropContent = new DragDropContent(false, HeldImageName);
            DoDragDrop(dropContent, DragDropEffects.Copy);
            SaveChanges();
        }

        public void SaveChanges()
        {
            if (RemoveImage)
            {
                HoldsImage = false;
                RemoveImage = false;
                UpdateImage();
            }
        }

        public void CancelChanges() { }
    }

    public class Song : PictureBox, UpdatableFromSettings, ProgressibleElement<int>, DraggableAutocheckElement<int>
    {
        private readonly Settings Settings;
        private readonly ProgressibleElementBehaviour<int> ProgressBehaviour;
        private readonly DraggableAutocheckElementBehaviour<int> DragBehaviour;

        private readonly string DragPictureName;

        public string[] ImageNames;
        string ActiveImageName;

        private int ImageIndex = 0;

        public SongMarker SongMarker;

        Size SongSize;

        public Song(ObjectPointSong data, Settings settings)
        {
            Settings = settings;
            ProgressBehaviour = new ProgressibleElementBehaviour<int>(this, settings);
            DragBehaviour = new DraggableAutocheckElementBehaviour<int>(this, settings);

            if (data.ImageCollection != null)
            {
                ImageNames = data.ImageCollection;
                ActiveImageName = data.ActiveSongImage;
            }

            Name = data.Name;
            SongSize = data.Size;
            
            BackColor = Color.Transparent;
            Location = new Point(data.X, data.Y);
            TabStop = false;
            AllowDrop = true;

            if (ImageNames.Length > 0)
            {
                UpdateImage();
                SizeMode = PictureBoxSizeMode.Zoom;

                if (string.IsNullOrEmpty(data.DragAndDropImageName))
                    DragPictureName = ImageNames[1];
                else
                    DragPictureName = data.DragAndDropImageName;
            }

            SongMarker = new SongMarker(this, settings, data.TinyImageCollection)
            {
                BackColor = Color.Transparent,
                TabStop = false,
                AllowDrop = false,
            };

            Size = new Size(SongSize.Width, SongSize.Height + SongMarker.Height * 5 / 6);
            Controls.Add(SongMarker);
            SongMarker.BringToFront();

            MouseUp += DragBehaviour.Mouse_ClickUp;
            MouseDown += ProgressBehaviour.Mouse_ClickDown;
            MouseDown += DragBehaviour.Mouse_ClickDown;
            DragEnter += Mouse_DragEnter;

            UpdateFromSettings();
        }

        public void UpdateFromSettings()
        {
            SongMarker.DragEnter -= Mouse_DragEnter;
            DragDrop -= Mouse_DragDrop;
            DragDrop -= Mouse_DragDrop_WithMoveLocationToSong;
            DragDrop -= SongMarker.Mouse_DragDrop;
            MouseMove -= Mouse_Move;
            MouseMove -= Mouse_Move_WithMoveLocationToSong;
            SongMarker.MouseMove -= Mouse_Move;
            SongMarker.MouseMove -= Mouse_Move_WithMoveLocationToSong;

            if (Settings.SongMarkerBehaviour != Settings.SongMarkerBehaviourOption.None)
            {
                if (ImageNames.Length > 0)
                {
                    SongMarker.Location = new Point(
                        (SongSize.Width - SongMarker.Width) / 2,
                        SongSize.Height - SongMarker.Height / 6
                    );
                }

                if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourOption.CheckOnly)
                {
                    MouseMove += Mouse_Move;
                    SongMarker.MouseMove += Mouse_Move;
                }
                else if (Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourOption.DropOnly
                    || Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourOption.DropAndCheck
                    || Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourOption.DragAndDrop
                    || Settings.SongMarkerBehaviour == Settings.SongMarkerBehaviourOption.Full)
                {
                    SongMarker.DragEnter += Mouse_DragEnter;
                    DragDrop += SongMarker.Mouse_DragDrop;

                    if (Settings.MoveLocationToSong)
                    {
                        DragDrop += Mouse_DragDrop_WithMoveLocationToSong;
                        MouseMove += Mouse_Move_WithMoveLocationToSong;
                        SongMarker.MouseMove += Mouse_Move_WithMoveLocationToSong;
                    }
                    else
                    {
                        DragDrop += Mouse_DragDrop;
                        MouseMove += Mouse_Move;
                        SongMarker.MouseMove += Mouse_Move;
                    }
                }
            }

            SongMarker.UpdateFromSettings();
        }

        private void UpdateImage()
        {
            Image = Image.FromFile(@"Resources/" + ImageNames[ImageIndex]);
        }

        private void Mouse_Move(object sender, MouseEventArgs e)
        {
            DragBehaviour.Mouse_Move_WithAutocheck(sender, e);
        }

        private void Mouse_Move_WithMoveLocationToSong(object sender, MouseEventArgs e)
        {
            DragBehaviour.Mouse_Move(sender, e);
        }

        private void Mouse_DragDrop(object sender, DragEventArgs e) { }

        private void Mouse_DragDrop_WithMoveLocationToSong(object sender, DragEventArgs e)
        {
            var dropContent = (DragDropContent)e.Data.GetData(typeof(DragDropContent));
            if (dropContent.IsAutocheck)
            {
                IncrementState();
                DragBehaviour.SaveChanges();
            }
        }

        private void Mouse_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        public int GetState()
        {
            return ImageIndex;
        }

        public void SetState(int state)
        {
            ImageIndex = state;
            UpdateImage();
        }

        public void IncrementState()
        {
            if (ImageIndex < ImageNames.Length - 1)
            {
                ImageIndex += 1;
                UpdateImage();
            }
        }

        public void DecrementState()
        {
            if (ImageIndex > 0)
            {
                ImageIndex -= 1;
                UpdateImage();
            }
        }

        public void ResetState()
        {
            ImageIndex = 0;
            UpdateImage();
        }

        public void StartDragDrop()
        {
            var dropContent = new DragDropContent(DragBehaviour.AutocheckDragDrop, DragPictureName);
            DoDragDrop(dropContent, DragDropEffects.Copy);
        }

        public void SaveChanges() { }
        public void CancelChanges() { }
    }
}
