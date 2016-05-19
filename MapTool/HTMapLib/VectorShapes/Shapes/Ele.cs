using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace HTMapLib.VectorShapes.Shapes
{
    // base Element
    [Serializable]
    public abstract class Ele : Object
    {
        //protected PathGradientBrush pgb;//TEST
        //protected PenWR pen=new PenWR(Color.Black);//TEST

        static public Single dpix;
        static public Single dpiy;

        protected bool IamGroup = false;
        protected bool rot; // Can Rotate

        //HT Code: lưu thông tin tọa độ map
        public double LeftLon;
        public double TopLat;
        public double RightLon;
        public double BottomLat;
        /// <summary>
        /// Biến lưu các tọa độ Lon, Lat của từng point (dùng cho polyline, polygon)
        /// </summary>
        public ArrayList mapPoints;

        //Start point
        public int X;
        public int Y;
        //End point
        public int X1;
        public int Y1;

        //Grouped objs properties. When you decide to create a group with the
        // selected objs, U can preset these props in the objs: Move,Resize,Nothing
        // Move: On the resize of the group the grouped obj moves
        // Resize: On the resize of the group the grouped obj resizes
        // Nothing: On the resize of the group the grouped obj mantains its position and dimension.
        // When you resize the group using the X handle (West):
        protected OnGroupResize _onGroupXRes = OnGroupResize.Resize;
        // When you resize the group using the X1 handle (East):
        protected OnGroupResize _onGroupX1Res = OnGroupResize.Resize;
        // When you resize the group using the Y handle (North):
        protected OnGroupResize _onGroupYRes = OnGroupResize.Resize;
        // When you resize the group using the Y handle (South):
        protected OnGroupResize _onGroupY1Res = OnGroupResize.Resize;

        // When double click on Group obj, forward it to sub-obj
        protected bool _onGroupDoubleClick = true;

        // Store start position during moving/resizing
        protected int startX;
        protected int startY;
        protected int startX1;
        protected int startY1;

        // some pen properties
        protected LineCap start;
        protected LineCap end;
        protected DashStyle dashstyle;

        protected int _rotation;

        private Color _penColor;
        private float _penWidth;
        private Color _fillColor;
        private bool _filled;
        private bool _showBorder;
        private DashStyle _dashStyle;
        private int _alpha;

        //LINEAR GRADIENT
        private bool _useGradientLine = false;
        private Color _endColor = Color.White;
        private int _endalpha = 255;
        private int _gradientLen = 0;
        private int _gradientAngle = 0;
        private float _endColorPos = 1f;

        //Group obj zoom 
        protected float gprZoomX = 1f;
        protected float gprZoomY = 1f;

        public bool sonoUnaLinea; //I am a Line

        public bool Selected;

        public bool Deleted;

        public Ele undoEle;

        //HT Code: Lưu name object
        public string _name;

        //HT Code: Hiển thị text object
        public string _text;

        //protected ArrayList links; //TEST 2206

        //public Ele()
        //{
        //    penColor = Color.Black;
        //    penWidth = 1f;
        //    fillColor = Color.Black;
        //    filled = false;
        //    showBorder = true;
        //    this.dashstyle = DashStyle.Solid;
        //    this.alpha = 255;
        //    //links = new ArrayList(); //TEST 2206
        //}

        public Ele()
        {
            penColor = Color.Red;
            penWidth = 2f;
            fillColor = Color.Black;
            filled = false;
            showBorder = true;
            this.dashstyle = DashStyle.Solid;
            this.alpha = 255;
            //links = new ArrayList(); //TEST 2206
        }

        ~Ele()
        {
            // System.Console.WriteLine("Destrojed obj:  {0}/{1}", this.getX().ToString(), this.getY().ToString());
        }

        #region GET/SET

        /*
        [Editor(typeof(PenTypeEditor),
             typeof(System.Drawing.Design.UITypeEditor))]
        [CategoryAttribute("Apparence"), DescriptionAttribute("Pen")]
        public PenWR PEN
        {
            set
            {
                pen = value;
            }
            get
            {
                return pen;
            }
        }
        */


        /*
        public PathGradientBrush PGB
        {
            set
            {
                pgb = value;
            }
            get
            {
                return pgb;
            }
        }
        */

        /*
        [Editor(typeof(myTypeEditor),
             typeof(System.Drawing.Design.UITypeEditor))]
        public string TEST
        {
            set
            {
                test = value;
            }
            get
            {
                return test;
            }
        }
        */

        public bool canRotate()
        {
            return rot;
        }
        public int getRotation()
        {
            if (canRotate())
                return _rotation;
            else
                return 0;
        }

        public void addRotation(int a)
        {
            //if (canRotate())
            this._rotation += a;
        }

        public bool AmIaGroup()
        {
            return this.IamGroup;
            //return false;
        }

        [Category("Apperance"), Description("Name ")]
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        [Category("Apperance"), Description("Text ")]
        public string Text
        {
            get
            {
                return this._text;
            }
        }

        [Category("Position"), Description("X ")]
        public int PosX
        {
            get
            {
                return this.X;
            }
        }
        [Category("Position"), Description("Y ")]
        public int PosY
        {
            get
            {
                return this.Y;
            }
        }
        [Category("Dimention"), Description("Width ")]
        public int Width
        {
            get
            {
                return System.Math.Abs(this.X1 - this.X);
            }
        }
        [Category("Dimention"), Description("Height ")]
        public int Height
        {
            get
            {
                return System.Math.Abs(this.Y1 - this.Y);
            }
        }

        [Category("Dimention mm"), Description("Width (mm)")]
        public int Width_mm
        {
            get
            {
                return (int)(System.Math.Abs(this.X1 - this.X) / Ele.dpix * 25.4);
            }
        }
        [Category("Dimention mm"), Description("Height (mm)")]
        public int Height_mm
        {
            get
            {
                return (int)((System.Math.Abs(this.Y1 - this.Y) / Ele.dpiy) * 25.4);
            }
        }

        [Category("Dimention mm"), Description(" ")]
        public Single dpiX
        {
            get
            {
                return Ele.dpix;
            }
        }
        [Category("Dimention mm"), Description(" ")]
        public Single dpiY
        {
            get
            {
                return Ele.dpiy;
            }
        }


        [Category("Group Behav"), Description("On Group Resize X")]
        public OnGroupResize OnGrpXRes
        {
            set
            {
                _onGroupXRes = value;
            }
            get
            {
                return _onGroupXRes;
            }
        }
        [Category("Group Behav"), Description("On Group Resize X1")]
        public OnGroupResize OnGrpX1Res
        {
            set
            {
                _onGroupX1Res = value;
            }
            get
            {
                return _onGroupX1Res;
            }
        }
        [Category("Group Behav"), Description("On Group Resize Y")]
        public OnGroupResize OnGrpYRes
        {
            set
            {
                _onGroupYRes = value;
            }
            get
            {
                return _onGroupYRes;
            }
        }
        [Category("Group Behav"), Description("On Group Resize Y1")]
        public OnGroupResize OnGrpY1Res
        {
            set
            {
                _onGroupY1Res = value;
            }
            get
            {
                //return System.Math.Abs(this.Y1 - this.Y);
                return _onGroupY1Res;
            }
        }
        [Category("Group Behav"), Description("Manage On Group Double Click")]
        public bool OnGrpDClick
        {
            set
            {
                _onGroupDoubleClick = value;
            }
            get
            {
                return _onGroupDoubleClick;
            }
        }

        [Category("Appearance"), Description("Set Border Dash Style ")]
        public virtual DashStyle dashStyle
        {
            get
            {
                return _dashStyle;
            }
            set
            {
                _dashStyle = value;
            }
        }
        [Category("Appearance"), Description("Show border when filled or contains Text")]
        public virtual bool showBorder
        {
            get
            {
                return _showBorder;
            }
            set
            {
                _showBorder = value;
            }
        }
        [Category("Appearance"), Description("Pen Color")]
        public virtual Color penColor
        {
            get
            {
                return _penColor;
            }
            set
            {
                _penColor = value;
            }
        }
        [Category("Appearance"), Description("Fill Color")]
        public virtual Color fillColor
        {
            get
            {
                return _fillColor;
            }
            set
            {
                _fillColor = value;
            }
        }
        [Category("Appearance"), Description("Pen Width")]
        public virtual float penWidth
        {
            get
            {
                return _penWidth;
            }
            set
            {
                _penWidth = value;
            }
        }
        [Category("Appearance"), Description("Filled/Unfilled")]
        public virtual bool filled
        {
            get
            {
                return _filled;
            }
            set
            {
                _filled = value;
            }
        }
        [Category("Appearance"), Description("Trasparency")]
        public virtual int alpha
        {
            get
            {
                return _alpha;
            }
            set
            {
                if (value < 0)
                    _alpha = 0;
                else
                    if (value > 255)
                        _alpha = 255;
                    else
                        _alpha = value;
            }
        }

        [Category("GradientBrush"), Description("True: use gradient fill color")]
        public virtual bool UseGradientLineColor
        {
            get
            {
                return _useGradientLine;
            }
            set
            {
                _useGradientLine = value;
            }
        }

        [Category("GradientBrush"), Description("End Color Position [0..1])")]
        public virtual float EndColorPosition
        {
            get
            {
                return _endColorPos;
            }
            set
            {
                if (value > 1)
                    value = 1;
                if (value < 0)
                    value = 0;
                _endColorPos = value;
            }
        }


        [Category("GradientBrush"), Description("Gradient End Color")]
        public virtual Color EndColor
        {
            get
            {
                return _endColor;
            }
            set
            {
                _endColor = value;
            }
        }


        [Category("GradientBrush"), Description("End Color Trasparency")]
        public virtual int EndAlpha
        {
            get
            {
                return _endalpha;
            }
            set
            {
                if (value < 0)
                    _endalpha = 0;
                else
                    if (value > 255)
                        _endalpha = 255;
                    else
                        _endalpha = value;
            }
        }

        [Category("GradientBrush"), Description("Gradient Dimension")]
        public virtual int GradientLen
        {
            get
            {
                return _gradientLen;
            }
            set
            {
                if (value >= 0)
                    _gradientLen = value;
                else
                    _gradientLen = 0;
            }
        }

        [Category("GradientBrush"), Description("Gradient Angle")]
        public virtual int GradientAngle
        {
            get
            {
                return _gradientAngle;
            }
            set
            {
                _gradientAngle = value;
            }
        }


        public int getX()
        {
            return X;
        }
        public int getY()
        {
            return Y;
        }
        public int getX1()
        {
            return X1;
        }
        public int getY1()
        {
            return Y1;
        }

        public float getGprZoomX()
        {
            return gprZoomX;
        }
        public float getGprZoomY()
        {
            return gprZoomY;
        }

        public void UpdateRect(int x, int y, int x1, int y1)
        {
            X = x;
            Y = y;
            X1 = x1;
            Y1 = y1;
        }



        #endregion

        #region virtual metods
        /// <summary>
        /// Draw this shape to a graphic ogj. 
        /// </summary>
        public virtual void Draw(Graphics g, int dx, int dy, float zoom)
        { }

        /// <summary>
        /// Add this shape to a graphic path. 
        /// </summary>        
        public void AddGraphPath(GraphicsPath gp, int dx, int dy, float zoom)
        {
            GraphicsPath tmpGp = new GraphicsPath();
            AddGp(tmpGp, dx, dy, zoom);// AddGp is defined in derived classes
            Matrix translateMatrix = new Matrix();
            translateMatrix.RotateAt(this._rotation, new PointF((this.X + dx + (int)(this.X1 - this.X) / 2) * zoom, (this.Y + dy + (int)(this.Y1 - this.Y) / 2) * zoom));
            tmpGp.Transform(translateMatrix);
            gp.AddPath(tmpGp, true);
        }

        /// <summary>
        /// Add this shape to a graphic path. 
        /// </summary>
        public virtual void AddGp(GraphicsPath gp, int dx, int dy, float zoom)
        { }

        /// <summary>
        /// Used to degroup a grouped shape. Returns a list of shapes.
        /// </summary>
        public virtual ArrayList deGroup()
        {
            return null;
        }
        /// <summary>
        /// Select this shape.
        /// </summary>
        public virtual void Select()
        { }
        /// <summary>
        /// Select this shape.
        /// </summary>
        public virtual void Select(RichTextBox r)
        { }
        /// <summary>
        /// Select this shape.
        /// </summary>
        public virtual void Select(int sX, int sY, int eX, int eY)
        { }
        /// <summary>
        /// Deselct this shape.
        /// </summary>
        public virtual void DeSelect()
        { }

        /// <summary>
        /// Used for RTF editor.
        /// </summary>
        public virtual void ShowEditor(richForm2 f)
        { }

        /// <summary>
        /// Used after the load from file. Manage here the creation of object not serialized.
        /// </summary>
        public virtual void AfterLoad()
        { }

        /// <summary>
        /// Copy the properties from another shape
        /// </summary>
        public virtual void CopyFrom(Ele ele)
        { }

        /// <summary>
        /// Clone this shape
        /// </summary>
        public virtual Ele Copy()
        {
            return null;
        }

        /// <summary>
        /// Copy the gradient properties. 
        /// </summary>
        protected void copyGradprop(Ele ele)
        {
            _useGradientLine = ele._useGradientLine;
            _endColor = ele._endColor;
            _endalpha = ele._endalpha;
            _gradientLen = ele._gradientLen;
            _gradientAngle = ele._gradientAngle;
            _endColorPos = ele._endColorPos;
        }

        //public virtual void Rotate(float x, float y)
        //{ }

        #endregion

        /// <summary>
        /// To fill a shape with parallel lines 
        /// </summary>
        protected void FillWithLines(Graphics g, int dx, int dy, float zoom, GraphicsPath myPath, float gridSize, float gridRot)
        {
            GraphicsState gs = g.Save();//store previos trasformation
            g.SetClip(myPath, CombineMode.Intersect);
            Matrix mx = g.Transform; // get previous trasformation
            PointF p = new PointF(zoom * (this.X + dx + (this.X1 - this.X) / 2), zoom * (this.Y + dy + (this.Y1 - this.Y) / 2));
            if (this._rotation > 0)
                mx.RotateAt(this._rotation, p, MatrixOrder.Append); //add a trasformation
            mx.RotateAt(gridRot, p, MatrixOrder.Append); //add a trasformation
            g.Transform = mx;

            int max = System.Math.Max(this.Width, this.Height);
            System.Drawing.Pen linePen = new System.Drawing.Pen(System.Drawing.Color.Gray);
            //linePen.DashStyle = DashStyle.Dash;
            int nY = (int)(max * 3 / (gridSize));
            for (int i = 0; i <= nY; i++)
            {
                g.DrawLine(linePen, (this.X - max + dx) * zoom, (this.Y - max + dy + i * gridSize) * zoom, (this.X + dx + max * 2) * zoom, (this.Y - max + dy + i * gridSize) * zoom);
            }
            linePen.Dispose();
            g.Restore(gs);
            //g.ResetClip();
        }

        /// <summary>
        /// Used to define pen with. 
        /// </summary>
        protected float scaledPenWidth(float zoom)
        {
            if (zoom < 0.1f)
                zoom = 0.1f;
            return this.penWidth * zoom;
        }

        /// <summary>
        /// Adapt the shape at the gridsize 
        /// </summary>
        public virtual void Fit2grid(int gridsize)
        {
            this.startX = gridsize * (int)(this.startX / gridsize);
            this.startY = gridsize * (int)(this.startY / gridsize);
            this.startX1 = gridsize * (int)(this.startX1 / gridsize);
            this.startY1 = gridsize * (int)(this.startY1 / gridsize);
        }

        /// <summary>
        /// Confirm the rotation 
        /// </summary>
        public virtual void CommitRotate(float x, float y)
        {
        }

        /// <summary>
        /// Rotate
        /// </summary>
        public virtual void Rotate(float x, float y)
        {
            float tmp = _rotate(x, y);
            this._rotation = (int)tmp;
        }

        /// <summary>
        /// Return a point obtained rotating p by RotAng respect 0,0 
        /// </summary>
        protected PointF rotatePoint(PointF p, int RotAng)
        {
            double RotAngF = RotAng * Math.PI / 180;
            double SinVal = Math.Sin(RotAngF);
            double CosVal = Math.Cos(RotAngF);
            float Nx = (float)(p.X * CosVal - p.Y * SinVal);
            float Ny = (float)(p.Y * CosVal + p.X * SinVal);
            return new PointF(Nx, Ny);
        }

        /// <summary>
        /// Gets a rotation angle from a vertical line from the center of the shape and a line
        /// from the center to the point (x,y)
        /// </summary>
        protected float _rotate(float x, float y)
        {
            //
            Point c = new Point((int)(this.X + (this.X1 - this.X) / 2), (int)(this.Y + (this.Y1 - this.Y) / 2));
            float dx = x - c.X;
            float dy = y - c.Y;
            float b = 0f;
            float alpha = 0f;
            float f = 0f;
            if ((dx > 0) & (dy > 0))
            {
                b = 90;
                alpha = (float)Math.Abs((Math.Atan((double)(dy / dx)) * (180 / Math.PI)));
            }
            else
                if ((dx <= 0) & (dy >= 0))
                {
                    b = 180;
                    if (dy > 0)
                    {
                        alpha = (float)Math.Abs((Math.Atan((double)(dx / dy)) * (180 / Math.PI)));
                    }
                    else if (dy == 0)
                    {
                        b = 270;
                    }
                }
                else
                    if ((dx < 0) & (dy < 0))
                    {
                        b = 270;
                        alpha = (float)Math.Abs((Math.Atan((double)(dy / dx)) * (180 / Math.PI)));
                    }
                    else
                    {
                        b = 0;
                        alpha = (float)Math.Abs((Math.Atan((double)(dx / dy)) * (180 / Math.PI)));
                    }
            f = (b + alpha);
            return f;
        }

        private float getDimX()
        {
            return (float)(System.Math.Sqrt(System.Math.Pow(this.Width, 2) + System.Math.Pow(this.Height, 2)) - this.Width) / 2;
        }
        private float getDimY()
        {
            return (float)(System.Math.Sqrt(System.Math.Pow(this.Width, 2) + System.Math.Pow(this.Height, 2)) - this.Height) / 2;
        }

        /// <summary>
        /// gets a brush from the properties of the shape
        /// </summary>
        protected Brush getBrush(int dx, int dy, float zoom)
        {
            if (this.filled)
            {
                if (this.UseGradientLineColor)
                {
                    float wid;
                    float hei;
                    if (this.GradientLen > 0)
                    {
                        wid = this.GradientLen;
                        hei = this.GradientLen;
                    }
                    else
                    {
                        wid = ((this.X1 - this.X) + 2 * getDimX()) * zoom;
                        hei = ((this.Y1 - this.Y) + 2 * getDimY()) * zoom;
                    }
                    LinearGradientBrush br = new LinearGradientBrush(
                        new RectangleF((this.X - getDimX() + dx) * zoom, (this.Y - getDimY() + dy) * zoom, wid, hei)
                        , this.Trasparency(this.fillColor, this.alpha)
                        , this.Trasparency(this.EndColor, this.EndAlpha)
                        , this.GradientAngle
                        , true);
                    br.SetBlendTriangularShape(this.EndColorPosition, 0.95f);
                    br.WrapMode = WrapMode.TileFlipXY;
                    return br;
                }
                else
                {
                    return new SolidBrush(this.Trasparency(this.fillColor, this.alpha));
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Copy the properties common to all shapes. 
        /// </summary>
        protected void copyStdProp(Ele from, Ele to)
        {
            to.X = from.X;
            to.X1 = from.X1;
            to.Y = from.Y;
            to.Y1 = from.Y1;
            to.start = from.start;
            to.startX = from.startX;
            to.startX1 = from.startX1;
            to.startY = from.startY;
            to.startY1 = from.startY1;
            to.sonoUnaLinea = from.sonoUnaLinea;
            to.alpha = from.alpha;
            to.dashstyle = from.dashstyle;
            to.fillColor = from.fillColor;
            to.filled = from.filled;
            to.penColor = from.penColor;
            to._penWidth = from.penWidth;
            to.showBorder = from.showBorder;
            to._onGroupX1Res = from._onGroupX1Res;
            to._onGroupXRes = from._onGroupXRes;
            to._onGroupY1Res = from._onGroupY1Res;
            to._onGroupYRes = from._onGroupYRes;

            to._useGradientLine = from._useGradientLine;
            to._endColor = from._endColor;
            to._endalpha = from._endalpha;
            to._gradientLen = from._gradientLen;
            to._gradientAngle = from._gradientAngle;
            to._endColorPos = from._endColorPos;

        }

        /// <summary>
        /// 2 points distance
        /// </summary>
        protected int Dist(int x, int y, int x1, int y1)
        {
            return (int)System.Math.Sqrt(System.Math.Pow((x - x1), 2) + System.Math.Pow((y - y1), 2));
        }

        /// <summary>
        /// Make a color darker or lighter
        /// </summary>
        protected Color dark(Color c, int v, int a)
        {

            int r = c.R;
            r = r - v;
            if (r < 0)
                r = 0;
            if (r > 255)
                r = 255;
            int green = c.G;
            green = green - v;
            if (green < 0)
                green = 0;
            if (green > 255)
                green = 255;
            int b = c.B;
            b = b - v;
            if (b < 0)
                b = 0;
            if (b > 255)
                b = 255;
            if (a > 255)
                a = 255;
            if (a < 0)
                a = 0;

            return Color.FromArgb(a, r, green, b);

        }

        /// <summary>
        /// Make a color Tresparent/Solid
        /// </summary>
        protected Color Trasparency(Color c, int v)
        {
            if (v < 0)
                v = 0;
            if (v > 255)
                v = 255;

            return Color.FromArgb(v, c);
        }


        /// <summary>
        /// true if the shape contains the point x,y
        /// </summary>
        public virtual bool contains(int x, int y)
        {

            if (sonoUnaLinea)
            {
                int appo = Dist(x, y, this.X, this.Y) + Dist(x, y, this.X1, this.Y1);
                int appo1 = Dist(this.X1, this.Y1, this.X, this.Y) + 7;

                return appo < appo1;
            }
            else
            {
                return new Rectangle(this.X, this.Y, this.X1 - this.X, this.Y1 - this.Y).Contains(x, y);
            }


            // LINES HIT TEST
            /*
            GraphicsPath tmpGp = new GraphicsPath();
            AddGp(tmpGp, 0, 0, 1);// AddGp is defined in derived classes

            Point p = new Point(x, y);
            Pen pen = new Pen(this.penColor, this.penWidth);
            tmpGp.Widen(pen);
            pen.Dispose();

            if (tmpGp.IsVisible(p))
                return true;
            return false; 
            */
        }

        /// <summary>
        /// Moves the shape by x,y
        /// </summary>
        public virtual void move(int x, int y)
        {
            this.X = this.startX - x;
            this.Y = this.startY - y;
            this.X1 = this.startX1 - x;
            this.Y1 = this.startY1 - y;
        }

        /* 
        public void move(int x, int startx,int y, int starty)
        {
            int dx = startx - x;
            int dy = starty - y;
            this.X = this.X - dx;
            this.Y = this.Y - dy;
            this.X1 = this.X1 - dx;
            this.Y1 = this.Y1 - dy;
        }
        */

        /// <summary>
        /// Redim the shape 
        /// </summary>
        public virtual void redim(int x, int y, string redimSt)
        {
            switch (redimSt)
            {
                case "NW":
                    this.X = this.startX + x;
                    this.Y = this.startY + y;
                    break;
                case "N":
                    this.Y = this.startY + y;
                    break;
                case "NE":
                    this.X1 = this.startX1 + x;
                    this.Y = this.startY + y;
                    break;
                case "E":
                    this.X1 = this.startX1 + x;
                    break;
                case "SE":
                    this.X1 = this.startX1 + x;
                    this.Y1 = this.startY1 + y;
                    break;
                case "S":
                    this.Y1 = this.startY1 + y;
                    break;
                case "SW":
                    this.X = this.startX + x;
                    this.Y1 = this.startY1 + y;
                    break;
                case "W":
                    this.X = this.startX + x;
                    break;
                default:
                    break;
            }

            if (!this.sonoUnaLinea)
            {   // manage redim limits
                if (this.X1 <= this.X)
                    this.X1 = this.X + 10;
                if (this.Y1 <= this.Y)
                    this.Y1 = this.Y + 10;
            }

        }



        /// <summary>
        /// Called at the end of move/redim of the shape. Stores startX|Y|X1|Y1 
        /// for a correct rendering during object move/redim
        /// </summary>
        public virtual void endMoveRedim()
        {
            this.startX = this.X;
            this.startY = this.Y;
            this.startX1 = this.X1;
            this.startY1 = this.Y1;
        }

        /// <summary>
        /// HT Code: Cập nhật tọa độ theo kinh, vĩ độ
        /// </summary>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="lon1"></param>
        /// <param name="lat1"></param>
        public void UpdateLonLatCoord(double lon, double lat, double lon1, double lat1, ArrayList mPoints = null)
        {
            //Các biến lưu kinh vĩ độ
            this.LeftLon = lon;
            this.TopLat = lat;
            this.RightLon = lon1;
            this.BottomLat = lat1;
            this.mapPoints = mPoints;
        }
    }
}
