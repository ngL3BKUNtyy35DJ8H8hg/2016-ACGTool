using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace HTMapLib.VectorShapes.Shapes
{
    //Point Wrapper: used to store and update points in arraylist
    [Serializable]
    public class PointWr
    {
        private Point p;
        public bool selected = false;
        private Point startp;
        public int id;

        public void Zoom(float dx, float dy)
        {
            this.X = (int)(startp.X * dx);
            this.Y = (int)(startp.Y * dy);
        }
        public void endZoom()
        {
            startp = p;
        }

        public PointWr(Point pp)
        {
            p = pp;
            startp = p;
        }
        public PointWr(int x, int y)
        {
            p.X = x;
            p.Y = y;
            startp = p;
        }

        public PointWr copy()
        {
            return new PointWr(this.X, this.Y);
        }

        public void RotateAt(float x, float y, int rotAngle)
        {
            float tmpX = this.X - x;
            float tmpY = this.Y - y;
            PointF p = this.rotatePoint(new PointF(tmpX, tmpY), rotAngle);
            p.X = p.X + x;
            p.Y = p.Y + y;
            this.X = (int)p.X;
            this.Y = (int)p.Y;
        }

        public void XMirror(int wid)
        {
            this.X = (-1) * p.X + wid;
            startp = p;
        }
        public void YMirror(int hei)
        {
            this.Y = (-1) * p.Y + hei;
            startp = p;
        }

        protected PointF rotatePoint(PointF p, int RotAng)
        {
            double RotAngF = RotAng * Math.PI / 180;
            double SinVal = Math.Sin(RotAngF);
            double CosVal = Math.Cos(RotAngF);
            float Nx = (float)(p.X * CosVal - p.Y * SinVal);
            float Ny = (float)(p.Y * CosVal + p.X * SinVal);
            return new PointF(Nx, Ny);
        }


        [Category("Position"), Description("X ")]
        public int X
        {
            get
            {
                return p.X;
            }
            set
            {
                p.X = value;
            }
        }
        [Category("Position"), Description("Y ")]
        public int Y
        {
            get
            {
                return p.Y;
            }
            set
            {
                p.Y = value;
            }
        }
        [Category("Position"), Description(" ")]
        public Point point
        {
            get
            {
                return p;
            }
            set
            {
                p = value;
            }
        }
    }
}
