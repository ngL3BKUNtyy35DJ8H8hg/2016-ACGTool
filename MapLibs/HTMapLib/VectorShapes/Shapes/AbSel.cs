using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace HTMapLib.VectorShapes.Shapes
{
    /// <summary>
    /// 
    /// Handle tool for redim/move/rotate shapes
    /// </summary>
    [Serializable]
    public class AbSel : Ele
    {
        public AbSel(Ele EL)
        {
            this.X = EL.getX();
            this.Y = EL.getY();
            this.X1 = EL.getX1();
            this.Y1 = EL.getY1();
            this.Selected = false;
            this.endMoveRedim();
            this.rot = EL.canRotate();
            this._rotation = EL.getRotation();
        }

        public void showHandles(bool i)
        {
            this.IamGroup = i;
        }

        /// <summary>
        /// Su quale maniglia cade il punto x,y? 
        /// </summary>
        public string isOver(int x, int y)
        {
            if (this.contains(x, y))
                return "C";
            return "NO";
        }

        public override void Select()
        {
            this.undoEle = this.Copy();
        }

        public override void Draw(Graphics g, int dx, int dy, float zoom)
        {
            System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            myBrush.Color = this.Trasparency(Color.Black, 80);

            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Blue, 1.5f);
            myPen.DashStyle = DashStyle.Dash;

            System.Drawing.Pen whitePen = new System.Drawing.Pen(System.Drawing.Color.White);

            g.DrawRectangle(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);

            myPen.Dispose();
            myBrush.Dispose();
        }


    }
}
