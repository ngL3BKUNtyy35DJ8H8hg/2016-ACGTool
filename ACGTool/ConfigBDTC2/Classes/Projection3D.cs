using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConfigBDTC.Classes
{
    public enum Conversion3DConstants
    {
        To3D,
        ToMap
    };

    public static class Projection3D
    {
        public static double Width3D = 130;
        public static double Height3D = 100;
        //center is South West (SW)
        public static double CenterMapX = 107.11f;
        public static double CenterMapY = 16.65f;
        public static double WidthMap = 107.24f - 107.11f;
        public static double HeightMap = 16.75f - 16.65f;

        public static void Convert3DCoord(ref float x, ref float y, ref double mapX, ref double mapY, Conversion3DConstants type)
        {
            double center3DX = 0;
            double center3DY = 0;
            
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
