﻿using GSTHD.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSTHD
{
    public class MainForm_MenuBar : Panel
    {
        private struct MenuItems
        {
            // File
            public ToolStripMenuItem Reset;
            public ToolStripMenuItem LoadLayout;
            public ToolStripMenuItem ShowMenuBar;

            // Settings
                // Layout Settings
                    // BackgroundColor?
                    // Default Woth Gossip Stone Count?
                    // Default Path Goal Count?
                    // Default Woth Color Index?
            
                // Scroll Wheel
            public ToolStripMenuItem InvertScrollWheel;
            public ToolStripMenuItem Wraparound;

                // Drag & Drop
            public ToolStripMenuItem DragButton;
            public ToolStripMenuItem AutocheckDragButton;

                // Song Markers
            public ToolStripMenuItem MoveLocation;
                // public ToolStripMenuItem Autocheck;
            public ToolStripMenuItem Behaviour;

                // WotH
            public ToolStripMenuItem EnableLastWoth;
            public ToolStripMenuItem LastWothColor;
            public ToolStripMenuItem EnableDuplicateWoth;

                // Barren
            public ToolStripMenuItem EnableBarrenColors;
        }

        private readonly Dictionary<Settings.DragButtonOption, string> DragButtonNames = new Dictionary<Settings.DragButtonOption, string>
        {
            { Settings.DragButtonOption.None, "None" },
            { Settings.DragButtonOption.Left, "Left Click" },
            { Settings.DragButtonOption.Middle, "Middle Click" },
            { Settings.DragButtonOption.Right, "Right Click" },
            { Settings.DragButtonOption.LeftAndRight, "Left + Right Click" },
        };

        private readonly Dictionary<Settings.SongMarkerBehaviourOption, string> SongMarkerBehaviourNames = new Dictionary<Settings.SongMarkerBehaviourOption, string>
        {
            { Settings.SongMarkerBehaviourOption.None, "None" },
            { Settings.SongMarkerBehaviourOption.CheckOnly, "Click to Check" },
            { Settings.SongMarkerBehaviourOption.DropOnly, "Drop Items/Songs onto" },
            { Settings.SongMarkerBehaviourOption.DragAndDrop, "Full Drag && Drop" },
            { Settings.SongMarkerBehaviourOption.DropAndCheck, "Drop Items/Songs onto, Click to Check" },
            { Settings.SongMarkerBehaviourOption.Full, "Full Drag && Drop, Click to Check" },
        };

        MainForm Form;
        LocalSettings LocalSettings;
        Layout ActiveLayout;
        MenuStrip MenuStrip;
        MenuItems Items;
        Dictionary<Settings.DragButtonOption, ToolStripMenuItem> DragButtonOptions;
        Dictionary<Settings.DragButtonOption, ToolStripMenuItem> AutocheckDragButtonOptions;
        Dictionary<Settings.SongMarkerBehaviourOption, ToolStripMenuItem> SongMarkerBehaviourOptions;
        Dictionary<KnownColor, ToolStripMenuItem> LastWothColorOptions;
        Size SavedSize;
        OpenFileDialog OpenLayoutDialog;

        public MainForm_MenuBar(MainForm form, LocalSettings localSettings)
        {
            Form = form;
            LocalSettings = localSettings;

            MakeMenu();

            Size = MenuStrip.Size;
            SavedSize = MenuStrip.Size;

            OpenLayoutDialog = new OpenFileDialog()
            {
                Filter = "Layout files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Load Layout"
            };

            ReadSettings();

            Controls.Add(MenuStrip);
        }

        public void SetActiveLayout(Layout activeLayout)
        {
            ActiveLayout = activeLayout;
            UpdateLayoutSettings();
        }

        private void UpdateLayoutSettings()
        {
            // TODO
        }

        private void MakeMenu()
        {
            MenuStrip = new MenuStrip();
            Items = new MenuItems();

            var layoutMenu = new ToolStripMenuItem("File");
            {
                Items.Reset = new ToolStripMenuItem("Reset", null, new EventHandler(menuBar_Reset))
                {
                    ShortcutKeys = Keys.Control | Keys.R,
                    ShowShortcutKeys = true,
                };
                layoutMenu.DropDownItems.Add(Items.Reset);

                Items.LoadLayout = new ToolStripMenuItem("Load Layout", null, new EventHandler(menuBar_LoadLayout))
                {
                    ShortcutKeys = Keys.Control | Keys.L,
                    ShowShortcutKeys = true,
                };
                layoutMenu.DropDownItems.Add(Items.LoadLayout);

                Items.ShowMenuBar = new ToolStripMenuItem("Show Menu Bar", null, new EventHandler(menuBar_Enable))
                {
                    ShortcutKeys = Keys.F10,
                    ShowShortcutKeys = true,
                    CheckOnClick = true,
                };
                layoutMenu.DropDownItems.Add(Items.ShowMenuBar);
                
            }
            MenuStrip.Items.Add(layoutMenu);

            var optionMenu = new ToolStripMenuItem("Settings");
            {


                var scrollWheelSubMenu = new ToolStripMenuItem("Scroll Wheel");
                {
                    Items.InvertScrollWheel = new ToolStripMenuItem("Invert Scroll Wheel", null, new EventHandler(menuBar_ToggleInvertScrollWheel))
                    {
                        CheckOnClick = true,
                    };
                    scrollWheelSubMenu.DropDownItems.Add(Items.InvertScrollWheel);

                    Items.Wraparound = new ToolStripMenuItem("Wraparound Dungeon Names", null, new EventHandler(menuBar_ToggleWraparound))
                    {
                        CheckOnClick = true,
                    };
                    scrollWheelSubMenu.DropDownItems.Add(Items.Wraparound);
                }
                optionMenu.DropDownItems.Add(scrollWheelSubMenu);

                var dragDropSubMenu = new ToolStripMenuItem("Drag && Drop");
                {
                    DragButtonOptions = new Dictionary<Settings.DragButtonOption, ToolStripMenuItem>();
                    AutocheckDragButtonOptions = new Dictionary<Settings.DragButtonOption, ToolStripMenuItem>();

                    int i = 0;
                    foreach (var button in DragButtonNames)
                    {
                        DragButtonOptions.Add(button.Key, new ToolStripMenuItem(button.Value, null, new EventHandler(menuBar_SetDragButton)));
                        AutocheckDragButtonOptions.Add(button.Key, new ToolStripMenuItem(button.Value, null, new EventHandler(menuBar_SetAutocheckDragButton)));
                        i++;
                    }
                    
                    Items.DragButton = new ToolStripMenuItem("Drag Button", null, DragButtonOptions.Values.ToArray());
                    dragDropSubMenu.DropDownItems.Add(Items.DragButton);

                    Items.AutocheckDragButton = new ToolStripMenuItem("Autocheck Drag Button", null, AutocheckDragButtonOptions.Values.ToArray());
                    dragDropSubMenu.DropDownItems.Add(Items.AutocheckDragButton);
                }
                optionMenu.DropDownItems.Add(dragDropSubMenu);

                var songMarkersSubMenu = new ToolStripMenuItem("Song Markers");
                {
                    Items.MoveLocation = new ToolStripMenuItem("Move Location to Song", null, new EventHandler(menuBar_ToggleMoveLocation))
                    {
                        CheckOnClick = true,
                    };
                    songMarkersSubMenu.DropDownItems.Add(Items.MoveLocation);

                    //Items.Autocheck = new ToolStripMenuItem("Autocheck Songs", null, new EventHandler(menuBar_ToggleAutocheckSongs))
                    //{
                    //    CheckOnClick = true,
                    //};
                    //songMarkersSubMenu.DropDownItems.Add(Items.Autocheck);

                    SongMarkerBehaviourOptions = new Dictionary<Settings.SongMarkerBehaviourOption, ToolStripMenuItem>();

                    int i = 0;
                    foreach (var behaviour in SongMarkerBehaviourNames)
                    {
                        SongMarkerBehaviourOptions.Add(behaviour.Key, new ToolStripMenuItem(behaviour.Value, null, new EventHandler(menuBar_SetSongMarkerBehaviour)));
                        i++;
                    }

                    Items.Behaviour = new ToolStripMenuItem("Default Song Marker Behaviour", null, SongMarkerBehaviourOptions.Values.ToArray());
                    songMarkersSubMenu.DropDownItems.Add(Items.Behaviour);
                }
                optionMenu.DropDownItems.Add(songMarkersSubMenu);

                ToolStripMenuItem wothSubMenu = new ToolStripMenuItem("WotH");
                {
                    Items.EnableLastWoth = new ToolStripMenuItem("Enable Last WotH", null, new EventHandler(menuBar_ToggleEnableLastWotH))
                    {
                        CheckOnClick = true,
                    };
                    wothSubMenu.DropDownItems.Add(Items.EnableLastWoth);


                    LastWothColorOptions = new Dictionary<KnownColor, ToolStripMenuItem>();

                    var firstColorId = 28;
                    var lastColorId = 167;

                    for (int i = firstColorId; i <= lastColorId; i++)
                    {
                        var color = (KnownColor)i;
                        LastWothColorOptions.Add(color, new ToolStripMenuItem(color.ToString(), null, new EventHandler(menuBar_SetLastWothColor)));
                        i++;
                    }

                    Items.LastWothColor = new ToolStripMenuItem("Last WotH Color", null, LastWothColorOptions.Values.ToArray());
                    wothSubMenu.DropDownItems.Add(Items.LastWothColor);

                    Items.EnableDuplicateWoth = new ToolStripMenuItem("Allow Duplicate WotH Entries", null, new EventHandler(menuBar_ToggleEnableDuplicateWotH))
                    {
                        CheckOnClick = true,
                    };
                    wothSubMenu.DropDownItems.Add(Items.EnableDuplicateWoth);
                }
                optionMenu.DropDownItems.Add(wothSubMenu);

                ToolStripMenuItem barrenSubMenu = new ToolStripMenuItem("Barren");
                {
                    Items.EnableBarrenColors = new ToolStripMenuItem("Enable Barren Colors", null, new EventHandler(menuBar_ToggleEnableBarrenColors))
                    {
                        CheckOnClick = true,
                    };
                    barrenSubMenu.DropDownItems.Add(Items.EnableBarrenColors);
                }
                optionMenu.DropDownItems.Add(barrenSubMenu);
            }
            MenuStrip.Items.Add(optionMenu);
        }

        public void ReadSettings()
        {
            Items.ShowMenuBar.Checked = LocalSettings.ShowMenuBar;
            Enabled = LocalSettings.ShowMenuBar;
            if (Enabled)
                menuBar_Show();
            else
                menuBar_Hide();

            Items.InvertScrollWheel.Checked = LocalSettings.InvertScrollWheel;
            Items.Wraparound.Checked = LocalSettings.DefaultDungeonNames.Wraparound.Value;

            DragButtonOptions[LocalSettings.DragButton].Checked = true;
            AutocheckDragButtonOptions[LocalSettings.AutocheckDragButton].Checked = true;

            Items.MoveLocation.Checked = LocalSettings.MoveLocationToSong;
            //Items.Autocheck.Checked = Settings.AutoCheckSongs;
            SongMarkerBehaviourOptions[LocalSettings.SongMarkerBehaviour].Checked = true;

            Items.EnableDuplicateWoth.Checked = LocalSettings.EnableDuplicateWoth;
            Items.EnableLastWoth.Checked = LocalSettings.EnableLastWoth;
            LastWothColorOptions[LocalSettings.LastWothColor].Checked = true;

            Items.EnableBarrenColors.Checked = LocalSettings.EnableBarrenColors;
        }

        public void SetRenderer()
        {
            MenuStrip.BackColor = Form.BackColor;

            ProfessionalColorTable theme;
            if (MenuStrip.BackColor.GetBrightness() < 0.5)
            {
                MenuStrip.ForeColor = Color.White;
                theme = new Form1_MenuBar_ColorTable_DarkTheme(MenuStrip.BackColor);
            }
            else
            {
                MenuStrip.ForeColor = Color.Black;
                theme = new Form1_MenuBar_ColorTable_LightTheme(MenuStrip.BackColor);
            }

            MenuStrip.Renderer = new ToolStripProfessionalRenderer(theme);
        }

        public void menuBar_Reset(object sender, EventArgs e)
        {
            Form.Reset(sender);
        }

        public void menuBar_Enable(object sender, EventArgs e)
        {
            if (Enabled)
            {
                menuBar_Hide();
                Enabled = false;
            }
            else
            {
                menuBar_Show();
                Enabled = true;
            }

            LocalSettings.ShowMenuBar = Enabled;
            Items.ShowMenuBar.Checked = Enabled;
            JsonIO.Write(LocalSettings, LocalSettings.LocalSettingsFileName);
            Form.UpdateSettings();
        }

        public void menuBar_Show()
        {
            Size = SavedSize;
            Form.Size = new Size(Form.Size.Width, Form.Size.Height + Size.Height);
            Form.Refresh();
        }

        public void menuBar_Hide()
        {
            Form.Size = new Size(Form.Size.Width, Form.Size.Height - Size.Height);
            Size = new Size(0, 0);
            Form.Refresh();
        }

        private void menuBar_ToggleInvertScrollWheel(object sender, EventArgs e)
        {
            // Items.InvertScrollWheel.Checked = !Items.InvertScrollWheel.Checked;
            LocalSettings.InvertScrollWheel = Items.InvertScrollWheel.Checked;
            JsonIO.Write(LocalSettings, LocalSettings.LocalSettingsFileName);
            Form.UpdateSettings();
        }

        private void menuBar_ToggleWraparound(object sender, EventArgs e)
        {
            // Items.Wraparound.Checked = !Items.Wraparound.Checked;
            LocalSettings.DefaultDungeonNames.Wraparound = Items.Wraparound.Checked;
            JsonIO.Write(LocalSettings, LocalSettings.LocalSettingsFileName);
            Form.UpdateSettings();
        }

        private void menuBar_SetDragButton(object sender, EventArgs e)
        {
            var choice = (ToolStripMenuItem)sender;

            DragButtonOptions[LocalSettings.DragButton].Checked = false;
            choice.Checked = true;

            var option = DragButtonOptions.FirstOrDefault((x) => x.Value == choice);
            if (option.Value == null) throw new NotImplementedException();
            LocalSettings.DragButton = option.Key;
            JsonIO.Write(LocalSettings, LocalSettings.LocalSettingsFileName);
            Form.UpdateSettings();
        }

        private void menuBar_SetAutocheckDragButton(object sender, EventArgs e)
        {
            var choice = (ToolStripMenuItem)sender;

            AutocheckDragButtonOptions[LocalSettings.AutocheckDragButton].Checked = false;
            choice.Checked = true;

            var option = AutocheckDragButtonOptions.FirstOrDefault((x) => x.Value == choice);
            if (option.Value == null) throw new NotImplementedException();
            LocalSettings.AutocheckDragButton = option.Key;
            JsonIO.Write(LocalSettings, LocalSettings.LocalSettingsFileName);
            Form.UpdateSettings();
        }

        //private void menuBar_ToggleAutocheckSongs(object sender, EventArgs e)
        //{
        //    Items.Autocheck.Checked = !Items.Autocheck.Checked;
        //    Settings.AutoCheckSongs = Items.Autocheck.Checked;
        //    Settings.Write();
        //}

        private void menuBar_ToggleMoveLocation(object sender, EventArgs e)
        {
            // Items.MoveLocation.Checked = !Items.MoveLocation.Checked;
            LocalSettings.MoveLocationToSong = Items.MoveLocation.Checked;
            JsonIO.Write(LocalSettings, LocalSettings.LocalSettingsFileName);
            Form.UpdateSettings();
        }

        private void menuBar_SetSongMarkerBehaviour(object sender, EventArgs e)
        {
            var choice = (ToolStripMenuItem)sender;

            SongMarkerBehaviourOptions[LocalSettings.SongMarkerBehaviour].Checked = false;
            choice.Checked = true;

            var option = SongMarkerBehaviourOptions.FirstOrDefault((x) => x.Value == choice);
            if (option.Value == null) throw new NotImplementedException();
            LocalSettings.SongMarkerBehaviour = option.Key;
            JsonIO.Write(LocalSettings, LocalSettings.LocalSettingsFileName);
            Form.UpdateSettings();
        }

        private void menuBar_LoadLayout(object sender, EventArgs e)
        {
            if (OpenLayoutDialog.ShowDialog() == DialogResult.OK)
            {
                var filePath = OpenLayoutDialog.FileName;
                Form.LoadLayout(filePath);
            }
        }

        private void menuBar_ToggleEnableDuplicateWotH(object sender, EventArgs e)
        {
            // Items.EnableLastWoth.Enabled = !Items.EnableLastWoth.Enabled;
            LocalSettings.EnableDuplicateWoth = Items.EnableDuplicateWoth.Checked;
            JsonIO.Write(LocalSettings, LocalSettings.LocalSettingsFileName);
            Form.UpdateSettings();
        }

        private void menuBar_ToggleEnableLastWotH(object sender, EventArgs e)
        {
            // Items.EnableLastWoth.Enabled = !Items.EnableLastWoth.Enabled;
            LocalSettings.EnableLastWoth = Items.EnableLastWoth.Checked;
            JsonIO.Write(LocalSettings, LocalSettings.LocalSettingsFileName);
            Form.UpdateSettings();
        }

        private void menuBar_ToggleEnableBarrenColors(object sender, EventArgs e)
        {
            // Items.EnableBarrenColors.Enabled = !Items.EnableBarrenColors.Enabled;
            LocalSettings.EnableBarrenColors = Items.EnableBarrenColors.Checked;
            JsonIO.Write(LocalSettings, LocalSettings.LocalSettingsFileName);
            Form.UpdateSettings();
        }

        private void menuBar_SetLastWothColor(object sender, EventArgs e)
        {
            var choice = (ToolStripMenuItem)sender;

            LastWothColorOptions[LocalSettings.LastWothColor].Checked = false;
            choice.Checked = true;

            var option = LastWothColorOptions.FirstOrDefault((x) => x.Value == choice);
            if (option.Value == null) throw new NotImplementedException();
            LocalSettings.LastWothColor = option.Key;
            JsonIO.Write(LocalSettings, LocalSettings.LocalSettingsFileName);
            Form.UpdateSettings();
        }

        private class Form1_MenuBar_ColorTable_LightTheme : ProfessionalColorTable
        {
            private Color bgColor;

            public Form1_MenuBar_ColorTable_LightTheme(Color bgColor)
            {
                this.bgColor = bgColor;
            }
            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(bgColor.A, bgColor.R - 50, bgColor.G - 50, bgColor.B - 50); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(bgColor.A, bgColor.R - 50, bgColor.G - 50, bgColor.B - 50); }
            }

            public override Color MenuItemBorder
            {
                get { return Color.DimGray; }
            }
        }

        private class Form1_MenuBar_ColorTable_DarkTheme : ProfessionalColorTable
        {
            private Color bgColor;

            public Form1_MenuBar_ColorTable_DarkTheme(Color bgColor)
            {
                this.bgColor = bgColor;
            }

            public override Color MenuBorder
            {
                get { return Color.LightGray; }
            }

            public override Color MenuItemSelectedGradientBegin
            {
                get { return Color.FromArgb(bgColor.A, bgColor.R + 50, bgColor.G + 50, bgColor.B + 50); }
            }
            public override Color MenuItemSelectedGradientEnd
            {
                get { return Color.FromArgb(bgColor.A, bgColor.R + 50, bgColor.G + 50, bgColor.B + 50); }
            }

            public override Color MenuItemPressedGradientBegin
            {
                get { return Color.FromArgb(bgColor.A, bgColor.R + 50, bgColor.G + 50, bgColor.B + 50); }
            }
            public override Color MenuItemPressedGradientEnd
            {
                get { return Color.FromArgb(bgColor.A, bgColor.R + 50, bgColor.G + 50, bgColor.B + 50); }
            }

            public override Color MenuStripGradientBegin
            {
                get { return Color.FromArgb(bgColor.A, bgColor.R + 50, bgColor.G + 50, bgColor.B + 200); }
            }

            public override Color MenuItemBorder
            {
                get { return Color.LightGray; }
            }
        }
    }
}
