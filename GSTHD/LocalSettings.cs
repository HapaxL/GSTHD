using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GSTHD
{
    public class LocalSettings
    {
        public static string LocalSettingsFileName = @"settings.json";

        private const Settings.SongMarkerBehaviourOption DefaultSongMarkerBehaviour = Settings.SongMarkerBehaviourOption.DropAndCheck;

        public bool ShowMenuBar { get; set; } = true;
        public string ActiveLayout { get; set; } = string.Empty;
        public bool InvertScrollWheel { get; set; } = false;
        //public bool WraparoundDungeonNames { get; set; } = true;
        public Settings.DragButtonOption DragButton { get; set; } = Settings.DragButtonOption.Middle;
        public Settings.DragButtonOption AutocheckDragButton { get; set; } = Settings.DragButtonOption.None;
        public int MinDragThreshold { get; set; } = 6;
        public bool MoveLocationToSong { get; set; } = false;
        // public bool AutoCheckSongs { get; set; } = false;
        public Settings.SongMarkerBehaviourOption SongMarkerBehaviour { get; set; } = DefaultSongMarkerBehaviour;
        public string[] DefaultSongMarkerImages { get; set; } = new string[0];
        public string[] DefaultGossipStoneImages { get; set; } = new string[0];
        public string[] DefaultPathGoalImages { get; set; } = new string[0];
        public int DefaultPathGoalCount { get; set; } = 0;
        public int DefaultWothGossipStoneCount { get; set; } = 4;
        public string[] DefaultWothColors { get; set; } = new string[]
        {
            "White",
            "Orange",
            "Crimson",
        };
        public string[] DefaultBarrenColors { get; set; } = new string[]
        {
            "White",
            "Gold",
        };
        public int DefaultWothColorIndex { get; set; } = 0;
        public bool EnableDuplicateWoth { get; set; } = true;
        public bool EnableLastWoth { get; set; } = false;
        public bool EnableBarrenColors { get; set; } = true;
        public KnownColor LastWothColor { get; set; } = KnownColor.BlueViolet;

        public MedallionLabel DefaultDungeonNames { get; set; } = new MedallionLabel()
        {
            TextCollection = new string[] { "????", "FREE", "DEKU", "DC", "JABU", "FOREST", "FIRE", "WATER", "SHADOW", "SPIRIT" },
            DefaultValue = 0,
            Wraparound = true,
            FontName = "Consolas",
            FontSize = 8,
            FontStyle = FontStyle.Bold,
        };

        //public static Settings.SongMarkerBehaviourOption GetSongMarkerBehaviour(string value)
        //{
        //    if (string.IsNullOrEmpty(value))
        //        return DefaultSongMarkerBehaviour;

        //    return (Settings.SongMarkerBehaviourOption) Enum.Parse(typeof(Settings.SongMarkerBehaviourOption), value);
        //}
    }
}
