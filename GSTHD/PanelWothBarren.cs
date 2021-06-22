using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSTHD
{
    class PanelWothBarren : Panel
    {
        public List<WotH> ListWotH = new List<WotH>();
        public List<Barren> ListBarren = new List<Barren>();

        public TextBoxCustom textBoxCustom;
        private string[] ListImage_WothItemsOption;
        Size GossipStoneSize;
        int NbMaxRows;
        System.Windows.Forms.Label LabelSettings = new System.Windows.Forms.Label();

        public PanelWothBarren(ObjectPanelWotH data)
        {
            GossipStoneSize = data.GossipStoneSize;
            this.BackColor = data.BackColor;
            this.Location = new Point(data.X, data.Y);
            this.Name = data.Name;
            this.Size = new Size(data.Width, data.Height);
            this.TabStop = false;
            if(data.IsScrollable)
                this.MouseWheel += Panel_MouseWheel;
        }

        public PanelWothBarren(ObjectPanelBarren data)
        {
            this.BackColor = data.BackColor;
            this.Location = new Point(data.X, data.Y);
            this.Name = data.Name;
            this.Size = new Size(data.Width, data.Height);
            this.TabStop = false;
            if (data.IsScrollable)
                this.MouseWheel += Panel_MouseWheel;
        }

        private void Panel_MouseWheel(object sender, MouseEventArgs e)
        {
            var panel = (Panel)sender;
            if (e.Delta < 0)
            {
                foreach (var element in panel.Controls)
                {
                    if (element is System.Windows.Forms.Label)
                        ((System.Windows.Forms.Label)element).Location = new Point(((System.Windows.Forms.Label)element).Location.X, ((System.Windows.Forms.Label)element).Location.Y - 15);
                    if (element is GossipStone)
                        ((GossipStone)element).Location = new Point(((GossipStone)element).Location.X, ((GossipStone)element).Location.Y - 15);
                    if (element is TextBox)
                        ((TextBox)element).Location = new Point(((TextBox)element).Location.X, ((TextBox)element).Location.Y - 15);
                }
            }
            if (e.Delta > 0)
            {
                foreach (var element in panel.Controls)
                {
                    if (element is System.Windows.Forms.Label)
                        ((System.Windows.Forms.Label)element).Location = new Point(((System.Windows.Forms.Label)element).Location.X, ((System.Windows.Forms.Label)element).Location.Y + 15);
                    if (element is GossipStone)
                        ((GossipStone)element).Location = new Point(((GossipStone)element).Location.X, ((GossipStone)element).Location.Y + 15);
                    if (element is TextBox)
                        ((TextBox)element).Location = new Point(((TextBox)element).Location.X, ((TextBox)element).Location.Y + 15);
                }
            }
            ((PanelWothBarren)panel).SetSuggestionContainer();
        }

        public void PanelWoth(Dictionary<string, string> PlacesWithTag, ObjectPanelWotH data)
        {
            ListImage_WothItemsOption = data.GossipStoneImageCollection;
            NbMaxRows = data.NbMaxRows;

            LabelSettings = new System.Windows.Forms.Label
            {
                ForeColor = data.LabelColor,
                BackColor = data.LabelBackColor,
                Font = new Font(data.LabelFontName, data.LabelFontSize, data.LabelFontStyle),
                Width = data.LabelWidth,
                Height = data.LabelHeight
            };

            textBoxCustom = new TextBoxCustom
                (
                    PlacesWithTag,
                    new Point(0, 0),
                    data.TextBoxBackColor,
                    new Font(data.TextBoxFontName, data.TextBoxFontSize, data.TextBoxFontStyle),
                    data.TextBoxName,
                    new Size(data.TextBoxWidth, data.TextBoxHeight),
                    data.TextBoxText
                );
            textBoxCustom.TextBoxField.KeyDown += textBoxCustom_KeyDown_WotH;
            textBoxCustom.TextBoxField.MouseClick += textBoxCustom_MouseClick;
            this.Controls.Add(textBoxCustom.TextBoxField);
        }

        public void PanelBarren(Dictionary<string, string> PlacesWithTag, ObjectPanelBarren data)
        {
            NbMaxRows = data.NbMaxRows;

            LabelSettings = new System.Windows.Forms.Label
            {
                ForeColor = data.LabelColor,
                BackColor = data.LabelBackColor,
                Font = new Font(data.LabelFontName, data.LabelFontSize, data.LabelFontStyle),
                Width = data.LabelWidth,
                Height = data.LabelHeight
            };

            textBoxCustom = new TextBoxCustom
                (
                    PlacesWithTag,
                    new Point(0, 0),
                    data.TextBoxBackColor,
                    new Font(data.TextBoxFontName, data.TextBoxFontSize, data.TextBoxFontStyle),
                    data.TextBoxName,
                    new Size(data.TextBoxWidth, data.TextBoxHeight),
                    data.TextBoxText
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
                if (ListBarren.Count < NbMaxRows)
                {
                    var selectedPlace = textbox.Text.ToUpper().Trim();
                    var find = ListBarren.Where(x => x.Name == selectedPlace);
                    if (find.Count() <= 0)
                    {
                        Barren newBarren = null;
                        if(ListBarren.Count <= 0)
                            newBarren = new Barren(selectedPlace, new Point(2, -LabelSettings.Height), LabelSettings);
                        else
                        {
                            var lastLocation = ListBarren.Last().LabelPlace.Location;
                            newBarren = new Barren(selectedPlace, lastLocation, LabelSettings);
                        }
                        ListBarren.Add(newBarren);
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
                if (ListWotH.Count < NbMaxRows)
                {
                    if (textbox.Text != string.Empty)
                    {
                        var selectedPlace = textbox.Text.ToUpper().Trim();
                        var find = ListWotH.Where(x => x.Name == selectedPlace);
                        if(find.Count() <= 0)
                        {
                            WotH newWotH = null;
                            if (ListWotH.Count <= 0)
                                newWotH = new WotH(selectedPlace, ListImage_WothItemsOption, new Point(2, -LabelSettings.Height), LabelSettings, GossipStoneSize);
                            else
                            {
                                var lastLocation = ListWotH.Last().LabelPlace.Location;
                                newWotH = new WotH(selectedPlace, ListImage_WothItemsOption, lastLocation, LabelSettings, GossipStoneSize);
                               
                            }
                            ListWotH.Add(newWotH);
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
            if (e.Button == MouseButtons.Middle)
            {
                var label = (System.Windows.Forms.Label)sender;
                var barren = this.ListBarren.Where(x => x.LabelPlace.Name == label.Name).ToList()[0];
                this.RemoveBarren(barren);
            }
        }

        private void LabelPlace_MouseClick_WotH(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                var label = (System.Windows.Forms.Label)sender;
                var woth = this.ListWotH.Where(x => x.LabelPlace.Name == label.Name).ToList()[0];
                this.RemoveWotH(woth);
            }
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
