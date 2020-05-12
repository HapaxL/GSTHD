using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerOOT
{
    public partial class Form1 : Form
    {
        SortedSet<String> ListPlaces = new SortedSet<String>();
        SortedSet<String> ListSometimesHints = new SortedSet<string>();
        List<String> ListDungeons = new List<string>();
        ComboBox comboBox_placesWoth;
        ComboBox comboBox_placesBarren;
        Panel PanelWoth;
        Panel PanelBarren;

        Button button_StartChrono;
        Button button_ResetChrono;
        Label label_Chrono;

        PictureBox pbox_collectedSkulls;

        List<Song> listSong = new List<Song>();

        Dictionary<Label, int> wothPosition = new Dictionary<Label, int>();
        List<Label> barrenPosition = new List<Label>();

        bool chronoRunning = false;
        bool SongMode = false;
        Stopwatch chrono = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
        }

        private void autocomplete()
        {
            //Textbox AutoComplete
            var source = new AutoCompleteStringCollection();
            source.AddRange(ListPlaces.ToArray());

            comboBox_placesWoth.AutoCompleteCustomSource = source;
            comboBox_placesWoth.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_placesWoth.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBox_placesBarren.AutoCompleteCustomSource = source;
            comboBox_placesBarren.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_placesBarren.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
            foreach (var categorie in json_places)
            {
                foreach (var name in categorie.Value)
                {
                    ListPlaces.Add(name.ToString());
                }
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
            }

            setListUpgrade();

            addItems();
            addSongs();
            addMedallions();

            addGoMode();

            addGuaranteedHints();

            addWothAndBarren();
            loadComboBoxData(ListPlaces.ToArray());
            autocomplete();

            addSometimesHints();

            addChrono();
            button_ResetChrono.Focus();

            this.KeyPreview = true;
            this.KeyDown += changeCollectedSkulls;
        }

        private void addChrono()
        {
            button_StartChrono = new Button
            {
                BackColor = Color.DimGray,
                ForeColor = Color.White,
                Location = new Point(11, 550),
                Name = "button_StartChrono",
                Size = new Size(52, 23),
                TabStop = false,
                Text = "Start"
            };
            button_StartChrono.Click += new EventHandler(this.button_chrono_Click);
            this.Controls.Add(button_StartChrono);

            button_ResetChrono = new Button
            {
                BackColor = Color.DimGray,
                ForeColor = Color.White,
                Location = new Point(11, button_StartChrono.Location.Y + button_StartChrono.Height + 15),
                Name = "button_ResetChrono",
                Size = new Size(52, 23),
                TabStop = false,
                Text = "Reset"
            };
            button_ResetChrono.Click += new EventHandler(this.button_chrono_reset_Click);
            this.Controls.Add(button_ResetChrono);

            label_Chrono = new Label
            {
                AutoSize = true,
                Font = new Font("Calibri", 36F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.White,
                Location = new Point(button_StartChrono.Location.X + button_StartChrono.Width + 30, button_StartChrono.Location.Y + 0),
                Name = "label_Chrono",
                Size = new Size(256, 59),
                TabStop = false,
                Text = "00:00:00.00",
                TextAlign = ContentAlignment.MiddleRight
            };
            this.Controls.Add(label_Chrono);

        }

        private void addSometimesHints()
        {
            var GossipStone1 = createNewSometimesHint("sometimes_hint_1", ListImage_SometimesHintOption, new Point(140, PanelWoth.Location.Y + PanelWoth.Height + 5));
            var GossipStone2 = createNewSometimesHint("sometimes_hint_2", ListImage_SometimesHintOption, new Point(140, GossipStone1.Location.Y + GossipStone1.Height + 5));

            var GossipStone3 = createNewSometimesHint("sometimes_hint_3", ListImage_SometimesHintOption, new Point(140, GossipStone2.Location.Y + GossipStone2.Height + 5));

            var GossipStone4 = createNewSometimesHint("sometimes_hint_4", ListImage_SometimesHintOption, new Point(GossipStone1.Location.X + GossipStone1.Height + 15 + 125, GossipStone1.Location.Y));
            var GossipStone5 = createNewSometimesHint("sometimes_hint_5", ListImage_SometimesHintOption, new Point(GossipStone4.Location.X, GossipStone2.Location.Y));
        }

        private GossipStone createNewSometimesHint(string name, List<Image> images, Point Location)
        {
            var newGossipStone = new GossipStone(name, images, Location.X, Location.Y);
            this.Controls.Add(newGossipStone);

            var newTextbox = new TextBox
            {
                BackColor = Color.FromArgb(64, 64, 64),
                Font = new Font("Calibri", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.White,
                Name = name + "_text",
                Size = new Size(125, 23),
                AutoCompleteCustomSource = new AutoCompleteStringCollection(),
                AutoCompleteMode = AutoCompleteMode.SuggestAppend,
                AutoCompleteSource = AutoCompleteSource.CustomSource
            };
            newTextbox.AutoCompleteCustomSource.AddRange(ListSometimesHints.ToArray());
            newTextbox.Location = new Point(newGossipStone.Location.X - newTextbox.Width - 5, newGossipStone.Location.Y + 5);
            this.Controls.Add(newTextbox);

            return newGossipStone;
        }

        private void addWothAndBarren()
        {
            PanelBarren = new Panel
            {
                BackColor = Color.FromArgb(64, 64, 64),
                Location = new Point(3, 200),
                Name = "panel_barren",
                Size = new Size(172, 70),
                TabStop = false
            };

            PanelWoth = new Panel
            {
                BackColor = Color.FromArgb(64, 64, 64),
                Location = new Point(PanelBarren.Location.X, PanelBarren.Location.Y + PanelBarren.Height + 2),
                Name = "panel_woth",
                Size = new Size(336, 160),
                TabStop = false
            };
            
            comboBox_placesWoth = new ComboBox
            {
                BackColor = Color.CadetBlue,
                CausesValidation = false,
                Font = new Font("Calibri", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.White,
                FormattingEnabled = true,
                IntegralHeight = false,
                MaxDropDownItems = 60,
                Name = "comboBox_placesWoth",
                Size = new Size(172, 23),
                TabIndex = 0,
                Text = ":: Way of the Hero ::"
            };
            comboBox_placesWoth.Location = new Point(5, 5);
            comboBox_placesWoth.KeyDown += new KeyEventHandler(this.comboBox_placesWoth_KeyDown);
            comboBox_placesWoth.MouseClick += new MouseEventHandler(comboBox_places_MouseClick);
            PanelWoth.Controls.Add(comboBox_placesWoth);
            this.Controls.Add(PanelWoth);

            comboBox_placesBarren = new ComboBox
            {
                BackColor = Color.IndianRed,
                CausesValidation = false,
                Font = new Font("Calibri", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.White,
                FormattingEnabled = true,
                IntegralHeight = false,
                MaxDropDownItems = 60,
                Name = "comboBox_placesBarren",
                Size = new Size(172, 23),
                TabIndex = 1,
                Text = ":: Barren ::"
            };
            comboBox_placesBarren.KeyDown += new KeyEventHandler(this.comboBox_placesBarren_KeyDown);
            comboBox_placesBarren.MouseClick += new MouseEventHandler(comboBox_places_MouseClick);
            comboBox_placesBarren.Location = new Point(0, 5);
            PanelBarren.Controls.Add(comboBox_placesBarren);
            this.Controls.Add(PanelBarren);
        }

        private void comboBox_places_MouseClick(object sender, MouseEventArgs e)
        {
            ((ComboBox)sender).Text = string.Empty;
        }

        private void comboBox_placesWoth_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_woth_Click(sender, new EventArgs());
            }
        }
        private void comboBox_placesBarren_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button_barren_Click(sender, new EventArgs());
            }
        }

        private void addGuaranteedHints()
        {
            //30skulls
            createNewGuaranteedHint("30Skulls", Properties.Resources._30_gold_skulltula, new Point(3, 145));
            //40skulls
            createNewGuaranteedHint("40Skulls", Properties.Resources._40_gold_skulltula, new Point(38, 145));
            //50skulls
            createNewGuaranteedHint("50Skulls", Properties.Resources._50_gold_skulltula, new Point(73, 145));
            //SkullMask
            createNewGuaranteedHint("SkullMask", Properties.Resources.skull_mask2, new Point(108, 145));
            //Biggoron
            createNewGuaranteedHint("Bigorron", Properties.Resources.biggoron_test, new Point(143, 145));
            //Frogs
            createNewGuaranteedHint("Frogs", Properties.Resources.frogs, new Point(178, 145));
            //OoT
            createNewGuaranteedHint("OcarinaOfTime", Properties.Resources.ocarina_of_time, new Point(178, 195));
        }

        private void createNewGuaranteedHint(string name, Image image, Point location)
        {
            PictureBox newPBox = new PictureBox
            {
                BackColor = Color.Transparent,
                Name = name,
                Image = image,
                Location = location,
                Size = new Size(32, 50)
            };

            var X = 0;
            var Y = 18;
            GossipStone newGossipStone = new GossipStone(name + "_gossip", ListImage_GuaranteedHintsOption, X, Y);
            newPBox.Controls.Add(newGossipStone);
            newGossipStone.BringToFront();
            this.Controls.Add(newPBox);
        }

        private void addGoMode()
        {

            PictureBox pbox_GoMode = new PictureBox
            {
                BackColor = Color.Transparent,
                Image = ListImage_GoMode[0],
                Location = new Point(265, 220),
                Name = "collectedSkulls",
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
                var index = ListImage_GoMode.FindIndex(x => x == pbox.Image);
                if (index == (ListImage_GoMode.Count - 1))
                {
                    pbox.Image = ListImage_GoMode[0];
                    this.BackgroundImage = null;
                }
                else
                {
                    pbox.Image = ListImage_GoMode[index + 1];
                    this.BackgroundImage = Properties.Resources.go_mode_background;
                }
            }

        }

        private PictureBox addCollectedSkulls()
        {
            pbox_collectedSkulls = new PictureBox
            {
                BackColor = Color.Transparent,
                Image = Properties.Resources.collected_skulltulas,
                Location = new Point(310, 173),
                Name = "collectedSkulls",
                Size = new Size(32, 32),
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
                Font = new Font("Consolas", 18, FontStyle.Bold),
                ForeColor = Color.White,
                Width = 42,
                Height = 32,
                AutoSize = false,
                Location = new Point(-3, -5),
                TextAlign = ContentAlignment.MiddleLeft
            };
            nb_skulls.MouseDown += new MouseEventHandler(label_collectedSkulls_MouseDown);

            pbox_collectedSkulls.Controls.Add(nb_skulls);
            //this.Controls.Add(pbox_collectedSkulls);
            return pbox_collectedSkulls;
        }

        private void addItems()
        {
            Panel newPanel = new Panel
            {
                Name = "panel_items",
                Width = 140,
                Height = 170,
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(210, 45)
            };

            var ItemSlingshot = createNewItem("slingshot", ListImage_Slingshot, new Point(2, 2), 0, newPanel);
            var ItemBomb = createNewItem("bomb", ListImage_Bomb, ItemSlingshot.Location, ItemSlingshot.Width, newPanel);
            var ItemBombchu = createNewItem("bombchu", ListImage_Bombchu, ItemBomb.Location, ItemBomb.Width, newPanel);
            var ItemHookshot = createNewItem("hookshot", ListImage_Hookshot, ItemBombchu.Location, ItemBombchu.Width, newPanel);

            var ItemBow = createNewItem("bow", ListImage_Bow, new Point(2, ItemSlingshot.Location.Y + ItemSlingshot.Height), 0, newPanel);
            var ItemArrow = createNewItemDouble("arrow", ListImage_Arrow, ItemBow.Location, ItemBow.Width, newPanel);
            var ItemSpell = createNewItemDouble("spell", ListImage_Spell, ItemArrow.Location, ItemArrow.Width, newPanel);
            var ItemMagic = createNewItem("magic", ListImage_Magic, ItemSpell.Location, ItemSpell.Width, newPanel);

            var ItemBoots = createNewItemDouble("boots", ListImage_Boots, new Point(2, ItemBow.Location.Y + ItemBow.Height), 0, newPanel);
            var ItemPrescription = createNewItem("biggoron_quest", ListImage_BiggoronQuest, ItemBoots.Location, ItemBoots.Width, newPanel);
            var ItemBoomerang = createNewItem("bommerang", ListImage_Boomerang, ItemPrescription.Location, ItemPrescription.Width, newPanel);
            var ItemScale = createNewItem("scale", ListImage_Scale, ItemBoomerang.Location, ItemBoomerang.Width, newPanel);
            
            var ItemStrength = createNewItem("strength", ListImage_Strength, new Point(2, ItemBoots.Location.Y + ItemBoots.Height), 0, newPanel);
            var ItemLens = createNewItem("lens", ListImage_Lens, ItemStrength.Location, ItemStrength.Width, newPanel);
            var ItemHammer = createNewItem("hammer", ListImage_Hammer, ItemLens.Location, ItemLens.Width, newPanel);
            var ItemTunic = createNewItemDouble("tunic", ListImage_Tunic, ItemHammer.Location, ItemHammer.Width, newPanel);
            
            var ItemWallet = createNewItem("wallet", ListImage_Wallet, new Point(2, ItemStrength.Location.Y + ItemStrength.Height), 0, newPanel);
            var ItemRutosLetter = createNewItem("rutos_letter", ListImage_RutosLetter, ItemWallet.Location, ItemWallet.Width, newPanel);
            var ItemMirrorShield = createNewItem("mirror_shield", ListImage_MirrorShield, ItemRutosLetter.Location, ItemRutosLetter.Width, newPanel);

            var collectedSkulls = addCollectedSkulls();
            collectedSkulls.Location = new Point(ItemMirrorShield.Location.X + ItemMirrorShield.Width, ItemMirrorShield.Location.Y);
            newPanel.Controls.Add(collectedSkulls);

            this.Controls.Add(newPanel);
        }
        private Item createNewItem(string name, List<Image> ListImage, Point PreviousElementLocation, int PreviousElementWidth, Panel panelItem)
        {
            var X = PreviousElementLocation.X + PreviousElementWidth+2;
            var Y = PreviousElementLocation.Y;
            Item newItem = new Item(name, ListImage, X, Y);
            panelItem.Controls.Add(newItem);
            return newItem;
        }

        private ItemDouble createNewItemDouble(string name, List<Image> ListImage, Point PreviousElementLocation, int PreviousElementWidth, Panel panelItem)
        {
            var X = PreviousElementLocation.X + PreviousElementWidth;
            var Y = PreviousElementLocation.Y;
            ItemDouble newItemDouble = new ItemDouble(name, ListImage, X, Y);
            panelItem.Controls.Add(newItemDouble);
            return newItemDouble;
        }

        private Panel addSongs()
        {
            Panel newPanel = new Panel
            {
                Name = "panel_songs",
                Width = 210,
                Height = 95,
                BackColor = Color.Transparent,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(2, 45)
            };

            var SongZeldasLullaby = createNewSong("zeldas_lullaby", ListImage_ZeldasLullaby, new Point(2, 3), 0, ListImage_TinySongs, newPanel);
            var SongEpona = createNewSong("epona", ListImage_EponasSong, SongZeldasLullaby.Location, SongZeldasLullaby.Width, ListImage_TinySongs, newPanel);
            var SongSaria = createNewSong("saria", ListImage_SariasSong, SongEpona.Location, SongEpona.Width, ListImage_TinySongs, newPanel);
            var SongSun = createNewSong("suns_song", ListImage_SunsSong, SongSaria.Location, SongSaria.Width, ListImage_TinySongs, newPanel);
            var SongTime = createNewSong("song_of_time", ListImage_SongOfTime, SongSun.Location, SongSun.Width, ListImage_TinySongs, newPanel);
            var SongStorms = createNewSong("song_of_storms", ListImage_SongOfStorms, SongTime.Location, SongTime.Width, ListImage_TinySongs, newPanel);

            var SongMinuet = createNewSong("minuet", ListImage_Minuet, new Point(0, SongZeldasLullaby.Location.Y + SongZeldasLullaby.Height + 6), 0, ListImage_TinySongs, newPanel);
            var SongBolero = createNewSong("bolero", ListImage_Bolero, SongMinuet.Location, SongMinuet.Width, ListImage_TinySongs, newPanel);
            var SongSerenade = createNewSong("serenade", ListImage_Serenade, SongBolero.Location, SongBolero.Width, ListImage_TinySongs, newPanel);
            var SongNocturne = createNewSong("nocturne", ListImage_Nocturne, SongSerenade.Location, SongSerenade.Width, ListImage_TinySongs, newPanel);
            var SongRequiem = createNewSong("requiem", ListImage_Requiem, SongNocturne.Location, SongNocturne.Width, ListImage_TinySongs, newPanel);
            var SongPrelude = createNewSong("prelude", ListImage_Prelude, SongRequiem.Location, SongRequiem.Width, ListImage_TinySongs, newPanel);
            this.Controls.Add(newPanel);
            return newPanel;
        }

        private Song createNewSong(string name, List<Image> ListImages, Point PreviousElementLocation, int PreviousElementWidth, List<Image> ListTinyImages, Panel PanelContainer)
        {
            var X = PreviousElementLocation.X + PreviousElementWidth+2;
            var Y = PreviousElementLocation.Y;
            Song newSong = new Song(name, ListImages, X, Y, ListTinyImages, SongMode);
            listSong.Add(newSong);
            PanelContainer.Controls.Add(newSong);
            return newSong;
        }

        private void addMedallions()
        {
            var Green = createNewMedallion("green_medallion", ListImage_GreenMedallion, new Point(-2, 3), 0);
            var Red = createNewMedallion("red_medallion", ListImage_RedMedallion, Green.Location, Green.Width);
            var Blue = createNewMedallion("blue_medallion", ListImage_BlueMedallion, Red.Location, Red.Width);

            var Purple = createNewMedallion("purple_medallion", ListImage_PurpleMedallion, Blue.Location, Blue.Width);
            var Orange = createNewMedallion("orange_medallion", ListImage_OrangeMedallion, Purple.Location, Purple.Width);
            var Yellow = createNewMedallion("yellow_medallion", ListImage_YellowMedallion, Orange.Location, Orange.Width);

            var Kokiri = createNewStone("kokiri_stone", ListImage_KokiriStone, Yellow.Location, Yellow.Width);
            var Goron = createNewStone("goron_stone", ListImage_GoronStone, Kokiri.Location, Kokiri.Width);
            var Zora = createNewStone("zora_stone", ListImage_ZoraStone, Goron.Location, Goron.Width);
        }

        private Medallion createNewStone(string name, List<Image> ListImage, Point PreviousElementLocation, int PreviousElementWidth)
        {
            var X = PreviousElementLocation.X + PreviousElementWidth + 7;
            var Y = PreviousElementLocation.Y;
            Medallion newMedallion = new Medallion(name, ListImage, X, Y, ListDungeons);
            this.Controls.Add(newMedallion);
            this.Controls.Add(newMedallion.SelectedDungeon);
            newMedallion.SelectedDungeon.BringToFront();
            return newMedallion;
        }

        private Medallion createNewMedallion(string name, List<Image> ListImage, Point PreviousElementLocation, int PreviousElementWidth)
        {
            var X = PreviousElementLocation.X + PreviousElementWidth + 7;
            var Y = PreviousElementLocation.Y;
            Medallion newMedallion = new Medallion(name, ListImage, X, Y, ListDungeons);
            this.Controls.Add(newMedallion);
            this.Controls.Add(newMedallion.SelectedDungeon);
            newMedallion.SelectedDungeon.BringToFront();
            return newMedallion;
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

        private void loadComboBoxData(String[] data)
        {
            comboBox_placesWoth.Items.Clear();
            comboBox_placesBarren.Items.Clear();
            comboBox_placesWoth.Items.AddRange(data);
            comboBox_placesBarren.Items.AddRange(data);
        }

        #region chrono and timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan time = chrono.Elapsed;
            label_Chrono.Text = time.ToString(@"hh\:mm\:ss\.ff");

            if (!SongMode)
            {
                foreach (Song song in listSong)
                {
                    if (song.elementFoundAtLocation.Image != song.tinyImageEmpty)
                    {
                        var filledSong = listSong.Find(x => x.tinyImage == song.elementFoundAtLocation.Image);
                        if (filledSong != null)
                        {
                            var index = filledSong.listImage.FindIndex(x => x == filledSong.Image);
                            if (index == 0) filledSong.Click_MouseUp(song, new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0));
                        }
                    }
                }
            }
        }

        private void button_chrono_Click(object sender, EventArgs e)
        {
            if (chronoRunning)
            {
                chrono.Stop();
                button_StartChrono.Text = "Start";
            }
            else
            {
                chrono.Start();
                button_StartChrono.Text = "Stop";
            }

            chronoRunning = !chronoRunning;
        }

        private void button_chrono_reset_Click(object sender, EventArgs e)
        {
            chronoRunning = false;
            chrono.Reset();
            button_StartChrono.Text = "Start";
        }
        #endregion

        private void button_woth_Click(object sender, EventArgs e)
        {
            if(wothPosition.Count < 5)
            {
                if (comboBox_placesWoth.Text != string.Empty)
                {
                    var selectedPlace = comboBox_placesWoth.Text.ToUpper().Trim();
                    var existingWoth = wothPosition.Where(x => x.Key.Text == selectedPlace).ToList();
                    if (existingWoth.Count == 0)
                    {
                        Label newLabel = new Label
                        {
                            Name = Guid.NewGuid().ToString(),
                            Text = selectedPlace,
                            ForeColor = Color.White,
                            BackColor = Color.CadetBlue,
                            Font = new Font("Corbel", 10, FontStyle.Bold),
                            Width = 200,
                            Height = 32,
                            TextAlign = ContentAlignment.MiddleLeft
                        };
                        newLabel.Location = new Point(2, (wothPosition.Count * newLabel.Height));
                        newLabel.MouseDown += new MouseEventHandler(label_woth_MouseDown);

                        for (int i = 0; i < 4; i++)
                        {
                            GossipStone newGossipStone = new GossipStone(newLabel.Name+i, ListImage_WothItemsOption, 0, 0);
                            switch (i)
                            {
                                case 0: newGossipStone.Location = new Point(newLabel.Width+5, newLabel.Location.Y); break;
                                case 1: newGossipStone.Location = new Point(newLabel.Width+5 + 32, newLabel.Location.Y); break;
                                case 2: newGossipStone.Location = new Point(newLabel.Width+5 + 64, newLabel.Location.Y); break;
                                case 3: newGossipStone.Location = new Point(newLabel.Width+5 + 96, newLabel.Location.Y); break;
                                default: break;
                            }

                            PanelWoth.Controls.Add(newGossipStone);
                        }
                        wothPosition.Add(newLabel, 1);
                        PanelWoth.Controls.Add(newLabel);
                        //Move Combobox
                        comboBox_placesWoth.Location = new Point(5, newLabel.Location.Y + newLabel.Height + 5);
                    }
                    
                }
            }
            comboBox_placesWoth.Text = string.Empty;
        }

        private void label_woth_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                var label = (Label)sender;
                var existingWoth = wothPosition.Where(x => x.Key.Text == label.Text).ToList();
                switch(existingWoth[0].Value)
                {
                    case 1:
                        existingWoth[0].Key.Font = new Font("Corbel", 10, FontStyle.Bold | FontStyle.Underline);
                        wothPosition[existingWoth[0].Key]++; 
                        break;
                    case 2:
                        existingWoth[0].Key.ForeColor = Color.MidnightBlue;
                        wothPosition[existingWoth[0].Key]++; 
                        break;
                    case 3:
                        existingWoth[0].Key.Font = new Font("Corbel", 10, FontStyle.Bold);
                        existingWoth[0].Key.ForeColor = Color.White;
                        wothPosition[existingWoth[0].Key] = 1; 
                        break;
                }
            }

            if(e.Button == MouseButtons.Right)
            {
                var lastLabel = wothPosition.Keys.Last();
                comboBox_placesWoth.Location = new Point(5, lastLabel.Location.Y+5);

                var label = (Label)sender;
                wothPosition.Remove(label);

                PanelWoth.Controls.Remove(label);
                PanelWoth.Controls.RemoveByKey(label.Name + 0);
                PanelWoth.Controls.RemoveByKey(label.Name + 1);
                PanelWoth.Controls.RemoveByKey(label.Name + 2);
                PanelWoth.Controls.RemoveByKey(label.Name + 3);

                for (int i = 0; i < wothPosition.Count; i++)
                {
                    var labelName = wothPosition.Keys.ElementAt(i).Name;
                    var wothLabel = (Label)PanelWoth.Controls.Find(labelName, false)[0];
                    wothLabel.Location = new Point(2, (i * label.Height));

                    var pictureBox1 = (PictureBox)PanelWoth.Controls.Find(labelName + 0, false)[0];
                    pictureBox1.Location = new Point(wothLabel.Width+5, wothLabel.Location.Y);

                    var pictureBox2 = (PictureBox)PanelWoth.Controls.Find(labelName + 1, false)[0];
                    pictureBox2.Location = new Point(wothLabel.Width+5 + 32, wothLabel.Location.Y);

                    var pictureBox3 = (PictureBox)PanelWoth.Controls.Find(labelName + 2, false)[0];
                    pictureBox3.Location = new Point(wothLabel.Width+5 + 64, wothLabel.Location.Y);

                    var pictureBox4 = (PictureBox)PanelWoth.Controls.Find(labelName + 3, false)[0];
                    pictureBox4.Location = new Point(wothLabel.Width+5 + 96, wothLabel.Location.Y);
                }
                
            }
        }

        private void label_barren_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                var lastLabel = barrenPosition.Last();
                comboBox_placesBarren.Location = new Point(5, lastLabel.Location.Y + 5);

                var label = (Label)sender;
                barrenPosition.Remove(label);
                label.Dispose();
                PanelBarren.Controls.Remove(label);

                for (int i = 0; i < barrenPosition.Count; i++)
                {
                    var labelName = barrenPosition[i].Name;
                    var barrenLabel = (Label)PanelBarren.Controls.Find(labelName, false)[0];
                    barrenLabel.Location = new Point(2, (i * label.Height));
                }
            }
        }

        private void button_barren_Click(object sender, EventArgs e)
        {
            if (barrenPosition.Count < 3)
            {
                if (comboBox_placesBarren.Text != string.Empty)
                {
                    var selectedPlace = comboBox_placesBarren.Text.ToUpper().Trim();
                    Label newLabel = new Label
                    {
                        Name = Guid.NewGuid().ToString(),
                        Text = selectedPlace,
                        ForeColor = Color.White,
                        BackColor = Color.IndianRed,
                        Font = new Font("Corbel", 9, FontStyle.Bold),
                        Width = 192,
                        TextAlign = ContentAlignment.MiddleLeft
                    };
                    newLabel.Location = new Point(2, (barrenPosition.Count * newLabel.Height));
                    newLabel.MouseClick += new MouseEventHandler(label_barren_MouseClick);
                    barrenPosition.Add(newLabel);
                    PanelBarren.Controls.Add(newLabel);
                    //Move Combobox
                    comboBox_placesBarren.Location = new Point(0, newLabel.Location.Y + newLabel.Height);
                }
            }
            comboBox_placesBarren.Text = string.Empty;
        }

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

        private void button_save_Click(object sender, EventArgs e)
        {
            saveJSON();
        }

        private void saveJSON()
        {

        }        
    }
}
