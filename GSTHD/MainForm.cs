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
    public partial class MainForm : Form
    {
        Dictionary<string, string> ListPlacesWithTag = new Dictionary<string, string>();
        SortedSet<string> ListPlaces = new SortedSet<string>();
        SortedSet<string> ListSometimesHintsSuggestions = new SortedSet<string>();

        MainForm_MenuBar MenuBar;
        Layout ActiveLayout;

        //PictureBox pbox_collectedSkulls;


        Settings Settings;
        
        public MainForm()
        {
            InitializeComponent();
        }


        //private void MainForm_KeyDown(object sender, KeyEventArgs e)
        //{
        //    //if (e.Control && e.KeyCode == Keys.R)
        //    //{
        //    //    this.Controls.Clear();
        //    //    e.Handled = true;
        //    //    e.SuppressKeyPress = true;
        //    //    this.Form1_Load(sender, new EventArgs());
        //    //}

        //    /*
        //    if(e.KeyCode == Keys.F2)
        //    {
        //        var window = new Editor(CurrentLayout);
        //        window.Show();
        //    }
        //    */
        //}

        private void LoadAll(object sender, EventArgs e)
        {
            var assembly = Assembly.GetEntryAssembly().GetName();
            this.Text = $"{Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title} v{assembly.Version.Major}.{assembly.Version.Minor}";
            this.AcceptButton = null;
            this.MaximizeBox = false;

            LoadSettings();
            
            MenuBar = new MainForm_MenuBar(this, Settings);

            LoadLayout(Settings.ActiveLayout);
            SetMenuBar();

            this.KeyPreview = true;
            //this.KeyDown += changeCollectedSkulls;
        }

        private void Reload()
        {
            LoadSettings();
            LoadLayout(Settings.ActiveLayout);
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

        private string FixLayoutPath(string layoutPath)
        {
            if (File.Exists(layoutPath))
            {
                return layoutPath;
            }

            var fixedPath = $@"Layouts\{layoutPath}.json";
            if (File.Exists(fixedPath))
            {
                return fixedPath;
            }
            else throw new FileNotFoundException($"Layout file \"{layoutPath}\" was not found. Layout file \"{fixedPath}\" was not found.");
        }

        public void LoadLayout(string layoutPath)
        {
            var path = FixLayoutPath(layoutPath);

            Controls.Clear();
            ActiveLayout = GSTHD.Layout.Load(path, Settings, ListSometimesHintsSuggestions, ListPlacesWithTag, this);
            Size = new Size(ActiveLayout.Size.Width, ActiveLayout.Size.Height + MenuBar.Size.Height);
            ActiveLayout.Dock = DockStyle.Top;
            Controls.Add(ActiveLayout);
            MenuBar.Dock = DockStyle.Top;
            Controls.Add(MenuBar);
            Settings.ActiveLayout = path;
            Settings.Write();
        }

        public void UpdateLayoutFromSettings()
        {
            ActiveLayout.UpdateFromSettings();
        }

        //private void changeCollectedSkulls(object sender, KeyEventArgs k)
        //{
        //    if (k.KeyCode == Keys.F9) { }
        //    //button_chrono_Click(sender, new EventArgs());
        //    if (k.KeyCode == Keys.F11) { }
        //    //label_collectedSkulls_MouseDown(pbox_collectedSkulls.Controls[0], new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
        //    if (k.KeyCode == Keys.F12) { }
        //    //label_collectedSkulls_MouseDown(pbox_collectedSkulls.Controls[0], new MouseEventArgs(MouseButtons.Right, 1, 0, 0, 0));
        //}

        public void Reset(object sender)
        {
            ControlExtensions.ClearAndDispose(ActiveLayout);
            Reload();
            Process.GetCurrentProcess().Refresh();
        }
    }
}
