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
        SortedSet<String> ListSometimesHints = new SortedSet<string>();
        List<String> ListDungeons = new List<string>();
        PanelWothBarren PanelWoth;
        PanelWothBarren PanelBarren;

        Label label_Chrono;

        PictureBox pbox_collectedSkulls;

        List<Song> listSong = new List<Song>();

        Dictionary<Label, int> wothPosition = new Dictionary<Label, int>();
        List<Label> barrenPosition = new List<Label>();

        bool chronoRunning = false;
        bool SongMode = false;
        string ActiveLayoutName = string.Empty;
        Layout ActiveLayout;

        Stopwatch chrono = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Gossip Stones Tracker v1.8 (Standard Edition)";
            this.AcceptButton = null;
            this.MaximizeBox = false;
            timer1.Start();

            ListDungeons = new List<string>
            {
                "????",
                "FREE",
                "DEKU",
                "DC",
                "JABU",
                "FOREST",
                "FIRE",
                "WATER",
                "SHADOW",
                "SPIRIT"
            };

            ListPlaces.Add("");
            JObject json_places = JObject.Parse(File.ReadAllText(@"oot_places.json"));
            foreach (var property in json_places)
            {
                ListPlaces.Add(property.Key.ToString());
                ListPlacesWithTag.Add(property.Key, property.Value.ToString());
            }

            JObject json_hints = JObject.Parse(File.ReadAllText(@"sometimes_hints.json"));
            foreach (var categorie in json_hints)
            {
                foreach (var hint in categorie.Value)
                {
                    ListSometimesHints.Add(hint.ToString());
                }
            }

            JObject json_properties = JObject.Parse(File.ReadAllText(@"settings.json"));
            foreach (var property in json_properties)
            {
                if (property.Key == "MoveLocationToSong")
                {
                    SongMode = Convert.ToBoolean(property.Value);
                }
                if(property.Key == "ActiveLayout")
                {
                    ActiveLayoutName = property.Value.ToString();
                }
            }

            if (ActiveLayoutName != string.Empty)
            {
                var json_file = File.ReadAllText(@"Layouts/" + ActiveLayoutName + ".json");
                ActiveLayout = JsonConvert.DeserializeObject<Layout>(json_file);
            }
            this.Size = new Size(ActiveLayout.AppSize.Width, ActiveLayout.AppSize.Height);


            LoadListImage();

            addItems();

            addSongs();

            addMedallions();

            addGoMode();

            addGuaranteedHints();

            addWothAndBarren();
            
            addSometimesHints();

            addChrono();
            
            this.KeyPreview = true;
            this.KeyDown += changeCollectedSkulls;
        }

        private void addChrono()
        {
            label_Chrono = new Label
            {
                BackColor = Color.Transparent,
                AutoSize = true,
                Font = new Font("Calibri", 36F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.White,
                Location = new Point(ActiveLayout.Chronometer.X, ActiveLayout.Chronometer.Y),
                Name = "label_Chrono",
                Size = new Size(256, 59),
                TabStop = false,
                Text = "00:00:00.00",
                TextAlign = ContentAlignment.MiddleRight
            };
            label_Chrono.MouseClick += Label_Chrono_MouseClick;
            this.Controls.Add(label_Chrono);

        }

        private void Label_Chrono_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                button_chrono_Click(sender, new EventArgs());
            if(e.Button == MouseButtons.Right)
                button_chrono_reset_Click(sender, new EventArgs());
        }

        private void addSometimesHints()
        {
            var SometimesHint1 = new SometimesHint(ListImage_SometimesHintOption, ActiveLayout.SH_GossipStone1.X, ActiveLayout.SH_GossipStone1.Y, ListSometimesHints.ToList(), ActiveLayout.SH_GossipStone1.Size);
            this.Controls.Add(SometimesHint1.SH_GossipStone);
            this.Controls.Add(SometimesHint1.SH_TextBox);

            var SometimesHint2 = new SometimesHint(ListImage_SometimesHintOption, ActiveLayout.SH_GossipStone2.X, ActiveLayout.SH_GossipStone2.Y, ListSometimesHints.ToList(), ActiveLayout.SH_GossipStone2.Size);
            this.Controls.Add(SometimesHint2.SH_GossipStone);
            this.Controls.Add(SometimesHint2.SH_TextBox);

            var SometimesHint3 = new SometimesHint(ListImage_SometimesHintOption, ActiveLayout.SH_GossipStone3.X, ActiveLayout.SH_GossipStone3.Y, ListSometimesHints.ToList(), ActiveLayout.SH_GossipStone3.Size);
            this.Controls.Add(SometimesHint3.SH_GossipStone);
            this.Controls.Add(SometimesHint3.SH_TextBox);

            var SometimesHint4 = new SometimesHint(ListImage_SometimesHintOption, ActiveLayout.SH_GossipStone4.X, ActiveLayout.SH_GossipStone4.Y, ListSometimesHints.ToList(), ActiveLayout.SH_GossipStone4.Size);
            this.Controls.Add(SometimesHint4.SH_GossipStone);
            this.Controls.Add(SometimesHint4.SH_TextBox);

            var SometimesHint5 = new SometimesHint(ListImage_SometimesHintOption, ActiveLayout.SH_GossipStone5.X, ActiveLayout.SH_GossipStone5.Y, ListSometimesHints.ToList(), ActiveLayout.SH_GossipStone5.Size);
            this.Controls.Add(SometimesHint5.SH_GossipStone);
            this.Controls.Add(SometimesHint5.SH_TextBox);
        }

        private void addWothAndBarren()
        {
            PanelBarren = new PanelWothBarren(ListPlacesWithTag)
            {
                BackColor = Color.FromArgb(64, 64, 64),
                Location = new Point(ActiveLayout.PanelBarren.X, ActiveLayout.PanelBarren.Y),
                Name = "panel_barren",
                Size = new Size(222, 70),
                TabStop = false
            };
            this.Controls.Add(PanelBarren);
            this.Controls.Add(PanelBarren.textBoxCustom.SuggestionContainer);
            PanelBarren.SetSuggestionContainer();

            PanelWoth = new PanelWothBarren (ListPlacesWithTag, ListImage_WothItemsOption, 24)
            {
                BackColor = Color.FromArgb(64, 64, 64),
                Location = new Point(ActiveLayout.PanelWoth.X, ActiveLayout.PanelWoth.Y),
                Name = "panel_woth",
                Size = new Size(this.Width-38, 120),
                TabStop = false
            };
            this.Controls.Add(PanelWoth);
            this.Controls.Add(PanelWoth.textBoxCustom.SuggestionContainer);
            PanelWoth.SetSuggestionContainer();
        }

        private void comboBox_places_MouseClick(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).Text = string.Empty;
        }

        private void addGuaranteedHints()
        {
            //30skulls
            createNewGuaranteedHint(
                ListImage_GuaranteedHints[0], 
                ActiveLayout.Skulltulas_30.Size, 
                new Point(ActiveLayout.Skulltulas_30.X, ActiveLayout.Skulltulas_30.Y), 
                ListImage_30SkulltulasOption,
                ActiveLayout.Skulltulas_30_GossipStone.Size, 
                new Point(ActiveLayout.Skulltulas_30_GossipStone.X, ActiveLayout.Skulltulas_30_GossipStone.Y)
            );
            //40skulls
            createNewGuaranteedHint(
                ListImage_GuaranteedHints[1], 
                ActiveLayout.Skulltulas_40.Size, 
                new Point(ActiveLayout.Skulltulas_40.X, ActiveLayout.Skulltulas_40.Y), 
                ListImage_40SkulltulasOption,
                ActiveLayout.Skulltulas_40_GossipStone.Size, 
                new Point(ActiveLayout.Skulltulas_40_GossipStone.X, ActiveLayout.Skulltulas_40_GossipStone.Y)
            );
            //50skulls
            createNewGuaranteedHint(
                ListImage_GuaranteedHints[2], 
                ActiveLayout.Skulltulas_50.Size,
                new Point(ActiveLayout.Skulltulas_50.X, ActiveLayout.Skulltulas_50.Y),
                ListImage_50SkulltulasOption,
                ActiveLayout.Skulltulas_50_GossipStone.Size,
                new Point(ActiveLayout.Skulltulas_50_GossipStone.X, ActiveLayout.Skulltulas_50_GossipStone.Y)
            );
            //SkullMask
            createNewGuaranteedHint(
                ListImage_GuaranteedHints[3],
                ActiveLayout.SkullMask.Size,
                new Point(ActiveLayout.SkullMask.X, ActiveLayout.SkullMask.Y),
                ListImage_SkullMaskOption,
                ActiveLayout.SkullMask_GossipStone.Size,
                new Point(ActiveLayout.SkullMask_GossipStone.X, ActiveLayout.SkullMask_GossipStone.Y)
            );
            //Biggoron
            createNewGuaranteedHint(
                ListImage_GuaranteedHints[4],
                ActiveLayout.Biggoron.Size,
                new Point(ActiveLayout.Biggoron.X, ActiveLayout.Biggoron.Y),
                ListImage_BiggoronOption,
                ActiveLayout.Biggoron_GossipStone.Size,
                new Point(ActiveLayout.Biggoron_GossipStone.X, ActiveLayout.Biggoron_GossipStone.Y)
            );
            //Frogs
            createNewGuaranteedHint(
                ListImage_GuaranteedHints[5],
                ActiveLayout.Frogs.Size,
                new Point(ActiveLayout.Frogs.X, ActiveLayout.Frogs.Y),
                ListImage_FrogsOption,
                ActiveLayout.Frogs_GossipStone.Size,
                new Point(ActiveLayout.Frogs_GossipStone.X, ActiveLayout.Frogs_GossipStone.Y)
            );
            //OoT
            createNewGuaranteedHint(
                ListImage_GuaranteedHints[6],
                ActiveLayout.OcarinaOfTimeHint.Size,
                new Point(ActiveLayout.OcarinaOfTimeHint.X, ActiveLayout.OcarinaOfTimeHint.Y),
                ListImage_OcarinaOfTimeOption,
                ActiveLayout.OcarinaOfTimeHint_GossipStone.Size,
                new Point(ActiveLayout.OcarinaOfTimeHint_GossipStone.X, ActiveLayout.OcarinaOfTimeHint_GossipStone.Y)
            );
        }

        private void createNewGuaranteedHint(string imageName, int size, Point location, List<string> gossipImageName, int gossip_size, Point gossipLocation)
        {
            PictureBox newPBox = new PictureBox
            {
                BackColor = Color.Transparent,
                Image = (Image)Properties.Resources.ResourceManager.GetObject(imageName),
                Location = location,
                Size = new Size(size, size)
            };

            GossipStone newGossipStone = new GossipStone(gossipImageName, gossipLocation, gossip_size);
            this.Controls.Add(newGossipStone);
            this.Controls.Add(newPBox);
        }
        
        private void addGoMode()
        {

            PictureBox pbox_GoMode = new PictureBox
            {
                BackColor = Color.Transparent,
                Image = (Image)Properties.Resources.ResourceManager.GetObject(ListImage_GoMode[0]),
                Location = new Point(ActiveLayout.GoMode.X, ActiveLayout.GoMode.Y),
                Name = ListImage_GoMode[0],
                Size = new Size(32, 32),
                TabStop = false
            };
            pbox_GoMode.MouseUp += Pbox_GoMode_MouseUp;
            this.Controls.Add(pbox_GoMode);
        }

        private void Pbox_GoMode_MouseUp(object sender, MouseEventArgs e)
        {
            var pbox = (PictureBox)sender;
            if (e.Button == MouseButtons.Left)
            {
                var index = ListImage_GoMode.FindIndex(x => x == pbox.Name) + 1;
                if (index <= 0 || index >= ListImage_GoMode.Count)
                {
                    pbox.Image = (Image)Properties.Resources.ResourceManager.GetObject(ListImage_GoMode[0]);
                    //this.BackgroundImage = null;
                    pbox.Name = ListImage_GoMode[0];
                }
                else
                {
                    pbox.Image = (Image)Properties.Resources.ResourceManager.GetObject(ListImage_GoMode[index]);
                    //this.BackgroundImage = Properties.Resources.go_mode_background;
                    pbox.Name = ListImage_GoMode[index];
                }
            }

        }

        private PictureBox addCollectedSkulls()
        {
            pbox_collectedSkulls = new PictureBox
            {
                BackColor = Color.Transparent,
                Image = (Image)Properties.Resources.ResourceManager.GetObject("collected_skulltulas_" + ActiveLayout.CollectedSkulls.Size),
                Name = "collectedSkulls",
                Size = new Size(ActiveLayout.CollectedSkulls.Size, ActiveLayout.CollectedSkulls.Size),
                TabStop = false
            };

            //Count Skulltulas
            Label nb_skulls = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                Dock = DockStyle.None,
                Margin = new Padding(0, 0, 0, 0),
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.None,
                Text = "00",
                Font = new Font("Consolas", 14, FontStyle.Bold),
                ForeColor = Color.White,
                Width = 42,
                Height = 24,
                AutoSize = false,
                Location = new Point(-2, -5),
                TextAlign = ContentAlignment.MiddleLeft
            };
            nb_skulls.MouseDown += new MouseEventHandler(label_collectedSkulls_MouseDown);

            pbox_collectedSkulls.Controls.Add(nb_skulls);
            return pbox_collectedSkulls;
        }

        private void addItems()
        {
            this.Controls.Add(new Item(ListImage_Slingshot, ActiveLayout.Slingshot.X, ActiveLayout.Slingshot.Y, ActiveLayout.Slingshot.Size));
            this.Controls.Add(new Item(ListImage_Bomb, ActiveLayout.Bombs.X, ActiveLayout.Bombs.Y, ActiveLayout.Bombs.Size));
            this.Controls.Add(new Item(ListImage_Bombchu, ActiveLayout.Bombchus.X, ActiveLayout.Bombchus.Y, ActiveLayout.Bombchus.Size));
            this.Controls.Add(new Item(ListImage_Hookshot, ActiveLayout.Hookshot.X, ActiveLayout.Hookshot.Y, ActiveLayout.Hookshot.Size));
            this.Controls.Add(new Item(ListImage_Bow, ActiveLayout.Bow.X, ActiveLayout.Bow.Y, ActiveLayout.Bow.Size));
            this.Controls.Add(new ItemDouble(ListImage_Arrow, ActiveLayout.Arrows.X, ActiveLayout.Arrows.Y, ActiveLayout.Arrows.Size));
            this.Controls.Add(new ItemDouble(ListImage_Spell, ActiveLayout.Spells.X, ActiveLayout.Spells.Y, ActiveLayout.Spells.Size));
            this.Controls.Add(new Item(ListImage_Magic, ActiveLayout.Magic.X, ActiveLayout.Magic.Y, ActiveLayout.Magic.Size));
            this.Controls.Add(new ItemDouble(ListImage_Boots, ActiveLayout.Boots.X, ActiveLayout.Boots.Y, ActiveLayout.Boots.Size));
            this.Controls.Add(new Item(ListImage_BiggoronQuest, ActiveLayout.BiggoronItem.X, ActiveLayout.BiggoronItem.Y, ActiveLayout.BiggoronItem.Size));
            this.Controls.Add(new Item(ListImage_Boomerang, ActiveLayout.Boomerang.X, ActiveLayout.Boomerang.Y, ActiveLayout.Boomerang.Size));
            this.Controls.Add(new Item(ListImage_Scale, ActiveLayout.Scale.X, ActiveLayout.Scale.Y, ActiveLayout.Scale.Size));
            this.Controls.Add(new Item(ListImage_Strength, ActiveLayout.Strength.X, ActiveLayout.Strength.Y, ActiveLayout.Strength.Size));
            this.Controls.Add(new Item(ListImage_Lens, ActiveLayout.Lens.X, ActiveLayout.Lens.Y, ActiveLayout.Lens.Size));
            this.Controls.Add(new Item(ListImage_Hammer, ActiveLayout.Hammer.X, ActiveLayout.Hammer.Y, ActiveLayout.Hammer.Size));
            this.Controls.Add(new ItemDouble(ListImage_Tunic, ActiveLayout.Tunics.X, ActiveLayout.Tunics.Y, ActiveLayout.Tunics.Size));
            this.Controls.Add(new Item(ListImage_Wallet, ActiveLayout.Wallet.X, ActiveLayout.Wallet.Y, ActiveLayout.Wallet.Size));
            this.Controls.Add(new Item(ListImage_RutosLetter, ActiveLayout.RutosLetter.X, ActiveLayout.RutosLetter.Y, ActiveLayout.RutosLetter.Size));
            this.Controls.Add(new Item(ListImage_MirrorShield, ActiveLayout.MirrorShield.X, ActiveLayout.MirrorShield.Y, ActiveLayout.MirrorShield.Size));

            var collectedSkulls = addCollectedSkulls();
            collectedSkulls.Location = new Point(ActiveLayout.CollectedSkulls.X, ActiveLayout.CollectedSkulls.Y);
            this.Controls.Add(collectedSkulls);
        }

        private void addSongs()
        {
            this.Controls.Add(new Song(ListImage_ZeldasLullaby, ActiveLayout.ZeldasLullaby.X, ActiveLayout.ZeldasLullaby.Y, ListImage_TinySongs, ActiveLayout.ZeldasLullaby.Size, SongMode));
            this.Controls.Add(new Song(ListImage_EponasSong, ActiveLayout.EponasSong.X, ActiveLayout.EponasSong.Y, ListImage_TinySongs, ActiveLayout.EponasSong.Size, SongMode));
            this.Controls.Add(new Song(ListImage_SariasSong, ActiveLayout.SariasSong.X, ActiveLayout.SariasSong.Y, ListImage_TinySongs, ActiveLayout.SariasSong.Size, SongMode));
            this.Controls.Add(new Song(ListImage_SunsSong, ActiveLayout.SunsSong.X, ActiveLayout.SunsSong.Y, ListImage_TinySongs, ActiveLayout.SunsSong.Size, SongMode));
            this.Controls.Add(new Song(ListImage_SongOfTime, ActiveLayout.SongOfTime.X, ActiveLayout.SongOfTime.Y, ListImage_TinySongs, ActiveLayout.SongOfTime.Size, SongMode));
            this.Controls.Add(new Song(ListImage_SongOfStorms, ActiveLayout.SongOfStorms.X, ActiveLayout.SongOfStorms.Y, ListImage_TinySongs, ActiveLayout.SongOfStorms.Size, SongMode));
            this.Controls.Add(new Song(ListImage_Minuet, ActiveLayout.Minuet.X, ActiveLayout.Minuet.Y, ListImage_TinySongs, ActiveLayout.Minuet.Size, SongMode));
            this.Controls.Add(new Song(ListImage_Bolero, ActiveLayout.Bolero.X, ActiveLayout.Bolero.Y, ListImage_TinySongs, ActiveLayout.Bolero.Size, SongMode));
            this.Controls.Add(new Song(ListImage_Serenade, ActiveLayout.Serenade.X, ActiveLayout.Serenade.Y, ListImage_TinySongs, ActiveLayout.Serenade.Size, SongMode));
            this.Controls.Add(new Song(ListImage_Nocturne, ActiveLayout.Nocturne.X, ActiveLayout.Nocturne.Y, ListImage_TinySongs, ActiveLayout.Nocturne.Size, SongMode));
            this.Controls.Add(new Song(ListImage_Requiem, ActiveLayout.Requiem.X, ActiveLayout.Requiem.Y, ListImage_TinySongs, ActiveLayout.Requiem.Size, SongMode));
            this.Controls.Add(new Song(ListImage_Prelude, ActiveLayout.Prelude.X, ActiveLayout.Prelude.Y, ListImage_TinySongs, ActiveLayout.Prelude.Size, SongMode));
        }

        private void addMedallions()
        {
            createNewMedallion(ListImage_GreenMedallion, ActiveLayout.GreenMedallion.X, ActiveLayout.GreenMedallion.Y, ListDungeons, ActiveLayout.GreenMedallion.Size);
            createNewMedallion(ListImage_RedMedallion, ActiveLayout.RedMedallion.X, ActiveLayout.RedMedallion.Y, ListDungeons, ActiveLayout.RedMedallion.Size);
            createNewMedallion(ListImage_BlueMedallion, ActiveLayout.BlueMedallion.X, ActiveLayout.BlueMedallion.Y, ListDungeons, ActiveLayout.BlueMedallion.Size);
            createNewMedallion(ListImage_PurpleMedallion, ActiveLayout.PurpleMedallion.X, ActiveLayout.PurpleMedallion.Y, ListDungeons, ActiveLayout.PurpleMedallion.Size);
            createNewMedallion(ListImage_OrangeMedallion, ActiveLayout.OrangeMedallion.X, ActiveLayout.OrangeMedallion.Y, ListDungeons, ActiveLayout.OrangeMedallion.Size);
            createNewMedallion(ListImage_YellowMedallion, ActiveLayout.YellowMedallion.X, ActiveLayout.YellowMedallion.Y, ListDungeons, ActiveLayout.YellowMedallion.Size);
            createNewMedallion(ListImage_KokiriStone, ActiveLayout.KokiriStone.X, ActiveLayout.KokiriStone.Y, ListDungeons, ActiveLayout.KokiriStone.Size);
            createNewMedallion(ListImage_GoronStone, ActiveLayout.GoronStone.X, ActiveLayout.GoronStone.Y, ListDungeons, ActiveLayout.GoronStone.Size);
            createNewMedallion(ListImage_ZoraStone, ActiveLayout.ZoraStone.X, ActiveLayout.ZoraStone.Y, ListDungeons, ActiveLayout.ZoraStone.Size);
        }

        private void createNewMedallion(List<string> ListImage, int X, int Y, List<string> ListText, int size)
        {
            Medallion newMedallion = new Medallion(ListImage, X, Y, ListText, size);
            this.Controls.Add(newMedallion);
            this.Controls.Add(newMedallion.SelectedDungeon);
            newMedallion.SetSelectedDungeonLocation();
            newMedallion.SelectedDungeon.BringToFront();
        }

        private void changeCollectedSkulls(object sender, KeyEventArgs k)
        {
            if (k.KeyCode == Keys.F9)
                button_chrono_Click(sender, new EventArgs());
            if (k.KeyCode == Keys.F11)
            {
                label_collectedSkulls_MouseDown(pbox_collectedSkulls.Controls[0], new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
            }
            if (k.KeyCode == Keys.F12)
                label_collectedSkulls_MouseDown(pbox_collectedSkulls.Controls[0], new MouseEventArgs(MouseButtons.Right, 1, 0, 0, 0));
        }

        #region chrono and timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan time = chrono.Elapsed;
            label_Chrono.Text = time.ToString(@"hh\:mm\:ss\.ff");
        }

        private void button_chrono_Click(object sender, EventArgs e)
        {
            if (chronoRunning)
            {
                chrono.Stop();
            }
            else
            {
                chrono.Start();
            }
            chronoRunning = !chronoRunning;
        }

        private void button_chrono_reset_Click(object sender, EventArgs e)
        {
            chronoRunning = false;
            chrono.Reset();
        }
        #endregion

        private void label_collectedSkulls_MouseDown(object sender, MouseEventArgs e)
        {
            var label_collectedSkulls = (Label)sender;
            var intLabelText = Convert.ToInt32(label_collectedSkulls.Text);
            if(e.Button == MouseButtons.Left)
                intLabelText++;
            if (e.Button == MouseButtons.Right && intLabelText > 0)
                intLabelText--;
            
            if(intLabelText < 10) 
                label_collectedSkulls.Text = "0" + intLabelText.ToString();
            else 
                label_collectedSkulls.Text = intLabelText.ToString();
        }
    }
}
