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
    public class SelRectBK : Ele
    {
        public SelRectBK(Ele EL)
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
            Rectangle r;
            r = new Rectangle(this.X - 2, this.Y - 2, 5, 5);
            if (!this.AmIaGroup())
            {
                //NW
                if (r.Contains(x, y))
                    return "NW";
                //SE
                r.X = this.X1 - 2;
                r.Y = this.Y1 - 2;
                if (r.Contains(x, y))
                    return "SE";
            }
            if (!sonoUnaLinea)
            {
                //N
                r.X = this.X - 2 + (this.X1 - this.X) / 2;
                r.Y = this.Y - 2;
                if (r.Contains(x, y))
                    return "N";
                if (rot)
                {
                    //ROT
                    float midX, midY = 0;
                    midX = (this.X1 - this.X) / 2;
                    midY = (this.Y1 - this.Y) / 2;
                    PointF Hp = new PointF(0, -25);
                    PointF RotHP = this.rotatePoint(Hp, this._rotation);
                    midX += RotHP.X;
                    midY += RotHP.Y;

                    r.X = this.X + (int)midX - 2;
                    r.Y = this.Y + (int)midY - 2;
                    if (r.Contains(x, y))
                        return "ROT";
                }
                if (!this.AmIaGroup())
                {
                    //NE
                    r.X = this.X1 - 2;
                    r.Y = this.Y - 2;
                    if (r.Contains(x, y))
                        return "NE";
                }
                //E
                r.X = this.X1 - 2;
                r.Y = this.Y - 2 + (this.Y1 - this.Y) / 2;
                if (r.Contains(x, y))
                    return "E";
                //S
                r.X = this.X - 2 + (this.X1 - this.X) / 2;
                r.Y = this.Y1 - 2;
                if (r.Contains(x, y))
                    return "S";
                if (!this.AmIaGroup())
                {

                    //SW
                    r.X = this.X - 2;
                    r.Y = this.Y1 - 2;
                    if (r.Contains(x, y))
                        return "SW";
                }
                //W
                r.X = this.X - 2;
                r.Y = this.Y - 2 + (this.Y1 - this.Y) / 2;
                if (r.Contains(x, y))
                    return "W";
            }

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
            myBrush.Color = this.Trasparency(Color.Black, 90);

            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Blue, 1.5f);
            myPen.DashStyle = DashStyle.Dash;

            System.Drawing.Pen whitePen = new System.Drawing.Pen(System.Drawing.Color.White);
            if (!this.AmIaGroup())
            {
                //NW
                g.FillRectangle(myBrush, new RectangleF((int)((this.X + dx - 2)) * zoom, (int)((this.Y + dy - 2)) * zoom, (int)5 * zoom, (int)5 * zoom));
                g.DrawRectangle(whitePen, (this.X + dx - 2) * zoom, (this.Y + dy - 2) * zoom, 5 * zoom, 5 * zoom);
                //SE
                g.FillRectangle(myBrush, new RectangleF((this.X1 + dx - 2) * zoom, (this.Y1 + dy - 2) * zoom, (5) * zoom, (5) * zoom));
                g.DrawRectangle(whitePen, (this.X1 + dx - 2) * zoom, (this.Y1 + dy - 2) * zoom, (5) * zoom, (5) * zoom);
            }
            if (!sonoUnaLinea)
            {
                g.DrawRectangle(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
                //N
                g.FillRectangle(myBrush, (this.X + dx - 2 + (this.X1 - this.X) / 2) * zoom, (this.Y + dy - 2) * zoom, 5 * zoom, 5 * zoom);
                g.DrawRectangle(whitePen, (this.X + dx - 2 + (this.X1 - this.X) / 2) * zoom, (this.Y + dy - 2) * zoom, 5 * zoom, 5 * zoom);
                if (!this.AmIaGroup())
                {
                    //NE
                    g.FillRectangle(myBrush, (this.X1 + dx - 2) * zoom, (this.Y + dy - 2) * zoom, 5 * zoom, 5 * zoom);
                    g.DrawRectangle(whitePen, (this.X1 + dx - 2) * zoom, (this.Y + dy - 2) * zoom, 5 * zoom, 5 * zoom);
                }
                //E
                g.FillRectangle(myBrush, (this.X1 + dx - 2) * zoom, (this.Y + dy - 2 + (this.Y1 - this.Y) / 2) * zoom, 5 * zoom, 5 * zoom);
                g.DrawRectangle(whitePen, (this.X1 + dx - 2) * zoom, (this.Y + dy - 2 + (this.Y1 - this.Y) / 2) * zoom, 5 * zoom, 5 * zoom);
                //S
                g.FillRectangle(myBrush, (this.X + dx - 2 + (this.X1 - this.X) / 2) * zoom, (this.Y1 + dy - 2) * zoom, 5 * zoom, 5 * zoom);
                g.DrawRectangle(whitePen, (this.X + dx - 2 + (this.X1 - this.X) / 2) * zoom, (this.Y1 + dy - 2) * zoom, 5 * zoom, 5 * zoom);
                if (!this.AmIaGroup())
                {
                    //SW
                    g.FillRectangle(myBrush, (this.X + dx - 2) * zoom, (this.Y1 + dy - 2) * zoom, 5 * zoom, 5 * zoom);
                    g.DrawRectangle(whitePen, (this.X + dx - 2) * zoom, (this.Y1 + dy - 2) * zoom, 5 * zoom, 5 * zoom);
                }
                //W
                g.FillRectangle(myBrush, (this.X + dx - 2) * zoom, (this.Y + dy - 2 + (this.Y1 - this.Y) / 2) * zoom, 5 * zoom, 5 * zoom);
                g.DrawRectangle(whitePen, (this.X + dx - 2) * zoom, (this.Y + dy - 2 + (this.Y1 - this.Y) / 2) * zoom, 5 * zoom, 5 * zoom);

                //TEST                
                if (rot)
                {
                    //C
                    float midX, midY = 0;
                    midX = (this.X1 - this.X) / 2;
                    midY = (this.Y1 - this.Y) / 2;
                    g.FillEllipse(myBrush, (this.X + midX + dx - 3) * zoom, (this.Y + dy - 3 + midY) * zoom, 6 * zoom, 6 * zoom);
                    g.DrawEllipse(whitePen, (this.X + midX + dx - 3) * zoom, (this.Y + dy - 3 + midY) * zoom, 6 * zoom, 6 * zoom);
                    // ROT
                    PointF Hp = new PointF(0, -25);
                    PointF RotHP = this.rotatePoint(Hp, this._rotation);
                    RotHP.X += midX;
                    RotHP.Y += midY;
                    g.FillRectangle(myBrush, (this.X + RotHP.X + dx - 3) * zoom, (this.Y + dy - 3 + RotHP.Y) * zoom, 6 * zoom, 6 * zoom);
                    g.DrawRectangle(whitePen, (this.X + RotHP.X + dx - 3) * zoom, (this.Y + dy - 3 + RotHP.Y) * zoom, 6 * zoom, 6 * zoom);
                    g.DrawLine(myPen, (this.X + midX + dx) * zoom, (this.Y + midY + dy) * zoom, (this.X + RotHP.X + dx) * zoom, (this.Y + RotHP.Y + dy) * zoom);
                }
            }
            else
            {
                g.DrawLine(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 + dx) * zoom, (this.Y1 + dy) * zoom);
            }


            // else
            // {
            //     g.DrawRectangle(myPen, (this.X + dx) * zoom, (this.Y + dy) * zoom, (this.X1 - this.X) * zoom, (this.Y1 - this.Y) * zoom);
            // }
            myPen.Dispose();
            myBrush.Dispose();
        }


    }
}
