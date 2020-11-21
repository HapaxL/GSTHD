using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerOOT
{
    public partial class Form1 : Form
    {
        TabControl tabControl = new TabControl();
        TabPage page_Tracker;
        TabPage page_KokiriForest;
        TabPage page_DekuTree;
        TabPage page_HyruleField;
        TabPage page_HyruleCastle;
        TabPage page_RanchLonLon;
        TabPage page_LostWoods;
        TabPage page_SacredForestMeadow;
        TabPage page_GerudoValley;
        TabPage page_Kakariko;
        TabPage page_Graveyard;
        TabPage page_DeathMountainTrails;
        TabPage page_DeathMountainCrater;
        TabPage page_GoronCity;
        TabPage page_DodongosCavern;
        TabPage page_ZorasRiver;
        TabPage page_ZorasDomain;
        TabPage page_ZorasFontain;
        TabPage page_LakeHylia;
        TabPage page_Jabu;
        TabPage page_ForestTemple;
        TabPage page_FireTemple;
        TabPage page_WaterTemple;
        TabPage page_IceCavern;
        TabPage page_BottomOfTheWell;
        TabPage page_ShadowTemple;
        TabPage page_GerudoFortress;
        TabPage page_HauntedWasteland;
        TabPage page_ColossusDesert;
        TabPage page_SpiritTemple;
        TabPage page_GanonCastle;

        private void LoadTabControl()
        {
            page_Tracker = new TabPage
            {
                Name = "Tracker",
                Text = "Tracker",
                BackColor = Color.FromArgb(29, 29, 29),
                Size = new Size(370, 710)
            };
            tabControl.Controls.Add(page_Tracker);

            LoadTabKokiriForest();

            page_DekuTree = new TabPage
            {
                Name = "Deku Tree",
                Text = "Deku Tree",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_DekuTree);

            page_HyruleField = new TabPage
            {
                Name = "Hyrule Field",
                Text = "Hyrule Field",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_HyruleField);

            page_HyruleCastle = new TabPage
            {
                Name = "Hyrule Castle",
                Text = "Hyrule Castle",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_HyruleCastle);

            page_RanchLonLon = new TabPage
            {
                Name = "Ranch LonLon",
                Text = "Ranch LonLon",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_RanchLonLon);

            page_LostWoods = new TabPage
            {
                Name = "Lost Woods",
                Text = "Lost Woods",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_LostWoods);

            page_SacredForestMeadow = new TabPage
            {
                Name = "Sacred Forest Meadow",
                Text = "Sacred Forest Meadow",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_SacredForestMeadow);

            page_GerudoValley = new TabPage
            {
                Name = "Gerudo Valley",
                Text = "Gerudo Valley",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_GerudoValley);

            page_Kakariko = new TabPage
            {
                Name = "Kakariko",
                Text = "Kakariko",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_Kakariko);

            page_Graveyard = new TabPage
            {
                Name = "Graveyard",
                Text = "Graveyard",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_Graveyard);

            page_DeathMountainTrails = new TabPage
            {
                Name = "Death Mountain Trails",
                Text = "Death Mountain Trails",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_DeathMountainTrails);

            page_DeathMountainCrater = new TabPage
            {
                Name = "Death Mountain Crater",
                Text = "Death Mountain Crater",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_DeathMountainCrater);

            page_GoronCity = new TabPage
            {
                Name = "Goron City",
                Text = "Goron City",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_GoronCity);

            page_DodongosCavern = new TabPage
            {
                Name = "Dodongo's Cavern",
                Text = "Dodongo's Cavern",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_DodongosCavern);

            page_ZorasRiver = new TabPage
            {
                Name = "Zora's River",
                Text = "Zora's River",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_ZorasRiver);

            page_ZorasDomain = new TabPage
            {
                Name = "Zora's Domain",
                Text = "Zora's Domain",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_ZorasDomain);

            page_ZorasFontain = new TabPage
            {
                Name = "Zora's Fontain",
                Text = "Zora's Fontain",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_ZorasFontain);

            page_LakeHylia = new TabPage
            {
                Name = "Lake Hylia",
                Text = "Lake Hylia",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_LakeHylia);

            page_Jabu = new TabPage
            {
                Name = "Jabu",
                Text = "Jabu",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_Jabu);

            page_ForestTemple = new TabPage
            {
                Name = "Forest Temple",
                Text = "Forest Temple",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_ForestTemple);

            page_FireTemple = new TabPage
            {
                Name = "Fire Temple",
                Text = "Fire Temple",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_FireTemple);

            page_WaterTemple = new TabPage
            {
                Name = "Water Temple",
                Text = "Water Temple",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_WaterTemple);

            page_IceCavern = new TabPage
            {
                Name = "Ice Cavern",
                Text = "Ice Cavern",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_IceCavern);

            page_BottomOfTheWell = new TabPage
            {
                Name = "Bottom Of The Well",
                Text = "Bottom Of The Well",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_BottomOfTheWell);

            page_ShadowTemple = new TabPage
            {
                Name = "Shadow Temple",
                Text = "Shadow Temple",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_ShadowTemple);

            page_GerudoFortress = new TabPage
            {
                Name = "Gerudo Fortress",
                Text = "Gerudo Fortress",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_GerudoFortress);

            page_HauntedWasteland = new TabPage
            {
                Name = "Haunted Wasteland",
                Text = "Haunted Wasteland",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_HauntedWasteland);

            page_ColossusDesert = new TabPage
            {
                Name = "Colossus Desert",
                Text = "Colossus Desert",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_ColossusDesert);

            page_SpiritTemple = new TabPage
            {
                Name = "Spirit Temple",
                Text = "Spirit Temple",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_SpiritTemple);

            page_GanonCastle = new TabPage
            {
                Name = "Ganon Castle",
                Text = "Ganon Castle",
                BackColor = Color.FromArgb(29, 29, 29)
            };
            tabControl.Controls.Add(page_GanonCastle);

            

            tabControl.Location = new Point(0, 0);
            tabControl.Size = new Size(370, 710);
            this.Controls.Add(tabControl);
        }

        private void LoadTabKokiriForest()
        {
            int variableY = 6;

            page_KokiriForest = new TabPage
            {
                Name = "Kokiri Forest",
                Text = "Kokiri Forest",
                BackColor = Color.FromArgb(29, 29, 29)
            };

            Label LobbyChestLabel = new Label
            {
                Text = "Lobby Chest :",
                Location = new Point(2, 10),
                Font = new Font("Consolas", 10, FontStyle.Bold),
                ForeColor = Color.White,
                //Size = new Size(120, 23)
                AutoSize = true
            };
            page_KokiriForest.Controls.Add(LobbyChestLabel);

            GossipStone LobbyChestStone = new GossipStone("LobbyChest", ListImage_Chests, LobbyChestLabel.Location.X + LobbyChestLabel.Width, LobbyChestLabel.Location.Y - variableY);
            page_KokiriForest.Controls.Add(LobbyChestStone);

            Label CompassChestLabel = new Label
            {
                Text = "Compass Chest :",
                Location = new Point(LobbyChestLabel.Location.X, LobbyChestStone.Location.Y + LobbyChestStone.Height + variableY + 2),
                Font = new Font("Consolas", 10, FontStyle.Bold),
                ForeColor = Color.White,
                //Size = new Size(120, 23)
                AutoSize = true
            };
            page_KokiriForest.Controls.Add(CompassChestLabel);

            GossipStone CompassChestStone = new GossipStone("LobbyChest", ListImage_Chests, CompassChestLabel.Location.X + CompassChestLabel.Width, CompassChestLabel.Location.Y - variableY);
            page_KokiriForest.Controls.Add(CompassChestStone);

            Label CompassRoomSideChestLabel = new Label
            {
                Text = "Compass Side Room Chest :",
                Location = new Point(LobbyChestLabel.Location.X, CompassChestStone.Location.Y + CompassChestStone.Height + variableY + 2),
                Font = new Font("Consolas", 10, FontStyle.Bold),
                ForeColor = Color.White,
                //Size = new Size(120, 23),
                AutoSize = true
            };
            page_KokiriForest.Controls.Add(CompassRoomSideChestLabel);

            GossipStone CompassRoomSideChestStone = new GossipStone("LobbyChest", ListImage_Chests, CompassRoomSideChestLabel.Location.X + CompassRoomSideChestLabel.Width, CompassRoomSideChestLabel.Location.Y - variableY);
            page_KokiriForest.Controls.Add(CompassRoomSideChestStone);




            tabControl.Controls.Add(page_KokiriForest);
        }
    }
}
