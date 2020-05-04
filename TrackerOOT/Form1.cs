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
        HashSet<String> placesByCategories = new HashSet<String>();
        SortedSet<String> placesAlphabeticalOrder = new SortedSet<String>();
        List<String> listDungeons = new List<string>();

        List<Image> bottleUpgrade = new List<Image>();
        List<Image> strengthUpgrade = new List<Image>();
        List<Image> hookUpgrade = new List<Image>();
        List<Image> scaleUpgrade = new List<Image>();
        List<Image> ocarinaUpgrade = new List<Image>();
        List<Image> biggoronQuestUpgrade = new List<Image>();
        List<Image> maskQuestUpgrade = new List<Image>();
        List<Image> magicUpgrade = new List<Image>();
        List<Image> walletUpgrade = new List<Image>();


        bool isMouseDown = false;

        bool dead30skulls = false;
        bool dead40skulls = false;
        bool dead50skulls = false;
        bool deadSkullMask = false;
        bool deadBiggoron = false;
        bool deadFrogs = false;

        bool isColoredGreenMedaillon = false;
        bool isColoredRedMedaillon = false;
        bool isColoredBlueMedaillon = false;
        bool isColoredOrangeMedaillon = false;
        bool isColoredPurpleMedaillon = false;
        bool isColoredYellowMedaillon = false;
        bool isColoredKokiriStone = false;
        bool isColoredGoronStone = false;
        bool isColoredZoraStone = false;

        bool chronoRunning = false;

        bool alphabeticalOrder = true;

        Stopwatch chrono = new Stopwatch();

        public Form1()
        {
            InitializeComponent();
        }

        private void autocomplete()
        {
            //Textbox AutoComplete
            var source = new AutoCompleteStringCollection();
            source.AddRange(placesAlphabeticalOrder.ToArray());

            comboBox_woth1.AutoCompleteCustomSource = source;
            comboBox_woth1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_woth1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBox_woth2.AutoCompleteCustomSource = source;
            comboBox_woth2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_woth2.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBox_woth3.AutoCompleteCustomSource = source;
            comboBox_woth3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_woth3.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBox_woth4.AutoCompleteCustomSource = source;
            comboBox_woth4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_woth4.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBox_woth5.AutoCompleteCustomSource = source;
            comboBox_woth5.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_woth5.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBox_barren1.AutoCompleteCustomSource = source;
            comboBox_barren1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_barren1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBox_barren2.AutoCompleteCustomSource = source;
            comboBox_barren2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_barren2.AutoCompleteSource = AutoCompleteSource.CustomSource;

            comboBox_barren3.AutoCompleteCustomSource = source;
            comboBox_barren3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox_barren3.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listDungeons = new List<string>
            {
                "????",
                "FREE",
                "DEKU",
                "DC",
                "JABU",
                "FOREST",
                "FIRE",
                "WATER",
                "SPIRIT",
                "SHADOW"
            };
            this.label_green_medaillon.Text = listDungeons[0];

            placesAlphabeticalOrder.Add("");
            placesByCategories.Add("");

            JObject json = JObject.Parse(File.ReadAllText(@"oot_places.json"));
            foreach(var categorie in json)
            {
                placesByCategories.Add(categorie.Key.ToString());
                foreach(var name in categorie.Value)
                {
                    placesByCategories.Add(name.ToString());
                    placesAlphabeticalOrder.Add(name.ToString());
                }
            }

            loadComboBoxData(placesAlphabeticalOrder.ToArray());

            autocomplete();
            timer1.Start();

            pictureBox_oot_hint.AllowDrop = true;
            pictureBox_30skulls.AllowDrop = true;
            pictureBox_40skulls.AllowDrop = true;
            pictureBox_50skulls.AllowDrop = true;
            pictureBox_skullMask.AllowDrop = true;
            pictureBox_biggoron.AllowDrop = true;
            pictureBox_frogs.AllowDrop = true;
            
            panel_item_woth1.AllowDrop = true;
            panel_item_woth2.AllowDrop = true;
            panel_item_woth3.AllowDrop = true;
            panel_item_woth4.AllowDrop = true;
            panel_item_woth5.AllowDrop = true;

            setListUpgrade();
            setPictureBoxTag();
        }

        private void setListUpgrade()
        {
            bottleUpgrade = new List<Image>
            {
                Properties.Resources.bottle_empty_bw,
                Properties.Resources.bottle_empty,
                Properties.Resources.bottle_big_poe,
                Properties.Resources.bottle_blue_fire,
                Properties.Resources.bottle_bugs,
                Properties.Resources.bottle_poe,
                Properties.Resources.bottle_fairy,
                Properties.Resources.bottle_milk,
                Properties.Resources.bottle_green,
                Properties.Resources.bottle_red,
                Properties.Resources.bottle_blue
            };
            this.pictureBox_bottle2.Image = bottleUpgrade[0];
            this.pictureBox_bottle3.Image = bottleUpgrade[0];
            this.pictureBox_bottle4.Image = bottleUpgrade[0];

            strengthUpgrade = new List<Image>
            {
                Properties.Resources.strength_bw,
                Properties.Resources.strength,
                Properties.Resources.strength2,
                Properties.Resources.strength3
            };
            this.pictureBox_strength.Image = strengthUpgrade[0];

            hookUpgrade = new List<Image>
            {
                Properties.Resources.hookshot_bw,
                Properties.Resources.hookshot,
                Properties.Resources.longshot,
            };
            this.pictureBox_hookshot.Image = hookUpgrade[0];

            scaleUpgrade = new List<Image>
            {
                Properties.Resources.scale_bw,
                Properties.Resources.scale,
                Properties.Resources.golden_scale
            };
            this.pictureBox_scale.Image = scaleUpgrade[0];

            ocarinaUpgrade = new List<Image>
            {
                Properties.Resources.ocarina_bw,
                Properties.Resources.ocarina,
                Properties.Resources.ocarina_of_time,
            };
            this.pictureBox_ocarina.Image = ocarinaUpgrade[0];

            biggoronQuestUpgrade = new List<Image>
            {
                Properties.Resources.egg_bw,
                Properties.Resources.egg,
                Properties.Resources.chicken,
                Properties.Resources.blue_chicken,
                Properties.Resources.mushroom_powder,
                Properties.Resources.saw,
                Properties.Resources.broken_bgs,
                Properties.Resources.prescription,
                Properties.Resources.kz_frog,
                Properties.Resources.eye_drops,
                Properties.Resources.claim_check
            };
            this.pictureBox_egg_biggoron.Image = biggoronQuestUpgrade[0];

            maskQuestUpgrade = new List<Image>
            {
                Properties.Resources.egg_bw,
                Properties.Resources.egg,
                Properties.Resources.chicken,
                Properties.Resources.zeldas_letter,
                Properties.Resources.keaton_mask,
                Properties.Resources.skull_mask2,
                Properties.Resources.spooky_mask,
                Properties.Resources.bunny_hood,
                Properties.Resources.mask_of_truth,
                Properties.Resources.goron_mask,
                Properties.Resources.zora_mask,
                Properties.Resources.gerudo_mask
            };
            this.pictureBox_egg_masks.Image = maskQuestUpgrade[0];

            magicUpgrade = new List<Image>
            { 
                Properties.Resources.magic_bw,
                Properties.Resources.magic,
                Properties.Resources.double_magic,
            };
            this.pictureBox_magic.Image = magicUpgrade[0];

            walletUpgrade = new List<Image>
            {
                 Properties.Resources.wallet,
                 Properties.Resources.wallet2,
                 Properties.Resources.wallet3,
            };
            this.pictureBox_wallet.Image = walletUpgrade[0];
        }
        private void setPictureBoxTag()
        {
            //row1
            pictureBox_sticks.Image.Tag = "sticks";
            pictureBox_nuts.Image.Tag = "nuts";
            pictureBox_bombs.Image.Tag = "bombs";
            pictureBox_bow.Image.Tag = "bow";
            pictureBox_fire_arrow.Image.Tag = "fire_arrow";
            pictureBox_dins_fire.Image.Tag = "dins_fire";
            //row2
            pictureBox_slingshot.Image.Tag = "slingshot";
            pictureBox_ocarina.Image.Tag = "ocarina";
            pictureBox_bchu.Image.Tag = "bomb_chu";
            pictureBox_hookshot.Image.Tag = "hookshot";
            pictureBox_ice_arrow.Image.Tag = "ice_arrow";
            pictureBox_farores_wind.Image.Tag = "farores_wind";
            //row3
            pictureBox_boomerang.Image.Tag = "boomerang";
            pictureBox_lens.Image.Tag = "lens";
            pictureBox_beans.Image.Tag = "beans";
            pictureBox_hammer.Image.Tag = "hammer";
            pictureBox_light_arrow.Image.Tag = "light_arrow";
            pictureBox_nairus_love.Image.Tag = "nairus_love";
            //row4
            pictureBox_bottle1.Image.Tag = "bottle_rutos_letter";
            pictureBox_bottle2.Image.Tag = "bottle2";
            pictureBox_bottle3.Image.Tag = "bottle3";
            pictureBox_bottle4.Image.Tag = "bottle4";
            pictureBox_egg_biggoron.Image.Tag = "egg_biggoron";
            pictureBox_egg_masks.Image.Tag = "egg_masks";

            //column1
            pictureBox_kokiri_sword.Image.Tag = "kokiri_sword";
            pictureBox_master_sword.Image.Tag = "master_sword";
            pictureBox_bgs.Image.Tag = "bgs";
            pictureBox_kokiri_shield.Image.Tag = "kokiri_shield";
            pictureBox_hylian_shield.Image.Tag = "hylian_shield";
            pictureBox_mirror_shield.Image.Tag = "mirror_shield";
            pictureBox_green_tunic.Image.Tag = "green_tunic";
            pictureBox_goron_tunic.Image.Tag = "goron_tunic";
            pictureBox_zora_tunic.Image.Tag = "zora_tunic";
            pictureBox_boots.Image.Tag = "boots";
            pictureBox_iron_boots.Image.Tag = "iron_boots";
            pictureBox_hover_boots.Image.Tag = "hover_boots";
            //column2
            pictureBox_magic.Image.Tag = "magic";
            pictureBox_wallet.Image.Tag = "wallet";
            pictureBox_gerudo_card.Image.Tag = "gerudo_card";
            pictureBox_stone_of_agony.Image.Tag = "stone_of_agony";
            pictureBox_scale.Image.Tag = "scale";
            pictureBox_strength.Image.Tag = "strength";
            pictureBox_kokiri_stone.Image.Tag = "kokiri_stone";
            pictureBox_goron_stone.Image.Tag = "goron_stone";
            pictureBox_zora_stone.Image.Tag = "zora_stone";

            //songs
            pictureBox_ZL.Image.Tag = "zeldas_lullaby";
            pictureBox_epona.Image.Tag = "epona";
            pictureBox_saria.Image.Tag = "saria";
            pictureBox_suns_song.Image.Tag = "suns_song";
            pictureBox_song_of_time.Image.Tag = "song_of_time";
            pictureBox_song_of_storms.Image.Tag = "song_of_storms";
            pictureBox_minuet.Image.Tag = "minuet";
            pictureBox_bolero.Image.Tag = "bolero";
            pictureBox_serenade.Image.Tag = "serenade";
            pictureBox_requiem.Image.Tag = "requiem";
            pictureBox_nocturne.Image.Tag = "nocturne";
            pictureBox_prelude.Image.Tag = "prelude";

            //medaillons
            pictureBox_green_medaillon.Image.Tag = "green_medaillon";
            pictureBox_red_medaillon.Image.Tag = "red_medaillon";
            pictureBox_blue_medaillon.Image.Tag = "blue_medaillon";
            pictureBox_orange_medaillon.Image.Tag = "orange_medaillon";
            pictureBox_purple_medaillon.Image.Tag = "purple_medaillon";
            pictureBox_yellow_medaillon.Image.Tag = "yellow_medaillon";
        }
        private void loadComboBoxData(String[] data)
        {
            comboBox_woth1.Items.Clear();
            comboBox_woth1.Items.AddRange(data);

            comboBox_woth2.Items.Clear();
            comboBox_woth2.Items.AddRange(data);

            comboBox_woth3.Items.Clear();
            comboBox_woth3.Items.AddRange(data);

            comboBox_woth4.Items.Clear();
            comboBox_woth4.Items.AddRange(data);

            comboBox_woth5.Items.Clear();
            comboBox_woth5.Items.AddRange(data);

            comboBox_barren1.Items.Clear();
            comboBox_barren1.Items.AddRange(data);

            comboBox_barren2.Items.Clear();
            comboBox_barren2.Items.AddRange(data);

            comboBox_barren3.Items.Clear();
            comboBox_barren3.Items.AddRange(data);
        }

        #region combobox DropDownClosed
        void comboBox_woth1_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_woth1.Select(0, 0); }));
            if (comboBox_woth1.SelectedIndex <= 0) label_woth1.Text = "0"; else label_woth1.Text = "1";
        }

        void comboBox_woth2_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_woth2.Select(0, 0); }));
            if (comboBox_woth2.SelectedIndex <= 0) label_woth2.Text = "0"; else label_woth2.Text = "1";
        }

        void comboBox_woth3_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_woth3.Select(0, 0); }));
            if (comboBox_woth3.SelectedIndex <= 0) label_woth3.Text = "0"; else label_woth3.Text = "1";
        }

        void comboBox_woth4_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_woth4.Select(0, 0); }));
            if (comboBox_woth4.SelectedIndex <= 0) label_woth4.Text = "0"; else label_woth4.Text = "1";
        }

        void comboBox_woth5_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_woth5.Select(0, 0); }));
            if (comboBox_woth5.SelectedIndex <= 0) label_woth5.Text = "0"; else label_woth5.Text = "1";
        }

        void comboBox_barren_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { ((ComboBox)sender).Select(0, 0); }));
        }
        #endregion

        #region dead click
        private bool eval_dead_hint(bool isDead, PictureBox picture)
        {
            if (isDead) picture.Image = Properties.Resources.gossip_stone_2;
            else picture.Image = Properties.Resources.dead;
            return !isDead;
            
        }
        private void button_dead30skulls_Click(object sender, EventArgs e)
        {
            dead30skulls = eval_dead_hint(dead30skulls, pictureBox_30skulls);
        }

        private void button_dead40skulls_Click(object sender, EventArgs e)
        {
            dead40skulls = eval_dead_hint(dead40skulls, pictureBox_40skulls);
        }

        private void button_dead50skulls_Click(object sender, EventArgs e)
        {
            dead50skulls = eval_dead_hint(dead50skulls, pictureBox_50skulls);
        }

        private void button_deadSkullMask_Click(object sender, EventArgs e)
        {
            deadSkullMask = eval_dead_hint(deadSkullMask, pictureBox_skullMask);
        }

        private void button_deadBiggoron_Click(object sender, EventArgs e)
        {
            deadBiggoron = eval_dead_hint(deadBiggoron, pictureBox_biggoron);
        }

        private void button_deadFrogs_Click(object sender, EventArgs e)
        {
            deadFrogs = eval_dead_hint(deadFrogs, pictureBox_frogs);
        }
        #endregion

        #region chrono and timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan time = chrono.Elapsed;
            label1.Text = time.ToString(@"hh\:mm\:ss\.ff");
        }

        private void button_chrono_Click(object sender, EventArgs e)
        {
            if (chronoRunning)
            {
                chrono.Stop();
                button_chrono.Text = "Start";
            }
            else
            {
                chrono.Start();
                button_chrono.Text = "Stop";
            }

            chronoRunning = !chronoRunning;
        }

        private void button_chrono_reset_Click(object sender, EventArgs e)
        {
            chronoRunning = false;
            chrono.Reset();
            button_chrono.Text = "Start";
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if(alphabeticalOrder)
            {
                loadComboBoxData(placesByCategories.ToArray());
                button1.Text = "Order : Categories";
            }
            else
            {
                loadComboBoxData(placesAlphabeticalOrder.ToArray());
                button1.Text = "Order : Alphabetical";
            }

            alphabeticalOrder = !alphabeticalOrder;
        }

        #region DragEnter
        private void object_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }
        #endregion

        #region DragDrop
        private void panel_DragDrop(object sender, DragEventArgs e)
        {
            var coord = new Point();
            var panel = (Panel)sender;

            for (int i = 0; i < panel.Controls.Count; i++)
            {
                coord = new Point(panel.Controls[i].Location.X + 32, panel.Controls[i].Location.Y);
            }

            var image = new PictureBox();
            var tag = ((Bitmap)e.Data.GetData(DataFormats.Bitmap)).Tag.ToString().Replace("_bw", "");
            image.Size = new Size(32, 32);
            image.Image = (Image)Properties.Resources.ResourceManager.GetObject(tag);
            image.Tag = tag;
            image.Location = coord;
            
            image.MouseClick += new MouseEventHandler(imageWoth_RightClick);

            panel.Controls.Add(image);
        }
        private void imageWoth_RightClick(object sender, MouseEventArgs e)
        {
            var image = ((PictureBox)sender);
            if (e.Button == MouseButtons.Right)
                image.Parent.Controls.Remove(image);
        }

        private void pictureBox_DragDrop(object sender, DragEventArgs e)
        {
            var imageTag = ((Bitmap)e.Data.GetData(DataFormats.Bitmap)).Tag.ToString().Replace("_bw", "");
            var imageColor = (Image)Properties.Resources.ResourceManager.GetObject(imageTag);
            ((PictureBox)sender).Image = imageColor;
        }
        #endregion

        #region MouseDown
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Clicks != 1)
                isMouseDown = false;
            else isMouseDown = true;
        }
        #endregion

        #region MouseUp
        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            var pBox = (PictureBox)sender;
            var imageTag = pBox.Image.Tag.ToString();
            var imageColor = (Image)Properties.Resources.ResourceManager.GetObject(imageTag);
            var imageBlackWhite = (Image)Properties.Resources.ResourceManager.GetObject(imageTag + "_bw");
            isMouseDown = false;
            
            if (e.Button == MouseButtons.Left)
            {
                pBox.Image = imageColor;
            }
            if (e.Button == MouseButtons.Right)
                pBox.Image = imageBlackWhite;

            pBox.Image.Tag = imageTag;
        }

        private void pictureBox_MouseUp_Upgrade(PictureBox pBox, MouseButtons mouseButton, List<Image> listUpgrade)
        {
            var imageTag = pBox.Image.Tag.ToString();
            isMouseDown = false;

            if (mouseButton == MouseButtons.Left)
            {
                var index = listUpgrade.IndexOf(pBox.Image);
                if (index == listUpgrade.Count - 1)
                    pBox.Image = listUpgrade[0];
                else pBox.Image = listUpgrade[index + 1];
            }
            if (mouseButton == MouseButtons.Right)
            {
                var index = listUpgrade.IndexOf(pBox.Image);
                if (index == 0)
                    pBox.Image = listUpgrade[listUpgrade.Count - 1];
                else pBox.Image = listUpgrade[index - 1];
            }
            pBox.Image.Tag = imageTag;
        }

        private void pictureBox_bottle_MouseUp_Upgrade(object sender, MouseEventArgs e)
        {
            pictureBox_MouseUp_Upgrade((PictureBox)sender, e.Button, bottleUpgrade); 
        }

        private void pictureBox_strength_MouseUp_Upgrade(object sender, MouseEventArgs e)
        {
            pictureBox_MouseUp_Upgrade((PictureBox)sender, e.Button, strengthUpgrade);
        }

        private void pictureBox_scale_MouseUp_Upgrade(object sender, MouseEventArgs e)
        {
            pictureBox_MouseUp_Upgrade((PictureBox)sender, e.Button, scaleUpgrade);
        }

        private void pictureBox_ocarina_MouseUp_Upgrade(object sender, MouseEventArgs e)
        {
            pictureBox_MouseUp_Upgrade((PictureBox)sender, e.Button, ocarinaUpgrade);
        }

        private void pictureBox_hookshot_MouseUp_Upgrade(object sender, MouseEventArgs e)
        {
            pictureBox_MouseUp_Upgrade((PictureBox)sender, e.Button, hookUpgrade);
        }

        private void pictureBox_magic_MouseUp_Upgrade(object sender, MouseEventArgs e)
        {
            pictureBox_MouseUp_Upgrade((PictureBox)sender, e.Button, magicUpgrade);
        }

        private void pictureBox_wallet_MouseUp_Upgrade(object sender, MouseEventArgs e)
        {
            pictureBox_MouseUp_Upgrade((PictureBox)sender, e.Button, walletUpgrade);
        }
        private void pictureBox_biggoronQuest_MouseUp_Upgrade(object sender, MouseEventArgs e)
        {
            pictureBox_MouseUp_Upgrade((PictureBox)sender, e.Button, biggoronQuestUpgrade);
        }
        private void pictureBox_maskQuest_MouseUp_Upgrade(object sender, MouseEventArgs e)
        {
            pictureBox_MouseUp_Upgrade((PictureBox)sender, e.Button, maskQuestUpgrade);
        }

        private void medaillons_MouseUp(PictureBox pBox, MouseButtons mouseButton, ref bool isColored, Label label_medaillon)
        {
            var imageTag = pBox.Image.Tag.ToString();
            var imageColor = (Image)Properties.Resources.ResourceManager.GetObject(imageTag);
            var imageBlackWhite = (Image)Properties.Resources.ResourceManager.GetObject(imageTag + "_bw");
            isMouseDown = false;

            if (mouseButton == MouseButtons.Left)
            {
                if (isColored) pBox.Image = imageBlackWhite;
                else pBox.Image = imageColor;
                isColored = !isColored;
            }
            if (mouseButton == MouseButtons.Right)
            {
                var index = listDungeons.IndexOf(label_medaillon.Text);
                if (index == listDungeons.Count-1)
                    label_medaillon.Text = listDungeons[0];
                else label_medaillon.Text = listDungeons[index + 1];
            }

            pBox.Image.Tag = imageTag;
        }
        #endregion

        #region LabelMouseClick
        private void label_woth_MouseClick(object sender, MouseEventArgs e)
        {
            var label = (Label)sender;
            if(e.Button == MouseButtons.Left)
                label.Text = (Convert.ToInt32(label.Text) + 1).ToString();
            if(e.Button == MouseButtons.Right && (Convert.ToInt32(label.Text) > 0))
                label.Text = (Convert.ToInt32(label.Text) - 1).ToString();
        }
        #endregion

        #region MouseMove for DragAndDrop
        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var pBox = (PictureBox)sender;
            if (e.Button == MouseButtons.Left && isMouseDown)
            {
                pBox.DoDragDrop(pBox.Image, DragDropEffects.Copy);
                isMouseDown = false;
            }
        }


        #endregion

        private void pictureBox_medaillons_green_MouseUp(object sender, MouseEventArgs e)
        {
            medaillons_MouseUp((PictureBox)sender, e.Button, ref isColoredGreenMedaillon, label_green_medaillon);
        }
        private void pictureBox_medaillons_red_MouseUp(object sender, MouseEventArgs e)
        {
            medaillons_MouseUp((PictureBox)sender, e.Button, ref isColoredRedMedaillon, label_red_medaillon);
        }
        private void pictureBox_medaillons_blue_MouseUp(object sender, MouseEventArgs e)
        {
            medaillons_MouseUp((PictureBox)sender, e.Button, ref isColoredBlueMedaillon, label_blue_medaillon);
        }
        private void pictureBox_medaillons_orange_MouseUp(object sender, MouseEventArgs e)
        {
            medaillons_MouseUp((PictureBox)sender, e.Button, ref isColoredOrangeMedaillon, label_orange_medaillon);
        }
        private void pictureBox_medaillons_purple_MouseUp(object sender, MouseEventArgs e)
        {
            medaillons_MouseUp((PictureBox)sender, e.Button, ref isColoredPurpleMedaillon, label_purple_medaillon);
        }
        private void pictureBox_medaillons_yellow_MouseUp(object sender, MouseEventArgs e)
        {
            medaillons_MouseUp((PictureBox)sender, e.Button, ref isColoredYellowMedaillon, label_yellow_medaillon);
        }
        private void pictureBox_stones_kokiri_MouseUp(object sender, MouseEventArgs e)
        {
            medaillons_MouseUp((PictureBox)sender, e.Button, ref isColoredKokiriStone, label_kokiri_stone);
        }
        private void pictureBox_stones_goron_MouseUp(object sender, MouseEventArgs e)
        {
            medaillons_MouseUp((PictureBox)sender, e.Button, ref isColoredGoronStone, label_goron_stone);
        }
        private void pictureBox_stones_zora_MouseUp(object sender, MouseEventArgs e)
        {
            medaillons_MouseUp((PictureBox)sender, e.Button, ref isColoredZoraStone, label_zora_stone);
        }
    }
}
