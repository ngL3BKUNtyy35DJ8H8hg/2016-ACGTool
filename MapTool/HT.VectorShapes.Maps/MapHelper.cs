using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using HT.VectorShapes.Maps;

namespace HT.VectorShapes.Maps
{
    public static class MapHelper
    {
        public static bool IsDrawing = false;


        /// <summary>
        /// Tính khoảng cách tọa độ
        /// </summary>
        /// <param name="pointForm"></param>
        /// <param name="pointTo"></param>
        /// <param name="eDistance"></param>
        /// <returns></returns>
        public static double CalculateDistance(AxMapXLib.AxMap axMap1, PointF pointForm, PointF pointTo, enumDistance eDistance)
        {
            double distance = 0;
            float x1, y1, x2, y2;
            x1 = pointForm.X;
            y1 = pointForm.Y;
            x2 = pointTo.X;
            y2 = pointTo.Y;
            //Nếu cần tính khoảng cách theo kilomet
            if (eDistance == enumDistance.Kilometer)
            {
                double lon1, lat1, lon2, lat2;
                lon1 = lat1 = lon2 = lat2 = 0;
                //axMap1.ConvertCoord(ref pointForm.X, ref pointForm.Y, ref lon1, ref lat1, MapXLib.ConversionConstants.miScreenToMap);
                //axMap1.ConvertCoord(ref pointTo.X, ref pointTo.Y, ref lon2, ref lat2, MapXLib.ConversionConstants.miMapToScreen);
                axMap1.ConvertCoord(ref x1, ref y1, ref lon1, ref lat1, MapXLib.ConversionConstants.miScreenToMap);
                axMap1.ConvertCoord(ref x2, ref y2, ref lon2, ref lat2, MapXLib.ConversionConstants.miScreenToMap);
                distance = axMap1.Distance(lon1, lat1, lon2, lat2) / 1000;
            }
            else //Nếu tính khoảng cách theo pixel
            {
                distance = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            }
            return distance;
        }

        /// <summary>
        /// Tính khoảng cách giữa 2 điểm theo km
        /// </summary>
        /// <param name="axMap1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat1"></param>
        /// <param name="lon2"></param>
        /// <param name="lat2"></param>
        /// <returns></returns>
        public static double CalculateDistance(AxMapXLib.AxMap axMap1, double lon1, double lat1, double lon2, double lat2)
        {
            double distance = axMap1.Distance(lon1, lat1, lon2, lat2) / 1000;
            return distance;
        }
    }
}
