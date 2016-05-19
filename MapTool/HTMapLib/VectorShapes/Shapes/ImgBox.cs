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
    /// Box Immagine ( estende Ele ) 
    /// </summary>
    [Serializable]
    public class ImgBox : Ele
    {
        private Bitmap _img;
        //private int _rotation;
        private bool _trasparent = false;

        public ImgBox(int x, int y, int x1, int y1)
        {

            this.X = x;
            this.Y = y;
            this.X1 = x1;
            this.Y1 = y1;
            this.Selected = true;
            this.endMoveRedim();
            this.rot = true; //can rotate
        }

        [Category("Image"), Description("File image")]
        public Bitmap img
        {
            get
            {
                return _img;
            }
            set
            {
                _img = value;
            }
        }

        [Category("Image"), Description("Trasparent")]
        public bool Trasparent
        {
            get
            {
                return _trasparent;
            }
            set
            {
                _trasparent = value;
            }
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

        [Category("1"), Description("Image Box")]
        public string ObjectType
        {
            get
            {
                return "Image Box";
            }
        }

        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            gp.AddRectangle(new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom));
        }


        public override Ele Copy()
        {
            ImgBox newE = new ImgBox(this.X, this.Y, this.X1, this.Y1);
            newE.penColor = this.penColor;
            newE.penWidth = this.penWidth;
            newE.fillColor = this.fillColor;
            newE.filled = this.filled;
            newE.sonoUnaLinea = this.sonoUnaLinea;
            newE.alpha = this.alpha;
            newE.dashStyle = this.dashStyle;
            newE.showBorder = this.showBorder;
            newE.Trasparent = this.Trasparent;
            newE.Rotation = this.Rotation;

            newE.OnGrpXRes = this.OnGrpXRes;
            newE.OnGrpX1Res = this.OnGrpX1Res;
            newE.OnGrpYRes = this.OnGrpYRes;
            newE.OnGrpY1Res = this.OnGrpY1Res;


            newE.img = this.img;

            newE.copyGradprop(this);

            return newE;
        }

        public override void CopyFrom(Ele ele)
        {
            this.copyStdProp(ele, this);
            this.img = ((ImgBox)ele).img;
        }

        public override void Select()
        {
            this.undoEle = this.Copy();
        }

        public void Load_IMG()
        {
            string f_name = this.imgLoader();
            if (f_name != null)
            {
                try
                {
                    Bitmap loadTexture = new Bitmap(f_name);
                    this.img = loadTexture;
                }
                catch { }
            }
        }

        private string imgLoader()
        {
            try
            {
                OpenFileDialog DialogueCharger = new OpenFileDialog();
                DialogueCharger.Title = "Load background image";
                DialogueCharger.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
                //DialogueCharger.DefaultExt = "frame";
                if (DialogueCharger.ShowDialog() == DialogResult.OK)
                {
                    return (DialogueCharger.FileName);
                }
            }
            catch { }
            return null;
        }



        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            System.Drawing.Pen myPen = new System.Drawing.Pen(this.penColor, scaledPenWidth(zoom));
            myPen.DashStyle = this.dashStyle;

            if (this.Selected)
            {
                myPen.Color = Color.Red;
                myPen.Color = this.Trasparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;
            }

            if (img != null)
            {
                Color backColor = this.img.GetPixel(0, 0); //get the back color from the first pixel (UP-LEFT)
                //Create a tmp Bitmap and a graphic object
                // the dimension of the tmp bitmap must permit the rotation of img
                int dim = (int)System.Math.Sqrt(img.Width * img.Width + img.Height * img.Height);
                Bitmap curBitmap = new Bitmap(dim, dim);
                Graphics curG = Graphics.FromImage(curBitmap);

                if (this.Rotation > 0)
                {
                    // activate the rotation on the graphic obj
                    Matrix X = new Matrix();
                    X.RotateAt(this.Rotation, new PointF(curBitmap.Width / 2, curBitmap.Height / 2));
                    curG.Transform = X;
                    X.Dispose();
                }
                // I draw img over the tmp bitmap 
                curG.DrawImage(img, (dim - img.Width) / 2, (dim - img.Height) / 2, img.Width, img.Height);

                if (this.Trasparent)
                    curBitmap.MakeTransparent(backColor); // here I perform trasparency

                curG.Save();
                // I draw the tmp bitmap on canvas
                g.DrawImage(curBitmap, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);

                curG.Dispose();
                curBitmap.Dispose();

            }

            if (this.showBorder)
                g.DrawRectangle(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);

            myPen.Dispose();

        }
    }
}
