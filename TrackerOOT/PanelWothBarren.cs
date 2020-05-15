using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerOOT
{
    class PanelWothBarren : Panel
    {
        public List<WotH> ListWotH = new List<WotH>();
        public List<Barren> ListBarren = new List<Barren>();
        int size_gossip;

        public TextBoxCustom textBoxCustom;
        private List<string> ListImage_WothItemsOption;
        public PanelWothBarren(Dictionary<string, string> PlacesWithTag, List<string> listImage_WothItemsOption, int size_gossip)
        {
            ListImage_WothItemsOption = listImage_WothItemsOption;

            textBoxCustom = new TextBoxCustom
                (
                    PlacesWithTag,
                    new Point(0, 0),
                    Color.FromArgb(74, 138, 182),
                    new Font("Calibri", 11, FontStyle.Bold, GraphicsUnit.Point, 0),
                    "TBC_WotH",
                    new Size(222, 24),
                    ":: Way of the Hero ::"
                );
            textBoxCustom.TextBoxField.KeyDown += textBoxCustom_KeyDown_WotH;
            textBoxCustom.TextBoxField.MouseClick += textBoxCustom_MouseClick;
            this.Controls.Add(textBoxCustom.TextBoxField);

        }

        public PanelWothBarren(Dictionary<string, string> PlacesWithTag)
        {
            ListImage_WothItemsOption = new List<string>();

            textBoxCustom = new TextBoxCustom
                (
                    PlacesWithTag,
                    new Point(0, 2),
                    Color.FromArgb(198, 68, 92),
                    new Font("Calibri", 11, FontStyle.Bold),
                    "barren",
                    new Size(222, 23),
                    ":: Barren places ::"
                );
            textBoxCustom.TextBoxField.KeyDown += textBoxCustom_KeyDown_Barren;
            textBoxCustom.TextBoxField.MouseClick += textBoxCustom_MouseClick;
            this.Controls.Add(textBoxCustom.TextBoxField);

        }

        public void SetSuggestionContainer()
        {
            textBoxCustom.SetSuggestionsContainerLocation(this.Location);
            textBoxCustom.SuggestionContainer.BringToFront();
        }


        private void textBoxCustom_MouseClick(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).Text = string.Empty;
        }

        private void textBoxCustom_KeyDown_Barren(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                var textbox = (TextBox)sender;
                if (ListBarren.Count < 3)
                {
                    var selectedPlace = textbox.Text.ToUpper().Trim();
                    var newBarren = new Barren(selectedPlace, ListBarren.Count);
                    if(this.AddPlaces(newBarren))
                    {
                        this.Controls.Add(newBarren.LabelPlace);
                        newBarren.LabelPlace.MouseClick += LabelPlace_MouseClick_Barren;
                        textBoxCustom.newLocation(new Point(2, newBarren.LabelPlace.Location.Y + newBarren.LabelPlace.Height), this.Location);
                    }
                }
                textbox.Text = string.Empty;
            }
        }

        private void textBoxCustom_KeyDown_WotH(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                var textbox = (TextBox)sender;
                if (ListWotH.Count < 5)
                {
                    if (textbox.Text != string.Empty)
                    {
                        var selectedPlace = textbox.Text.ToUpper().Trim();
                        var newWotH = new WotH(selectedPlace, ListImage_WothItemsOption, ListWotH.Count, size_gossip);

                        if (this.AddPlaces(newWotH))
                        {
                            this.Controls.Add(newWotH.LabelPlace);
                            newWotH.LabelPlace.MouseClick += LabelPlace_MouseClick_WotH;
                            foreach (var gossipStone in newWotH.listGossipStone)
                            {
                                this.Controls.Add(gossipStone);
                            }
                            //Move TextBoxCustom
                            textBoxCustom.newLocation(new Point(2, newWotH.LabelPlace.Location.Y + newWotH.LabelPlace.Height), this.Location);
                        }
                    }
                }
                textbox.Text = string.Empty;
            }
        }

        private void LabelPlace_MouseClick_Barren(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var label = (Label)sender;
                var barren = this.ListBarren.Where(x => x.LabelPlace.Name == label.Name).ToList()[0];
                this.RemoveBarren(barren);
            }
        }

        private void LabelPlace_MouseClick_WotH(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var label = (Label)sender;
                var woth = this.ListWotH.Where(x => x.LabelPlace.Name == label.Name).ToList()[0];
                this.RemoveWotH(woth);
            }
        }

        public bool AddPlaces(WotH woth)
        {
            var isAdded = false;
            if (!ListWotH.Contains(woth))
            {
                ListWotH.Add(woth);
                isAdded = true;
            }
            return isAdded;
        }

        public bool AddPlaces(Barren barren)
        {
            var isAdded = false;
            if (!ListBarren.Contains(barren))
            {
                ListBarren.Add(barren);
                isAdded = true;
            }
            return isAdded;
        }

        public void RemoveWotH(WotH woth)
        {
            ListWotH.Remove(woth);

            this.Controls.Remove(woth.LabelPlace);
            foreach (var gossipStone in woth.listGossipStone)
            {
                this.Controls.Remove(gossipStone);
            }

            for (int i = 0; i < ListWotH.Count; i++)
            {
                var wothLabel = ListWotH[i].LabelPlace;
                wothLabel.Location = new Point(2, (i * wothLabel.Height));

                for (int j = 0; j < ListWotH[i].listGossipStone.Count; j++)
                {
                    ListWotH[i].listGossipStone[j].Location = 
                        new Point(wothLabel.Width + 5 + (j * (ListWotH[i].listGossipStone[j].Width+2)), wothLabel.Location.Y);
                }
            }
            textBoxCustom.newLocation(new Point(2, ListWotH.Count * woth.LabelPlace.Height), this.Location);
        }

        public void RemoveBarren(Barren barren)
        {
            ListBarren.Remove(barren);

            this.Controls.Remove(barren.LabelPlace);

            for (int i = 0; i < ListBarren.Count; i++)
            {
                var wothLabel = ListBarren[i].LabelPlace;
                wothLabel.Location = new Point(2, (i * wothLabel.Height));
            }
            textBoxCustom.newLocation(new Point(2, ListBarren.Count * barren.LabelPlace.Height), this.Location);
        }
    }
}
