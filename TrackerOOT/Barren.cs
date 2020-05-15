using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerOOT
{
    class Barren
    {
        public Label LabelPlace;

        public Barren(string selectedPlace, int nbBarren)
        {
            LabelPlace = new Label
            {
                Name = Guid.NewGuid().ToString(),
                Text = selectedPlace,
                ForeColor = Color.White,
                BackColor = Color.IndianRed,
                Font = new Font("Calibri", 11, FontStyle.Bold),
                Width = 222,
                TextAlign = ContentAlignment.MiddleLeft
            };
            LabelPlace.Location = new Point(2, (nbBarren * LabelPlace.Height));
        }
    }
}
