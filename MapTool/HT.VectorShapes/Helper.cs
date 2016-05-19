using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HT.VectorShapes
{
    /// <summary>
    /// Enum cho biết tool chọn tương tác là gì
    /// </summary>
    public enum enumActionStatus
    {
        NONE,           //Không chọn gì cả (nếu liên quan đến map thì là chọn tương tác Map)
        SELSHAPE,       //Đang trạng thái chọn các shape trên bản đồ (click vào button cho phép chọn shape)
        DRAWSHAPE,      //Đang trạng thái vẽ mới shape (click vào button chọn hình cần vẽ: line, rectangel, ellipse, circle, ...)
        REDIMSHAPE      //Đang trạng thái redim shape đang chọn (sau khi chọn được một đối tượng trên bản đồ thì cho phép thực hiện transform đối tượng đó)
    }

    /// <summary>
    /// Enum thực hiện vẽ ký hiệu
    /// </summary>
    public enum enumDrawingOption
    {
        NONE,
        DR,
        LI,
        ELL,
        DRR,
        IB,
        ARC,
        POLY,
        STB,
        TB,
        PEN,
        GRAPH,
        NEWPOLY
    }

    /// <summary>
    /// Enum thực hiện redim ký hiệu
    /// </summary>
    public enum enumRedimOption
    {
        NONE, //None
        NEWP,
        POLY,
        GRAPH,
        ROT,
        C,
        ZOOM,
        NW,
        N,
        NE,
        E,
        SE,
        S,
        SW,
        W
    }

    /// <summary>
    /// Enum thực hiện tương tác bản đồ
    /// </summary>
    public enum enumMapOption
    {
        NONE,
        ARROW,
        ZOOMIN,
        ZOOMOUT,
        PAN,
        ALLLAYERS,
        DISTANCE
    }

    public static class Helper
    {
        /// <summary>
        /// Cho biết hành động thực hiện: thao tác bản đồ, chọn đối tượng, vẽ đối tượng, transform đối tượng
        /// </summary>
        public static enumActionStatus ActionStatus;

        /// <summary>
        /// Cho biết đang ở trạng thái lựa chọn vẽ ký hiệu nào (line, ret, ellipse, ...
        /// </summary>
        public static enumDrawingOption DrawStatus;

        /// <summary>
        /// Cho biết trạng thái đang lựa chọn redim nào
        /// </summary>
        public static enumRedimOption RedimOption = enumRedimOption.NONE;

        /// <summary>
        /// Cho biết trạng thái đang lựa chọn thao tác trên bản đồ nào
        /// </summary>
        public static enumMapOption MapOption = enumMapOption.NONE;


        /// <summary>
        /// Reset Status khi click vào các toolbox khác nhau
        /// </summary>
        /// <param name="action"></param>
        public static void ResetStatus(enumActionStatus action)
        {
            Helper.ActionStatus = action;

            switch (action)
            {
                //Nếu chọn tool tương tác bản đồ thì Drawing và Redim là NONE
                case enumActionStatus.NONE:
                    DrawStatus = enumDrawingOption.NONE;
                    RedimOption = enumRedimOption.NONE;
                    break;
                //Nếu chọn tool focus ký hiệu
                case enumActionStatus.SELSHAPE:
                    MapOption = enumMapOption.NONE;
                    DrawStatus = enumDrawingOption.NONE;
                    RedimOption = enumRedimOption.NONE;
                    break;
                case enumActionStatus.DRAWSHAPE:
                    MapOption = enumMapOption.NONE;
                    RedimOption = enumRedimOption.NONE;
                    break;
                case enumActionStatus.REDIMSHAPE:
                    MapOption = enumMapOption.NONE;
                    DrawStatus = enumDrawingOption.NONE;
                    break;
            }
        }

        /// <summary>
        /// Reset Status khi chọn chức năng draw ký hiệu
        /// </summary>
        /// <param name="action"></param>
        public static void ResetStatus(enumDrawingOption action)
        {
            DrawStatus = action;

            ActionStatus = enumActionStatus.DRAWSHAPE;
            MapOption = enumMapOption.NONE;
            RedimOption = enumRedimOption.NONE;
        }

        /// <summary>
        /// Reset Status khi chọn chức năng redim ký hiệu
        /// </summary>
        /// <param name="action"></param>
        public static void ResetStatus(enumRedimOption action)
        {
            RedimOption = action;

            ActionStatus = enumActionStatus.REDIMSHAPE;
            MapOption = enumMapOption.NONE;
            DrawStatus = enumDrawingOption.NONE;
        }

        /// <summary>
        /// Reset Status khi chọn chức năng redim ký hiệu
        /// </summary>
        /// <param name="action"></param>
        public static void ResetStatus(enumMapOption action)
        {
            MapOption = action;
            ActionStatus = enumActionStatus.NONE;
            DrawStatus = enumDrawingOption.NONE;
            RedimOption = enumRedimOption.NONE;
        }

    }
}
