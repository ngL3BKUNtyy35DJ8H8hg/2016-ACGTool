using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace HT.VectorShapes.Shapes
{
    /// <summary>
    /// Ellipse  
    /// </summary>
    [Serializable]
    public class Ell : Ele
    {
        public Ell(int x, int y, int x1, int y1)
        {
            this.X = x;
            this.Y = y;
            this.X1 = x1;
            this.Y1 = y1;
            this.Selected = true;
            this.endMoveRedim();
            this.Rotation = 0;
            this.rot = true; //can rotate
        }

        public override Ele Copy()
        {
            Ell newE = new Ell(this.X, this.Y, this.X1, this.Y1);
            newE.penColor = this.penColor;
            newE.penWidth = this.penWidth;
            newE.fillColor = this.fillColor;
            newE.filled = this.filled;
            newE.isaLine = this.isaLine;
            newE.alpha = this.alpha;
            newE.dashStyle = this.dashStyle;
            newE.showBorder = this.showBorder;
            newE.Rotation = this.Rotation;

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
            this.Rotation = ((Ell)ele).Rotation;
        }


        [Description("Rotation angle")]
        public int Rotation
        {
            get
            {
                return _rotation;
            }
            set
            {
                _rotation = value;
            }
        }


        [Category("1"), Description("Ellipse")]
        public string ObjectType
        {
            get
            {
                return "Ellipse";
            }
        }


        public override void Select()
        {
            this.undoEle = this.Copy();

        }

        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            gp.AddEllipse((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
        }


        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            //System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(this.fillColor);
            Brush myBrush = getBrush(dx, dy, zoom);
            //myBrush.Color = this.Trasparency(this.fillColor, this.alpha);
            System.Drawing.Pen myPen = new System.Drawing.Pen(this.penColor, scaledPenWidth(zoom));
            myPen.DashStyle = this.dashStyle;
            if (this.Selected)
            {
                //myBrush.Color = this.dark(this.fillColor, 5, this.alpha);
                myPen.Color = Color.Red;
                myPen.Color = this.Trasparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;

            }

            //test
            //myPen = PEN.getPen();

            // Create a path and add the object.
            GraphicsPath myPath = new GraphicsPath();
            myPath.AddEllipse((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
            Matrix translateMatrix = new Matrix();
            translateMatrix.RotateAt(this.Rotation, new PointF((this.X + dx + (int)(this.X1 - this.X) / 2) * zoom, (this.Y + dy + (int)(this.Y1 - this.Y) / 2) * zoom));
            myPath.Transform(translateMatrix);

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
