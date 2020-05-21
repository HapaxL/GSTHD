using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TrackerOOT
{
    class GuaranteedHint : PictureBox
    {
        List<string> ListImageName = new List<string>();
        bool isMouseDown = false;

        Size GuaranteddHintSize;

        public GuaranteedHint(ObjectPoint data)
        {
            if(data.ImageCollection != null)
                ListImageName = data.ImageCollection.ToList();

            GuaranteddHintSize = data.Size;

            this.BackColor = Color.Transparent;
            if (ListImageName.Count > 0)
            {
                this.Name = ListImageName[0];
                this.Image = Image.FromFile(@"Resources/" + ListImageName[0]);
                this.SizeMode = PictureBoxSizeMode.StretchImage;
                this.Size = GuaranteddHintSize;
            }            
            this.Location = new Point(data.X, data.Y);
            this.TabStop = false;
            this.AllowDrop = false;
        }
    }
}
