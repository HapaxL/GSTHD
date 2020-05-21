using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerOOT
{
    class Chronometer : Stopwatch
    {
        public Label ChronoLabel;
        bool ChronoRunning = false;
        Timer ChronoTimer;

        public Chronometer(ObjectPointLabel data)
        {
            this.ChronoTimer = new Timer();
            this.ChronoTimer.Interval = 10;
            this.ChronoTimer.Tick += new EventHandler(this.timer_Tick);
            this.ChronoTimer.Start();

            ChronoLabel = new Label
            {
                BackColor = Color.Transparent,
                AutoSize = true,
                Font = new Font(data.FontName, data.FontSize, data.FontStyle),
                ForeColor = data.FontColor,
                Location = new Point(data.X, data.Y),
                Name = "label_Chrono",
                Size = new Size(data.Width, data.Height),
                TabStop = false,
                Text = "00:00:00.00",
                TextAlign = ContentAlignment.MiddleRight
            };
            this.ChronoLabel.MouseClick += Label_Chrono_MouseClick;

            
        }

        private void Label_Chrono_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (ChronoRunning)
                {
                    this.Stop();
                }
                else
                {
                    this.Start();
                }
                ChronoRunning = !ChronoRunning;
            }
            if (e.Button == MouseButtons.Right)
            {
                ChronoRunning = false;
                this.Reset();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            TimeSpan time = this.Elapsed;
            ChronoLabel.Text = time.ToString(@"hh\:mm\:ss\.ff");
        }
    }
}