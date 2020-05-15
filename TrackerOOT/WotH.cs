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
        int LabelPlaceNbClick = 0;

        public WotH(string selectedPlace, List<string> listImage, int nbWoth, int size_gossip)
        {
            LabelPlace = new Label
            {
                Name = Guid.NewGuid().ToString(),
                Text = selectedPlace,
                ForeColor = Color.White,
                BackColor = Color.CadetBlue,
                Font = new Font("Calibri", 11, FontStyle.Bold),
                Width = 222,
                Height = 24,
                TextAlign = ContentAlignment.MiddleLeft
            };
            LabelPlace.Location = new Point(2, (nbWoth * LabelPlace.Height));
            LabelPlace.MouseDown += new MouseEventHandler(label_woth_MouseDown);

            if (listImage.Count > 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    GossipStone newGossipStone = new GossipStone(listImage, new Point(0, 0), 24);
                    newGossipStone.Location = 
                        new Point(LabelPlace.Width + 5 + ((newGossipStone.Width+2) * i ), LabelPlace.Location.Y);
                    listGossipStone.Add(newGossipStone);
                }
            }
        }

        private void label_woth_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                switch (LabelPlaceNbClick)
                {
                    case 0:
                        LabelPlace.ForeColor = Color.Orange;
                        LabelPlaceNbClick++;
                        break;
                    case 1:
                        LabelPlace.ForeColor = Color.Crimson;
                        LabelPlaceNbClick++;
                        break;
                    case 2:
                        LabelPlace.ForeColor = Color.White;
                        LabelPlaceNbClick = 0;
                        break;
                }
            }            
        }
    }
}
