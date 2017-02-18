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
        Bitmap b = new Bitmap(651, 352);
        Point p = new Point();
        Image car = Properties.Resources.Car_Top_Red_icon1;

        enum d { up, down, left, right };
        int direction = (int) d.down;

        public Snake()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Interval = 1000 * 5;
            timer2.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g = Graphics.FromImage(b);
            g.DrawImage(car, p);
            //g.FillEllipse(Brushes.Red, p.X, p.Y, 10, 10);
            pictureBox1.Image = b;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch(direction)
            {
                case (int) d.up:
                    p.Y -= 3;
                    break;
                case (int) d.down:
                    p.Y += 3;
                    break;
                case (int) d.left:
                    p.X -= 3;
                    break;
                case (int) d.right:
                    p.X += 3;
                    break;
            }
            b.Dispose();
            b = new Bitmap(651, 352);
            g = Graphics.FromImage(b);
            g.DrawImage(car, p);
            //g.FillEllipse(Brushes.Red, p.X, p.Y, 10, 10);
            pictureBox1.Image = b;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            GC.Collect();
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
                timer1.Stop();
                timer2.Stop();

        }

        #region direction
        private void button3_Click(object sender, EventArgs e)
        {
            //上
            direction = (int) d.up;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //下
            direction = (int) d.down;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //左
            direction = (int) d.left;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //右
            direction = (int) d.right;
        }
        #endregion
    }
}
