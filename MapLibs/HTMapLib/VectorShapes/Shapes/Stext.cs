using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace HTMapLib.VectorShapes.Shapes
{
    /// <summary>
    /// Simple Text 
    /// </summary>
    [Serializable]
    public class Stext : Ele
    {
        //TEST
        private Font f;
        private StringAlignment sa;
        public string Text { get; set; }

        public Stext(int x, int y, int x1, int y1)
        {

            this.X = x;
            this.Y = y;
            this.X1 = x1;
            this.Y1 = y1;
            this.Selected = true;
            this.endMoveRedim();

            this.Rotation = 0;
            //this.CharFont = new Font(FontFamily.GenericMonospace, 8); ;
            this.rot = true; //can rotate?
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

        public StringAlignment StrAllin
        {
            get
            {
                return sa;
            }
            set
            {
                sa = value;
            }
        }

        public Font CharFont
        {
            get
            {
                return f;
            }
            set
            {
                f = value;
            }
        }

        [Category("1"), Description("Simple Text")]
        public string ObjectType
        {
            get
            {
                return "Text Rectangle";
            }
        }

        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            gp.AddRectangle(new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom));

            /*
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = this.sa;
            stringFormat.LineAlignment = StringAlignment.Near;
            FontFamily family = new FontFamily(this.CharFont.FontFamily.Name);
            //int fontStyle = (int)FontStyle.Bold;
            gp.AddString(this.Text, family, fontStyle, this.CharFont.Size * zoom, new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom), stringFormat);
            */
        }


        public override Ele Copy()
        {
            Stext newE = new Stext(this.X, this.Y, this.X1, this.Y1);
            newE.penColor = this.penColor;
            newE.penWidth = this.penWidth;
            newE.fillColor = this.fillColor;
            newE.filled = this.filled;
            newE.sonoUnaLinea = this.sonoUnaLinea;
            newE.alpha = this.alpha;
            newE.dashStyle = this.dashStyle;
            newE.showBorder = this.showBorder;

            newE.OnGrpXRes = this.OnGrpXRes;
            newE.OnGrpX1Res = this.OnGrpX1Res;
            newE.OnGrpYRes = this.OnGrpYRes;
            newE.OnGrpY1Res = this.OnGrpY1Res;

            newE.copyGradprop(this);

            newE.Text = this.Text;
            newE.CharFont = this.CharFont;
            newE.StrAllin = this.StrAllin;
            //newE.rtf = this.rtf;

            return newE;
        }


        public override void CopyFrom(Ele ele)
        {
            this.copyStdProp(ele, this);

        }

        public override void Select()
        {
            this.undoEle = this.Copy();
        }




        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            GraphicsState gs = g.Save();//store previos trasformation
            Matrix mx = g.Transform; // get previous trasformation

            PointF p = new PointF(zoom * (this.X + dx + (this.X1 - this.X) / 2), zoom * (this.Y + dy + (this.Y1 - this.Y) / 2));
            if (this.Rotation > 0)
                mx.RotateAt(this.Rotation, p, MatrixOrder.Append); //add a trasformation

            g.Transform = mx;

            //System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(this.fillColor);
            Brush myBrush = getBrush(dx, dy, zoom);
            //myBrush.Color = this.Trasparency(this.fillColor, this.alpha);
            System.Drawing.Pen myPen = new System.Drawing.Pen(this.penColor, scaledPenWidth(zoom));
            myPen.DashStyle = this.dashStyle;
            if (this.Selected)
            {
                myPen.Color = Color.Red;
                myPen.Color = this.Trasparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;
            }

            if (this.filled)
            {
                g.FillRectangle(myBrush, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
            }
            if (this.showBorder || this.Selected)
                g.DrawRectangle(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = this.sa;
            //stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Near;

            Font tmpf = new Font(this.CharFont.FontFamily, this.CharFont.Size * zoom, this.CharFont.Style);
            g.DrawString(this.Text, tmpf, new SolidBrush(this.penColor), new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom), stringFormat);
            tmpf.Dispose();
            myPen.Dispose();
            if (myBrush != null)
                myBrush.Dispose();

            g.Restore(gs);//restore previos trasformation

        }


    }
}
