using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GSTHD
{
    public interface UpdatableFromSettings
    {
        void UpdateFromSettings();
    }

    public class Layout
    {
        public List<GenericLabel> ListLabels = new List<GenericLabel>();
        public List<GenericTextBox> ListTextBoxes = new List<GenericTextBox>();
        public List<ObjectPoint> ListItems = new List<ObjectPoint>();
        public List<ObjectPointSong> ListSongs = new List<ObjectPointSong>();
        public List<ObjectPoint> ListDoubleItems = new List<ObjectPoint>();
        public List<ObjectPointCollectedItem> ListCollectedItems = new List<ObjectPointCollectedItem>();
        public List<ObjectPointMedallion> ListMedallions = new List<ObjectPointMedallion>();
        public List<ObjectPoint> ListGuaranteedHints = new List<ObjectPoint>();
        public List<ObjectPoint> ListGossipStones = new List<ObjectPoint>();
        public List<ObjectPointGrid> ListGossipStoneGrids = new List<ObjectPointGrid>();
        public List<AutoFillTextBox> ListSometimesHints = new List<AutoFillTextBox>();
        public List<AutoFillTextBox> ListChronometers = new List<AutoFillTextBox>();
        public List<ObjectPanelWotH> ListPanelWotH = new List<ObjectPanelWotH>();
        public List<ObjectPanelBarren> ListPanelBarren = new List<ObjectPanelBarren>();
        public List<ObjectPointGoMode> ListGoMode = new List<ObjectPointGoMode>();

        public List<UpdatableFromSettings> ListUpdatables = new List<UpdatableFromSettings>();

        public AppSettings App_Settings = new AppSettings();

        public void UpdateFromSettings()
        {
            foreach (var updatable in ListUpdatables)
            {
                updatable.UpdateFromSettings();
            }
        }

        public void LoadLayout(Panel panelLayout, Settings settings, SortedSet<string> listSometimesHintsSuggestions, Dictionary<string, string> listPlacesWithTag, Form1 form)
        {
            if (settings.ActiveLayout != string.Empty)
            {
                JObject json_layout = JObject.Parse(File.ReadAllText(@"Layouts/" + settings.ActiveLayout + ".json"));
                foreach (var category in json_layout)
                {
                    if (category.Key.ToString() == "AppSize")
                    {
                        App_Settings = JsonConvert.DeserializeObject<AppSettings>(category.Value.ToString());
                    }

                    if (category.Key.ToString() == "Labels")
                    {
                        foreach (var element in category.Value)
                        {
                            ListLabels.Add(JsonConvert.DeserializeObject<GenericLabel>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "TextBoxes")
                    {
                        foreach (var element in category.Value)
                        {
                            ListTextBoxes.Add(JsonConvert.DeserializeObject<GenericTextBox>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "Items")
                    {
                        foreach (var element in category.Value)
                        {
                            ListItems.Add(JsonConvert.DeserializeObject<ObjectPoint>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "Songs")
                    {
                        foreach (var element in category.Value)
                        {
                            ListSongs.Add(JsonConvert.DeserializeObject<ObjectPointSong>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "DoubleItems")
                    {
                        foreach (var element in category.Value)
                        {
                            ListDoubleItems.Add(JsonConvert.DeserializeObject<ObjectPoint>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "CollectedItems")
                    {
                        foreach (var element in category.Value)
                        {
                            ListCollectedItems.Add(JsonConvert.DeserializeObject<ObjectPointCollectedItem>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "Medallions")
                    {
                        foreach (var element in category.Value)
                        {
                            ListMedallions.Add(JsonConvert.DeserializeObject<ObjectPointMedallion>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "GuaranteedHints")
                    {
                        foreach (var element in category.Value)
                        {
                            ListGuaranteedHints.Add(JsonConvert.DeserializeObject<ObjectPoint>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "GossipStones")
                    {
                        foreach (var element in category.Value)
                        {
                            ListGossipStones.Add(JsonConvert.DeserializeObject<ObjectPoint>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "GossipStoneGrids")
                    {
                        foreach (var element in category.Value)
                        {
                            ListGossipStoneGrids.Add(JsonConvert.DeserializeObject<ObjectPointGrid>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "SometimesHints")
                    {
                        foreach (var element in category.Value)
                        {
                            ListSometimesHints.Add(JsonConvert.DeserializeObject<AutoFillTextBox>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "Chronometers")
                    {
                        foreach (var element in category.Value)
                        {
                            ListChronometers.Add(JsonConvert.DeserializeObject<AutoFillTextBox>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "PanelWoth")
                    {
                        foreach (var element in category.Value)
                        {
                            ListPanelWotH.Add(JsonConvert.DeserializeObject<ObjectPanelWotH>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "PanelBarren")
                    {
                        foreach (var element in category.Value)
                        {
                            ListPanelBarren.Add(JsonConvert.DeserializeObject<ObjectPanelBarren>(element.ToString()));
                        }
                    }

                    if (category.Key.ToString() == "GoMode")
                    {
                        foreach (var element in category.Value)
                        {
                            ListGoMode.Add(JsonConvert.DeserializeObject<ObjectPointGoMode>(element.ToString()));
                        }
                    }
                }

                panelLayout.Size = new Size(App_Settings.Width, App_Settings.Height);
                if (App_Settings.BackgroundColor.HasValue)
                    form.BackColor = App_Settings.BackgroundColor.Value;
                panelLayout.BackColor = form.BackColor;

                if (App_Settings.DefaultSongMarkerImages != null)
                {
                    settings.DefaultSongMarkerImages = App_Settings.DefaultSongMarkerImages;
                }
                if (App_Settings.DefaultGossipStoneImages != null)
                {
                    settings.DefaultGossipStoneImages = App_Settings.DefaultGossipStoneImages;
                }
                if (App_Settings.DefaultPathGoalImages != null)
                {
                    settings.DefaultPathGoalImages = App_Settings.DefaultPathGoalImages;
                }
                if (App_Settings.DefaultPathGoalCount.HasValue)
                {
                    settings.DefaultPathGoalCount = App_Settings.DefaultPathGoalCount.Value;
                }
                if (App_Settings.DefaultWothGossipStoneCount.HasValue)
                {
                    settings.DefaultWothGossipStoneCount = App_Settings.DefaultWothGossipStoneCount.Value;
                }
                if (App_Settings.WothColors != null)
                {
                    settings.DefaultWothColors = App_Settings.WothColors;
                }
                if (App_Settings.BarrenColors != null)
                {
                    settings.DefaultBarrenColors = App_Settings.BarrenColors;
                }
                if (App_Settings.DefaultWothColorIndex.HasValue)
                {
                    settings.DefaultWothColorIndex = App_Settings.DefaultWothColorIndex.Value;
                }
                if (App_Settings.DefaultDungeonNames != null)
                {
                    if (App_Settings.DefaultDungeonNames.TextCollection != null)
                        settings.DefaultDungeonNames.TextCollection = App_Settings.DefaultDungeonNames.TextCollection;
                    if (App_Settings.DefaultDungeonNames.DefaultValue.HasValue)
                        settings.DefaultDungeonNames.DefaultValue = App_Settings.DefaultDungeonNames.DefaultValue;
                    if (App_Settings.DefaultDungeonNames.Wraparound.HasValue)
                        settings.DefaultDungeonNames.Wraparound = App_Settings.DefaultDungeonNames.Wraparound;
                    if (App_Settings.DefaultDungeonNames.FontName != null)
                        settings.DefaultDungeonNames.FontName = App_Settings.DefaultDungeonNames.FontName;
                    if (App_Settings.DefaultDungeonNames.FontSize.HasValue)
                        settings.DefaultDungeonNames.FontSize = App_Settings.DefaultDungeonNames.FontSize;
                    if (App_Settings.DefaultDungeonNames.FontStyle.HasValue)
                        settings.DefaultDungeonNames.FontStyle = App_Settings.DefaultDungeonNames.FontStyle;
                }

                if (ListLabels.Count > 0)
                {
                    foreach (var item in ListLabels)
                    {
                        if (item.Visible)
                        {
                            panelLayout.Controls.Add(new Label()
                            {
                                Text = item.Text,
                                Left = item.X,
                                Top = item.Y,
                                Font = new Font(new FontFamily(item.FontName), item.FontSize, item.FontStyle),
                                ForeColor = Color.FromName(item.Color),
                                BackColor = Color.Transparent,
                                AutoSize = true,
                            });
                        }
                    }
                }

                if (ListTextBoxes.Count > 0)
                {
                    foreach (var box in ListTextBoxes)
                    {
                        if (box.Visible)
                        {
                            panelLayout.Controls.Add(new TextBox()
                            {
                                BackColor = box.BackColor,
                                Font = new Font(box.FontName, box.FontSize, box.FontStyle),
                                ForeColor = box.FontColor,
                                Size = new Size(box.Width, box.Height),
                                Location = new Point(box.X, box.Y),
                            });
                        }
                    }
                }

                if (ListItems.Count > 0)
                {
                    foreach (var item in ListItems)
                    {
                        if (item.Visible)
                            panelLayout.Controls.Add(new Item(item, settings));
                    }
                }

                if (ListSongs.Count > 0)
                {
                    foreach (var song in ListSongs)
                    {
                        if (song.Visible)
                        {
                            var s = new Song(song, settings);
                            panelLayout.Controls.Add(s);
                            ListUpdatables.Add(s);
                        }
                    }
                }

                if (ListDoubleItems.Count > 0)
                {
                    foreach (var doubleItem in ListDoubleItems)
                    {
                        if (doubleItem.Visible)
                            panelLayout.Controls.Add(new DoubleItem(doubleItem));
                    }
                }

                if (ListCollectedItems.Count > 0)
                {
                    foreach (var item in ListCollectedItems)
                    {
                        if (item.Visible)
                            panelLayout.Controls.Add(new CollectedItem(item, settings));
                    }
                }

                if (ListMedallions.Count > 0)
                {
                    foreach (var medallion in ListMedallions)
                    {
                        if (medallion.Visible)
                        {
                            var element = new Medallion(medallion, settings);
                            panelLayout.Controls.Add(element);
                            panelLayout.Controls.Add(element.SelectedDungeon);
                            ListUpdatables.Add(element);
                            element.SetSelectedDungeonLocation();
                            element.SelectedDungeon.BringToFront();
                        }
                    }
                }

                if (ListGuaranteedHints.Count > 0)
                {
                    foreach (var item in ListGuaranteedHints)
                    {
                        if (item.Visible)
                            panelLayout.Controls.Add(new GuaranteedHint(item));
                    }
                }

                if (ListGossipStones.Count > 0)
                {
                    foreach (var item in ListGossipStones)
                    {
                        if (item.Visible)
                            panelLayout.Controls.Add(new GossipStone(item, settings));
                    }
                }

                if (ListGossipStoneGrids.Count > 0)
                {
                    foreach (var item in ListGossipStoneGrids)
                    {
                        if (item.Visible)
                        {
                            for (int j = 0; j < item.Rows; j++)
                            {
                                for (int i = 0; i < item.Columns; i++)
                                {
                                    var gs = new ObjectPoint()
                                    {
                                        Id = item.Id,
                                        Name = item.Name,
                                        X = item.X + i * (item.Size.Width + item.Spacing.Width),
                                        Y = item.Y + j * (item.Size.Height + item.Spacing.Height),
                                        Size = item.Size,
                                        ImageCollection = item.ImageCollection,
                                        TinyImageCollection = item.TinyImageCollection,
                                        Visible = item.Visible,
                                    };
                                    panelLayout.Controls.Add(new GossipStone(gs, settings));
                                }
                            }
                        }
                    }
                }

                if (ListSometimesHints.Count > 0)
                {
                    foreach (var item in ListSometimesHints)
                    {
                        if (item.Visible)
                            panelLayout.Controls.Add(new SometimesHint(listSometimesHintsSuggestions, item));
                    }
                }

                if (ListChronometers.Count > 0)
                {
                    foreach (var item in ListChronometers)
                    {
                        if (item.Visible)
                            panelLayout.Controls.Add(new Chronometer(item).ChronoLabel);
                    }
                }

                if (ListPanelWotH.Count > 0)
                {
                    foreach (var item in ListPanelWotH)
                    {
                        if (item.Visible)
                        {
                            var panel = new PanelWothBarren(item, settings);
                            panel.PanelWoth(listPlacesWithTag, item);
                            panelLayout.Controls.Add(panel);
                            panelLayout.Controls.Add(panel.textBoxCustom.SuggestionContainer);
                            ListUpdatables.Add(panel);
                            panel.SetSuggestionContainer();
                        }
                    }
                }

                if (ListPanelBarren.Count > 0)
                {
                    foreach (var item in ListPanelBarren)
                    {
                        if (item.Visible)
                        {
                            var panel = new PanelWothBarren(item, settings);
                            panel.PanelBarren(listPlacesWithTag, item);
                            panelLayout.Controls.Add(panel);
                            panelLayout.Controls.Add(panel.textBoxCustom.SuggestionContainer);
                            ListUpdatables.Add(panel);
                            panel.SetSuggestionContainer();
                        }
                    }
                }

                if (ListGoMode.Count > 0)
                {
                    foreach (var item in ListGoMode)
                    {
                        if (item.Visible)
                        {
                            var element = new GoMode(item);
                            panelLayout.Controls.Add(element);
                            element.SetLocation();
                        }
                    }
                }
            }
        }
    }

    public class GenericLabel
    {
        public string Text { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int FontSize { get; set; }
        public string FontName { get; set; }
        public FontStyle FontStyle { get; set; }
        public string Color { get; set; }
        // public Size MaxSize { get; set; }
        public bool Visible { get; set; }
    }

    public class GenericTextBox
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visible { get; set; }
        public Color BackColor { get; set; }
        public int FontSize { get; set; }
        public string FontName { get; set; }
        public FontStyle FontStyle { get; set; }
        public Color FontColor { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class ObjectPoint
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Size Size { get; set; }
        public bool Visible { get; set; }
        public string[] ImageCollection { get; set; }
        public string[] TinyImageCollection { get; set; }
    }

    public class ObjectPointSong
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Size Size { get; set; }
        public bool Visible { get; set; }
        public string DragAndDropImageName { get; set; }
        public string[] ImageCollection { get; set; }
        public string[] TinyImageCollection { get; set; }
        public string ActiveSongImage { get; set; }
        public string ActiveTinySongImage { get; set; }
    }

    public class MedallionLabel
    {
        public string[] TextCollection { get; set; }
        public int? DefaultValue { get; set; }
        public bool? Wraparound { get; set; }
        public int? FontSize { get; set; }
        public string FontName { get; set; }
        public FontStyle? FontStyle { get; set; }
    }

    public class ObjectPointMedallion
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Size Size { get; set; }
        public bool Visible { get; set; }
        public string[] ImageCollection { get; set; }
        public MedallionLabel Label { get; set; }
    }

    public class ObjectPointGrid
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Columns { get; set; }
        public int Rows { get; set; }
        public Size Size { get; set; }
        public Size Spacing { get; set; }
        public bool Visible { get; set; }
        public string[] ImageCollection { get; set; }
        public string[] TinyImageCollection { get; set; }
    }

    public class AutoFillTextBox
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visible { get; set; }
        public Color BackColor { get; set; }
        public int FontSize { get; set; }
        public string FontName { get; set; }
        public FontStyle FontStyle { get; set; }
        public Color FontColor { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class ObjectPanelWotH
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visible { get; set; }
        public Color BackColor { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NbMaxRows { get; set; }
        public bool IsScrollable { get; set; }

        public string TextBoxName { get; set; }
        public Color TextBoxBackColor { get; set; }
        public string TextBoxFontName { get; set; }
        public int TextBoxFontSize { get; set; }
        public FontStyle TextBoxFontStyle { get; set; }
        public int TextBoxHeight { get; set; }
        public string TextBoxText { get; set; }

        public Color LabelColor { get; set; }
        public Color LabelBackColor { get; set; }
        public string LabelFontName { get; set; }
        public int LabelFontSize { get; set; }
        public FontStyle LabelFontStyle { get; set; }
        public int LabelHeight { get; set; }

        public Size GossipStoneSize { get; set; }
        public int? GossipStoneCount { get; set; }
        public string[] GossipStoneImageCollection { get; set; }
        public int GossipStoneSpacing { get; set; }

        public int? PathGoalCount { get; set; }
        public string[] PathGoalImageCollection { get; set; }
        public int PathGoalSpacing { get; set; }
    }

    public class ObjectPanelBarren
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visible { get; set; }
        public Color BackColor { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NbMaxRows { get; set; }
        public bool IsScrollable { get; set; }

        public string TextBoxName { get; set; }
        public Color TextBoxBackColor { get; set; }
        public string TextBoxFontName { get; set; }
        public int TextBoxFontSize { get; set; }
        public FontStyle TextBoxFontStyle { get; set; }
        public int TextBoxWidth { get; set; }
        public int TextBoxHeight { get; set; }
        public string TextBoxText { get; set; }

        public Color LabelColor { get; set; }
        public Color LabelBackColor { get; set; }
        public string LabelFontName { get; set; }
        public int LabelFontSize { get; set; }
        public FontStyle LabelFontStyle { get; set; }
        public int LabelWidth { get; set; }
        public int LabelHeight { get; set; }
    }

    public class ObjectPointGoMode
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Size Size { get; set; }
        public bool Visible { get; set; }
        public string[] ImageCollection { get; set; }
        public string BackgroundImage { get; set; }
    }

    public class ObjectPointCollectedItem
    {
        public string Name { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Size Size { get; set; }
        public Size CountPosition { get; set; }
        public int CountMax { get; set; }
        public int Step { get; set; }
        public bool Visible { get; set; }
        public string[] ImageCollection { get; set; }
        public string LabelFontName { get; set; }
        public int LabelFontSize { get; set; }
        public FontStyle LabelFontStyle { get; set; }
        public Color LabelColor { get; set; }
    }

    public class AppSettings
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public Color? BackgroundColor { get;set; }
        public string[] DefaultSongMarkerImages { get; set; } = null;
        public string[] DefaultGossipStoneImages { get; set; } = null;
        public string[] DefaultPathGoalImages { get; set; } = null; 
        public int? DefaultWothGossipStoneCount { get; set; } = null;
        public int? DefaultPathGoalCount { get; set; } = null;
        public string[] WothColors { get; set; }
        public string[] BarrenColors { get; set; }
        public int? DefaultWothColorIndex { get; set; }
        public MedallionLabel DefaultDungeonNames { get; set; } = null;
    }
}
