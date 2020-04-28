using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerOOT
{
    public partial class Form1 : Form
    {
        String[] places = 
        {
            "",
            //"===== HYRULE =====",
            "Hyrule Field",
            "Hyrule Castle",
            "Market",
            "Lon Lon Ranch",
            "Temple of Time",
            //"===== FOREST =====",
            "Kokiri Forest",
            "Lost Woods",            
            "Sacred Forest Meadow",
            "Deku Tree",
            "Forest Temple",            
            //"===== MOUNTAIN =====",
            "Kakariko",
            "Bottom of the Well",
            "Graveyard",
            "Goron City",
            "Death Mountain Trails",
            "Death Mountain Crater",
            "Dodongo's Cavern",
            "Fire Temple",
            "Shadow Temple",
            //"===== ZORA =====",
            "Zora's River",
            "Zora's Domain",
            "Zora's Fountain",            
            "Ice Cavern",
            "Lake Hylia",
            "Jabu Jabu's Belly",
            "Water Temple",
            //"===== DESERT =====",
            "Gerudo Valley",
            "Gerudo Fortress",
            "Gerudo Training Grounds",
            "Haunted Wasteland",
            "Desert Colossus",
            "Spirit Temple",
            //"===== GANON =====",
            "Outside Ganon's Castle",
            "Ganon's Castle"
        };

        String[] songs = {
            "Zelda's Lullaby",
            "Epona's Song",
            "Saria's Song",
            "Sun's Song",
            "Song of Time",
            "Song of Storms",
            "Minuet of Forest",
            "Bolero of Fire",
            "Serenade of Water",
            "Requiem of Spirit",
            "Nocturne of Shadow",
            "Prelude of Light"
        };

        bool dead30skulls = false;
        bool dead40skulls = false;
        bool dead50skulls = false;
        bool deadSkullMask = false;
        bool deadBiggoron = false;
        bool deadFrogs = false;

        public Form1()
        {
            InitializeComponent();

            Array.Sort(places);
            comboBox_woth1.Items.AddRange(places);
            comboBox_woth2.Items.AddRange(places);
            comboBox_woth3.Items.AddRange(places);
            comboBox_woth4.Items.AddRange(places);
            comboBox_woth5.Items.AddRange(places);

            comboBox_barren1.Items.AddRange(places);
            comboBox_barren2.Items.AddRange(places);
            comboBox_barren3.Items.AddRange(places);

            comboBox_oot.Items.AddRange(songs);

            autocomplete();
        }

        private void autocomplete()
        {
            //Textbox AutoComplete
            var source = new AutoCompleteStringCollection();
            foreach (String place in places)
            {
                source.Add(place);
            }

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

        }

        void comboBox_woth1_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_woth1.Select(0, 0); }));
        }

        void comboBox_woth2_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_woth2.Select(0, 0); }));
        }

        void comboBox_woth3_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_woth3.Select(0, 0); }));
        }

        void comboBox_woth4_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_woth4.Select(0, 0); }));
        }

        void comboBox_woth5_DropDownClosed(object sender, EventArgs e)
        {
            this.BeginInvoke(new Action(() => { comboBox_woth5.Select(0, 0); }));
        }

        private void button_dead30skulls_Click(object sender, EventArgs e)
        {
            textBox_30skulls.Enabled = dead30skulls;
            if (textBox_30skulls.Enabled)
            {
                textBox_30skulls.BackColor = Color.Khaki;
                textBox_30skulls.Text = string.Empty;
            }
            else
            {
                textBox_30skulls.BackColor = Color.Empty;
                textBox_30skulls.Text = "X";
            }

            dead30skulls = !dead30skulls;
        }

        private void button_dead40skulls_Click(object sender, EventArgs e)
        {
            textBox_40skulls.Enabled = dead40skulls;
            if (textBox_40skulls.Enabled)
            {
                textBox_40skulls.BackColor = Color.Khaki;
                textBox_40skulls.Text = string.Empty;
            }
            else
            {
                textBox_40skulls.BackColor = Color.Empty;
                textBox_40skulls.Text = "X";
            }

            dead40skulls = !dead40skulls;
        }

        private void button_dead50skulls_Click(object sender, EventArgs e)
        {
            textBox_50skulls.Enabled = dead50skulls;
            if (textBox_50skulls.Enabled)
            {
                textBox_50skulls.BackColor = Color.Khaki;
                textBox_50skulls.Text = string.Empty;
            }
            else
            {
                textBox_50skulls.BackColor = Color.Empty;
                textBox_50skulls.Text = "X";
            }

            dead50skulls = !dead50skulls;
        }

        private void button_deadSkullMask_Click(object sender, EventArgs e)
        {
            textBox_skullMask.Enabled = deadSkullMask;
            if (textBox_skullMask.Enabled)
            {
                textBox_skullMask.BackColor = Color.Khaki;
                textBox_skullMask.Text = string.Empty;
            }
            else
            {
                textBox_skullMask.BackColor = Color.Empty;
                textBox_skullMask.Text = "X";
            }

            deadSkullMask = !deadSkullMask;
        }

        private void button_deadBiggoron_Click(object sender, EventArgs e)
        {
            textBox_biggoron.Enabled = deadBiggoron;
            if (textBox_biggoron.Enabled)
            {
                textBox_biggoron.BackColor = Color.Khaki;
                textBox_biggoron.Text = string.Empty;
            }
            else
            {
                textBox_biggoron.BackColor = Color.Empty;
                textBox_biggoron.Text = "X";
            }

            deadBiggoron = !deadBiggoron;
        }

        private void button_deadFrogs_Click(object sender, EventArgs e)
        {
            textBox_frogs.Enabled = deadFrogs;
            if (textBox_frogs.Enabled)
            {
                textBox_frogs.BackColor = Color.Khaki;
                textBox_frogs.Text = string.Empty;
            }
            else
            {
                textBox_frogs.BackColor = Color.Empty;
                textBox_frogs.Text = "X";
            }

            deadFrogs = !deadFrogs;
        }
    }
}
