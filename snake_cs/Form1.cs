using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace snake_cs
{
    public partial class Snake : Form
    {
        Graphics g;
        Bitmap b = new Bitmap(640, 480);
        //Bitmap map = new Bitmap(640, 480);
        PointF p = new PointF();
        Image car = Properties.Resources.Car_Top_Red_icon1;
        Rectangle carRec;
        Rectangle obsRec;
        Region r, ro;

        int refreshCount = 0;

        enum d { up, right, down, left };
        int degree = 0;
        const bool D = true;
        const bool R = false;
        bool gear = D;
        int speed = 1;
        bool driving = false;
        bool leftLight = false;
        bool rightLight = false;

        public Snake()
        {
            InitializeComponent();
            KeyPreview = true;
            carRec = new Rectangle((int)p.X, (int)p.Y + 18, 64, 28);
            r = new Region(carRec);
            obsRec = new Rectangle((int)p.X + 50, (int)p.Y + 50 + 18, 64, 28);
            ro = new Region(obsRec);

        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //timer1.Start();
            //timer2.Interval = 1000 * 5;
            //timer2.Start();
            forward(degree);
            refresh();
        }

        #region timer
        private void timer1_Tick(object sender, EventArgs e)
        {
            forward(degree);
            refresh();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            GC.Collect();
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
                //timer1.Stop();
                //timer2.Stop();

        }

        #region direction
        private void button3_Click(object sender, EventArgs e)
        {
            //上
            p.Y -= 3;
            refresh();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //下
            p.Y += 3;
            refresh();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //左
            if (driving)
            {
                if (gear)
                    degree = (degree + 10) % 360;
                else
                    degree = (degree - 10) % 360;
                refresh();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //右
            if (driving)
            {
                if (gear)
                    degree = (degree - 10) % 360;
                else
                    degree = (degree + 10) % 360;
                refresh();
            }
        }
        #endregion

        private void forward(int degree)
        {
            if (gear == D)
            {
                p.X += (float)(speed * Math.Cos(degree * Math.PI / 180));
                p.Y -= (float)(speed * Math.Sin(degree * Math.PI / 180));
            }
            else
            {
                p.X -= (float)(speed * Math.Cos(degree * Math.PI / 180));
                p.Y += (float)(speed * Math.Sin(degree * Math.PI / 180));
            }
        }

        private void refresh()
        {
            Image rotateCar = RotateImageByAngle(car, degree);
            b.Dispose();
            b = new Bitmap(640, 480);
            g = Graphics.FromImage(b);
            g.DrawImage(rotateCar, p);
            //g.FillEllipse(Brushes.Red, p.X, p.Y, 10, 10);
            pictureBox1.Image = b;
            carRec.X = (int)p.X;
            carRec.Y = (int)p.Y+18;
            
            //g.DrawRectangle(Pens.Red, carRec);
            //g.DrawRectangle(Pens.Blue, obsRec);
            if (ro.IsVisible(carRec)) System.Console.WriteLine("oops");
            if (refreshCount >= 200)
            {
                GC.Collect();
                refreshCount = 0;
            }
        }
        private static Bitmap RotateImageByAngle(Image oldBitmap, float angle)
        {
            var newBitmap = new Bitmap(oldBitmap.Width, oldBitmap.Height);
            var graphics = Graphics.FromImage(newBitmap);
            graphics.TranslateTransform((float)oldBitmap.Width / 2, (float)oldBitmap.Height / 2);
            graphics.RotateTransform(-angle);
            graphics.TranslateTransform(-(float)oldBitmap.Width / 2, -(float)oldBitmap.Height / 2);
            graphics.DrawImage(oldBitmap, new Point(0, 0));

            newBitmap.SetResolution(oldBitmap.HorizontalResolution, oldBitmap.VerticalResolution);

            return newBitmap;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Properties.Resources.map1;
            pictureBox1.Show();
            label2.Show();
            tableLayoutPanel1.Hide();
            p.X = 180;
            p.Y = -30;
            degree = 270;
            refresh();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Properties.Resources.map2;
            pictureBox1.Show();
            label2.Show();
            tableLayoutPanel1.Hide();
            p.X = 0;
            p.Y = 350;
            degree = 0;
            refresh();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox1.BackgroundImage = Properties.Resources.map3;
            pictureBox1.Show();
            label2.Show();
            tableLayoutPanel1.Hide();
            p.X = 300;
            p.Y = 300;
            degree = 135;
            refresh();
        }

        private void Snake_KeyUp(object sender, KeyEventArgs e)
        {
            System.Console.WriteLine(e.KeyCode + " up");
            switch (e.KeyCode)
            {
                case Keys.D:
                    System.Console.WriteLine(e.KeyCode + " up");
                    if (driving)
                    {
                        timer1.Stop();
                        driving = false;
                    }
                    break;
                case Keys.ShiftKey:
                    gear = !gear;
                    label2.Text = (gear == D) ? "D" : "R";
                    break;
                case Keys.A:
                    LogitechGSDK.LogiLedInit();
                    LogitechGSDK.LogiLedSetLighting(0, 0, 0);
                    // get the values from the sample

                    int duration = 6000;
                    int interval = 300;
                    int redVal = 100;
                    int greenVal = 0;
                    int blueVal = 50;
                    redVal = 0;
                    greenVal = 100;
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.THREE, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.W, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.A, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.Z, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.LEFT_ALT, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.S, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.D, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.F, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.G, redVal, greenVal, blueVal, duration, interval);

                    leftLight = !leftLight;
                    button5.Visible = leftLight;
                    break;
                case Keys.S:
                    LogitechGSDK.LogiLedInit();
                    LogitechGSDK.LogiLedSetLighting(0, 0, 0);
                    // get the values from the sample

                    duration = 6000;
                    interval = 300;
                    redVal = 100;
                    greenVal = 0;
                    blueVal = 50;
                    redVal = 0;
                    greenVal = 100;
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.H, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.J, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.K, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.L, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.SEMICOLON, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.P, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.ZERO, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.PERIOD, redVal, greenVal, blueVal, duration, interval);
                    LogitechGSDK.LogiLedFlashSingleKey(keyboardNames.RIGHT_ALT, redVal, greenVal, blueVal, duration, interval);
                    rightLight = !rightLight;
                    button6.Visible = rightLight;
                    break;
                case Keys.Escape:
                    pictureBox1.Hide();
                    label2.Hide();
                    tableLayoutPanel1.Show();
                    break;
            }
        }

        private void Snake_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //MessageBox.Show("preview key down "+e.KeyCode);
            switch (e.KeyCode)
            {
                case Keys.D:
                    if (driving != true)
                    {
                        System.Console.WriteLine(e.KeyCode + " pre down");
                        driving = true;
                        timer1.Start();
                    }
                    break;
                case Keys.Left:
                    if (driving)
                    {
                        if (gear)
                            degree = (degree + 10) % 360;
                        else
                            degree = (degree - 10) % 360;
                        refresh();
                    }
                       break;
                case Keys.Right:
                    if (driving)
                    {
                        if (gear)
                            degree = (degree - 10) % 360;
                        else
                            degree = (degree + 10) % 360;
                        refresh();
                    }
                    break;
            }
        }
        
    }
}
