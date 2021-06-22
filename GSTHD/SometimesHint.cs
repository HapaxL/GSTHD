using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GSTHD
{
    class SometimesHint : TextBox
    {
        public SometimesHint(SortedSet<string> listSometimesHints, AutoFillTextBox textInput)
        {
            this.BackColor = textInput.BackColor;
            this.Font = new Font(textInput.FontName, textInput.FontSize, textInput.FontStyle);
            this.ForeColor = textInput.FontColor;
            this.Size = new Size(textInput.Width, textInput.Height);
            this.Name = textInput.Name;

            this.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            this.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.AutoCompleteSource = AutoCompleteSource.CustomSource;

            this.KeyDown += textBox_KeyDown;
            this.AutoCompleteCustomSource.AddRange(listSometimesHints.ToArray());
            this.Location = new Point(textInput.X, textInput.Y);
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var array = this.Parent.Controls.Find(this.Name + "_GossipStone", false);
                if(array.Length > 0)
                    ((GossipStone)array[0]).Click_MouseUp(sender, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
        }
    }
}
