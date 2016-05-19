using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BDTCLib;
using ConfigBDTC2.Scripts;
using HTMapLib;
using HTMapLib.Classes;
using HTMapLib.VectorShapes.Shapes;
using MapTool;
using MapTool.Properties;

namespace MapTool
{

    public partial class frmBDTCShapeViewer : Form
    {
        public ucVectShapes_Map mapControl;
        private ucToolStrip objToolStripControl;

        //Draw grid to create booms
        private DrawRegion objDrawRegion;

        private DiaHinh _objDiaHinh;
        private Projection3D objProjection3D;

        public frmBDTCShapeViewer()
        {
            Thread th = new Thread(new ThreadStart(DoSplash));
            th.Start();

            InitializeComponent();
            mapControl = new ucVectShapes_Map();
            this.ucVectShapeToolBox1.setVectShape(this.mapControl);

            //Load Map user control           
            mapControl.VectShapMap_MouseDownEvent += new AxMapXLib.CMapXEvents_MouseDownEventHandler(AxMap_MouseDownEvent);
            mapControl.VectShapMap_MouseMoveEvent += new AxMapXLib.CMapXEvents_MouseMoveEventHandler(AxMap_MouseMoveEvent);
            mapControl.VectShapMap_MouseUpEvent += new AxMapXLib.CMapXEvents_MouseUpEventHandler(AxMap_MouseUpEvent);
            mapControl.VectShapMap_DrawUserLayer += new AxMapXLib.CMapXEvents_DrawUserLayerEventHandler(AxMap_DrawUserLayer);
            mapControl.VectShapMap_MouseWheelEvent += new AxMapXLib.CMapXEvents_MouseWheelEventHandler(AxMap_MouseWheelEvent);
            mapControl.VectShapMap_MapViewChanged += new System.EventHandler(AxMap_MapViewChangedEvent);
            mapControl.VectShapMap_DblClick += new System.EventHandler(this.AxMap_DblClick);

            //Load MapControl
            mapControl.Dock = DockStyle.Fill;
            splitContainer2.Panel1.Controls.Add(mapControl);
            
            //Load ToolStrip user control
            objToolStripControl = new ucToolStrip(mapControl.ucMap1);
            objToolStripControl.Dock = DockStyle.Top;
            objToolStripControl.SendToBack();
            splitContainer2.Panel1.Controls.Add(objToolStripControl);

            if (sp != null)
            {
                sp.CloseSplash();
            }
            else
            {
                th.Abort();
            }
        }

        private void frmBDTCShapeViewer_Load(object sender, EventArgs e)
        {
            try
            {
                mapControl.isShowDrawingRoute = true;
                splitContainer1.SplitterDistance = 300;
                if (Properties.Settings.Default.RecentDiaHinhDirectory != "")
                {
                    FileAttributes attr = File.GetAttributes(Properties.Settings.Default.RecentDiaHinhDirectory);
                    //detect whether its a directory or file
                    if ((attr & FileAttributes.Directory) != FileAttributes.Directory) //It isn't a folder
                    {
                        txtFilePath.Text = Properties.Settings.Default.RecentDiaHinhDirectory;
                        if (File.Exists(txtFilePath.Text))
                        {
                            LoadFile(txtFilePath.Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                
            }
            
        }

        splash sp;
        private void DoSplash()
        {
            sp = new splash();
            sp.ShowDialog();

        }

        #region "Map Events"
        private bool DrawingDragging = false;
        private void AxMap_MouseDownEvent(object sender, AxMapXLib.CMapXEvents_MouseDownEvent e)
        {
            Point pScreen = new Point((int)e.x, (int)e.y);
            if (e.button == 1)
            {
                double mapX, mapY;
                mapX = mapY = 0;
                mapControl.ucMap1.Map.ConvertCoord(ref e.x, ref e.y, ref mapX, ref mapY, MapXLib.ConversionConstants.miScreenToMap);
                if (e.button == 1)
                {
                    
                    if (mapControl.ucMap1.ActiveToolStrip == enumToolStripType.GridRegion)
                    {
                        DrawingDragging = true;
                        int numOfBooms = int.Parse(txtNumOfBooms.Text);
                        int numOfRows = int.Parse(txtNumOfRows.Text);
                        int numOfColumns = int.Parse(txtNumOfColumns.Text);
                        objDrawRegion = new DrawRegion(mapControl.ucMap1.Map, objProjection3D, numOfBooms, numOfRows, numOfColumns);
                        objDrawRegion.MoveHandleTo(pScreen, 1);
                    }
                }
            }
            else if (e.button == 2) //Trường hợp click chuột phải
            {
                //Nếu là đang chọn ActionToolStrip và CurrentTool đang là mũi tên
                if (mapControl.ucMap1.ActiveToolStrip == enumToolStripType.ActionToolStrip
                    && mapControl.ucMap1.Map.CurrentTool == MapXLib.ToolConstants.miArrowTool)
                {
                    // Convert from Map coordinates to Screen coordinates 
                    Point p = mapControl.ucMap1.Map.PointToScreen(pScreen);
                    contextMenuStripMap.Show(p);
                }
            }
            
        }

        private void AxMap_MouseMoveEvent(object sender, AxMapXLib.CMapXEvents_MouseMoveEvent e)
        {
            UpdateStatusStrip(e.x, e.y);
            Point pScreen = new Point((int)e.x, (int)e.y);
            if (e.button == 1)
            {
                Graphics g = mapControl.ucMap1.Map.CreateGraphics();
                g.SmoothingMode = SmoothingMode.AntiAlias;

                //HT: Kiểm tra nếu đang vẽ ký hiệu và đang nhấn giữ chuột trái
                if (mapControl.ucMap1.ActiveToolStrip == enumToolStripType.GridRegion)
                {
                    if (DrawingDragging)
                    {
                        objDrawRegion.MoveHandleTo(pScreen, 5);
                        mapControl.ucMap1.Map.Refresh();
                        objDrawRegion.Draw(g);
                    }
                }

                g.Dispose();
            }

        }

        private void AxMap_MouseUpEvent(object sender, AxMapXLib.CMapXEvents_MouseUpEvent e)
        {
            UpdateStatusStrip(e.x, e.y);
            Point pScreen = new Point((int)e.x, (int)e.y);
            if (e.button == 1)
            {
                //HT: Nếu đang là trường hợp vẽ drag chuột thì reset lại
                if (mapControl.ucMap1.ActiveToolStrip == enumToolStripType.GridRegion)
                {
                    if (DrawingDragging)
                    {
                        DrawingDragging = false;
                        objDrawRegion.MoveHandleTo(pScreen, 5);
                    }
                }
                
            }
        }

        private void AxMap_DrawUserLayer(object sender, AxMapXLib.CMapXEvents_DrawUserLayerEvent e)
        {
            Graphics g = Graphics.FromHdc(new IntPtr(e.hOutputDC));
            //Graphics g = mapControl.ucMap1.Map.CreateGraphics();
            g.SmoothingMode = SmoothingMode.AntiAlias;
            try
            {
                if (mapControl.ucMap1.ActiveToolStrip == enumToolStripType.GridRegion)
                {
                    if (objDrawRegion != null)
                        objDrawRegion.Draw(g);
                }
                

                //Vẽ khung tọa độ trong file .diahinh
                //RectangleF rectDiaHinh = new RectangleF();
                float x1, y1, x2, y2;
                x1 = y1 = x2 = y2 = 0;
                double lon1, lat1, lon2, lat2;
                lon1 = double.Parse(_objDiaHinh._myMap2X);
                lat1 = double.Parse(_objDiaHinh._myMap1Y);
                lon2 = double.Parse(_objDiaHinh._myMap1X);
                lat2 = double.Parse(_objDiaHinh._myMap2Y);
                mapControl.ucMap1.Map.ConvertCoord(ref x1, ref y1, ref lon1, ref lat1, MapXLib.ConversionConstants.miMapToScreen);
                mapControl.ucMap1.Map.ConvertCoord(ref x2, ref y2, ref lon2, ref lat2, MapXLib.ConversionConstants.miMapToScreen);
                Pen pen = new Pen(Color.Red, 2);
                
                g.DrawRectangle(new Pen(Brushes.Red), x1, y1, Math.Abs(x2 - x1), Math.Abs(y2-y1));

                //Vẽ khung tọa độ trong file .tab
                ListView listViewGstMap = new ListView();
                _objDiaHinh._objMyMapGst.BindGstMap_ListView(listViewGstMap);
                lon1 = double.Parse(_objDiaHinh._objMyMapGst._objTabFileInfo.PointInfoDict["NW"].Lon);
                lat1 = double.Parse(_objDiaHinh._objMyMapGst._objTabFileInfo.PointInfoDict["NW"].Lat);
                lon2 = double.Parse(_objDiaHinh._objMyMapGst._objTabFileInfo.PointInfoDict["SE"].Lon);
                lat2 = double.Parse(_objDiaHinh._objMyMapGst._objTabFileInfo.PointInfoDict["SE"].Lat);
                mapControl.ucMap1.Map.ConvertCoord(ref x1, ref y1, ref lon1, ref lat1, MapXLib.ConversionConstants.miMapToScreen);
                mapControl.ucMap1.Map.ConvertCoord(ref x2, ref y2, ref lon2, ref lat2, MapXLib.ConversionConstants.miMapToScreen);
                pen = new Pen(Color.Yellow,2);
                g.DrawRectangle(pen, x1, y1, Math.Abs(x2 - x1), Math.Abs(y2 - y1));

                //Vẽ khung tọa độ trong file .xyz

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                throw new System.ApplicationException("Error Drawing Graphics Surface", ex);
            }
            finally
            {
                g.Dispose();
            }
        }

       

        private void AxMap_MouseWheelEvent(object sender, AxMapXLib.CMapXEvents_MouseWheelEvent e)
        {
            UpdateStatusStrip(e.x, e.y);
        }

        private void AxMap_MapViewChangedEvent(object sender, EventArgs e)
        {
            toolStripStatusLabelAltitude.Text = string.Format("Độ cao: {0} km", mapControl.ucMap1.Altitude.ToString("#,##0.##0"));
        }

        private void AxMap_DblClick(object sender, EventArgs e)
        {
            if (mapControl.ucMap1.ActiveToolStrip == enumToolStripType.VectorShapeToolStrip)
            {
                if (this.mapControl.ObjShapes.selEle != null)
                {
                    Ele el = this.mapControl.ObjShapes.selEle.Copy();
                    this.ucMitSymbols1.ObjShapes.List.Add(el);
                    this.ucMitSymbols1.redraw(true);
                }
            }
        }
        #endregion

        #region StatusStrip

        public void UpdateStatusStrip(float x, float y)
        {
            //PointF pScreenF = new PointF(x, y);
            //Hiển thị tọa độ Longitude, Latitude
            double lon, lat;
            lon = lat = 0;
            mapControl.ucMap1.Map.ConvertCoord(ref x, ref y, ref lon, ref lat, MapXLib.ConversionConstants.miScreenToMap);
            toolStripStatusLabelCoordinate.Text = string.Format("K:{0} | V:{1}", HTCollectionHelper.DecimalToDMS(lon), HTCollectionHelper.DecimalToDMS(lat));
            toolStripStatusLabelCoordinate2.Text = string.Format("(K:{0} | V:{1})", lon.ToString("#.####0"), lat.ToString("#.####0"));
            txtLongitude.Text = lon.ToString();
            txtLatitude.Text = lat.ToString();


            //Hiển thị tọa độ Screen
            toolStripStatusLabelScreenCoord.Text = string.Format("Screen Coord: ({0},{1})", x, y);
            txtSceenX.Text = x.ToString();
            txtScreenY.Text = y.ToString();


            //Hiển thị tọa độ 3D
            float x3D, y3D;
            x3D = y3D = 0;
            objProjection3D.Convert3DCoord(ref x3D, ref y3D, ref lon, ref lat, Conversion3DConstants.To3D);
            toolStripStatusLabel3DCoord.Text = string.Format("3D Coord: ({0},{1})", x3D.ToString("#,##0.##0"), y3D.ToString("#,##0.##0"));
            txtX3D.Text = x3D.ToString();
            txtY3D.Text = y3D.ToString();
        }


        #endregion
        

        private void LoadFile(string filePath)
        {
            _objDiaHinh = new DiaHinh(filePath);
            mapControl.ucMap1.InitMap(_objDiaHinh._objMyMapGst.FULLPATH_FILE);
            objProjection3D = new Projection3D(_objDiaHinh);
            
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                //mo form cho nguoi ta chon co so du lieu
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "DiaHinh File(*.diahinh)|*.diahinh";
                txtFilePath.Text = "";
                if (Properties.Settings.Default.RecentDiaHinhDirectory != "")
                    openFileDialog1.InitialDirectory = Path.GetDirectoryName(Properties.Settings.Default.RecentDiaHinhDirectory);
                openFileDialog1.ShowDialog();
                txtFilePath.Text = openFileDialog1.FileName;
                Properties.Settings.Default.RecentDiaHinhDirectory = txtFilePath.Text;
                Properties.Settings.Default.Save();
                LoadFile(txtFilePath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.ShowDialog();
        }

        private void btnDrawRegion_Click(object sender, EventArgs e)
        {
            mapControl.ucMap1.Map.CurrentTool = MapXLib.ToolConstants.miArrowTool;
            mapControl.ucMap1.ActiveToolStrip = enumToolStripType.GridRegion;
        }

        private void btnSaveXml_Click(object sender, EventArgs e)
        {
            try
            {
                ActionScript obj = new ActionScript();
                string tempFile = Path.GetDirectoryName(Application.ExecutablePath) + "\\temp.xml";
                txtXmlFile.Text = tempFile;
                toolTip1.SetToolTip(txtXmlFile, tempFile);
                string content = obj.CreateBoomXml(tempFile, objDrawRegion.Point3DList, txtObjName.Text, int.Parse(txtNumOfObjNames.Text), float.Parse(txtStart.Text), float.Parse(txtIncrement.Text), float.Parse(txtDuration.Text), int.Parse(txtMinGroup.Text), int.Parse(txtMaxGroup.Text), checkBoxDanDich.Checked);
                Clipboard.SetText(content);

                MessageBox.Show("Save Successful!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("{0} {1}", ex.Message, ex.StackTrace));
            }
            
        }

        private void toolStripMenuItemCopyLonLatCoord_Click(object sender, EventArgs e)
        {
            string content = string.Format("{0} {1}", txtLongitude.Text, txtLatitude.Text);
            Clipboard.SetText(content);
            //MessageBox.Show("Copy Successful!");
        }

        private void toolStripMenuItemCopyScreenCoord_Click(object sender, EventArgs e)
        {
            string content = string.Format("{0} {1}", txtSceenX.Text, txtScreenY.Text);
            Clipboard.SetText(content);
            //MessageBox.Show("Copy Successful!");
        }

        private void toolStripMenuItemCopy3DCoord_Click(object sender, EventArgs e)
        {
            string content = string.Format("{0} {1}", txtX3D.Text, txtY3D.Text);
            Clipboard.SetText(content);
            //MessageBox.Show("Copy Successful!");
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                mapControl.ucMap1.Map.ZoomTo(mapControl.ucMap1.Map.Zoom, double.Parse(txtLongitude.Text), double.Parse(txtLatitude.Text));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void chkShowGridXYZ_CheckedChanged(object sender, EventArgs e)
        {
            //_objDiaHinh.BindDataGrid_ListView();
        }

        private void btnDemoUcVectShapes_Form_Click(object sender, EventArgs e)
        {
            frmVectShapes_Form frm = new frmVectShapes_Form();
            frm.ShowDialog();
        }
    }
}
