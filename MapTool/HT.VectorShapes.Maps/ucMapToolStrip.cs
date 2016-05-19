using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using HT.VectorShapes.Maps.Properties;
using MapXLib;

namespace HT.VectorShapes.Maps
{
    

    public partial class ucMapToolStrip : UserControl
    {
        private ucVectShapes_Map s;
        
        public ucMapToolStrip()
        {
            InitializeComponent();
        }

        private void ucToolStrip_Load(object sender, EventArgs e)
        {
            ToolStripSetup();
        }

        public void Init(ucVectShapes_Map uc)
        {
            s = uc;
            s.ucMap1.AxMapAction_MouseDownEvent += new AxMapXLib.CMapXEvents_MouseDownEventHandler(AxMapAction_MouseDownEvent);
            s.ucMap1.AxMapAction_MouseMoveEvent += new AxMapXLib.CMapXEvents_MouseMoveEventHandler(AxMapAction_MouseMoveEvent);
            s.ucMap1.AxMapAction_MouseUpEvent += new AxMapXLib.CMapXEvents_MouseUpEventHandler(AxMapAction_MouseUpEvent);
            s.ucMap1.AxMapAction_DrawUserLayer += new AxMapXLib.CMapXEvents_DrawUserLayerEventHandler(AxMapAction_DrawUserLayer);
            s.ucMap1.AxMapAction_MouseWheelEvent += new AxMapXLib.CMapXEvents_MouseWheelEventHandler(AxMapAction_MouseWheelEvent);
        }
        public ToolStrip ToolStripControl
        {
            get { return toolStrip1; }
        }

        private ImageList imgList = new ImageList();
        public void ToolStripSetup()
        {
            imgList.ImageSize = new Size(18, 16);
            imgList.ColorDepth = ColorDepth.Depth4Bit;
            imgList.TransparentColor = Color.FromArgb(192, 192, 192);

            imgList.Images.AddStrip(Resources.buttons);

            toolStripButtonSelectTool.Image = imgList.Images[9];
            toolStripButtonPanTool.Image = imgList.Images[18];
            toolStripButtonZoomIn.Image = imgList.Images[15];
            toolStripButtonZoomOut.Image = imgList.Images[16];
            toolStripButtonDistance.Image = imgList.Images[31];
            toolStripButtonAllLayersView.Image = imgList.Images[32];
            toolStripButtonDrawing.Image = imgList.Images[33];
        }

        private void toolStripButtonSelectTool_Click(object sender, EventArgs e)
        {
            Helper.ResetStatus(enumMapOption.ARROW);
            s.ucMap1.Map.CurrentTool = MapXLib.ToolConstants.miArrowTool;
        }

        private void toolStripButtonPanTool_Click(object sender, EventArgs e)
        {
            Helper.ResetStatus(enumMapOption.PAN);
            s.ucMap1.Map.CurrentTool = MapXLib.ToolConstants.miPanTool;
        }

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            Helper.ResetStatus(enumMapOption.ZOOMIN);
            s.ucMap1.Map.CurrentTool = MapXLib.ToolConstants.miZoomInTool;
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            Helper.ResetStatus(enumMapOption.ZOOMOUT);
            s.ucMap1.Map.CurrentTool = MapXLib.ToolConstants.miZoomOutTool;
        }

        private void toolStripButtonDistance_Click(object sender, EventArgs e)
        {
            Helper.ResetStatus(enumMapOption.DISTANCE);
            s.ucMap1.Map.CurrentTool = ToolConstants.miArrowTool;
        }

        private void toolStripButtonAllLayersView_Click(object sender, EventArgs e)
        {
            Helper.MapOption = enumMapOption.ALLLAYERS;
            s.ucMap1.Map.ZoomTo(s.ucMap1.Zoom_Original, s.ucMap1.CenterX_Original, s.ucMap1.CenterY_Original);
        }

        #region "Map Events"

        private PointF firstPoint;
        private PointF mouseDownPoint;
        private PointF mouseMovePoint;
        private void AxMapAction_MouseDownEvent(object sender, AxMapXLib.CMapXEvents_MouseDownEvent e)
        {
            mouseDownPoint = new PointF(e.x, e.y);
            if (e.button == 1)
            {
                double mapX, mapY;
                mapX = mapY = 0;
                s.ucMap1.Map.ConvertCoord(ref e.x, ref e.y, ref mapX, ref mapY, MapXLib.ConversionConstants.miScreenToMap);
                if (e.button == 1)
                {
                    //Nếu là đo khoảng cách
                    if (Helper.MapOption == enumMapOption.DISTANCE)
                    {
                        firstPoint = mouseDownPoint;
                    }
                }
            }
            else if (e.button == 2)
            {
                //Nếu đang ở kéo bản đồ thì chuyển sang mũi tên
                if (Helper.MapOption == enumMapOption.PAN)
                    toolStripButtonSelectTool.PerformClick();
                else if (Helper.MapOption == enumMapOption.DISTANCE)
                    toolStripButtonSelectTool.PerformClick();
            }

        }

        private void AxMapAction_MouseMoveEvent(object sender, AxMapXLib.CMapXEvents_MouseMoveEvent e)
        {
            if (e.button == 1)
            {
                //Nếu đang ở trạng thái thao tác bản đồ hoặc đang chọn ký hiệu
                Graphics g = s.ucMap1.Map.CreateGraphics();
                g.SmoothingMode = SmoothingMode.AntiAlias;
                if (Helper.MapOption == enumMapOption.ARROW)
                {
                    mouseMovePoint = new PointF(e.x, e.y);

                    //Nếu đang ở tool mũi tên và nhấn giữ chuột trái di chuyển > 100 pixel thì chuyển sang PAN
                    if (Helper.MapOption == enumMapOption.ARROW &&
                        MapHelper.CalculateDistance(s.ucMap1.Map, mouseDownPoint, mouseMovePoint, enumDistance.Pixel) >
                        100)
                    {
                        toolStripButtonPanTool.PerformClick();
                    }
                }
                else if (Helper.MapOption == enumMapOption.DISTANCE)
                {
                    PointF secondPoint = new PointF(e.x, e.y);
                    s.ucMap1.Map.Refresh();

                    Pen p = new Pen(Color.Green, 2);
                    g.DrawLine(p, firstPoint, secondPoint);
                    p.Dispose();
                }

                g.Dispose();
            }

        }

        private void AxMapAction_MouseUpEvent(object sender, AxMapXLib.CMapXEvents_MouseUpEvent e)
        {
            if (e.button == 1)
            {
                if (Helper.MapOption == enumMapOption.DISTANCE)
                {
                    PointF secondPoint = new PointF(e.x, e.y);
                    double distance = MapHelper.CalculateDistance(s.ucMap1.Map, firstPoint, secondPoint, enumDistance.Kilometer);
                    MessageBox.Show(string.Format("Khoảng cách là {0} km", distance.ToString("#,##0.##0")));
                    toolStripButtonSelectTool.PerformClick();
                }
            }
        }

        private void AxMapAction_DrawUserLayer(object sender, AxMapXLib.CMapXEvents_DrawUserLayerEvent e)
        {
            //Graphics myGraphics = Graphics.FromHdc(new IntPtr(e.hOutputDC));
            //try
            //{
            //    foreach (DrawObject obj in objList)
            //    {
            //        obj.Draw(myGraphics);
            //    }
            //    if (objDrawRegion != null)
            //        objDrawRegion.Draw(myGraphics);
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.ToString());
            //    throw new System.ApplicationException("Error Drawing Graphics Surface", ex);
            //}
            //finally
            //{
            //    //mPen1.Dispose();
            //    //seleBrush.Dispose();
            //}
        }

        private void AxMapAction_MouseWheelEvent(object sender, AxMapXLib.CMapXEvents_MouseWheelEvent e)
        {
            
        }
        #endregion

        public void AddToolStrip(ToolStripButton toolStrip, int imageIndex =-1)
        {
            if (imageIndex > 0)
            {
                toolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
                toolStrip.Image = imgList.Images[imageIndex];
                toolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            }
            toolStrip1.Items.Add(toolStrip);
        }

        public event EventHandler ToolStripButtonDrawing_Clicked;
        private void toolStripButtonDrawing_Click(object sender, EventArgs e)
        {
            MapHelper.IsDrawing = !MapHelper.IsDrawing;
            if (ToolStripButtonDrawing_Clicked != null)
                ToolStripButtonDrawing_Clicked(sender, e);
        }
    }
}
