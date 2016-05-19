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

namespace HTMapLib
{
    public enum enumDrawingToolStrip
    {
        None,
        Rectangle,
        Ellipse,
        Line,
        Polygon,
        NumberOfDrawTools
    };

    public partial class ucDrawingToolStrip : UserControl
    {
        private enumDrawingToolStrip ActiveTool;
        private ucMap mapControl;
        private AxMapXLib.AxMap AxMap1;

        private List<DrawObject> objList = new List<DrawObject>();
        private DrawObject ObjDrawing;
        private bool DrawingDragging = false;

        public ucDrawingToolStrip(ucMap map)
        {
            InitializeComponent();
            mapControl = map;
            mapControl.AxMapDrawing_MouseDownEvent += new AxMapXLib.CMapXEvents_MouseDownEventHandler(AxMapDrawing_MouseDownEvent);
            mapControl.AxMapDrawing_MouseMoveEvent += new AxMapXLib.CMapXEvents_MouseMoveEventHandler(AxMapDrawing_MouseMoveEvent);
            mapControl.AxMapDrawing_MouseUpEvent += new AxMapXLib.CMapXEvents_MouseUpEventHandler(AxMapDrawing_MouseUpEvent);
            mapControl.AxMapDrawing_DrawUserLayer += new AxMapXLib.CMapXEvents_DrawUserLayerEventHandler(AxMapDrawing_DrawUserLayer);
            mapControl.AxMapDrawing_MouseWheelEvent += new AxMapXLib.CMapXEvents_MouseWheelEventHandler(AxMapDrawing_MouseWheelEvent);
            AxMap1 = mapControl.Map;
        }

        private void ucDrawingToolStrip_Load(object sender, EventArgs e)
        {
            ToolStripSetup();
        }

        private ImageList imgList = new ImageList();
        public void ToolStripSetup()
        {
            imgList.ImageSize = new Size(18, 16);
            imgList.ColorDepth = ColorDepth.Depth4Bit;
            imgList.TransparentColor = Color.FromArgb(192, 192, 192);

            imgList.Images.AddStrip(Resources.buttons);
        }

        private void toolStripButtonEllipse_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miArrowTool;
            mapControl.ActiveToolStrip = enumToolStripType.DrawingToolStrip;
            ActiveTool = enumDrawingToolStrip.Ellipse;
            ObjDrawing = new DrawEllipse();
        }

        private void toolStripButtonLine_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miArrowTool;
            mapControl.ActiveToolStrip = enumToolStripType.DrawingToolStrip;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miArrowTool;
            mapControl.ActiveToolStrip = enumToolStripType.DrawingToolStrip;
        }

        private void toolStripButtonGrid_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miArrowTool;
            mapControl.ActiveToolStrip = enumToolStripType.GridRegion;
            ActiveTool = enumDrawingToolStrip.None;
        }

        private void toolStripButtonRectangle_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miArrowTool;
            mapControl.ActiveToolStrip = enumToolStripType.DrawingToolStrip;
            ActiveTool = enumDrawingToolStrip.Rectangle;
            ObjDrawing = new DrawRectangle();
        }

        #region "Map Events"
        private void AxMapDrawing_MouseDownEvent(object sender, AxMapXLib.CMapXEvents_MouseDownEvent e)
        {
            Point pScreen = new Point((int)e.x, (int)e.y);
            if (e.button == 1)
            {
                //double mapX, mapY;
                //mapX = mapY = 0;
                //mapControl.Map.ConvertCoord(ref e.x, ref e.y, ref mapX, ref mapY, MapXLib.ConversionConstants.miScreenToMap);
                if (e.button == 1)
                {
                    //Nếu không phải các tool pointer, zoom, hand 
                    DrawingDragging = true;
                    ObjDrawing.MoveHandleTo(pScreen, 1);
                }
            }

        }

        private void AxMapDrawing_MouseMoveEvent(object sender, AxMapXLib.CMapXEvents_MouseMoveEvent e)
        {
            Point pScreen = new Point((int)e.x, (int)e.y);
            if (e.button == 1)
            {
                Graphics g = mapControl.Map.CreateGraphics();
                g.SmoothingMode = SmoothingMode.AntiAlias;

                //HT: Kiểm tra nếu đang vẽ ký hiệu và đang nhấn giữ chuột trái
                if (DrawingDragging)
                {
                    ObjDrawing.MoveHandleTo(pScreen, 5);
                    mapControl.Map.Refresh();
                    ObjDrawing.Draw(g);
                }

                g.Dispose();
            }

        }

        private void AxMapDrawing_MouseUpEvent(object sender, AxMapXLib.CMapXEvents_MouseUpEvent e)
        {
            Point pScreen = new Point((int)e.x, (int)e.y);
            if (e.button == 1)
            {
                //HT: Nếu đang là trường hợp vẽ drag chuột thì reset lại
                if (DrawingDragging)
                {
                    DrawingDragging = false;
                    ObjDrawing.MoveHandleTo(pScreen, 5);
                    objList.Add(ObjDrawing);
                }
            }
        }

        private void AxMapDrawing_DrawUserLayer(object sender, AxMapXLib.CMapXEvents_DrawUserLayerEvent e)
        {
            Graphics myGraphics = Graphics.FromHdc(new IntPtr(e.hOutputDC));
            try
            {
                foreach (DrawObject obj in objList)
                {
                    obj.Draw(myGraphics);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw new System.ApplicationException("Error Drawing Graphics Surface", ex);
            }
            finally
            {
                //mPen1.Dispose();
                //seleBrush.Dispose();
            }
        }

        

        private void AxMapDrawing_MouseWheelEvent(object sender, AxMapXLib.CMapXEvents_MouseWheelEvent e)
        {

        }

        #endregion

       
    }
}
