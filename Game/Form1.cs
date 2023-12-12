using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Form1 : Form
    {
        private Rectangle rect1, rect2;
        private SolidBrush RedBrush = new SolidBrush(Color.Red), RdBrush = new SolidBrush(Color.Blue);
        private int NumOfCircle = 5;
        private Random random = new Random();
        private Graphics g;
        private double time = 15, score = 0;
        private bool IsPress = false;
        public Form1()
        {
            InitializeComponent();
            rect1 = new Rectangle(0, 0, this.Height / 8, this.Height / 8);
            rect2 = new Rectangle(0, 0, this.Height / 8, this.Height / 8);
            g = splitContainer1.Panel2.CreateGraphics();
            lbtime.Text = "Time: " + time;
        }

        private void RandomBrush()
        {
            int i = random.Next(0, 2);
            switch (i)
            {
                case 0:
                    RdBrush.Color = Color.Blue;
                    break;
                case 1:
                    RdBrush.Color = Color.Green;
                    break;
                case 2:
                    RdBrush.Color = Color.Yellow;
                    break;
                default:
                    RdBrush.Color = Color.Blue;
                    break;
            }
        }

        private void splitContainer1_Panel2_MouseClick(object sender, MouseEventArgs e)
        {
            
            double d = Math.Sqrt(Math.Pow(e.Location.X - rect1.X - (rect1.Width / 2), 2) + Math.Pow(e.Location.Y - rect1.Y - (rect1.Width / 2), 2));
            if (d < rect1.Width / 2 && !IsPress)
            {
                score++;
                lbscore.Text = "Score: " + score;
                IsPress = true;
            }
        }

        private void RandomDraw()
        {
            for (int i = 0; i < NumOfCircle - 1; i++)
            {
               
                rect2.X = random.Next(0, this.Width - this.Height / 8 - 18);
                rect2.Y = random.Next(0, this.Height - this.Height / 8 - 80);
                g.FillEllipse(RdBrush, rect2);
            }
            rect1.X = random.Next(0, this.Width - this.Height / 8 - 18);
            rect1.Y = random.Next(0, this.Height - this.Height / 8 - 80);
            g.FillEllipse(RedBrush, rect1);
        }

        private void timer500_Tick(object sender, EventArgs e)
        {
            if (time == 0)
            {
                timer500.Stop();
                timer1000.Stop();
            }
            else
            {
                time = time - 0.5;
                lbtime.Text = "Time: " + time;
                g.Clear(Color.White);
                RandomBrush();
                RandomDraw();
                IsPress = false;
            }
        }

        private void timer1000_Tick(object sender, EventArgs e)
        {
            NumOfCircle++;
        }
    }
}
