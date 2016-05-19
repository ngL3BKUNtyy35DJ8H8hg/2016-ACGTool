using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO; 						//streamer io
using System.Runtime.Serialization;     // io
using System.Runtime.Serialization.Formatters.Binary; // io
using System.Drawing.Printing;
using HTMapLib.Properties;
using HTMapLib.VectorShapes.Shapes;
using MapXLib;
using Point = System.Drawing.Point;


namespace HTMapLib
{
    
    public partial class ucVectShapes_Form :  ucVectShapes
    {

        //private bool _isMapControl;
        public ucVectShapes_Form()
        {
            InitializeComponent();
            Graphics g;

            this.Load += new System.EventHandler(this.ucVectShapes_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucVectShapes_Paint);
            this.DoubleClick += new System.EventHandler(this.ucVectShapes_DoubleClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ucVectShapes_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ucVectShapes_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ucVectShapes_MouseUp);
            this.Resize += new System.EventHandler(this.ucVectShapes_Resize);
            g = this.CreateGraphics();

            myInit(g);

            //from Ilango.M 
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true); // added line

        }

        public override void ucVectShapes_Paint(object sender, PaintEventArgs e)
        {
            // from Ilango.M
            //this.redraw(false);
            this.redraw(e.Graphics, false);
        }

        #region DRAWING

        /// <summary>
        /// redraws this.s on this control
        /// All=true : redraw all graphic
        /// All=false : redraw only selected objects
        /// </summary>
        public override void redraw(bool All)
        {
            Graphics g = this.CreateGraphics();
            redraw(g, All);
            g.Dispose();
        }

        /// <summary>
        /// Vẽ theo graphics của this control
        /// </summary>
        /// <param name="g"></param>
        /// <param name="All"></param>
        private void RedrawUserControl(Graphics g, bool All)
        {
            if (fit2grid & this.gridSize > 0)
            {
                this.startX = this.gridSize * (int)(startX / this.gridSize);
                this.startY = this.gridSize * (int)(startY / this.gridSize);
            }

            this.GraphicSetUp(g);

            if (All)
            {
                // Redraw static objects
                // in the back Layer 
                Graphics backG;
                backG = Graphics.FromImage(offScreenBackBmp);
                this.GraphicSetUp(backG);
                //backG.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                //backG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                backG.Clear(this.BackColor);

                if (this.BackgroundImage != null)
                    backG.DrawImage(this.BackgroundImage, 0, 0);

                // Render the grid
                if (this.gridSize > 0)
                {
                    System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.LightGray);
                    int nX = (int)(this.Width / (this.gridSize * Zoom));
                    for (int i = 0; i <= nX; i++)
                    {
                        backG.DrawLine(myPen, i * this.gridSize * Zoom, 0, i * this.gridSize * Zoom, this.Height);
                    }
                    int nY = (int)(this.Height / (this.gridSize * Zoom));
                    for (int i = 0; i <= nY; i++)
                    {
                        backG.DrawLine(myPen, 0, i * this.gridSize * Zoom, this.Width, i * this.gridSize * Zoom);
                    }
                    myPen.Dispose();
                }

                // Draws unselected objects
                s.DrawUnselected(backG, this.dx, this.dy, this.Zoom);

                backG.Dispose();
            }

            //Do Double Buffering
            Graphics offScreenDC;
            offScreenDC = Graphics.FromImage(offScreenBmp);
            offScreenDC.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            offScreenDC.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            offScreenDC.Clear(this.BackColor);


            // I draw the background image with statics objects
            offScreenDC.DrawImageUnscaled(this.offScreenBackBmp, 0, 0);

            // Now I draw the dynamic objects on the buffer
            s.DrawSelected(offScreenDC, this.dx, this.dy, this.Zoom);

            //this.DrawMesure(offScreenDC);

            // Now I draw the graphics effects (creation and selection )
            #region Creation/Selection/PenPoimts plus A4 margin
            //Draw Red creation Rect/Line
            if (this.MouseSx & this.Status == enumStatus.DRAWRECT)
            {
                System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red, 1.5f);
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                myPen.StartCap = System.Drawing.Drawing2D.LineCap.DiamondAnchor;
                //myPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

                //if (this.Option == "LI" || this.Option == "POLY" || this.Option == "GRAPH")
                if (this.Option == enumOption.LI || this.Option == enumOption.POLY || this.Option == enumOption.GRAPH)
                {
                    offScreenDC.DrawLine(myPen, (startX + this.dx) * this.Zoom, (startY + this.dy) * this.Zoom, (tempX + this.dx) * this.Zoom, (tempY + this.dy) * this.Zoom);
                }
                else if (this.Option == enumOption.NEWPOLY) //else if (this.Option == "NEWPOLY")
                {
                    if (this.polyPoints.Count > 0)
                    {
                        GraphicsPath myPath = new GraphicsPath();
                        // To ARRAY
                        PointF[] myArr = new PointF[this.polyPoints.Count + 1];
                        int i = 0;
                        foreach (Point p in this.polyPoints)
                        {
                            myArr[i++] = new PointF((p.X + this.dx) * this.Zoom, (p.Y + this.dy) * this.Zoom);
                        }
                        //Thêm temp point đang vẽ
                        myArr[i] = new PointF((tempX + this.dx) * this.Zoom, (tempY + this.dy) * this.Zoom);

                        // myPath.AddPolygon(myArr);
                        myPath.AddLines(myArr);
                        //g.DrawLines(myPen, myArr);

                        offScreenDC.DrawPath(myPen, myPath);

                    }
                }
                else
                {
                    offScreenDC.DrawRectangle(myPen, (this.startX + this.dx) * this.Zoom, (this.startY + this.dy) * this.Zoom, (tempX - this.startX) * this.Zoom, (tempY - this.startY) * this.Zoom);
                }
                myPen.Dispose();
            }

            //Draw selection Rect
            if (this.MouseSx & this.Status == enumStatus.SELRECT)
            {
                System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Green, 1.5f);
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                offScreenDC.DrawRectangle(myPen, (this.startX + this.dx) * this.Zoom, (this.startY + this.dy) * this.Zoom, (tempX - this.startX) * this.Zoom, (tempY - this.startY) * this.Zoom);
                myPen.Dispose();
            }

            //Draw msg
            this.drawDebugInfo(offScreenDC);

            //Draw A4 margin
            if (this.A4)
            {
                System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Blue, 0.5f);
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                offScreenDC.DrawRectangle(myPen, (1 + this.dx) * this.Zoom, (1 + this.dy) * this.Zoom, 810 * this.Zoom, 1140 * this.Zoom);
                myPen.Dispose();
            }

            //Draw Pen construction shape
            if (PenPointList != null)
            {
                System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red, 1.5f);
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                myPen.StartCap = System.Drawing.Drawing2D.LineCap.DiamondAnchor;

                // To ARRAY
                PointF[] myArr = new PointF[this.VisPenPointList.Count];
                int i = 0;
                foreach (PointWr p in this.VisPenPointList)
                {
                    myArr[i++] = new PointF((startX + p.X + this.dx) * this.Zoom, (startY + p.Y + this.dy) * this.Zoom);// p.point;

                }

                if (myArr.Length > 1)
                    offScreenDC.DrawCurve(myPen, myArr);
            }

            #endregion

            // I draw the buffer on the graphic of my control
            g.DrawImageUnscaled(offScreenBmp, 0, 0);

            offScreenDC.Dispose();



            //g.Dispose();
        }

        public override void redraw(Graphics g,bool All)
        {
            //Nếu có dùng MapControl thì vẽ theo MapControl
            RedrawUserControl(g, All);  
        }

        #endregion

        private void ucVectShapes_Resize(object sender, EventArgs e)
        {
            if (this.Width > 0 & this.Height > 0)
            {
                offScreenBackBmp = new Bitmap(this.Width, this.Height);
                offScreenBmp = new Bitmap(this.Width, this.Height);
                redraw(true);
            }
        }

        private void ucVectShapes_Load(object sender, EventArgs e)
        {

        }



       


        #region USER CONTROL MOUSE EVENT MANAGMENT

        private void ucVectShapes_MouseDown(object sender, MouseEventArgs e)
        {
            MouseDownEvent(sender, e.X, e.Y, e.Button);
            if (e.Button == MouseButtons.Right)
            {
                this.Cursor = Cursors.Cross;
            }
        }

        private void ucVectShapes_MouseMove(object sender, MouseEventArgs e)
        {
            MouseMoveEvent(sender, e.X, e.Y, e.Button);
            if (e.Button == MouseButtons.None)
            #region NO muse button pressed
            {
                //Nếu đang vẽ đối tượng
                if (this.MouseSx)
                {
                    tempX = (int)(e.X / Zoom);
                    tempY = (int)(e.Y / Zoom);
                    if (fit2grid & this.gridSize > 0)
                    {
                        tempX = this.gridSize * (int)((e.X / Zoom) / this.gridSize);
                        tempY = this.gridSize * (int)((e.Y / Zoom) / this.gridSize);
                    }
                    tempX = tempX - this.dx;
                    tempY = tempY - this.dy;

                }
                //if (this.Option == "select")
                if (this.Option == enumOption.SELECT)
                {
                    if (this.s.sRec != null)
                    {
                        string st = this.s.sRec.isOver((int)(e.X / Zoom) - this.dx, (int)(e.Y / Zoom - this.dy));
                        //Chuyển đổi string sang enum
                        this.redimStatus = (enumRedimStatus)Enum.Parse(typeof(enumRedimStatus), st);
                        switch (this.redimStatus)
                        {
                            case enumRedimStatus.NEWP:
                                this.Cursor = Cursors.SizeAll;
                                this.Cursor = AddPointCur;
                                /*To change the cursor
                                Cursor cc = new Cursor("NewPoint.ico");
                                this.Cursor = cc;
                                */
                                break;
                            case enumRedimStatus.POLY:
                                this.Cursor = Cursors.SizeAll;
                                break;
                            case enumRedimStatus.GRAPH:
                                this.Cursor = Cursors.SizeAll;
                                break;
                            case enumRedimStatus.ROT:
                                this.Cursor = Cursors.SizeAll;
                                break;
                            case enumRedimStatus.C:
                                this.Cursor = Cursors.Hand;
                                break;
                            case enumRedimStatus.NW:
                                this.Cursor = Cursors.SizeNWSE;
                                break;
                            case enumRedimStatus.N:
                                this.Cursor = Cursors.SizeNS;
                                break;
                            case enumRedimStatus.NE:
                                this.redimStatus = enumRedimStatus.NE;
                                this.Cursor = Cursors.SizeNESW;
                                break;
                            case enumRedimStatus.E:
                                this.redimStatus = enumRedimStatus.E;
                                this.Cursor = Cursors.SizeWE;
                                break;
                            case enumRedimStatus.SE:
                                this.redimStatus = enumRedimStatus.SE;
                                this.Cursor = Cursors.SizeNWSE;
                                break;
                            case enumRedimStatus.S:
                                this.redimStatus = enumRedimStatus.S;
                                this.Cursor = Cursors.SizeNS;
                                break;
                            case enumRedimStatus.SW:
                                this.Cursor = Cursors.SizeNESW;
                                break;
                            case enumRedimStatus.W:
                                this.Cursor = Cursors.SizeWE;
                                break;
                            case enumRedimStatus.ZOOM:
                                this.redimStatus = enumRedimStatus.ZOOM;
                                this.Cursor = Cursors.SizeNWSE;
                                break;
                            default:
                                this.Cursor = Cursors.Default;
                                this.redimStatus = enumRedimStatus.NO;
                                break;
                        }
                    }
                    else
                    {
                        this.Cursor = Cursors.Default;
                        this.redimStatus = enumRedimStatus.NO;
                    }
                }
                else if (this.Option == enumOption.NEWPOLY) //else if (this.Option == "NEWPOLY")
                {
                    redraw(false);
                    //this.Refresh();
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    this.redimStatus = enumRedimStatus.NO;
                }
            }
            #endregion
            //redraw();
        }

        protected override void AddPoly(ArrayList aa, int minX, int minY, int maxX, int maxY)
        {
            //Cho biến x1 = minY, y1 = minY vì bên trong hàm addPoly sẽ tự động xác định lại x1, y1
            this.s.addPoly(minX, minY, maxX, maxY, this.CreationPenColor, CreationFillColor,
                           CreationPenWidth, CreationFilled, aa, false);
        }

        public void ucVectShapes_MouseUp(object sender, MouseEventArgs e)
        {
            MouseUpEvent(sender, e.X, e.Y, e.Button);
        }

        /*public void ucVectShapes_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            #region left up
            {
                int tmpX = (int)((e.X) / Zoom - this.dx);
                int tmpY = (int)((e.Y) / Zoom - this.dy);
                if (fit2grid & this.gridSize > 0)
                {
                    tmpX = this.gridSize * (int)((e.X / Zoom - this.dx) / this.gridSize);
                    tmpY = this.gridSize * (int)((e.Y / Zoom - this.dy) / this.gridSize);
                }

                switch (this.Option)
                {
                    #region selectrect
                    case enumOption.SELECT:
                        if (this.Status != enumStatus.REDIM)
                        {
                            this.s.click((int)((e.X) / Zoom - this.dx), (int)((e.Y) / Zoom - this.dy), this.r);
                        }
                        else
                        {
                            if (this.s.selEle is PointSet)
                            {//POLY MANAGEMENT START
                                s.addPoint();
                                //((PointSet)this.s.selEle).rePos();
                                if (fit2grid & this.gridSize > 0)
                                {
                                    this.s.Fit2grid(this.gridSize);
                                    //this.s.sRec = new SelPoly(this.s.selEle);//create handling rect
                                }
                                switch (this.redimStatus)
                                {
                                    case enumRedimStatus.ROT:
                                        this.s.selEle.CommitRotate(tmpX, tmpY);
                                        //this.s.sRec = new SelPoly(this.s.selEle);//create handling rect                                     
                                        break;
                                    default:
                                        break;
                                }//POLY MANAGEMENT END
                            }

                            if (this.s.selEle is Graph)
                            {//GRAPH MANAGEMENT START
                                s.addPoint();
                                //((PointSet)this.s.selEle).rePos();
                                if (fit2grid & this.gridSize > 0)
                                {
                                    this.s.Fit2grid(this.gridSize);
                                    //this.s.sRec = new SelPoly(this.s.selEle);//create handling rect
                                }
                                switch (this.redimStatus)
                                {
                                    case enumRedimStatus.ROT:
                                        this.s.selEle.CommitRotate(tmpX, tmpY);
                                        //this.s.sRec = new SelPoly(this.s.selEle);//create handling rect                                     
                                        break;
                                    default:
                                        break;
                                }//GRAPH MANAGEMENT END
                            }

                        }

                        if (this.Status == enumStatus.SELRECT)
                        {
                            if ((((e.X) / Zoom - this.dx - this.startX) + ((e.Y) / Zoom - this.dy - this.startY)) > 12)
                            {
                                // manage multi objeect selection
                                this.s.multiSelect(this.startX, this.startY, (int)((e.X) / Zoom - this.dx), (int)((e.Y) / Zoom - this.dy), this.r);
                            }
                        }

                        changeStatus(enumStatus.NONE);
                        break;
                    #endregion
                    #region Rect
                    case enumOption.DR: //DrawRect

                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            this.s.addRect(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor, CreationPenWidth, CreationFilled);
                            //this.Option = "select";
                            changeOption(enumOption.SELECT);
                        }
                        break;
                    #endregion
                    //#region LINk TEST
                    //case "LINK": //Link//test
                    //    if (this.Status == enumStatus.DRAWRECT)
                    //    {


                    //        changeOption(enumOption.SELECT);
                    //    }
                    //    break;
                    //#endregion
                    #region Arc
                    case enumOption.ARC: //Arc
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            this.s.addArc(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor, CreationPenWidth, CreationFilled);
                            changeOption(enumOption.SELECT);
                        }
                        break;
                    #endregion
                    #region Poly & Pen & Graph
                    case enumOption.PEN:
                        //if (this.Status == enumStatus.DRAWRECT)
                        //{
                        this.s.addPoly(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor, CreationPenWidth, CreationFilled, PenPointList, true);
                        PenPointList = null;
                        VisPenPointList = null;
                        changeOption(enumOption.SELECT);
                        //}
                        break;
                    case enumOption.POLY: //polygon/pointSet/curvedshape..
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            ArrayList aa = new ArrayList();
                            aa.Add(new PointWr(0, 0));
                            //aa.Add(new PointWr(System.Math.Abs(startX - tmpX), System.Math.Abs(startY - tmpY)));
                            aa.Add(new PointWr(tmpX - startX, tmpY - startY));
                            this.s.addPoly(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor, CreationPenWidth, CreationFilled, aa, false);
                            changeOption(enumOption.SELECT);
                        }
                        break;
                    case enumOption.GRAPH:
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            ArrayList aa = new ArrayList();
                            aa.Add(new PointWr(0, 0));
                            //aa.Add(new PointWr(System.Math.Abs(startX - tmpX), System.Math.Abs(startY - tmpY)));
                            aa.Add(new PointWr(tmpX - startX, tmpY - startY));
                            this.s.addGraph(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor, CreationPenWidth, CreationFilled, aa);
                            changeOption(enumOption.SELECT);
                        }
                        break;
                    #endregion
                    #region RRect
                    case enumOption.DRR: //DrawRRect

                        if (this.Status == enumStatus.DRAWRECT)
                        {

                            this.s.addRRect(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor, CreationPenWidth, CreationFilled);
                            //this.Option = "select";
                            changeOption(enumOption.SELECT);
                        }
                        break;
                    #endregion
                    #region Ellipse
                    case enumOption.ELL: //DrawEllipse

                        if (this.Status == enumStatus.DRAWRECT)
                        {

                            this.s.addEllipse(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor, CreationPenWidth, CreationFilled);
                            //this.Option = "select";
                            changeOption(enumOption.SELECT);
                        }
                        break;
                    #endregion
                    #region DrawTextBox
                    case enumOption.TB: //DrawTextBox
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            editorFrm.ShowDialog();
                            this.Cursor = Cursors.Arrow;
                            this.s.addTextBox(startX, startY, tmpX, tmpY, r, this.CreationPenColor, CreationFillColor, CreationPenWidth, CreationFilled);
                            changeOption(enumOption.SELECT);
                        }
                        break;
                    #endregion
                    #region DrawSimpleTextBox
                    case enumOption.STB: //DrawSimpleTextBox
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            editorFrm.ShowDialog();
                            this.Cursor = Cursors.Arrow;
                            this.s.addSimpleTextBox(startX, startY, tmpX, tmpY, r, this.CreationPenColor, CreationFillColor, CreationPenWidth, CreationFilled);
                            changeOption(enumOption.SELECT);
                        }
                        break;
                    #endregion
                    #region ImgBox
                    case enumOption.IB: //DrawImgBox

                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            // load image

                            string f_name = this.imgLoader();
                            this.s.addImgBox(startX, startY, tmpX, tmpY, f_name, this.CreationPenColor, CreationPenWidth);
                            //this.Option = "select";
                            changeOption(enumOption.SELECT);
                        }
                        break;
                    #endregion
                    #region Line
                    case enumOption.LI: //Draw Line

                        if (this.Status == enumStatus.DRAWRECT)
                        {

                            this.s.addLine(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationPenWidth);
                            //this.Option = "select";
                            changeOption(enumOption.SELECT);
                        }
                        break;
                    #endregion
                    default:
                        //this.Status = "";
                        changeStatus(enumStatus.NONE);
                        break;
                }


                // store start X,Y,X1,Y1 of selected item
                if (this.s.selEle != null)
                {
                    if (this.s.selEle is PointSet)
                    {//POLY MANAGEMENT START
                        ((PointSet)this.s.selEle).setupSize();
                        this.s.sRec = new SelPoly(this.s.selEle);//create handling rect
                    }

                    if (this.s.selEle is Graph)
                    {//GRAPH MANAGEMENT START

                        ((Graph)this.s.selEle).setupSize();
                        this.s.sRec = new SelGraph(this.s.selEle);//create handling rect
                    }


                    if (this.redimStatus != enumRedimStatus.NO)
                    {
                        this.s.endMove();
                    }

                    if (this.s.sRec != null)
                    {
                        this.s.sRec.endMoveRedim();
                    }
                }
                // show properties
                PropertyEventArgs e1 = new PropertyEventArgs(this.s.getSelectedArray(), this.s.RedoEnabled(), this.s.UndoEnabled());
                objectSelected(this, e1);// raise event

                redraw(true); //redraw all=true 

                this.MouseSx = false; // end pressing SX
            }
            #endregion
            else
            #region right up
            {
                // show properties
                PropertyEventArgs e1 = new PropertyEventArgs(this.s.getSelectedArray(), this.s.RedoEnabled(), this.s.UndoEnabled());
                objectSelected(this, e1);// raise event
            }
            #endregion
        }
        */

        private void ucVectShapes_DoubleClick(object sender, EventArgs e)
        {

            switch (this.Option)
            {
                case enumOption.SELECT:
                    //if (this.Status != "redim")
                    //{
                    if (s.selEle != null)
                    {
                        if (s.selEle is BoxTesto || s.selEle is Group)
                        {
                            this.Cursor = Cursors.WaitCursor;
                            s.selEle.ShowEditor(this.editorFrm);
                            this.Cursor = Cursors.Arrow;
                        }
                        if (s.selEle is ImgBox)
                        {
                            //string f_name = this.imgLoader();
                            ((ImgBox)s.selEle).Load_IMG();
                        }
                        if (s.selEle is Group)
                        {
                            //string f_name = this.imgLoader();
                            ((Group)s.selEle).Load_IMG();
                        }
                        if (s.selEle is PointColorSet)
                        {
                            if (this.redimStatus == enumRedimStatus.POLY)
                            {
                                ((PointColorSet)s.selEle).dbl_Click();
                            }
                        }
                    }
                    this.changeStatus(enumStatus.NONE);
                    //}
                    break;
                default:
                    break;
            }

        }

        #endregion


        public override void ChangeCursor()
        {
            
        }
    }
         
}

