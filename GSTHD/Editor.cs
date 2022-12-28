using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using GSTHD.EditorObjects;

namespace GSTHD
{
    /*
    public partial class Editor : Form
    {
        Layout CurrentLayout = new Layout();

        public Editor()
        {
            InitializeComponent();
        }

        public Editor(Layout layout)
        {
            CurrentLayout = layout;

            this.SuspendLayout();
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(layout.App_Settings.Width, layout.App_Settings.Height);
            this.Size = new Size(layout.App_Settings.Width, layout.App_Settings.Height);
            this.BackColor = layout.App_Settings.BackgroundColor;
            this.Name = "Editor";
            this.Text = "Editor";
            this.Load += new System.EventHandler(this.Editor_Load);
            this.ResumeLayout(false);

            this.KeyDown += Editor_KeyDown;
        }

        private void Editor_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.S && e.Control)
            {
                string file = string.Empty;
                if (CurrentLayout.ListItems.Count > 0)
                {
                    
                    foreach (var item in CurrentLayout.ListItems)
                    {
                        file += JsonConvert.SerializeObject(item, Formatting.Indented) + ",\n";
                    }
                }
                File.WriteAllText(@"Layouts/" + DateTime.Now.ToString().Replace('/', '-').Replace(' ', '-').Replace(':', '-') + ".json", file);
            }
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            if (CurrentLayout.ListItems.Count > 0)
            {
                foreach (var item in CurrentLayout.ListItems)
                {
                    this.Controls.Add(new JSONItem(item));
                }
            }
        }


    }
    */
}
