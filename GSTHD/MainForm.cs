using GSTHD.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GSTHD
{
    public partial class MainForm : Form
    {
        Dictionary<string, string> ListPlacesWithTag = new Dictionary<string, string>();
        SortedSet<string> ListPlaces = new SortedSet<string>();
        SortedSet<string> ListSometimesHintsSuggestions = new SortedSet<string>();

        MainForm_MenuBar MenuBar;
        LocalSettings LocalSettings;
        Layout ActiveLayout;
        Settings ActiveSettings;

        //PictureBox pbox_collectedSkulls;

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

        private void Initialize(object sender, EventArgs e)
        {
            //this.AcceptButton = null;
            //this.MaximizeBox = false;
            //this.KeyPreview = true;
            //this.KeyDown += changeCollectedSkulls;

            LoadSettings();
            LoadMenuBar();
            LoadActiveLayout();
        }

        private void LoadSettings()
        {
            LocalSettings = LoadLocalSettings();
            ActiveSettings = new Settings(LocalSettings);
        }

        private void LoadMenuBar()
        {
            MenuBar = new MainForm_MenuBar(this, LocalSettings);
            MenuBar.Dock = DockStyle.Top;
            MenuBar.SetRenderer();
        }

        private void LoadActiveLayout()
        {
            Layout layout;
            try
            {
                layout = PreloadLayout(LocalSettings.ActiveLayout);
            }
            catch (GSTHDException ex) when (
                ex is FilesNotFoundException ||
                ex is InvalidLayoutFileException)
            {
                // do nothing, application will open in an empty no-layout state
                ShowErrorMessage(ex);
                return;
            }

            ActiveLayout = layout;
            PostloadLayout();
        }

        public void LoadLayout(string layoutPath)
        {
            Layout layout;
            try
            {
                layout = PreloadLayout(layoutPath);
            }
            catch (GSTHDException ex) when (
                ex is FilesNotFoundException ||
                ex is InvalidLayoutFileException)
            {
                // do nothing, stay on previous layout state
                ShowErrorMessage(ex);
                return;
            }

            Controls.Clear();
            ActiveLayout = layout;
            LocalSettings.ActiveLayout = layoutPath;
            JsonIO.Write(LocalSettings, LocalSettings.LocalSettingsFileName);
            PostloadLayout();
        }

        public void ReloadActiveLayout()
        {
            Layout layout;
            try
            {
                layout = PreloadLayout(LocalSettings.ActiveLayout);
            }
            catch (GSTHDException ex)  when (
                ex is FilesNotFoundException ||
                ex is InvalidLayoutFileException)
            {
                // do nothing, stay on previous layout state
                ShowErrorMessage(ex);
                return;
            }

            Controls.Clear();
            ActiveLayout = layout;
            PostloadLayout();
        }

        //public SaveState()
        //{
        //    // TODO
        //}

        //public void LoadSavedState()
        //{
        //    // TODO
        //}

        private LocalSettings LoadLocalSettings()
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

            return JsonIO.Read<LocalSettings>(LocalSettings.LocalSettingsFileName);
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
            else throw new FilesNotFoundException(layoutPath, fixedPath);
        }

        private Layout PreloadLayout(string layoutPath)
        {
            var path = FixLayoutPath(layoutPath);

            var layout = new Layout(path);
            layout.LoadContents(ActiveSettings, ListSometimesHintsSuggestions, ListPlacesWithTag, this);
            return layout;
        }

        private void SetSize()
        {
            Size = new Size(ActiveLayout.Size.Width, ActiveLayout.Size.Height + MenuBar.Size.Height);
        }

        private void SetBackColor()
        {
            if (ActiveLayout.Settings.BackgroundColor.HasValue)
                BackColor = ActiveLayout.Settings.BackgroundColor.Value;
            ActiveLayout.BackColor = BackColor;
        }

        private void PostloadLayout()
        {
            ActiveLayout.Dock = DockStyle.Top;
            SetSize();
            SetBackColor();

            MenuBar.SetActiveLayout(ActiveLayout);
            ActiveSettings.SetLayoutSettings(ActiveLayout.Settings);

            Controls.Add(ActiveLayout);
            Controls.Add(MenuBar);
        }

        public void UpdateSettings()
        {
            ActiveSettings.Update();
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
            ReloadActiveLayout();
            Process.GetCurrentProcess().Refresh();
        }
        
        public void ShowErrorMessage(GSTHDException ex)
        {
            MessageBox.Show(ex.Message, $"{Config.ErrorMessageTitlePrefix}: {ex.Title}");
        }
    }

    public class FilesNotFoundException : GSTHDException
    {
        private static string GetMessage(string[] filePaths)
        {
            if (filePaths.Length == 0)
                return GenericMessage;

            var sb = new StringBuilder();
            foreach (var filePath in filePaths)
            {
                sb.Append($"File \"{filePath}\" not found. ");
            }

            return sb.ToString();
        }

        private static string GenericMessage = $"File(s) not found.";

        public FilesNotFoundException(params string[] fileNames)
            : base(Config.LayoutFileExceptionTitle, GetMessage(fileNames)) { }
        public FilesNotFoundException(string[] fileNames, Exception inner)
            : base(Config.LayoutFileExceptionTitle, GetMessage(fileNames), inner) { }
    }
}
