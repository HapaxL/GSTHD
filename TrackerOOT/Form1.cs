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

        Layout CurrentLayout = new Layout();
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
                e.Handled = true;
                e.SuppressKeyPress = true;
                this.Form1_Load(sender, new EventArgs());
            }

            if(e.KeyCode == Keys.F2)
            {
                var window = new Editor(CurrentLayout);
                window.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Items&Hints Tracker v1.8.3";
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

            CurrentLayout.LoadLayout(this, ActiveLayoutName, SongMode, AutoCheck, ListSometimesHintsSuggestions, ListPlacesWithTag);
            
            
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
