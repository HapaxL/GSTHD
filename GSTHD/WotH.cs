using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSTHD
{
    class WotH
    {
        public Settings Settings;

        public Label LabelPlace;
        public List<GossipStone> listGossipStone = new List<GossipStone>();
        public string Name;

        private Color[] Colors;
        private int ColorIndex;
        private int MinIndex;

        public WotH(Settings settings, string selectedPlace, int wothItemCount, string[] wothItemImageList, int pathGoalCount, string[] pathGoalImageList, Point lastLabelLocation, Label labelSettings, Size gossipStoneSize)
        {
            Settings = settings;
            Name = selectedPlace;

            int goalStoneStartX = 2;
            int labelStartX = 2;
            int labelWidth = labelSettings.Width;
            if (pathGoalCount > 0)
            {
                labelStartX += pathGoalCount * gossipStoneSize.Width + 4;
            }
            int gossipStoneStartX = labelStartX + labelWidth + 3;

            LabelPlace = new Label
            {
                Name = Guid.NewGuid().ToString(),
                Text = selectedPlace,
                ForeColor = labelSettings.ForeColor,
                BackColor = labelSettings.BackColor,
                Font = labelSettings.Font,
                Width = labelWidth,
                Height = labelSettings.Height,
                TextAlign = ContentAlignment.MiddleLeft,
            };
            LabelPlace.Location = new Point(labelStartX, lastLabelLocation.Y + LabelPlace.Height);
            LabelPlace.MouseDown += new MouseEventHandler(label_woth_MouseDown);

            if (wothItemImageList.Length > 0)
            {
                for (int i = 0; i < wothItemCount; i++)
                {
                    GossipStone newGossipStone = new GossipStone(Settings, Name + "_GossipStone" + i, 0, 0, wothItemImageList, gossipStoneSize);
                    newGossipStone.Location =
                        new Point(gossipStoneStartX + ((newGossipStone.Width + 2) * i), LabelPlace.Location.Y);
                    listGossipStone.Add(newGossipStone);
                }
            }

            if (pathGoalImageList?.Length > 0)
            {
                for (int i = 0; i < pathGoalCount; i++)
                {
                    GossipStone newGossipStone = new GossipStone(Settings, Name + "_GoalGossipStone" + i, 0, 0, pathGoalImageList, gossipStoneSize);
                    newGossipStone.Location =
                        new Point(goalStoneStartX + ((newGossipStone.Width + 2) * i), LabelPlace.Location.Y);
                    listGossipStone.Add(newGossipStone);
                }
            }

            Colors = new Color[Settings.DefaultWothColors.Length + 1];
            for (int i = 0; i < Settings.DefaultWothColors.Length; i++)
            {
                Colors[i + 1] = Color.FromName(Settings.DefaultWothColors[i]);
            }
            ColorIndex = Settings.DefaultWothColorIndex + 1;
            UpdateFromSettings();
        }

        public void UpdateFromSettings()
        {
            MinIndex = Settings.EnableLastWoth ? 0 : 1;
            Colors[0] = Color.FromKnownColor(Settings.LastWothColor);
            UpdateColor();
        }

        public void UpdateColor()
        {
            LabelPlace.ForeColor = Colors[ColorIndex];
        }

        private void label_woth_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && ColorIndex < Colors.Length - 1)
            {
                ColorIndex++;
            }
            else if (e.Button == MouseButtons.Right && ColorIndex > MinIndex)
            {
                ColorIndex--;
            }
            UpdateColor();
        }
    }
}
