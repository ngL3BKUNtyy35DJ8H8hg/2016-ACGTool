using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BDTCLib
{
    public enum Conversion3DConstants
    {
        To3D,
        ToMap
    };

    public class Projection3D
    {
        //Length of XYZ file
        private double Width3D = 130;
        private double Height3D = 100;
        
        //Center is South West (SW)
        private double CenterMapX = 107.11f;
        private double CenterMapY = 16.65f;

        //Right Lon - Left Lon 
        private double WidthMap = 107.24f - 107.11f;
        //Top Lat - Bottom Lat
        private double HeightMap = 16.75f - 16.65f;

        public Projection3D(DiaHinh objDiaHinh)
        {
            Width3D = double.Parse(objDiaHinh._myGRID_WIDTH);
            Height3D = double.Parse(objDiaHinh._myGRID_HEIGHT);

            CenterMapX = double.Parse(objDiaHinh._myMap2X);
            CenterMapY = double.Parse(objDiaHinh._myMap2Y);

            WidthMap = double.Parse(objDiaHinh._myMap1X) - double.Parse(objDiaHinh._myMap2X);
            HeightMap = double.Parse(objDiaHinh._myMap1Y) - double.Parse(objDiaHinh._myMap2Y);
        }

        public void Convert3DCoord(ref float x, ref float y, ref double mapX, ref double mapY, Conversion3DConstants type)
        {
            if (type == Conversion3DConstants.To3D)
            {
                x = (float)((mapX - CenterMapX) * Width3D / WidthMap);
                y = (float)((mapY - CenterMapY) * Height3D / HeightMap);
            }
            else
            {
                mapX = (float)(x * WidthMap / Width3D);
                mapY = (float)(y * HeightMap / Height3D);
            }
        }
    }
}
