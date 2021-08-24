using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeneratePicturePornHub
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("输入黑底白字");
            var whiteString = Console.ReadLine();
            Console.WriteLine("输入黄底黑字");
            var blackString = Console.ReadLine();
            int fontSize;
            while (true)
            {
                Console.WriteLine("文字大小");
                if (int.TryParse(Console.ReadLine(), out fontSize))
                    break;
            }
            string path;
            while (true)
            {
                Console.WriteLine("选择存放文件夹");
                var dialog = new FolderBrowserDialog();
                var dialogResult = dialog.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    path = $"{dialog.SelectedPath}\\{whiteString}{blackString}.jpg";
                    break;
                }
            }
            var width = 1024;
            var Height = 1024;
            var bitmap = new Bitmap(width, Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;//设置图片质量
            g.Clear(Color.Black);
            var white = new SolidBrush(Color.White);
            var black = new SolidBrush(Color.Black);
            var yellow = new SolidBrush(Color.FromArgb(253, 152, 39));
            var font = new Font("雅黑", fontSize, FontStyle.Bold);
            SizeF whiteStringSize = g.MeasureString(whiteString, font);
            SizeF blackStringSize = g.MeasureString(blackString, font);
            GetCoordinate(width, Height, whiteStringSize, blackStringSize, out var parameter);
            g.FillRectangle(yellow, parameter.RectangleX, parameter.RectangleY, parameter.RectangleWidth, parameter.RectangleHeight);
            g.DrawString(whiteString, font, white, parameter.WhiteX, parameter.WhiteY);
            g.DrawString(blackString, font, black, parameter.BlackX, parameter.BlackY);
            bitmap.Save(path, ImageFormat.Jpeg);
            Console.WriteLine(":)完成!");
            Console.ReadKey();
        }

        static void GetCoordinate(int w, int h, SizeF whiteStringSize, SizeF blackStringSize, out BitmapParameter parameter)
        {
            parameter = new BitmapParameter();
            parameter.RectangleX = 4;
            parameter.RectangleY = h / 2 + parameter.RectangleX;
            parameter.RectangleWidth = w - 2 * parameter.RectangleX;
            parameter.RectangleHeight = h / 2 - 2 * parameter.RectangleX;
            parameter.WhiteX = (int)(w - whiteStringSize.Width) / 2;
            parameter.WhiteY = (int)(h / 2 - whiteStringSize.Height) / 2;
            parameter.BlackX = (int)(w - blackStringSize.Width) / 2;
            parameter.BlackY = (int)(h / 2 - blackStringSize.Height) / 2 + h / 2;
        }

        struct BitmapParameter
        {
            public int RectangleX;
            public int RectangleY;
            public int RectangleWidth;
            public int RectangleHeight;
            public int WhiteX;
            public int WhiteY;
            public int BlackX;
            public int BlackY;
        }
    }
}
