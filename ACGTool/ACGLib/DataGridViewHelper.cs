using System.Drawing;
using System.Windows.Forms;

namespace ACGLib
{
    public static class DataGridViewHelper
    {
        /// <summary>
        /// Khởi tạo các default events của datagridview. Test functinol
        /// </summary>
        /// <param name="gridView"></param>
        public static void DataGridView_InitEvent(DataGridView gridView)
        {
            gridView.RowPostPaint += new DataGridViewRowPostPaintEventHandler(DataGridView_RowPostPaint);
        }

        /// <summary>
        /// Thêm stt ở row header. Test ...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void DataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, grid.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

    }
}
