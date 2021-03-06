﻿using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using HT.VectorShapes.Shapes;

namespace HT.VectorShapes.Tools
{
    /// <summary>
    /// HT Note: biến lưu các node của một shape
    /// Node: các điểm để khi đưa chuột vào node sẽ thực hiện redim hay rotate hay zoom của một shape
    /// </summary>
    [Serializable]
    public abstract class Handle:Ele
    {
        public enumRedimOption op;
        public bool visible=true;

        public Handle()
        {
        }

        public Handle(Ele e, enumRedimOption o)
        {
            op = o;
            this.rePosition(e);        
        }


        public virtual bool isSelected()
        {
            return this.Selected;
        }

        public enumRedimOption isOver(int x, int y)
        {
            Rectangle r;
            r = new Rectangle(this.X, this.Y, this.X1 - this.X, this.Y1 - this.Y);
            if (r.Contains(x, y))
            {
                this.Selected = true;
                return this.op;
            }
            else
            {
                this.Selected = false;
                return enumRedimOption.NONE;
            }
        }

        public virtual void rePosition(Ele e)
        { }
    }

    /// <summary>
    /// 
    /// Handle object for redim/move/rotate shapes
    /// </summary>
    [Serializable]
    public class RedimHandle : Handle
    {

        public RedimHandle(Ele e, enumRedimOption o)
            : base(e, o) 
        {
            this.fillColor = Color.Black;
        }

        public override void rePosition(Ele e)
        {
            switch (this.op)
            {
                case enumRedimOption.NW:
                    this.X = e.getX() - 2;
                    this.Y = e.getY() - 2;   
                    break;
                case enumRedimOption.N:
                    this.X = e.getX() - 2 + ((e.getX1() - e.getX()) / 2);
                    this.Y = e.getY() - 2;
                    break;
                case enumRedimOption.NE:
                    this.X = e.getX1() - 2;
                    this.Y = e.getY() - 2;
                    break;
                case enumRedimOption.E:
                    this.X = e.getX1() - 2;
                    this.Y = e.getY() - 2 + (e.getY1() - e.getY()) / 2;                    
                    break;
                case enumRedimOption.SE:
                    this.X = e.getX1() - 2;
                    this.Y = e.getY1() - 2;
                    break;
                case enumRedimOption.S:
                    this.X = e.getX() - 2 + (e.getX1() - e.getX()) / 2;
                    this.Y = e.getY1() - 2;                    
                    break;
                case enumRedimOption.SW:
                    this.X = e.getX() - 2;
                    this.Y = e.getY1() - 2;
                    break;
                case enumRedimOption.W:
                    this.X = e.getX() - 2;
                    this.Y = e.getY() - 2 + (e.getY1() - e.getY()) / 2;
                    break;
                default: 
                    break;
            }
            this.X1 = this.X + 5;
            this.Y1 = this.Y + 5;

        }

        
        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            //System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(this.fillColor);
            myBrush.Color = this.Trasparency(Color.Black, 80);
            System.Drawing.Pen whitePen = new System.Drawing.Pen(System.Drawing.Color.White);            
            g.FillRectangle(myBrush, new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom));
            g.DrawRectangle(whitePen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
            myBrush.Dispose();
            whitePen.Dispose();
        }
    }

    [Serializable]
    public class ZoomHandle : Handle
    {
        public ZoomHandle(Ele e, enumRedimOption o)
            : base(e, o)
        {
            this.fillColor = Color.Red;
        }

        public override void rePosition(Ele e)
        {
            float zx = (e.Width - (e.Width * e.getGprZoomX()))/2 ;
            float zy = (e.Height - (e.Height * e.getGprZoomY())) / 2;
            this.X = (int)((e.getX1() - 2) - zx);
            this.Y = (int)((e.getY1() - 2) - zy);
            this.X1 = this.X + 5;
            this.Y1 = this.Y + 5;
            //this._zoomX = ((SelRect)e).zoomX;
            //this._zoomY = ((SelRect)e).zoomY;

        }
        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(this.fillColor);
            myBrush.Color = this.Trasparency(myBrush.Color, 80);
            System.Drawing.Pen whitePen = new System.Drawing.Pen(System.Drawing.Color.White);
            System.Drawing.Pen fillPen = new System.Drawing.Pen(this.fillColor);

            g.FillRectangle(myBrush, new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom));
            g.DrawRectangle(whitePen, (this.X + dx) * zoom , (this.Y + dy) * zoom , (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
            
            g.DrawRectangle(fillPen, (this.X + dx -1 ) * zoom, (this.Y + dy -1) * zoom, (this.X1 - this.X +2) * zoom, (this.Y1 - this.Y +2) * zoom);
            
            myBrush.Dispose();
            whitePen.Dispose();
            fillPen.Dispose();
        }
    }

    [Serializable]
    public  class RotHandle : Handle 
    {

        public RotHandle(Ele e, enumRedimOption o)
            : base(e, o)
        { }

        public override void rePosition(Ele e)
        {
            float midX, midY = 0;
            midX = (e.getX1() - e.getX()) / 2;
            midY = (e.getY1() - e.getY()) / 2;
            PointF Hp = new PointF(0, -25);
            PointF RotHP = this.rotatePoint(Hp, e.getRotation());
            midX += RotHP.X;
            midY += RotHP.Y;

            this.X = e.getX() + (int)midX - 2;
            this.Y = e.getY() + (int)midY - 2;
            this._rotation = e.getRotation();

            this.X1 = this.X + 5;
            this.Y1 = this.Y + 5;


        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            myBrush.Color = this.Trasparency(Color.Black, 80);
            System.Drawing.Pen whitePen = new System.Drawing.Pen(System.Drawing.Color.White);
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Blue, 1.5f);
            myPen.DashStyle = DashStyle.Dash;

            g.FillRectangle(myBrush, new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom));
            g.DrawRectangle(whitePen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);

            //CENTER POINT
            float midX, midY = 0;
            midX = (this.X1 - this.X) / 2;
            midY = (this.Y1 - this.Y) / 2;

            PointF Hp = new PointF(0, 25);

            PointF RotHP = this.rotatePoint(Hp, this._rotation);
            
            RotHP.X += this.X;
            RotHP.Y += this.Y;
            g.FillEllipse(myBrush, (RotHP.X + midX + dx - 3) * zoom, (RotHP.Y + dy - 3 + midY) * zoom, 6 * zoom, 6 * zoom);
            g.DrawEllipse(whitePen, (RotHP.X + midX + dx - 3) * zoom, (RotHP.Y + dy - 3 + midY) * zoom, 6 * zoom, 6 * zoom);
            g.DrawLine(myPen, (this.X +midX+ dx) * zoom, (this.Y + midY + dy) * zoom, ( RotHP.X+midX + dx) * zoom, ( RotHP.Y+midY + dy) * zoom);

            myPen.Dispose();
            myBrush.Dispose();
            whitePen.Dispose();
        }

    }


    [Serializable]
    public class PointHandle : Handle
    {
        PointWr linkedPoint;
        Ele el;
        public PointHandle(Ele e, enumRedimOption o, PointWr p)
        {
            op = o;
            this.fillColor = Color.BlueViolet;
            this.linkedPoint = p;
            el = e;
            this.rePosition(e); 
        }

        public PointWr getPoint()
        {
            return this.linkedPoint;
        }

        public override bool isSelected()
        {
            //return base.isSelected();
            return (this.Selected | this.linkedPoint.selected);
        }

        public override void move(int x, int y)
        {
            base.move(x, y);
            this.linkedPoint.X = this.X + 2 -el.getX();
            this.linkedPoint.Y = this.Y + 2 - el.getY();
            
        }

        public override void rePosition(Ele e)
        {

            this.X = (int)(linkedPoint.X + e.getX() -2);
            this.Y = (int)(linkedPoint.Y + e.getY() -2);
            this.X1 = this.X + 5;
            this.Y1 = this.Y + 5;

        }
        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(this.fillColor);
            myBrush.Color = this.Trasparency(myBrush.Color, 80);
            System.Drawing.Pen whitePen = new System.Drawing.Pen(System.Drawing.Color.White);
            
            System.Drawing.Pen fillPen = new System.Drawing.Pen(this.fillColor);
            if (this.isSelected())
                fillPen.Color = Color.Red;

            g.FillRectangle(myBrush, new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom));
            g.DrawRectangle(whitePen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);

            g.DrawRectangle(fillPen, (this.X + dx - 1) * zoom, (this.Y + dy - 1) * zoom, (this.X1 - this.X + 2) * zoom, (this.Y1 - this.Y + 2) * zoom);

            myBrush.Dispose();
            whitePen.Dispose();
            fillPen.Dispose();
        }
    }

    [Serializable]
    public class NewPointHandle : Handle
    {
        PointWr linkedPoint;
        PointWr realPoint;
        GrArc linkedArc;

        Ele el;
        public int Index = 0;

        public NewPointHandle(Ele e, enumRedimOption o, PointWr p, int i)
        {
            Index = i;
            op = o;
            this.fillColor = Color.YellowGreen;
            this.linkedPoint = p;
            el = e;
            this.rePosition(e);
        }


        public GrArc getArc()
        {
            return linkedArc;
        }
        public void setArc(GrArc a)
        {
            linkedArc = a; 
        }


        public PointWr getRealPoint()
        {
            return realPoint;
        }
        public void setRealPoint(PointWr p)
        {
            this.realPoint = p;
        }


        public PointWr getPoint()
        {
            return linkedPoint;
        }

        public override void move(int x, int y)
        {
            base.move(x, y);
            this.linkedPoint.X = this.X + 2 - el.getX();
            this.linkedPoint.Y = this.Y + 2 - el.getY();

        }

        public override void rePosition(Ele e)
        {
            this.X = (int)(linkedPoint.X + e.getX() - 1);
            this.Y = (int)(linkedPoint.Y + e.getY() - 1);
            this.X1 = this.X + 3;
            this.Y1 = this.Y + 3;
        }
        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(this.fillColor);
            myBrush.Color = this.Trasparency(myBrush.Color, 80);
            System.Drawing.Pen whitePen = new System.Drawing.Pen(System.Drawing.Color.White);
            System.Drawing.Pen fillPen = new System.Drawing.Pen(this.fillColor);

            g.FillRectangle(myBrush, new RectangleF((this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom));
            g.DrawRectangle(whitePen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);

            g.DrawRectangle(fillPen, (this.X + dx - 1) * zoom, (this.Y + dy - 1) * zoom, (this.X1 - this.X + 2) * zoom, (this.Y1 - this.Y + 2) * zoom);

            myBrush.Dispose();
            whitePen.Dispose();
            fillPen.Dispose();
        }



    }



    /// <summary>
    ///  Abstract Handle collection for redim/move/rotate shapes
    /// </summary>
    [Serializable]
    public abstract class AbstractSel : Ele
    {
        //public float zoomX=1;
        //public float zoomY=1;
        protected ArrayList handles; //HT Note: là các node hiển thị ở một Shape

        public AbstractSel(Ele el)
        {
            try
            {
                this.X = el.getX();
                this.Y = el.getY();
                this.X1 = el.getX1(); ;
                this.Y1 = el.getY1();
                this.Selected = false;
                this.rot = el.canRotate();// RotAllowed;
                this._rotation = el.getRotation();
                this.gprZoomX = el.getGprZoomX();
                this.gprZoomY = el.getGprZoomY();
                this.isaLine = el.isaLine;
                this.IamGroup = el.AmIaGroup();
                handles = new ArrayList();
                this.endMoveRedim();
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
           
        }


        public override void endMoveRedim()
        {
            base.endMoveRedim();
            foreach (Handle h in handles)
            {
                h.endMoveRedim();
            }
        }


        public void setZoom(float x, float y)
        {
            this.gprZoomX = x;
            this.gprZoomY = y;
            foreach (Handle h in handles)
            {
                h.rePosition(this);
            }
        }

        public override void Rotate(float x, float y)
        {
            base.Rotate(x, y);
            foreach (Handle h in handles)
            {
                h.rePosition(this);
            }            
        }

        public override void move(int x, int y)
        {
            base.move(x, y);
            //
            foreach (Handle h in handles)
            {
                //h.move(x, y);
                h.rePosition(this);
            }
        }

        public void showHandles(bool i)
        {
            this.IamGroup = i;
        }

        /// <summary>
        /// 2014-11-16:
        /// HT Note: Xác định vị trí chuột đưa vào sRect là redim nào
        /// On wich handle is point x,y 
        /// </summary>
        public enumRedimOption isOver(int x, int y)
        {
            enumRedimOption ret = enumRedimOption.NONE;
            foreach (Handle h in handles)
            {
                ret = h.isOver(x, y);
                if (ret != enumRedimOption.NONE)
                    return ret;
            }

            //HT Note: Nếu chứa điểm này sẽ là move shape
            if (this.contains(x, y))
                ret = enumRedimOption.C; //HT Note: Move đối tượng shape

            return ret; //HT Note: Không làm gì đối tượng shape

        }

        public override void Select()
        {
            this.undoEle = this.Copy();
        }

        public override void redim(int x, int y, string redimSt)
        {
            base.redim(x, y, redimSt); //
            foreach (Handle h in handles)
            {
                h.rePosition(this);
            }

        }

        /// <summary>
        /// 2014-11-17:
        /// Thêm kiểm tra nếu redim mới hiện các node handles
        /// </summary>
        /// <param name="g"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="zoom"></param>
        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            if (Helper.ActionStatus == enumActionStatus.REDIMSHAPE)
            {
                foreach (Handle h in handles)
                {
                    if (h.visible)
                        h.Draw(g, dx, dy, zoom);
                }
            }
        }

    }


    /// <summary>
    /// Handle tool for redim/move/rotate shapes
    /// </summary>
    [Serializable]
    public class SelRect : AbstractSel
    {
        public SelRect(Ele el)
            : base(el)
        {
            setup();
        }

        /// <summary>
        ///set ups handles
        /// </summary>
        public void setup()
        {
            if (!this.AmIaGroup())
            {
                //NW
                this.handles.Add(new RedimHandle(this, enumRedimOption.NW));
                //SE
                this.handles.Add(new RedimHandle(this, enumRedimOption.SE));
                if (!isaLine)
                {
                    //N
                    this.handles.Add(new RedimHandle(this, enumRedimOption.N));
                    if (rot)
                    {
                        //ROT
                        this.handles.Add(new RotHandle(this, enumRedimOption.ROT));
                    }
                    //NE
                    this.handles.Add(new RedimHandle(this, enumRedimOption.NE));
                    //E
                    this.handles.Add(new RedimHandle(this, enumRedimOption.E));
                    //S
                    this.handles.Add(new RedimHandle(this, enumRedimOption.S));
                    //SW
                    this.handles.Add(new RedimHandle(this, enumRedimOption.SW));
                    //W
                    this.handles.Add(new RedimHandle(this, enumRedimOption.W));
                }
            }
            else
            {
                //N
                this.handles.Add(new RedimHandle(this, enumRedimOption.N));
                if (rot)
                {
                    //ROT
                    this.handles.Add(new RotHandle(this, enumRedimOption.ROT));
                }
                //E
                this.handles.Add(new RedimHandle(this, enumRedimOption.E));
                //S
                this.handles.Add(new RedimHandle(this, enumRedimOption.S));
                //W
                this.handles.Add(new RedimHandle(this, enumRedimOption.W));
                //ZOOM
                this.handles.Add(new ZoomHandle(this, enumRedimOption.ZOOM));
            }

        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            //TEST
            //Matrix precMx = g.Transform.Clone();//store previos trasformation
            //Matrix mx = g.Transform; // get previous trasformation
            //PointF p = new PointF(zoom * (this.X + dx + (this.X1 - this.X) / 2), zoom * (this.Y + dy + (this.Y1 - this.Y) / 2));
            //mx.RotateAt(this.getRotation(), p, MatrixOrder.Append); //add a trasformation
            //g.Transform = mx;

            base.Draw(g, dx, dy, zoom);   
            //System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Blue, 1.5f);
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.GreenYellow, 1.5f);
            myPen.DashStyle = DashStyle.Dash;
            if (this.isaLine)
                g.DrawLine(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 + dx) * zoom, (this.Y1 + dy) * zoom);
            else
                g.DrawRectangle(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
            myPen.Dispose();

            //TEST
            //g.Transform = precMx;
            //precMx.Dispose();
            //mx.Dispose();
        }

    }


    /// <summary>
    /// Handle tool for redim/move/rotate Polygons
    /// </summary>
    [Serializable]
    public class SelPoly : AbstractSel
    {

        public SelPoly(Ele el)
            : base(el)
        {
            try
            {
                setup((PointSet)el);
                /*this.X = ((PointSet)el).getMinX();
                this.Y = ((PointSet)el).getMinY();
                this.X1 = ((PointSet)el).getMaxX();
                this.Y1 = ((PointSet)el).getMaxY();*/
            }
            catch (Exception ex)
            {

                string s = ex.Message;
            }
            
        }

        /// <summary>
        /// HT Code: Tạo node mới ở cuối các node của handles
        /// </summary>
        /// <param name="newP"></param>
        public void AddNewPoint(PointWr newP)
        {
            this.handles.Add(new NewPointHandle(this, enumRedimOption.NEWP, newP, this.handles.Count));
        }

        /// <summary>
        ///set ups handles
        /// </summary>
        public void setup(PointSet el)
        {
            if (rot)
            {
                //ROT
                this.handles.Add(new RotHandle(this, enumRedimOption.ROT));
            }

            PointWr prec = null;
            int c = 0;
            int minx = 0;
            int miny = 0;
            int maxx = 0;
            int maxy = 0;
            //Vẽ các node giữa các đoạn thẳng poly
            foreach (PointWr p in  el.points )
            {
                c++;
                this.handles.Add(new PointHandle(this, enumRedimOption.POLY, p));
                if (prec != null)
                {
                    minx = System.Math.Min(p.X, prec.X);
                    miny = System.Math.Min(p.Y, prec.Y);
                    maxx = System.Math.Max(p.X, prec.X);
                    maxy = System.Math.Max(p.Y, prec.Y);
                    //HT Notes: Với mỗi 2 node kế nhau, vẽ node newpoint ở giữa 2 node kế nhau
                    PointWr newP = new PointWr(minx + (int)((maxx - minx)/2), miny + (int)((maxy - miny)/2));
                    this.handles.Add(new NewPointHandle(this, enumRedimOption.NEWP, newP, c));
                }
                prec = p;
            }

            //Vẽ node newpoint ở bên ngoài (cách vị trí point cuối cùng 7 pixel)
            if (c > 0)
            {
                PointWr newP = new PointWr(prec.X + 7, prec.Y + 7);
                this.handles.Add(new NewPointHandle(this, enumRedimOption.NEWP, newP, c + 1));
            }

            //Vẽ các node redim hình
            //SE
            this.handles.Add(new RedimHandle(this, enumRedimOption.SE));
            //S
            this.handles.Add(new RedimHandle(this, enumRedimOption.S));
            //E
            this.handles.Add(new RedimHandle(this, enumRedimOption.E));
            //W
            this.handles.Add(new RedimHandle(this, enumRedimOption.W));
            //SW
            this.handles.Add(new RedimHandle(this, enumRedimOption.SW));
            //NW
            this.handles.Add(new RedimHandle(this, enumRedimOption.NW));
            //N
            this.handles.Add(new RedimHandle(this, enumRedimOption.N));
            //NE
            this.handles.Add(new RedimHandle(this, enumRedimOption.NE));
        }



        /// <summary>
        /// set ups handles
        /// </summary>
        public void reCreateCreationHandles(PointSet el)
        {
            ArrayList tmp = new ArrayList();
            foreach (Handle h in handles)
            {
                if (h is NewPointHandle)
                { 
                    tmp.Add(h);
                }
            }
            foreach (Handle h in tmp)
            {
                handles.Remove(h);
            }


            PointWr prec = null;
            int c = 0;
            int minx = 0;
            int miny = 0;
            int maxx = 0;
            int maxy = 0;
            //Vẽ các node giữa các đoạn thẳng poly
            foreach (PointWr p in el.points)
            {
                c++;
                if (prec != null)
                {
                    minx = System.Math.Min(p.X, prec.X);
                    miny = System.Math.Min(p.Y, prec.Y);
                    maxx = System.Math.Max(p.X, prec.X);
                    maxy = System.Math.Max(p.Y, prec.Y);
                    PointWr newP = new PointWr(minx + (int)((maxx - minx) / 2), miny + (int)((maxy - miny) / 2));
                    this.handles.Add(new NewPointHandle(this, enumRedimOption.NEWP, newP, c));
                }
                prec = p;
            }

            //Vẽ node newpoint ở bên ngoài (cách vị trí point cuối cùng 7 pixel)
            if (c > 0)
            {
                PointWr newP = new PointWr(prec.X + 7, prec.Y + 7);
                this.handles.Add(new NewPointHandle(this, enumRedimOption.NEWP, newP, c + 1));
            }

        }

        public ArrayList getSelPoints()
        {
            ArrayList a = new ArrayList();
            foreach (Handle h in handles)
            {
                if (h is PointHandle & h.isSelected())
                {
                    a.Add(((PointHandle)h).getPoint());
                }
            }
            return a;
        }

        /// <summary>
        /// HT Notes:
        /// Lấy index của node newpoint đang chọn
        /// </summary>
        /// <returns></returns>
        public int getIndex()
        {
            foreach (Handle h in handles)
            {
                if (h is NewPointHandle)
                {
                    if (h.Selected)
                    {
                        return ((NewPointHandle)h).Index;
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// HT Notes: 
        /// Lấy node loại NewPointHandle và node này được select (tạo node mới bằng cách click chuột vào node là NewPointHandle)
        /// </summary>
        /// <returns></returns>
        public PointWr getNewPoint()
        {
            foreach (Handle h in handles)
            {
                //Node là newpoint
                if (h is NewPointHandle)
                {
                    //Node đang được chọn
                    if (h.Selected)
                    {
                        return ((NewPointHandle)h).getPoint();
                    }
                }
            }
            return null;
        }

        public void movePoints(int dx, int dy)
        {
            //(SelPoly)(this.sRec).movePoints(dx, dy);
            foreach (Handle h in handles)
            {
                if (h is PointHandle)
                {
                    //if (h.isOver(dx,dy)!="")
                    if (h.isSelected())
                    {
                        h.move(dx, dy);
                    }
                }
            }
        }


        public override void redim(int x, int y, string redimSt)
        {
            base.redim(x, y, redimSt);
            foreach (Handle h in handles)
            {
                if ( h is NewPointHandle)
                {
                    h.visible = false;
                }
            }
        }

        public override void Rotate(float x, float y)
        {
            base.Rotate(x, y);
            foreach (Handle h in handles)
            {
                if (h is PointHandle | h is NewPointHandle)
                {
                    h.visible = false;
                }
            }
        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            //TEST
            //Matrix precMx = g.Transform.Clone();//store previos trasformation
            //Matrix mx = g.Transform; // get previous trasformation
            //PointF p = new PointF(zoom * (this.X + dx + (this.X1 - this.X) / 2), zoom * (this.Y + dy + (this.Y1 - this.Y) / 2));
            //mx.RotateAt(this.getRotation(), p, MatrixOrder.Append); //add a trasformation
            //g.Transform = mx;

            base.Draw(g, dx, dy, zoom);
           
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Blue, 1f);
            myPen.DashStyle = DashStyle.Dash;
                g.DrawRectangle(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
            myPen.Dispose();

            //TEST
            //g.Transform = precMx;
            //precMx.Dispose();
            //mx.Dispose();
        }

    }



    /// <summary>
    /// Handle tool for redim/move/rotate Graphs
    /// </summary>
    [Serializable]
    public class SelGraph : AbstractSel
    {

        public SelGraph(Ele el)
            : base(el)
        {
            setup((Graph)el);
        }


        public void manageJoins()
        {
        // foreach selected handle.
            //if handle is pointHandle
            // handle.checkJoin
            return;
        }

        /// <summary>
        ///set ups handles
        /// </summary>
        public void setup(Graph el)
        {
            if (rot)
            {
                //ROT
                this.handles.Add(new RotHandle(this, enumRedimOption.ROT));
            }

            
            int c = 0;
            foreach (PointWr p in el.points)
            {
                c++;
                this.handles.Add(new PointHandle(this, enumRedimOption.GRAPH, p));

                PointWr newP = new PointWr(p.X + 7, p.Y + 7);
                NewPointHandle tmpH = new NewPointHandle(this, enumRedimOption.NEWP, newP, c);
                tmpH.setRealPoint(p);
                this.handles.Add(tmpH);
            }

            c = 0;
            int minx = 0;
            int miny = 0;
            int maxx = 0;
            int maxy = 0;
            foreach (GrArc a in el.arcs)
            {
                c++;
                minx = System.Math.Min(a.getStartPoint().X, a.getEndPoint().X);
                miny = System.Math.Min(a.getStartPoint().Y, a.getEndPoint().Y);
                maxx = System.Math.Max(a.getStartPoint().X, a.getEndPoint().X);
                maxy = System.Math.Max(a.getStartPoint().Y, a.getEndPoint().Y);

                PointWr newP = new PointWr(minx + (int)((maxx - minx) / 2), miny + (int)((maxy - miny) / 2));
                NewPointHandle tmpH = new NewPointHandle(this, enumRedimOption.NEWP, newP, c);
                tmpH.setArc(a);
                this.handles.Add(tmpH);

            }

            //SE
            this.handles.Add(new RedimHandle(this, enumRedimOption.SE));
            //S
            this.handles.Add(new RedimHandle(this, enumRedimOption.S));
            //E
            this.handles.Add(new RedimHandle(this, enumRedimOption.E));
            //W
            this.handles.Add(new RedimHandle(this, enumRedimOption.W));
            //SW
            this.handles.Add(new RedimHandle(this, enumRedimOption.SW));
            //NW
            this.handles.Add(new RedimHandle(this, enumRedimOption.NW));
            //N
            this.handles.Add(new RedimHandle(this, enumRedimOption.N));
            //NE
            this.handles.Add(new RedimHandle(this, enumRedimOption.NE));


        }

        /// <summary>
        ///set ups handles
        /// </summary>
        public void reCreateCreationHandles(Graph el)
        {
            ArrayList tmp = new ArrayList();
            foreach (Handle h in handles)
            {
                if (h is NewPointHandle)
                {
                    tmp.Add(h);
                }
            }
            foreach (Handle h in tmp)
            {
                handles.Remove(h);
            }

            int c = 0;
            foreach (PointWr p in el.points)
            {
                    c++;
                    PointWr newP = new PointWr(p.X + 7, p.Y + 7);
                    NewPointHandle tmpH = new NewPointHandle(this, enumRedimOption.NEWP, newP, c);
                    tmpH.setRealPoint(p);
                    this.handles.Add(tmpH);
            }

            c = 0;
            int minx = 0;
            int miny = 0;
            int maxx = 0;
            int maxy = 0;
            foreach (GrArc a in el.arcs)
            {
                c++;
                minx = System.Math.Min(a.getStartPoint().X, a.getEndPoint().X);
                miny = System.Math.Min(a.getStartPoint().Y, a.getEndPoint().Y);
                maxx = System.Math.Max(a.getStartPoint().X, a.getEndPoint().X);
                maxy = System.Math.Max(a.getStartPoint().Y, a.getEndPoint().Y);

                PointWr newP = new PointWr(minx + (int)((maxx - minx) / 2), miny + (int)((maxy - miny) / 2));
                NewPointHandle tmpH = new NewPointHandle(this, enumRedimOption.NEWP, newP, c);
                tmpH.setArc(a);
                this.handles.Add(tmpH);
            }
        }

        public ArrayList getSelPoints()
        {
            ArrayList a = new ArrayList();
            foreach (Handle h in handles)
            {
                if (h is PointHandle & h.isSelected())
                {
                    a.Add(((PointHandle)h).getPoint());
                }
            }
            return a;
        }

        public int getIndex()
        {
            foreach (Handle h in handles)
            {
                if (h is NewPointHandle)
                {
                    if (h.Selected)
                    {
                        return ((NewPointHandle)h).Index;
                    }
                }
            }
            return 0;
        }


        public PointWr getNewPoint()
        {
            foreach (Handle h in handles)
            {
                if (h is NewPointHandle)
                {
                    if (h.Selected)
                    {
                        return ((NewPointHandle)h).getPoint();
                    }
                }
            }
            return null;
        }

        public NewPointHandle getNewPointHandle()
        {
            foreach (Handle h in handles)
            {
                if (h is NewPointHandle)
                {
                    if (h.Selected)
                    {
                        return (NewPointHandle)h;
                    }
                }
            }
            return null;
        }

        public void movePoints(int dx, int dy)
        {
            //(SelPoly)(this.sRec).movePoints(dx, dy);
            foreach (Handle h in handles)
            {
                if (h is PointHandle)
                {
                    //if (h.isOver(dx,dy)!="")
                    if (h.isSelected())
                    {
                        h.move(dx, dy);
                    }
                }
            }
        }


        public override void redim(int x, int y, string redimSt)
        {
            base.redim(x, y, redimSt);
            foreach (Handle h in handles)
            {
                if (h is NewPointHandle)
                {
                    h.visible = false;
                }
            }
        }

        public override void Rotate(float x, float y)
        {
            base.Rotate(x, y);
            foreach (Handle h in handles)
            {
                if (h is PointHandle | h is NewPointHandle)
                {
                    h.visible = false;
                }
            }
        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            //TEST
            //Matrix precMx = g.Transform.Clone();//store previos trasformation
            //Matrix mx = g.Transform; // get previous trasformation
            //PointF p = new PointF(zoom * (this.X + dx + (this.X1 - this.X) / 2), zoom * (this.Y + dy + (this.Y1 - this.Y) / 2));
            //mx.RotateAt(this.getRotation(), p, MatrixOrder.Append); //add a trasformation
            //g.Transform = mx;

            base.Draw(g, dx, dy, zoom);

            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Blue, 1f);
            myPen.DashStyle = DashStyle.Dash;
            g.DrawRectangle(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
            myPen.Dispose();

            //TEST
            //g.Transform = precMx;
            //precMx.Dispose();
            //mx.Dispose();
        }

    }

}
