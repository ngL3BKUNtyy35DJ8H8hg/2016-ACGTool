using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace HTMapLib.VectorShapes.Shapes
{
    /// <summary>
    /// Rettangolo smussato ( estende Ele ) 
    /// </summary>
    /// 
    [Description("Rounded rectangle")]
    [Serializable]
    public class RRect : Ele
    {
        private int _arcsWidth;
        //private int _rotation;

        public RRect(int x, int y, int x1, int y1)
        {

            this.X = x;
            this.Y = y;
            this.X1 = x1;
            this.Y1 = y1;
            this.Selected = true;
            this.arcsWidth = 20;
            this._rotation = 0;
            this.endMoveRedim();
            this.rot = true; //can rotate?
        }

        [Category("1"), Description("Rounded Rectangle")]
        public string ObjectType
        {
            get
            {
                return "Rounded Rectangle";
            }
        }

        [Category("Vertex Appearance"), Description("Dimension of vertex arcs.")]
        public int arcsWidth
        {
            get
            {
                return _arcsWidth;
            }
            set
            {
                if (value <= 9)
                    _arcsWidth = 10;
                else
                    _arcsWidth = value;
            }
        }

        [Description("Rotation Angle.")]
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


        public override Ele Copy()
        {
            RRect newE = new RRect(this.X, this.Y, this.X1, this.Y1);
            newE.penColor = this.penColor;
            newE.penWidth = this.penWidth;
            newE.fillColor = this.fillColor;
            newE.filled = this.filled;
            newE.sonoUnaLinea = this.sonoUnaLinea;
            newE.alpha = this.alpha;
            newE.dashStyle = this.dashStyle;
            newE.showBorder = this.showBorder;
            newE.arcsWidth = this.arcsWidth;
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
            this.arcsWidth = ((RRect)ele).arcsWidth;
            this.Rotation = ((RRect)ele).Rotation;
        }


        public override void Select()
        {
            this.undoEle = this.Copy();
        }


        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            float n = this.arcsWidth;
            gp.AddArc(new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, n * zoom, n * zoom), 180, 90);
            gp.AddLine((this.X + dx + n / 2) * zoom, (this.Y + dy) * zoom, (this.X1 + dx - n / 2) * zoom, (this.Y + dy) * zoom);

            gp.AddArc(new RectangleF((this.X1 + dx - n) * zoom, (this.Y + dy) * zoom, n * zoom, n * zoom), 270, 90);
            gp.AddLine((this.X1 + dx) * zoom, (this.Y + dy + n / 2) * zoom, (this.X1 + dx) * zoom, (this.Y1 + dy - n / 2) * zoom);

            gp.AddArc(new RectangleF((this.X1 + dx - n) * zoom, (this.Y1 + dy - n) * zoom, n * zoom, n * zoom), 0, 90);
            gp.AddLine((this.X + dx + n / 2) * zoom, (this.Y1 + dy) * zoom, (this.X1 + dx - n / 2) * zoom, (this.Y1 + dy) * zoom);

            gp.AddArc(new RectangleF((this.X + dx) * zoom, (this.Y1 + dy - n) * zoom, n * zoom, n * zoom), 90, 90);
            gp.AddLine((this.X + dx) * zoom, (this.Y1 + dy - n / 2) * zoom, (this.X + dx) * zoom, (this.Y + dy + n / 2) * zoom);

        }


        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            float n = this.arcsWidth;
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

            // Create a path and add the object.
            GraphicsPath myPath = new GraphicsPath();

            myPath.AddArc(new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, n * zoom, n * zoom), 180, 90);
            myPath.AddLine((this.X + dx + n / 2) * zoom, (this.Y + dy) * zoom, (this.X1 + dx - n / 2) * zoom, (this.Y + dy) * zoom);

            myPath.AddArc(new RectangleF((this.X1 + dx - n) * zoom, (this.Y + dy) * zoom, n * zoom, n * zoom), 270, 90);
            myPath.AddLine((this.X1 + dx) * zoom, (this.Y + dy + n / 2) * zoom, (this.X1 + dx) * zoom, (this.Y1 + dy - n / 2) * zoom);

            myPath.AddArc(new RectangleF((this.X1 + dx - n) * zoom, (this.Y1 + dy - n) * zoom, n * zoom, n * zoom), 0, 90);
            myPath.AddLine((this.X + dx + n / 2) * zoom, (this.Y1 + dy) * zoom, (this.X1 + dx - n / 2) * zoom, (this.Y1 + dy) * zoom);

            myPath.AddArc(new RectangleF((this.X + dx) * zoom, (this.Y1 + dy - n) * zoom, n * zoom, n * zoom), 90, 90);
            myPath.AddLine((this.X + dx) * zoom, (this.Y1 + dy - n / 2) * zoom, (this.X + dx) * zoom, (this.Y + dy + n / 2) * zoom);

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

            //TEST START     
            float gridSize = 4;
            float gridRot = 45;

            //FillWithLines(g, dx, dy, zoom, myPath, gridSize, gridRot);
            //TEST END

            myPath.Dispose();
            myPen.Dispose();
            if (myBrush != null)
                myBrush.Dispose();
        }


    }
}
