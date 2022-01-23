using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSTHD
{
    class Barren
    {
        public Settings Settings;

        public Label LabelPlace;
        public string Name;

        private Color[] Colors;
        private int ColorIndex;

        public Barren(Settings settings, string selectedPlace, Point lastLabelLocation, Label labelSettings)
        {
            Settings = settings;
            Name = selectedPlace;

            LabelPlace = new Label
            {
                Name = Guid.NewGuid().ToString(),
                Text = selectedPlace,
                ForeColor = labelSettings.ForeColor,
                BackColor = labelSettings.BackColor,
                Font = labelSettings.Font,
                Width = labelSettings.Width,
                Height = labelSettings.Height,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true,
            };
            LabelPlace.Location = new Point(0, lastLabelLocation.Y + LabelPlace.Height);
            LabelPlace.MouseDown += new MouseEventHandler(Mouse_ClickDown);

            Colors = new Color[Settings.DefaultBarrenColors.Length];
            for (int i = 0; i < Settings.DefaultBarrenColors.Length; i++)
            {
                Colors[i] = Color.FromName(Settings.DefaultBarrenColors[i]);
            }
            ColorIndex = 0;
            UpdateFromSettings();
        }

        public void UpdateFromSettings()
        {
            UpdateColor();
        }

        public void UpdateColor()
        {
            LabelPlace.ForeColor = Colors[Settings.EnableBarrenColors ? ColorIndex : 0];
        }

        private void Mouse_ClickDown(object sender, MouseEventArgs e)
        {
            if (!Settings.EnableBarrenColors)
                return;

            if (e.Button == MouseButtons.Left && ColorIndex < Colors.Length - 1)
            {
                ColorIndex++;
            }
            else if (e.Button == MouseButtons.Right && ColorIndex > 0)
            {
                ColorIndex--;
            }
            UpdateColor();
        }
    }
}
