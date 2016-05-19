using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace HT.VectorShapes.Shapes
{
    //TEST
    //Point Color: used to store and update points in arraylist of gradient brush
    [Serializable]
    public class PointColor : PointWr
    {
        public Color col { get; set; }
        public PointColor(Point pp) :
            base(pp)
        {
        }

        public PointColor(int x, int y) :
            base(x, y)
        {
        }

        public PointColor copy()
        {
            return new PointColor(this.X, this.Y);
        }

    }
}
