using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrackerOOT
{
    class SometimesHint
    {
        public GossipStone SH_GossipStone;
        public TextBox SH_TextBox;

        public SometimesHint(List<string> images, int X, int Y, List<string> listSometimesHints, int gossip_size)
        {
            SH_GossipStone = new GossipStone(images, new Point(X, Y), gossip_size);

            SH_TextBox = new TextBox
            {
                BackColor = Color.FromArgb(64, 64, 64),
                Font = new Font("Calibri", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.White,
                Size = new Size(125, 23),
                AutoCompleteCustomSource = new AutoCompleteStringCollection(),
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.CustomSource
            };
            SH_TextBox.KeyDown += textBox_KeyDown;
            SH_TextBox.AutoCompleteCustomSource.AddRange(listSometimesHints.ToArray());
            SH_TextBox.Location = new Point(SH_GossipStone.Location.X - SH_TextBox.Width - 5, Y);

        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SH_GossipStone.Click_MouseUp(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        }
    }
}
