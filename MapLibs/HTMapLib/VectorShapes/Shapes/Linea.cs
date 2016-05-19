using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace HTMapLib.VectorShapes.Shapes
{
    /// <summary>
    /// Linea ( estende Ele ) 
    /// </summary>
    [Serializable]
    public class Linea : Ele
    {
        private LineCap _starCap;
        private LineCap _endCap;

        public Linea()
        { }

        public Linea(int x, int y, int x1, int y1)
        {
            this.X = x;
            this.Y = y;
            this.X1 = x1;
            this.Y1 = y1;
            this.sonoUnaLinea = true;
            this.Selected = true;
            this.starCap = LineCap.Custom;
            this.endCap = LineCap.Custom;
            this.endMoveRedim();
            this.rot = false; //can rotate?
        }

        [Category("Line Appearance"), Description("Line Start Cap")]
        public LineCap starCap
        {
            get
            {
                return _starCap;
            }
            set
            {
                _starCap = value;
            }
        }

        [Category("Line Appearance"), Description("Line End Cap")]
        public LineCap endCap
        {
            get
            {
                return _endCap;
            }
            set
            {
                _endCap = value;
            }
        }

        [Category("1"), Description("Line")]
        public string ObjectType
        {
            get
            {
                return "Line";
            }
        }


        public override Ele Copy()
        {
            Linea newE = new Linea(this.X, this.Y, this.X1, this.Y1);
            newE.penColor = this.penColor;
            newE.penWidth = this.penWidth;
            newE.fillColor = this.fillColor;
            newE.filled = this.filled;
            newE.dashstyle = this.dashstyle;
            newE.sonoUnaLinea = this.sonoUnaLinea;
            newE.alpha = this.alpha;
            //
            newE.starCap = this.starCap;
            newE.endCap = this.endCap;

            newE.OnGrpXRes = this.OnGrpXRes;
            newE.OnGrpX1Res = this.OnGrpX1Res;
            newE.OnGrpYRes = this.OnGrpYRes;
            newE.OnGrpY1Res = this.OnGrpY1Res;

            newE.gprZoomX = this.gprZoomX;
            newE.gprZoomY = this.gprZoomY;


            return newE;
        }

        public override void CopyFrom(Ele ele)
        {
            this.copyStdProp(ele, this);
            this.endCap = ((Linea)ele).endCap;
            this.starCap = ((Linea)ele).starCap;
        }

        public override void Select()
        {
            this.undoEle = this.Copy();
        }





        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            gp.AddLine((this.getX() + dx) * zoom, (this.getY() + dy) * zoom, (this.getX1() + dx) * zoom, (this.getY1() + dy) * zoom);
        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(this.penColor, scaledPenWidth(zoom));
            myPen.DashStyle = this.dashStyle;
            myPen.StartCap = this.starCap;
            myPen.EndCap = this.endCap;


            myPen.Color = this.Trasparency(this.penColor, this.alpha);

            //test
            //myPen = PEN.getPen();


            if (this.Selected)
            {
                myPen.Color = Color.Red;
                myPen.Color = this.Trasparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;
                g.DrawEllipse(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, 3, 3);
            }

            if (this.X == this.X1 && this.Y == this.Y1)
                g.DrawEllipse(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, 3, 3);
            else
                g.DrawLine(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 + dx) * zoom, (this.Y1 + dy) * zoom);

            myPen.Dispose();

        }

    }

}
