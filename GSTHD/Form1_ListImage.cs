using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSTHD
{
    public partial class Form1 : Form
    {
        //row 1
        List<string> ListImage_Slingshot = new List<string>();
        List<string> ListImage_Bomb = new List<string>();
        List<string> ListImage_Bombchu = new List<string>();
        List<string> ListImage_Hookshot = new List<string>();
        List<string> ListImage_Bow = new List<string>();
        List<string> ListImage_Arrow = new List<string>();
        List<string> ListImage_Spell = new List<string>();
        List<string> ListImage_Magic = new List<string>();
        List<string> ListImage_Boots = new List<string>();
        List<string> ListImage_BiggoronQuest = new List<string>();


        //row 2
        List<string> ListImage_Boomerang = new List<string>();
        List<string> ListImage_Scale = new List<string>();
        List<string> ListImage_Strength = new List<string>();
        List<string> ListImage_Lens = new List<string>();
        List<string> ListImage_Hammer = new List<string>();
        List<string> ListImage_Tunic = new List<string>();
        List<string> ListImage_Wallet = new List<string>();
        List<string> ListImage_RutosLetter = new List<string>();
        List<string> ListImage_MirrorShield = new List<string>();

        //Songs
        List<string> ListImage_TinySongs = new List<string>();
        List<string> ListImage_ZeldasLullaby = new List<string>();
        List<string> ListImage_EponasSong = new List<string>();
        List<string> ListImage_SariasSong = new List<string>();
        List<string> ListImage_SunsSong = new List<string>();
        List<string> ListImage_SongOfTime = new List<string>();
        List<string> ListImage_SongOfStorms = new List<string>();

        List<string> ListImage_Minuet = new List<string>();
        List<string> ListImage_Bolero = new List<string>();
        List<string> ListImage_Serenade = new List<string>();
        List<string> ListImage_Nocturne = new List<string>();
        List<string> ListImage_Requiem = new List<string>();
        List<string> ListImage_Prelude = new List<string>();

        //Medallions
        List<string> ListImage_GreenMedallion = new List<string>();
        List<string> ListImage_RedMedallion = new List<string>();
        List<string> ListImage_BlueMedallion = new List<string>();
        List<string> ListImage_PurpleMedallion = new List<string>();
        List<string> ListImage_OrangeMedallion = new List<string>();
        List<string> ListImage_YellowMedallion = new List<string>();
        //Stones
        List<string> ListImage_KokiriStone = new List<string>();
        List<string> ListImage_GoronStone = new List<string>();
        List<string> ListImage_ZoraStone = new List<string>();

        //GoMode
        List<string> ListImage_GoMode = new List<string>();

        List<string> ListImage_GuaranteedHints = new List<string>();
        List<string> ListImage_30SkulltulasOption = new List<string>();
        List<string> ListImage_40SkulltulasOption = new List<string>();
        List<string> ListImage_50SkulltulasOption = new List<string>();
        List<string> ListImage_SkullMaskOption = new List<string>();
        List<string> ListImage_BiggoronOption = new List<string>();
        List<string> ListImage_FrogsOption = new List<string>();
        List<string> ListImage_OcarinaOfTimeOption = new List<string>();

        List<string> ListImage_SometimesHintOption = new List<string>();
        List<string> ListImage_WothItemsOption = new List<string>();

        //private void LoadListImage()
        //{
        //    // Items
        //    ListImage_Slingshot = new List<string>
        //    {
        //        "slingshot_bw_" + ActiveLayout.Slingshot.Size,
        //        "slingshot_" + ActiveLayout.Slingshot.Size
        //    };

        //    ListImage_Bomb = new List<string>
        //    {
        //        "bombs_bw_" + ActiveLayout.Bombs.Size,
        //        "bombs_" + ActiveLayout.Bombs.Size,
        //    };

        //    ListImage_Bombchu = new List<string>
        //    {
        //        "bomb_chu_bw_" + ActiveLayout.Bombchus.Size,
        //        "bomb_chu_" +  + ActiveLayout.Bombchus.Size
        //    };

        //    ListImage_Hookshot = new List<string>
        //    {
        //        "hookshot_bw_" + ActiveLayout.Hookshot.Size,
        //        "hookshot_" + ActiveLayout.Hookshot.Size,
        //        "longshot_" + ActiveLayout.Hookshot.Size
        //    };

        //    ListImage_Bow = new List<string>
        //    {
        //        "bow_bw_" + ActiveLayout.Bow.Size,
        //        "bow_" + ActiveLayout.Bow.Size
        //    };

        //    ListImage_Arrow = new List<string>
        //    {
        //        "fire_light_arrow_bw_" + ActiveLayout.FireLightArrows.Size,
        //        "half_fire_arrow_" + ActiveLayout.FireLightArrows.Size,
        //        "half_light_arrow_" + ActiveLayout.FireLightArrows.Size,
        //        "fire_light_arrow_" + ActiveLayout.FireLightArrows.Size,
        //        "fire_arrow_" + ActiveLayout.FireLightArrows.Size,
        //        "light_arrow_" + ActiveLayout.FireLightArrows.Size
        //    };

        //    ListImage_Spell = new List<string>
        //    {
        //        "dins_farores_bw_" + ActiveLayout.DinFaroreSpells.Size, 
        //        "half_dins_fire_" + ActiveLayout.DinFaroreSpells.Size,
        //        "half_farores_wind_" + ActiveLayout.DinFaroreSpells.Size,
        //        "dins_farores_" + ActiveLayout.DinFaroreSpells.Size,
        //        "dins_fire_" + ActiveLayout.DinFaroreSpells.Size,
        //        "farores_wind_" + ActiveLayout.DinFaroreSpells.Size
        //    };

        //    ListImage_Magic = new List<string>
        //    {
        //        "magic_bw_" + ActiveLayout.Magic.Size,
        //        "magic_" + ActiveLayout.Magic.Size,
        //        "double_magic_" + ActiveLayout.Magic.Size
        //    };

        //    ListImage_Boots = new List<string>
        //    {
        //        "iron_hover_boots_bw_" + ActiveLayout.IronHoverBoots.Size,
        //        "half_iron_boots_" + ActiveLayout.IronHoverBoots.Size,
        //        "half_hover_boots_" + ActiveLayout.IronHoverBoots.Size,
        //        "iron_hover_boots_" + ActiveLayout.IronHoverBoots.Size,
        //        "iron_boots_" + ActiveLayout.IronHoverBoots.Size,
        //        "hover_boots_" + ActiveLayout.IronHoverBoots.Size
        //    };

        //    ListImage_BiggoronQuest = new List<string>
        //    {
        //        "prescription_bw_" + ActiveLayout.BiggoronItem.Size,
        //        "prescription_" + ActiveLayout.BiggoronItem.Size,
        //        "kz_frog_" + ActiveLayout.BiggoronItem.Size,
        //        "eye_drops_" + ActiveLayout.BiggoronItem.Size,
        //        "claim_check_" + ActiveLayout.BiggoronItem.Size
        //    };

        //    ListImage_Boomerang = new List<string>
        //    {
        //        "boomerang_bw_" + ActiveLayout.Boomerang.Size,
        //        "boomerang_" + ActiveLayout.Boomerang.Size
        //    };

        //    ListImage_Scale = new List<string>
        //    {
        //        "scale_bw_" + ActiveLayout.Scale.Size,
        //        "scale_" + ActiveLayout.Scale.Size,
        //        "golden_scale_" + ActiveLayout.Scale.Size
        //    };

        //    ListImage_Strength = new List<string>
        //    {
        //        "strength_bw_" + ActiveLayout.Strength.Size,
        //        "strength_" + ActiveLayout.Strength.Size,
        //        "strength2_" + ActiveLayout.Strength.Size,
        //        "strength3_" + ActiveLayout.Strength.Size
        //    };

        //    ListImage_Lens = new List<string>
        //    {
        //        "lens_bw_" + ActiveLayout.Lens.Size,
        //        "lens_" + ActiveLayout.Lens.Size
        //    };

        //    ListImage_Hammer = new List<string>
        //    {
        //        "hammer_bw_" + ActiveLayout.Hammer.Size,
        //        "hammer_" + ActiveLayout.Hammer.Size
        //    };

        //    ListImage_Tunic = new List<string>
        //    {
        //        "goron_zora_tunic_bw_" + ActiveLayout.GoronZoraTunics.Size,
        //        "half_goron_tunic_" + ActiveLayout.GoronZoraTunics.Size,
        //        "half_zora_tunic_" + ActiveLayout.GoronZoraTunics.Size,
        //        "goron_zora_tunic_" + ActiveLayout.GoronZoraTunics.Size,
        //        "goron_tunic_" + ActiveLayout.GoronZoraTunics.Size,
        //        "zora_tunic_" + ActiveLayout.GoronZoraTunics.Size
        //    };

        //    ListImage_Wallet = new List<string>
        //    {
        //        "wallet_" + ActiveLayout.Wallet.Size,
        //        "wallet2_" + ActiveLayout.Wallet.Size,
        //        "wallet3_" + ActiveLayout.Wallet.Size
        //    };

        //    ListImage_RutosLetter = new List<string>
        //    {
        //        "bottle_rutos_letter_bw_" + ActiveLayout.Bottle1.Size,
        //        "bottle_rutos_letter_" + ActiveLayout.Bottle1.Size
        //    };

        //    ListImage_MirrorShield = new List<string>
        //    {
        //        "mirror_shield_bw_" + ActiveLayout.MirrorShield.Size,
        //        "mirror_shield_" + ActiveLayout.MirrorShield.Size
        //    };

        //    // Songs
        //    ListImage_TinySongs = new List<string>
        //    {
        //        "no_song",
        //        "check_song"
        //    };

        //    ListImage_ZeldasLullaby = new List<string>
        //    {
        //        "zeldas_lullaby_bw_" + ActiveLayout.ZeldasLullaby.Size,
        //        "zeldas_lullaby_" + ActiveLayout.ZeldasLullaby.Size
        //    };

        //    ListImage_EponasSong = new List<string>
        //    {
        //        "epona_bw_" + ActiveLayout.EponasSong.Size,
        //        "epona_" + ActiveLayout.EponasSong.Size
        //    };

        //    ListImage_SariasSong = new List<string>
        //    {
        //        "saria_bw_" + ActiveLayout.SariasSong.Size,
        //        "saria_" + ActiveLayout.SariasSong.Size
        //    };

        //    ListImage_SunsSong = new List<string>
        //    {
        //        "suns_song_bw_" + ActiveLayout.SunsSong.Size,
        //        "suns_song_" + ActiveLayout.SunsSong.Size
        //    };

        //    ListImage_SongOfTime = new List<string>
        //    {
        //        "song_of_time_bw_" + ActiveLayout.SongOfTime.Size,
        //        "song_of_time_" + ActiveLayout.SongOfTime.Size
        //    };

        //    ListImage_SongOfStorms = new List<string>
        //    {
        //        "song_of_storms_bw_" + ActiveLayout.SongOfStorms.Size,
        //        "song_of_storms_" + ActiveLayout.SongOfStorms.Size
        //    };

        //    ListImage_Minuet = new List<string>
        //    {
        //        "minuet_bw_" + ActiveLayout.Minuet.Size,
        //        "minuet_" + ActiveLayout.Minuet.Size
        //    };

        //    ListImage_Bolero = new List<string>
        //    {
        //        "bolero_bw_" + ActiveLayout.Bolero.Size,
        //        "bolero_" + ActiveLayout.Bolero.Size
        //    };

        //    ListImage_Serenade = new List<string>
        //    {
        //        "serenade_bw_" + ActiveLayout.Serenade.Size,
        //        "serenade_" + ActiveLayout.Serenade.Size
        //    };

        //    ListImage_Nocturne = new List<string>
        //    {
        //        "nocturne_bw_" + ActiveLayout.Nocturne.Size,
        //        "nocturne_" + ActiveLayout.Nocturne.Size
        //    };

        //    ListImage_Requiem = new List<string>
        //    {
        //        "requiem_bw_" + ActiveLayout.Requiem.Size,
        //        "requiem_" + ActiveLayout.Requiem.Size
        //    };

        //    ListImage_Prelude = new List<string>
        //    {
        //        "prelude_bw_" + ActiveLayout.Prelude.Size,
        //        "prelude_" + ActiveLayout.Prelude.Size
        //    };

        //    // Medallions
        //    ListImage_GreenMedallion = new List<string>
        //    {
        //        "green_medaillon_bw_" + ActiveLayout.GreenMedallion.Size,
        //        "green_medaillon_" + ActiveLayout.GreenMedallion.Size
        //    };

        //    ListImage_RedMedallion = new List<string>
        //    {
        //        "red_medaillon_bw_" + ActiveLayout.RedMedallion.Size,
        //        "red_medaillon_" + ActiveLayout.RedMedallion.Size
        //    };

        //    ListImage_BlueMedallion = new List<string>
        //    {
        //        "blue_medaillon_bw_" + ActiveLayout.BlueMedallion.Size,
        //        "blue_medaillon_" + ActiveLayout.BlueMedallion.Size
        //    };

        //    ListImage_PurpleMedallion = new List<string>
        //    {
        //        "purple_medaillon_bw_" + ActiveLayout.PurpleMedallion.Size,
        //        "purple_medaillon_" + ActiveLayout.PurpleMedallion.Size
        //    };

        //    ListImage_OrangeMedallion = new List<string>
        //    {
        //        "orange_medaillon_bw_" + ActiveLayout.OrangeMedallion.Size,
        //        "orange_medaillon_" + ActiveLayout.OrangeMedallion.Size
        //    };

        //    ListImage_YellowMedallion = new List<string>
        //    {
        //        "yellow_medaillon_bw_" + ActiveLayout.YellowMedallion.Size,
        //        "yellow_medaillon_" + ActiveLayout.YellowMedallion.Size,
        //    };
        //    // Stones
        //    ListImage_KokiriStone = new List<string>
        //    {
        //        "kokiri_stone_bw_" + ActiveLayout.KokiriStone.Size,
        //        "kokiri_stone_" + ActiveLayout.KokiriStone.Size,
        //    };

        //    ListImage_GoronStone = new List<string>
        //    {
        //        "goron_stone_bw_" + ActiveLayout.GoronStone.Size,
        //        "goron_stone_" + ActiveLayout.GoronStone.Size
        //    };

        //    ListImage_ZoraStone = new List<string>
        //    {
        //        "zora_stone_bw_" + ActiveLayout.ZoraStone.Size,
        //        "zora_stone_" + ActiveLayout.ZoraStone.Size
        //    };

        //    // Go Mode
        //    ListImage_GoMode = new List<string>
        //    {
        //        "go_mode_bw_" + ActiveLayout.GoMode.Size,
        //        "go_mode_" + ActiveLayout.GoMode.Size
        //    };

        //    // Guaranteed Hints And Option
        //    ListImage_GuaranteedHints = new List<string>
        //    {
        //        "_30_gold_skulltula_" + ActiveLayout.Skulltulas_30.Size,
        //        "_40_gold_skulltula_" + ActiveLayout.Skulltulas_40.Size,
        //        "_50_gold_skulltula_" + ActiveLayout.Skulltulas_50.Size,
        //        "skull_mask_" + ActiveLayout.SkullMask.Size,
        //        "biggoron_" + ActiveLayout.Biggoron.Size,
        //        "frogs_" + ActiveLayout.Frogs.Size,
        //        "ocarina_of_time_" + ActiveLayout.OcarinaOfTimeHint.Size
        //    };

        //    ListImage_30SkulltulasOption = new List<string>
        //    {
        //        "gossip_stone_bw_" + ActiveLayout.Skulltulas_30_GossipStone.Size,
        //        "sold_out_" + ActiveLayout.Skulltulas_30_GossipStone.Size,
        //        "bottle_empty_" + ActiveLayout.Skulltulas_30_GossipStone.Size,
        //        "bottle_big_poe_" + ActiveLayout.Skulltulas_30_GossipStone.Size
        //    };

        //    ListImage_40SkulltulasOption = new List<string>
        //    {
        //        "gossip_stone_bw_" + ActiveLayout.Skulltulas_40_GossipStone.Size,
        //        "sold_out_" + ActiveLayout.Skulltulas_40_GossipStone.Size,
        //        "bottle_empty_" + ActiveLayout.Skulltulas_40_GossipStone.Size,
        //        "bottle_big_poe_" + ActiveLayout.Skulltulas_40_GossipStone.Size
        //    };

        //    ListImage_50SkulltulasOption = new List<string>
        //    {
        //        "gossip_stone_bw_" + ActiveLayout.Skulltulas_50_GossipStone.Size,
        //        "sold_out_" + ActiveLayout.Skulltulas_50_GossipStone.Size,
        //        "bottle_empty_" + ActiveLayout.Skulltulas_50_GossipStone.Size,
        //        "bottle_big_poe_" + ActiveLayout.Skulltulas_50_GossipStone.Size
        //    };

        //    ListImage_SkullMaskOption = new List<string>
        //    {
        //        "gossip_stone_bw_" + ActiveLayout.SkullMask_GossipStone.Size,
        //        "sold_out_" + ActiveLayout.SkullMask_GossipStone.Size,
        //        "bottle_empty_" + ActiveLayout.SkullMask_GossipStone.Size,
        //        "bottle_big_poe_" + ActiveLayout.SkullMask_GossipStone.Size
        //    };

        //    ListImage_BiggoronOption = new List<string>
        //    {
        //        "gossip_stone_bw_" + ActiveLayout.Biggoron_GossipStone.Size,
        //        "sold_out_" + ActiveLayout.Biggoron_GossipStone.Size,
        //        "bottle_empty_" + ActiveLayout.Biggoron_GossipStone.Size,
        //        "bottle_big_poe_" + ActiveLayout.Biggoron_GossipStone.Size
        //    };

        //    ListImage_FrogsOption = new List<string>
        //    {
        //        "gossip_stone_bw_" + ActiveLayout.Frogs_GossipStone.Size,
        //        "sold_out_" + ActiveLayout.Frogs_GossipStone.Size,
        //        "bottle_empty_" + ActiveLayout.Frogs_GossipStone.Size,
        //        "bottle_big_poe_" + ActiveLayout.Frogs_GossipStone.Size
        //    };

        //    ListImage_OcarinaOfTimeOption = new List<string>
        //    {
        //        "gossip_stone_bw_" + ActiveLayout.OcarinaOfTimeHint_GossipStone.Size,
        //        "sold_out_" + ActiveLayout.OcarinaOfTimeHint_GossipStone.Size,
        //        "bottle_empty_" + ActiveLayout.OcarinaOfTimeHint_GossipStone.Size,
        //        "bottle_big_poe_" + ActiveLayout.OcarinaOfTimeHint_GossipStone.Size
        //    };

        //    // WotH Items Options
        //    ListImage_WothItemsOption = new List<string>
        //    {
        //        "gossip_stone_bw_" + 24,
        //        "bottle_empty_" + 24,
        //        "bottle_big_poe_" + 24
        //    };

        //    // Sometimes Hints Option
        //    ListImage_SometimesHintOption = new List<string>
        //    {
        //        "gossip_stone_bw_" + ActiveLayout.SH_GossipStone1.Size,
        //        "sold_out_" + ActiveLayout.SH_GossipStone1.Size,
        //        "key_" + ActiveLayout.SH_GossipStone1.Size,
        //        "bk_" + ActiveLayout.SH_GossipStone1.Size,
        //        "bottle_empty_" + ActiveLayout.SH_GossipStone1.Size,
        //        "bottle_big_poe_" + ActiveLayout.SH_GossipStone1.Size
        //    };
        //}
    }
}
