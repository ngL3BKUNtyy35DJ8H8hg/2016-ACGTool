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
        private void RedrawMapControl(Graphics g, bool All)
        {
            //if (s.selEle != null)
            //{
            //    //=========================================
            //    //Convert tọa độ lon, lat sang tọa độ pixel
            //    float x, y, x1, y1;
            //    x = y = x1 = y1 = 0;
            //    double lon, lat, lon1, lat1;
            //    lon = s.selEle.LeftLon;
            //    lat = s.selEle.TopLat;
            //    lon1 = s.selEle.RightLon;
            //    lat1 = s.selEle.BottomLat;
            //    ucMap1.Map.ConvertCoord(ref x, ref y, ref lon, ref lat, ConversionConstants.miMapToScreen);
            //    ucMap1.Map.ConvertCoord(ref x1, ref y1, ref lon1, ref lat1, ConversionConstants.miMapToScreen);
            //    this.startX = (int)x;
            //    this.startY = (int)y;
            //    this.tempX = (int)x1;
            //    this.tempY = (int)y1;
            //    s.selEle.UpdateRect((int)x, (int)y, (int)x1, (int)y1);
            //    s.sRec.UpdateRect((int)x, (int)y, (int)x1, (int)y1);
            //}

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
                //Duyệt tất cả các ele đang có
                foreach (Ele obj in s.List)
                {
                    //=========================================
                    //Convert tọa độ lon, lat sang tọa độ pixel
                    float x, y, x1, y1;
                    x = y = x1 = y1 = 0;
                    double lon, lat, lon1, lat1;
                    lon = obj.LeftLon;
                    lat = obj.TopLat;
                    lon1 = obj.RightLon;
                    lat1 = obj.BottomLat;
                    ucMap1.Map.ConvertCoord(ref x, ref y, ref lon, ref lat, ConversionConstants.miMapToScreen);
                    ucMap1.Map.ConvertCoord(ref x1, ref y1, ref lon1, ref lat1, ConversionConstants.miMapToScreen);

                    obj.UpdateRect((int)x, (int)y, (int)x1, (int)y1);

                    //=========================================
                    //Nếu là PointSet thì cập nhật thêm các Node: Convert tọa độ lon, lat sang tọa độ pixel
                    if (obj is PointSet)
                    {
                        ArrayList aa = new ArrayList();
                        int minX, minY; //, maxX, maxY;
                        minX = minY = int.MaxValue;

                        //Tính giá trị min trong poly 
                        //(không cần tính max vì max được tính ở hàm setupSize() của class PointSet)
                        List<Point> pointList = new List<Point>();
                        foreach (PointF p in ((PointSet)obj).mapPoints)
                        {
                            x = y = 0;
                            lon = p.X;
                            lat = p.Y;
                            ucMap1.Map.ConvertCoord(ref x, ref y, ref lon, ref lat, ConversionConstants.miMapToScreen);
                            Point newp = new Point((int)x, (int)y);
                            pointList.Add(newp);
                            minX = Math.Min(minX, newp.X);
                            minY = Math.Min(minY, newp.Y);
                        }

                        foreach (Point p in pointList)
                        {
                            aa.Add(new PointWr(p.X - minX, p.Y - minY));
                        }
                        ((PointSet)obj).UpdatePoints(aa);
                    }
                }

                //Tạo các handle nodes cho ele đang chọn
                if (this.s.selEle != null)
                {
                    ////Xác định kích thước rect bao quanh của shape
                    //if (this.s.selEle is PointSet)
                    //    ((PointSet)this.s.selEle).setupSize();
                    //if (this.Status == enumStatus.SELRECT)
                    try
                    {
                        this.s.sRec = new SelPoly(this.s.selEle); //create handling rect
                    }
                    catch (Exception)
                    {


                    }

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
            //if (isShowDrawingRoute)
            //{
            s.DrawUnselected(g, this.dx, this.dy, this.Zoom);
            //}


            // Now I draw the dynamic objects on the buffer
            //if (isShowDrawingRoute)
            //{
                s.DrawSelected(g, this.dx, this.dy, this.Zoom);
            //}


            // Now I draw the graphics effects (creation and selection )
            #region Creation/Selection/PenPoimts plus A4 margin
            //Trạng thái đang vẽ
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
                    g.DrawLine(myPen, (startX + this.dx) * this.Zoom, (startY + this.dy) * this.Zoom, (tempX + this.dx) * this.Zoom, (tempY + this.dy) * this.Zoom);
                }
                else if (this.Option == enumOption.NEWPOLY) //else if (this.Option == "NEWPOLY")
                {
                    if (this.polyPoints.Count > 0)
                    {
                        GraphicsPath myPath =  new GraphicsPath();
                        // To ARRAY
                        PointF[] myArr = new PointF[this.polyPoints.Count + 1];
                        int i = 0;
                        foreach (Point p in this.polyPoints)
                        {
                            myArr[i++] = new PointF((p.X + this.dx)*this.Zoom, (p.Y + this.dy)*this.Zoom);
                        }
                        //Thêm temp point đang vẽ
                        myArr[i] = new PointF((tempX + this.dx) * this.Zoom, (tempY + this.dy) * this.Zoom);

                        // myPath.AddPolygon(myArr);
                        myPath.AddLines(myArr);
                        //g.DrawLines(myPen, myArr);

                        g.DrawPath(myPen, myPath);

                    }
                }
                else
                {
                    g.DrawRectangle(myPen, (this.startX + this.dx) * this.Zoom, (this.startY + this.dy) * this.Zoom, (tempX - this.startX) * this.Zoom, (tempY - this.startY) * this.Zoom);
                }
                myPen.Dispose();
            }

            //Vẽ vùng chọn ký hiệu Draw selection Rect
            if (this.MouseSx & this.Status == enumStatus.SELRECT)
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

            // I draw the buffer on the graphic of my control
            //g.DrawImageUnscaled(offScreenBmp, 0, 0);

            //g.Dispose();
        }


        public override void redraw(Graphics g,bool All)
        {
            //Nếu có dùng MapControl thì vẽ theo MapControl
            RedrawMapControl(g, All);
        }


        #endregion

        #region AxMap Functions
        //public enumToolStripType ActiveToolStrip;
        public ucMap ucMap1;

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
            ucMap1 = new ucMap();
            ucMap1.Dock = DockStyle.Fill;
            this.Controls.Add(ucMap1);

            this.ucMap1.Map.KeyDownEvent += new AxMapXLib.CMapXEvents_KeyDownEventHandler(this.axMap1_KeyDownEvent);
            this.ucMap1.Map.DrawUserLayer += new AxMapXLib.CMapXEvents_DrawUserLayerEventHandler(this.axMap1_DrawUserLayer);
            this.ucMap1.Map.MouseWheelEvent += new AxMapXLib.CMapXEvents_MouseWheelEventHandler(this.axMap1_MouseWheelEvent);
            this.ucMap1.Map.MouseDownEvent += new AxMapXLib.CMapXEvents_MouseDownEventHandler(this.axMap1_MouseDownEvent);
            this.ucMap1.Map.MouseMoveEvent += new AxMapXLib.CMapXEvents_MouseMoveEventHandler(this.axMap1_MouseMoveEvent);
            this.ucMap1.Map.MouseUpEvent += new AxMapXLib.CMapXEvents_MouseUpEventHandler(this.axMap1_MouseUpEvent);
            this.ucMap1.Map.MapViewChanged += new System.EventHandler(this.axMap1_MapViewChanged);
            this.ucMap1.Map.DblClick += new System.EventHandler(this.axMap1_DblClick);

        }

        public event AxMapXLib.CMapXEvents_MouseDownEventHandler VectShapMap_MouseDownEvent;
        public void axMap1_MouseDownEvent(object sender, AxMapXLib.CMapXEvents_MouseDownEvent e)
        {
            if (ucMap1.ActiveToolStrip == enumToolStripType.VectorShapeToolStrip)
            {
                ucVectShapes_MouseDown(sender, e);
            }

            if (VectShapMap_MouseDownEvent != null)
                VectShapMap_MouseDownEvent(sender, e);


        }

        public event AxMapXLib.CMapXEvents_MouseMoveEventHandler VectShapMap_MouseMoveEvent;
        public void axMap1_MouseMoveEvent(object sender, AxMapXLib.CMapXEvents_MouseMoveEvent e)
        {
            if (ucMap1.ActiveToolStrip == enumToolStripType.VectorShapeToolStrip)
            {
                ucVectShapes_MouseMove(sender, e);
            }

            if (VectShapMap_MouseMoveEvent != null)
                VectShapMap_MouseMoveEvent(sender, e);
        }

        public event AxMapXLib.CMapXEvents_MouseUpEventHandler VectShapMap_MouseUpEvent;
        public void axMap1_MouseUpEvent(object sender, AxMapXLib.CMapXEvents_MouseUpEvent e)
        {
            if (ucMap1.ActiveToolStrip == enumToolStripType.VectorShapeToolStrip)
            {
                ucVectShapes_MouseUp(sender, e);
            }

            if (VectShapMap_MouseUpEvent != null)
                VectShapMap_MouseUpEvent(sender, e);
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

        public event EventHandler VectShapMap_DblClick;
        public void axMap1_DblClick(object sender, EventArgs e)
        {
            if (ucMap1.ActiveToolStrip == enumToolStripType.VectorShapeToolStrip)
            {
                ucVectShapes_DoubleClick(sender, e);
            }

            if (VectShapMap_DblClick != null)
                VectShapMap_DblClick(sender, e);
        }

        public event AxMapXLib.CMapXEvents_KeyDownEventHandler VectShapMap_KeyDown;
        public void axMap1_KeyDownEvent(object  sender, AxMapXLib.CMapXEvents_KeyDownEvent e)
        {
            //Delete 
            if (e.keyCode == 46)
            {
                if (MessageBox.Show("Bạn thật sự muốn xóa đường bay này?", "Thông báo", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                    s.rmSelected();
            }
                
            //if (VectShapMap_KeyDown != null)
            //    VectShapMap_KeyDown(sender, e);
        }

        #region USER CONTROL MOUSE EVENT MANAGMENT

        private void ucVectShapes_MouseDown(object sender, AxMapXLib.CMapXEvents_MouseDownEvent e)
        {
            if (e.button == 1)
                MouseDownEvent(sender, (int)e.x, (int)e.y, MouseButtons.Left);
            else if (e.button == 2)
            {
                MouseDownEvent(sender, (int)e.x, (int)e.y, MouseButtons.Right);
                this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miCrossCursor;
            }
        }

        private void ucVectShapes_MouseMove(object sender, AxMapXLib.CMapXEvents_MouseMoveEvent e)
        {
            if (e.button == 1)
                MouseMoveEvent(sender, (int)e.x, (int)e.y, MouseButtons.Left);
            else
            {
                if (e.button == 2)
                    MouseMoveEvent(sender, (int) e.x, (int) e.y, MouseButtons.Right);
                else
                #region NO muse button pressed
                {
                    //Nếu đang vẽ đối tượng
                    if (this.MouseSx)
                    {
                        tempX = (int)(e.x / Zoom);
                        tempY = (int)(e.y / Zoom);
                        if (fit2grid & this.gridSize > 0)
                        {
                            tempX = this.gridSize * (int)((e.x / Zoom) / this.gridSize);
                            tempY = this.gridSize * (int)((e.y / Zoom) / this.gridSize);
                        }
                        tempX = tempX - this.dx;
                        tempY = tempY - this.dy;

                    }


                    //if (this.Option == "select")
                    if (this.Option == enumOption.SELECT)
                    {
                        if (this.s.sRec != null)
                        {
                            string st = this.s.sRec.isOver((int)(e.x / Zoom) - this.dx, (int)(e.y / Zoom - this.dy));
                            //Enum.GetName(typeof(enumRedimStatus), enumRedimStatus.FirstName);
                            //Chuyển đổi string sang enum
                            this.redimStatus = (enumRedimStatus)Enum.Parse(typeof(enumRedimStatus), st);
                            switch (this.redimStatus)
                            {
                                case enumRedimStatus.NEWP: //Thêm mới node trong Polygon object
                                    //this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSelectPlusCursor;
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miCustomCursor;
                                    this.ucMap1.Map.MouseIcon = Path.GetDirectoryName(Application.ExecutablePath) + "\\cross_r.cur";
                                    //this.ucMap1.Map.Cursor = AddPointCur;
                                    /*To change the cursor
                                    Cursor cc = new Cursor("NewPoint.ico");
                                    this.ucMap1.Map.Cursor = cc;
                                    */
                                    break;
                                case enumRedimStatus.POLY: //Chọn node trong polygon object
                                    //this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSelectPlusCursor;
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miCustomCursor;
                                    this.ucMap1.Map.MouseIcon = Path.GetDirectoryName(Application.ExecutablePath) + "\\aero_link.cur";
                                    break;
                                //case "NEWPOLY": //Chọn node trong polygon object
                                //    this.redimStatus = enumRedimStatus.NEWPOLY;
                                //    //this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSelectPlusCursor;
                                //    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miCustomCursor;
                                //    this.ucMap1.Map.MouseIcon = Path.GetDirectoryName(Application.ExecutablePath) + "\\aero_link.cur";
                                //    break;
                                case enumRedimStatus.GRAPH: //Chọn Graph
                                    //this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeAllCursor;
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miCustomCursor;
                                    this.ucMap1.Map.MouseIcon = Path.GetDirectoryName(Application.ExecutablePath) + "\\aero_link.cur";
                                    break;
                                case enumRedimStatus.ROT: //Chọn rotate objet
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miCustomCursor;
                                    this.ucMap1.Map.MouseIcon = Path.GetDirectoryName(Application.ExecutablePath) + "\\aero_link_l.cur";
                                    //this.Cursor = new Cursor(new System.IO.MemoryStream(Properties.Resources.rotate));
                                    //this.ucMap1.Map.CurrentTool = (MapXLib.ToolConstants)enumVectShapeCursor.SizeAll;
                                    break;
                                case enumRedimStatus.C: //Move object
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeAllCursor;
                                    //this.ucMap1.Map.CurrentTool = MapXLib.ToolConstants.miSelectTool;
                                    break;
                                case enumRedimStatus.NW: //resize object theo hướng Tây Bắc
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNWSECursor;
                                    break;
                                case enumRedimStatus.N: //resize object theo hướng Bắc
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNSCursor;
                                    break;
                                case enumRedimStatus.NE: //resize object theo hướng Đông Bắc
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNESWCursor;
                                    break;
                                case enumRedimStatus.E: //resize object theo hướng Đông
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeEWCursor;
                                    break;
                                case enumRedimStatus.SE: //resize object theo hướng Đông Nam
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNWSECursor;
                                    break;
                                case enumRedimStatus.S: //resize object theo hướng Nam
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNSCursor;
                                    break;
                                case enumRedimStatus.SW: //resize object theo hướng Tây Nam
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNESWCursor;
                                    break;
                                case enumRedimStatus.W: //resize object theo hướng tây
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeEWCursor;
                                    break;
                                case enumRedimStatus.ZOOM:
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miSizeNWSECursor;
                                    break;
                                default:
                                    this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miDefaultCursor;
                                    this.redimStatus = enumRedimStatus.NO;
                                    break;
                            }
                        }
                        else
                        {
                            this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miDefaultCursor;
                            this.redimStatus = enumRedimStatus.NO;
                        }
                    }
                    else if (this.Option == enumOption.NEWPOLY) //else if (this.Option == "NEWPOLY")
                    {
                        redraw(false);
                    }
                    else
                    {
                        this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miDefaultCursor;
                        this.redimStatus = enumRedimStatus.NO;
                    }
                #endregion
                }
            }
            
            
            //redraw();
        }

        private void ucVectShapes_MouseUp(object sender, AxMapXLib.CMapXEvents_MouseUpEvent e)
        {
            //int tmpX = (int)((e.x) / Zoom - this.dx);
            //int tmpY = (int)((e.y) / Zoom - this.dy);
            //if (fit2grid & this.gridSize > 0)
            //{
            //    tmpX = this.gridSize * (int)((e.x / Zoom - this.dx) / this.gridSize);
            //    tmpY = this.gridSize * (int)((e.y / Zoom - this.dy) / this.gridSize);
            //}

            if (e.button == 1)
            {
                MouseUpEvent(sender, (int)e.x, (int)e.y, MouseButtons.Left);
                // store start X,Y,X1,Y1 of selected item
                //if (this.Status == enumStatus.DRAWRECT)
                //{
                //double lon, lat, lon1, lat1;
                //lon = lat = lon1 = lat1 = 0;
                //ConvertScreenToMapCoord(startX, startY, tmpX, tmpY, ref lon, ref lat, ref lon1, ref lat1);
                //}
            }
            else if (e.button == 2)
            {
                MouseUpEvent(sender, (int)e.x, (int)e.y, MouseButtons.Right);
            }

            
        }

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


        #endregion

        public override void ChangeCursor()
        {
            this.ucMap1.Map.CurrentTool = ToolConstants.miArrowTool;
            this.ucMap1.ActiveToolStrip = enumToolStripType.VectorShapeToolStrip;
            
        }

        /// <summary>
        /// HT Code: Cập nhật lại các tọa độ lon, lat của các point thuộc một poly
        /// Bao gồm: 
        /// + Tọa độ min, max
        /// + Các point bên trong
        /// </summary>
        /// <param name="aa"></param>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        protected override void AddPoly(ArrayList aa, int minX, int minY, int maxX, int maxY)
        {
            double lon, lat, lon1, lat1;
            lon = lat = lon1 = lat1 = 0;
            //Chuyển đổi tọa độ màn hình sang tọa độ lon, lat vùng chọn
            ConvertScreenToMapCoord(minX, minY, maxX, maxY, ref lon, ref lat, ref lon1, ref lat1);

            ArrayList mPoints = new ArrayList();
            foreach (Point p in this.polyPoints)
            {
                float tempX = p.X;
                float tempY = p.Y;
                double tempLon, tempLat;
                tempLon = tempLat = 0;
                ucMap1.Map.ConvertCoord(ref tempX, ref tempY, ref tempLon, ref tempLat,
                                        ConversionConstants.miScreenToMap);
                mPoints.Add(new PointF((float)tempLon, (float)tempLat));
            }

            this.s.addPoly(minX, minY, maxX, maxY, this.CreationPenColor, CreationFillColor,
                                       CreationPenWidth, CreationFilled, aa,  false);
            this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1, mPoints);
        }

        /// <summary>
        /// Chuyển đổi tọa độ min (topleft), max (rightbottom) theo pixel thành tọa độ lon, lat
        /// </summary>
        /// <param name="minX"></param>
        /// <param name="minY"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <param name="lon"></param>
        /// <param name="lat"></param>
        /// <param name="lon1"></param>
        /// <param name="lat1"></param>
        protected override void ConvertScreenToMapCoord(int minX, int minY, int maxX, int maxY, ref double lon, ref double lat, ref double lon1, ref double lat1)
        {
            float x, y, x1, y1;
            x = minX;
            y = minY;
            x1 = maxX;
            y1 = maxY;
            ucMap1.Map.ConvertCoord(ref x, ref y, ref lon, ref lat, ConversionConstants.miScreenToMap);
            ucMap1.Map.ConvertCoord(ref x1, ref y1, ref lon1, ref lat1,
                                    ConversionConstants.miScreenToMap);
        }

        /// <summary>
        /// Chuyển đổi tọa độ lon, lat sang tọa độ màn hình của vùng chọn
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        /// <param name="leftlon"></param>
        /// <param name="toplat"></param>
        /// <param name="rightlon"></param>
        /// <param name="bottomlat"></param>
        protected void ConvertMapToScreenCoord(ref int left, ref int top, ref int right, ref int bottom, double leftlon, double toplat, double rightlon, double bottomlat)
        {
            float x, y, x1, y1;
            
            x = left;
            y = top;
            x1 = right;
            y1 = bottom;
            
            ucMap1.Map.ConvertCoord(ref x, ref y, ref leftlon, ref toplat, ConversionConstants.miMapToScreen);
            ucMap1.Map.ConvertCoord(ref x1, ref y1, ref rightlon, ref bottomlat, ConversionConstants.miMapToScreen);

            left = (int)x;
            top = (int)y;
            right = (int)x1;
            bottom = (int)y1;
        }
        
    }
}

