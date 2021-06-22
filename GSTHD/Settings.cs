using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace GSTHD
{
    public class Settings
    {
        private const string SettingsFileName = @"settings.json";
        
        public enum SongMarkerBehaviourEnum
        {
            None,
            CheckOnly,
            DropOnly,
            DropAndCheck,
            DragAndDrop,
            Full,
        }

        private const SongMarkerBehaviourEnum DefaultSongMarkerBehaviour = SongMarkerBehaviourEnum.DropAndCheck;

        public string ActiveLayout { get; set; } = string.Empty;
        public bool InvertScrollWheel { get; set; } = false;
        public bool MoveLocationToSong { get; set; } = false;
        public bool AutoCheckSongs { get; set; } = false;
        public SongMarkerBehaviourEnum SongMarkerBehaviour { get; set; } = DefaultSongMarkerBehaviour;
        public string[] DefaultSongMarkerImages { get; set; } = new string[0];
        public string[] DefaultGossipStoneImages { get; set; } = new string[0];

        public static SongMarkerBehaviourEnum GetSongMarkerBehaviour(string value)
        {
            if (string.IsNullOrEmpty(value))
                return DefaultSongMarkerBehaviour;

            return (SongMarkerBehaviourEnum) Enum.Parse(typeof(SongMarkerBehaviourEnum), value);
        }

        public static Settings Get()
        {
            return JsonConvert.DeserializeObject<Settings>(File.ReadAllText(SettingsFileName));
        }
    }
}
