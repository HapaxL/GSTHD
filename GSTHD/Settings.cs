using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTHD
{
    public class Settings
    {
        private const string SettingsFileName = @"settings.json";

        private const string MoveLocationToSongString = "MoveLocationToSong";
        private const string AutoCheckSongsString = "AutoCheckSongs";
        private const string ActiveLayoutString = "ActiveLayout";
        private const string InvertScrollWheelString = "InvertScrollWheel";

        public bool MoveLocationToSong = false;
        public bool AutoCheckSongs = false;
        public string ActiveLayout = string.Empty;
        public bool InvertScrollWheel = false;

        public static Settings GetSettings()
        {
            var settings = new Settings();
            
            foreach (var property in JObject.Parse(File.ReadAllText(SettingsFileName)))
            {
                switch (property.Key)
                {
                    case MoveLocationToSongString:
                        settings.MoveLocationToSong = Convert.ToBoolean(property.Value);
                        break;
                    case AutoCheckSongsString:
                        settings.AutoCheckSongs = Convert.ToBoolean(property.Value);
                        break;
                    case ActiveLayoutString:
                        settings.ActiveLayout = property.Value.ToString();
                        break;
                    case InvertScrollWheelString:
                        settings.InvertScrollWheel = Convert.ToBoolean(property.Value);
                        break;
                    default: break;
                }
            }

            return settings;
        }
    }
}
