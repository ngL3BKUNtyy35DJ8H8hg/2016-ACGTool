using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ACGLib;

namespace ACGTool.UserControls
{
    public partial class ucImportExcel : UserControl
    {
        private BackgroundWorker bw = new BackgroundWorker();

        private Microsoft.Office.Interop.Excel.Application xlApp;
        private Microsoft.Office.Interop.Excel.Workbook xlWorkbook;
        private Microsoft.Office.Interop.Excel._Worksheet xlWorksheet;
        private Microsoft.Office.Interop.Excel.Range xlRange;


        public ucImportExcel()
        {
            InitializeComponent();

            DataGridViewHelper.DataGridView_InitEvent(dataGridViewExcel);
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                //mo form cho nguoi ta chon co so du lieu
                OpenFileDialog OpenFileDialog1 = new OpenFileDialog();
                OpenFileDialog1.Filter = "Excel 2003 File(*.xls)|*.xls;|Excel 2007 File(*.xlsx)|*.xlsx;|All files (*.*)|*.*";
                txtFilePath.Text = "";
                OpenFileDialog1.ShowDialog();
                txtFilePath.Text = OpenFileDialog1.FileName;
                comboBoxSheet.Items.Clear();
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkbook = xlApp.Workbooks.Open(txtFilePath.Text, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                for (int i = 0; i < xlWorkbook.Sheets.Count; i++)
                {
                    comboBoxSheet.Items.Add((i + 1).ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private System.Data.DataTable dbExcel;
        private Dictionary<int, int> colLen; //Lưu chiều dài lớn nhất có thể có của mỗi col

        private int sheetIndex = 0;
        private int colStart = 0;
        private int rowStart = 0;
        private int colCount = 0;
        private int rowCount = 0;

        private void btnReadExcel_Click(object sender, EventArgs e)
        {
            
                this.statusStrip1.Visible = true;
                dbExcel = new System.Data.DataTable();
                sheetIndex = int.Parse(comboBoxSheet.Text);
                colStart = int.Parse(txtColumnStart.Text);
                rowStart = int.Parse(txtRowStart.Text);
                colCount = int.Parse(txtColumnCount.Text);
                rowCount = int.Parse(txtRowCount.Text);
                string value = "";

                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet =
                    (Microsoft.Office.Interop.Excel._Worksheet)xlWorkbook.Sheets[sheetIndex];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                //Khởi tạo header datagridview
                for (int i = colStart; i <= colCount; i++)
                {
                    try
                    {
                        value =
                            (string)
                            (xlWorksheet.Cells[1, i] as Microsoft.Office.Interop.Excel.Range).Value2.ToString();
                        value =
                            value.Trim()
                                 .Replace("\n", "_")
                                 .Replace(@"'", "_")
                                 .Replace(@"\", "_")
                                 .Replace(@"/", "_")
                                 .Replace(" ", "_");

                        if (dbExcel.Columns.Contains(value))
                            value = value + "_" + i.ToString();
                        DataColumn col = new DataColumn(value, typeof(string));
                        dbExcel.Columns.Add(col);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            string.Format(
                                "Tiêu đề column {0} sau column \"{1}\" bị rỗng. Vui lòng đặt tiêu đề hoặc chỉnh lại số column cần đọc",
                                i.ToString(), value));
                        value = "";
                        return;
                    }
                }

                try
                {
                    if (bw.IsBusy != true)
                    {
                        btnReadExcel.Text = "Cancel Read";

                        //Lưu kích thước lớn nhất của mỗi col
                        colLen = new Dictionary<int, int>();
                        //Add giá trị
                        bw = new BackgroundWorker();
                        bw.WorkerReportsProgress = true;
                        bw.WorkerSupportsCancellation = true;
                        bw.DoWork += new DoWorkEventHandler(bw_DoWork_ReadExcel);
                        bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
                        bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted_ReadExcel);

                        bw.RunWorkerAsync();
                    }
                    else
                    {
                        if (bw.WorkerSupportsCancellation == true)
                        {

                            bw.CancelAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

        }

        private void btnSaveInDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                if (bw.IsBusy != true)
                {
                    btnReadExcel.Text = "Cancel Save";

                    this.statusStrip1.Visible = true;

                    //delete table if exists
                    string sqlDelete =
                        string.Format(
                            "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[{0}]') AND type in (N'U')) DROP TABLE [dbo].[{1}]",
                            txtTableName.Text, txtTableName.Text);
                    MITI.BaseDB.ExecSql_NoneQuery(sqlDelete);
                    //create a table
                    string sqlCreateTable = string.Format("CREATE TABLE [dbo].[{0}](", txtTableName.Text);

                    int index = 0;
                    for (int i = 0; i < dbExcel.Columns.Count; i++)
                    {
                        //sqlCreateTable += string.Format("[{0}] [nvarchar]({1}) NULL,", col.ColumnName, (colLen[index++] + 1).ToString());
                        sqlCreateTable += string.Format("[{0}] [nvarchar]({1}) NULL,", dbExcel.Columns[i].ColumnName, colLen[i]);
                    }

                    sqlCreateTable = sqlCreateTable.Substring(0, sqlCreateTable.Length - 1) + ")";
                    MITI.BaseDB.ExecSql_NoneQuery(sqlCreateTable);
                    bw = new BackgroundWorker();
                    bw.WorkerReportsProgress = true;
                    bw.WorkerSupportsCancellation = true;
                    bw.DoWork += new DoWorkEventHandler(bw_DoWork_SaveIntoTable);
                    bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
                    bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted_SaveIntoTable);

                    bw.RunWorkerAsync();
                }
                else
                {
                    if (bw.WorkerSupportsCancellation == true)
                    {

                        bw.CancelAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            

            
        }

        private void comboBoxSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxSheet.SelectedIndex >= 0)
            {
                int index = int.Parse(comboBoxSheet.Text);
                xlWorksheet = (Microsoft.Office.Interop.Excel._Worksheet)xlWorkbook.Sheets[index];
                xlRange = xlWorksheet.UsedRange;
                txtColumnCount.Text = xlRange.Columns.Count.ToString();
                txtRowCount.Text = xlRange.Rows.Count.ToString();
            }
        }


        private void bw_DoWork_ReadExcel(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            string value = "";
            for (int r = rowStart; r <= rowCount; r++)
            {
                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    int indexCol = 0;
                    DataRow row = dbExcel.NewRow();
                    for (int c = colStart; c <= colCount; c++)
                    {
                        try
                        {
                            value = (xlWorksheet.Cells[r, c] as Microsoft.Office.Interop.Excel.Range).Value2.ToString();
                        }
                        catch
                        {
                            value = "";
                        }

                        row[indexCol] = value;
                        if (!colLen.ContainsKey(indexCol))
                            colLen.Add(indexCol, value.Length + 1);
                        else
                        {
                            //Lấy giá trị lớn nhất
                            colLen[indexCol] = value.Length > colLen[indexCol] ? value.Length : colLen[indexCol];
                        }
                        indexCol++;
                    }
                    dbExcel.Rows.Add(row);
                    dataGridViewExcel.Invoke((MethodInvoker)delegate
                        {
                            dataGridViewExcel.DataSource = dbExcel;
                            //dataGridViewExcel.Rows[dataGridViewExcel.Rows.Count - 1].Selected = true;
                            dataGridViewExcel.FirstDisplayedScrollingRowIndex = dataGridViewExcel.Rows.Count - 1;
                        });
                    int percentValue = (int) (((float) r/rowCount)*100);
                    worker.ReportProgress(percentValue);
                }
                // Perform a time consuming operation and report progress.
                //System.Threading.Thread.Sleep(500);
            }
        }

        private void bw_RunWorkerCompleted_ReadExcel(object sender, RunWorkerCompletedEventArgs e)
        {
            this.statusStrip1.Visible = false;
            btnReadExcel.Text = "Read Excel";
            if ((e.Cancelled == true))
            {
                //this.toolStripStatusLabelPercentage.Text = "Canceled!";
            }

            else if (!(e.Error == null))
            {
                MessageBox.Show("Error: " + e.Error.Message);
            }

            else
            {
                dataGridViewExcel.DataSource = dbExcel;
                MessageBox.Show("Complete!");
            }
        }

        private string sqlInsert = "";
        private void bw_DoWork_SaveIntoTable(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            for (int i = 0; i < dbExcel.Rows.Count; i++)
            {

                if ((worker.CancellationPending == true))
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    DataRow row = dbExcel.Rows[i];
                    sqlInsert += string.Format(" INSERT INTO [dbo].[{0}] VALUES(", txtTableName.Text);
                    foreach (DataColumn col in dbExcel.Columns)
                    {
                        sqlInsert += string.Format("N'{0}',", row[col.ColumnName].ToString().Replace("'", "''"));
                    }
                    sqlInsert = sqlInsert.Substring(0, sqlInsert.Length - 1) + ")";
                    int percentValue = (int)(((float)i / (dbExcel.Rows.Count - 1)) * 100);
                    worker.ReportProgress(percentValue);
                }
            }

        }

        private void bw_RunWorkerCompleted_SaveIntoTable(object sender, RunWorkerCompletedEventArgs e)
        {
            this.statusStrip1.Visible = false;
            btnReadExcel.Text = "Save as a new table in Database";
            if ((e.Cancelled == true))
            {
                //this.toolStripStatusLabelPercentage.Text = "Canceled!";
            }

            else if (!(e.Error == null))
            {
                MessageBox.Show("Error: " + e.Error.Message);
            }

            else
            {
                MITI.BaseDB.ExecSql_NoneQuery(sqlInsert);
                MessageBox.Show("Create a table successful!");
            }
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.toolStripStatusLabelPercentage.Text = (e.ProgressPercentage.ToString() + "%");
            toolStripProgressBarImport.Value = e.ProgressPercentage;
        }
        
    }
}
