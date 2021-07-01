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
        public Label LabelPlace;
        public List<GossipStone> listGossipStone = new List<GossipStone>();
        public string Name;

        private Color[] Colors;
        private int ColorIndex;

        public WotH(Settings settings, string selectedPlace, string[] listImage, Point lastLabelLocation, Label labelSettings, Size gossipStoneSize)
        {
            this.Name = selectedPlace;

            this.LabelPlace = new Label
            {
                Name = Guid.NewGuid().ToString(),
                Text = selectedPlace,
                ForeColor = labelSettings.ForeColor,
                BackColor = labelSettings.BackColor,
                Font = labelSettings.Font,
                Width = labelSettings.Width,
                Height = labelSettings.Height,
                TextAlign = ContentAlignment.MiddleLeft,
            };

            this.LabelPlace.Location = new Point(2, lastLabelLocation.Y + LabelPlace.Height);
            this.LabelPlace.MouseDown += new MouseEventHandler(label_woth_MouseDown);

            if (listImage.Length > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    GossipStone newGossipStone = new GossipStone(this.Name + "_GossipStone" + i, 0, 0, listImage, gossipStoneSize);
                    newGossipStone.Location =
                        new Point(LabelPlace.Width + 5 + ((newGossipStone.Width + 2) * i), LabelPlace.Location.Y);
                    listGossipStone.Add(newGossipStone);
                }
            }

            Colors = new Color[settings.WothColors.Length];
            for (int i = 0; i < settings.WothColors.Length; i++)
            {
                Colors[i] = Color.FromName(settings.WothColors[i]);
            }
            ColorIndex = settings.DefaultWothColorIndex;
        }

        private void label_woth_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ColorIndex++;
            }
            else if (e.Button == MouseButtons.Right)
            {
                ColorIndex--;
            }

            if (ColorIndex < 0) ColorIndex = 0;
            else if (ColorIndex >= Colors.Length) ColorIndex = Colors.Length - 1;

            LabelPlace.ForeColor = Colors[ColorIndex];
        }
    }
}
