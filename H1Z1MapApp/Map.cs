using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Resources;
namespace H1Z1MapApp
{
    public partial class Map : Form
    {
        private static  int MaxPins = 10;

        private double maganfication = 0.15;
        private int DisplayXShift;
        private int DisplayZShift;
        private int MapWidth;
        private int MapHeight;

        Rectangle mapBounding;
        FontFamily fontFamily = new FontFamily("Arial");
        Font font;
        Rectangle[] pinrecs = new Rectangle[MaxPins];
        Point[] pin = new Point[MaxPins];
        SolidBrush[] colours = new SolidBrush[MaxPins];

        public string[] PinNames = new string[MaxPins];
        public int NumberOfPins { get { return MaxPins; } }
        private Image map;


        public Map(String[] args)
        {
            font = new Font(
            fontFamily,
            16,
            FontStyle.Regular,
            GraphicsUnit.Pixel
            );

            InitializeComponent();
            loadArgs(args);

            for (int i = 0; i < MaxPins; i++)
            {
                PinNames[i] = "Pin " + (i + 1);
                pin[i] = new Point();
                pinrecs[i] = new System.Drawing.Rectangle(((int)Math.Round(pin[i].X * maganfication) + DisplayZShift) - 3, ((int)Math.Round(pin[i].Y * maganfication * -1) + DisplayXShift) - 3, 6, 6);
                switch (i % 5)
                {
                    case 0:
                        colours[i] = new System.Drawing.SolidBrush(System.Drawing.Color.LightGreen);
                        break;
                    case 1:
                        colours[i] = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
                        break;
                    case 2:
                        colours[i] = new System.Drawing.SolidBrush(System.Drawing.Color.Orange);
                        break;
                    case 3:
                        colours[i] = new System.Drawing.SolidBrush(System.Drawing.Color.HotPink);
                        break;
                    case 4:
                        colours[i] = new System.Drawing.SolidBrush(System.Drawing.Color.LightSkyBlue);
                        break;
                }
            }

            System.Resources.ResourceManager resources =
            new System.Resources.ResourceManager(typeof(Map));
            map = Bitmap.FromFile(".\\Img\\newmap.png");


        }
        private void loadArgs(String[] args)
        {
            bool sreenSet = false;
            foreach (string arg in args)
            {
                if (arg.StartsWith("-p"))
                {
                    MaxPins = Convert.ToInt32(arg.Substring(2));
                }
                if (arg.StartsWith("-s"))
                {
                    int size = Convert.ToInt32(arg.Substring(2));
                    if (size > 100 && size < 4000)
                    {
                        SetMapSize(size);
                        sreenSet = true;
                    }
                }
            }

            if (!sreenSet)
            {
                Resize(0);
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void Resize(int index)
        {
            switch (index)
            {
                case 0:
                    SetMapSize(1400);
                    break;
                case 1:
                    SetMapSize(1200);
                    break;
                case 2:
                    SetMapSize(1000);
                    break;
                case 3:
                    SetMapSize(850);
                    break;

            }

            for (int i = 0; i < pin.Length; i++)
            {
                pinrecs[i] = new System.Drawing.Rectangle(((int)Math.Round(pin[i].X * maganfication) + DisplayZShift) - 3, ((int)Math.Round(pin[i].Y * maganfication * -1) + DisplayXShift) - 3, 6, 6);
            }
            

        }
        private void SetMapSize(int size)
        {
            maganfication = (float)size / 8000f;
            MapWidth = size;
            MapHeight = size;
            DisplayXShift = MapWidth / 2;
            DisplayZShift = MapHeight / 2;
            this.Size = new System.Drawing.Size(MapWidth, MapHeight);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DrawPoint();

        }
        public void SetPoint(int numb, int X, int Y)
        {
            pin[numb].X = X;
            pin[numb].Y = Y;
            pinrecs[numb] = new System.Drawing.Rectangle(((int)Math.Round(pin[numb].X * maganfication) + DisplayZShift) - 3, ((int)Math.Round(pin[numb].Y * maganfication * -1) + DisplayXShift) - 3, 6, 6);
        }
        public void DrawPoint()
        {

            System.Drawing.Graphics formGraphics;
            formGraphics = this.CreateGraphics();
            formGraphics.DrawImage(map, 0, 0, MapHeight, MapHeight);
            for (int i = 0; i < MaxPins; i++)
            {
                formGraphics.DrawString(PinNames[i], font, colours[i], new Point(20, 20 + (i * 20)));
                formGraphics.FillRectangle(colours[i], pinrecs[i]);
            }
            formGraphics.Dispose();
        }
    }
}
