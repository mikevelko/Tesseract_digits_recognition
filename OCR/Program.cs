using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using tessnet2;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;

namespace OCR
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap Resize(Bitmap bmp, int newWidth, int newHeight)
            {

                Bitmap temp = (Bitmap)bmp;

                Bitmap bmap = new Bitmap(newWidth, newHeight, temp.PixelFormat);

                double nWidthFactor = (double)temp.Width / (double)newWidth;
                double nHeightFactor = (double)temp.Height / (double)newHeight;

                double fx, fy, nx, ny;
                int cx, cy, fr_x, fr_y;
                Color color1 = new Color();
                Color color2 = new Color();
                Color color3 = new Color();
                Color color4 = new Color();
                byte nRed, nGreen, nBlue;

                byte bp1, bp2;

                for (int x = 0; x < bmap.Width; ++x)
                {
                    for (int y = 0; y < bmap.Height; ++y)
                    {

                        fr_x = (int)Math.Floor(x * nWidthFactor);
                        fr_y = (int)Math.Floor(y * nHeightFactor);
                        cx = fr_x + 1;
                        if (cx >= temp.Width) cx = fr_x;
                        cy = fr_y + 1;
                        if (cy >= temp.Height) cy = fr_y;
                        fx = x * nWidthFactor - fr_x;
                        fy = y * nHeightFactor - fr_y;
                        nx = 1.0 - fx;
                        ny = 1.0 - fy;

                        color1 = temp.GetPixel(fr_x, fr_y);
                        color2 = temp.GetPixel(cx, fr_y);
                        color3 = temp.GetPixel(fr_x, cy);
                        color4 = temp.GetPixel(cx, cy);

                        // Blue
                        bp1 = (byte)(nx * color1.B + fx * color2.B);

                        bp2 = (byte)(nx * color3.B + fx * color4.B);

                        nBlue = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        // Green
                        bp1 = (byte)(nx * color1.G + fx * color2.G);

                        bp2 = (byte)(nx * color3.G + fx * color4.G);

                        nGreen = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        // Red
                        bp1 = (byte)(nx * color1.R + fx * color2.R);

                        bp2 = (byte)(nx * color3.R + fx * color4.R);

                        nRed = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        bmap.SetPixel(x, y, System.Drawing.Color.FromArgb
                (255, nRed, nGreen, nBlue));
                    }
                }
                bmap = SetGrayscale(bmap);
                bmap = RemoveNoise(bmap);

                return bmap;

            }
            Bitmap SetGrayscale(Bitmap img)
            {

                Bitmap temp = img;
                Bitmap bmap = (Bitmap)temp.Clone();
                Color c;
                for (int i = 0; i < bmap.Width; i++)
                {
                    for (int j = 0; j < bmap.Height; j++)
                    {
                        c = bmap.GetPixel(i, j);
                        byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

                        bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                    }
                }
                return (Bitmap)bmap.Clone();

            }
            Bitmap RemoveNoise(Bitmap bmap)
            {

                for (var x = 0; x < bmap.Width; x++)
                {
                    for (var y = 0; y < bmap.Height; y++)
                    {
                        var pixel = bmap.GetPixel(x, y);
                        if (pixel.R < 162 && pixel.G < 162 && pixel.B < 162)
                            bmap.SetPixel(x, y, Color.Black);
                        else if (pixel.R > 162 && pixel.G > 162 && pixel.B > 162)
                            bmap.SetPixel(x, y, Color.White);
                    }
                }

                return bmap;
            }
            //Bitmap image = new Bitmap(@".\train_1.PNG");
            var ocr = new Tesseract();
            ocr.Init(@"C:\Users\Mike\source\repos\OCR\OCR\Content\tessdata", "eng", true);

            Bitmap image = ocr.GetThresholdedImage(new Bitmap(@".\train_1.PNG"), Rectangle.Empty);
            var result = ocr.DoOCR(image, Rectangle.Empty);
            Console.WriteLine("Train_1 by tessnet");
            foreach (Word word in result)
            {
                Console.Write(word.Text + " ");
            }
            Console.WriteLine();
            image.Save("1.png");

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("tesseract 1.png 1 --psm 6");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());

            image = ocr.GetThresholdedImage(new Bitmap(@".\train_2.PNG"), Rectangle.Empty);
            result = ocr.DoOCR(image, Rectangle.Empty);
            Console.WriteLine("Train_2 by tessnet");
            foreach (Word word in result)
            {
                Console.Write(word.Text + " ");
            }
            Console.WriteLine();
            image.Save("2.png");


            cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("tesseract 2.png 2 --psm 6");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            
            // 3 image

            image = ocr.GetThresholdedImage(new Bitmap(@".\train_3.PNG"), Rectangle.Empty);
            result = ocr.DoOCR(image, Rectangle.Empty);
            Console.WriteLine("Train_3 by tessnet");
            foreach (Word word in result)
            {
                Console.Write(word.Text + " ");
            }
            Console.WriteLine();
            image.Save("3.png");


            cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("tesseract 3.png 3 --psm 6");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());


            //4 image
            image = ocr.GetThresholdedImage(new Bitmap(@".\train_4.PNG"), Rectangle.Empty);
            result = ocr.DoOCR(image, Rectangle.Empty);
            Console.WriteLine("Train_4 by tessnet");
            foreach (Word word in result)
            {
                Console.Write(word.Text + " ");
            }
            Console.WriteLine();
            image.Save("4.png");


            cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("tesseract 4.png 4 --psm 6");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());

            //image 5
            image = ocr.GetThresholdedImage(new Bitmap(@".\train_5.PNG"), Rectangle.Empty);
            result = ocr.DoOCR(image, Rectangle.Empty);
            Console.WriteLine("Train_5 by tessnet");
            foreach (Word word in result)
            {
                Console.Write(word.Text + " ");
            }
            Console.WriteLine();
            image.Save("5.png");


            cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.StandardInput.WriteLine("tesseract 5.png 5 --psm 6");
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());


            string image_1 = File.ReadAllText(@".\1.txt");
            string image_2 = File.ReadAllText(@".\2.txt");
            string image_3 = File.ReadAllText(@".\3.txt");
            string image_4 = File.ReadAllText(@".\4.txt");
            string image_5 = File.ReadAllText(@".\5.txt");
            Console.WriteLine("\n=============================================\n"+ "Train_1 image by tesseract\n" + image_1);
            Console.WriteLine("\n=============================================\n" + "Train_1 image by tesseract\n" + image_2);
            Console.WriteLine("\n=============================================\n" + "Train_1 image by tesseract\n" + image_3);
            Console.WriteLine("\n=============================================\n" + "Train_1 image by tesseract\n" + image_4);
            Console.WriteLine("\n=============================================\n" + "Train_1 image by tesseract\n" + image_5);
            Console.ReadLine();
        }


    }
}
