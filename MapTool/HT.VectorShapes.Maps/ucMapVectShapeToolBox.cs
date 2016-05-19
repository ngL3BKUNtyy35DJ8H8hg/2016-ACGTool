using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HT.VectorShapes.Maps
{
    public partial class ucMapVectShapeToolBox : ucVectShapeToolBox
    {
        private ucVectShapes_Map s;

        public ucMapVectShapeToolBox()
        {
            InitializeComponent();
        }

        public void Init(ucVectShapes_Map uc)
        {
            s = uc;
            s.ucMap1.AxMapDrawing_MouseDownEvent += new AxMapXLib.CMapXEvents_MouseDownEventHandler(AxMapDrawing_MouseDownEvent);
            s.ucMap1.AxMapDrawing_MouseMoveEvent += new AxMapXLib.CMapXEvents_MouseMoveEventHandler(AxMapDrawing_MouseMoveEvent);
            s.ucMap1.AxMapDrawing_MouseUpEvent += new AxMapXLib.CMapXEvents_MouseUpEventHandler(AxMapDrawing_MouseUpEvent);
            s.ucMap1.AxMapDrawing_DrawUserLayer += new AxMapXLib.CMapXEvents_DrawUserLayerEventHandler(AxMapDrawing_DrawUserLayer);
            //s.ucMap1.AxMapDrawing_MouseWheelEvent += new AxMapXLib.CMapXEvents_MouseWheelEventHandler(AxMapDrawing_MouseWheelEvent);
        }

        private void AxMapDrawing_MouseDownEvent(object sender, AxMapXLib.CMapXEvents_MouseDownEvent e)
        {
            if (e.button == 1)
            {
                double mapX, mapY;
                mapX = mapY = 0;
                s.ucMap1.Map.ConvertCoord(ref e.x, ref e.y, ref mapX, ref mapY, MapXLib.ConversionConstants.miScreenToMap);
                if (e.button == 1)
                {
                 
                }
            }
            else if (e.button == 2)
            {
                //Nếu đang ở kéo bản đồ thì chuyển sang mũi tên
                if (s.ucMap1.Map.CurrentTool == MapXLib.ToolConstants.miPanTool)
                {
                    s.ucMap1.Map.CurrentTool = MapXLib.ToolConstants.miArrowTool;
                }
            }

        }

        private void AxMapDrawing_MouseMoveEvent(object sender, AxMapXLib.CMapXEvents_MouseMoveEvent e)
        {

        }

        private void AxMapDrawing_MouseUpEvent(object sender, AxMapXLib.CMapXEvents_MouseUpEvent e)
        {

        }

        private void AxMapDrawing_DrawUserLayer(object sender, AxMapXLib.CMapXEvents_DrawUserLayerEvent e)
        {

        }
    }
}
