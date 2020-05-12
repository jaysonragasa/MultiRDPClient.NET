using System;
using System.Text;

namespace CAPTCHA
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Windows.Forms;
    using System.ComponentModel;
    using System.Security.Cryptography;

    public class Captcha : PictureBox
    {
        private Random random = new Random(1000);
        private string text = string.Empty;

        public Captcha()
        {
            text = GetRandomPassword(8);
            GenerateImage();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GenerateImage();
        }

        [Category("Appearance")]
        [Browsable(true)]
        public string RandomText
        {
            set
            {
                text = value;
                GenerateImage();
            }
            get
            {
                return this.text;
            }
        }

        // ====================================================================
        
        // CAPTCHA Image
        // http://www.codeproject.com/KB/aspnet/CaptchaImage.aspx
        // Note by me(Nullstring)
        // I modified it a little bit to fit on this component

        // Creates the bitmap image.

        // ====================================================================

        private void GenerateImage()
        {
            // Create a new 32-bit bitmap image.

            Bitmap bitmap = new Bitmap(
              this.Width,
              this.Height,
              PixelFormat.Format32bppArgb);

            // Create a graphics object for drawing.

            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);

            // Fill in the background.

            HatchBrush hatchBrush = new HatchBrush(
              HatchStyle.SmallConfetti,
              Color.LightGray,
              Color.White);
            g.FillRectangle(hatchBrush, rect);

            // Set up the text font.

            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            // Adjust the font size until the text fits within the image.

            do
            {
                fontSize--;
                font = new Font(
                  new FontFamily("Arial"),
                  fontSize,
                  FontStyle.Bold);
                size = g.MeasureString(this.text, font);
            } while (size.Width > rect.Width);

            // Set up the text format.

            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            // Create a path using the text and warp it randomly.

            GraphicsPath path = new GraphicsPath();
            path.AddString(
              this.text,
              font.FontFamily,
              (int)font.Style,
              font.Size, rect,
              format);
            float v = 4F;
            PointF[] points =
      {
        new PointF(
          this.random.Next(rect.Width) / v,
          this.random.Next(rect.Height) / v),
        new PointF(
          rect.Width - this.random.Next(rect.Width) / v,
          this.random.Next(rect.Height) / v),
        new PointF(
          this.random.Next(rect.Width) / v,
          rect.Height - this.random.Next(rect.Height) / v),
        new PointF(
          rect.Width - this.random.Next(rect.Width) / v,
          rect.Height - this.random.Next(rect.Height) / v)
      };
            Matrix matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);

            // Draw the text.

            hatchBrush = new HatchBrush(
              HatchStyle.LargeConfetti,
              Color.LightGray,
              Color.DarkGray);
            g.FillPath(hatchBrush, path);

            // Add some random noise.

            int m = Math.Max(rect.Width, rect.Height);
            for (int i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                int x = this.random.Next(rect.Width);
                int y = this.random.Next(rect.Height);
                int w = this.random.Next(m / 50);
                int h = this.random.Next(m / 50);
                g.FillEllipse(hatchBrush, x, y, w, h);
            }

            // Clean up.

            font.Dispose();
            hatchBrush.Dispose();
            g.Dispose();

            // Set the image.

            this.Image = bitmap;
        }

        public void Renew()
        {
            text = GetRandomPassword(8);
            GenerateImage();
        }

        public string GetRandomPassword(int length)
        {
            char[] chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            string password = string.Empty;

            for (int i = 0; i < length; i++)
            {
                int x = random.Next(1, chars.Length);
                //Don't Allow Repetation of Characters
                if (!password.Contains(chars.GetValue(x).ToString()))
                    password += chars.GetValue(x);
                else
                    i--;
            }
            return password;
        }
    }
}
