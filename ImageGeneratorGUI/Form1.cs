using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageGeneratorGUI
{
    public partial class Form1 : Form
    {
        public int i = 0;
        public Form1()
        {
            InitializeComponent();
            
        }

        public static void generateImage(int width, int height, PictureBox pictureBox, CheckBox checkBox)
        {
            Bitmap img = new Bitmap(width, height);
            Random rnd = new Random();
            Color clr = Color.FromArgb(rnd.Next(0,255), rnd.Next(0, 255), rnd.Next(0, 255));
            int[,] halfimg = new int[img.Width, img.Height];
            for (int y = 0; y < img.Height / 2; y++)
            {
                for (int x = 0; x < img.Width / 2; x++)
                {
                    halfimg[x, y] = rnd.Next(0, 2);
                }
            }
            for (int y = 0; y < img.Height / 2; y++)
            {
                for (int x = 0; x < img.Width / 2; x++)
                {
                    halfimg[img.Width - 1 - x, y] = halfimg[x, y];
                }
            }
            for (int y = 0; y < img.Height / 2; y++)
            {
                for (int x = 0; x < img.Width; x++)
                {
                    halfimg[x, img.Height - 1 - y] = halfimg[x, y];
                }
            }
            for (int y = 0; y < halfimg.GetLength(1); y++)
            {
                for (int x = 0; x < halfimg.GetLength(0); x++)
                {
                    if (checkBox.Checked)
                    {
                        if (halfimg[x, y] == 1)
                        {
                            img.SetPixel(x, y, Color.White);
                        }
                        else
                        {
                            img.SetPixel(x, y, clr);
                        }
                    } else
                    {
                        if (halfimg[x, y] == 1)
                        {
                            img.SetPixel(x, y, Color.White);
                        }
                        else
                        {
                            img.SetPixel(x, y, Color.Black);
                        }
                    }
                }
            }
            Bitmap fullSize = new Bitmap(800, 800);
            using (Graphics gr = Graphics.FromImage(fullSize))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.NearestNeighbor;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(img, new Rectangle(0, 0, fullSize.Width, fullSize.Height));
            }
            pictureBox.Image = fullSize;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int width = 16;
            int height = 16;
            try
            {
                width = Int32.Parse(textBox1.Text);
                height = Int32.Parse(textBox2.Text);
            }
            catch
            {
                Console.WriteLine("incorrect width and height");
            }
            generateImage(width, height, pictureBox1, checkBox1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                i++;
                pictureBox1.Image.Save("file"+i+".bmp", ImageFormat.Bmp);
            }
            catch
            {
                Console.WriteLine("Unable to save");
            }
        }
    }
}
