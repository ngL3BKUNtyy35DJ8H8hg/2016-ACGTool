using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace HT.VectorShapes.Shapes
{
    /// <summary>
    /// A set of color point for Path Gradient Path management 
    /// </summary>
    [Serializable]
    public class PointColorSet : PointSet
    {

        public PointColorSet(int x, int y, int x1, int y1, ArrayList a) :
            base(x, y, x1, y1, a)
        { }

        public void dbl_Click()
        {
            //base.Select();
            //this.undoEle = this.Copy();
        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {

            //myBrush.Color = this.Trasparency(this.fillColor, this.alpha);
            System.Drawing.Pen myPen = new System.Drawing.Pen(this.penColor, scaledPenWidth(zoom));
            myPen.DashStyle = this.dashStyle;

            if (this.Selected)
            {
                //myBrush.Color = this.dark(this.fillColor, 5,this.alpha);
                myPen.Color = Color.Red;
                myPen.Color = this.Trasparency(myPen.Color, 120);
                myPen.Width = myPen.Width + 1;

            }

            // Create a path and add the object.
            GraphicsPath myPath = new GraphicsPath();



            // To ARRAY
            PointF[] myArr = new PointF[this.points.Count];
            Color[] myColorArr = new Color[this.points.Count];
            int i = 0;
            foreach (PointWr p in this.points)
            {
                myArr[i] = new PointF((p.X + this.X + dx) * zoom, (p.Y + this.Y + dy) * zoom);// p.point;
                if (p is PointColor)
                    myColorArr[i++] = ((PointColor)p).col;
            }

            if (myArr.Length < 3 | !this.Curved)
            {
                if (Closed & myArr.Length >= 3)
                    myPath.AddPolygon(myArr);
                else
                    myPath.AddLines(myArr);
            }
            else
            {
                if (Closed)
                    myPath.AddClosedCurve(myArr);
                else
                    myPath.AddCurve(myArr);
            }

            //PGB
            PathGradientBrush myBrush = new PathGradientBrush(myPath);
            myBrush.SurroundColors = myColorArr;
            myBrush.CenterColor = this.fillColor;



            Matrix translateMatrix = new Matrix();
            translateMatrix.RotateAt(this.Rotation, new PointF((this.X + dx + (int)(this.X1 - this.X) / 2) * zoom, (this.Y + dy + (int)(this.Y1 - this.Y) / 2) * zoom));
            myPath.Transform(translateMatrix);

            // Draw the transformed obj to the screen.
            g.FillPath(myBrush, myPath);
            if (this.showBorder)
                g.DrawPath(myPen, myPath);

            myPath.Dispose();
            myPen.Dispose();
            if (myBrush != null)
                myBrush.Dispose();
        }




    }
}
