using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConfigBDTC.Classes
{
    public abstract class DrawObject
    {
        protected AxMapXLib.AxMap AxMap1;
        
        // Object properties
        //private bool selected;
        private Color color;
        private int penWidth;

        // Last used property values (may be kept in the Registry)
        private static Color lastUsedColor = Color.Black;
        private static int lastUsedPenWidth = 1;

        /// <summary>
        /// Color
        /// </summary>
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }

        /// <summary>
        /// Pen width
        /// </summary>
        public int PenWidth
        {
            get
            {
                return penWidth;
            }
            set
            {
                penWidth = value;
            }
        }

        /// <summary>
        /// Last used pen width
        /// </summary>
        public static int LastUsedPenWidth
        {
            get
            {
                return lastUsedPenWidth;
            }
            set
            {
                lastUsedPenWidth = value;
            }
        }

        /// <summary>
        /// Initialization
        /// </summary>
        protected void Initialize()
        {
            color = lastUsedColor;
            penWidth = LastUsedPenWidth;
        }

        //public virtual void Drawing(Graphics g, Rectangle rect)
        //{
            
        //}

        public virtual void Draw(Graphics g)
        {

        }

        /// <summary>
        /// Move handle to the point
        /// </summary>
        /// <param name="point"></param>
        /// <param name="handleNumber"></param>
        public virtual void MoveHandleTo(Point point, int handleNumber)
        {

        }
    }

    
}
