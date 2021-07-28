using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GSTHD
{
    public class Medallion : PictureBox, UpdatableFromSettings, ProgressibleElement<int>, DraggableAutocheckElement<int>
    {
        private readonly Settings Settings;
        private readonly ProgressibleElementBehaviour<int> ProgressBehaviour;
        private readonly DraggableAutocheckElementBehaviour<int> DragBehaviour;

        private string[] ImageNames;
        private string[] DungeonNames;
        private bool Wraparound;
        private int ImageIndex = 0;

        private int DefaultDungeonIndex;
        private int DungeonIndex;

        public Label SelectedDungeon;

        public Medallion(ObjectPointMedallion data, Settings settings)
        {
            Settings = settings;

            if (data.ImageCollection == null)
                ImageNames = new string[0];
            else
                ImageNames = data.ImageCollection;

            if (data.Label == null)
                data.Label = Settings.DefaultDungeonNames;
            else
            {
                if (data.Label.TextCollection == null)
                    data.Label.TextCollection = Settings.DefaultDungeonNames.TextCollection;
                if (!data.Label.DefaultValue.HasValue)
                    data.Label.DefaultValue = Settings.DefaultDungeonNames.DefaultValue;
                if (!data.Label.Wraparound.HasValue)
                    data.Label.Wraparound = Settings.DefaultDungeonNames.Wraparound;
                if (data.Label.FontName == null)
                    data.Label.FontName = Settings.DefaultDungeonNames.FontName;
                if (!data.Label.FontSize.HasValue)
                    data.Label.FontSize = Settings.DefaultDungeonNames.FontSize;
                if (!data.Label.FontStyle.HasValue)
                    data.Label.FontStyle = Settings.DefaultDungeonNames.FontStyle;
            }

            DungeonNames = data.Label.TextCollection;
            DefaultDungeonIndex = data.Label.DefaultValue.Value;
            DungeonIndex = DefaultDungeonIndex;
            Wraparound = data.Label.Wraparound.Value;

            Name = data.Name;
            BackColor = Color.Transparent;

            if (ImageNames.Length > 0)
            {
                UpdateImage();
                SizeMode = PictureBoxSizeMode.StretchImage;
                Size = data.Size;
            }

            ProgressBehaviour = new ProgressibleElementBehaviour<int>(this, Settings);
            DragBehaviour = new DraggableAutocheckElementBehaviour<int>(this, Settings);

            Location = new Point(data.X, data.Y);
            TabStop = false;
            AllowDrop = false;
            MouseUp += DragBehaviour.Mouse_ClickUp;
            MouseDown += ProgressBehaviour.Mouse_ClickDown;
            MouseDown += DragBehaviour.Mouse_ClickDown;
            MouseMove += DragBehaviour.Mouse_Move_WithAutocheck;

            SelectedDungeon = new Label
            {
                Font = new Font(new FontFamily(data.Label.FontName), data.Label.FontSize.Value, data.Label.FontStyle.Value),
                Text = DungeonNames[DefaultDungeonIndex],
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                AutoSize = true,
            };

            SelectedDungeon.Location = new Point(Location.X + Size.Width / 2, (int)(Location.Y + Size.Height * 0.75));

            SelectedDungeon.MouseUp += DragBehaviour.Mouse_ClickUp;
            SelectedDungeon.MouseDown += ProgressBehaviour.Mouse_ClickDown;
            SelectedDungeon.MouseDown += DragBehaviour.Mouse_ClickDown;
            SelectedDungeon.MouseMove += DragBehaviour.Mouse_Move_WithAutocheck;

            UpdateFromSettings();
        }

        public void UpdateFromSettings()
        {
            MouseWheel -= Mouse_Wheel;
            MouseWheel -= Mouse_Wheel_WithWraparound;
            SelectedDungeon.MouseWheel -= Mouse_Wheel;
            SelectedDungeon.MouseWheel -= Mouse_Wheel_WithWraparound;

            if (Settings.WraparoundDungeonNames)
            {
                MouseWheel += Mouse_Wheel_WithWraparound;
                SelectedDungeon.MouseWheel += Mouse_Wheel_WithWraparound;
            }
            else
            {
                MouseWheel += Mouse_Wheel;
                SelectedDungeon.MouseWheel += Mouse_Wheel;
            }
        }

        public void SetSelectedDungeonLocation()
        {
            SelectedDungeon.Location = new Point(Location.X + Size.Width / 2 - SelectedDungeon.Width / 2, (int)(Location.Y + Size.Height * 0.75));
        }

        private void Mouse_Wheel(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                var scrolls = e.Delta / SystemInformation.MouseWheelScrollDelta;
                DungeonIndex += (Settings.InvertScrollWheel ? scrolls : -scrolls);
                if (DungeonIndex < 0) DungeonIndex = 0;
                else if (DungeonIndex >= DungeonNames.Length) DungeonIndex = DungeonNames.Length - 1;
                SelectedDungeon.Text = DungeonNames[DungeonIndex];
                SetSelectedDungeonLocation();
            }
        }

        private void Mouse_Wheel_WithWraparound(object sender, MouseEventArgs e)
        {
            if (e.Delta != 0)
            {
                var scrolls = e.Delta / SystemInformation.MouseWheelScrollDelta;
                var newIndex = DungeonIndex + (Settings.InvertScrollWheel ? scrolls : -scrolls);
                DungeonIndex = Math.EMod(newIndex, DungeonNames.Length);
                SelectedDungeon.Text = DungeonNames[DungeonIndex];
                SetSelectedDungeonLocation();
            }
        }

        private void UpdateImage()
        {
            Image = Image.FromFile(@"Resources/" + ImageNames[ImageIndex]);
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
            if (ImageIndex < ImageNames.Length - 1) ImageIndex += 1;
            UpdateImage();
        }

        public void DecrementState()
        {
            if (ImageIndex > 0) ImageIndex -= 1;
            UpdateImage();
        }

        public void ResetState()
        {
            DungeonIndex = DefaultDungeonIndex;
            SelectedDungeon.Text = DungeonNames[DungeonIndex];
            SetSelectedDungeonLocation();
        }

        public void StartDragDrop()
        {
            var dropContent = new DragDropContent(DragBehaviour.AutocheckDragDrop, ImageNames[1]);
            DoDragDrop(dropContent, DragDropEffects.Copy);
        }

        public void SaveChanges() { }
        public void CancelChanges() { }
    }
}
