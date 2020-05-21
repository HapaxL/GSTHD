using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TrackerOOT
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> ListPlacesWithTag = new Dictionary<string, string>();
        SortedSet<String> ListPlaces = new SortedSet<String>();
        SortedSet<String> ListSometimesHintsSuggestions = new SortedSet<string>();

        PictureBox pbox_collectedSkulls;

        bool SongMode = false;
        bool AutoCheck = false;
        string ActiveLayoutName = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.R)
            {
                this.Controls.Clear();
                this.Form1_Load(sender, new EventArgs());
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Gossip Stones Tracker v1.8.1 (Standard Edition)";
            this.AcceptButton = null;
            this.MaximizeBox = false;

            ListPlaces.Clear();
            ListPlaces.Add("");
            ListPlacesWithTag.Clear();
            JObject json_places = JObject.Parse(File.ReadAllText(@"oot_places.json"));
            foreach (var property in json_places)
            {
                ListPlaces.Add(property.Key.ToString());
                ListPlacesWithTag.Add(property.Key, property.Value.ToString());
            }

            ListSometimesHintsSuggestions.Clear();
            JObject json_hints = JObject.Parse(File.ReadAllText(@"sometimes_hints.json"));
            foreach (var categorie in json_hints)
            {
                foreach (var hint in categorie.Value)
                {
                    ListSometimesHintsSuggestions.Add(hint.ToString());
                }
            }

            JObject json_properties = JObject.Parse(File.ReadAllText(@"settings.json"));
            foreach (var property in json_properties)
            {
                if (property.Key == "MoveLocationToSong")
                {
                    SongMode = Convert.ToBoolean(property.Value);
                }
                if(property.Key == "AutoCheckSongs")
                {
                    AutoCheck = Convert.ToBoolean(property.Value);
                }
                if(property.Key == "ActiveLayout")
                {
                    ActiveLayoutName = property.Value.ToString();
                }
            }

            if(ActiveLayoutName != string.Empty)
            {
                List<ObjectPoint> ListItems = new List<ObjectPoint>();
                List<ObjectPoint> ListSongs = new List<ObjectPoint>();
                List<ObjectPoint> ListDoubleItems = new List<ObjectPoint>();
                List<ObjectPointCollectedItem> ListCollectedItems = new List<ObjectPointCollectedItem>();
                List<ObjectPointMedallion> ListMedallions = new List<ObjectPointMedallion>();
                List<ObjectPoint> ListGuaranteedHints = new List<ObjectPoint>();
                List<ObjectPoint> ListGossipStones = new List<ObjectPoint>();
                List<ObjectPointLabel> ListSometimesHints = new List<ObjectPointLabel>();
                List<ObjectPointLabel> ListChronometers = new List<ObjectPointLabel>();
                List<ObjectPanelWotH> ListPanelWotH = new List<ObjectPanelWotH>();
                List<ObjectPanelBarren> ListPanelBarren = new List<ObjectPanelBarren>();
                List<ObjectPointGoMode> ListGoMode = new List<ObjectPointGoMode>();

                AppSettings App_Settings = new AppSettings();

                JObject json_layouts = JObject.Parse(File.ReadAllText(@"Layouts/" + ActiveLayoutName + ".json"));
                foreach(var category in json_layouts)
                {
                    if(category.Key.ToString() == "AppSize")
                    {
                        App_Settings = JsonConvert.DeserializeObject<AppSettings>(category.Value.ToString());   
                    }

                    if (category.Key.ToString() == "Items")
                    {
                        foreach(var element in category.Value)
                        {
                            ListItems.Add(JsonConvert.DeserializeObject<ObjectPoint>(element.ToString()));
                        }
                        
                    }
                    if (category.Key.ToString() == "Songs")
                    {
                        foreach (var element in category.Value)
                        {
                            ListSongs.Add(JsonConvert.DeserializeObject<ObjectPoint>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "DoubleItems")
                    {
                        foreach (var element in category.Value)
                        {
                            ListDoubleItems.Add(JsonConvert.DeserializeObject<ObjectPoint>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "CollectedItems")
                    {
                        foreach (var element in category.Value)
                        {
                            ListCollectedItems.Add(JsonConvert.DeserializeObject<ObjectPointCollectedItem>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "Medallions")
                    {
                        foreach (var element in category.Value)
                        {
                            ListMedallions.Add(JsonConvert.DeserializeObject<ObjectPointMedallion>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "GuaranteedHints")
                    {
                        foreach (var element in category.Value)
                        {
                            ListGuaranteedHints.Add(JsonConvert.DeserializeObject<ObjectPoint>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "GossipStones")
                    {
                        foreach (var element in category.Value)
                        {
                            ListGossipStones.Add(JsonConvert.DeserializeObject<ObjectPoint>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "SometimesHints")
                    {
                        foreach (var element in category.Value)
                        {
                            ListSometimesHints.Add(JsonConvert.DeserializeObject<ObjectPointLabel>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "Chronometers")
                    {
                        foreach (var element in category.Value)
                        {
                            ListChronometers.Add(JsonConvert.DeserializeObject<ObjectPointLabel>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "PanelWoth")
                    {
                        foreach (var element in category.Value)
                        {
                            ListPanelWotH.Add(JsonConvert.DeserializeObject<ObjectPanelWotH>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "PanelBarren")
                    {
                        foreach (var element in category.Value)
                        {
                            ListPanelBarren.Add(JsonConvert.DeserializeObject<ObjectPanelBarren>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "GoMode")
                    {
                        foreach (var element in category.Value)
                        {
                            ListGoMode.Add(JsonConvert.DeserializeObject<ObjectPointGoMode>(element.ToString()));
                        }
                    }
                }

                this.Size = new Size(App_Settings.Width, App_Settings.Height);
                this.BackColor = App_Settings.BackgroundColor;

                if (ListItems.Count > 0)
                {
                    foreach(var item in ListItems)
                    {
                        if(item.Visible)
                            this.Controls.Add(new Item(item));
                    }
                }

                if (ListSongs.Count > 0)
                {
                    foreach (var song in ListSongs)
                    {
                        if (song.Visible)
                            this.Controls.Add(new Song(song, SongMode, AutoCheck));
                    }
                }

                if (ListDoubleItems.Count > 0)
                {
                    foreach (var doubleItem in ListDoubleItems)
                    {
                        if (doubleItem.Visible)
                            this.Controls.Add(new DoubleItem(doubleItem));
                    }
                }

                if (ListCollectedItems.Count > 0)
                {
                    foreach (var item in ListCollectedItems)
                    {
                        if (item.Visible)
                            this.Controls.Add(new CollectedItem(item));
                    }
                }

                if (ListMedallions.Count > 0)
                {
                    foreach (var medallion in ListMedallions)
                    {
                        if (medallion.Visible)
                        {
                            var element = new Medallion(medallion);
                            this.Controls.Add(element);
                            this.Controls.Add(element.SelectedDungeon);
                            element.SetSelectedDungeonLocation();
                            element.SelectedDungeon.BringToFront();
                        }
                    }
                }

                if (ListGuaranteedHints.Count > 0)
                {
                    foreach (var item in ListGuaranteedHints)
                    {
                        if (item.Visible)
                            this.Controls.Add(new GuaranteedHint(item));
                    }
                }

                if (ListGossipStones.Count > 0)
                {
                    foreach (var item in ListGossipStones)
                    {
                        if (item.Visible)
                            this.Controls.Add(new GossipStone(item));
                    }
                }

                if (ListSometimesHints.Count > 0)
                {
                    foreach (var item in ListSometimesHints)
                    {
                        if (item.Visible)
                            this.Controls.Add(new SometimesHint(ListSometimesHintsSuggestions, item));
                    }
                }

                if (ListChronometers.Count > 0)
                {
                    foreach (var item in ListChronometers)
                    {
                        if (item.Visible)
                            this.Controls.Add(new Chronometer(item).ChronoLabel);
                    }
                }

                if (ListPanelWotH.Count > 0)
                {
                    foreach (var item in ListPanelWotH)
                    {
                        if (item.Visible)
                        {
                            var newPanel = new PanelWothBarren(item);
                            newPanel.PanelWoth(ListPlacesWithTag, item);
                            this.Controls.Add(newPanel);
                            this.Controls.Add(newPanel.textBoxCustom.SuggestionContainer);
                            newPanel.SetSuggestionContainer();
                        }
                    }
                }

                if (ListPanelBarren.Count > 0)
                {
                    foreach (var item in ListPanelBarren)
                    {
                        if (item.Visible)
                        {
                            var newPanel = new PanelWothBarren(item);
                            newPanel.PanelBarren(ListPlacesWithTag, item);
                            this.Controls.Add(newPanel);
                            this.Controls.Add(newPanel.textBoxCustom.SuggestionContainer);
                            newPanel.SetSuggestionContainer();
                        }
                    }
                }

                if(ListGoMode.Count > 0)
                {
                    foreach(var item in ListGoMode)
                    {
                        if (item.Visible)
                        {
                            var element = new GoMode(item);
                            this.Controls.Add(element);
                            element.SetLocation();
                        }
                    }
                }
            }
            
            this.KeyPreview = true;
            //this.KeyDown += changeCollectedSkulls;
        }

        private void changeCollectedSkulls(object sender, KeyEventArgs k)
        {
            if (k.KeyCode == Keys.F9) { }
            //button_chrono_Click(sender, new EventArgs());
            if (k.KeyCode == Keys.F11) { }
            //label_collectedSkulls_MouseDown(pbox_collectedSkulls.Controls[0], new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            if (k.KeyCode == Keys.F12) { }
            //label_collectedSkulls_MouseDown(pbox_collectedSkulls.Controls[0], new MouseEventArgs(MouseButtons.Right, 1, 0, 0, 0));
        }
    }
}
