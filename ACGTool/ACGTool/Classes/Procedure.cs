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


namespace ACGTool.Classes
{
    public class Procedure
    {
        private static Procedure _instance;
        public static Procedure Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Procedure();
                return _instance;
            }
        }

        //Doan code nay dung de set width cua column cua Datagrid va xuat ra file
        public void OutputText(DataGridView gridObject)
        {
            FileInfo t = new FileInfo("CodeOutput.txt");
            StreamWriter Tex = t.CreateText();
            string str = "";
            Tex.WriteLine(gridObject.Width.ToString());
            Tex.Write(Tex.NewLine);
            foreach (DataGridViewColumn column in gridObject.Columns)
            {
                str = gridObject.Name + ".RootTable.Columns[\"" + column.Name + "\"].Width =" + column.Width + ";";
                Tex.WriteLine(str);
                Tex.Write(Tex.NewLine);
            }
            Tex.Close();
        }

        /// <summary>
        /// Get description of a column in a table
        /// </summary>
        /// <param name="table_name"></param>
        /// <param name="column_name"></param>
        /// <returns></returns>
        public string GetDescription(string table_name, string column_name)
        {
            string _description = "";
            string sql = "SELECT * FROM ::fn_listextendedproperty (NULL, N'user', N'dbo', N'table', N'" + table_name + "', N'column', N'" + column_name + "')";
            DataSet ds = MITI.BaseDB.ExecSql_DataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
                _description = ds.Tables[0].Rows[0]["value"].ToString();
            return _description;
        }

        /// <summary>
        /// Get descriptions of all columns in a table
        /// </summary>
        /// <param name="table_name"></param>
        /// <returns></returns>
        public DataSet GetDescriptionOfColumns(string table_name)
        {
            string sql = "SELECT * FROM ::fn_listextendedproperty (NULL, N'user', N'dbo', N'table', N'" + table_name + "', N'column', NULL)";
            DataSet ds = MITI.BaseDB.ExecSql_DataSet(sql);
            return ds;
        }

        /// <summary>
        /// Get descriptions of all tables in the database
        /// </summary>
        /// <returns></returns>
        public DataSet GetDescriptionOfTables()
        {
            string sql = "SELECT * FROM ::fn_listextendedproperty (NULL, N'user', N'dbo', N'table', NULL, NULL, NULL)";
            DataSet ds = MITI.BaseDB.ExecSql_DataSet(sql);
            return ds;
        }

        /// <summary>
        /// Get all foreign keys of a table
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataSet GetForeignKeys(string tableName)
        {
            string sql = "SELECT f.name AS ForeignKey, OBJECT_NAME(f.parent_object_id) AS TableName, COL_NAME(fc.parent_object_id,";
            sql += "fc.parent_column_id) AS ColumnName, OBJECT_NAME (f.referenced_object_id) AS ReferenceTableName,";
            sql += "COL_NAME(fc.referenced_object_id, fc.referenced_column_id) AS ReferenceColumnName FROM sys.foreign_keys AS f";
            sql += " INNER JOIN sys.foreign_key_columns AS fc ON f.OBJECT_ID = fc.constraint_object_id";
            sql += " where OBJECT_NAME(f.parent_object_id) = '" + tableName + "'";
            DataSet ds = MITI.BaseDB.ExecSql_DataSet(sql);
            return ds;
        }

        public virtual string GetTypeMap(string typedb)
        {
            return string.Empty;
        }

        public virtual string GetTypeMap(string type_name, int precision, object scale, int length)
        {
            return string.Empty;
        }
        
        /// <summary>
        /// Hàm kiểm tra xem column có set Identity không
        /// </summary>
        /// <param name="typedb"></param>
        /// <returns></returns>
        public bool IsIdentity(string typedb)
        {
            return typedb.Contains("identity");
        }

        #region SetColor
        // To mau cho syntax trong toan bo ma nguon
        public void SetForeColorTexts(ref RichTextBox richTextBox1, string searchText, int searchStart, int searchEnd, Color valueColor)
        {
            // Ensure that a search string and a valid starting point are specified.
            if (searchText.Length > 0 && searchStart >= 0)
            {
                // Ensure that a valid ending value is provided.
                if (searchEnd > searchStart || searchEnd == -1)
                {
                    // Obtain the location of the search string in richTextBox1.
                    int indexToText = richTextBox1.Find(searchText, searchStart, searchEnd, RichTextBoxFinds.WholeWord);
                    // Determine whether the text was found in richTextBox1.
                    if (indexToText >= 0)
                    {
                        // Return the index to the specified search text.
                        richTextBox1.SelectionColor = valueColor;
                        SetForeColorTexts(ref richTextBox1, searchText, indexToText + 1, searchEnd, valueColor);
                    }
                }
            }
        }

        //Xu ly to mau xanh cho chuoi ky tu ben phai dau --, //, /* */, 
        public void SetForeColor(ref RichTextBox richTextBox1, int searchStart, string valueStart, string valueEnd, Color valueColor)
        {
            int indexS = richTextBox1.Text.IndexOf(valueStart, searchStart);
            int indexE = richTextBox1.Text.IndexOf(valueEnd, indexS + 1);
            if (indexE > indexS && indexS >= 0)
            {
                string searchText = richTextBox1.Text.Substring(indexS, indexE - indexS);
                int indexToText = richTextBox1.Find(searchText, searchStart, -1, RichTextBoxFinds.WholeWord);
                if (indexToText >= 0)
                {
                    // Return the index to the specified search text.
                    richTextBox1.SelectionColor = valueColor;
                    SetForeColor(ref richTextBox1, indexE + 1, valueStart, valueEnd, valueColor);
                }
            }
        }
        public virtual void SetForeColorSyntaxes(ref RichTextBox richTextBox1)
        {
            return;
        }
#endregion
    }

    public class ProcedureVB : Procedure
    {
        public string[] _keyWords = {
            //VB.NET syntax
            "Class"
            
            ,"Imports"

            ,"Public"
            ,"Private"
            ,"Protected"
            ,"Region"
            ,"End"
            ,"Return"
            ,"Sub"
            ,"Function"
            ,"Select"
            ,"Case"

            ,"As"
            ,"Property"
            ,"Get"
            ,"Set"
            
            ,"ByRef"
            ,"ByVal"

            //VB.NET type
            ,"Integer"
            ,"Int16"
            ,"Int32"
            ,"Int64"
            ,"String"
            ,"Date"
            ,"DateTime"
            ,"Boolean"
            ,"Single"
            ,"Short"
            ,"Double"

            ,"IsNot" 
            ,"Nothing"
            ,"If"
            ,"Else"
            ,"Then"
            ,"Handles"
            ,"Dim"
            ,"Const"
            ,"Overrides"
            ,"MustOverride"
            ,"Object"
            ,"CType"
            ,"GetType"
            ,"MustInherit"
            ,"Shared"
            ,"Shadows"
        };

        public override string GetTypeMap(string typedb)
        {
            string[] temp;
            temp = typedb.Split(' ');
            typedb = temp[0];
            string stype = "";
            switch (typedb)
            {
                case "nvarchar":
                    stype = "String";
                    break;
                case "int":
                    stype = "Integer";
                    break;
                case "datetime":
                case "smalldatetime":
                    stype = "System.DateTime";
                    break;
                case "smallint":
                case "tinyint":
                    stype = "System.Int16";
                    break;
                case "float":
                case "numeric":
                    stype = "Single";
                    break;
                case "bit":
                    stype = "Boolean";
                    break;
                case "image":
                    stype = "Byte()";
                    break;
            }
            return stype;
        }

        public override void SetForeColorSyntaxes(ref RichTextBox richTextBox1)
        {
            for (int i = 0; i < _keyWords.Length; i++)
            {
                SetForeColorTexts(ref richTextBox1, _keyWords[i], 0, -1, Color.Blue);
            }
            SetForeColor(ref richTextBox1, 0, "\"", "\"", Color.Red);
            SetForeColor(ref richTextBox1, 0, "\'", "\n", Color.Green);
            SetForeColor(ref richTextBox1, 0, "\'''", "\n", Color.Gray);
        }
    }

    public class ProcedureSQL : Procedure
    {
        public string[] _keyWords = {
            
            //SQL syntax
            "procedure"
            ,"select"
            ,"create"
            ,"alter"
            ,"insert into"
            ,"update"
            ,"delete"
            ,"as"
            ,"begin"
            ,"end"
            ,"from"
            ,"where"
            ,"set"
            ,"values"

            //SQL Type
            ,"int"
            ,"real"
            ,"float"
            ,"decimal"
            ,"bigint"
            ,"smalldatetime"
            ,"tinyint"
            ,"nvarchar"
            ,"smallint"
            ,"varchar"
            ,"bit"
            ,"binary"
            ,"char"
            ,"image"
            ,"money"
            ,"smallmoney"
            ,"ntext"
            ,"text"
            ,"varbinary"
        };

        public override string GetTypeMap(string type_name, int precision, object scale, int length)
        {
            string[] temp;
            temp = type_name.Split(' ');
            type_name = temp[0];
            string stype = "";
            switch (type_name)
            {
                case "nvarchar":
                case "varchar":
                    stype = type_name + "(" + precision.ToString() + ")";
                    break;
                case "numeric":
                case "decimal":
                    stype = type_name + "(" + precision.ToString() + "," + scale.ToString() + ")";
                    break;
                case "int":
                case "tinyint":
                case "smallint":
                case "datetime":
                case "smalldatetime":
                case "float":
                case "bit":
                case "image":
                    stype = type_name;
                    break;
            }
            return stype;
        }

        public override void SetForeColorSyntaxes(ref RichTextBox richTextBox1)
        {
            for (int i = 0; i < _keyWords.Length; i++)
            {
                SetForeColorTexts(ref richTextBox1, _keyWords[i], 0, -1, Color.Blue);
            }
            SetForeColor(ref richTextBox1, 0, "'", "'", Color.Red);
            SetForeColor(ref richTextBox1, 0, "//", "\n", Color.Green);
            SetForeColor(ref richTextBox1, 0, "--", "\n", Color.Green);
            SetForeColor(ref richTextBox1, 0, "/*", "*/", Color.Green);
        }
    }

    public class ProcedureCSharp : Procedure
    {
        public string[] _keyWords = {
            //VB.NET syntax
            "class"
            
            ,"using"

            ,"public"
            ,"private"
            ,"protected"
            ,"region"
            ,"return"
            ,"void"
            ,"switch"
            ,"case"

            ,"property"
            ,"get"
            ,"set"
            
            ,"ref"

            //VB.NET type
            ,"int"
            ,"Int16"
            ,"Int32"
            ,"Int64"
            ,"string"
            ,"date"
            ,"dateTime"
            ,"bool"
            ,"float"
            ,"short"
            ,"double"

            ,"null"
            ,"if"
            ,"else"
            ,"handles"
            ,"const"
            ,"override"
            ,"object"
        };

        public override string GetTypeMap(string typedb)
        {
            string[] temp;
            temp = typedb.Split(' ');
            typedb = temp[0];
            string stype = "";
            switch (typedb)
            {
                case "nvarchar":
                    stype = "string";
                    break;
                case "int":
                    stype = "int";
                    break;
                case "datetime":
                case "smalldatetime":
                    stype = "System.DateTime";
                    break;
                case "smallint":
                case "tinyint":
                    stype = "System.Int16";
                    break;
                case "float":
                case "numeric":
                    stype = "float";
                    break;
                case "bit":
                    stype = "bool";
                    break;
                //case "image":
                //    stype = "Byte()";
                //break;
            }
            return stype;
        }


        public override void SetForeColorSyntaxes(ref RichTextBox richTextBox1)
        {
            for (int i = 0; i < _keyWords.Length; i++)
            {
                SetForeColorTexts(ref richTextBox1, _keyWords[i], 0, -1, Color.Blue);
            }
            SetForeColor(ref richTextBox1, 0, "\"", "\"", Color.Red);
            SetForeColor(ref richTextBox1, 0, "//", "\n", Color.Green);
            SetForeColor(ref richTextBox1, 0, "///", "\n", Color.Gray);
        }
    }
}
