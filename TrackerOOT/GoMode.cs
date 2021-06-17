using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TrackerOOT
{
    class GoMode : PictureBox
    {
        List<string> ListImageName;
        string BackgroundImage;        
        Point FirstLocation;
        Timer tictac;
        int imageIndex = 0;

        public PictureBox GoModeImage = new PictureBox();
        Size GoModeImageSize;

        public GoMode(ObjectPointGoMode data)
        {
            tictac = new Timer();
            tictac.Interval = 95;
            tictac.Tick += Tictac_Tick;
            tictac.Start();

            if (data.ImageCollection != null)
            {
                this.ListImageName = data.ImageCollection.ToList();
            }

            GoModeImageSize = data.Size;

            this.FirstLocation = new Point(data.X, data.Y);

            if(ListImageName.Count > 0)
            {
                GoModeImage.Name = ListImageName[0];
                GoModeImage.Image = Image.FromFile(@"Resources/" + ListImageName[0]);
                GoModeImage.SizeMode = PictureBoxSizeMode.StretchImage;
                GoModeImage.Size = this.GoModeImageSize;
            }                        
            GoModeImage.BackColor = Color.Transparent;
            // GoModeImage.MouseUp += Click_MouseUp;
            GoModeImage.MouseDown += Click_MouseDown;

            if (data.BackgroundImage != string.Empty)
            {
                BackgroundImage = data.BackgroundImage;
                this.Name = data.BackgroundImage;
            }
            this.BackColor = Color.Transparent;
            this.Controls.Add(GoModeImage);
        }

        private void Tictac_Tick(object sender, EventArgs e)
        {
            if (this.Image != null)
            {
                Image flipImage = this.Image;
                flipImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
                this.Image = flipImage;
            }

        }

        //Copy/Paste from the web, need to test and adjust
        // https://stackoverflow.com/questions/2163829/how-do-i-rotate-a-picture-in-winforms
        private Image RotateImage(Image img, float rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        private void Click_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && imageIndex < ListImageName.Count - 1)
            {
                imageIndex += 1;
            }

            if (e.Button == MouseButtons.Right && imageIndex > 0)
            {
                imageIndex -= 1;
            }

            var pbox = (PictureBox)sender;
            pbox.Image = Image.FromFile(@"Resources/" + ListImageName[imageIndex]);
            pbox.Name = ListImageName[imageIndex];
            pbox.Size = GoModeImageSize;
            

            if (imageIndex <= 0)
            {
                this.Name = "NoImage";
                this.Image = null;
            }
            else
            {
                this.Name = BackgroundImage;
                this.Image = Image.FromFile(@"Resources/" + BackgroundImage);
                this.Size = new Size(this.Image.Width, this.Image.Height);
            }

            SetLocation();
        }

        public void SetLocation()
        {
            GoModeImage.Location = new Point(this.Width / 2 - GoModeImage.Width / 2, this.Height / 2 - GoModeImage.Height / 2);
            this.Location = new Point(this.FirstLocation.X - GoModeImage.Location.X, this.FirstLocation.Y - GoModeImage.Location.Y);
        }
    }
}
