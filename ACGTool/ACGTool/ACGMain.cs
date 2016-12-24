using System;
using System.Data.OleDb;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Globalization;

using ACGTool.Classes;
using System.Drawing.Drawing2D;

using System.Runtime.InteropServices; // For COMException
using Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using DataTable = Microsoft.Office.Interop.Excel.DataTable;
using Excel = Microsoft.Office.Interop.Excel;

namespace ACGTool 
{
    public partial class ACGMain : Form
    {
        const string _FONT = "Courier New";
        const int _FONTSIZE = 10;

        DataView dv; //chua toan bo data
        DataView dvPK; //chua toan bo khoa chinh

        public ACGMain()
        {
            InitializeComponent();
        }

        public void InitTreeView()
        {
            tvControl.Nodes.Clear();
            tvControl.ImageList = ImageList1;
            //Lay ds table
            DataSet ds = MITI.BaseDB.ExecSqlProcedure_DataSet("sp_tables");
            
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                //Add Node in Treeview
                TreeNode node;
                if (cmbHienThi.Text != "All")
                {
                    if (dr["TABLE_TYPE"].ToString() == cmbHienThi.Text)
                    {
                        node = tvControl.Nodes.Add(dr["TABLE_NAME"].ToString(), dr["TABLE_NAME"].ToString());
                        FormatLeafNode(ref node);
                    }
                }
                else
                {
                    node = tvControl.Nodes.Add(dr["TABLE_NAME"].ToString(), dr["TABLE_NAME"].ToString());
                    FormatLeafNode(ref node);
                }


                
                //Kiem tra node co phai la node dau cua treeview khong
                //Neu bang thi cho mau xanh, nguoc lai mau do
                //Tra ve node dau tien theo format dinh san
                //if (((bool)dr[cuoiColumnName] == false))
                //    FormatNode(ref node);
                //else  //Tra ve node leaf theo format dinh san
                //    FormatLeafNode(ref node);
            }
            tvControl.Focus();
            //tvControl.SelectedNode = tvControl.Nodes[0];
        }

        //Add node dau tien cua treeview
        public void FormatNode(ref TreeNode node)
        {
            node.ForeColor = Color.Blue;
            node.ImageIndex = 0;
            node.SelectedImageIndex = 0;
        }

        public void FormatLeafNode(ref TreeNode node)
        {
            node.ForeColor = Color.DarkRed;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Font font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //int cout = this.Controls.Count;
            //foreach (Control objControl in this.Controls)
            //{
            //    if (objControl is RichTextBox)
            //    {
            //        objControl.Font = font;
            //    }
            //}
            MITI.BaseDB.g_ConnectStr = MITI.BaseDB.GetConnectStrFromFile(System.Windows.Forms.Application.StartupPath + "\\DBConnect.udl");
            //De mac dinh CSDL la master
            MITI.BaseDB.ThayGiaTri("Initial Catalog", "master");

            DataSet ds = MITI.BaseDB.ExecSqlProcedure_DataSet("sp_databases");
            cmbDatabase.ValueMember = "DATABASE_NAME";
            cmbDatabase.DisplayMember = "DATABASE_NAME";
            cmbDatabase.DataSource = ds.Tables[0].DefaultView;
            cmbHienThi.SelectedIndex = 1;
            InitTreeView();
            tvControl.SelectedNode = tvControl.Nodes[0];
            FormatDataGridColumns();
            FormatDataGridPrimaryKeys();

            //Hien thi danh muc don vi
            chkShowDMDonVi.Checked = true;
            comboBoxLanguage.SelectedIndex = 1;

            
            InitRichTextBoxes();
        }

        private void cmbDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Chon csdl can ket noi
            lblDatabase.Text = "Database: " + cmbDatabase.Text;
            MITI.BaseDB.ThayGiaTri("Initial Catalog", cmbDatabase.Text);
            DataSet ds = MITI.BaseDB.ExecSqlProcedure_DataSet("sp_databases");
            InitTreeView();
        }

        private void cmbHienThi_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitTreeView();
        }

        public void FormatDataGridColumns()
        {
            gridEXColumns.AllowUserToAddRows = false;
            gridEXColumns.AllowUserToDeleteRows = false;
            gridEXColumns.ReadOnly = true;
            gridEXColumns.SelectionMode = DataGridViewSelectionMode.CellSelect;
            gridEXColumns.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            CultureInfo culture = new CultureInfo("fr-FR", true);

            //Set visible = false cho tat ca cac thuoc tinh
            for (int i = 0; i < gridEXColumns.Columns.Count; i++)
            {
                gridEXColumns.Columns[i].Visible = false;
            }

            //Cac thuoc tinh chung
            gridEXColumns.Columns["COLUMN_NAME"].Visible = true;
            //gridEXColumns.Columns["COLUMN_NAME"].Caption = "COLUMN";
            gridEXColumns.Columns["COLUMN_NAME"].Width = 135;

            gridEXColumns.Columns["TYPE_NAME"].Visible = true;
            //gridEXColumns.Columns["TYPE_NAME"].Caption = "TYPE";
            gridEXColumns.Columns["TYPE_NAME"].Width = 75;

            gridEXColumns.Columns["LENGTH"].Visible = true;
            //gridEXColumns.Columns["LENGTH"].Caption = "LENGTH";
            gridEXColumns.Columns["LENGTH"].Width = 60;

            gridEXColumns.Columns["PRECISION"].Visible = true;
            gridEXColumns.Columns["PRECISION"].Width = 70;

            gridEXColumns.Columns["SCALE"].Visible = true;
            gridEXColumns.Columns["SCALE"].Width = 50;

            gridEXColumns.Columns["NULLABLE"].Visible = true;
            gridEXColumns.Columns["NULLABLE"].Width = 70;
            //Cho panel chua datagrid rong ra
            panel2.Width = 485;
        }


        public void FormatDataGridPrimaryKeys()
        {
            gridEXPrimaryKeys.AllowUserToAddRows = false;
            gridEXPrimaryKeys.AllowUserToDeleteRows = false;
            gridEXPrimaryKeys.ReadOnly = true;
            gridEXPrimaryKeys.SelectionMode = DataGridViewSelectionMode.CellSelect;
            gridEXPrimaryKeys.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            //Set visible = false cho tat ca cac thuoc cua primary key
            for (int i = 0; i < gridEXPrimaryKeys.Columns.Count; i++)
            {
                gridEXPrimaryKeys.Columns[i].Visible = false;
            }
            gridEXPrimaryKeys.Columns["COLUMN_NAME"].Visible = true;
        }

        

        public void LoadDataGridJanusColumns()
        {
            string sql = "exec sp_columns" + " N'" + tvControl.SelectedNode.Text + "'";
            DataSet ds = MITI.BaseDB.ExecSql_DataSet(sql);
            dv = ds.Tables[0].DefaultView; 
            //Filter dataview
            gridEXColumns.DataSource = dv;
            TongSoToolStripStatusLabel.Text = dv.Count.ToString() + " fields";
        }

        public void LoadDataGridJanusPrimaryKeys()
        {
            //Load primary keys
            string sql = "exec sp_pkeys" + " N'" + tvControl.SelectedNode.Text + "'";
            DataSet ds = MITI.BaseDB.ExecSql_DataSet(sql);
            dvPK = ds.Tables[0].DefaultView;
            gridEXPrimaryKeys.DataSource = dvPK;
        }

        private void tvControl_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadDataGridJanusColumns();
            LoadDataGridJanusPrimaryKeys();
        }

        private void chkShowDMDonVi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowDMDonVi.Checked)
            {
                panelTables.Visible = true;
            }
            else
                panelTables.Visible = false;
        }

        #region "Generate code"

        public void InitRichTextBoxes()
        {
            System.Drawing.Font objFont = new System.Drawing.Font(_FONT, _FONTSIZE, FontStyle.Regular);
            //.NET
            richTextBoxClassInfo.Font = objFont;
            richTextBoxClassController.Font = objFont;
            richTextBoxDataProvider.Font = objFont;
            richTextBoxSqlDataProvider.Font = objFont;
            //SQL
            richTextBoxSqlProcedures.Font = objFont;

        }

        #region "ClassInfo"
        private void VBDotNet_ClassInfo()
        {
            richTextBoxClassInfo.Clear();
            Procedure objProcedure = new ProcedureVB();
            ClassInfoGeneration objClassInfo = new ClassInfoGenerationVBDotNet();
            //Init a class
            string str = objClassInfo.InitClass(tvControl.SelectedNode.Name);

            int index, len;
            string type, name, des;
            //DataGridViewRow row;
            DataGridViewRow row;
            //Add attributies to the class
            //len = gridEXColumns.Rows.Count;
            len = gridEXColumns.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                //Get row
                row = gridEXColumns.Rows[len - 1 - i];
                //Get type, name, description of attribute
                type = objProcedure.GetTypeMap(row.Cells["TYPE_NAME"].Value.ToString());
                name = row.Cells["COLUMN_NAME"].Value.ToString();
                des = objProcedure.GetDescription(tvControl.SelectedNode.Name, row.Cells["COLUMN_NAME"].Value.ToString());

                //Add code 1
                index = str.IndexOf(objClassInfo._ADDCODEHERE1) + objClassInfo._ADDCODEHERE1.Length;
                str = str.Insert(index, objClassInfo.DeclareAttribute(type, name, des));

                //Add code 2
                index = str.IndexOf(objClassInfo._ADDCODEHERE2) + objClassInfo._ADDCODEHERE2.Length;
                str = str.Insert(index, objClassInfo.DeclareProperty(type, name, des));
            }

            //Remove string code 1
            index = str.IndexOf(objClassInfo._ADDCODEHERE1);
            str = str.Remove(index, objClassInfo._ADDCODEHERE1.Length);

            //Remove string code 2
            index = str.IndexOf(objClassInfo._ADDCODEHERE2);
            str = str.Remove(index, objClassInfo._ADDCODEHERE2.Length);

            richTextBoxClassInfo.AppendText(str);
        }

        private void CSharp_ClassInfo()
        {
            richTextBoxClassInfo.Clear();
            Procedure objProcedure = new ProcedureCSharp();
            ClassInfoGeneration objClassInfo = new ClassInfoGenerationCSharp();
            //Init a class
            string str = objClassInfo.InitClass(tvControl.SelectedNode.Name);

            int index, len;
            string type, name, des;
            //DataGridViewRow row;
            DataGridViewRow row;

            //Add attributies to the class
            len = gridEXColumns.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                //Get row
                row = gridEXColumns.Rows[len - 1 - i];
                //Get type, name, description of attribute
                type = objProcedure.GetTypeMap(row.Cells["TYPE_NAME"].Value.ToString());
                name = row.Cells["COLUMN_NAME"].Value.ToString();
                des = objProcedure.GetDescription(tvControl.SelectedNode.Name, row.Cells["COLUMN_NAME"].Value.ToString());

                //Add code 1
                index = str.IndexOf(objClassInfo._ADDCODEHERE1) + objClassInfo._ADDCODEHERE1.Length;
                str = str.Insert(index, objClassInfo.DeclareAttribute(type, name, des));

                //Add code 2
                index = str.IndexOf(objClassInfo._ADDCODEHERE2) + objClassInfo._ADDCODEHERE2.Length;
                str = str.Insert(index, objClassInfo.DeclareProperty(type, name, des));
            }

            //Remove string code 1
            index = str.IndexOf(objClassInfo._ADDCODEHERE1);
            str = str.Remove(index, objClassInfo._ADDCODEHERE1.Length);

            //Remove string code 2
            index = str.IndexOf(objClassInfo._ADDCODEHERE2);
            str = str.Remove(index, objClassInfo._ADDCODEHERE2.Length);

            richTextBoxClassInfo.AppendText(str);
        }
        #endregion

        private void SqlProcedure()
        {
            richTextBoxSqlProcedures.Clear();
            Procedure objProcedure = new ProcedureSQL();

            //Init a class
            ClassSQLStoreProcedureGeneration objSqlProcedure = new ClassSQLStoreProcedureGeneration();
            string str = objSqlProcedure.InitClass(tvControl.SelectedNode.Name);

            int index, len;
            string type, name, filter;
            
            DataView dvFilter;
            DataRowView dr;

            //DataGridViewRow row;
            DataGridViewRow row;

            //Bat dau khai bao bien

            //XU LY VONG LAP CHO PRIMARY KEYS
            dvFilter = dv.ToTable().DefaultView;
            //len = gridEXPrimaryKeys.GetRows().Length;
            len = gridEXPrimaryKeys.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                //row = gridEXPrimaryKeys.GetRow(len - 1 - i);
                row = gridEXPrimaryKeys.Rows[len - 1 - i];
                filter = "COLUMN_NAME = '" + row.Cells["COLUMN_NAME"].Value.ToString() + "'";
                dvFilter.RowFilter = filter;
                dr = dvFilter[0];
                type = objProcedure.GetTypeMap(dr["TYPE_NAME"].ToString(), (int)dr["PRECISION"], dr["SCALE"], (int)dr["LENGTH"]);
                name = dr["COLUMN_NAME"].ToString();

                //GET CODE
                //Add biến đầu vào cho store procedure GET là các khóa chính
                //Add code 1
                index = str.IndexOf(objSqlProcedure._ADDCODEHERE[0]) + objSqlProcedure._ADDCODEHERE[1].Length;
                if (i > 0)
                    str = str.Insert(index, objSqlProcedure.Template2(type, name, true));
                else
                    str = str.Insert(index, objSqlProcedure.Template2(type, name, false));

                //Lấy các thuộc tính khóa chính cho điều kiện WHERE của câu lệnh SELECT
                //Add code 3
                index = str.IndexOf(objSqlProcedure._ADDCODEHERE[2]) + objSqlProcedure._ADDCODEHERE[2].Length;
                if (i > 0)
                    str = str.Insert(index, objSqlProcedure.Template3(name, true));
                else
                    str = str.Insert(index, objSqlProcedure.Template3(name, false));

                //UPDATE CODE
                //Lấy các thuộc tính khóa chính cho điều kiện WHERE của câu lệnh UPDATE
                //Add code 9
                index = str.IndexOf(objSqlProcedure._ADDCODEHERE[8]) + objSqlProcedure._ADDCODEHERE[8].Length;
                if (i > 0)
                    str = str.Insert(index, objSqlProcedure.Template3(name, true));
                else
                    str = str.Insert(index, objSqlProcedure.Template3(name, false));

                //DELETE CODE
                //Add biến đầu vào cho store procedure DELETE là các khóa chính
                //Add code 10
                index = str.IndexOf(objSqlProcedure._ADDCODEHERE[9]) + objSqlProcedure._ADDCODEHERE[9].Length;
                if (i > 0)
                    str = str.Insert(index, objSqlProcedure.Template2(type, name, true));
                else
                    str = str.Insert(index, objSqlProcedure.Template2(type, name, false));

                //Lấy các thuộc tính khóa chính cho điều kiện WHERE của câu lệnh DELETE
                //Add code 11
                index = str.IndexOf(objSqlProcedure._ADDCODEHERE[10]) + objSqlProcedure._ADDCODEHERE[10].Length;
                if (i > 0)
                    str = str.Insert(index, objSqlProcedure.Template3(name, true));
                else
                    str = str.Insert(index, objSqlProcedure.Template3(name, false));
            }

            //XU LY VONG LAP CHO TAT CA COLUMNS TRONG TABLE
            //len = gridEXColumns.Rows.Count;
            len = gridEXColumns.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                row = gridEXColumns.Rows[len - 1 - i];
                type = objProcedure.GetTypeMap(row.Cells["TYPE_NAME"].Value.ToString(), (int)row.Cells["PRECISION"].Value, row.Cells["SCALE"].Value, (int)row.Cells["LENGTH"].Value);
                name = row.Cells["COLUMN_NAME"].Value.ToString();

                //GET CODE
                //Lấy tất cả các column cho câu lệnh SELECT
                //Add code 2
                index = str.IndexOf(objSqlProcedure._ADDCODEHERE[1]) + objSqlProcedure._ADDCODEHERE[1].Length;
                if (i > 0)
                    str = str.Insert(index, objSqlProcedure.Template1(name, true));
                else
                    str = str.Insert(index, objSqlProcedure.Template1(name, false));

                //ADD CODE
                //Nếu column set Identity thì bỏ qua trong hàm Add
                if (objProcedure.IsIdentity(row.Cells["TYPE_NAME"].Value.ToString()) == false)
                {
                    //Add biến đầu vào cho store procedure ADD
                    //Add code 4
                    index = str.IndexOf(objSqlProcedure._ADDCODEHERE[3]) + objSqlProcedure._ADDCODEHERE[3].Length;
                    if (i > 0)
                        str = str.Insert(index, objSqlProcedure.Template2(type, name, true));
                    else
                        str = str.Insert(index, objSqlProcedure.Template2(type, name, false));

                    //Lấy tất cả các column cho câu lệnh INSERT INTO
                    //Add code 5
                    index = str.IndexOf(objSqlProcedure._ADDCODEHERE[4]) + objSqlProcedure._ADDCODEHERE[4].Length;
                    if (i > 0)
                        str = str.Insert(index, objSqlProcedure.Template1(name, true));
                    else
                        str = str.Insert(index, objSqlProcedure.Template1(name, false));

                    //Lấy tất cả các column cho câu lệnh VALUES
                    //Add code 6
                    index = str.IndexOf(objSqlProcedure._ADDCODEHERE[5]) + objSqlProcedure._ADDCODEHERE[5].Length;
                    if (i > 0)
                        str = str.Insert(index, objSqlProcedure.Template4(name, true));
                    else
                        str = str.Insert(index, objSqlProcedure.Template4(name, false));
                }
                 
                //UPDATE CODE
                //Add columns cho bien dau vao cua store procedure UPDATE
                //Add code 7
                index = str.IndexOf(objSqlProcedure._ADDCODEHERE[6]) + objSqlProcedure._ADDCODEHERE[6].Length;
                if (i > 0)
                    str = str.Insert(index, objSqlProcedure.Template2(type, name, true));
                else
                    str = str.Insert(index, objSqlProcedure.Template2(type, name, false));
            }

            //UPDATE CODE
            /* XU LY UPDATE THUOC TINH
             * Chi update nhung thuoc tinh khong phai la khoa chinh.
             * De lay nhung thuoc tinh khong phai khoa chinh, ta copy sang mot dataview moi,
             * sau do xoa het nhung khoa chinh cua dataview moi ta se duoc cac thuoc tinh khong phai la khoa
             */
            //Copy du lieu sang mot DataView moi
            dvFilter = dv.ToTable().DefaultView;
            //Xoa het khoa chinh cua DataView moi duoc copy
            //len = gridEXPrimaryKeys.GetRows().Length;
            len = gridEXPrimaryKeys.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                row = gridEXPrimaryKeys.Rows[i];
                filter = "COLUMN_NAME = '" + row.Cells["COLUMN_NAME"].Value.ToString() + "'";
                dvFilter.RowFilter = filter;
                if (dvFilter.Count > 0)
                    dvFilter[0].Row.Delete();
            }
            //Bo loc sau khi da xoa het khoa chinh, ta được các column không phải là khóa chính
            dvFilter.RowFilter = String.Empty;
            //Lay length cua dvFilter sau khi delete khoa chinh
            len = dvFilter.Count;
            //Xu ly xong, thuc hien add code cho store procedure
            for (int i = 0; i < len; i++)
            {
                dr = dvFilter[len - 1 - i];
                //type = objSqlProcedure.GetTypeMap(dr["TYPE_NAME"].ToString(), (int)dr["PRECISION"], (int)dr["LENGTH"]);
                name = dr["COLUMN_NAME"].ToString();
                //Add code 8
                index = str.IndexOf(objSqlProcedure._ADDCODEHERE[7]) + objSqlProcedure._ADDCODEHERE[7].Length;
                if (i > 0)
                    str = str.Insert(index, objSqlProcedure.Template5(name, true));
                else
                    str = str.Insert(index, objSqlProcedure.Template5(name, false));
            }

            //Remove all string code
            for (int i = 0; i < objSqlProcedure._ADDCODEHERE.Length; i++)
            {
                index = str.IndexOf(objSqlProcedure._ADDCODEHERE[i]);
                str = str.Remove(index, objSqlProcedure._ADDCODEHERE[i].Length);
            }
            

            //Insert into richtextbox
            richTextBoxSqlProcedures.AppendText(str);
        }
        

        #region "ClassController"
        private void VBDotNet_ClassController()
        {
            richTextBoxClassController.Clear();
            Procedure objProcedure = new ProcedureVB();
            //Init a class
            ClassControllerGeneration objClassController = new ClassControllerGenerationVB();
            string str = objClassController.InitClass(tvControl.SelectedNode.Name);

            int index, len;
            string type, name, filter;
            //DataGridViewRow row;
            DataGridViewRow row;
            DataView dvFilter;
            DataRowView dr;

            //Bat dau khai bao bien
            len = gridEXColumns.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                row = gridEXColumns.Rows[len - 1 - i];
                name = row.Cells["COLUMN_NAME"].Value.ToString();

                //Add code 1 
                //Nếu column set Identity thì bỏ qua trong hàm Add
                if (objProcedure.IsIdentity(row.Cells["TYPE_NAME"].Value.ToString()) == false)
                {
                    index = str.IndexOf(objClassController._ADDCODEHERE[0]) + objClassController._ADDCODEHERE[0].Length;
                    if (i > 0)
                        str = str.Insert(index, objClassController.Template1(name, true));
                    else
                        str = str.Insert(index, objClassController.Template1(name, false));
                }

                //Add code 2 
                index = str.IndexOf(objClassController._ADDCODEHERE[1]) + objClassController._ADDCODEHERE[1].Length;
                if (i > 0)
                    str = str.Insert(index, objClassController.Template1(name, true));
                else
                    str = str.Insert(index, objClassController.Template1(name, false));
            }

            dvFilter = dv.ToTable().DefaultView;
            len = gridEXPrimaryKeys.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                row = gridEXPrimaryKeys.Rows[len - 1 - i];
                filter = "COLUMN_NAME = '" + row.Cells["COLUMN_NAME"].Value.ToString() + "'";
                dvFilter.RowFilter = filter;
                dr = dvFilter[0];
                type = objProcedure.GetTypeMap(dvFilter[0]["TYPE_NAME"].ToString());
                name = dr["COLUMN_NAME"].ToString();

                //Add code 3
                index = str.IndexOf(objClassController._ADDCODEHERE[2]) + objClassController._ADDCODEHERE[2].Length;
                if (i > 0)
                    str = str.Insert(index, objClassController.Template2(type, name, true));
                else
                    str = str.Insert(index, objClassController.Template2(type, name, false));

                //Add code 4
                index = str.IndexOf(objClassController._ADDCODEHERE[3]) + objClassController._ADDCODEHERE[3].Length;
                if (i > 0)
                    str = str.Insert(index, objClassController.Template3(name, true));
                else
                    str = str.Insert(index, objClassController.Template3(name, false));

                //Add code 5
                index = str.IndexOf(objClassController._ADDCODEHERE[4]) + objClassController._ADDCODEHERE[4].Length;
                if (i > 0)
                    str = str.Insert(index, objClassController.Template2(type, name, true));
                else
                    str = str.Insert(index, objClassController.Template2(type, name, false));

                //Add code 6
                index = str.IndexOf(objClassController._ADDCODEHERE[5]) + objClassController._ADDCODEHERE[5].Length;
                if (i > 0)
                    str = str.Insert(index, objClassController.Template3(name, true));
                else
                    str = str.Insert(index, objClassController.Template3(name, false));
            }

            //Remove all string code
            for (int i = 0; i < objClassController._ADDCODEHERE.Length; i++)
            {
                index = str.IndexOf(objClassController._ADDCODEHERE[i]);
                str = str.Remove(index, objClassController._ADDCODEHERE[i].Length);
            }

            richTextBoxClassController.AppendText(str);
        }

        
        #endregion

        #region "Data Provider"
        private void VBDotNet_DataProvider()
        {
            richTextBoxDataProvider.Clear();
            Procedure objProcedure = new ProcedureVB();
            //Init a class
            ClassDataProviderGeneration objDataProvider = new ClassDataProviderGenerationVB();
            string str = objDataProvider.InitClass(tvControl.SelectedNode.Name);

            int index, len;
            string type, name, filter;
            DataGridViewRow row;
            DataView dvFilter;
            DataRowView dr;

            //Bat dau khai bao bien
            len = gridEXColumns.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                row = gridEXColumns.Rows[len - 1 - i];
                type = objProcedure.GetTypeMap(row.Cells["TYPE_NAME"].Value.ToString());
                name = row.Cells["COLUMN_NAME"].Value.ToString();

                //Nếu column set Identity thì bỏ qua trong hàm Add
                if (objProcedure.IsIdentity(row.Cells["TYPE_NAME"].Value.ToString()) == false)
                {
                    //Add code 1
                    index = str.IndexOf(objDataProvider._ADDCODEHERE[0]) + objDataProvider._ADDCODEHERE[0].Length;
                    if (i > 0)
                        str = str.Insert(index, objDataProvider.Template1(type, name, true));
                    else
                        str = str.Insert(index, objDataProvider.Template1(type, name, false));
                }

                //Add code 2
                index = str.IndexOf(objDataProvider._ADDCODEHERE[1]) + objDataProvider._ADDCODEHERE[1].Length;
                if (i > 0)
                    str = str.Insert(index, objDataProvider.Template1(type, name, true));
                else
                    str = str.Insert(index, objDataProvider.Template1(type, name, false));
                
            }

            dvFilter = dv.ToTable().DefaultView;
            len = gridEXPrimaryKeys.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                row = gridEXPrimaryKeys.Rows[len - 1 - i];
                filter = "COLUMN_NAME = '" + row.Cells["COLUMN_NAME"].Value.ToString() + "'";
                dvFilter.RowFilter = filter;
                dr = dvFilter[0];
                type = objProcedure.GetTypeMap(dvFilter[0]["TYPE_NAME"].ToString());
                name = dr["COLUMN_NAME"].ToString();

                //Add code 3
                index = str.IndexOf(objDataProvider._ADDCODEHERE[2]) + objDataProvider._ADDCODEHERE[2].Length;
                if (i > 0)
                    str = str.Insert(index, objDataProvider.Template1(type, name, true));
                else
                    str = str.Insert(index, objDataProvider.Template1(type, name, false));

                //Add code 4
                index = str.IndexOf(objDataProvider._ADDCODEHERE[3]) + objDataProvider._ADDCODEHERE[3].Length;
                if (i > 0)
                    str = str.Insert(index, objDataProvider.Template1(type, name, true));
                else
                    str = str.Insert(index, objDataProvider.Template1(type, name, false));
            }

            //Remove all string code
            for (int i = 0; i < objDataProvider._ADDCODEHERE.Length; i++)
            {
                index = str.IndexOf(objDataProvider._ADDCODEHERE[i]);
                str = str.Remove(index, objDataProvider._ADDCODEHERE[i].Length);
            }

            richTextBoxDataProvider.AppendText(str);
        }
        #endregion

        #region "SQL Data Provider"
        private void VBDotNet_SQLDataProvider()
        {
            richTextBoxSqlDataProvider.Clear();
            Procedure objProcedure = new ProcedureVB();
            //Init a class
            ClassSQLDataProviderGeneration objSQLDateProvider = new ClassSQLDataProviderGeneration();
            string str = objSQLDateProvider.InitClass(tvControl.SelectedNode.Name);

            int allow_null;

            int index, len;
            string type, name, filter;
            DataGridViewRow row;
            DataView dvFilter;
            DataRowView dr;
            
            //Bat dau khai bao bien
            len = gridEXColumns.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                row = gridEXColumns.Rows[len - 1 - i];
                type = objProcedure.GetTypeMap(row.Cells["TYPE_NAME"].Value.ToString());
                name = row.Cells["COLUMN_NAME"].Value.ToString();
                allow_null = int.Parse(row.Cells["NULLABLE"].Value.ToString());
                
                
                //Nếu column set Identity thì bỏ qua trong hàm Add
                if (objProcedure.IsIdentity(row.Cells["TYPE_NAME"].Value.ToString()) == false)
                {
                    //Add code 1
                    index = str.IndexOf(objSQLDateProvider._ADDCODEHERE[0]) + objSQLDateProvider._ADDCODEHERE[0].Length;
                    if (i > 0)
                        str = str.Insert(index, objSQLDateProvider.Template1(type, name, true));
                    else
                        str = str.Insert(index, objSQLDateProvider.Template1(type, name, false));


                    //Add code 2
                    index = str.IndexOf(objSQLDateProvider._ADDCODEHERE[1]) + objSQLDateProvider._ADDCODEHERE[1].Length;
                    if (i > 0)
                        str = str.Insert(index, objSQLDateProvider.Template2(name, allow_null, true));
                    else
                        str = str.Insert(index, objSQLDateProvider.Template2(name, allow_null, false));
                }

                //Add code 3
                index = str.IndexOf(objSQLDateProvider._ADDCODEHERE[2]) + objSQLDateProvider._ADDCODEHERE[2].Length;
                if (i > 0)
                    str = str.Insert(index, objSQLDateProvider.Template1(type, name, true));
                else
                    str = str.Insert(index, objSQLDateProvider.Template1(type, name, false));

                //Add code 4
                index = str.IndexOf(objSQLDateProvider._ADDCODEHERE[3]) + objSQLDateProvider._ADDCODEHERE[3].Length;
                if (i > 0)
                    str = str.Insert(index, objSQLDateProvider.Template2(name, allow_null, true));
                else
                    str = str.Insert(index, objSQLDateProvider.Template2(name, allow_null, false));
            }

            dvFilter = dv.ToTable().DefaultView;
            len = gridEXPrimaryKeys.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                row = gridEXPrimaryKeys.Rows[len - 1 - i];
                filter = "COLUMN_NAME = '" + row.Cells["COLUMN_NAME"].Value.ToString() + "'";
                dvFilter.RowFilter = filter;
                dr = dvFilter[0];
                type = objProcedure.GetTypeMap(dvFilter[0]["TYPE_NAME"].ToString());
                name = dr["COLUMN_NAME"].ToString();

                //Add code 5
                index = str.IndexOf(objSQLDateProvider._ADDCODEHERE[4]) + objSQLDateProvider._ADDCODEHERE[4].Length;
                if (i > 0)
                    str = str.Insert(index, objSQLDateProvider.Template1(type, name, true));
                else
                    str = str.Insert(index, objSQLDateProvider.Template1(type, name, false));

                //Add code 6
                index = str.IndexOf(objSQLDateProvider._ADDCODEHERE[5]) + objSQLDateProvider._ADDCODEHERE[5].Length;
                if (i > 0)
                    str = str.Insert(index, objSQLDateProvider.Template3(name, true));
                else
                    str = str.Insert(index, objSQLDateProvider.Template3(name, false));

                //Add code 7
                index = str.IndexOf(objSQLDateProvider._ADDCODEHERE[6]) + objSQLDateProvider._ADDCODEHERE[6].Length;
                if (i > 0)
                    str = str.Insert(index, objSQLDateProvider.Template1(type, name, true));
                else
                    str = str.Insert(index, objSQLDateProvider.Template1(type, name, false));

                //Add code 8
                index = str.IndexOf(objSQLDateProvider._ADDCODEHERE[7]) + objSQLDateProvider._ADDCODEHERE[7].Length;
                if (i > 0)
                    str = str.Insert(index, objSQLDateProvider.Template3(name, true));
                else
                    str = str.Insert(index, objSQLDateProvider.Template3(name, false));
            }

            //Remove all string code
            for (int i = 0; i < objSQLDateProvider._ADDCODEHERE.Length; i++)
            {
                index = str.IndexOf(objSQLDateProvider._ADDCODEHERE[i]);
                str = str.Remove(index, objSQLDateProvider._ADDCODEHERE[i].Length);
            }

            richTextBoxSqlDataProvider.AppendText(str);
        }
        #endregion

        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            Procedure syn;
            if (comboBoxLanguage.SelectedIndex == 0)
            {
                CSharp_ClassInfo();
                //Fore color
                syn = new ProcedureCSharp();
                syn.SetForeColorSyntaxes(ref richTextBoxClassInfo);
                syn.SetForeColorSyntaxes(ref richTextBoxClassController);
                syn.SetForeColorSyntaxes(ref richTextBoxDataProvider);
                syn.SetForeColorSyntaxes(ref richTextBoxSqlDataProvider);
                
            }
            else
            {
                //VBDotNet
                VBDotNet_ClassInfo();
                VBDotNet_ClassController();
                VBDotNet_DataProvider();
                VBDotNet_SQLDataProvider();
                
                //Fore color
                syn = new ProcedureVB();
                syn.SetForeColorSyntaxes(ref richTextBoxClassInfo);
                syn.SetForeColorSyntaxes(ref richTextBoxClassController);
                syn.SetForeColorSyntaxes(ref richTextBoxDataProvider);
                syn.SetForeColorSyntaxes(ref richTextBoxSqlDataProvider);
            }

            //SQL
            SqlProcedure();
            //Fore color
            syn = new ProcedureSQL();
            syn.SetForeColorSyntaxes(ref richTextBoxSqlProcedures);
        }

        #endregion

        private void buttonRun_Click(object sender, EventArgs e)
        {
            string str = "";
            int len;
            string name;
            DataView dvFilter;
            DataRowView dr;

            Procedure objProcedure;
            ClassOthersGeneration objOthers;
            richTextBoxOthers.Clear();
            if (comboBoxLanguage.SelectedIndex == 1)
            {
                objProcedure = new ProcedureVB();
                //Init a class
                objOthers = new ClassOthersGenerationVB();
            }
            else
            {
                objProcedure = new ProcedureCSharp();
                //Init a class
                objOthers = new ClassOthersGenerationCSharp();
            }
            

            //Bat dau khai bao bien

            //UPDATE CODE
            /* XU LY UPDATE THUOC TINH
             * Chi update nhung thuoc tinh khong phai la khoa chinh.
             * De lay nhung thuoc tinh khong phai khoa chinh, ta copy sang mot dataview moi,
             * sau do xoa het nhung khoa chinh cua dataview moi ta se duoc cac thuoc tinh khong phai la khoa
             */
            //Copy du lieu sang mot DataView moi
            dvFilter = dv.ToTable().DefaultView;
            //Lay length cua dvFilter sau khi delete khoa chinh
            len = dvFilter.Count;
            //Xu ly xong, thuc hien add code cho store procedure
            for (int i = 0; i < len; i++)
            {
                dr = dvFilter[i];
                name = dr["COLUMN_NAME"].ToString();
                str += objOthers.Template1(name, textBoxLeft.Text, textBoxMiddle.Text, textBoxRight.Text);
            }

            //Insert into richtextbox
            richTextBoxOthers.AppendText(str);

            //Set Fore color
            objProcedure.SetForeColorSyntaxes(ref richTextBoxOthers);
        }

        public void SamleExportToWord()
        {
            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            Word._Application oWord;
            Word._Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing,
                ref oMissing, ref oMissing);

            //Insert a paragraph at the beginning of the document.
            Word.Paragraph oPara1;
            oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
            oPara1.Range.Text = "Heading 1";
            oPara1.Range.Font.Bold = 1;
            oPara1.Format.SpaceAfter = 24;    //24 pt spacing after paragraph.
            oPara1.Range.InsertParagraphAfter();

            //Insert a paragraph at the end of the document.
            Word.Paragraph oPara2;
            object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara2 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara2.Range.Text = "Heading 2";
            oPara2.Format.SpaceAfter = 6;
            oPara2.Range.InsertParagraphAfter();

            //Insert another paragraph.
            Word.Paragraph oPara3;
            oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara3 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara3.Range.Text = "This is a sentence of normal text. Now here is a table:";
            oPara3.Range.Font.Bold = 0;
            oPara3.Format.SpaceAfter = 24;
            oPara3.Range.InsertParagraphAfter();

            //Insert a 3 x 5 table, fill it with data, and make the first row
            //bold and italic.
            Word.Table oTable;
            Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, 3, 5, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            int r, c;
            string strText;
            for (r = 1; r <= 3; r++)
                for (c = 1; c <= 5; c++)
                {
                    strText = "r" + r + "c" + c;
                    oTable.Cell(r, c).Range.Text = strText;
                }
            oTable.Rows[1].Range.Font.Bold = 1;
            oTable.Rows[1].Range.Font.Italic = 1;
            
            //Add some text after the table.
            Word.Paragraph oPara4;
            oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oPara4 = oDoc.Content.Paragraphs.Add(ref oRng);
            oPara4.Range.InsertParagraphBefore();
            oPara4.Range.Text = "And here's another table:";
            oPara4.Format.SpaceAfter = 24;
            oPara4.Range.InsertParagraphAfter();

            //Insert a 5 x 2 table, fill it with data, and change the column widths.
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oTable = oDoc.Tables.Add(wrdRng, 5, 2, ref oMissing, ref oMissing);
            oTable.Range.ParagraphFormat.SpaceAfter = 6;
            for (r = 1; r <= 5; r++)
                for (c = 1; c <= 2; c++)
                {
                    strText = "r" + r + "c" + c;
                    oTable.Cell(r, c).Range.Text = strText;
                }
            oTable.Columns[1].Width = oWord.InchesToPoints(2); //Change width of columns 1 & 2
            oTable.Columns[2].Width = oWord.InchesToPoints(3);

            //Keep inserting text. When you get to 7 inches from top of the
            //document, insert a hard page break.
            object oPos;
            double dPos = oWord.InchesToPoints(7);
            oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range.InsertParagraphAfter();
            do
            {
                wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                wrdRng.ParagraphFormat.SpaceAfter = 6;
                wrdRng.InsertAfter("A line of text");
                wrdRng.InsertParagraphAfter();
                oPos = wrdRng.get_Information
                               (Word.WdInformation.wdVerticalPositionRelativeToPage);
            }
            while (dPos >= Convert.ToDouble(oPos));
            object oCollapseEnd = Word.WdCollapseDirection.wdCollapseEnd;
            object oPageBreak = Word.WdBreakType.wdPageBreak;
            wrdRng.Collapse(ref oCollapseEnd);
            wrdRng.InsertBreak(ref oPageBreak);
            wrdRng.Collapse(ref oCollapseEnd);
            wrdRng.InsertAfter("We're now on page 2. Here's my chart:");
            wrdRng.InsertParagraphAfter();

            //Insert a chart.
            Word.InlineShape oShape;
            object oClassType = "MSGraph.Chart.8";
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            oShape = wrdRng.InlineShapes.AddOLEObject(ref oClassType, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing,
                ref oMissing, ref oMissing, ref oMissing);

            //Demonstrate use of late bound oChart and oChartApp objects to
            //manipulate the chart object with MSGraph.
            object oChart;
            object oChartApp;
            oChart = oShape.OLEFormat.Object;
            oChartApp = oChart.GetType().InvokeMember("Application",
                BindingFlags.GetProperty, null, oChart, null);

            //Change the chart type to Line.
            object[] Parameters = new Object[1];
            Parameters[0] = 4; //xlLine = 4
            oChart.GetType().InvokeMember("ChartType", BindingFlags.SetProperty,
                null, oChart, Parameters);

            //Update the chart image and quit MSGraph.
            oChartApp.GetType().InvokeMember("Update",
                BindingFlags.InvokeMethod, null, oChartApp, null);
            oChartApp.GetType().InvokeMember("Quit",
                BindingFlags.InvokeMethod, null, oChartApp, null);
            //... If desired, you can proceed from here using the Microsoft Graph 
            //Object model on the oChart and oChartApp objects to make additional
            //changes to the chart.

            //Set the width of the chart.
            oShape.Width = oWord.InchesToPoints(6.25f);
            oShape.Height = oWord.InchesToPoints(3.57f);

            //Add text after the chart.
            wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
            wrdRng.InsertParagraphAfter();
            wrdRng.InsertAfter("THE END.");

            //Close this form.
            this.Close();
        }

        private void ExportSqlTablesToWord1()
        {
            int index;
            string des;

            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            Word.Application oWord;
            Word.Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            object n = 1;
            Word.ListTemplate template = oWord.ListGalleries[Word.WdListGalleryType.wdNumberGallery].ListTemplates.get_Item(ref n);
            Word.ListLevel level = template.ListLevels[1];
            level.NumberFormat = "%1.";
            level.TrailingCharacter = Word.WdTrailingCharacter.wdTrailingTab;
            level.NumberStyle = Word.WdListNumberStyle.wdListNumberStyleArabic;
            level.NumberPosition = oWord.InchesToPoints(0.25f);
            level.Alignment = Word.WdListLevelAlignment.wdListLevelAlignLeft;
            level.TextPosition = oWord.InchesToPoints(0.5f);
            level.TabPosition = (float)Word.WdConstants.wdUndefined;
            level.ResetOnHigher = 0;
            level.StartAt = 1;

            foreach (TreeNode node in tvControl.Nodes)
            {
                //Get DataSet
                DataSet ds = GetInformationsOfTable(node.Text);
                DataView dvPrimaryKey = ds.Tables[1].DefaultView;
                DataView dvForeignKey = Procedure.Instance.GetForeignKeys(node.Text).Tables[0].DefaultView;
                DataView dvDescriptionTables = Procedure.Instance.GetDescriptionOfTables().Tables[0].DefaultView;
                DataView dvDescriptionColumns = Procedure.Instance.GetDescriptionOfColumns(node.Text).Tables[0].DefaultView;
                
                //oPara1.Range.set_Style
                //Insert a paragraph at the beginning of the document.
                Word.Paragraph oPara1;
                object styleHeading1 = "Heading 1";
                oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara1.Range.set_Style(ref styleHeading1);
                oPara1.Range.Text = node.Text;
                oPara1.Range.InsertParagraphBefore();
                oPara1.Range.Font.Bold = 1;
                oPara1.Range.InsertParagraphAfter();
                oPara1.Range.Font.Bold = 0;

                //Get description of the table
                dvDescriptionTables.Sort = "objname";
                index = dvDescriptionTables.Find(node.Text);
                if (index != -1)
                {
                    Word.Paragraph oPara2;
                    object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara2 = oDoc.Content.Paragraphs.Add(ref oRng);
                    //Set descriptions
                    oPara2.Range.Text = dvDescriptionTables[index]["value"].ToString();
                    oPara2.Range.InsertParagraphAfter();
                }

                //Insert a 3 x 5 table, fill it with data, and make the first row
                //bold and italic.
                Word.Table oTable;
                Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oTable = oDoc.Tables.Add(wrdRng, ds.Tables[0].Rows.Count + 1, 5, ref oMissing, ref oMissing);
                //Set border
                oTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                oTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                //Thay đổi kích thước column
                oTable.Columns[1].Width = oWord.CentimetersToPoints((float)1.27); //Change width of columns 1 & 2
                oTable.Columns[2].Width = oWord.CentimetersToPoints((float)4.28);
                oTable.Columns[3].Width = oWord.CentimetersToPoints((float)2.38);
                oTable.Columns[4].Width = oWord.CentimetersToPoints((float)1.75);
                oTable.Columns[5].Width = oWord.CentimetersToPoints((float)7.3);
                //oTable.Range.ParagraphFormat.SpaceAfter = 6;
                int stt = 1;
                //Create Header column
                oTable.Rows[1].Range.Font.Bold = 1;
                oTable.Cell(stt, 1).Range.Text = "TT";
                oTable.Cell(stt, 2).Range.Text = "Thuộc tính";
                oTable.Cell(stt, 3).Range.Text = "Kiểu dữ liệu";
                oTable.Cell(stt, 4).Range.Text = "Kích thước";
                oTable.Cell(stt, 5).Range.Text = "Mô tả";
                
                
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    stt++; //increase stt number
                    oTable.Rows[stt].Range.Font.Bold = 0;
                    oTable.Cell(stt, 1).Range.Text = (stt-1).ToString();
                    oTable.Cell(stt, 2).Range.Text = dr["COLUMN_NAME"].ToString();
                    oTable.Cell(stt, 3).Range.Text = dr["TYPE_NAME"].ToString();
                    oTable.Cell(stt, 4).Range.Text = dr["TYPE_NAME"].ToString().Contains("varchar") ? dr["PRECISION"].ToString() : dr["LENGTH"].ToString();
                    
                    //Check the column is a primary key
                    des = "";
                    dvPrimaryKey.Sort = "COLUMN_NAME";
                    index = dvPrimaryKey.Find(dr["COLUMN_NAME"]);
                    if (index != -1)
                        des = "Primary Key.";
                    
                    //Check the column is a foreign key
                    dvForeignKey.Sort = "ColumnName";
                    index = dvForeignKey.Find(dr["COLUMN_NAME"]);
                    if (index != -1)
                    {
                        if (!string.IsNullOrEmpty(des))
                            des += " ";
                        des += "Foreign Key (" + dvForeignKey[index]["ReferenceTableName"].ToString() + ").";
                    }

                    //Get description of the column
                    dvDescriptionColumns.Sort = "objname";
                    index = dvDescriptionColumns.Find(dr["COLUMN_NAME"]);
                    if (index != -1)
                    {
                        if (!string.IsNullOrEmpty(des))
                            des += " ";
                        des += dvDescriptionColumns[index]["value"].ToString();
                    }
                        
                    oTable.Cell(stt, 5).Range.Text = des;
                }
            }
        }

        private void ExportSqlTablesToWord2()
        {
            int index;
            string des;

            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            Word.Application oWord;
            Word.Document oDoc;
            oWord = new Word.Application();
            oWord.Visible = true;
            oDoc = oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            object n = 1;
            Word.ListTemplate template = oWord.ListGalleries[Word.WdListGalleryType.wdNumberGallery].ListTemplates.get_Item(ref n);
            Word.ListLevel level = template.ListLevels[1];
            level.NumberFormat = "%1.";
            level.TrailingCharacter = Word.WdTrailingCharacter.wdTrailingTab;
            level.NumberStyle = Word.WdListNumberStyle.wdListNumberStyleArabic;
            level.NumberPosition = oWord.InchesToPoints(0.25f);
            level.Alignment = Word.WdListLevelAlignment.wdListLevelAlignLeft;
            level.TextPosition = oWord.InchesToPoints(0.5f);
            level.TabPosition = (float)Word.WdConstants.wdUndefined;
            level.ResetOnHigher = 0;
            level.StartAt = 1;

            foreach (TreeNode node in tvControl.Nodes)
            {
                //Get DataSet
                DataSet ds = GetInformationsOfTable(node.Text);
                DataView dvPrimaryKey = ds.Tables[1].DefaultView;
                DataView dvForeignKey = Procedure.Instance.GetForeignKeys(node.Text).Tables[0].DefaultView;
                DataView dvDescriptionTables = Procedure.Instance.GetDescriptionOfTables().Tables[0].DefaultView;
                DataView dvDescriptionColumns = Procedure.Instance.GetDescriptionOfColumns(node.Text).Tables[0].DefaultView;

                //oPara1.Range.set_Style
                //Insert a paragraph at the beginning of the document.
                Word.Paragraph oPara1;
                object styleHeading1 = "Heading 1";
                oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara1.Range.set_Style(ref styleHeading1);
                oPara1.Range.Text = node.Text;
                oPara1.Range.InsertParagraphBefore();
                oPara1.Range.Font.Bold = 1;
                oPara1.Range.InsertParagraphAfter();
                oPara1.Range.Font.Bold = 0;

                //Get description of the table
                dvDescriptionTables.Sort = "objname";
                index = dvDescriptionTables.Find(node.Text);
                if (index != -1)
                {
                    Word.Paragraph oPara2;
                    object oRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    oPara2 = oDoc.Content.Paragraphs.Add(ref oRng);
                    //Set descriptions
                    oPara2.Range.Text = dvDescriptionTables[index]["value"].ToString();
                    oPara2.Range.InsertParagraphAfter();
                }

                //Insert a 3 x 5 table, fill it with data, and make the first row
                int colNums = 4;
                //bold and italic.
                Word.Table oTable;
                Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oTable = oDoc.Tables.Add(wrdRng, ds.Tables[0].Rows.Count + 1, colNums, ref oMissing, ref oMissing);
                //Set border
                oTable.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                oTable.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                oTable.Columns[1].Width = oWord.CentimetersToPoints((float)1.3); //Change width of columns 1 & 2
                oTable.Columns[2].Width = oWord.CentimetersToPoints((float)4.5);
                oTable.Columns[3].Width = oWord.CentimetersToPoints((float)3);
                oTable.Columns[4].Width = oWord.CentimetersToPoints((float)9);
                //oTable.Range.ParagraphFormat.SpaceAfter = 6;
                int stt = 1;
                //Create Header column
                oTable.Rows[1].Range.Font.Bold = 1;
                oTable.Cell(stt, 1).Range.Text = "TT";
                oTable.Cell(stt, 2).Range.Text = "Thuộc tính";
                oTable.Cell(stt, 3).Range.Text = "Kiểu dữ liệu";
                oTable.Cell(stt, 4).Range.Text = "Ghi chú";


                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    stt++; //increase stt number
                    oTable.Rows[stt].Range.Font.Bold = 0;
                    oTable.Cell(stt, 1).Range.Text = (stt - 1).ToString();
                    oTable.Cell(stt, 2).Range.Text = dr["COLUMN_NAME"].ToString();
                    string len = "";
                    if (dr["TYPE_NAME"].ToString().Contains("char") || dr["TYPE_NAME"].ToString().Contains("binary"))
                    len = "(" + dr["PRECISION"].ToString() + ")";
                    else if (dr["TYPE_NAME"].ToString().Contains("numeric") || dr["TYPE_NAME"].ToString().Contains("decimal"))
                        len = "(" + dr["PRECISION"].ToString() + ":" + dr["LENGTH"].ToString() + ")";
                    oTable.Cell(stt, 3).Range.Text = string.Format("{0}{1}", dr["TYPE_NAME"].ToString(), len);

                    //Check the column is a primary key
                    des = "";
                    dvPrimaryKey.Sort = "COLUMN_NAME";
                    index = dvPrimaryKey.Find(dr["COLUMN_NAME"]);
                    if (index != -1)
                        des = "Primary Key";

                    //Check the column is a foreign key
                    dvForeignKey.Sort = "ColumnName";
                    index = dvForeignKey.Find(dr["COLUMN_NAME"]);
                    if (index != -1)
                    {
                        if (!string.IsNullOrEmpty(des))
                            des += System.Environment.NewLine;;
                        des += "Foreign Key (" + dvForeignKey[index]["ReferenceTableName"].ToString() + ").";
                    }

                    //Get description of the column
                    dvDescriptionColumns.Sort = "objname";
                    index = dvDescriptionColumns.Find(dr["COLUMN_NAME"]);
                    if (index != -1)
                    {
                        if (!string.IsNullOrEmpty(des))
                            des += System.Environment.NewLine;
                        des += dvDescriptionColumns[index]["value"].ToString();
                    }

                    oTable.Cell(stt, 4).Range.Text = des;
                }
            }
        }

        /// <summary>
        /// Get informations of a table, including following infos:
        /// 1. Columns of a table
        /// 1. Primary keys
        /// 2. Foreign keys
        /// 3. Descriptions of columns
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetInformationsOfTable(string tableName)
        {
            string sql = "exec sp_columns" + " N'" + tableName + "'";
            //Primary keys
            sql += " " + "exec sp_pkeys" + " N'" + tableName + "'";
            DataSet ds = MITI.BaseDB.ExecSql_DataSet(sql);
            return ds;
        }

        private void btnExportToWord_Click(object sender, EventArgs e)
        {
            //SamleExportToWord();
            ExportSqlTablesToWord1();
            //vxcv
        }

        private void btnRepositoryCode_Click(object sender, EventArgs e)
        {

        }

        
        private void btnExportToWord2_Click(object sender, EventArgs e)
        {
            ExportSqlTablesToWord2();
        }
    }
}