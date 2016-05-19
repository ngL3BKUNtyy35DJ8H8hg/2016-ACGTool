using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MapXLib;
using Point = System.Drawing.Point;
using Rectangle = MapXLib.Rectangle;

namespace HT.VectorShapes.Maps
{
    public enum LAYERTYPE
    {
        POINT,
        POLYLINE,
        POLYON
    }

    public enum STYLEMODE
    {
        STYLESINGLE,
        STYLEMULTI
    }


    public enum STYLESINGLETYPE
    {
        NORMALPOINT,
        NORMALPOINT_RED,
        BITMAPPOINT,
        NORMALCAR,
        NORMALROUTE,
        BITMAPCOMP
    }

    public enum STYLE_Line_TYPE
    {
        HISTORYLINE,
        NORMALLINE
    }

    //public enum enumToolStripType
    //{
    //    ActionToolStrip,
    //    DrawingToolStrip, //ToolStrip dùng vẽ ký hiệu (OLD)
    //    VectorShapeToolStrip, //ToolStrip dùng vẽ ký hiệu (NEW)
    //    GridRegion //ToolStrip vẽ ô lưới để tạo hỏa lực bắn (dùng cho chương trình tạo kịch bản)
    //}

    public enum enumDistance
    {
        Pixel,
        Kilometer
    }

    public partial class ucMap : UserControl
    {
        public double Zoom_Original;
        public double CenterX_Original;
        public double CenterY_Original;

        /// <summary>
        /// Active drawing tool.
        /// </summary>
        public AxMapXLib.AxMap Map
        {
            get
            {
                return axMap1;
            }
        }

        /// <summary>
        /// Khoảng cách zoom hiển thị bản đồ (đơn vị là km)
        /// </summary>
        public double Altitude
        {
            get
            {
                return axMap1.Zoom / 1000;
            }
        }

        public ucVectShapes _ucVectShapes;

        public ucMap()
        {
            InitializeComponent();

            this.axMap1.DrawUserLayer += new AxMapXLib.CMapXEvents_DrawUserLayerEventHandler(this.axMap1_DrawUserLayer);
            this.axMap1.MouseWheelEvent += new AxMapXLib.CMapXEvents_MouseWheelEventHandler(this.axMap1_MouseWheelEvent);
            this.axMap1.MouseDownEvent += new AxMapXLib.CMapXEvents_MouseDownEventHandler(this.axMap1_MouseDownEvent);
            this.axMap1.MouseMoveEvent += new AxMapXLib.CMapXEvents_MouseMoveEventHandler(this.axMap1_MouseMoveEvent);
            this.axMap1.MouseUpEvent += new AxMapXLib.CMapXEvents_MouseUpEventHandler(this.axMap1_MouseUpEvent);
            this.axMap1.MapViewChanged += new System.EventHandler(this.axMap1_MapViewChanged);
            this.axMap1.DblClick += new System.EventHandler(this.axMap1_DblClick);
            this.axMap1.ClickEvent += new System.EventHandler(this.axMap1_ClickEvent);
            this.axMap1.ToolUsed += new AxMapXLib.CMapXEvents_ToolUsedEventHandler(this.axMap1_ToolUsed);

        }

        public void InitMap(string gstMapPath)
        {
            try
            {
                //Cho phép dùng scroll để thu phóng bản đồ
                axMap1.MousewheelSupport = MapXLib.MousewheelSupportConstants.miMousewheelNoAutoScroll;
                //axMap1.MousewheelSupport = MapXLib.MousewheelSupportConstants.miFullMousewheelSupport;//.miNoMousewheelSupport;//.miMousewheelNoAutoScroll;
                //axMap1.GeoSet = string.Format(@"{0}\Maps\BanDo.gst", Application.StartupPath);
                axMap1.GeoSet = gstMapPath;
                axMap1.Title.Visible = false;
                
                
                this.axMap1.Zoom = axMap1.Zoom;
                axMap1.CurrentTool = MapXLib.ToolConstants.miArrowTool;
                //axMap1.NumericCoordSys = axMap1.DisplayCoordSys;
                axMap1.PaperUnit = MapXLib.PaperUnitConstants.miPaperUnitPoint;
                axMap1.MapUnit = MapXLib.MapUnitConstants.miUnitMeter;//.miUnitKilometer;
                axMap1.Layers.AddUserDrawLayer("Symbols", 1);

               
                //axMap1.ZoomTo(axMap1.Zoom, (this.axMap1.Layers._Item("ASIAMAP").Bounds.XMin + this.axMap1.Layers._Item("ASIAMAP").Bounds.XMax) / 2, (this.axMap1.Layers._Item("ASIAMAP").Bounds.YMin + this.axMap1.Layers._Item("ASIAMAP").Bounds.YMax) / 2);
                //axMap1.CreateCustomTool(3, MapXLib.ToolTypeConstants.miToolTypePoint,
                //                            MapXLib.CursorConstants.miArrowQuestionCursor);
            }
            catch (Exception ex)
            {
                
                //throw ex;
            }
           
        }

        /// <summary>
        /// Khởi tạo map với zoom, centerX, centerY mặc định
        /// </summary>
        /// <param name="gstMapPath"></param>
        /// <param name="zoom"></param>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        public void InitMap(string gstMapPath, double zoom, double centerX, double centerY)
        {
            InitMap(gstMapPath);
            //Zoom_Original = zoom;
            //CenterX_Original = centerX;
            //CenterY_Original = centerY;
            //axMap1.ZoomTo(Zoom_Original, CenterX_Original, CenterY_Original);

        }

        public override void Refresh()
        {
            //axMap1.Invalidate(new System.Drawing.Rectangle(0, 0, 500, 500));  
            //axMap1.Layers["Symbols"].Refresh();
            //axMap1.Layers["Symbols"].Invalidate(true);
            axMap1.Refresh();

        }

        public event AxMapXLib.CMapXEvents_MouseDownEventHandler AxMap_MouseDownEvent;
        public event AxMapXLib.CMapXEvents_MouseDownEventHandler AxMapAction_MouseDownEvent;
        public event AxMapXLib.CMapXEvents_MouseDownEventHandler AxMapDrawing_MouseDownEvent;
        public void axMap1_MouseDownEvent(object sender, AxMapXLib.CMapXEvents_MouseDownEvent e)
        {
            if (AxMap_MouseDownEvent != null)
                AxMap_MouseDownEvent(sender, e);
             
            if (Helper.ActionStatus == enumActionStatus.NONE && AxMapAction_MouseDownEvent != null)
                AxMapAction_MouseDownEvent(sender, e);

            if (Helper.ActionStatus == enumActionStatus.DRAWSHAPE && AxMapDrawing_MouseDownEvent != null)
                AxMapDrawing_MouseDownEvent(sender, e);

            //if (Helper.ActionStatus == enumToolStripType.VectorShapeToolStrip)
            //    UserControl_MouseDown(sender, e);
        }

        public event AxMapXLib.CMapXEvents_MouseMoveEventHandler AxMap_MouseMoveEvent;
        public event AxMapXLib.CMapXEvents_MouseMoveEventHandler AxMapAction_MouseMoveEvent;
        public event AxMapXLib.CMapXEvents_MouseMoveEventHandler AxMapDrawing_MouseMoveEvent;
        public void axMap1_MouseMoveEvent(object sender, AxMapXLib.CMapXEvents_MouseMoveEvent e)
        {
            if (AxMap_MouseMoveEvent != null)
                AxMap_MouseMoveEvent(sender, e);

            if (Helper.ActionStatus == enumActionStatus.NONE && AxMapAction_MouseMoveEvent != null)
                AxMapAction_MouseMoveEvent(sender, e);

            if (Helper.ActionStatus == enumActionStatus.DRAWSHAPE && AxMapDrawing_MouseMoveEvent != null)
                AxMapDrawing_MouseMoveEvent(sender, e);

            //if (Helper.ActionStatus == enumToolStripType.VectorShapeToolStrip)
            //    UserControl_MouseMove(sender, e);
        }


        public event AxMapXLib.CMapXEvents_MouseUpEventHandler AxMap_MouseUpEvent;
        public event AxMapXLib.CMapXEvents_MouseUpEventHandler AxMapAction_MouseUpEvent;
        public event AxMapXLib.CMapXEvents_MouseUpEventHandler AxMapDrawing_MouseUpEvent;
        public void axMap1_MouseUpEvent(object sender, AxMapXLib.CMapXEvents_MouseUpEvent e)
        {
            if (AxMap_MouseUpEvent != null)
                AxMap_MouseUpEvent(sender, e);

            if (Helper.ActionStatus == enumActionStatus.NONE && AxMapAction_MouseUpEvent != null)
                AxMapAction_MouseUpEvent(sender, e);

            if (Helper.ActionStatus == enumActionStatus.DRAWSHAPE && AxMapDrawing_MouseUpEvent != null)
                AxMapDrawing_MouseUpEvent(sender, e);

            //if (Helper.ActionStatus == enumToolStripType.VectorShapeToolStrip)
            //    UserControl_MouseUp(sender, e);
        }


        public event AxMapXLib.CMapXEvents_DrawUserLayerEventHandler AxMap_DrawUserLayer;
        public event AxMapXLib.CMapXEvents_DrawUserLayerEventHandler AxMapAction_DrawUserLayer;
        public event AxMapXLib.CMapXEvents_DrawUserLayerEventHandler AxMapDrawing_DrawUserLayer;
        public void axMap1_DrawUserLayer(object sender, AxMapXLib.CMapXEvents_DrawUserLayerEvent e)
        {
            if (AxMap_DrawUserLayer != null)
                AxMap_DrawUserLayer(sender, e);

            if (AxMapAction_DrawUserLayer != null)
                AxMapAction_DrawUserLayer(sender, e);

            if (AxMapDrawing_DrawUserLayer != null)
                AxMapDrawing_DrawUserLayer(sender, e);

            //Graphics myGraphics = Graphics.FromHdc(new IntPtr(e.hOutputDC));

            ////TODO SOMETHING
            //this.redraw(myGraphics, true);

            //myGraphics.Dispose();
        }

        public event AxMapXLib.CMapXEvents_MouseWheelEventHandler AxMap_MouseWheelEvent;
        public event AxMapXLib.CMapXEvents_MouseWheelEventHandler AxMapAction_MouseWheelEvent;
        public event AxMapXLib.CMapXEvents_MouseWheelEventHandler AxMapDrawing_MouseWheelEvent;
        public void axMap1_MouseWheelEvent(object sender, AxMapXLib.CMapXEvents_MouseWheelEvent e)
        {
            if (AxMap_MouseWheelEvent != null)
                AxMap_MouseWheelEvent(sender, e);

            if (Helper.ActionStatus == enumActionStatus.NONE && AxMapAction_MouseWheelEvent != null)
                AxMapAction_MouseWheelEvent(sender, e);

            if (Helper.ActionStatus == enumActionStatus.DRAWSHAPE && AxMapDrawing_MouseWheelEvent != null)
                AxMapDrawing_MouseWheelEvent(sender, e);

            Refresh();
        }


        public event EventHandler AxMap_MapViewChanged;
        public event EventHandler AxMapAction_MapViewChanged;
        public event EventHandler AxMapDrawing_MapViewChanged;
        public void axMap1_MapViewChanged(object sender, EventArgs e)
        {
            if (AxMap_MapViewChanged != null)
                AxMap_MapViewChanged(sender, e);

            if (Helper.ActionStatus == enumActionStatus.NONE && AxMapAction_MapViewChanged != null)
                AxMapAction_MapViewChanged(sender, e);

            if (Helper.ActionStatus == enumActionStatus.DRAWSHAPE && AxMapDrawing_MapViewChanged != null)
                AxMapDrawing_MapViewChanged(sender, e);
        }

        public event EventHandler AxMap_DblClick;
        public event EventHandler AxMapAction_DblClick;
        public event EventHandler AxMapDrawing_DblClick;
        public void axMap1_DblClick(object sender, EventArgs e)
        {
            if (AxMap_DblClick != null)
                AxMap_DblClick(sender, e);

            if (Helper.ActionStatus == enumActionStatus.NONE && AxMapAction_DblClick != null)
                AxMapAction_DblClick(sender, e);

            if (Helper.ActionStatus == enumActionStatus.DRAWSHAPE && AxMapDrawing_DblClick != null)
                AxMapDrawing_DblClick(sender, e);

            //if (Helper.ActionStatus == enumToolStripType.VectorShapeToolStrip)
            //    UserControl_DoubleClick(sender, e);
        }


        private void axMap1_ToolUsed(object sender, AxMapXLib.CMapXEvents_ToolUsedEvent e)
        {
            double X1 = e.x1;
            double X2 = e.x2;
        }

        public event EventHandler AxMap_ClickEvent;
        private void axMap1_ClickEvent(object sender, EventArgs e)
        {
            if (AxMap_ClickEvent != null)
                AxMap_ClickEvent(sender, e);
        }

        #region "Các hàm xử lý thêm Layer và Feature"
        
        public void miToolZoomToPoint(double dx, double dy)
        {
            this.axMap1.ZoomTo(this.axMap1.Zoom, dx, dy);
        }

        /// <summary>
        /// Thêm layer mới
        /// </summary>
        /// <param name="_layerName"></param>
        /// <param name="_layerType"></param>
        public void miAddLayer(string _layerName, LAYERTYPE _layerType)
        {
            try
            {
                MapXLib.Layer lyr;
                MapXLib.LayerInfoClass lyrInfo = new MapXLib.LayerInfoClass();
                MapXLib.FieldsClass flds = new MapXLib.FieldsClass();

                flds.AddStringField("name", 50, 10);

                lyrInfo.Type = MapXLib.LayerInfoTypeConstants.miLayerInfoTypeTemp;
                lyrInfo.AddParameter("name", _layerName);
                lyrInfo.AddParameter("Fields", flds);

                lyr = this.axMap1.Layers.Add(lyrInfo, 1);  //Ô­±¾ÊÇ1
                lyr.Editable = true;
                this.axMap1.Layers.InsertionLayer = lyr;

                switch (_layerType)
                {
                    case LAYERTYPE.POLYLINE:
                        break;
                    case LAYERTYPE.POINT:
                        this.axMap1.Layers.AnimationLayer = lyr;
                        break;
                }

                this.miSetStyleLabel(_layerName);
            }
            catch (Exception ex)
            {
                ;
            }

            
        }

        

        public void miRemoveLayer(string _layerName)
        {
            this.axMap1.Layers.Remove(_layerName);
        }

        /// <summary>
        /// Format layer label
        /// </summary>
        /// <param name="_layerName"></param>
        public void miSetStyleLabel(string _layerName)
        {
            //this.axMap1.Layers._Item(_layerName).LabelProperties.Style.TextFont.Name = "";
            //this.axMap1.Layers._Item(_layerName).LabelProperties.Style.TextFont.Size = 10;
            this.axMap1.Layers._Item(_layerName).LabelProperties.Style.TextFontColor = (uint)MapXLib.ColorConstants.miColorWhite;
            this.axMap1.Layers._Item(_layerName).LabelProperties.Style.TextFontHalo = true;
            this.axMap1.Layers._Item(_layerName).LabelProperties.Style.TextFontBackColor = (uint)MapXLib.ColorConstants.miColorBlack;
            this.axMap1.Layers._Item(_layerName).LabelProperties.Position = MapXLib.PositionConstants.miPositionBR;
            this.axMap1.Layers._Item(_layerName).LabelProperties.Offset = 5;
            this.axMap1.Layers._Item(_layerName).LabelProperties.Overlap = true;

        }


        /// <summary>
        /// Format Line layer
        /// </summary>
        /// <param name="_lyername"></param>
        /// <param name="linetype"></param>
        /// <param name="startx"></param>
        /// <param name="starty"></param>
        /// <param name="endx"></param>
        /// <param name="endy"></param>
        /// <param name="_keystring"></param>
        public void miAddStyleLine(string _lyername, STYLE_Line_TYPE linetype, double startx, double starty, double endx, double endy, string _keystring)
        {
            MapXLib.Point start = new MapXLib.PointClass();//ÐÂ½¨Æðµã
            MapXLib.Point end = new MapXLib.PointClass();  //ÐÂ½¨ÖÕµã
            start.Set(startx, starty);
            end.Set(endx, endy);
            MapXLib.StyleClass newstyle = new StyleClass();
            MapXLib.Feature LineObj;
            MapXLib.Points pts = new MapXLib.PointsClass();
            pts.Add(start, 1);
            pts.Add(end, 2);
            newstyle.LineColor = 227 + 233 * 256 + 256 * 256 * 22;           //r + 256*g + 256*256*b
            //newstyle.LineStyle = (PenStyleConstants)59;
            newstyle.LineStyle = (PenStyleConstants)65;
            newstyle.LineWidth = 2;

            LineObj = axMap1.FeatureFactory.CreateLine(pts, newstyle);    //axMap1.newstyle        
            LineObj.KeyValue = _keystring;
            this.axMap1.Layers._Item(_lyername).AddFeature(LineObj, new MapXLib.RowValuesClass());//Type.Missing);

        }

        /// <summary>
        /// Format Point layer 
        /// </summary>
        /// <param name="_layerName"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="_keyValue"></param>
        /// <param name="pstyle"></param>
        public void miAddStylePoint(string _layerName, double dx, double dy, string _keyValue, STYLESINGLETYPE pstyle)
        {
            MapXLib.FeatureClass fe = null;
            fe = new MapXLib.FeatureClass();
            MapXLib.Point sp = new MapXLib.PointClass();
            sp.Set(dx, dy);
            fe.Attach(this.axMap1.GetOcx());
            fe.Point = sp;
            fe.KeyValue = _keyValue;
            fe.Style = this.miSetStylePoint(STYLEMODE.STYLESINGLE, pstyle);//STYLESINGLETYPE.NORMALPOINT
            this.axMap1.Layers._Item(_layerName).AddFeature(fe, new MapXLib.RowValuesClass());
        }

        /// <summary>
        /// Delete feature
        /// </summary>
        /// <param name="_layername"></param>
        /// <param name="_keyValue"></param>
        public void miDeleteFeature(string _layername, string _keyValue)
        {
            //CMapXFeatures features;
            //CMapXFeature fea;
            MapXLib.Layer lyr;
            lyr = this.axMap1.Layers._Item(_layername);
            MapXLib.Features fts = lyr.AllFeatures;
            try
            {
                //lyr.DeleteFeature(fts._Item(_keyValue));
                foreach (MapXLib.Feature f in fts)
                {
                    if (f.KeyValue == _keyValue)
                    {
                        lyr.DeleteFeature(f);
                        break;
                    }
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }

        }

        /// <summary>
        /// Delete all feature
        /// </summary>
        /// <param name="_layername"></param>
        public void miDeleteAllFeatures(string _layername)
        {
            CMapXFeatures features;
            MapXLib.Layer lyr;
            CMapXFeature fea;
            lyr = this.axMap1.Layers._Item(_layername);
            MapXLib.Features fts = lyr.AllFeatures;
            try
            {
                foreach (MapXLib.Feature f in fts)
                {
                    lyr.DeleteFeature(f);
                    break;
                }
            }
            catch (System.Exception e)
            {
                throw e;
            }

        }

        public MapXLib.Style miSetStylePoint(STYLEMODE _mode, STYLESINGLETYPE _singleType)
        {
            MapXLib.Style _style = new MapXLib.Style();
            if (_mode == STYLEMODE.STYLESINGLE)
            {
                switch (_singleType)
                {
                    //ÆÕÍ¨µã
                    case STYLESINGLETYPE.NORMALPOINT:
                        _style.SymbolType = MapXLib.SymbolTypeConstants.miSymbolTypeVector;
                        _style.SymbolVectorColor = (uint)MapXLib.ColorConstants.miColorGreen;
                        //_style.SymbolVectorColor = (uint)MapXLib.ColorConstants.miColorYellow;
                        _style.SymbolVectorSize = 8;
                        break;
                    case STYLESINGLETYPE.NORMALPOINT_RED:
                        _style.SymbolType = MapXLib.SymbolTypeConstants.miSymbolTypeVector;
                        _style.SymbolVectorColor = (uint)MapXLib.ColorConstants.miColorRed;
                        _style.SymbolVectorSize = 10;
                        break;
                    case STYLESINGLETYPE.BITMAPCOMP:
                        _style.SymbolType = MapXLib.SymbolTypeConstants.miSymbolTypeBitmap;
                        _style.SymbolBitmapName = "completeFlag.bmp";
                        _style.SymbolBitmapSize = 45;
                        _style.SymbolBitmapTransparent = true;
                        break;
                    case STYLESINGLETYPE.BITMAPPOINT:
                        _style.SymbolType = MapXLib.SymbolTypeConstants.miSymbolTypeBitmap;
                        _style.SymbolBitmapName = "boat.bmp";
                        _style.SymbolBitmapSize = 40;
                        _style.SymbolBitmapTransparent = true;
                        break;
                    case STYLESINGLETYPE.NORMALCAR:
                        _style.SymbolType = MapXLib.SymbolTypeConstants.miSymbolTypeTrueTypeFont;
                        stdole.IFontDisp _font = _style.SymbolFont;
                        _font.Name = "Arial";
                        _style.SymbolCharacter = 51;
                        _font.Size = 20;
                        _style.SymbolFontRotation = 360;
                        _style.SymbolFontHalo = true;
                        _style.RegionTransparent = true;
                        _style.SymbolFontColor = (uint)MapXLib.ColorConstants.miColorBlack;
                        _style.SymbolFontBackColor = (uint)MapXLib.ColorConstants.miColorLimeGreen;

                        //_style.SymbolType = MapXLib.SymbolTypeConstants.miSymbolTypeTrueTypeFont;
                        //stdole.IFontDisp _font = _style.SymbolFont;//(stdole.IFontDisp) new stdole.StdFontClass(); //_style.SymbolFont.;
                        //_font.Name = "Times New Roman";
                        //_style.SymbolCharacter = 51;
                        //_font.Size = 20;
                        //_style.SymbolFontRotation = 360;
                        //_style.SymbolFontHalo = true;
                        //_style.RegionTransparent = true;
                        //_style.SymbolFontColor = (uint)MapXLib.ColorConstants.miColorYellow;
                        //_style.SymbolFontBackColor = (uint)MapXLib.ColorConstants.miColorLimeGreen;
                        break;
                    case STYLESINGLETYPE.NORMALROUTE:
                        _style.SymbolType = MapXLib.SymbolTypeConstants.miSymbolTypeVector;
                        _style.SymbolVectorColor = (uint)MapXLib.ColorConstants.miColorYellow;
                        _style.SymbolVectorSize = 1;

                        _style.LineStyle = (MapXLib.PenStyleConstants)1;
                        _style.LineColor = (uint)MapXLib.ColorConstants.miColorNavy;
                        _style.LineWidthUnit = MapXLib.StyleUnitConstants.miStyleUnitPixel;
                        _style.LineWidth = 2;
                        break;
                }
            }
            return _style;
        }

        #region "Cách đo khoảng cách bằng layer"

        //public void addMapXSanBay(string _layername, string _keyValue, MapXLib.Point point)
        //{
        //    MapXLib.Feature circF = new Feature();
        //    MapXLib.StyleClass newstylec = new StyleClass();
        //    newstylec.LineColor = 255 + 125 * 256 + 256 * 256 * 0;           //r + 256*g + 256*256*b
        //    //newstylec.LineStyle = (PenStyleConstants)11;
        //    newstylec.LineWidth = 4;
        //    newstylec.RegionPattern = MapXLib.FillPatternConstants.miPatternNoFill;// (miPatternNoFill);
        //    //newstylec.RegionTransparent = true;
        //    newstylec.SymbolType = MapXLib.SymbolTypeConstants.miSymbolTypeTrueTypeFont;
        //    //newstylec.TextFont.Name = ".VnArial";
        //    stdole.IFontDisp _font = (stdole.IFontDisp)newstylec.SymbolFont;
        //    _font.Name = ".VnArial";
        //    newstylec.SymbolCharacter = 51;
        //    //_font.Size = 40;
        //    //newstylec.SymbolFontRotation = 360;
        //    newstylec.SymbolFontHalo = true;
        //    //newstylec.SymbolFontColor = (uint)MapXLib.ColorConstants.miColorYellow;
        //    //newstylec.SymbolFontBackColor = (uint)MapXLib.ColorConstants.miColorLimeGreen;

        //    circF = this.axMap1.FeatureFactory.CreateCircularRegion(MapXLib.CircleTypeConstants.miCircleTypeMap, point, 500, MapXLib.MapUnitConstants.miUnitMeter, 200, newstylec);
        //    circF.KeyValue = _keyValue;
        //    this.axMap1.Layers._Item(_layername).AddFeature(circF, new MapXLib.RowValuesClass());
        //}

        public void addMapxGenLine(string _layername, string KeyValue, double p1x, double p1y, double p2x, double p2y)
        {
            MapXLib.Point start = new MapXLib.PointClass();//ÐÂ½¨Æðµã
            MapXLib.Point end = new MapXLib.PointClass();//ÐÂ½¨ÖÕµã
            start.Set(p1x, p1y);
            end.Set(p2x, p2y);
            MapXLib.StyleClass newstyle = new StyleClass();
            MapXLib.Feature LineObj;
            MapXLib.Points pts = new MapXLib.PointsClass();
            pts.Add(start, 1);
            pts.Add(end, 2);
            newstyle.LineColor = 255 + 125 * 256 + 256 * 256 * 0;           //r + 256*g + 256*256*b

            newstyle.LineStyle = (PenStyleConstants)11;
            newstyle.LineWidth = 2;

            LineObj = axMap1.FeatureFactory.CreateLine(pts, newstyle);    //eagleMap.newstyle        
            LineObj.KeyValue = KeyValue;
            this.axMap1.Layers._Item(_layername).AddFeature(LineObj, new MapXLib.RowValuesClass());//Type.Missing);

        }

        /// <summary>
        /// Delete feature
        /// </summary>
        /// <param name="_layername"></param>
        /// <param name="KeyValue"></param>
        public void delMapxGenFeature(string _layername, string KeyValue)
        {
            //CMapXFeatures features;
            MapXLib.Layer lyr;
            //CMapXFeature fea;
            lyr = this.axMap1.Layers._Item(_layername);
            MapXLib.Features fts = lyr.AllFeatures;
            try
            {
                foreach (MapXLib.Feature f in fts)
                {
                    if (f.KeyValue == KeyValue)
                    {
                        lyr.DeleteFeature(f);
                        break;
                    }
                }
            }
            catch (System.Exception e)
            {
                ;
            }

        }

        public void miAddGeneralPoint(string _layerName, double dx, double dy, string _keyValue)
        {
            MapXLib.FeatureClass fe = null;
            fe = new MapXLib.FeatureClass();
            MapXLib.Point sp = new MapXLib.PointClass();
            sp.Set(dx, dy);
            fe.Attach(this.axMap1.GetOcx());
            fe.Point = sp;
            fe.KeyValue = _keyValue;
            fe.Style = this.miSetStylePoint(STYLEMODE.STYLESINGLE, STYLESINGLETYPE.NORMALPOINT);
            this.axMap1.Layers._Item(_layerName).AddFeature(fe, new MapXLib.RowValuesClass());
        }

        #endregion

        #endregion
    }
}
