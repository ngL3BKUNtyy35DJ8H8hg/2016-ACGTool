using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace HT.VectorShapes.Shapes
{
    /// <summary>
    /// Arc 
    /// </summary>
    [Serializable]
    public class Arc : Ele
    {
        private int _startAng;
        private int _lenAng;
        private LineCap _starCap;
        private LineCap _endCap;


        public Arc(int x, int y, int x1, int y1)
        {

            this.X = x;
            this.Y = y;
            this.X1 = x1;
            this.Y1 = y1;
            this.Selected = true;
            this.endMoveRedim();
            this.StartAng = 0;
            this.LenAng = 90;
            this.startCap = LineCap.Custom;
            this.endCap = LineCap.Custom;
        }

        [Category("1"), Description("Rectangle")]
        public string ObjectType
        {
            get
            {
                return "Arc";
            }
        }

        [Category("Line Appearance"), Description("Line Start Cap")]
        public LineCap startCap
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


        [Description("Start angle")]
        public int StartAng
        {
            get
            {
                return _startAng;
            }
            set
            {
                _startAng = value;
            }
        }

        [Description("Angle length")]
        public int LenAng
        {
            get
            {
                return _lenAng;
            }
            set
            {
                _lenAng = value;
            }
        }


        public override Ele Copy()
        {
            Arc newE = new Arc(this.X, this.Y, this.X1, this.Y1);
            newE.penColor = this.penColor;
            newE.penWidth = this.penWidth;
            newE.fillColor = this.fillColor;
            newE.filled = this.filled;
            newE.dashStyle = this.dashStyle;
            newE.alpha = this.alpha;
            newE.isaLine = this.isaLine;
            newE.StartAng = this.StartAng;
            newE.LenAng = this.LenAng;
            newE.showBorder = this.showBorder;
            newE.endCap = this.endCap;
            newE.startCap = this.startCap;

            newE.OnGrpXRes = this.OnGrpXRes;
            newE.OnGrpX1Res = this.OnGrpX1Res;
            newE.OnGrpYRes = this.OnGrpYRes;
            newE.OnGrpY1Res = this.OnGrpY1Res;

            newE.copyGradprop(this);

            return newE;
        }


        public override void CopyFrom(Ele ele)
        {
            this.copyStdProp(ele, this);
            this.StartAng = ((Arc)ele).StartAng;
            this.LenAng = ((Arc)ele).LenAng;
            this.startCap = ((Arc)ele).startCap;
            this.endCap = ((Arc)ele).endCap;
        }


        public override void Select()
        {
            this.undoEle = this.Copy();
        }


        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            gp.AddArc((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom, this.StartAng, this.LenAng);
        }


        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {

            //System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(this.fillColor);
            Brush myBrush = getBrush(dx, dy, zoom);
            //myBrush.Color = this.Trasparency(this.fillColor, this.alpha);
            System.Drawing.Pen myPen = new System.Drawing.Pen(this.penColor, scaledPenWidth(zoom));
            myPen.DashStyle = this.dashStyle;
            myPen.EndCap = this.endCap;
            myPen.StartCap = this.startCap;

            //myBrush.Color = this.Trasparency(this.fillColor, this.alpha);

            if (this.Selected)
            {
                System.Drawing.Pen myPen1 = new System.Drawing.Pen(this.penColor, scaledPenWidth(zoom));
                myPen1.Width = 0.5f;
                myPen1.DashStyle = DashStyle.Dot;
                g.DrawEllipse(myPen1, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
                myPen1.Dispose();
                //myBrush.Color = this.dark(this.fillColor, 5, this.alpha);
                myPen.Color = Color.Red;
                myPen.Color = this.Trasparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;
            }

            // Create a path and add the object.
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddArc((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom, this.StartAng, this.LenAng);
            //Matrix translateMatrix = new Matrix();
            //translateMatrix.RotateAt(this.Rotation, new Point(this.X + (int)(this.X1 - this.X) / 2, this.Y + (int)(this.Y1 - this.Y) / 2));
            //myPath.Transform(translateMatrix);

            // Draw the transformed ellipse to the screen.
            if (this.filled)
            {
                g.FillPath(myBrush, myPath);
                if (this.showBorder)
                    g.DrawPath(myPen, myPath);
            }
            else
                g.DrawPath(myPen, myPath);

            myPath.Dispose();
            myPen.Dispose();
            if (myBrush != null)
                myBrush.Dispose();
        }
    }

}
