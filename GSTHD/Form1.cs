using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace GSTHD
{
    public partial class Form1 : Form
    {
        Dictionary<string, string> ListPlacesWithTag = new Dictionary<string, string>();
        SortedSet<string> ListPlaces = new SortedSet<string>();
        SortedSet<string> ListSometimesHintsSuggestions = new SortedSet<string>();

        Form1_MenuBar MenuBar;
        Layout CurrentLayout;
        Panel LayoutContent;

        PictureBox pbox_collectedSkulls;

        Settings Settings;
        
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Control && e.KeyCode == Keys.R)
            //{
            //    this.Controls.Clear();
            //    e.Handled = true;
            //    e.SuppressKeyPress = true;
            //    this.Form1_Load(sender, new EventArgs());
            //}

            /*
            if(e.KeyCode == Keys.F2)
            {
                var window = new Editor(CurrentLayout);
                window.Show();
            }
            */
        }

        private void LoadAll(object sender, EventArgs e)
        {
            var assembly = Assembly.GetEntryAssembly().GetName();
            this.Text = $"{Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title} v{assembly.Version.Major}.{assembly.Version.Minor}";
            this.AcceptButton = null;
            this.MaximizeBox = false;

            LoadSettings();
            
            MenuBar = new Form1_MenuBar(this, Settings);

            LoadLayout();
            SetMenuBar();

            this.KeyPreview = true;
            //this.KeyDown += changeCollectedSkulls;
        }

        private void Reload()
        {
            LoadSettings();
            LoadLayout();
            SetMenuBar();
        }

        private void LoadSettings()
        {
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

            Settings = Settings.Read();
        }

        private void SetMenuBar()
        {
            MenuBar.SetRenderer();
        }

        private void LoadLayout()
        {
            Controls.Clear();
            LayoutContent = new Panel();
            CurrentLayout = new Layout();
            CurrentLayout.LoadLayout(LayoutContent, Settings, ListSometimesHintsSuggestions, ListPlacesWithTag, this);
            Size = new Size(LayoutContent.Size.Width, LayoutContent.Size.Height + MenuBar.Size.Height);
            LayoutContent.Dock = DockStyle.Top;
            Controls.Add(LayoutContent);
            MenuBar.Dock = DockStyle.Top;
            Controls.Add(MenuBar);
        }

        public void UpdateLayoutFromSettings()
        {
            CurrentLayout.UpdateFromSettings();
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

        public void Reset(object sender)
        {
            ControlExtensions.ClearAndDispose(LayoutContent);
            Reload();
            Process.GetCurrentProcess().Refresh();
        }
    }
}
