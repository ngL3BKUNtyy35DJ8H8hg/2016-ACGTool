using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ConfigBDTC.Classes;
using ConfigBDTC2.Scripts;

namespace ConfigBDTC
{
    public partial class frmCreateBooms : Form
    {
        public enum DrawToolType
        {
            Pointer,
            Rectangle,
            Ellipse,
            Line,
            Polygon,
            GridRegion,
            NumberOfDrawTools
        };

        private DrawToolType activeTool;      // active drawing tool

        private DrawObject objDrawing;
        private bool DrawingDragging = false;
        
        private List<DrawObject> objList = new List<DrawObject>();

        //Draw grid to create booms
        private DrawRegion objDrawRegion;

        public frmCreateBooms()
        {
            InitializeComponent();
            InitMap();
            ToolStripSetup();
        }

        public void ToolStripSetup()
        {
            //// Create the ImageList
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(18, 16);
            imgList.ColorDepth = ColorDepth.Depth4Bit;
            imgList.TransparentColor = Color.FromArgb(192, 192, 192);

            // adds the bitmap
            if (File.Exists("buttons.bmp"))
            {
                //path = System.IO.Path.GetFullPath("buttons.bmp");
                //bmp = new Bitmap(Assembly.GetExecutingAssembly().GetManifestResourceStream(this.GetType(), "buttons.bmp"));
                Bitmap bmp = new Bitmap("buttons.bmp");
                imgList.Images.AddStrip(bmp);
            }

            toolStripButtonSelectTool.Image = imgList.Images[9];
            toolStripButtonPanTool.Image = imgList.Images[18];
            toolStripButtonZoomIn.Image = imgList.Images[15];
            toolStripButtonZoomOut.Image = imgList.Images[16];
        }


        public void InitMap()
        {
            AxMap1.GeoSet = string.Format(@"{0}\BanDo\BanDo.gst", Application.StartupPath);
            AxMap1.Title.Visible = false;
            AxMap1.Zoom = 100000;

            AxMap1.CurrentTool = MapXLib.ToolConstants.miArrowTool;
            //MapXLib.CoordSys mydcs = AxMap1.DisplayCoordSys;
            //AxMap1.NumericCoordSys = mydcs;
            //AxMap1.InfotipSupport = false;
            //AxMap1.PaperUnit = MapXLib.PaperUnitConstants.miPaperUnitPoint;
            AxMap1.Layers.AddUserDrawLayer("Symbols", 1);
            

            //double x = AxMap1.Layers["Symbols"].CoordSys.Bounds.XMin;
            //double y = AxMap1.Layers["Symbols"].CoordSys.Bounds.YMin;
            
            //double zoom = AxMap1.Zoom;
            //double cX = AxMap1.CenterX;
            //double cY = AxMap1.CenterY;
            //AxMap1.CreateCustomTool(3, MapXLib.ToolTypeConstants.miToolTypePoint,
            //                            MapXLib.CursorConstants.miArrowQuestionCursor);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripButtonSelectTool_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miSelectTool;
            activeTool = DrawToolType.Pointer;
        }

        private void toolStripButtonPanTool_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miPanTool;
             
            activeTool = DrawToolType.Pointer;
        }

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miZoomInTool;
            activeTool = DrawToolType.Pointer;
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miZoomOutTool;
            activeTool = DrawToolType.Pointer;
        }

        private void toolStripButtonRectangle_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miArrowTool;
            activeTool = DrawToolType.Rectangle;
            objDrawing = new DrawRectangle();
        }

        private void toolStripButtonEllipse_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miArrowTool;
            activeTool = DrawToolType.Ellipse;
            objDrawing = new DrawEllipse();
        }

        private void toolStripButtonLine_Click(object sender, EventArgs e)
        {

        }

        private void AxMap1_MouseDownEvent(object sender, AxMapXLib.CMapXEvents_MouseDownEvent e)
        {
            Point pScreen = new Point((int)e.x, (int)e.y);
            PointF pScreen1 = new PointF(e.x, e.y);
            double mapX, mapY;
            mapX = mapY = 0;
            //AxMap1.ConvertCoord(ref pScreen1.X, ref pScreen1.Y, ref mapX, ref mapY, MapXLib.ConversionConstants.miScreenToMap);
            AxMap1.ConvertCoord(ref e.x, ref e.y, ref mapX, ref mapY, MapXLib.ConversionConstants.miScreenToMap);
            if (e.button == 1)
            {
                if (activeTool != DrawToolType.Pointer)
                {
                    DrawingDragging = true;
                    if (activeTool == DrawToolType.GridRegion)
                    {
                        int numOfBooms = int.Parse(txtNumOfBooms.Text);
                        int numOfRows = int.Parse(txtNumOfRows.Text);
                        int numOfColumns = int.Parse(txtNumOfColumns.Text);
                        objDrawRegion = new DrawRegion(AxMap1, numOfBooms, numOfRows, numOfColumns);
                        objDrawRegion.MoveHandleTo(pScreen, 1);
                    }
                    else
                        objDrawing.MoveHandleTo(pScreen, 1);
                }
            }
        }

        private void AxMap1_MouseMoveEvent(object sender, AxMapXLib.CMapXEvents_MouseMoveEvent e)
        {
            Point pScreen = new Point((int)e.x, (int)e.y);
            

            //HT: Kiểm tra nếu đang vẽ ký hiệu và đang nhấn giữ chuột trái
            if (DrawingDragging)
            {
                Graphics g = AxMap1.CreateGraphics();
                if (activeTool == DrawToolType.GridRegion)
                {
                    objDrawRegion.MoveHandleTo(pScreen, 5);
                    AxMap1.Refresh();
                    objDrawRegion.Draw(g);
                }
                else
                {
                    objDrawing.MoveHandleTo(pScreen, 5);
                    AxMap1.Refresh();
                    objDrawing.Draw(g);
                }
            }
            else
            {
                //HT: Hiển thị tọa độ Screen
                txtSceenX.Text = e.x.ToString();
                txtScreenY.Text = e.y.ToString();
                
                //Hiển thị tọa độ Longitude, Latitude
                double lon, lat;
                lon = lat = 0;
                AxMap1.ConvertCoord(ref e.x, ref e.y, ref lon, ref lat, MapXLib.ConversionConstants.miScreenToMap);
                txtLongitude.Text = lon.ToString();
                txtLatitude.Text = lat.ToString();
                toolStripStatusLabelCoordinate.Text = string.Format("K: {0} - V: {1}", lon.ToString(), lat.ToString());

                //Hiển thị tọa độ 3D
                float x3D, y3D;
                x3D = y3D = 0;
                Projection3D.Convert3DCoord(ref x3D, ref y3D, ref lon, ref lat, Conversion3DConstants.To3D);
                txtX3D.Text = x3D.ToString();
                txtY3D.Text = y3D.ToString();
            }
        }

        private void AxMap1_MouseUpEvent(object sender, AxMapXLib.CMapXEvents_MouseUpEvent e)
        {
            Point pScreen = new Point((int)e.x, (int)e.y);
            //HT: Nếu đang là trường hợp vẽ drag chuột thì reset lại
            if (DrawingDragging)
            {
                DrawingDragging = false;
                if (activeTool == DrawToolType.GridRegion)
                {
                    objDrawRegion.MoveHandleTo(pScreen, 5);
                }
                else
                {
                    objDrawing.MoveHandleTo(pScreen, 5);
                    objList.Add(objDrawing);
                }
                
            }
        }

        private void AxMap1_DrawUserLayer(object sender, AxMapXLib.CMapXEvents_DrawUserLayerEvent e)
        {
            //HatchBrush seleBrush = new HatchBrush(HatchStyle.DiagonalCross, Color.White, Color.Transparent);
            //Pen mPen1 = new Pen(Color.Blue, 4);
            //mPen1.Brush = seleBrush;
            Graphics myGraphics = Graphics.FromHdc(new IntPtr(e.hOutputDC));
            try
            {
                foreach (DrawObject obj in objList)
                {
                    obj.Draw(myGraphics);
                }
                if (objDrawRegion != null)
                    objDrawRegion.Draw(myGraphics);
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

        private void toolStripButtonGrid_Click(object sender, EventArgs e)
        {
            AxMap1.CurrentTool = MapXLib.ToolConstants.miArrowTool;
            activeTool = DrawToolType.GridRegion;
            
        }

        private void btnCreateBooms_Click(object sender, EventArgs e)
        {
            Projection3D.CenterMapX = double.Parse(txtCenterMapX.Text);
            Projection3D.CenterMapY = double.Parse(txtCenterMapY.Text);
            Projection3D.Height3D = double.Parse(txtHeight3D.Text);
            Projection3D.Width3D = double.Parse(txtWidth3D.Text);
            Projection3D.WidthMap = double.Parse(txtWidthMap.Text);
            Projection3D.HeightMap = double.Parse(txtHeightMap.Text);



            ActionScript obj = new ActionScript();
            obj.CreateBoomXml(objDrawRegion.Point3DList, txtObjName.Text, int.Parse(txtNumOfObjNames.Text), int.Parse(txtStart.Text), float.Parse(txtIncrement.Text), int.Parse(txtMinGroup.Text), int.Parse(txtMaxGroup.Text), checkBoxDanDich.Checked);
        }

        private void txtWidth3D_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            frmConfigDiaHinhFile frm = new frmConfigDiaHinhFile();
            frm.ShowDialog();
        }
    }
}
