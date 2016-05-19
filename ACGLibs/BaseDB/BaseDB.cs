using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlClient;

using Microsoft.SqlServer;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace MITI
{
    public class BaseDB
    {
        private static SqlConnection sqlConnect;
        public static string g_ConnectStr = "";     //Connect String
        public static SqlDataAdapter g_SqlDataAdapter;

        #region Old Code

        /// <summary>
        /// Ham khong dung bien username va password
        /// </summary>
        /// <param name="pFileSpec"></param>
        /// <returns></returns>
        public static string GetConnectStrFromFile(string pFileSpec)
        {
            string mCnnStr = "";
            try
            {
                StreamReader sr = new StreamReader(pFileSpec);
                string line;
                do
                {
                    line = sr.ReadLine();
                    if ((line != null))
                    {
                        string str = "Provider=SQLOLEDB.1;";
                        int index = line.IndexOf(str) + str.Length;
                        if (index == str.Length)
                        {
                            mCnnStr = line.Substring(index);
                            break; // TODO: might not be correct. Was : Exit Do
                        }
                    }
                }
                while (!(line == null));
                sr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mCnnStr;
        }

        public static void GanCauLenhKetNoi(string sUserName, string sPassword)
        {
            if (sUserName != "")
            {
                ThayGiaTri("User ID", sUserName);
            }
            if (sPassword != "")
            {
                ThayGiaTri("Password", sPassword);
            }
        }

        ///<summary>
        /// Dùng để thay thế giá trị trong chuỗi <para>strConnect</para>.
        /// Bien tra ve cho biet:   < 0 khong tim thay gia tri can thay
        ///                         > 0 tim thay gia tri can thay
        ///</summary>
        /// <param name="pName">Nơi bắt đầu cần thay thế</param>
        /// <param name="sGiaTri">Giá trị thay thế</param>
        public static int ThayGiaTri(string pName, string sGiaTri)
        {
            //Tim gia tri can thay
            int index = -1;
            string[] data = g_ConnectStr.Split(new Char[] { ';' });
            for (int i = 0; i <= data.GetUpperBound(0); i++)
            {
                if (data[i].IndexOf(pName) > -1)
                {
                    index = i;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }

            //Neu tim thay
            if (index >= 0)
            {
                //Thay the gia tri
                string[] data2 = data[index].Split(new Char[] { '=' });
                if (data2.GetUpperBound(0) > 0)
                {
                    data[index] = data2[0] + "=" + sGiaTri;
                }

                //noi chuoi lai sau khi da thay the
                g_ConnectStr = data[0];
                for (int i = 1; i <= data.GetUpperBound(0); i++)
                {
                    g_ConnectStr += ";" + data[i];
                }
            }
            return index;
        }

        public static string LayGiaTriThanhPhan(string pName)
        {
            string KQ = "";
            int index = -1;
            string[] data = g_ConnectStr.Split(new Char[] { ';' });
            for (int i = 0; i <= data.GetUpperBound(0); i++)
            {
                if (data[i].IndexOf(pName) > -1)
                {
                    index = i;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            string[] data2 = data[index].Split(new Char[] { '=' });
            if (data2.GetUpperBound(0) > 0)
            {
                KQ = data2[1];
            }
            return KQ;
        }

        //public static void OpenConnect()
        //{
        //    try
        //    {
        //        //Mo ket noi CSDL
        //        sqlConnect = new SqlConnection();
        //        if (g_ConnectStr == "")
        //        {
        //            g_ConnectStr = GetConnectStrFromFile("DBConnect.udl");//, g_UserName, g_Password);
        //        }
        //        sqlConnect.ConnectionString = g_ConnectStr;

        //        //GanCauLenhKetNoi(ref g_ConnectStr, CopyOfBaseDB.g_UserName, CopyOfBaseDB.g_Password);
        //        //sqlConnect.ConnectionString = g_ConnectStr;
        //        if (sqlConnect.State == ConnectionState.Closed)
        //        {
        //            sqlConnect.Open();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion


        #region Create Connection
        public static void OpenConnect()
        {
            try
            {
                //Mo ket noi CSDL
                sqlConnect = new SqlConnection();
                //if (g_ConnectStr == "")
                //{
                //    //g_ConnectStr = GetConnectStrFromFile("DBConnect.udl");//, g_UserName, g_Password);
                //    g_ConnectStr = GetConnectStrFromFile("DBConnect.udl");
                //}
                sqlConnect.ConnectionString = g_ConnectStr;

                //GanCauLenhKetNoi(ref g_ConnectStr, CopyOfBaseDB.g_UserName, CopyOfBaseDB.g_Password);
                //sqlConnect.ConnectionString = g_ConnectStr;
                if (sqlConnect.State == ConnectionState.Closed)
                {
                    sqlConnect.Open();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Close connect
        /// </summary>
        public static void CloseConnect()
        {
            try
            {
                if (sqlConnect.State == ConnectionState.Open)
                {
                    sqlConnect.Close();
                }
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Create Parameter
        /// <summary>
        /// Returns a SQL statement parameter name that is specific for the data provider.
        /// For example it returns ? for OleDb provider, or @paramName for MS SQL provider.
        /// </summary>
        /// <param name="paramName">The data provider neutral SQL parameter name.</param>
        /// <param name="bAddComma">Add a Comma</param>
        /// <returns>The SQL statement parameter name.</returns>
        public static string GetParamPlaceHolder(string paramName, bool bAddComma)
        {
            return "@" + paramName + (bAddComma ? "," : "");
        }

        /// <summary>
        /// Adds a new parameter to the specified command. It is not recommended that
        /// you use this method directly from your custom code. Instead use the
        /// <c>AddParameter</c> method of the &lt;TableCodeName&gt;Collection_Base classes.
        /// </summary>
        /// <param name="cmd">The <see cref="System.Data.SqlClien.SqlCommand"/> object to add the parameter to.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="dbType">One of the <see cref="System.Data.DbType"/> values. </param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>A reference to the added parameter.</returns>
        public static SqlParameter CreateParameter(ref SqlCommand cmd, string paramName, DbType dbType, object value)
        {
            SqlParameter parameter = cmd.CreateParameter();
            parameter.ParameterName = "@" + paramName;
            parameter.DbType = dbType;
            if (value == null)
            {
                parameter.Value = DBNull.Value;
            }
            else
            {
                parameter.Value = value;
            }
            cmd.Parameters.Add(parameter);
            return parameter;
        }
        #endregion

        #region Functions for Store Procedure
        /// <summary>
        /// Creates and returns a new <see cref="System.Data.SqlClient.SqlCommand"/> object.
        /// </summary>
        /// <param name="procedureName">The text of the query.</param>
        /// <param name="par">Specifies whether the sqlText parameter is
        /// the name of a stored procedure.</param>
        /// <returns>An <see cref="System.Data.SqlClient.SqlCommand"/> object.</returns>
        public static SqlCommand CreateCommand(string procedureName, params SqlParameter[] par)
        {
            //Thuc thi store procedure
            SqlCommand cmd = new SqlCommand();
            OpenConnect();
            cmd.CommandText = procedureName;
            cmd.Connection = sqlConnect;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlParameter p in par)
            {
                cmd.Parameters.Add(p);
            }
            return cmd;
        }

        public static SqlCommand ExecSqlProcedure_Command(string procedureName, params SqlParameter[] par)
        {
            SqlCommand cmd;
            cmd = CreateCommand(procedureName, par);
            try
            {
                cmd.ExecuteReader();
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cmd;
        }

        public static SqlCommand ExecSqlProcedure_Command(string procedureName)
        {
            SqlCommand cmd;
            cmd = CreateCommand(procedureName);
            try
            {
                cmd.ExecuteReader();
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cmd;
        }

        public static long ExecSqlProcedure_NoneQuery(string procedureName, params SqlParameter[] par)
        {
            SqlCommand cmd = null;
            long lRowsAffected = 0;
            try
            {
                cmd = CreateCommand(procedureName, par);
                cmd.Transaction = cmd.Connection.BeginTransaction();
                lRowsAffected = cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                cmd.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return lRowsAffected;
        }



        public static DataSet ExecSqlProcedure_DataSet(string procedureName, params SqlParameter[] par)
        {
            SqlCommand cmd = CreateCommand(procedureName, par);
            DataSet ds = new DataSet();
            try
            {
                g_SqlDataAdapter = new SqlDataAdapter(cmd);
                g_SqlDataAdapter.Fill(ds, procedureName);
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return ds;
        }

        public static DataSet ExecSqlProcedure_DataSet(string procedureName)
        {
            SqlCommand cmd = CreateCommand(procedureName);
            g_SqlDataAdapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                g_SqlDataAdapter.Fill(ds, procedureName);
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return ds;
        }

        public static SqlDataReader ExecSqlProcedure_DataReader(string procedureName, params SqlParameter[] par)
        {
            SqlCommand cmd;
            cmd = CreateCommand(procedureName, par);
            SqlDataReader myReader;
            try
            {
                myReader = cmd.ExecuteReader();
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myReader;
        }

        public static object ExecSqlProcedure_DataValue(string procedureName, params SqlParameter[] par)
        {
            SqlCommand cmd = null;
            object result;
            try
            {
                cmd = CreateCommand(procedureName, par);
                cmd.Transaction = cmd.Connection.BeginTransaction();
                result = cmd.ExecuteScalar();
                cmd.Transaction.Commit();
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                cmd.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return result;
        }
        #endregion

        #region Functions direct processing
        /// <summary>
        /// Creates and returns a new <see cref="System.Data.SqlClient.SqlCommand"/> object.
        /// </summary>
        /// <param name="sqlText">The text of the query.</param>
        /// <param name="procedure">Specifies whether the sqlText parameter is
        /// the name of a stored procedure.</param>
        /// <returns>An <see cref="System.Data.SqlClient.SqlCommand"/> object.</returns>
        public static SqlCommand CreateCommand(string sqlText, CommandType type)
        {
            //Thuc thi store procedure
            SqlCommand cmd = new SqlCommand();
            OpenConnect();
            cmd.CommandText = sqlText;
            cmd.Connection = sqlConnect;
            cmd.CommandType = type;
            return cmd;
        }

        public static long ExecSql_NoneQuery(string strSql)
        {
            SqlCommand cmd = null;
            long lRowsAffected = 0;
            try
            {
                cmd = CreateCommand(strSql, CommandType.Text);
                cmd.Transaction = cmd.Connection.BeginTransaction();
                lRowsAffected = cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                cmd.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return lRowsAffected;
        }

        public static SqlDataReader ExecSql_DataReader(string strSql)
        {
            SqlDataReader myReader;
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd = CreateCommand(strSql, CommandType.Text);
                myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myReader;
        }

        public static SqlDataAdapter ExecSql_SqlDataAdapter(string strSql)
        {
            try
            {
                SqlCommand cmd = null;
                cmd = CreateCommand(strSql, CommandType.Text);
                g_SqlDataAdapter = new SqlDataAdapter(cmd);
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
            return g_SqlDataAdapter;
        }

        /// <summary>
        /// Update data from dataset into database
        /// </summary>
        /// <param name="ds"></param>
        public static void ExecSql_UpdateDataSet(DataSet ds)
        {
            try
            {
                g_SqlDataAdapter.Update(ds);
                ds.AcceptChanges();
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
        }

        public static DataSet ExecSql_DataSet(string strSql)
        {
            SqlCommand cmd = null;
            DataSet ds = new DataSet();
            try
            {
                cmd = CreateCommand(strSql, CommandType.Text);
                g_SqlDataAdapter = new SqlDataAdapter(cmd);
                g_SqlDataAdapter.Fill(ds);
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return ds;
        }

        public static object ExecSql_DataValue(string strSql)
        {
            SqlCommand cmd = null;
            object result;
            try
            {
                cmd = CreateCommand(strSql, CommandType.Text);
                cmd.Transaction = cmd.Connection.BeginTransaction();
                result = cmd.ExecuteScalar();
                cmd.Transaction.Commit();
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                cmd.Transaction.Rollback();
                throw ex;
            }
            finally
            {
                cmd.Connection.Close();
            }
            return result;
        }

        /// <summary>
        /// Query from script files
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static object ExecSql_FromScriptFile(string filepath)
        {
            //Declare a ServerConnection object variable to specify SQL authentication, login and password.
            object result;
            try
            {
                FileInfo File = new FileInfo(filepath);
                string script = File.OpenText().ReadToEnd();
                result = ExecSql_MultiQueries(script);
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// Exec multi queries in a string query
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public static object ExecSql_MultiQueries(string query)
        {
            //Declare a ServerConnection object variable to specify SQL authentication, login and password.
            ServerConnection conn = new ServerConnection();
            object result;
            try
            {
                conn.ConnectionString = g_ConnectStr;
                Microsoft.SqlServer.Management.Smo.Server ser = new Microsoft.SqlServer.Management.Smo.Server(conn);
                result = ser.ConnectionContext.ExecuteNonQuery(query);
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Disconnect();
            }
            return result;
        }
        #endregion
    }
}