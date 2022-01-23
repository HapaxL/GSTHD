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

        public LabelExtended LabelPlace;
        public List<GossipStone> listGossipStone = new List<GossipStone>();
        public string Name;

        private Color[] Colors;
        private int ColorIndex;
        private int MinIndex;

        public WotH(Settings settings,
            string selectedPlace,
            int gossipStoneCount, string[] wothItemImageList, int gossipStoneSpacing,
            int pathGoalCount, string[] pathGoalImageList, int pathGoalSpacing,
            Point lastLabelLocation, Label labelSettings, Size gossipStoneSize)
        {
            Settings = settings;
            Name = selectedPlace;

            var labelStartX = 0;

            if (pathGoalCount > 0)
            {
                labelStartX += pathGoalCount * (gossipStoneSize.Width + pathGoalSpacing) - pathGoalSpacing;
            }
            int panelWidth = labelSettings.Width;
            int labelWidth = panelWidth - labelStartX - gossipStoneCount * (gossipStoneSize.Width + gossipStoneSpacing) + gossipStoneSpacing;

            var gossipStoneStartX = panelWidth - gossipStoneCount * (gossipStoneSize.Width + gossipStoneSpacing) + gossipStoneSpacing;

            LabelPlace = new LabelExtended
            {
                Name = Guid.NewGuid().ToString(),
                Text = selectedPlace,
                ForeColor = labelSettings.ForeColor,
                BackColor = labelSettings.BackColor,
                Font = labelSettings.Font,
                Width = labelWidth,
                Height = labelSettings.Height,
                TextAlign = ContentAlignment.MiddleLeft,
                AutoEllipsis = true,
            };
            LabelPlace.Location = new Point(labelStartX, lastLabelLocation.Y + LabelPlace.Height);
            LabelPlace.MouseDown += new MouseEventHandler(Mouse_ClickDown);

            if (wothItemImageList.Length > 0)
            {
                for (int i = 0; i < gossipStoneCount; i++)
                {
                    GossipStone newGossipStone = new GossipStone(Settings, Name + "_GossipStone" + i, 0, 0, wothItemImageList, gossipStoneSize);
                    newGossipStone.Location =
                        new Point(gossipStoneStartX + (newGossipStone.Width + gossipStoneSpacing) * i, LabelPlace.Location.Y);
                    listGossipStone.Add(newGossipStone);
                }
            }

            if (pathGoalImageList != null && pathGoalImageList.Length > 0)
            {
                for (int i = 0; i < pathGoalCount; i++)
                {
                    GossipStone newGossipStone = new GossipStone(Settings, Name + "_GoalGossipStone" + i, 0, 0, pathGoalImageList, gossipStoneSize);
                    newGossipStone.Location =
                        new Point((newGossipStone.Width + pathGoalSpacing) * i, LabelPlace.Location.Y);
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

        private void Mouse_ClickDown(object sender, MouseEventArgs e)
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
