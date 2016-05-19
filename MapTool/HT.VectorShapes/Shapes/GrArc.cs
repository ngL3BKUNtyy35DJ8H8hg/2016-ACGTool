using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HT.VectorShapes.Shapes
{
    //Arcs : used in graph
    [Serializable]
    public class GrArc
    {
        public PointWr start;
        public PointWr end;

        public GrArc(PointWr s, PointWr e)
        {
            start = s;
            end = e;
        }

        public Point getStartPoint()
        {
            return start.point;
        }

        public Point getEndPoint()
        {
            return end.point;
        }


    }
}
