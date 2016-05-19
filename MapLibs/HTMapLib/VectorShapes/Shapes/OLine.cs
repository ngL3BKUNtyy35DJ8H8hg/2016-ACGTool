using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Text;

namespace HTMapLib.VectorShapes.Shapes
{
    /// <summary>
    /// OLinea //TEST!!!
    /// </summary>
    [Serializable]
    public class OLine : Linea
    {
        public OLine(int x, int y, int x1, int y1)
        {
            this.X = x;
            this.Y = y;
            this.X1 = x1;
            this.Y1 = y;
            this.sonoUnaLinea = true;
            this.Selected = true;
            this.starCap = LineCap.Custom;
            this.endCap = LineCap.Custom;
            this.endMoveRedim();
            this.rot = false; //can rotate?

        }

        //TEST
        public override void redim(int x, int y, string redimSt)
        {
            switch (redimSt)
            {
                case "NW":
                    this.X = this.startX + x;
                    //this.X1 = this.startX1 + x;
                    //this.Y = this.startY + y;
                    break;
                case "SE":
                    //this.X = this.startX + x;
                    this.X1 = this.startX1 + x;
                    //this.Y1 = this.startY1 + y;
                    break;
                default:
                    break;
            }

            if (!this.sonoUnaLinea)
            {   // manage redim limits
                if (this.X1 <= this.X)
                    this.X1 = this.X + 10;
                if (this.Y1 <= this.Y)
                    this.Y1 = this.Y + 10;
            }

        }


    }
}
