using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerOOT
{
    class CollectedItem : PictureBox
    {
        List<string> ListImageName = new List<string>();
        Size CollectedItemSize;

        public CollectedItem(ObjectPointCollectedItem data)
        {
            if (data.ImageCollection != null)
                ListImageName = data.ImageCollection.ToList();

            CollectedItemSize = data.Size;

            if(ListImageName.Count > 0)
            {
                this.Image = Image.FromFile(@"Resources/" + ListImageName[0]);
                this.Name = ListImageName[0];
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Size = CollectedItemSize;
            }
            this.Location = new Point(data.X, data.Y);
            this.BackColor = Color.Transparent;
            this.TabStop = false;

            Label nb_items = new Label
            {
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None,
                Text = "00",
                Font = new Font(data.LabelFontName, data.LabelFontSize, data.LabelFontStyle),
                ForeColor = data.LabelColor,
                AutoSize = false,
                TextAlign = ContentAlignment.TopLeft,
                Height = CollectedItemSize.Height
            };
            nb_items.Location = new Point(0, 0);
            nb_items.MouseDown += new MouseEventHandler(label_collectedSkulls_MouseDown);

            this.Controls.Add(nb_items);
        }

        private void label_collectedSkulls_MouseDown(object sender, MouseEventArgs e)
        {
            var label_collectedSkulls = (Label)sender;
            var intLabelText = Convert.ToInt32(label_collectedSkulls.Text);
            if (e.Button == MouseButtons.Left)
                intLabelText++;
            if (e.Button == MouseButtons.Right && intLabelText > 0)
                intLabelText--;

            if (intLabelText < 10)
                label_collectedSkulls.Text = "0" + intLabelText.ToString();
            else
                label_collectedSkulls.Text = intLabelText.ToString();
        }
    }
}
