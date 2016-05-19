using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACGTool
{
    public partial class frmReadAccess : Form
    {
        private string connString = "";
        public frmReadAccess()
        {
            InitializeComponent();
        }

        //public void ReadAccess(string fileDB)
        //{
        //    OleDbConnection co;
        //    DataTable db = new DataTable();
        //    try
        //    {
        //        co = new OleDbConnection(string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", fileDB));
        //        co.Open();
        //        OleDbCommand cmd = new OleDbCommand("SELECT * FROM tblAirport");
        //        OleDbDataReader reader = cmd.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            reader.Read();
        //            int len = reader.FieldCount;
        //            for (int i = 0; i < len; i++)
        //            {
        //                db.Columns.Add(reader.GetName(i));
        //            }

        //            do
        //            {
        //                DataRow dr = db.NewRow();
        //                for (int i = 0; i < len; i++)
        //                {
        //                    dr[i] = reader.GetString(i);
        //                }
        //                db.Rows.Add(dr);

        //            } while (reader.Read());

        //            BindingSource bsGPSData = new BindingSource();
        //            bsGPSData.DataSource = db.DefaultView;
        //            dataGridViewAirport.DataSource = bsGPSData;
        //            bindingNavigator1.BindingSource = bsGPSData;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(string.Format("Exception: {0} {1}", ex.Message, ex.StackTrace));
        //    }
            
        //}

        /// <summary>
        /// Lấy dữ liệu của một table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetTableData(string tableName)
        {
            DataTable results = new DataTable();
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    OleDbCommand cmd = new OleDbCommand(string.Format("SELECT * FROM {0}", tableName), conn);

                    conn.Open();

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    adapter.Fill(results);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }

        /// <summary>
        /// Lấy danh sách table của file .mdb
        /// </summary>
        /// <returns></returns>
        public DataTable GetTableList()
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory("System.Data.OleDb");
            DataTable results = new DataTable();
            try
            {
                using (DbConnection connection = factory.CreateConnection()) 
                {
                    connection.ConnectionString = connString;
                    // We only want user tables, not system tables
                    string[] restrictions = new string[4];
                    restrictions[3] = "Table";

                    connection.Open();

                    // Get list of user tables
                    results = connection.GetSchema("Tables", restrictions);

                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return results;
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                //mo form cho nguoi ta chon co so du lieu
                OpenFileDialog1.Filter = "Access File(*.mdb)|*.mdb";
                txtFilePath.Text = "";
                OpenFileDialog1.ShowDialog();
                txtFilePath.Text = OpenFileDialog1.FileName;
                connString = string.Format(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", txtFilePath.Text);

                GetTableListing();
                //DataTable db = GetTableList();
                //cmbTables.DisplayMember = "TABLE_NAME";
                //cmbTables.ValueMember = "TABLE_NAME";
                //cmbTables.DataSource = db;

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Exception: {0} {1}", ex.Message, ex.StackTrace));
            }
        }

        private void cmbTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbTables.SelectedIndex >= 0)
                {
                    //Load Design
                    GetColumnListing(cmbTables.SelectedValue.ToString());

                    //Load data
                    DataTable db = GetTableData(cmbTables.SelectedValue.ToString());

                    for (int i = 0; i < db.Columns.Count; i++)
                    {
                        //Kiểu dữ liệu Byte[] khi load vào datagridview sẽ bị báo lỗi
                        //Do đó, thay thế kiểu Byte[] bằng kiểu string để không bị lỗi
                        if (db.Columns[i].DataType.Name == "Byte[]")
                        {
                            //Tạo column thay thế với kiểu là string
                            DataColumn c = new DataColumn(db.Columns[i].ColumnName, System.Type.GetType("System.String"));
                            c.DefaultValue = "Long Binary Data";
                            //Remove column kiểu Byte[]
                            db.Columns.RemoveAt(i);
                            //Add column kiểu string vửa tạo
                            db.Columns.Add(c);
                        }
                    }
                    
                    BindingSource bsGPSData = new BindingSource();
                    bsGPSData.DataSource = db.DefaultView;
                    dataGridViewData.DataSource = bsGPSData;
                    bindingNavigator1.BindingSource = bsGPSData;
                }
            }
            catch (OleDbException ex)
            {
                ErrorsLabel.Text = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                ErrorsLabel.Text = ex.Message;
            }
            catch (ArgumentException ex)
            {
                ErrorsLabel.Text = ex.Message;
            }
            
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            List<string> export = new List<string>();
            foreach (DataGridViewRow dr in dataGridViewData.Rows)
            {
               export.Add(string.Format("insert into airport(airport_name,longitude,latitude) values('{0}','{1}','{2}')",
                              dr.Cells["Name"].Value, dr.Cells["PosX"].Value, dr.Cells["PosY"].Value));
            }
            string content = string.Join("\n", export.ToArray());
            Clipboard.SetText(content);
            MessageBox.Show("Export has been copied to clipboard!");
        }

        //private void ReadTableInfo1()
        //{
        //    OleDbConnection con = new OleDbConnection(connString);
        //    con.Open();
            
        //    System.Data.DataTable tables = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
        //    foreach (DataColumn col in tables.Columns)
        //        Console.WriteLine("{0}\t{1}", col.ColumnName, col.DataType);

        //    con.Close();
        //}

        //private void ReadTableInfo2()
        //{
        //    OleDbConnection con = new OleDbConnection(connString);
        //    con.Open();

        //    System.Data.DataTable db = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
        //    BindingSource bsGPSData = new BindingSource();
        //    bsGPSData.DataSource =  db.DefaultView;
        //    dataGridViewData.DataSource = bsGPSData;
        //    bindingNavigator1.BindingSource = bsGPSData;

        //    con.Close();
        //}

        private void GetColumnListing(string tableName)
        {
            try
            {
                OleDbConnection con = new OleDbConnection(connString);
                con.Open();
                DataTable dbSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Columns,
                                                             new object[] { null, null, tableName, null });

                BindingSource bsGPSData = new BindingSource();
                bsGPSData.DataSource = dbSchema.DefaultView;
                dataGridViewDesign.DataSource = bsGPSData;
                bindingNavigatorDesign.BindingSource = bsGPSData;

                foreach (DataRow dr in dbSchema.Rows)
                {
                    //ConvertToJetDataType(int.Parse(dr["DATA_TYPE"].ToString()));
                    ConvertToJetDataType((int)dr["DATA_TYPE"]);
                }
                con.Close();
            }
            catch (OleDbException ex)
            {
                ErrorsLabel.Text = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                ErrorsLabel.Text = ex.Message;
            }
            catch (ArgumentException ex)
            {
                ErrorsLabel.Text = ex.Message;
            }
            finally
            {

            }
        }

        private void GetTableListing()
        {
            try
            {
                OleDbConnection con = new OleDbConnection(connString);
                con.Open();
                DataTable dbSchema = con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,
                 new object[] { null, null, null, "TABLE" });

                cmbTables.DisplayMember = "TABLE_NAME";
                cmbTables.ValueMember = "TABLE_NAME";
                cmbTables.DataSource = dbSchema;
                con.Close();
            }
            catch (OleDbException ex)
            {
                ErrorsLabel.Text = ex.Message;
            }
            catch (InvalidOperationException ex)
            {
                ErrorsLabel.Text = ex.Message;
            }
            catch (ArgumentException ex)
            {
                ErrorsLabel.Text = ex.Message;
            }
        }

        private static string ConvertToJetDataType(int oleDbDataType)
        {
            switch (((OleDbType)oleDbDataType))
            {
                case OleDbType.LongVarChar:
                    return "varchar";
                case OleDbType.BigInt:
                    return "int";       // In Jet this is 32 bit while bigint is 64 bits
                case OleDbType.Binary:
                case OleDbType.LongVarBinary:
                    return "binary";
                case OleDbType.Boolean:
                    return "bit";
                case OleDbType.Char:
                    return "char";
                case OleDbType.Currency:
                    return "decimal";
                case OleDbType.DBDate:
                case OleDbType.Date:
                case OleDbType.DBTimeStamp:
                    return "datetime";
                case OleDbType.Decimal:
                case OleDbType.Numeric:
                    return "decimal";
                case OleDbType.Double:
                    return "double";
                case OleDbType.Integer:
                    return "int";
                case OleDbType.Single:
                    return "single";
                case OleDbType.SmallInt:
                    return "smallint";
                case OleDbType.TinyInt:
                    return "smallint";  // Signed byte not handled by jet so we need 16 bits
                case OleDbType.UnsignedTinyInt:
                    return "byte";
                case OleDbType.VarBinary:
                    return "varbinary";
                case OleDbType.VarChar:
                    return "varchar";
                case OleDbType.WChar:
                    return "Text";
                case OleDbType.BSTR:
                case OleDbType.Variant:
                case OleDbType.VarWChar:
                case OleDbType.VarNumeric:
                case OleDbType.Error:
                case OleDbType.DBTime:
                case OleDbType.Empty:
                case OleDbType.Filetime:
                case OleDbType.Guid:
                case OleDbType.IDispatch:
                case OleDbType.IUnknown:
                case OleDbType.UnsignedBigInt:
                case OleDbType.UnsignedInt:
                case OleDbType.UnsignedSmallInt:
                case OleDbType.PropVariant:
                default:
                    throw new ArgumentException(string.Format("The data type {0} is not handled by Jet. Did you retrieve this from Jet?", ((OleDbType)oleDbDataType)));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ReadTableInfo1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //ReadTableInfo2();
        }
    }
}
