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

        bool dead30skulls = false;
        bool dead40skulls = false;
        bool dead50skulls = false;
        bool deadSkullMask = false;
        bool deadBiggoron = false;
        bool deadFrogs = false;

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

        void comboBox_barren1_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_barren1.Select(0, 0); }));
        }

        void comboBox_barren2_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_barren2.Select(0, 0); }));
        }

        void comboBox_barren3_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_barren3.Select(0, 0); }));
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
        private void pictureBox_oot_hint_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }
        private void panel_item_woth2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }
        private void panel_item_woth3_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }
        private void panel_item_woth4_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }
        private void panel_item_woth5_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }
        private void pictureBox_30skulls_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void pictureBox_40skulls_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void pictureBox_50skulls_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void pictureBox_skullMask_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void pictureBox_biggoron_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void pictureBox_frogs_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }
        #endregion

        #region DragDrop
        private void imageWoth1_RightClick(object sender, MouseEventArgs e)
        {
            var image = ((PictureBox)sender);
            if (e.Button == MouseButtons.Right)
                panel_item_woth1.Controls.Remove(image);
        }

        private void imageWoth2_RightClick(object sender, MouseEventArgs e)
        {
            var image = ((PictureBox)sender);
            if (e.Button == MouseButtons.Right)
                panel_item_woth2.Controls.Remove(image);
        }
        private void imageWoth3_RightClick(object sender, MouseEventArgs e)
        {
            var image = ((PictureBox)sender);
            if (e.Button == MouseButtons.Right)
                panel_item_woth3.Controls.Remove(image);
        }
        private void imageWoth4_RightClick(object sender, MouseEventArgs e)
        {
            var image = ((PictureBox)sender);
            if (e.Button == MouseButtons.Right)
                panel_item_woth4.Controls.Remove(image);
        }
        private void imageWoth5_RightClick(object sender, MouseEventArgs e)
        {
            var image = ((PictureBox)sender);
            if (e.Button == MouseButtons.Right)
                panel_item_woth5.Controls.Remove(image);
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            var coord = new Point();

            for (int i = 0; i < panel_item_woth1.Controls.Count; i++)
            {
                coord = new Point(panel_item_woth1.Controls[i].Location.X + 32, panel_item_woth1.Controls[i].Location.Y);
            }

            var image = new PictureBox();
            image.Size = new Size(32, 32);
            image.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            image.Location = coord;
            image.MouseClick += new MouseEventHandler(imageWoth1_RightClick);

            panel_item_woth1.Controls.Add(image);
        }
        private void panel_item_woth2_DragDrop(object sender, DragEventArgs e)
        {
            var coord = new Point();

            for (int i = 0; i < panel_item_woth2.Controls.Count; i++)
            {
                coord = new Point(panel_item_woth2.Controls[i].Location.X + 32, panel_item_woth2.Controls[i].Location.Y);
            }

            var image = new PictureBox();
            image.Size = new Size(32, 32);
            image.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            image.Location = coord;
            image.MouseClick += new MouseEventHandler(imageWoth2_RightClick);

            panel_item_woth2.Controls.Add(image);
        }
        private void panel_item_woth3_DragDrop(object sender, DragEventArgs e)
        {
            var coord = new Point();

            for (int i = 0; i < panel_item_woth3.Controls.Count; i++)
            {
                coord = new Point(panel_item_woth3.Controls[i].Location.X + 32, panel_item_woth3.Controls[i].Location.Y);
            }

            var image = new PictureBox();
            image.Size = new Size(32, 32);
            image.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            image.Location = coord;
            image.MouseClick += new MouseEventHandler(imageWoth3_RightClick);

            panel_item_woth3.Controls.Add(image);
        }
        private void panel_item_woth4_DragDrop(object sender, DragEventArgs e)
        {
            var coord = new Point();

            for (int i = 0; i < panel_item_woth4.Controls.Count; i++)
            {
                coord = new Point(panel_item_woth4.Controls[i].Location.X + 32, panel_item_woth4.Controls[i].Location.Y);
            }

            var image = new PictureBox();
            image.Size = new Size(32, 32);
            image.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            image.Location = coord;
            image.MouseClick += new MouseEventHandler(imageWoth4_RightClick);

            panel_item_woth4.Controls.Add(image);
        }
        private void panel_item_woth5_DragDrop(object sender, DragEventArgs e)
        {
            var coord = new Point();

            for (int i = 0; i < panel_item_woth5.Controls.Count; i++)
            {
                coord = new Point(panel_item_woth5.Controls[i].Location.X + 32, panel_item_woth5.Controls[i].Location.Y);
            }

            var image = new PictureBox();
            image.Size = new Size(32, 32);
            image.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
            image.Location = coord;
            image.MouseClick += new MouseEventHandler(imageWoth5_RightClick);

            panel_item_woth5.Controls.Add(image);
        }
        private void pictureBox_oot_hint_DragDrop(object sender, DragEventArgs e)
        {
            pictureBox_oot_hint.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
        }
        private void pictureBox_30skulls_DragDrop(object sender, DragEventArgs e)
        {
            pictureBox_30skulls.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
        }
        private void pictureBox_40skulls_DragDrop(object sender, DragEventArgs e)
        {
            pictureBox_40skulls.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
        }
        private void pictureBox_50skulls_DragDrop(object sender, DragEventArgs e)
        {
            pictureBox_50skulls.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
        }
        private void pictureBox_skullMask_DragDrop(object sender, DragEventArgs e)
        {
            pictureBox_skullMask.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
        }
        private void pictureBox_biggoron_DragDrop(object sender, DragEventArgs e)
        {
            pictureBox_biggoron.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
        }
        private void pictureBox_frogs_DragDrop(object sender, DragEventArgs e)
        {
            pictureBox_frogs.Image = (Bitmap)e.Data.GetData(DataFormats.Bitmap);
        }
        #endregion

        #region MouseDown
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_bow.DoDragDrop(pictureBox_bow.Image, DragDropEffects.Copy);
        }
        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            pictureBox_bombs.DoDragDrop(pictureBox_bombs.Image, DragDropEffects.Copy);
        }
        private void pictureBox_ZL_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_ZL.DoDragDrop(pictureBox_ZL.Image, DragDropEffects.Copy);
        }
        private void pictureBox_epona_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_epona.DoDragDrop(pictureBox_epona.Image, DragDropEffects.Copy);
        }

        private void pictureBox_saria_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_saria.DoDragDrop(pictureBox_saria.Image, DragDropEffects.Copy);
        }

        private void pictureBox_suns_song_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_suns_song.DoDragDrop(pictureBox_suns_song.Image, DragDropEffects.Copy);
        }

        private void pictureBox_song_of_time_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_song_of_time.DoDragDrop(pictureBox_song_of_time.Image, DragDropEffects.Copy);
        }

        private void pictureBox_song_of_storms_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_song_of_storms.DoDragDrop(pictureBox_song_of_storms.Image, DragDropEffects.Copy);
        }

        private void pictureBox_minuet_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_minuet.DoDragDrop(pictureBox_minuet.Image, DragDropEffects.Copy);
        }

        private void pictureBox_bolero_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_bolero.DoDragDrop(pictureBox_bolero.Image, DragDropEffects.Copy);
        }

        private void pictureBox_serenade_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_serenade.DoDragDrop(pictureBox_serenade.Image, DragDropEffects.Copy);
        }

        private void pictureBox_requiem_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_requiem.DoDragDrop(pictureBox_requiem.Image, DragDropEffects.Copy);
        }

        private void pictureBox_nocturne_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_nocturne.DoDragDrop(pictureBox_nocturne.Image, DragDropEffects.Copy);
        }

        private void pictureBox_prelude_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_prelude.DoDragDrop(pictureBox_prelude.Image, DragDropEffects.Copy);
        }

        private void pictureBox_dins_fire_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_dins_fire.DoDragDrop(pictureBox_dins_fire.Image, DragDropEffects.Copy);
        }

        private void pictureBox_farores_wind_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_farores_wind.DoDragDrop(pictureBox_farores_wind.Image, DragDropEffects.Copy);
        }

        private void pictureBox_bchu_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_bchu.DoDragDrop(pictureBox_bchu.Image, DragDropEffects.Copy);
        }

        private void pictureBox_kokiri_shield_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_kokiri_shield.DoDragDrop(pictureBox_kokiri_shield.Image, DragDropEffects.Copy);
        }

        private void pictureBox_hylian_shield_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_hylian_shield.DoDragDrop(pictureBox_hylian_shield.Image, DragDropEffects.Copy);
        }

        private void pictureBox_mirror_shield_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_mirror_shield.DoDragDrop(pictureBox_mirror_shield.Image, DragDropEffects.Copy);
        }

        private void pictureBox_slingshot_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_slingshot.DoDragDrop(pictureBox_slingshot.Image, DragDropEffects.Copy);
        }

        private void pictureBox_hookshot_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_hookshot.DoDragDrop(pictureBox_hookshot.Image, DragDropEffects.Copy);
        }

        private void pictureBox_hammer_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_hammer.DoDragDrop(pictureBox_hammer.Image, DragDropEffects.Copy);
        }

        private void pictureBox_strength_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_strength.DoDragDrop(pictureBox_strength.Image, DragDropEffects.Copy);
        }

        private void pictureBox_lens_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_lens.DoDragDrop(pictureBox_lens.Image, DragDropEffects.Copy);
        }

        private void pictureBox_bgs_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_bgs.DoDragDrop(pictureBox_bgs.Image, DragDropEffects.Copy);
        }

        private void pictureBox_zora_tunic_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_zora_tunic.DoDragDrop(pictureBox_zora_tunic.Image, DragDropEffects.Copy);
        }

        private void pictureBox_goron_tunic_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_goron_tunic.DoDragDrop(pictureBox_goron_tunic.Image, DragDropEffects.Copy);
        }

        private void pictureBox_boomerang_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_boomerang.DoDragDrop(pictureBox_boomerang.Image, DragDropEffects.Copy);
        }

        private void pictureBox_rutos_letter_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_rutos_letter.DoDragDrop(pictureBox_rutos_letter.Image, DragDropEffects.Copy);
        }

        private void pictureBox_big_poe_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_big_poe.DoDragDrop(pictureBox_big_poe.Image, DragDropEffects.Copy);
        }

        private void pictureBox_bottle_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_bottle.DoDragDrop(pictureBox_bottle.Image, DragDropEffects.Copy);
        }

        private void pictureBox_prescription_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_prescription.DoDragDrop(pictureBox_prescription.Image, DragDropEffects.Copy);
        }

        private void pictureBox_hover_boots_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_hover_boots.DoDragDrop(pictureBox_hover_boots.Image, DragDropEffects.Copy);
        }

        private void pictureBox_iron_boots_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_iron_boots.DoDragDrop(pictureBox_iron_boots.Image, DragDropEffects.Copy);
        }

        private void pictureBox_wallet_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_wallet.DoDragDrop(pictureBox_wallet.Image, DragDropEffects.Copy);
        }

        private void pictureBox_fire_arrow_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_fire_arrow.DoDragDrop(pictureBox_fire_arrow.Image, DragDropEffects.Copy);
        }

        private void pictureBox_ice_arrow_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_ice_arrow.DoDragDrop(pictureBox_ice_arrow.Image, DragDropEffects.Copy);
        }

        private void pictureBox_light_arrow_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_light_arrow.DoDragDrop(pictureBox_light_arrow.Image, DragDropEffects.Copy);
        }

        private void pictureBox_scale_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_scale.DoDragDrop(pictureBox_scale.Image, DragDropEffects.Copy);
        }

        private void pictureBox_magic_meter_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox_magic_meter.DoDragDrop(pictureBox_magic_meter.Image, DragDropEffects.Copy);
        }
        #endregion

        #region LabelMouseClick
        private void label_woth1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
                label_woth1.Text = (Convert.ToInt32(label_woth1.Text) + 1).ToString();
            if(e.Button == MouseButtons.Right && (Convert.ToInt32(label_woth1.Text) > 0))
                label_woth1.Text = (Convert.ToInt32(label_woth1.Text) - 1).ToString();
        }

        private void label_woth2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                label_woth2.Text = (Convert.ToInt32(label_woth2.Text) + 1).ToString();
            if (e.Button == MouseButtons.Right && (Convert.ToInt32(label_woth2.Text) > 0))
                label_woth2.Text = (Convert.ToInt32(label_woth2.Text) - 1).ToString();
        }

        private void label_woth3_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                label_woth3.Text = (Convert.ToInt32(label_woth3.Text) + 1).ToString();
            if (e.Button == MouseButtons.Right && (Convert.ToInt32(label_woth3.Text) > 0))
                label_woth3.Text = (Convert.ToInt32(label_woth3.Text) - 1).ToString();
        }

        private void label_woth4_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                label_woth4.Text = (Convert.ToInt32(label_woth4.Text) + 1).ToString();
            if (e.Button == MouseButtons.Right && (Convert.ToInt32(label_woth4.Text) > 0))
                label_woth4.Text = (Convert.ToInt32(label_woth4.Text) - 1).ToString();
        }

        private void label_woth5_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                label_woth5.Text = (Convert.ToInt32(label_woth5.Text) + 1).ToString();
            if (e.Button == MouseButtons.Right && (Convert.ToInt32(label_woth5.Text) > 0))
                label_woth5.Text = (Convert.ToInt32(label_woth5.Text) - 1).ToString();
        }
        #endregion
    }
}
