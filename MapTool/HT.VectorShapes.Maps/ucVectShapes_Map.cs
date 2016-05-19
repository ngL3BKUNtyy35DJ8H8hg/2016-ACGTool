using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO; 						//streamer io
using HT.VectorShapes.Shapes;
using HT.VectorShapes.Tools;
using MapXLib;
using Point = System.Drawing.Point;


namespace HT.VectorShapes.Maps
{
    public partial class ucVectShapes_Map : ucVectShapes
    {
        public ucVectShapes_Map()
        {
            InitializeComponent();

            Graphics g;
            //Nếu dùng MapControl
            InitMapControl();
            g = this.ucMap1.Map.CreateGraphics();
            myInit(g);

            //from Ilango.M 
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true); // added line
        }

        #region DRAWING


        /// <summary>
        /// redraws this.s on this control
        /// All=true : redraw all graphic
        /// All=false : redraw only selected objects
        /// </summary>
        public override void redraw(bool All)
        {
            //Nếu có dùng MapControl thì vẽ theo MapControl
            this.ucMap1.Map.Refresh();
        }

        /// <summary>
        /// Nếu có dùng MapControl thì vẽ theo Graphics của map
        /// </summary>
        /// <param name="g"></param>
        /// <param name="All"></param>
        public override void redraw(Graphics g,bool All)
        {
            //Nếu hiện lưới thì sẽ vẽ hình bám vào lưới
            if (fit2grid & this.gridSize > 0)
            {
                this.startX = this.gridSize * (int)(startX / this.gridSize);
                this.startY = this.gridSize * (int)(startY / this.gridSize);
            }

            //HT Code: Cập nhật lại tọa độ chuyển đổi từ kinh, vĩ độ sang tọa độ màn hình
            //Trường hợp:
            //1. Đang chọn một đối tượng (this.Option == "select")
            //2. Và không phải đang nhấn chuột để vẽ (this.MouseSx) và đang thực hiện redim
            if (!this.MouseSx)
            {
                //Duyệt tất cả các ele đang có để cập nhật lại các tọa độ pixel
                for (int i = 0; i < s.List.Count; i++)
                {
                    Ele obj = (Ele)s.List[i];
                    ConvertLonLat2PixelCoord(ref obj);
                }

                //2014-11-17:
                //Cập nhật lại handles cho selEle
                if (this.s.selEle != null)
                {
                    if (this.s.selEle is PointSet)
                    {
                        //POLY MANAGEMENT START
                        //Xác định kích thước rect bao quanh của shape
                        ((PointSet)this.s.selEle).setupSize();
                        //Tạo các handle nodes
                        this.s.sRec = new SelPoly(this.s.selEle); //create handling rect
                    }
                    else if (this.s.selEle is Graph)
                    {
                        //GRAPH MANAGEMENT START
                        ((Graph)this.s.selEle).setupSize();
                        this.s.sRec = new SelGraph(this.s.selEle); //create handling rect
                    }
                    else
                    {
                        //GRAPH MANAGEMENT START
                        this.s.sRec = new SelRect(this.s.selEle); //create handling rect
                    }
                }
            }

            this.GraphicSetUp(g);

            if (this.BackgroundImage != null)
                g.DrawImage(this.BackgroundImage, 0, 0);

            // Render the grid
            if (this.gridSize > 0)
            {
                System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.LightGray);
                int nX = (int)(this.Width / (this.gridSize * Zoom));
                for (int i = 0; i <= nX; i++)
                {
                    g.DrawLine(myPen, i * this.gridSize * Zoom, 0, i * this.gridSize * Zoom, this.Height);
                }
                int nY = (int)(this.Height / (this.gridSize * Zoom));
                for (int i = 0; i <= nY; i++)
                {
                    g.DrawLine(myPen, 0, i * this.gridSize * Zoom, this.Width, i * this.gridSize * Zoom);
                }
                myPen.Dispose();
            }

            // Draws unselected objects
            s.DrawUnselected(g, this.dx, this.dy, this.Zoom);

            // Now I draw the dynamic objects on the buffer
            s.DrawSelected(g, this.dx, this.dy, this.Zoom);


            // Now I draw the graphics effects (creation and selection )
            #region Creation/Selection/PenPoimts plus A4 margin
            //Trạng thái đang vẽ
            //Draw Red creation Rect/Line
            if (Helper.ActionStatus == enumActionStatus.DRAWSHAPE)
            {
                System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red, 1.5f);
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                myPen.StartCap = System.Drawing.Drawing2D.LineCap.DiamondAnchor;
                //myPen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

                //Trường hợp vẽ nhấn giữ chuột trái
                if (this.MouseSx)
                {
                    if (Helper.DrawStatus == enumDrawingOption.LI
                        || Helper.DrawStatus == enumDrawingOption.POLY
                        || Helper.DrawStatus == enumDrawingOption.GRAPH)
                    {
                        g.DrawLine(myPen, (startX + this.dx)*this.Zoom, (startY + this.dy)*this.Zoom,
                                   (tempX + this.dx)*this.Zoom, (tempY + this.dy)*this.Zoom);
                    }
                    else
                    {
                        g.DrawRectangle(myPen, (this.startX + this.dx)*this.Zoom, (this.startY + this.dy)*this.Zoom,
                                        (tempX - this.startX)*this.Zoom, (tempY - this.startY)*this.Zoom);
                    }

                }
                else //Trường hợp vẽ mà không nhấn giữ chuột trái
                {
                    if (Helper.DrawStatus == enumDrawingOption.NEWPOLY) //else if (this.Option == "NEWPOLY")
                    {
                        if (this.tmpPolyPoints.Count > 0)
                        {
                            GraphicsPath myPath = new GraphicsPath();
                            // To ARRAY
                            PointF[] myArr = new PointF[this.tmpPolyPoints.Count + 1];
                            int i = 0;
                            foreach (Point p in this.tmpPolyPoints)
                            {
                                myArr[i++] = new PointF((p.X + this.dx)*this.Zoom, (p.Y + this.dy)*this.Zoom);
                            }
                            //Thêm temp point đang vẽ
                            myArr[i] = new PointF((tempX + this.dx)*this.Zoom, (tempY + this.dy)*this.Zoom);

                            myPath.AddLines(myArr);

                            g.DrawPath(myPen, myPath);

                        }
                    }
                }
                
                myPen.Dispose();
            }

            //Vẽ vùng chọn ký hiệu Draw selection Rect
            if (this.MouseSx && Helper.ActionStatus == enumActionStatus.SELSHAPE)
            {
                //System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Green, 1.5f);
                System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Yellow, 1.5f);
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawRectangle(myPen, (this.startX + this.dx) * this.Zoom, (this.startY + this.dy) * this.Zoom, (tempX - this.startX) * this.Zoom, (tempY - this.startY) * this.Zoom);
                //g.DrawRectangle(myPen, (this.s.sRec.X + this.dx) * this.Zoom, (this.s.sRec.Y + this.dy) * this.Zoom, (this.s.sRec.X1 - this.startX) * this.Zoom, (this.s.sRec.Y1 - this.startY) * this.Zoom);

                myPen.Dispose();
            }

            //Draw msg
            this.drawDebugInfo(g);

            //Draw A4 margin
            if (this.A4)
            {
                System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Blue, 0.5f);
                myPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                g.DrawRectangle(myPen, (1 + this.dx) * this.Zoom, (1 + this.dy) * this.Zoom, 810 * this.Zoom, 1140 * this.Zoom);
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
                    g.DrawCurve(myPen, myArr);
            }

            #endregion
        }


        #endregion

        #region AxMap Functions
        //public enumToolStripType ActiveToolStrip;
        //public ucMap ucMap1;

        //Cho biết có hiển thị các đối tượng vẽ hay ẩn đi
        public bool isShowDrawingRoute;

        public override void Refresh()
        {
            ucMap1.Refresh();
        }

        /// <summary>
        /// Hàm khởi tạo map control
        /// </summary>
        private void InitMapControl()
        {
            //ucMap1 = new ucMap();
            ucMap1.Dock = DockStyle.Fill;
            this.Controls.Add(ucMap1);

            this.ucMap1.Map.KeyDownEvent += new AxMapXLib.CMapXEvents_KeyDownEventHandler(this.axMap1_KeyDownEvent);
            this.ucMap1.Map.DrawUserLayer += new AxMapXLib.CMapXEvents_DrawUserLayerEventHandler(this.axMap1_DrawUserLayer);
            this.ucMap1.Map.MouseWheelEvent += new AxMapXLib.CMapXEvents_MouseWheelEventHandler(this.axMap1_MouseWheelEvent);
            this.ucMap1.Map.MouseDownEvent += new AxMapXLib.CMapXEvents_MouseDownEventHandler(this.ucVectShapes_MouseDown);
            this.ucMap1.Map.MouseMoveEvent += new AxMapXLib.CMapXEvents_MouseMoveEventHandler(this.ucVectShapes_MouseMove);
            this.ucMap1.Map.MouseUpEvent += new AxMapXLib.CMapXEvents_MouseUpEventHandler(this.ucVectShapes_MouseUp);
            this.ucMap1.Map.MapViewChanged += new System.EventHandler(this.axMap1_MapViewChanged);
            this.ucMap1.Map.DblClick += new System.EventHandler(this.ucVectShapes_DoubleClick);

        }

        public event AxMapXLib.CMapXEvents_DrawUserLayerEventHandler VectShapMap_DrawUserLayer;
        public void axMap1_DrawUserLayer(object sender, AxMapXLib.CMapXEvents_DrawUserLayerEvent e)
        {
            Graphics myGraphics = Graphics.FromHdc(new IntPtr(e.hOutputDC));

            //TODO SOMETHING
            this.redraw(myGraphics, true);
            if (VectShapMap_DrawUserLayer != null)
                VectShapMap_DrawUserLayer(sender, e);

            myGraphics.Dispose();
        }

        public event AxMapXLib.CMapXEvents_MouseWheelEventHandler VectShapMap_MouseWheelEvent;
        private void axMap1_MouseWheelEvent(object sender, AxMapXLib.CMapXEvents_MouseWheelEvent e)
        {
            ucMap1.Refresh();
        }

        public event EventHandler VectShapMap_MapViewChanged;
        public void axMap1_MapViewChanged(object sender, EventArgs e)
        {
            //ucMap1.Refresh();
        }

        public event AxMapXLib.CMapXEvents_KeyDownEventHandler VectShapMap_KeyDown;
        public void axMap1_KeyDownEvent(object  sender, AxMapXLib.CMapXEvents_KeyDownEvent e)
        {
            //Delete 
            if (e.keyCode == 46)
            {
                if (MessageBox.Show("Bạn thật sự muốn xóa ký hiệu này?", "Thông báo", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                    s.rmSelected();
            }
                
            //if (VectShapMap_KeyDown != null)
            //    VectShapMap_KeyDown(sender, e);
        }

        #region USER CONTROL MOUSE EVENT MANAGMENT

        public event AxMapXLib.CMapXEvents_MouseDownEventHandler VectShapMap_MouseDownEvent;
        private void ucVectShapes_MouseDown(object sender, AxMapXLib.CMapXEvents_MouseDownEvent e)
        {
            if (e.button == 1)
                MouseDownEvent(sender, (int)e.x, (int)e.y, MouseButtons.Left);
            else if (e.button == 2)
            {
                MouseDownEvent(sender, (int)e.x, (int)e.y, MouseButtons.Right);
                this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miCrossCursor;
            }
            if (VectShapMap_MouseDownEvent != null)
                VectShapMap_MouseDownEvent(sender, e);
        }

        public event AxMapXLib.CMapXEvents_MouseMoveEventHandler VectShapMap_MouseMoveEvent;
        private void ucVectShapes_MouseMove(object sender, AxMapXLib.CMapXEvents_MouseMoveEvent e)
        {
            if (e.button == 1)
                MouseMoveEvent(sender, (int)e.x, (int)e.y, MouseButtons.Left);
            else
            {
                if (e.button == 2)
                    MouseMoveEvent(sender, (int) e.x, (int) e.y, MouseButtons.Right);
                else
                #region NONE mouse button pressed
                {
                    //Nếu đang vẽ đối tượng
                    //if (this.MouseSx)
                    //{
                        tempX = (int)(e.x / Zoom);
                        tempY = (int)(e.y / Zoom);
                        if (fit2grid & this.gridSize > 0)
                        {
                            tempX = this.gridSize * (int)((e.x / Zoom) / this.gridSize);
                            tempY = this.gridSize * (int)((e.y / Zoom) / this.gridSize);
                        }
                        tempX = tempX - this.dx;
                        tempY = tempY - this.dy;

                    //}


                    //if (this.Option == "select")
                    if (Helper.ActionStatus == enumActionStatus.REDIMSHAPE)
                    {
                        if (this.s.sRec != null)
                        {
                            //enumRedimOption st = this.s.sRec.isOver((int)(e.x / Zoom) - this.dx, (int)(e.y / Zoom - this.dy));
                            ////Enum.GetName(typeof(enumRedimOption), enumRedimOption.FirstName);
                            ////Chuyển đổi string sang enum
                            //Helper.RedimOption = (enumRedimOption)Enum.Parse(typeof(enumRedimOption), st);

                            Helper.RedimOption = this.s.sRec.isOver((int)(e.x / Zoom) - this.dx, (int)(e.y / Zoom - this.dy));
                            switch (Helper.RedimOption)
                            {
                                case enumRedimOption.NEWP: //Thêm mới node trong Polygon object
                                    //this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSelectPlusCursor;
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miCustomCursor;
                                    this.ucMap1.Map.MouseIcon = Path.GetDirectoryName(Application.ExecutablePath) + "\\cross_r.cur";
                                    break;
                                case enumRedimOption.POLY: //Chọn node trong polygon object
                                    //this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSelectPlusCursor;
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miCustomCursor;
                                    this.ucMap1.Map.MouseIcon = Path.GetDirectoryName(Application.ExecutablePath) + "\\aero_link.cur";
                                    break;
                                case enumRedimOption.GRAPH: //Chọn Graph
                                    //this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeAllCursor;
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miCustomCursor;
                                    this.ucMap1.Map.MouseIcon = Path.GetDirectoryName(Application.ExecutablePath) + "\\aero_link.cur";
                                    break;
                                case enumRedimOption.ROT: //Chọn rotate objet
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miCustomCursor;
                                    this.ucMap1.Map.MouseIcon = Path.GetDirectoryName(Application.ExecutablePath) + "\\aero_link_l.cur";
                                    break;
                                case enumRedimOption.C: //Move object
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeAllCursor;
                                    break;
                                case enumRedimOption.NW: //resize object theo hướng Tây Bắc
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNWSECursor;
                                    break;
                                case enumRedimOption.N: //resize object theo hướng Bắc
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNSCursor;
                                    break;
                                case enumRedimOption.NE: //resize object theo hướng Đông Bắc
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNESWCursor;
                                    break;
                                case enumRedimOption.E: //resize object theo hướng Đông
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeEWCursor;
                                    break;
                                case enumRedimOption.SE: //resize object theo hướng Đông Nam
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNWSECursor;
                                    break;
                                case enumRedimOption.S: //resize object theo hướng Nam
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNSCursor;
                                    break;
                                case enumRedimOption.SW: //resize object theo hướng Tây Nam
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNESWCursor;
                                    break;
                                case enumRedimOption.W: //resize object theo hướng tây
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeEWCursor;
                                    break;
                                case enumRedimOption.ZOOM:
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNWSECursor;
                                    break;
                                default:
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miDefaultCursor;
                                    Helper.RedimOption = enumRedimOption.NONE;
                                    break;
                            }
                        }
                        else
                        {
                            this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miDefaultCursor;
                            Helper.RedimOption = enumRedimOption.NONE;
                        }
                    }
                    else if (Helper.ActionStatus == enumActionStatus.DRAWSHAPE) 
                    {
                        if (Helper.DrawStatus == enumDrawingOption.NEWPOLY)
                            redraw(false);
                    }
                    else
                    {
                        this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miDefaultCursor;
                        Helper.RedimOption = enumRedimOption.NONE;
                    }
                #endregion
                }
            }


            if (VectShapMap_MouseMoveEvent != null)
                VectShapMap_MouseMoveEvent(sender, e);
        }

        public event AxMapXLib.CMapXEvents_MouseUpEventHandler VectShapMap_MouseUpEvent;
        private void ucVectShapes_MouseUp(object sender, AxMapXLib.CMapXEvents_MouseUpEvent e)
        {
            if (e.button == 1)
            {
                MouseUpEvent(sender, (int)e.x, (int)e.y, MouseButtons.Left);
            }
            else if (e.button == 2)
            {
                MouseUpEvent(sender, (int)e.x, (int)e.y, MouseButtons.Right);
            }
            if (VectShapMap_MouseUpEvent != null)
                VectShapMap_MouseUpEvent(sender, e);
        }

        public event EventHandler VectShapMap_DblClick;
        private void ucVectShapes_DoubleClick(object sender, EventArgs e)
        {
            switch (Helper.DrawStatus)
            {
                case enumDrawingOption.NONE:
                    //if (Helper.ActionStatus != "redim")
                    //{
                    if (s.selEle != null)
                    {
                        if (s.selEle is BoxTesto || s.selEle is Group)
                        {
                            //this.ucMap1.Map.Cursor = Cursors.WaitCursor;
                            this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miHourglassCursor;
                            s.selEle.ShowEditor(this.editorFrm);
                            this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miDefaultCursor;
                            //this.ucMap1.Map.Cursor = Cursors.Arrow;
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
                            if (Helper.RedimOption == enumRedimOption.POLY)
                            {
                                ((PointColorSet)s.selEle).dbl_Click();
                            }
                        }
                    }
                    this.changeStatus(enumActionStatus.NONE);
                    //}
                    break;
                default:
                    break;
            }

            if (VectShapMap_DblClick != null)
                VectShapMap_DblClick(sender, e);
        }

        #endregion


        #endregion

        public override void ChangeCursor()
        {
            this.ucMap1.Map.CurrentTool = ToolConstants.miArrowTool;
            //this.ucMap1.ActiveToolStrip = enumToolStripType.VectorShapeToolStrip;
        }

        /// <summary>
        /// 2014-11-30
        /// Chuyen doi toa do pixel cua ele sang toa do lon lat
        /// </summary>
        protected override void ConvertPixel2LonLatCoord(ref Ele ele)
        {
            if (ele != null)
            {
                if (ele is PointSet)
                {
                    //============================================
                    //HT Code: Chuyển tọa độ pixel sang tọa độ kinh vĩ độ
                    //và lưu vào các biến this.s.selEle.LeftLon, this.s.selEle.TopLat, this.s.selEle.RightLon, this.s.selEle.BottomLat
                    //và biến ((PointSet)this.s.selEle).mapPoints
                    float x, y, x1, y1;
                    x = ele.X;
                    y = ele.Y;
                    x1 = ele.X1;
                    y1 = ele.Y1;

                    ucMap1.Map.ConvertCoord(ref x, ref y, ref ele.LeftLon, ref ele.TopLat,
                                            ConversionConstants.miScreenToMap);
                    ucMap1.Map.ConvertCoord(ref x1, ref y1, ref ele.RightLon,
                                            ref ele.BottomLat, ConversionConstants.miScreenToMap);
                    //if (this.s.selEle is PointSet)
                    //{
                        //Tính giá trị min trong poly 
                        //(không cần tính max vì max được tính ở hàm setupSize() của class PointSet)
                        ArrayList mPoints = new ArrayList();
                        foreach (PointWr p in ((PointSet)ele).Points)
                        {
                            float tempx = (float)(ele.X + p.X);
                            float tempy = (float)(ele.Y + p.Y);
                            double tempLon, tempLat;
                            tempLon = tempLat = 0;
                            ucMap1.Map.ConvertCoord(ref tempx, ref tempy, ref tempLon, ref tempLat, ConversionConstants.miScreenToMap);
                            PointF newp = new PointF((float)tempLon, (float)tempLat);
                            mPoints.Add(newp);
                        }
                        ((PointSet)ele).mapPoints = mPoints;
                    //}
                    //============================================
                }
                else if (ele is Graph)
                {

                }
                else if (ele is Group)
                {
                    for (int i = 0; i < ((Group)ele).Objs.Length; i++)
                    {
                        Ele e = ((Group)ele).Objs[i];
                        ConvertPixel2LonLatCoord(ref e);
                        ((Group)ele).Objs[i] = e;

                    }
                }
                else
                {
                    //Cập nhật lại tọa độ lon, lat
                    float x, y, x1, y1;
                    x = ele.X;
                    y = ele.Y;
                    x1 = ele.X1;
                    y1 = ele.Y1;

                    ucMap1.Map.ConvertCoord(ref x, ref y, ref ele.LeftLon, ref ele.TopLat,
                                            ConversionConstants.miScreenToMap);
                    ucMap1.Map.ConvertCoord(ref x1, ref y1, ref ele.RightLon,
                                            ref ele.BottomLat, ConversionConstants.miScreenToMap);

                    //ele.UpdateLonLatCoord(lon, lat, lon1, lat1);
                }
            }
        }

        /// <summary>
        /// 2014-11-30
        /// Convert tọa độ lon, lat sang tọa độ pixel để vẽ lại
        /// </summary>
        /// <param name="ele"></param>
        protected void ConvertLonLat2PixelCoord(ref Ele ele)
        {
            //=========================================
            float x, y, x1, y1;
            x = y = x1 = y1 = 0;
            double lon, lat, lon1, lat1;
            lon = ele.LeftLon;
            lat = ele.TopLat;
            lon1 = ele.RightLon;
            lat1 = ele.BottomLat;
            ucMap1.Map.ConvertCoord(ref x, ref y, ref lon, ref lat, ConversionConstants.miMapToScreen);
            ucMap1.Map.ConvertCoord(ref x1, ref y1, ref lon1, ref lat1, ConversionConstants.miMapToScreen);
            ele.UpdateRect((int)x, (int)y, (int)x1, (int)y1);
            
            //=========================================
            //Nếu là PointSet thì cập nhật thêm các Node: Convert tọa độ lon, lat sang tọa độ pixel
            if (ele is PointSet)
            {
                ArrayList aa = new ArrayList();
                int minX, minY; //, maxX, maxY;
                minX = minY = int.MaxValue;

                //Tính giá trị min trong poly 
                //(không cần tính max vì max được tính ở hàm setupSize() của class PointSet)
                if (ele.mapPoints != null)
                {
                    List<Point> pointList = new List<Point>();
                    foreach (PointF p in ((PointSet)ele).mapPoints)
                    {
                        x = y = 0;
                        lon = p.X;
                        lat = p.Y;
                        ucMap1.Map.ConvertCoord(ref x, ref y, ref lon, ref lat,
                                                ConversionConstants.miMapToScreen);
                        Point newp = new Point((int)x, (int)y);
                        pointList.Add(newp);
                        minX = Math.Min(minX, newp.X);
                        minY = Math.Min(minY, newp.Y);
                    }

                    foreach (Point p in pointList)
                    {
                        aa.Add(new PointWr(p.X - minX, p.Y - minY));
                    }
                    ((PointSet)ele).UpdatePoints(aa);
                }
            }
            else if (ele is Group)
            {
                //2014-11-27: Xét xử lý trường hợp group
                for (int i = 0; i < ((Group)ele).Objs.Length; i++)
                {
                    ConvertLonLat2PixelCoord(ref ((Group)ele).Objs[i]);
                }
            }
        }

        ///// <summary>
        ///// HT Code: Cập nhật lại các tọa độ lon, lat của các point thuộc một poly
        ///// Bao gồm: 
        ///// + Tọa độ min, max
        ///// + Các point bên trong
        ///// </summary>
        ///// <param name="aa"></param>
        ///// <param name="minX"></param>
        ///// <param name="minY"></param>
        ///// <param name="maxX"></param>
        ///// <param name="maxY"></param>
        //protected override void AddPoly(ArrayList aa, int minX, int minY, int maxX, int maxY)
        //{
        //    double lon, lat, lon1, lat1;
        //    lon = lat = lon1 = lat1 = 0;
        //    ConvertScreenToMapCoord(minX, minY, maxX, maxY, ref lon, ref lat, ref lon1, ref lat1);

        //    ArrayList mPoints = new ArrayList();
        //    foreach (Point p in this.tmpPolyPoints)
        //    {
        //        float tempX = p.X;
        //        float tempY = p.Y;
        //        double tempLon, tempLat;
        //        tempLon = tempLat = 0;
        //        ucMap1.Map.ConvertCoord(ref tempX, ref tempY, ref tempLon, ref tempLat,
        //                                ConversionConstants.miScreenToMap);
        //        mPoints.Add(new PointF((float)tempLon, (float)tempLat));
        //    }

        //    //this.s.addPoly(minX, minY, maxX, maxY, this.CreationPenColor, CreationFillColor,
        //    //                           CreationPenWidth, CreationFilled, aa,  false);
        //    this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1, mPoints);
        //}

        ///// <summary>
        ///// Cập nhật lại tọa độ lon,lat của các point trong poly sau khi redim
        ///// </summary>
        //protected override void UpdateSelectedPoly()
        //{
        //    //============================================
        //    //HT Code: Chuyển tọa độ pixel sang tọa độ kinh vĩ độ
        //    //và lưu vào các biến this.s.selEle.LeftLon, this.s.selEle.TopLat, this.s.selEle.RightLon, this.s.selEle.BottomLat
        //    //và biến ((PointSet)this.s.selEle).mapPoints
        //    float x, y, x1, y1;
        //    x = this.s.selEle.X;
        //    y = this.s.selEle.Y;
        //    x1 = this.s.selEle.X1;
        //    y1 = this.s.selEle.Y1;

        //    ucMap1.Map.ConvertCoord(ref x, ref y, ref this.s.selEle.LeftLon, ref this.s.selEle.TopLat,
        //                            ConversionConstants.miScreenToMap);
        //    ucMap1.Map.ConvertCoord(ref x1, ref y1, ref this.s.selEle.RightLon,
        //                            ref this.s.selEle.BottomLat, ConversionConstants.miScreenToMap);
        //    if (this.s.selEle is PointSet)
        //    {
        //        //Tính giá trị min trong poly 
        //        //(không cần tính max vì max được tính ở hàm setupSize() của class PointSet)
        //        ArrayList mPoints = new ArrayList();
        //        foreach (PointWr p in ((PointSet)this.s.selEle).Points)
        //        {
        //            float tempx = (float)(this.s.selEle.X + p.X);
        //            float tempy = (float)(this.s.selEle.Y + p.Y);
        //            double tempLon, tempLat;
        //            tempLon = tempLat = 0;
        //            ucMap1.Map.ConvertCoord(ref tempx, ref tempy, ref tempLon, ref tempLat, ConversionConstants.miScreenToMap);
        //            PointF newp = new PointF((float)tempLon, (float)tempLat);
        //            mPoints.Add(newp);
        //        }
        //        ((PointSet)this.s.selEle).mapPoints = mPoints;
        //    }
        //    //============================================
        //}


        ///// <summary>
        ///// Chuyển đổi tọa độ min (topleft), max (rightbottom) theo pixel thành tọa độ lon, lat
        ///// </summary>
        ///// <param name="minX"></param>
        ///// <param name="minY"></param>
        ///// <param name="maxX"></param>
        ///// <param name="maxY"></param>
        ///// <param name="lon"></param>
        ///// <param name="lat"></param>
        ///// <param name="lon1"></param>
        ///// <param name="lat1"></param>
        //protected override void ConvertScreenToMapCoord(int minX, int minY, int maxX, int maxY, ref double lon, ref double lat, ref double lon1, ref double lat1)
        //{
        //    float x, y, x1, y1;
        //    x = minX;
        //    y = minY;
        //    x1 = maxX;
        //    y1 = maxY;
        //    ucMap1.Map.ConvertCoord(ref x, ref y, ref lon, ref lat, ConversionConstants.miScreenToMap);
        //    ucMap1.Map.ConvertCoord(ref x1, ref y1, ref lon1, ref lat1,
        //                            ConversionConstants.miScreenToMap);
        //}

        //protected void ConvertMapToScreenCoord(ref int left, ref int top, ref int right, ref int bottom, double leftlon, double toplat, double rightlon, double bottomlat)
        //{
        //    float x, y, x1, y1;
            
        //    x = left;
        //    y = top;
        //    x1 = right;
        //    y1 = bottom;
            
        //    ucMap1.Map.ConvertCoord(ref x, ref y, ref leftlon, ref toplat, ConversionConstants.miMapToScreen);
        //    ucMap1.Map.ConvertCoord(ref x1, ref y1, ref rightlon, ref bottomlat, ConversionConstants.miMapToScreen);

        //    left = (int)x;
        //    top = (int)y;
        //    right = (int)x1;
        //    bottom = (int)y1;
        //}
        
    }
}

