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

    public class Layout : Panel
    {
        public List<UpdatableFromSettings> ListUpdatables;
        public string FilePath;
        public JObject Json;
        public LayoutSettings Settings;

        public Layout(string filePath)
        {
            ListUpdatables = new List<UpdatableFromSettings>();
            FilePath = filePath;
            try
            {
                Json = JObject.Parse(File.ReadAllText(FilePath));
            }
            catch (JsonReaderException ex)
            {
                throw new InvalidLayoutFileException(FilePath, "invalid JSON, or not a JSON file", ex);
            }
            LoadSettings();
            SetSizeFromSettings();
        }

        public void UpdateFromSettings()
        {
            foreach (var updatable in ListUpdatables)
            {
                updatable.UpdateFromSettings();
            }
        }

        private void LoadSettings()
        {
            JToken settingsToken;
            if (Json.ContainsKey("LayoutSettings"))
            {
                settingsToken = Json.GetValue("LayoutSettings");
            }
            else if (Json.ContainsKey("AppSize"))
            {
                settingsToken = Json.GetValue("AppSize");
            }
            else
            {
                throw new InvalidLayoutFileException(FilePath, "no LayoutSettings section");
            }
            Settings = JsonConvert.DeserializeObject<LayoutSettings>(settingsToken.ToString());
        }

        private void SetSizeFromSettings()
        {
            if (Settings.Width == 0 || Settings.Height == 0)
            {
                throw new InvalidLayoutFileException(FilePath, "invalid size");
            }
            Size = new Size(Math.Max(144, Settings.Width), Math.Max(80, Settings.Height));
        }

        public void LoadContents(Settings activeSettings, SortedSet<string> listSometimesHintsSuggestions, Dictionary<string, string> listPlacesWithTag, MainForm form)
        {
            activeSettings.SetLayoutSettings(Settings);

            foreach (var category in Json)
            {
                switch (category.Key)
                {
                    case "Labels":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<GenericLabel>(element.ToString());

                            if (obj.Visible)
                            {
                                Controls.Add(new Label()
                                {
                                    Text = obj.Text,
                                    Left = obj.X,
                                    Top = obj.Y,
                                    Font = new Font(new FontFamily(obj.FontName), obj.FontSize, obj.FontStyle),
                                    ForeColor = Color.FromName(obj.Color),
                                    BackColor = Color.Transparent,
                                    AutoSize = true,
                                });
                            }
                        }
                        break;

                    case "TextBoxes":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<GenericTextBox>(element.ToString());

                            if (obj.Visible)
                            {
                                Controls.Add(new TextBox()
                                {
                                    BackColor = obj.BackColor,
                                    Font = new Font(obj.FontName, obj.FontSize, obj.FontStyle),
                                    ForeColor = obj.FontColor,
                                    Size = new Size(obj.Width, obj.Height),
                                    Location = new Point(obj.X, obj.Y),
                                });
                            }
                        }
                        break;

                    case "Items":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<ObjectPoint>(element.ToString());

                            if (obj.Visible)
                            {
                                Controls.Add(new Item(obj, activeSettings));
                            }
                        }
                        break;

                    case "Songs":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<ObjectPointSong>(element.ToString());
                            if (obj.Visible)
                            {
                                var song = new Song(obj, activeSettings);
                                Controls.Add(song);
                                ListUpdatables.Add(song);
                            }
                        }
                        break;

                    case "DoubleItems":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<ObjectPoint>(element.ToString());
                            if (obj.Visible)
                                Controls.Add(new DoubleItem(obj));
                        }
                        break;

                    case "CollectedItems":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<ObjectPointCollectedItem>(element.ToString());
                            if (obj.Visible)
                                Controls.Add(new CollectedItem(obj, activeSettings));
                        }
                        break;

                    case "Medallions":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<ObjectPointMedallion>(element.ToString());
                            if (obj.Visible)
                            {
                                var medallion = new Medallion(obj, activeSettings);
                                Controls.Add(medallion);
                                Controls.Add(medallion.SelectedDungeon);
                                ListUpdatables.Add(medallion);
                                medallion.SetSelectedDungeonLocation();
                                medallion.SelectedDungeon.BringToFront();
                            }
                        }
                        break;

                    case "GuaranteedHints":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<ObjectPoint>(element.ToString());
                            if (obj.Visible)
                                Controls.Add(new GuaranteedHint(obj));
                        }
                        break;

                    case "GossipStones":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<ObjectPoint>(element.ToString());
                            if (obj.Visible)
                                Controls.Add(new GossipStone(obj, activeSettings));
                        }
                        break;

                    case "GossipStoneGrids":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<ObjectPointGrid>(element.ToString());
                            if (obj.Visible)
                            {
                                for (int j = 0; j < obj.Rows; j++)
                                {
                                    for (int i = 0; i < obj.Columns; i++)
                                    {
                                        var gs = new ObjectPoint()
                                        {
                                            Id = obj.Id,
                                            Name = obj.Name,
                                            X = obj.X + i * (obj.Size.Width + obj.Spacing.Width),
                                            Y = obj.Y + j * (obj.Size.Height + obj.Spacing.Height),
                                            Size = obj.Size,
                                            ImageCollection = obj.ImageCollection,
                                            TinyImageCollection = obj.TinyImageCollection,
                                            Visible = obj.Visible,
                                        };
                                        Controls.Add(new GossipStone(gs, activeSettings));
                                    }
                                }
                            }
                        }
                        break;

                    case "SometimesHints":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<AutoFillTextBox>(element.ToString());
                            if (obj.Visible)
                                Controls.Add(new SometimesHint(listSometimesHintsSuggestions, obj));
                        }
                        break;

                    case "Chronometers":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<AutoFillTextBox>(element.ToString());
                            if (obj.Visible)
                                Controls.Add(new Chronometer(obj).ChronoLabel);
                        }
                        break;

                    case "PanelWoth":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<ObjectPanelWotH>(element.ToString());
                            if (obj.Visible)
                            {
                                var panel = new PanelWothBarren(obj, activeSettings);
                                panel.PanelWoth(listPlacesWithTag, obj);
                                Controls.Add(panel);
                                Controls.Add(panel.textBoxCustom.SuggestionContainer);
                                ListUpdatables.Add(panel);
                                panel.SetSuggestionContainer();
                            }
                        }
                        break;

                    case "PanelBarren":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<ObjectPanelBarren>(element.ToString());
                            if (obj.Visible)
                            {
                                var panel = new PanelWothBarren(obj, activeSettings);
                                panel.PanelBarren(listPlacesWithTag, obj);
                                Controls.Add(panel);
                                Controls.Add(panel.textBoxCustom.SuggestionContainer);
                                ListUpdatables.Add(panel);
                                panel.SetSuggestionContainer();
                            }
                        }
                        break;

                    case "GoMode":
                        foreach (var element in category.Value)
                        {
                            var obj = JsonConvert.DeserializeObject<ObjectPointGoMode>(element.ToString());
                            if (obj.Visible)
                            {
                                var gomode = new GoMode(obj);
                                Controls.Add(gomode);
                                gomode.SetLocation();
                            }
                        }
                        break;
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
        public int CountMin { get; set; }
        public int? CountMax { get; set; }
        public int DefaultValue { get; set; }
        public int Step { get; set; }
        public bool Visible { get; set; }
        public string[] ImageCollection { get; set; }
        public string LabelFontName { get; set; }
        public int LabelFontSize { get; set; }
        public FontStyle LabelFontStyle { get; set; }
        public Color LabelColor { get; set; }
    }

    public class LayoutSettings
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

    public class InvalidLayoutFileException : GSTHDException
    {
        private static string GetMessagePrefix(string filePath)
        {
            return $"Provided file \"{filePath}\" is not a valid layout file";
        }

        private static string GenericMessagePrefix = $"Provided file is not a valid layout file";

        public InvalidLayoutFileException(string fileName, string message)
            : base(Config.LayoutFileExceptionTitle, $"{GetMessagePrefix(fileName)}: {message}.") { }
        public InvalidLayoutFileException(string fileName, string message, Exception inner)
            : base(Config.LayoutFileExceptionTitle, $"{GetMessagePrefix(fileName)}: {message}.", inner) { }

        public InvalidLayoutFileException(string message)
            : base(Config.LayoutFileExceptionTitle, $"{GenericMessagePrefix}: {message}.") { }
        public InvalidLayoutFileException(string message, Exception inner)
            : base(Config.LayoutFileExceptionTitle, $"{GenericMessagePrefix}: {message}.", inner) { }
    }
}
