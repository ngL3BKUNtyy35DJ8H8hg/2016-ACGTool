using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using MapXLib;
using Rectangle = System.Drawing.Rectangle;

namespace ConfigBDTC.Classes
{
    public class DrawRegion : DrawRectangle
    {
        private int numOfBooms;
        private int numOfRows;
        private int numOfColumns;
        
        
        private float dX = 0;
        private float dY = 0;

        private List<int> indexList;

        public List<PointF> Point3DList;

        public DrawRegion(AxMapXLib.AxMap map, int boom, int row, int col)
        {
            numOfRows = row;
            numOfColumns = col;
            numOfBooms = boom;
            indexList = GetRandom(numOfBooms, numOfRows * numOfColumns);
            AxMap1 = map;

        }

        ///// <summary>
        ///// Draw rectangle
        ///// </summary>
        ///// <param name="g"></param>
        //public override void Drawing(Graphics g, Rectangle rect)
        //{
        //    Pen pen = new Pen(Color, PenWidth);
        //    g.DrawRectangle(pen, rect);
            
        //    //Draw grid
        //    DrawGrid(g, rect, pen);

        //    pen.Dispose();
        //}

        /// <summary>
        /// Draw rectangle
        /// </summary>
        /// <param name="g"></param>
        public override void Draw(Graphics g)
        {
            Pen pen = new Pen(Color, PenWidth);

            g.DrawRectangle(pen, Rectangle);

            //Draw grid
            DrawGrid(g, Rectangle, pen);

            pen.Dispose();
        }

        
        /// <summary>
        /// Draw grid
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rect"></param>
        /// <param name="pen"></param>
        private void DrawGrid(Graphics g, Rectangle rect, Pen pen)
        {
            dX = (float)rect.Size.Width / numOfColumns;
            dY = (float)rect.Size.Height / numOfRows;

            Point3DList = new List<PointF>();
            foreach(int i in indexList)
                FillCell(g, rect, i);

            for (int row = 0; row < numOfRows; row++)
            {
                g.DrawLine(pen, rect.X, rect.Y + dY * row, rect.X + rect.Size.Width, rect.Y + dY * row);
            }

            for (int col = 0; col < numOfColumns; col++)
            {
                g.DrawLine(pen, rect.X + dX * col, rect.Y, rect.X + dX * col, rect.Y + rect.Size.Height);
            }
        }

        private List<int> GetRandom(int num, int size)
        {
            List<int> indexList = new List<int>();
            Random random = new Random();

            while (indexList.Count < num)
            {
                int randomNumber = random.Next(1, size+1);
                if (!indexList.Contains(randomNumber))
                    indexList.Add(randomNumber);
            }

            return indexList;
        }

        /// <summary>
        /// Fill color a cell in a grid
        /// </summary>
        /// <param name="indexCell"></param>
        private void FillCell(Graphics g, Rectangle rect, int indexCell)
        {
            int val = indexCell%numOfColumns;
            float x = (val > 0 ? val : numOfColumns) - 1;
            float y = indexCell/numOfColumns;
            if (val == 0)
                y -= 1;

            PointF p = new PointF(rect.X + dX*x, rect.Y + dY*y);
            SizeF size = new SizeF(dX, dY);

            RectangleF rectCell = new RectangleF(p, size);

            //Convert to 3D coordinate and add into a list
            x = p.X + dX/2;
            y = p.Y + dY/2;
            double mapX, mapY;
            mapX = mapY = 0;
            AxMap1.ConvertCoord(ref x, ref y, ref mapX, ref mapY, ConversionConstants.miScreenToMap);
            Projection3D.Convert3DCoord(ref x, ref y, ref mapX, ref mapY, Conversion3DConstants.To3D);
            PointF p3D = new PointF(x, y);
            Point3DList.Add(p3D);

            //Fill rect
            g.FillRectangle(Brushes.Yellow, rectCell);
            
        }
    }
}
