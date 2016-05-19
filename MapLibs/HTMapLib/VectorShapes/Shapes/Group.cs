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
    /// Group ( extends Ele ) 
    /// </summary>
    [Serializable]
    public class Group : Ele
    {
        // sub objects contained in group
        ArrayList objs;

        // Manage the gropu likr a Graphic path 
        bool _grapPath = false;
        bool _xMirror = false;
        bool _yMirror = false;

        GroupDisplay _GroupDisplay = GroupDisplay.Default;

        // the name of the group
        string _name = "";
        public static int Ngrp; // used to generate names

        /// <summary>
        /// .Ctor) 
        /// </summary>
        public Group(ArrayList a)
        {
            this.IamGroup = true;
            objs = a;

            int minX = +32000;
            int maxX = -32000;
            int minY = +32000;
            int maxY = -32000;

            foreach (Ele e in objs)
            {
                if (e.getX() < minX)
                    minX = e.getX();
                if (e.getY() < minY)
                    minY = e.getY();
                if (e.getX1() > maxX)
                    maxX = e.getX1();
                if (e.getY1() > maxY)
                    maxY = e.getY1();
                e.Selected = false;
            }

            this.X = minX;
            this.Y = minY;
            this.X1 = maxX;
            this.Y1 = maxY;
            this.Selected = true;
            this.endMoveRedim();
            this.Rotation = 0;
            this.rot = true; //can rotate?
            this.Name = "Itm" + Ngrp.ToString();
            Ngrp++;
        }

        #region OVERRIDEN PROPERIES

        #region GET/SET

        [Category("Group"), Description("Shape List")]
        public Ele[] Objs
        {
            get
            {
                Ele[] aar = new Ele[objs.Count];
                int i = 0;
                foreach (Ele e in objs)
                {
                    aar[i++] = e;
                }
                return aar;
            }
        }

        [Category("Group"), Description("Grp.Display")]
        public GroupDisplay GroupDisplay
        {
            get
            {
                return this._GroupDisplay;
            }
            set
            {
                this._GroupDisplay = value;
            }
        }

        [Category("Group"), Description("X Mirror ON/OFF ")]
        public bool XMirror
        {
            get
            {
                return this._xMirror;
            }
            set
            {
                this._xMirror = value;
            }
        }
        [Category("Group"), Description("Y Mirror ON/OFF ")]
        public bool YMirror
        {
            get
            {
                return this._yMirror;
            }
            set
            {
                this._yMirror = value;
            }
        }



        [Category("Group"), Description("X Scale Zoom ")]
        public float GrpZoomX
        {
            get
            {
                return this.gprZoomX;
            }
            set
            {
                if (value > 0)
                    this.gprZoomX = value;
            }
        }

        [Category("Group"), Description("Y Scale Zoom ")]
        public float GrpZoomY
        {
            get
            {
                return this.gprZoomY;
            }
            set
            {
                if (value > 0)
                    this.gprZoomY = value;
            }
        }



        [Category("GradientBrush"), Description("True: use gradient fill color")]
        public override bool UseGradientLineColor
        {
            get
            {
                return base.UseGradientLineColor;
            }
            set
            {
                base.UseGradientLineColor = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.UseGradientLineColor = value;
                    }
            }
        }

        [Category("GradientBrush"), Description("End Color Position [0..1])")]
        public override float EndColorPosition
        {
            get
            {
                return base.EndColorPosition;
            }
            set
            {
                base.EndColorPosition = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.EndColorPosition = value;
                    }
            }
        }


        [Category("GradientBrush"), Description("Gradient End Color")]
        public override Color EndColor
        {
            get
            {
                return base.EndColor;
            }
            set
            {
                base.EndColor = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.EndColor = value;
                    }
            }
        }


        [Category("GradientBrush"), Description("End Color Trasparency")]
        public override int EndAlpha
        {
            get
            {
                return base.EndAlpha;
            }
            set
            {
                base.EndAlpha = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.EndAlpha = value;
                    }
            }
        }

        [Category("GradientBrush"), Description("Gradient Dimension")]
        public override int GradientLen
        {
            get
            {
                return base.GradientLen;
            }
            set
            {
                base.GradientLen = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.GradientLen = value;
                    }
            }
        }

        [Category("GradientBrush"), Description("Gradient Angle")]
        public override int GradientAngle
        {
            get
            {
                return base.GradientAngle;
            }
            set
            {
                base.GradientAngle = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.GradientAngle = value;
                    }
            }
        }

        public override int alpha
        {
            get
            {
                return base.alpha;
            }
            set
            {
                base.alpha = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.alpha = value;
                    }
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != "")
                    _name = value;
            }
        }


        [Description("Manege group as a graphic path.")]
        public bool graphPath
        {
            get
            {
                return _grapPath;
            }
            set
            {

                _grapPath = value;
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

        public override Color fillColor
        {
            get
            {
                return base.fillColor;
            }
            set
            {
                base.fillColor = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.fillColor = value;
                    }
            }
        }

        public override bool filled
        {
            get
            {
                return base.filled;
            }
            set
            {
                base.filled = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.filled = value;
                    }
            }
        }

        public override Color penColor
        {
            get
            {
                return base.penColor;
            }
            set
            {
                base.penColor = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.penColor = value;
                    }
            }
        }

        public override float penWidth
        {
            get
            {
                return base.penWidth;
            }
            set
            {
                base.penWidth = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.penWidth = value;
                    }
            }
        }

        public override bool showBorder
        {
            get
            {
                return base.showBorder;
            }
            set
            {
                base.showBorder = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.showBorder = value;
                    }
            }
        }

        public override DashStyle dashStyle
        {
            get
            {
                return base.dashStyle;
            }
            set
            {
                base.dashStyle = value;
                if (this.objs != null)
                    foreach (Ele e in this.objs)
                    {
                        e.dashStyle = value;
                    }
            }
        }
        #endregion

        #endregion

        public override void AfterLoad()
        {
            base.AfterLoad();
            foreach (Ele e in objs)
            {
                e.AfterLoad();
            }
        }

        public override void endMoveRedim()
        {
            base.endMoveRedim();
            foreach (Ele e in objs)
            {
                e.endMoveRedim();
            }
        }

        public override ArrayList deGroup()
        {
            return this.objs;
        }

        public override void move(int x, int y)
        {
            foreach (Ele e in objs)
            {
                e.move(x, y);
            }
            this.X = this.startX - x;
            this.Y = this.startY - y;
            this.X1 = this.startX1 - x;
            this.Y1 = this.startY1 - y;
        }

        [Category("1"), Description("GroupObj")]
        public string ObjectType
        {
            get
            {
                return "Group";
            }
        }

        public override void ShowEditor(richForm2 f)
        {
            foreach (Ele e in this.objs)
            {
                if (e.OnGrpDClick)
                {
                    e.ShowEditor(f);
                }
            }
        }

        public void Load_IMG()
        {
            foreach (Ele e in this.objs)
            {
                if (e.OnGrpDClick)
                {
                    if (e is ImgBox)
                    {
                        ((ImgBox)e).Load_IMG();
                    }
                    if (e is Group)
                    {
                        ((Group)e).Load_IMG();
                    }
                }
            }
        }

        public void setZoom(int x, int y)
        {
            float dx = (this.X1 - x) * 2 > this.Width ? (this.X1 - x) : (this.X1 - x) * 2;
            float dy = (this.Y1 - y) * 2 > this.Height ? (this.Y1 - y) : (this.Y1 - y) * 2;
            if (this.Width < dx)
            {
                dx = dx;
            }
            this.GrpZoomX = (this.Width - dx) / this.Width;
            this.GrpZoomY = (this.Height - dy) / this.Height;
            //this.X1 = (int)(this.Width - dx);
            //this.Y1 = (int)(this.Height - dy) / this.Height;            
        }

        public override Ele Copy()
        {
            //Copy chils
            ArrayList l1 = new ArrayList();
            foreach (Ele e in this.objs)
            {
                Ele e1 = e.Copy();
                l1.Add(e1);
            }

            Group newE = new Group(l1);
            /*
            newE.penColor = this.penColor;
            newE.penWidth = this.penWidth;
            newE.fillColor = this.fillColor;
            newE.filled = this.filled;
            newE.dashStyle = this.dashStyle;
            newE.alpha = this.alpha;
            newE.sonoUnaLinea = this.sonoUnaLinea;
            //newE.Rotation = this.Rotation;
            newE.showBorder = this.showBorder;
            */
            newE.Rotation = this.Rotation;
            newE._grapPath = this._grapPath;
            newE.gprZoomX = this.gprZoomX;
            newE.gprZoomY = this.gprZoomY;
            newE.IamGroup = this.IamGroup;
            newE._name = this.Name + "_" + Group.Ngrp.ToString();
            newE.OnGrpDClick = this.OnGrpDClick;
            newE.OnGrpXRes = this.OnGrpXRes;
            newE.OnGrpX1Res = this.OnGrpX1Res;
            newE.OnGrpYRes = this.OnGrpYRes;
            newE.OnGrpY1Res = this.OnGrpY1Res;

            newE.GroupDisplay = this.GroupDisplay;

            if (newE._grapPath)
            {
                newE.penColor = this.penColor;
                newE.penWidth = this.penWidth;
                newE.fillColor = this.fillColor;
                newE.filled = this.filled;
                newE.dashStyle = this.dashStyle;
                newE.alpha = this.alpha;
                newE.sonoUnaLinea = this.sonoUnaLinea;
                newE.Rotation = this.Rotation;
                newE.showBorder = this.showBorder;

                newE.UseGradientLineColor = this.UseGradientLineColor;
                newE.GradientAngle = this.GradientAngle;
                newE.GradientLen = this.GradientLen;
                newE.EndAlpha = this.EndAlpha;
                newE.EndColor = this.EndColor;
                newE.EndColorPosition = this.EndColorPosition;

            }

            return newE;

        }

        public override void CopyFrom(Ele ele)
        {
            this.copyStdProp(ele, this);
            //
            //this._grapPath = ele._grapPath;
        }

        public override void Select()
        {
            this.undoEle = this.Copy();
        }

        public override void redim(int x, int y, string redimSt)
        {
            foreach (Ele e in objs)
            {
                switch (redimSt)
                {
                    case "N":
                        base.redim(x, y, redimSt);
                        if (e.OnGrpYRes != OnGroupResize.Nothing)
                        {
                            if (e.OnGrpYRes == OnGroupResize.Move)
                            {
                                e.move(0, -y);
                            }
                            else
                            {
                                e.redim(0, y, redimSt);
                            }
                        }
                        break;
                    case "E":
                        base.redim(x, y, redimSt);
                        if (e.OnGrpX1Res != OnGroupResize.Nothing)
                        {
                            if (e.OnGrpX1Res == OnGroupResize.Move)
                            {
                                e.move(-x, 0);
                            }
                            else
                            {
                                e.redim(x, 0, redimSt);
                            }
                        }
                        break;
                    case "S":
                        base.redim(x, y, redimSt);
                        if (e.OnGrpY1Res != OnGroupResize.Nothing)
                        {
                            if (e.OnGrpY1Res == OnGroupResize.Move)
                            {
                                e.move(0, -y);
                            }
                            else
                            {
                                e.redim(0, y, redimSt);
                            }
                        }
                        break;
                    case "W":
                        base.redim(x, y, redimSt);
                        if (e.OnGrpXRes != OnGroupResize.Nothing)
                        {
                            if (e.OnGrpXRes == OnGroupResize.Move)
                            {
                                e.move(-x, 0);
                            }
                            else
                            {
                                e.redim(x, 0, redimSt);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }



        public override void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        {
            foreach (Ele e in this.objs)
            {
                //e.AddGp(gp, dx, dy, zoom);
                e.AddGraphPath(gp, dx, dy, zoom);
            }
        }


        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            //Matrix precMx = g.Transform.Clone();//store previos trasformation
            GraphicsState gs = g.Save();//store previos trasformation
            Matrix mx = g.Transform; // get previous trasformation

            PointF p = new PointF(zoom * (this.X + dx + (this.X1 - this.X) / 2), zoom * (this.Y + dy + (this.Y1 - this.Y) / 2));
            if (this.Rotation > 0)
                mx.RotateAt(this.Rotation, p, MatrixOrder.Append); //add a trasformation

            //X MIRROR  //Y MIRROR
            if (this._xMirror || this._yMirror)
            {
                mx.Translate(-(this.X + this.Width / 2 + dx) * zoom, -(this.Y + this.Height / 2 + dy) * zoom, MatrixOrder.Append);
                if (this._xMirror)
                    mx.Multiply(new Matrix(-1, 0, 0, 1, 0, 0), MatrixOrder.Append);
                if (this._yMirror)
                    mx.Multiply(new Matrix(1, 0, 0, -1, 0, 0), MatrixOrder.Append);
                mx.Translate((this.X + this.Width / 2 + dx) * zoom, (this.Y + this.Height / 2 + dy) * zoom, MatrixOrder.Append);
            }

            if (this.GrpZoomX > 0 && this.GrpZoomY > 0)
            {
                mx.Translate((-1) * zoom * (this.X + dx + (this.X1 - this.X) / 2), (-1) * zoom * (this.Y + dy + (this.Y1 - this.Y) / 2), MatrixOrder.Append);
                mx.Scale(this.GrpZoomX, this.GrpZoomY, MatrixOrder.Append);
                mx.Translate(zoom * (this.X + dx + (this.X1 - this.X) / 2), zoom * (this.Y + dy + (this.Y1 - this.Y) / 2), MatrixOrder.Append);
            }
            g.Transform = mx;

            //g.ResetTransform();
            //The next drawn objs are translated over origin , rotated and then traslated again.
            //g.TranslateTransform((-1) * zoom * (this.X + dx + (this.X1 - this.X) / 2), (-1) * zoom * (this.Y + dy + (this.Y1 - this.Y) / 2), MatrixOrder.Append);
            //g.RotateTransform(this.Rotation, MatrixOrder.Append);
            //g.TranslateTransform(zoom * (this.X + dx + (this.X1 - this.X) / 2), zoom * (this.Y + dy + (this.Y1 - this.Y) / 2), MatrixOrder.Append);

            if (this._grapPath)
            #region path
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

                GraphicsPath gp = new GraphicsPath();
                //gp.FillMode = FillMode.Winding;
                //gp.FillMode = FillMode.Alternate;
                //Region rr = new Region();
                foreach (Ele e in objs)
                {

                    //e.AddGp(gp, dx, dy, zoom);
                    e.AddGraphPath(gp, dx, dy, zoom);
                    //rr.Intersect(gp);
                    //rr.Xor(gp);
                }
                //g.SetClip(rr,CombineMode.Intersect);
                if (this.filled)
                {

                    g.FillPath(myBrush, gp);
                    if (this.showBorder)
                        g.DrawPath(myPen, gp);
                }
                else
                {
                    g.DrawPath(myPen, gp);
                }
                //g.ResetClip();
                //TEST START     
                float gridSize = 4;
                float gridRot = 45;

                //FillWithLines(g, dx, dy, zoom, gp, gridSize, gridRot);
                //TEST END

                if (myBrush != null)
                    myBrush.Dispose();
                myPen.Dispose();
            }
            #endregion
            else
            {
                //MANAGE This.GroupDisplay
                Region rr = new Region();
                if (GroupDisplay != GroupDisplay.Default)
                {
                    bool first = true;
                    foreach (Ele e in objs)
                    {
                        GraphicsPath gp = new GraphicsPath(FillMode.Winding);
                        //e.AddGp(gp, dx, dy, zoom);
                        e.AddGraphPath(gp, dx, dy, zoom);
                        if (first)
                        {
                            rr.Intersect(gp);
                        }
                        else
                        {
                            switch (GroupDisplay)
                            {
                                case GroupDisplay.Intersect:
                                    rr.Intersect(gp);
                                    break;
                                case GroupDisplay.Xor:
                                    rr.Xor(gp);
                                    break;
                                case GroupDisplay.Exclude:
                                    rr.Exclude(gp);
                                    break;
                                default:
                                    break;
                            }
                        }
                        first = false;
                    }
                    g.SetClip(rr, CombineMode.Intersect);
                }

                foreach (Ele e in objs)
                {
                    e.Draw(g, dx, dy, zoom);
                }
                if (GroupDisplay != GroupDisplay.Default)
                {
                    g.ResetClip();
                }
            }

            g.Restore(gs);//restore previos trasformation

            if (this.Selected)
            {
                //System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(this.fillColor);
                Brush myBrush = getBrush(dx, dy, zoom);
                //myBrush.Color = this.Trasparency(this.fillColor, this.alpha);

                System.Drawing.Pen myPen = new System.Drawing.Pen(this.penColor, scaledPenWidth(zoom));
                myPen.DashStyle = this.dashStyle;

                //myBrush.Color = this.dark(this.fillColor, 5, this.alpha);
                myPen.Color = Color.Red;
                myPen.Color = this.Trasparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;
                g.DrawRectangle(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
                if (myBrush != null)
                    myBrush.Dispose();
                myPen.Dispose();

            }

            mx.Dispose();
            //precMx.Dispose();

        }
    }
}
