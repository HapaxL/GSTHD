using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GSTHD
{
    public class Settings
    {
        private const string SettingsFileName = @"settings.json";

        [JsonConverter(typeof(StringEnumConverter))]
        public enum SongMarkerBehaviourOption
        {
            None,
            CheckOnly,
            DropOnly,
            DropAndCheck,
            DragAndDrop,
            Full,
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum DragButtonOption
        {
            None,
            Left,
            Middle,
            Right,
            LeftAndRight,
        }

        private const SongMarkerBehaviourOption DefaultSongMarkerBehaviour = SongMarkerBehaviourOption.DropAndCheck;

        public bool ShowMenuBar { get; set; } = true;
        public string ActiveLayout { get; set; } = string.Empty;
        public bool InvertScrollWheel { get; set; } = false;
        public bool WraparoundDungeonNames { get; set; } = true;
        public DragButtonOption DragButton { get; set; } = DragButtonOption.Middle;
        public DragButtonOption AutocheckDragButton { get; set; } = DragButtonOption.None;
        public int MinDragThreshold { get; set; } = 6;
        public bool MoveLocationToSong { get; set; } = false;
        // public bool AutoCheckSongs { get; set; } = false;
        public SongMarkerBehaviourOption SongMarkerBehaviour { get; set; } = DefaultSongMarkerBehaviour;
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

        public static SongMarkerBehaviourOption GetSongMarkerBehaviour(string value)
        {
            if (string.IsNullOrEmpty(value))
                return DefaultSongMarkerBehaviour;

            return (SongMarkerBehaviourOption) Enum.Parse(typeof(SongMarkerBehaviourOption), value);
        }

        public static Settings Read()
        {
            if (File.Exists(SettingsFileName))
                return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(SettingsFileName));
            else
            {
                var settings = new Settings();
                settings.Write();
                return settings;
            }
        }

        public void Write()
        {
            var str = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(SettingsFileName, str);
        }
    }
}
