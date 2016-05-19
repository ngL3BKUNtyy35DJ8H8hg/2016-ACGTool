using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace HTMapLib.VectorShapes.Shapes
{
    /// <summary>
    /// PointSet ( extends Ele ) 
    /// </summary>
    [Serializable]
    public class PointSet : Ele
    {
        //private int _rotation;
        public ArrayList points;
        private bool _curved = false;
        private bool _closed = false;

        public PointSet(int x, int y, int x1, int y1, ArrayList a)
        {
            this.X = x;
            this.Y = y;
            this.X1 = x1;
            this.Y1 = y1;
            this.Selected = true;
            //
            this.points = a;
            this.setupSize();
            //
            this.endMoveRedim();
            this.Rotation = 0;
            this.rot = true; //can rotate?
        }

        ///// <summary>
        ///// HT Code: Cập nhật thêm tọa độ kinh vĩ, độ
        ///// </summary>
        ///// <param name="x"></param>
        ///// <param name="y"></param>
        ///// <param name="x1"></param>
        ///// <param name="y1"></param>
        ///// <param name="a"></param>
        ///// <param name="lon"></param>
        ///// <param name="lat"></param>
        ///// <param name="lon1"></param>
        ///// <param name="lat1"></param>
        ///// <param name="mPoints"></param>
        //public PointSet(int x, int y, int x1, int y1, ArrayList a, double lon, double lat, double lon1, double lat1, ArrayList mPoints)
        //{
        //    //Các biến lưu kinh vĩ độ
        //    this.LeftLon = lon;
        //    this.TopLat = lat;
        //    this.RightLon = lon1;
        //    this.BottomLat = lat1;
        //    this.mapPoints = mPoints;

        //    //==========================================
        //    this.X = x;
        //    this.Y = y;
        //    this.X1 = x1;
        //    this.Y1 = y1;
        //    this.Selected = true;
        //    //
        //    this.points = a;
        //    this.setupSize();
        //    //
        //    this.endMoveRedim();
        //    this.Rotation = 0;
        //    this.rot = true; //can rotate?
        //    //==========================================
        //}

       

        [Editor(typeof(myTypeEditor),
             typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Polygon"), Description("Points")]
        public ArrayList Points
        {
            get
            {
                return this.points;
            }
            set
            {
                points = value;
            }
        }


        [Category("Polygon"), Description("Curved")]
        public bool Curved
        {
            get
            {
                return _curved;
            }
            set
            {
                _curved = value;
            }
        }
        [Category("Polygon"), Description("Closed")]
        public bool Closed
        {
            get
            {
                return _closed;
            }
            set
            {
                _closed = value;
            }
        }


        [Category("1"), Description("Rectangle")]
        public string ObjectType
        {
            get
            {
                return "PointSet";
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

        private void rePos()
        {
            if (points != null)
            {
                int minNegativeX = 0;
                int minNegativeY = 0;
                foreach (PointWr p in points)
                {
                    minNegativeX = p.X;
                    minNegativeY = p.Y;
                    break;
                }
                foreach (PointWr p in points)
                {
                    if (p.X < minNegativeX)
                        minNegativeX = p.X;
                    if (p.Y < minNegativeY)
                        minNegativeY = p.Y;
                }
                //if (minNegativeX < 0 | minNegativeY < 0)
                //{
                foreach (PointWr p in points)
                {
                    p.X = p.X - minNegativeX;
                    p.Y = p.Y - minNegativeY;
                }
                //}
                this.X = this.X + minNegativeX;
                this.Y = this.Y + minNegativeY;
            }
        }

        public ArrayList getRealPosPoints()
        {
            ArrayList a = new ArrayList();
            foreach (PointWr p in points)
            {
                a.Add(new PointWr(p.X + this.X, p.Y + this.Y));
            }
            return a;
        }

        /// <summary>
        /// HT Code: Cập nhật lại points
        /// </summary>
        public void UpdatePoints(ArrayList a)
        {
            this.points = a;
            this.setupSize();
            this.endMoveRedim();
        }

        /// <summary>
        /// Xác định kích thước rect bao quanh của shape
        /// </summary>
        public void setupSize()
        {
            if (this.points != null)
            {
                int maxX = 0;
                int maxY = 0;
                foreach (PointWr p in this.points)
                {
                    if (p.X > maxX)
                        maxX = p.X;
                    if (p.Y > maxY)
                        maxY = p.Y;
                }
                this.Y1 = this.Y + maxY;
                this.X1 = this.X + maxX;
                this.rePos();
            }
        }

        public void addPoint(Point p)
        {
            this.points.Add(p);
            //this.rePos();
        }

        public void rmPoint(Point p)
        {
            /*foreach (Point pp in points)
            { 
                if (pp==p)
            }*/
            this.points.Remove(p);
            //this.points.Add(p);
        }

        #region OVERRIDDEN

        public override void endMoveRedim()
        {
            base.endMoveRedim();
            foreach (PointWr p in points)
            {
                p.endZoom();
            }
        }

        public override void redim(int x, int y, string redimSt)
        {
            base.redim(x, y, redimSt);
            //if (redimSt == "SE" || redimSt == "E" || redimSt == "S")
            //{
            //MANAGE point set redim as zoom v
            float dx = (float)(this.X1 - this.X) / (float)(this.startX1 - startX);
            float dy = (float)(this.Y1 - this.Y) / (float)(startY1 - startY);
            foreach (PointWr p in points)
            {
                p.Zoom(dx, dy);
            }
            //}
        }

        public override bool contains(int x, int y)
        {
            int minX = X;
            int minY = Y;
            int maxX = X1;
            int maxY = Y1;
            foreach (PointWr p in points)
            {
                if (minX > X + p.X)
                    minX = X + p.X;
                if (minY > Y + p.Y)
                    minY = Y + p.Y;
                if (maxX < X + p.X)
                    maxX = X + p.X;
                if (maxY < Y + p.Y)
                    maxY = Y + p.Y;
            }
            return new Rectangle(minX, minY, maxX - minX, maxY - minY).Contains(x, y);
        }

        public override void Fit2grid(int gridsize)
        {
            base.Fit2grid(gridsize);

            if (points != null)
            {
                foreach (PointWr p in points)
                {
                    p.X = gridsize * (int)(p.X / gridsize);
                    p.Y = gridsize * (int)(p.Y / gridsize);
                }
            }

        }

        public override void CommitRotate(float x, float y)
        {
            //base.CommitRotate(x, y);
            //this.Rotation
            if (this.Rotation > 0)
            {
                //CENTER POINT
                float midX, midY = 0;
                midX = (this.X1 - this.X) / 2;
                midY = (this.Y1 - this.Y) / 2;

                foreach (PointWr p in points)
                {
                    p.RotateAt(midX, midY, this.Rotation);
                }
                this.Rotation = 0;
            }

        }

        public void CommitMirror(bool xmirr, bool ymirr)
        {
            foreach (PointWr p in points)
            {
                if (xmirr)
                    p.XMirror(this.Width);
                if (ymirr)
                    p.YMirror(this.Height);
            }
            //rePos();
            setupSize();
        }


        public override void DeSelect()
        {
            //base.DeSelect();
            foreach (PointWr p in points)
            {
                p.selected = false;
            }
        }

        public override void Select(int sX, int sY, int eX, int eY)
        {
            foreach (PointWr p in points)
            {
                p.selected = false;
                if (new Rectangle(sX, sY, eX - sX, eY - sY).Contains(new Point(p.X + this.X, p.Y + this.Y)))
                    p.selected = true;
            }

        }



        public override Ele Copy()
        {
            ArrayList aa = new ArrayList();
            if (this.points != null)
            {
                foreach (PointWr p in points)
                {
                    aa.Add(p.copy());
                }
            }

            PointSet newE = new PointSet(this.X, this.Y, this.X1, this.Y1, aa);

            newE.penColor = this.penColor;
            newE.penWidth = this.penWidth;
            newE.fillColor = this.fillColor;
            newE.filled = this.filled;
            newE.dashStyle = this.dashStyle;
            newE.alpha = this.alpha;
            newE.sonoUnaLinea = this.sonoUnaLinea;
            newE.Rotation = this.Rotation;
            newE.showBorder = this.showBorder;

            newE.OnGrpXRes = this.OnGrpXRes;
            newE.OnGrpX1Res = this.OnGrpX1Res;
            newE.OnGrpYRes = this.OnGrpYRes;
            newE.OnGrpY1Res = this.OnGrpY1Res;

            newE.copyGradprop(this);

            newE.Closed = this.Closed;
            newE.Curved = this.Curved;

            return newE;
        }

        public override void CopyFrom(Ele ele)
        {
            this.copyStdProp(ele, this);
            this.Rotation = ((PointSet)ele).Rotation;
            this.Curved = ((PointSet)ele).Curved;
            this.Closed = ((PointSet)ele).Closed;
        }

        public override void Select()
        {
            this.undoEle = this.Copy();
        }

        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            // To ARRAY
            PointF[] myArr = new PointF[this.points.Count];
            int i = 0;
            foreach (PointWr p in this.points)
            {
                myArr[i++] = new PointF((p.X + this.X + dx) * zoom, (p.Y + this.Y + dy) * zoom);// p.point;
            }

            if (i < 2)
                gp.AddLines(myArr);
            else
                if (this.Curved)
                    gp.AddCurve(myArr);
                else
                    gp.AddPolygon(myArr);

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
                //myBrush.Color = this.dark(this.fillColor, 5,this.alpha);
                myPen.Color = Color.Red;
                myPen.Color = this.Trasparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;

            }

            // Create a path and add the object.
            GraphicsPath myPath = new GraphicsPath();

            // To ARRAY
            PointF[] myArr = new PointF[this.points.Count];
            int i = 0;
            foreach (PointWr p in this.points)
            {
                myArr[i++] = new PointF((p.X + this.X + dx) * zoom, (p.Y + this.Y + dy) * zoom);// p.point;
                //if (p.selected)
                //  g.FillEllipse(new SolidBrush(Color.Green), (p.X + this.X + dx-2) * zoom, (p.Y + this.Y + dy-2) * zoom, 5*zoom, 5*zoom);
            }

            if (myArr.Length < 3 | !this.Curved)
            {
                if (Closed & myArr.Length >= 3)
                    myPath.AddPolygon(myArr);
                else
                    myPath.AddLines(myArr);
            }
            else
            {
                if (Closed)
                    myPath.AddClosedCurve(myArr);
                else
                    myPath.AddCurve(myArr);
                //myPath.AddBeziers(myArr);
            }


            //myPath.AddRectangle(new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom));

            Matrix translateMatrix = new Matrix();
            translateMatrix.RotateAt(this.Rotation, new PointF((this.X + dx + (int)(this.X1 - this.X) / 2) * zoom, (this.Y + dy + (int)(this.Y1 - this.Y) / 2) * zoom));
            myPath.Transform(translateMatrix);

            // Draw the transformed obj to the screen.
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

        #endregion
    }
}
