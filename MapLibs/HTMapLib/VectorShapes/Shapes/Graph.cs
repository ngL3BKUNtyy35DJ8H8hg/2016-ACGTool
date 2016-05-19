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
    /// Graph ( extends Ele ) 
    /// </summary>
    [Serializable]
    public class Graph : Ele
    {
        //private int _rotation;
        public ArrayList points;
        public ArrayList arcs;

        public Graph(int x, int y, int x1, int y1, ArrayList a)
        {
            this.X = x;
            this.Y = y;
            this.X1 = x1;
            this.Y1 = y1;
            this.Selected = true;
            //
            this.points = a;
            this.arcs = new ArrayList();

            // build arcs
            if (points != null)
            {

                PointWr prec = null;
                foreach (PointWr p in points)
                {
                    if (prec != null)
                    {
                        this.arcs.Add(new GrArc(prec, p));
                    }
                    prec = p;

                }
            }


            this.setupSize();
            //
            this.endMoveRedim();
            this.Rotation = 0;
            this.rot = true; //can rotate?
        }


        public Graph(int x, int y, int x1, int y1, ArrayList a, ArrayList b)
        {
            this.X = x;
            this.Y = y;
            this.X1 = x1;
            this.Y1 = y1;
            this.Selected = true;
            //
            this.points = a;
            this.arcs = b;


            this.setupSize();
            //
            this.endMoveRedim();
            this.Rotation = 0;
            this.rot = true; //can rotate?
        }



        [Editor(typeof(myTypeEditor),
             typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Graph"), Description("Points")]
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




        [Category("1"), Description("Graph")]
        public string ObjectType
        {
            get
            {
                return "Graph";
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
                    p.X = p.X - minNegativeX; ;
                    p.Y = p.Y - minNegativeY; ;
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


        public void manageJoins()
        {
            return;
        }


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
                this.rePos();//!
            }
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
            float dx = (float)(this.X1 - this.X) / (float)(this.startX1 - startX);
            float dy = (float)(this.Y1 - this.Y) / (float)(startY1 - startY);
            foreach (PointWr p in points)
            {
                p.Zoom(dx, dy);
            }
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

        public void addArc(PointWr first, PointWr second)
        {
            bool found = false;
            foreach (GrArc a in this.arcs)
            {
                if ((a.start == first & a.end == second) | (a.start == second & a.end == first))
                {
                    found = true;
                    break;
                }
            }
            if (!found)
                this.arcs.Add(new GrArc(first, second));
        }

        public void delArcs(PointWr first)
        {
            ArrayList tmpL = new ArrayList();
            //GrArc found = null;
            foreach (GrArc a in this.arcs)
            {
                if ((a.start == first) | (a.end == first))
                {
                    tmpL.Add(a);
                }
            }
            if (tmpL != null)
            {
                foreach (GrArc a in tmpL)
                {
                    this.arcs.Remove(a);
                }
            }

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
            int i = 1;
            //clear all id
            if (this.points != null)
            {
                foreach (PointWr p in this.points)
                {
                    p.id = 0;
                }
            }
            else
            {
                return null;
            }

            ArrayList pp = new ArrayList();//POINTS

            ArrayList aa = new ArrayList();//ARCS

            if (this.arcs != null)
            {
                foreach (GrArc a in arcs)
                {
                    //aa.Add(a.copy());
                    PointWr s = a.start;
                    PointWr e = a.end;
                    PointWr newS = null;
                    PointWr newE = null;
                    // first point 
                    if (s.id > 0)
                    {
                        //s aleady copied
                        foreach (PointWr p in pp)
                        {
                            if (p.id == s.id)
                            {
                                newS = p;
                                break;
                            }
                        }

                    }
                    else
                    {
                        //NOT yet copied
                        s.id = i++;
                        newS = s.copy();
                        newS.id = s.id;
                        pp.Add(newS);
                    }


                    // second point 
                    if (e.id > 0)
                    {
                        //e aleady copied
                        foreach (PointWr p in pp)
                        {
                            if (p.id == e.id)
                            {
                                newE = p;
                                break;
                            }
                        }

                    }
                    else
                    {
                        //NOT yet copied
                        e.id = i++;
                        newE = e.copy();
                        newE.id = e.id;
                        pp.Add(newE);
                    }
                    GrArc newA = new GrArc(newS, newE);
                    aa.Add(newA);

                }
            }

            Graph newEle = new Graph(this.X, this.Y, this.X1, this.Y1, pp, aa);

            newEle.penColor = this.penColor;
            newEle.penWidth = this.penWidth;
            newEle.fillColor = this.fillColor;
            newEle.filled = this.filled;
            newEle.dashStyle = this.dashStyle;
            newEle.alpha = this.alpha;
            newEle.sonoUnaLinea = this.sonoUnaLinea;
            newEle.Rotation = this.Rotation;
            newEle.showBorder = this.showBorder;

            newEle.OnGrpXRes = this.OnGrpXRes;
            newEle.OnGrpX1Res = this.OnGrpX1Res;
            newEle.OnGrpYRes = this.OnGrpYRes;
            newEle.OnGrpY1Res = this.OnGrpY1Res;

            newEle.copyGradprop(this);

            return newEle;

        }

        public override void CopyFrom(Ele ele)
        {
            this.copyStdProp(ele, this);
            this.Rotation = ((Graph)ele).Rotation;
        }

        public override void Select()
        {
            this.undoEle = this.Copy();
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


            // To ARRAY
            //PointF[] myArr = new PointF[this.points.Count];
            //int i = 0;
            foreach (GrArc a in this.arcs)
            {
                // Create a path and add the object.
                GraphicsPath myPath = new GraphicsPath();

                PointF s = new PointF((a.getStartPoint().X + this.X + dx) * zoom,
                    (a.getStartPoint().Y + this.Y + dy) * zoom);
                PointF e = new PointF((a.getEndPoint().X + this.X + dx) * zoom,
                    (a.getEndPoint().Y + this.Y + dy) * zoom);
                myPath.AddLine(s, e);

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

            }



            myPen.Dispose();
            if (myBrush != null)
                myBrush.Dispose();
        }

        #endregion
    }
}
