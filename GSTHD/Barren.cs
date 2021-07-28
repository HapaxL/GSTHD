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
        public Label LabelPlace;
        public string Name;

        public Barren(string selectedPlace, Point lastLabelLocation, Label labelSettings)
        {
            this.Name = selectedPlace;

            LabelPlace = new Label
            {
                Name = Guid.NewGuid().ToString(),
                Text = selectedPlace,
                ForeColor = labelSettings.ForeColor,
                BackColor = labelSettings.BackColor,
                Font = labelSettings.Font,
                Width = labelSettings.Width,
                Height = labelSettings.Height,
                TextAlign = ContentAlignment.MiddleLeft
            };
            LabelPlace.Location = new Point(2, lastLabelLocation.Y + LabelPlace.Height);
        }

        public void UpdateFromSettings() { }
    }
}
