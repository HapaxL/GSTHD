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
        //row 1
        List<Image> ListImage_Slingshot = new List<Image>();
        List<Image> ListImage_Bomb = new List<Image>();
        List<Image> ListImage_Bombchu = new List<Image>();
        List<Image> ListImage_Hookshot = new List<Image>();
        List<Image> ListImage_Bow = new List<Image>();
        List<Image> ListImage_Arrow = new List<Image>();
        List<Image> ListImage_Spell = new List<Image>();
        List<Image> ListImage_Magic = new List<Image>();
        List<Image> ListImage_Boots = new List<Image>();
        List<Image> ListImage_BiggoronQuest = new List<Image>();


        //row 2
        List<Image> ListImage_Boomerang = new List<Image>();
        List<Image> ListImage_Scale = new List<Image>();
        List<Image> ListImage_Strength = new List<Image>();
        List<Image> ListImage_Lens = new List<Image>();
        List<Image> ListImage_Hammer = new List<Image>();
        List<Image> ListImage_Tunic = new List<Image>();
        List<Image> ListImage_Wallet = new List<Image>();
        List<Image> ListImage_RutosLetter = new List<Image>();
        List<Image> ListImage_MirrorShield = new List<Image>();

        //Songs
        List<Image> ListImage_TinySongs = new List<Image>();
        List<Image> ListImage_ZeldasLullaby = new List<Image>();
        List<Image> ListImage_EponasSong = new List<Image>();
        List<Image> ListImage_SariasSong = new List<Image>();
        List<Image> ListImage_SunsSong = new List<Image>();
        List<Image> ListImage_SongOfTime = new List<Image>();
        List<Image> ListImage_SongOfStorms = new List<Image>();

        List<Image> ListImage_Minuet = new List<Image>();
        List<Image> ListImage_Bolero = new List<Image>();
        List<Image> ListImage_Serenade = new List<Image>();
        List<Image> ListImage_Nocturne = new List<Image>();
        List<Image> ListImage_Requiem = new List<Image>();
        List<Image> ListImage_Prelude = new List<Image>();

        //Medallions
        List<Image> ListImage_GreenMedallion = new List<Image>();
        List<Image> ListImage_RedMedallion = new List<Image>();
        List<Image> ListImage_BlueMedallion = new List<Image>();
        List<Image> ListImage_PurpleMedallion = new List<Image>();
        List<Image> ListImage_OrangeMedallion = new List<Image>();
        List<Image> ListImage_YellowMedallion = new List<Image>();
        //Stones
        List<Image> ListImage_KokiriStone = new List<Image>();
        List<Image> ListImage_GoronStone = new List<Image>();
        List<Image> ListImage_ZoraStone = new List<Image>();

        //GoMode
        List<Image> ListImage_GoMode = new List<Image>();

        List<Image> ListImage_GuaranteedHintsOption = new List<Image>();
        List<Image> ListImage_SometimesHintOption = new List<Image>();
        List<Image> ListImage_WothItemsOption = new List<Image>();

        private void setListUpgrade()
        {
            //row 1
            ListImage_Slingshot = new List<Image>
            {
                Properties.Resources.slingshot_bw,
                Properties.Resources.slingshot,
            };

            ListImage_Bomb = new List<Image>
            {
                Properties.Resources.bombs_bw,
                Properties.Resources.bombs,
            };

            ListImage_Bombchu = new List<Image>
            {
                Properties.Resources.bomb_chu_bw,
                Properties.Resources.bomb_chu,
            };

            ListImage_Hookshot = new List<Image>
            {
                Properties.Resources.hookshot_bw,
                Properties.Resources.hookshot,
                Properties.Resources.longshot
            };

            ListImage_Bow = new List<Image>
            {
                Properties.Resources.bow_bw,
                Properties.Resources.bow
            };

            ListImage_Arrow = new List<Image>
            {
                Properties.Resources.fire_light_arrow_bw,
                Properties.Resources.half_fire_arrow,
                Properties.Resources.half_light_arrow,
                Properties.Resources.fire_light_arrow,
                Properties.Resources.fire_arrow,
                Properties.Resources.light_arrow
            };

            ListImage_Spell = new List<Image>
            {
                Properties.Resources.dins_farores_bw,
                Properties.Resources.half_dins_fire,
                Properties.Resources.half_farores_wind,
                Properties.Resources.dins_farores,
                Properties.Resources.dins_fire,
                Properties.Resources.farores_wind
            };

            ListImage_Magic = new List<Image>
            {
                Properties.Resources.magic_bw,
                Properties.Resources.magic,
                Properties.Resources.double_magic
            };

            ListImage_Boots = new List<Image>
            {
                Properties.Resources.iron_hover_boots_bw,
                Properties.Resources.half_iron_boots,
                Properties.Resources.half_hover_boots,
                Properties.Resources.iron_hover_boots,
                Properties.Resources.iron_boots,
                Properties.Resources.hover_boots
            };

            ListImage_BiggoronQuest = new List<Image>
            {
                Properties.Resources.prescription_bw,
                Properties.Resources.prescription,
                Properties.Resources.kz_frog,
                Properties.Resources.eye_drops,
                Properties.Resources.claim_check
            };

            //row 2
            ListImage_Boomerang = new List<Image>
            {
                Properties.Resources.boomerang_bw,
                Properties.Resources.boomerang
            };

            ListImage_Scale = new List<Image>
            {
                Properties.Resources.scale_bw,
                Properties.Resources.scale
            };

            ListImage_Strength = new List<Image>
            {
                Properties.Resources.strength_bw,
                Properties.Resources.strength,
                Properties.Resources.strength2,
                Properties.Resources.strength3
            };

            ListImage_Lens = new List<Image>
            {
                Properties.Resources.lens_bw,
                Properties.Resources.lens
            };

            ListImage_Hammer = new List<Image>
            {
                Properties.Resources.hammer_bw,
                Properties.Resources.hammer
            };

            ListImage_Tunic = new List<Image>
            {
                Properties.Resources.goron_zora_tunic_bw,
                Properties.Resources.half_goron_tunic,
                Properties.Resources.half_zora_tunic,
                Properties.Resources.goron_zora_tunic,
                Properties.Resources.goron_tunic,
                Properties.Resources.zora_tunic
            };

            ListImage_Wallet = new List<Image>
            {
                Properties.Resources.wallet,
                Properties.Resources.wallet2,
                Properties.Resources.wallet3
            };

            ListImage_RutosLetter = new List<Image>
            {
                Properties.Resources.bottle_rutos_letter_bw,
                Properties.Resources.bottle_rutos_letter
            };

            ListImage_MirrorShield = new List<Image>
            {
                Properties.Resources.mirror_shield_bw,
                Properties.Resources.mirror_shield
            };

            //Songs
            ListImage_TinySongs = new List<Image>
            {
                Properties.Resources.no_song,
                Properties.Resources.check_song
            };

            ListImage_ZeldasLullaby = new List<Image>
            {
                Properties.Resources.zeldas_lullaby_bw,
                Properties.Resources.zeldas_lullaby
            };

            ListImage_EponasSong = new List<Image>
            {
                Properties.Resources.epona_bw,
                Properties.Resources.epona
            };

            ListImage_SariasSong = new List<Image>
            {
                Properties.Resources.saria_bw,
                Properties.Resources.saria
            };

            ListImage_SunsSong = new List<Image>
            {
                Properties.Resources.suns_song_bw,
                Properties.Resources.suns_song
            };

            ListImage_SongOfTime = new List<Image>
            {
                Properties.Resources.song_of_time_bw,
                Properties.Resources.song_of_time
            };

            ListImage_SongOfStorms = new List<Image>
            {
                Properties.Resources.song_of_storms_bw,
                Properties.Resources.song_of_storms
            };

            ListImage_Minuet = new List<Image>
            {
                Properties.Resources.minuet_bw,
                Properties.Resources.minuet
            };

            ListImage_Bolero = new List<Image>
            {
                Properties.Resources.bolero_bw,
                Properties.Resources.bolero
            };

            ListImage_Serenade = new List<Image>
            {
                Properties.Resources.serenade_bw,
                Properties.Resources.serenade
            };

            ListImage_Nocturne = new List<Image>
            {
                Properties.Resources.nocturne_bw,
                Properties.Resources.nocturne
            };

            ListImage_Requiem = new List<Image>
            {
                Properties.Resources.requiem_bw,
                Properties.Resources.requiem
            };

            ListImage_Prelude = new List<Image>
            {
                Properties.Resources.prelude_bw,
                Properties.Resources.prelude
            };

            //Medallions
            ListImage_GreenMedallion = new List<Image>
            {
                Properties.Resources.green_medaillon_bw,
                Properties.Resources.green_medaillon
            };

            ListImage_RedMedallion = new List<Image>
            {
                Properties.Resources.red_medaillon_bw,
                Properties.Resources.red_medaillon
            };

            ListImage_BlueMedallion = new List<Image>
            {
                Properties.Resources.blue_medaillon_bw,
                Properties.Resources.blue_medaillon
            };

            ListImage_PurpleMedallion = new List<Image>
            {
                Properties.Resources.purple_medaillon_bw,
                Properties.Resources.purple_medaillon
            };

            ListImage_OrangeMedallion = new List<Image>
            {
                Properties.Resources.orange_medaillon_bw,
                Properties.Resources.orange_medaillon
            };

            ListImage_YellowMedallion = new List<Image>
            {
                Properties.Resources.yellow_medaillon_bw,
                Properties.Resources.yellow_medaillon,
            };
            //Stones
            ListImage_KokiriStone = new List<Image>
            {
                Properties.Resources.kokiri_stone_bw,
                Properties.Resources.kokiri_stone,
            };

            ListImage_GoronStone = new List<Image>
            {
                Properties.Resources.goron_stone_bw,
                Properties.Resources.goron_stone
            };

            ListImage_ZoraStone = new List<Image>
            {
                Properties.Resources.zora_stone_bw,
                Properties.Resources.zora_stone
            };

            //Go Mode
            ListImage_GoMode = new List<Image>
            {
                Properties.Resources.go_mode_bw,
                Properties.Resources.go_mode
            };

            //Guaranteed Hints Option
            ListImage_GuaranteedHintsOption = new List<Image>
            {
                Properties.Resources.gossip_stone1,
                Properties.Resources.sold_out,
                Properties.Resources.bottle_empty,
                Properties.Resources.bottle_big_poe
            };

            //WotH Items Options
            ListImage_WothItemsOption = new List<Image>
            {
                Properties.Resources.gossip_stone1,
                Properties.Resources.bottle_empty,
                Properties.Resources.bottle_big_poe
            };

            //Sometimes Hints Option
            ListImage_SometimesHintOption = new List<Image>
            {
                Properties.Resources.gossip_stone1,
                Properties.Resources.sold_out,
                Properties.Resources.key,
                Properties.Resources.bk,
                Properties.Resources.bottle_empty,
                Properties.Resources.bottle_big_poe
            };
        }
    }
}
