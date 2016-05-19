using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using HTMapLib.Properties;
using MapXLib;

namespace HTMapLib
{
    public enum enumActionToolStrip
    {
        None,
        Distance
    };

    public partial class ucToolStrip : UserControl
    {
        public enumActionToolStrip ActiveTool;
        private ucMap mapControl;
        private AxMapXLib.AxMap AxMap1;
        
        public ucToolStrip(ucMap map)
        {
            InitializeComponent();
            mapControl = map;
            mapControl.AxMapAction_MouseDownEvent += new AxMapXLib.CMapXEvents_MouseDownEventHandler(AxMapAction_MouseDownEvent);
            mapControl.AxMapAction_MouseMoveEvent += new AxMapXLib.CMapXEvents_MouseMoveEventHandler(AxMapAction_MouseMoveEvent);
            mapControl.AxMapAction_MouseUpEvent += new AxMapXLib.CMapXEvents_MouseUpEventHandler(AxMapAction_MouseUpEvent);
            mapControl.AxMapAction_DrawUserLayer += new AxMapXLib.CMapXEvents_DrawUserLayerEventHandler(AxMapAction_DrawUserLayer);
            mapControl.AxMapAction_MouseWheelEvent += new AxMapXLib.CMapXEvents_MouseWheelEventHandler(AxMapAction_MouseWheelEvent);
            AxMap1 = mapControl.Map;
        }

        private void ucToolStrip_Load(object sender, EventArgs e)
        {
            ToolStripSetup();
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
        }

        private void toolStripButtonSelectTool_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miArrowTool;
            mapControl.ActiveToolStrip = enumToolStripType.ActionToolStrip;
            ActiveTool = enumActionToolStrip.None;
        }

        private void toolStripButtonPanTool_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miPanTool;
            mapControl.ActiveToolStrip = enumToolStripType.ActionToolStrip;
            ActiveTool = enumActionToolStrip.None;
        }

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miZoomInTool;
            mapControl.ActiveToolStrip = enumToolStripType.ActionToolStrip;
            ActiveTool = enumActionToolStrip.None;
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miZoomOutTool;
            mapControl.ActiveToolStrip = enumToolStripType.ActionToolStrip;
            ActiveTool = enumActionToolStrip.None;
        }

        private void toolStripButtonDistance_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = ToolConstants.miArrowTool;
            mapControl.ActiveToolStrip = enumToolStripType.ActionToolStrip;
            ActiveTool = enumActionToolStrip.Distance;
        }

        private void toolStripButtonAllLayersView_Click(object sender, EventArgs e)
        {
            mapControl.ActiveToolStrip = enumToolStripType.ActionToolStrip;
            AxMap1.ZoomTo(mapControl.Zoom_Original, mapControl.CenterX_Original, mapControl.CenterY_Original);
        }

        #region "Map Events"

        private PointF firstPoint;
        private PointF mouseDownPoint;
        private PointF mouseMovePoint;
        private void AxMapAction_MouseDownEvent(object sender, AxMapXLib.CMapXEvents_MouseDownEvent e)
        {
            if (e.button == 1)
            {
                double mapX, mapY;
                mapX = mapY = 0;
                mapControl.Map.ConvertCoord(ref e.x, ref e.y, ref mapX, ref mapY, MapXLib.ConversionConstants.miScreenToMap);
                if (e.button == 1)
                {
                    //Nếu là đo khoảng cách
                    if (ActiveTool == enumActionToolStrip.Distance)
                    {
                        firstPoint = new PointF(e.x, e.y);
                    }
                }
            }
            else if (e.button == 2)
            {
                //Nếu đang ở kéo bản đồ thì chuyển sang mũi tên
                if (AxMap1.CurrentTool == MapXLib.ToolConstants.miPanTool)
                {
                    AxMap1.CurrentTool = MapXLib.ToolConstants.miArrowTool;
                }

                if (ActiveTool == enumActionToolStrip.Distance)
                    ActiveTool = enumActionToolStrip.None;
            }

        }

        private void AxMapAction_MouseMoveEvent(object sender, AxMapXLib.CMapXEvents_MouseMoveEvent e)
        {
            if (e.button == 1)
            {
                Graphics g = mapControl.Map.CreateGraphics();
                g.SmoothingMode = SmoothingMode.AntiAlias;
                if (ActiveTool == enumActionToolStrip.None)
                {
                    mouseMovePoint = new PointF(e.x, e.y);

                    //Nếu đang ở tool mũi tên và nhấn giữ chuột trái di chuyển > 100 pixel thì chuyển sang PAN
                    if (AxMap1.CurrentTool == MapXLib.ToolConstants.miArrowTool
                        && MapHelper.CalculateDistance(AxMap1, mouseDownPoint, mouseMovePoint, enumDistance.Pixel) > 100)
                    {
                        AxMap1.CurrentTool = MapXLib.ToolConstants.miPanTool;

                    }
                }
                else if (ActiveTool == enumActionToolStrip.Distance)
                {
                    PointF secondPoint = new PointF(e.x, e.y);
                    mapControl.Map.Refresh();

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
                if (ActiveTool == enumActionToolStrip.Distance)
                {
                    PointF secondPoint = new PointF(e.x, e.y);
                    double distance = MapHelper.CalculateDistance(AxMap1, firstPoint, secondPoint, enumDistance.Kilometer);
                    MessageBox.Show(string.Format("Khoảng cách là {0} km", distance.ToString("#,##0.##0")));
                    ActiveTool = enumActionToolStrip.None;
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
    }
}
