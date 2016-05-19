using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConfigBDTC.Classes
{
    public class DrawEllipse : DrawRectangle
    {
        public DrawEllipse()
            : this(0, 0, 1, 1)
        {
        }

        public DrawEllipse(int x, int y, int width, int height)
            : base()
        {
            Rectangle = new Rectangle(x, y, width, height);
            Initialize();
        }

        //public override void Drawing(Graphics g, Rectangle rect)
        //{
        //    Pen pen = new Pen(Color, PenWidth);
        //    g.DrawEllipse(pen, rect);
        //    pen.Dispose();
        //}

        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, PenWidth);
            g.DrawEllipse(pen, Rectangle);
            pen.Dispose();
        }
    }
}
