using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerOOT
{
    class WotH
    {
        public Label LabelPlace;
        public List<GossipStone> listGossipStone = new List<GossipStone>();
        public string Name;
        int LabelPlaceNbClick = 0;

        public WotH(string selectedPlace, string[] listImage, Point lastLabelLocation, Label labelSettings, Size gossipStoneSize)
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
        }

        private void label_woth_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LabelPlaceNbClick++;
            }
            else if (e.Button == MouseButtons.Right)
            {
                LabelPlaceNbClick--;
            }

            if (LabelPlaceNbClick == -1) LabelPlace.ForeColor = Color.BlueViolet;
            else if (LabelPlaceNbClick == 0) LabelPlace.ForeColor = Color.White;
            else if (LabelPlaceNbClick == 1) LabelPlace.ForeColor = Color.Yellow;
            else if (LabelPlaceNbClick == 2) LabelPlace.ForeColor = Color.DarkOrange;
            else if (LabelPlaceNbClick == 3) LabelPlace.ForeColor = Color.Red;
            else if (LabelPlaceNbClick == 4) LabelPlace.ForeColor = Color.Black;
            else if (LabelPlaceNbClick < -1) LabelPlaceNbClick = -1;
            else LabelPlaceNbClick = 4;

        }
    }
}
