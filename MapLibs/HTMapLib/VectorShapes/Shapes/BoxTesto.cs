using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace HTMapLib.VectorShapes.Shapes
{
    /// <summary>
    /// Text Box ( estende Ele ) 
    /// </summary>
    [Serializable]
    public class BoxTesto : Ele
    {
        public string rtf; // save rtf text


        // placed here to not overload draw metod
        [NonSerialized()]
        protected RichTextBoxPrintCtrl.RichTextBoxPrintCtrl tmpR = new RichTextBoxPrintCtrl.RichTextBoxPrintCtrl();

        public BoxTesto(int x, int y, int x1, int y1)
        {

            this.X = x;
            this.Y = y;
            this.X1 = x1;
            this.Y1 = y1;
            this.Selected = true;
            this.endMoveRedim();
            rtf = "";
            //tBox = new TxtBox();
        }

        public override void AfterLoad()
        {
            // tmpR is not serialized, I must recreate after Load
            if (this.tmpR == null)
                tmpR = new RichTextBoxPrintCtrl.RichTextBoxPrintCtrl();
        }

        [Category("1"), Description("RTF Box")]
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
        }


        public override Ele Copy()
        {
            BoxTesto newE = new BoxTesto(this.X, this.Y, this.X1, this.Y1);
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

            newE.rtf = this.rtf;

            return newE;
        }


        public override void CopyFrom(Ele ele)
        {
            this.copyStdProp(ele, this);
            this.rtf = ((BoxTesto)ele).rtf;
        }

        public override void Select()
        {
            this.undoEle = this.Copy();
        }

        public override void Select(RichTextBox r)
        {
            r.Rtf = rtf;
        }

        public override void ShowEditor(richForm2 f)
        {
            f.richTextBox1.Rtf = rtf;
            f.ShowDialog();
            if (f.confermato)
                this.rtf = f.richTextBox1.Rtf;

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

            if (this.filled)
            {
                g.FillRectangle(myBrush, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
            }
            if (this.showBorder || this.Selected)
                g.DrawRectangle(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);

            tmpR.BorderStyle = BorderStyle.None;
            tmpR.ScrollBars = RichTextBoxScrollBars.None;

            tmpR.Rtf = rtf;

            /*
            // TO ENABLE TEST ROTATION / ZOOM
            //TEST START
                Bitmap curBitmap = new Bitmap((int)(this.Width * zoom),(int)( this.Height * zoom));
                curBitmap.SetResolution(this.Width , this.Height );
                Graphics curG = Graphics.FromImage(curBitmap);
                curG.PageUnit = GraphicsUnit.Point;
                if (this._rotation > 0)
                {
                    // activate the rotation on the graphic obj
                    Matrix X = new Matrix();
                    X.RotateAt(this._rotation, new PointF(curBitmap.Width / 2, curBitmap.Height / 2));
                    curG.Transform = X;
                    X.Dispose();
                }
                // I draw img over the tmp bitmap 
                if (curG.DpiX < 600)
                {
                    //tmpR.Draw(0, tmpR.TextLength, curG, (int)((this.X + dx) * zoom), (int)((this.Y + dy) * zoom), (int)((dx + this.X1 - (int)((this.X1 - this.X) * .08)) * zoom), (int)((dy + this.Y1 - (int)((this.Y1 - this.Y) * .08)) * zoom), 15);
                    tmpR.Draw(0, tmpR.TextLength, curG, 0, 0, (int)((this.Width * 1) * zoom), (int)(( this.Height * 1) * zoom), 15);
                }
                else
                {
                    //tmpR.Draw(0, tmpR.TextLength, curG, (int)((this.X + dx) * zoom), (int)((this.Y + dy) * zoom), (int)((this.X1 + dx) * zoom), (int)((this.Y1 + dy) * zoom), 14.4);
                    tmpR.Draw(0, tmpR.TextLength, curG, 0, 0, (int)((this.Width * 1) * zoom), (int)((this.Width * 1) * zoom), 14.4);
                }


                //if (this._tra .Trasparent)
                    //curBitmap.MakeTransparent(backColor); // here I perform trasparency

                curG.Save();
                // I draw the tmp bitmap on canvas
                g.DrawImage(curBitmap, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);

                curG.Dispose();
                curBitmap.Dispose();

            // END
            */

            //Console.WriteLine("OSVersion: {0}", Environment.OSVersion.ToString());
            //Console.WriteLine("OSVersion: {0}", Environment.OSVersion.Platform.ToString());
            if (g.DpiX < 600)
            {
                //    tmpR.Draw(0, tmpR.TextLength, g, (int)((this.X + dx) * zoom), (int)((this.Y + dy) * zoom), (int)((dx + this.X1 - (int)((this.X1 - this.X) * .08)) * zoom),(int)( (dy + this.Y1 - (int)((this.Y1 - this.Y) * .08)) * zoom), 15);
                tmpR.Draw(0, tmpR.TextLength, g, (int)((this.X + dx) * zoom), (int)((this.Y + dy) * zoom), (int)((this.X1 + dx) * zoom), (int)((this.Y1 + dy) * zoom), 1440 / g.DpiX, 1440 / g.DpiY);
            }
            else
                tmpR.Draw(0, tmpR.TextLength, g, (int)((this.X + dx) * zoom), (int)((this.Y + dy) * zoom), (int)((this.X1 + dx) * zoom), (int)((this.Y1 + dy) * zoom), 14.4, 14.4);



            //tmpR.Dispose();
            myPen.Dispose();
            if (myBrush != null)
                myBrush.Dispose();
        }

    }
}
