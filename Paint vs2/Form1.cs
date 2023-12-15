using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        private enum DrawingMode
        {
            Pen,
            Fill,
            Circle,
            Ellipse,
            Rectangle,
            Square,
            Line,
            Curve,
            Star
        }
        private Bitmap bmp,bmp_temp;
        private DrawingMode drawingMode = DrawingMode.Pen;
        private Point PreviousPoint = new Point(0, 0);
        private Point NewPoint = new Point(0, 0);
        private bool IsPainting = false;
        private Pen pen = new Pen(Color.Black, 2), pen2 = new Pen(Color.Black, 2);
        private Graphics g;
        private Graphics g2;
        private Rectangle currentRect = Rectangle.Empty;
        private GraphicsPath StarPath = new GraphicsPath();
        private Point pt0 = new Point(0, 0), pt1 = new Point(0, 0), pt2 = new Point(0, 0), pt3 = new Point(0, 0);
        private int checkStatus = 0;
        private int X, Y, W, H;

        public Form1()
        {
            InitializeComponent();
            cbDashStyle.SelectedIndex = 0;
            cbDashCap.SelectedIndex = 0;
            cbStartCap.SelectedIndex = 0;
            cbEndCap.SelectedIndex = 0;
            bmp = new Bitmap(this.Width, this.Height - splitContainer1.Panel1.Height);
            g = splitContainer1.Panel2.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g2 = Graphics.FromImage(bmp);
            g2.SmoothingMode = SmoothingMode.AntiAlias;
            cbSize.Text = (pen.Width).ToString();
            pen2.StartCap = LineCap.Round;
            pen2.EndCap = LineCap.Round;
        }

        private void bPen_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Pen;
        }

        private void bFill_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Fill;
        }

        private void bCircle_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Circle;
        }

        private void bEllipse_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Ellipse;
        }

        private void bSquare_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Square;
        }

        private void bRectangle_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Rectangle;
        }

        private void bColor_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                bColor.BackColor = colorDialog1.Color;
                pen.Color = colorDialog1.Color;
                pen2.Color = pen.Color;
            }
        }

        private void splitContainer1_Panel2_MouseDown(object sender, MouseEventArgs e)
        {
            PreviousPoint = e.Location;
            if (drawingMode != DrawingMode.Fill)
                IsPainting = true;
            else
            {
                
            }
        }

        private void splitContainer1_Panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsPainting)
            {
                //int X, Y, W, H;
                NewPoint = e.Location;
                switch (drawingMode)
                {
                    case DrawingMode.Pen:
                        g.DrawLine(pen2, PreviousPoint, e.Location);
                        g2.DrawLine(pen2, PreviousPoint, e.Location);
                        PreviousPoint = e.Location;
                        break;
                    //case DrawingMode.Rectangle:
                    //    g.Clear(Color.White);
                    //    g.DrawImageUnscaled(bmp, 0, 0);
                    //    X = Math.Min(PreviousPoint.X, e.Location.X);
                    //    Y = Math.Min(PreviousPoint.Y, e.Location.Y);
                    //    W = Math.Abs(e.Location.X - PreviousPoint.X);
                    //    H = Math.Abs(e.Location.Y - PreviousPoint.Y);
                    //    currentRect = new Rectangle(X,Y,W,H);
                    //    g.DrawRectangle(pen, currentRect);
                    //    break;
                    //case DrawingMode.Square:
                    //    g.Clear(Color.White);
                    //    g.DrawImageUnscaled(bmp, 0, 0);
                    //    W = Math.Min(Math.Abs(e.Location.X - PreviousPoint.X),Math.Abs(e.Location.Y - PreviousPoint.Y));
                    //    X = PreviousPoint.X <= e.Location.X ? PreviousPoint.X : PreviousPoint.X - W;
                    //    Y = PreviousPoint.Y <= e.Location.Y ? PreviousPoint.Y : PreviousPoint.Y - W;
                    //    currentRect = new Rectangle(X, Y, W, W);
                    //    g.DrawRectangle(pen, currentRect);
                    //    break;
                    //case DrawingMode.Ellipse:
                    //    g.Clear(Color.White);
                    //    g.DrawImageUnscaled(bmp, 0, 0);
                    //    X = Math.Min(PreviousPoint.X, e.Location.X);
                    //    Y = Math.Min(PreviousPoint.Y, e.Location.Y);
                    //    W = Math.Abs(e.Location.X - PreviousPoint.X);
                    //    H = Math.Abs(e.Location.Y - PreviousPoint.Y);
                    //    currentRect = new Rectangle(X, Y, W, H);
                    //    g.DrawEllipse(pen, currentRect);
                    //    break;
                    //case DrawingMode.Circle:
                    //    g.Clear(Color.White);
                    //    g.DrawImageUnscaled(bmp, 0, 0);
                    //    W = Math.Min(Math.Abs(e.Location.X - PreviousPoint.X), Math.Abs(e.Location.Y - PreviousPoint.Y));
                    //    X = PreviousPoint.X <= e.Location.X ? PreviousPoint.X : PreviousPoint.X - W;
                    //    Y = PreviousPoint.Y <= e.Location.Y ? PreviousPoint.Y : PreviousPoint.Y - W;
                    //    currentRect = new Rectangle(X, Y, W, W);
                    //    g.DrawEllipse(pen, currentRect);
                    //    break;
                    //case DrawingMode.Line:
                    //    g.Clear(Color.White);
                    //    g.DrawImageUnscaled(bmp, 0, 0);
                    //    g.DrawLine(pen, PreviousPoint, e.Location);
                    //    break;
                    //case DrawingMode.Star:
                    //    g.Clear(Color.White);
                    //    g.DrawImageUnscaled(bmp, 0, 0);
                    //    int dt = e.Location.X - PreviousPoint.X;
                    //    StarPath.Dispose();
                    //    StarPath = new GraphicsPath();
                    //    StarPath.AddLine(PreviousPoint.X ,PreviousPoint.Y + dt * 11 / 30, PreviousPoint.X + dt * 11 / 30, PreviousPoint.Y + dt * 11 / 30);
                    //    StarPath.AddLine(PreviousPoint.X + dt * 11 / 30, PreviousPoint.Y + dt * 11 / 30, PreviousPoint.X + dt / 2, PreviousPoint.Y);
                    //    StarPath.AddLine(PreviousPoint.X + dt / 2, PreviousPoint.Y, e.Location.X - dt * 11 / 30, PreviousPoint.Y + dt * 11 / 30);
                    //    StarPath.AddLine(e.Location.X - dt * 11 / 30, PreviousPoint.Y + dt * 11 / 30, e.Location.X, PreviousPoint.Y + dt * 11 / 30);
                    //    StarPath.AddLine(e.Location.X, PreviousPoint.Y + dt * 11 / 30, e.Location.X - dt * 14 / 50, PreviousPoint.Y + dt * 18 / 30);
                    //    StarPath.AddLine(e.Location.X - dt * 14 / 50, PreviousPoint.Y + dt * 18 / 30, PreviousPoint.X + dt * 7 / 8, PreviousPoint.Y + dt);
                    //    StarPath.AddLine(PreviousPoint.X + dt * 7 / 8, PreviousPoint.Y + dt, PreviousPoint.X + dt / 2, PreviousPoint.Y + dt * 3 / 4);
                    //    StarPath.AddLine(PreviousPoint.X + dt / 2, PreviousPoint.Y + dt * 3 / 4, PreviousPoint.X + dt * 1 / 8, PreviousPoint.Y + dt);
                    //    StarPath.AddLine(PreviousPoint.X + dt * 1 / 8, PreviousPoint.Y + dt, PreviousPoint.X + dt * 14 / 50, PreviousPoint.Y + dt * 18 / 30);
                    //    StarPath.AddLine(PreviousPoint.X + dt * 14 / 50, PreviousPoint.Y + dt * 18 / 30, PreviousPoint.X, PreviousPoint.Y + dt * 11 / 30);

                    //    StarPath.AddLine(PreviousPoint.X, PreviousPoint.Y + dt * 11 / 30, PreviousPoint.X + dt * 1 / 100, PreviousPoint.Y + dt * 11 / 30);
                    //    g.DrawPath(pen, StarPath);
                    //    break;
                    //case DrawingMode.Curve:
                    //    g.Clear(Color.White);
                    //    g.DrawImageUnscaled(bmp, 0, 0);
                    //    switch (checkStatus)
                    //    {
                    //        case 0:
                    //            pt0 = PreviousPoint;
                    //            pt1 = PreviousPoint;
                    //            pt2 = e.Location;
                    //            pt3 = e.Location;
                    //            break;
                    //        case 1:
                    //            pt1 = e.Location;
                    //            break;
                    //        case 2:
                    //            pt2 = e.Location;
                    //            break;
                    //    }
                    //    g.DrawBezier(pen, pt0, pt1, pt2, pt3);
                    //    break;
                }
            }
        }

        private void splitContainer1_Panel2_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsPainting)
            {
                switch (drawingMode)
                {
                    case DrawingMode.Pen:
                        break;
                    case DrawingMode.Rectangle:
                        X = Math.Min(PreviousPoint.X, e.Location.X);
                        Y = Math.Min(PreviousPoint.Y, e.Location.Y);
                        W = Math.Abs(e.Location.X - PreviousPoint.X);
                        H = Math.Abs(e.Location.Y - PreviousPoint.Y);
                        currentRect = new Rectangle(X, Y, W, H);
                        g.DrawRectangle(pen, currentRect);
                        break;
                    case DrawingMode.Square:
                        W = Math.Min(Math.Abs(e.Location.X - PreviousPoint.X), Math.Abs(e.Location.Y - PreviousPoint.Y));
                        X = PreviousPoint.X <= e.Location.X ? PreviousPoint.X : PreviousPoint.X - W;
                        Y = PreviousPoint.Y <= e.Location.Y ? PreviousPoint.Y : PreviousPoint.Y - W;
                        currentRect = new Rectangle(X, Y, W, W);
                        g.DrawRectangle(pen, currentRect);
                        //g2.DrawRectangle(pen, currentRect);
                        break;
                    case DrawingMode.Ellipse:
                        X = Math.Min(PreviousPoint.X, e.Location.X);
                        Y = Math.Min(PreviousPoint.Y, e.Location.Y);
                        W = Math.Abs(e.Location.X - PreviousPoint.X);
                        H = Math.Abs(e.Location.Y - PreviousPoint.Y);
                        currentRect = new Rectangle(X, Y, W, H);
                        g.DrawEllipse(pen, currentRect);
                        break;
                    case DrawingMode.Circle:
                        W = Math.Min(Math.Abs(e.Location.X - PreviousPoint.X), Math.Abs(e.Location.Y - PreviousPoint.Y));
                        X = PreviousPoint.X <= e.Location.X ? PreviousPoint.X : PreviousPoint.X - W;
                        Y = PreviousPoint.Y <= e.Location.Y ? PreviousPoint.Y : PreviousPoint.Y - W;
                        currentRect = new Rectangle(X, Y, W, W);
                        g.DrawEllipse(pen, currentRect);
                        //g2.DrawEllipse(pen, currentRect);
                        break;
                    case DrawingMode.Line:
                        g.DrawLine(pen, PreviousPoint, e.Location);
                        //g2.DrawLine(pen, PreviousPoint, e.Location);
                        break;
                    case DrawingMode.Star:
                        int dt = e.Location.X - PreviousPoint.X;
                        StarPath.Dispose();
                        StarPath = new GraphicsPath();
                        StarPath.AddLine(PreviousPoint.X, PreviousPoint.Y + dt * 11 / 30, PreviousPoint.X + dt * 11 / 30, PreviousPoint.Y + dt * 11 / 30);
                        StarPath.AddLine(PreviousPoint.X + dt * 11 / 30, PreviousPoint.Y + dt * 11 / 30, PreviousPoint.X + dt / 2, PreviousPoint.Y);
                        StarPath.AddLine(PreviousPoint.X + dt / 2, PreviousPoint.Y, e.Location.X - dt * 11 / 30, PreviousPoint.Y + dt * 11 / 30);
                        StarPath.AddLine(e.Location.X - dt * 11 / 30, PreviousPoint.Y + dt * 11 / 30, e.Location.X, PreviousPoint.Y + dt * 11 / 30);
                        StarPath.AddLine(e.Location.X, PreviousPoint.Y + dt * 11 / 30, e.Location.X - dt * 14 / 50, PreviousPoint.Y + dt * 18 / 30);
                        StarPath.AddLine(e.Location.X - dt * 14 / 50, PreviousPoint.Y + dt * 18 / 30, PreviousPoint.X + dt * 7 / 8, PreviousPoint.Y + dt);
                        StarPath.AddLine(PreviousPoint.X + dt * 7 / 8, PreviousPoint.Y + dt, PreviousPoint.X + dt / 2, PreviousPoint.Y + dt * 3 / 4);
                        StarPath.AddLine(PreviousPoint.X + dt / 2, PreviousPoint.Y + dt * 3 / 4, PreviousPoint.X + dt * 1 / 8, PreviousPoint.Y + dt);
                        StarPath.AddLine(PreviousPoint.X + dt * 1 / 8, PreviousPoint.Y + dt, PreviousPoint.X + dt * 14 / 50, PreviousPoint.Y + dt * 18 / 30);
                        StarPath.AddLine(PreviousPoint.X + dt * 14 / 50, PreviousPoint.Y + dt * 18 / 30, PreviousPoint.X, PreviousPoint.Y + dt * 11 / 30);
                        StarPath.AddLine(PreviousPoint.X, PreviousPoint.Y + dt * 11 / 30, PreviousPoint.X + dt * 1 / 100, PreviousPoint.Y + dt * 11 / 30);
                        g.DrawPath(pen, StarPath);
                        //g2.DrawPath(pen, StarPath);
                        break;
                    case DrawingMode.Curve:
                        switch (checkStatus)
                        {
                            case 0:
                                pt0 = PreviousPoint;
                                pt1 = PreviousPoint;
                                pt2 = e.Location;
                                pt3 = e.Location;
                                break;
                            case 1:
                                pt1 = e.Location;
                                break;
                            case 2:
                                pt2 = e.Location;
                                break;
                        }
                        g.Clear(Color.White);
                        g.DrawBezier(pen, pt0, pt1, pt2, pt3);
                        if (checkStatus < 2)
                            checkStatus++;
                        else
                        {
                            checkStatus = 0;
                            //g2.DrawBezier(pen, pt0, pt1, pt2, pt3);
                        }
                        break;
                }
            }
            IsPainting = false;
        }
        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics gp = e.Graphics;
            switch (drawingMode)
            {
                case DrawingMode.Pen:
                    break;
                case DrawingMode.Rectangle:
                    X = Math.Min(PreviousPoint.X, NewPoint.X);
                    Y = Math.Min(PreviousPoint.Y, NewPoint.Y);
                    W = Math.Abs(NewPoint.X - PreviousPoint.X);
                    H = Math.Abs(NewPoint.Y - PreviousPoint.Y);
                    currentRect = new Rectangle(X, Y, W, H);
                    gp.DrawRectangle(pen, currentRect);
                    break;
            }
        }

        private void cbDashStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbDashStyle.SelectedIndex)
            {
                case 0:
                    pen.DashStyle = DashStyle.Solid;
                    break;
                case 1:
                    pen.DashStyle = DashStyle.Dash;
                    break;
                case 2:
                    pen.DashStyle = DashStyle.Dot;
                    break;
                case 3:
                    pen.DashStyle = DashStyle.DashDot;
                    break;
                case 4:
                    pen.DashStyle = DashStyle.DashDotDot;
                    break;
            }
        }

        private void cbDashCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbDashCap.SelectedIndex)
            {
                case 0:
                    pen.DashCap = DashCap.Flat;
                    break;
                case 1:
                    pen.DashCap = DashCap.Round;
                    break;
                case 2:
                    pen.DashCap = DashCap.Triangle;
                    break;
            }
        }

        private void cbStartCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbStartCap.SelectedIndex)
            {
                case 0:
                    pen.StartCap = LineCap.Round;
                    break;
                case 1:
                    pen.StartCap = LineCap.Flat;
                    break;
                case 2:
                    pen.StartCap = LineCap.RoundAnchor;
                    break;
                case 3:
                    pen.StartCap = LineCap.ArrowAnchor;
                    break;
            }
        }

        private void cbEndCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbEndCap.SelectedIndex)
            {
                case 0:
                    pen.EndCap = LineCap.Round;
                    break;
                case 1:
                    pen.EndCap = LineCap.Flat;
                    break;
                case 2:
                    pen.EndCap = LineCap.RoundAnchor;
                    break;
                case 3:
                    pen.EndCap = LineCap.ArrowAnchor;
                    break;
            }
        }

        private void bLine_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Line;
        }

        private void bCurve_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Curve;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void bStar_Click(object sender, EventArgs e)
        {
            drawingMode = DrawingMode.Star;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            g2.Clear(Color.White);
        }

        private void cbSize_TextUpdate(object sender, EventArgs e)
        {
            if (cbSize.Text == "")
                return;
            pen.Width = int.Parse(cbSize.Text);
            pen2.Width = int.Parse(cbSize.Text);
        }

        private void cbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            pen.Width = int.Parse(cbSize.Text);
            pen2.Width = int.Parse(cbSize.Text);
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            g.DrawImageUnscaled(bmp, 0, 0);
        }
    }
}
