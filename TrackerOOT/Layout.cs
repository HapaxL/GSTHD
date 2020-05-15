using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerOOT
{
    class Layout
    {
        //App
        public Resolution AppSize { get; set; }
        // Items
        public ObjectPoint Slingshot { get; set; }
        public ObjectPoint Bombs { get; set; }
        public ObjectPoint Bombchus { get; set; }
        public ObjectPoint Hookshot { get; set; }
        public ObjectPoint Longshot { get; set; }
        public ObjectPoint Bow { get; set; }
        public ObjectPoint Arrows { get; set; }
        public ObjectPoint Spells { get; set; }
        public ObjectPoint Magic { get; set; }
        public ObjectPoint Boots { get; set; }
        public ObjectPoint BiggoronItem { get; set; }
        public ObjectPoint Boomerang { get; set; }
        public ObjectPoint Scale { get; set; }
        public ObjectPoint Strength { get; set; }
        public ObjectPoint Lens { get; set; }
        public ObjectPoint Hammer { get; set; }
        public ObjectPoint Tunics { get; set; }
        public ObjectPoint Wallet { get; set; }
        public ObjectPoint RutosLetter { get; set; }
        public ObjectPoint MirrorShield { get; set; }
        public ObjectPoint CollectedSkulls { get; set; }

        // Songs
        public ObjectPoint ZeldasLullaby { get; set; }
        public ObjectPoint EponasSong { get; set; }
        public ObjectPoint SariasSong { get; set; }
        public ObjectPoint SunsSong { get; set; }
        public ObjectPoint SongOfTime { get; set; }
        public ObjectPoint SongOfStorms { get; set; }
        public ObjectPoint Minuet { get; set; }
        public ObjectPoint Bolero { get; set; }
        public ObjectPoint Serenade { get; set; }
        public ObjectPoint Nocturne { get; set; }
        public ObjectPoint Requiem { get; set; }
        public ObjectPoint Prelude { get; set; }

        // Medallions
        public ObjectPoint GreenMedallion { get; set; }
        public ObjectPoint RedMedallion { get; set; }
        public ObjectPoint BlueMedallion { get; set; }
        public ObjectPoint PurpleMedallion { get; set; }
        public ObjectPoint OrangeMedallion { get; set; }
        public ObjectPoint YellowMedallion { get; set; }
        public ObjectPoint KokiriStone { get; set; }
        public ObjectPoint GoronStone { get; set; }
        public ObjectPoint ZoraStone { get; set; }

        // Go Mode
        public ObjectPoint GoMode { get; set; }

        // Hints
        public ObjectPoint Skulltulas_30 { get; set; }
        public ObjectPoint Skulltulas_30_GossipStone { get; set; }
        public ObjectPoint Skulltulas_40 { get; set; }
        public ObjectPoint Skulltulas_40_GossipStone { get; set; }
        public ObjectPoint Skulltulas_50 { get; set; }
        public ObjectPoint Skulltulas_50_GossipStone { get; set; }
        public ObjectPoint SkullMask { get; set; }
        public ObjectPoint SkullMask_GossipStone { get; set; }
        public ObjectPoint Biggoron { get; set; }
        public ObjectPoint Biggoron_GossipStone { get; set; }
        public ObjectPoint Frogs { get; set; }
        public ObjectPoint Frogs_GossipStone { get; set; }
        public ObjectPoint OcarinaOfTimeHint { get; set; }
        public ObjectPoint OcarinaOfTimeHint_GossipStone { get; set; }

        //SometimesHints
        public ObjectPoint SH_GossipStone1 { get; set; }
        public ObjectPoint SH_GossipStone2 { get; set; }
        public ObjectPoint SH_GossipStone3 { get; set; }
        public ObjectPoint SH_GossipStone4 { get; set; }
        public ObjectPoint SH_GossipStone5 { get; set; }

        public ObjectPoint Chronometer { get; set; }
        public ObjectPoint PanelWoth { get; set; }
        public ObjectPoint PanelBarren { get; set; }

    }

    class ObjectPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Size { get; set; }
    }

    class Resolution
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
