using System;
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
using ACGTool.Classes.LinqToSql;
using Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
//using DataTable = Microsoft.Office.Interop.Excel.DataTable;
using Excel = Microsoft.Office.Interop.Excel;

namespace ACGTool
{
    public partial class frmACG : Form
    {
        const string _FONT = "Courier New";
        const int _FONTSIZE = 10;

        private System.Data.DataTable _db;
        DataView dvPK; //chua toan bo khoa chinh

        public frmACG()
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

        public void LoadDataGridColumns()
        {
            string sql = "exec sp_columns" + " N'" + tvControl.SelectedNode.Text + "'";
            DataSet ds = MITI.BaseDB.ExecSql_DataSet(sql);
            _db = ds.Tables[0]; 
            //Filter dataview
            gridEXColumns.DataSource = _db.DefaultView;
            TongSoToolStripStatusLabel.Text = _db.Rows.Count.ToString() + " fields";
        }

        public void LoadDataGridPrimaryKeys()
        {
            //Load primary keys
            string sql = "exec sp_pkeys" + " N'" + tvControl.SelectedNode.Text + "'";
            DataSet ds = MITI.BaseDB.ExecSql_DataSet(sql);
            dvPK = ds.Tables[0].DefaultView;
            gridEXPrimaryKeys.DataSource = dvPK;
        }

        private void tvControl_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadDataGridColumns();
            LoadDataGridPrimaryKeys();
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

            dvFilter = _db.DefaultView;
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

        private void btnGenerateCode_Click(object sender, EventArgs e)
        {
            Procedure syn;
            if (comboBoxLanguage.SelectedIndex == 0)
            {
                RepositoryClass obj = new RepositoryClass_CSharp(tvControl.SelectedNode.Name, _db);
                richTextBoxClassInfo.AppendText(obj.CreateClass());
                //CSharp_ClassInfo();
                //Fore color
                syn = new ProcedureCSharp();
                syn.SetForeColorSyntaxes(ref richTextBoxClassInfo);
                syn.SetForeColorSyntaxes(ref richTextBoxClassController);
                
            }
            else
            {
                //VBDotNet
                RepositoryClass obj = new RepositoryClass_VB(tvControl.SelectedNode.Name, _db);
                richTextBoxClassInfo.AppendText(obj.CreateClass());
                //VBDotNet_ClassInfo();
                //VBDotNet_ClassController();
                
                //Fore color
                syn = new ProcedureVB();
                syn.SetForeColorSyntaxes(ref richTextBoxClassInfo);
                syn.SetForeColorSyntaxes(ref richTextBoxClassController);
            }
        }

        #endregion
    
    }
}