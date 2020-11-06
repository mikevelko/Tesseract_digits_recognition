
using ImageMagick;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tesseract_WinForms
{
    public partial class Form1 : Form
    {
        public Bitmap CurrentImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ImageLocation;
            try 
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpg files(.*jpg)|*.jpg| PNG files(.*png)|*.png| All Files(*.*)|*.*";

                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
                {
                    ImageLocation = dialog.FileName;
                    pictureBox1.ImageLocation = ImageLocation;
                }

                
            }
            catch (Exception)
            {
                MessageBox.Show(" error in uploading pic", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            string ImageLocation;
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "eps files(.*eps)|*.eps";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    ImageLocation = dialog.FileName;
                    using (MagickImage image = new MagickImage(@ImageLocation))
                    {
                        image.Format = MagickFormat.Png;
                        image.Write(@"./2.png");
                    }
                    pictureBox1.Image = Image.FromFile("2.png");
                }


            }
            catch (Exception)
            {
                MessageBox.Show(" error in uploading pic", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(CurrentImage==null) CurrentImage = new Bitmap(pictureBox1.Image);
            if (pictureBox1.Image != null) 
            {
                Bitmap image = AdjustThreshold(new Bitmap(CurrentImage),(float)(numericUpDown2.Value/100));
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }
        }
        private Bitmap AdjustThreshold(Image image, float threshold)
        {
            // Make the result bitmap.
            Bitmap bm = new Bitmap(image.Width, image.Height);

            // Make the ImageAttributes object and set the threshold.
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetThreshold(threshold);

            // Draw the image onto the new bitmap
            // while applying the new ColorMatrix.
            Point[] points =
            {
        new Point(0, 0),
        new Point(image.Width, 0),
        new Point(0, image.Height),
    };
            Rectangle rect =
                new Rectangle(0, 0, image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(image, points, rect,
                    GraphicsUnit.Pixel, attributes);
            }

            // Return the result.
            return bm;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap image = new Bitmap(pictureBox1.Image);
            image.Save("1.png");
            int psm = (int)numericUpDown1.Value;
            StringBuilder command = new StringBuilder();
            command.Append("tesseract 1.png 1 --psm ");
            command.Append(psm.ToString());

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine(command.ToString());
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());

            string image_1 = File.ReadAllText(@".\1.txt");
            if(image_1.Length > 1) image_1 = image_1.Remove(image_1.Length - 1);
            textBox1.Text = image_1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
