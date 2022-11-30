using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace assiment2_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
       
        }
        private Bitmap imgDefault = null;
        private Bitmap img = null;
        private int l1;
        private int l2;
        private int l3;
        private int l4;
        private int l5;
        private int l6;

        public event EventHandler TextChanged;

       //Color red = Color.FromArgb(255, 0, 0);
       //Color green = Color.FromArgb(0, 255, 0);
       //Color blue = Color.FromArgb(0, 0, 255);
       //Color yellow = Color.FromArgb(255, 255, 0);
       //Color cyan = Color.FromArgb(0, 255, 255);
       //Color magenta = Color.FromArgb(255, 0, 255);


        private void openBtn(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "bitmap (*.bmp)|*.bmp";
            openFileDialog1.FilterIndex = 1;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if (imgDefault != null)
                        imgDefault.Dispose();
                    imgDefault = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName, false);

                }
                catch (Exception)
                {
                    MessageBox.Show("Can not open file", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            img = new Bitmap(imgDefault.Width, imgDefault.Height);
            //image pixel

            for (int i = 0; i < imgDefault.Width; i++)
            {
                for (int j = 0; j < imgDefault.Height; j++)
                {
                    Color PixelColor = imgDefault.GetPixel(i, j);
                    int gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                    img.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            pictureBox1.Image = imgDefault;
            pictureBox2.Image = img;


        }

        //RGB with 6 level
        private void intensitySlicingButton(object sender, EventArgs e)
        {
            Bitmap img = new Bitmap(imgDefault.Width, imgDefault.Height);

            //image pixel
            for (int i = 0; i < imgDefault.Width; i++)
            {
                for (int j = 0; j < imgDefault.Height; j++)
                {
                    Color PixelColor = imgDefault.GetPixel(i, j);

                    int gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                    if (gray <= l1)
                        img.SetPixel(i, j, Color.FromArgb(255, 0, 0)); //R
                    else if (gray <= l2)
                        img.SetPixel(i, j, Color.FromArgb(0, 255, 0)); //G
                    else if (gray <= l3)
                        img.SetPixel(i, j, Color.FromArgb(0, 0, 255)); //B
                    else if (gray <= l4)
                        img.SetPixel(i, j, Color.FromArgb(0, 255, 255)); //C
                    else if (gray <= l5)
                        img.SetPixel(i, j, Color.FromArgb(255, 0, 255)); //M
                    else if (gray <= l6)
                        img.SetPixel(i, j, Color.FromArgb(255, 255, 0)); //Y

                }
            }
            pictureBox2.Image = img;

            //histrogram

            int[] hist = new int[256];

            for (int i = 0; i < imgDefault.Width; i++)
            {
                for (int j = 0; j < imgDefault.Height; j++)
                {
                    Color PixelColor = imgDefault.GetPixel(i, j);
                    int gray = (int)(PixelColor.R + PixelColor.G + PixelColor.B) / 3;
                    hist[gray]++;

                    chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                    chart1.ChartAreas[0].AxisX.Title = "Gray Level";
                    chart1.ChartAreas[0].AxisY.Title = "No. of Pixels";
                    //chart1.ChartAreas[0].AxisX.Maximum = 255;
                    chart1.ChartAreas[0].AxisX.Minimum = 0;
                    chart1.Series[0].Points.AddXY(gray, hist[gray]);
                }
            }

           
            
            
        }

        private void redScale(object sender, EventArgs e)
        {
            l1 = int.Parse(textBox1.Text);
        }

        private void greenScale(object sender, EventArgs e)
        {
            l2 = int.Parse(textBox2.Text);
        }

        private void blueScale(object sender, EventArgs e)
        {
            l3 = int.Parse(textBox3.Text);
        }
    

        private void cyanScale(object sender, EventArgs e)
        {
            l4 = int.Parse(textBox4.Text);
        }


        private void magentaScale(object sender, EventArgs e)
        {
            l5 = int.Parse(textBox5.Text);
        }

        

        private void yellowScale(object sender, EventArgs e)
        {
            l6 = int.Parse(textBox6.Text);
        }
        
    }
}
