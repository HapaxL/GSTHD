using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GSTHD
{
    public class Settings
    {
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

        private LocalSettings Local;
        private LayoutSettings Layout;

        public bool ShowMenuBar { get => Local.ShowMenuBar; }
        public string ActiveLayout { get => Local.ActiveLayout; }
        public bool InvertScrollWheel { get => Local.InvertScrollWheel; }
        //public bool WraparoundDungeonNames { get => Layout.DefaultDungeonNames.Wraparound.GetValueOrDefault(Local.WraparoundDungeonNames); }
        public DragButtonOption DragButton { get => Local.DragButton; }
        public DragButtonOption AutocheckDragButton { get => Local.AutocheckDragButton; }
        public int MinDragThreshold { get => Local.MinDragThreshold; }
        public bool MoveLocationToSong { get => Local.MoveLocationToSong; }
        // public bool AutoCheckSongs { get => LocalSettings.AutocheckSongs; }
        public SongMarkerBehaviourOption SongMarkerBehaviour { get => Local.SongMarkerBehaviour; }
        public string[] DefaultSongMarkerImages { get => Layout.DefaultSongMarkerImages ?? Local.DefaultSongMarkerImages; }
        public string[] DefaultGossipStoneImages { get => Layout.DefaultGossipStoneImages ?? Local.DefaultGossipStoneImages; }
        public string[] DefaultPathGoalImages { get => Layout.DefaultPathGoalImages ?? Local.DefaultPathGoalImages; }
        public int DefaultPathGoalCount { get => Layout.DefaultPathGoalCount.GetValueOrDefault(Local.DefaultPathGoalCount); }
        public int DefaultWothGossipStoneCount { get => Layout.DefaultWothGossipStoneCount.GetValueOrDefault(Local.DefaultWothGossipStoneCount); }
        public string[] DefaultWothColors { get => Layout.WothColors ?? Local.DefaultWothColors; }
        public string[] DefaultBarrenColors { get => Layout.BarrenColors ?? Local.DefaultBarrenColors; }
        public int DefaultWothColorIndex { get => Layout.DefaultWothColorIndex.GetValueOrDefault(Local.DefaultWothColorIndex); }
        public bool EnableDuplicateWoth { get => Local.EnableDuplicateWoth; }
        public bool EnableLastWoth { get => Local.EnableLastWoth; }
        public bool EnableBarrenColors { get => Local.EnableBarrenColors; }
        public KnownColor LastWothColor { get => Local.LastWothColor; }

        public MedallionLabel DefaultDungeonNames { get; private set; }

        public Settings(LocalSettings local)
        {
            Local = local;
            DefaultDungeonNames = new MedallionLabel();
            UpdateFromLocal();
        }

        public void SetLayoutSettings(LayoutSettings layout)
        {
            Layout = layout;
            Update();
        }

        public void Update()
        {
            if (Layout.DefaultDungeonNames != null)
            {
                UpdateFromLayout();
            }
            else
            {
                UpdateFromLocal();
            }
        }

        private void UpdateFromLayout()
        {
            DefaultDungeonNames.TextCollection = Layout.DefaultDungeonNames.TextCollection ?? Local.DefaultDungeonNames.TextCollection;
            DefaultDungeonNames.DefaultValue = Layout.DefaultDungeonNames.DefaultValue.GetValueOrDefault(Local.DefaultDungeonNames.DefaultValue.Value);
            DefaultDungeonNames.Wraparound = Layout.DefaultDungeonNames.Wraparound.GetValueOrDefault(Local.DefaultDungeonNames.Wraparound.Value);
            DefaultDungeonNames.FontName = Layout.DefaultDungeonNames.FontName ?? Local.DefaultDungeonNames.FontName;
            DefaultDungeonNames.FontSize = Layout.DefaultDungeonNames.FontSize.GetValueOrDefault(Local.DefaultDungeonNames.FontSize.Value);
            DefaultDungeonNames.FontStyle = Layout.DefaultDungeonNames.FontStyle.GetValueOrDefault(Local.DefaultDungeonNames.FontStyle.Value);
        }

        private void UpdateFromLocal()
        {
            DefaultDungeonNames.TextCollection = Local.DefaultDungeonNames.TextCollection;
            DefaultDungeonNames.DefaultValue = Local.DefaultDungeonNames.DefaultValue;
            DefaultDungeonNames.Wraparound = Local.DefaultDungeonNames.Wraparound;
            DefaultDungeonNames.FontName = Local.DefaultDungeonNames.FontName;
            DefaultDungeonNames.FontSize = Local.DefaultDungeonNames.FontSize;
            DefaultDungeonNames.FontStyle = Local.DefaultDungeonNames.FontStyle;
        }
    }
}
