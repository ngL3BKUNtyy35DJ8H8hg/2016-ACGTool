using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Data.SqlServerCe;

namespace MITI
{
    public class BaseDBCe
    {
        private static SqlCeConnection sqlConnect;
        public static string g_ConnectStr = "";     //Connect String
        public static SqlCeDataAdapter g_SqlCeDataAdapter;
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
        /// <param name="cmd">The <see cref="System.Data.SqlClien.SqlCeCommand"/> object to add the parameter to.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="dbType">One of the <see cref="System.Data.DbType"/> values. </param>
        /// <param name="value">The value of the parameter.</param>
        /// <returns>A reference to the added parameter.</returns>
        public static SqlCeParameter CreateParameter(ref SqlCeCommand cmd, string paramName, DbType dbType, object value)
        {
            SqlCeParameter parameter = cmd.CreateParameter();
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

        #region Create Connection
        
        ///// <summary>
        ///// Upgrade database version
        ///// </summary>
        //public static void Upgrade()
        //{
        //    SqlCeEngine ce = new SqlCeEngine(g_ConnectStr);
        //    ce.Upgrade(g_ConnectStr);
        //}

        public static void OpenConnect()
        {
            try
            {
                //Mo ket noi CSDL
                sqlConnect = new SqlCeConnection();
                sqlConnect.ConnectionString = g_ConnectStr;

                if (sqlConnect.State == ConnectionState.Closed)
                {
                    sqlConnect.Open();
                }
            }
            catch (SqlCeException sqlex)
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
            catch (SqlCeException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Functions for Store Procedure

        /// <summary>
        /// Creates and returns a new <see cref="System.Data.SqlClient.SqlCeCommand"/> object.
        /// </summary>
        /// <param name="procedureName">The text of the query.</param>
        /// <param name="par">Specifies whether the sqlText parameter is
        /// the name of a stored procedure.</param>
        /// <returns>An <see cref="System.Data.SqlClient.SqlCeCommand"/> object.</returns>
        public static SqlCeCommand CreateCommand(string procedureName, params SqlCeParameter[] par)
        {
            //Thuc thi store procedure
            SqlCeCommand cmd = new SqlCeCommand();
            OpenConnect();
            cmd.CommandText = procedureName;
            cmd.Connection = sqlConnect;
            cmd.CommandType = CommandType.StoredProcedure;
            foreach (SqlCeParameter p in par)
            {
                cmd.Parameters.Add(p);
            }
            return cmd;
        }

        public static SqlCeCommand ExecSqlProcedure_Command(string procedureName, params SqlCeParameter[] par)
        {
            SqlCeCommand cmd;
            cmd = CreateCommand(procedureName, par);
            try
            {
                cmd.ExecuteReader();
            }
            catch (SqlCeException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cmd;
        }

        public static SqlCeCommand ExecSqlProcedure_Command(string procedureName)
        {
            SqlCeCommand cmd;
            cmd = CreateCommand(procedureName);
            try
            {
                cmd.ExecuteReader();
            }
            catch (SqlCeException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cmd;
        }

        public static long ExecSqlProcedure_NoneQuery(string procedureName, params SqlCeParameter[] par)
        {
            SqlCeCommand cmd = null;
            long lRowsAffected = 0;
            try
            {
                cmd = CreateCommand(procedureName, par);
                cmd.Transaction = cmd.Connection.BeginTransaction();
                lRowsAffected = cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
            }
            catch (SqlCeException sqlex)
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

        public static DataSet ExecSqlProcedure_DataSet(string procedureName, params SqlCeParameter[] par)
        {
            SqlCeCommand cmd;
            cmd = CreateCommand(procedureName,par);
            g_SqlCeDataAdapter = new SqlCeDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                g_SqlCeDataAdapter.Fill(ds, procedureName);
            }
            catch (SqlCeException sqlex)
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
            SqlCeCommand cmd;
            cmd = CreateCommand(procedureName);
             g_SqlCeDataAdapter = new SqlCeDataAdapter(cmd);
            DataSet ds = new DataSet();
            try
            {
                g_SqlCeDataAdapter.Fill(ds, procedureName);
            }
            catch (SqlCeException sqlex)
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

        public static SqlCeDataReader ExecSqlProcedure_DataReader(string procedureName, params SqlCeParameter[] par)
        {
            SqlCeCommand cmd;
            cmd = CreateCommand(procedureName, par);
            SqlCeDataReader myReader;
            try
            {
                myReader = cmd.ExecuteReader();
            }
            catch (SqlCeException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return myReader;
        }

        public static object ExecSqlProcedure_DataValue(string procedureName, params SqlCeParameter[] par)
        {
            SqlCeCommand cmd = null;
            object result;
            try
            {
                cmd = CreateCommand(procedureName, par);
                cmd.Transaction = cmd.Connection.BeginTransaction();
                result = cmd.ExecuteScalar();
                cmd.Transaction.Commit();
            }
            catch (SqlCeException sqlex)
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
        /// Creates and returns a new <see cref="System.Data.SqlClient.SqlCeCommand"/> object.
        /// </summary>
        /// <param name="sqlText">The text of the query.</param>
        /// <param name="procedure">Specifies whether the sqlText parameter is
        /// the name of a stored procedure.</param>
        /// <returns>An <see cref="System.Data.SqlClient.SqlCeCommand"/> object.</returns>
        public static SqlCeCommand CreateCommand(string sqlText, CommandType type)
        {
            SqlCeCommand cmd = new SqlCeCommand();
            try
            {
                //Thuc thi store procedure
                OpenConnect();
                cmd.CommandText = sqlText;
                cmd.Connection = sqlConnect;
                cmd.CommandType = type;
            }
            catch (SqlCeException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return cmd;
        }

        public static long ExecSql_NoneQuery(string strSql)
        {
            SqlCeCommand cmd = null;
            long lRowsAffected = 0;
            try
            {
                cmd = CreateCommand(strSql, CommandType.Text);
                cmd.Transaction = cmd.Connection.BeginTransaction();
                lRowsAffected = cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
            }
            catch (SqlCeException sqlex)
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

        public static SqlCeDataReader ExecSql_DataReader(string strSql)
        {
            SqlCeDataReader myReader;
            try
            {
                SqlCeCommand cmd = new SqlCeCommand();
                cmd = CreateCommand(strSql, CommandType.Text);
                myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (SqlCeException sqlex)
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
            return myReader;
        }

        public static SqlCeResultSet ExecSql_GetTable(string tableName)
        {
            SqlCeCommand cmd = null;
            SqlCeResultSet rs;
            try
            {
                cmd = CreateCommand(tableName, CommandType.TableDirect);
                // Get the table
                rs = cmd.ExecuteResultSet(ResultSetOptions.Updatable | 
                ResultSetOptions.Scrollable);
            }
            catch (SqlCeException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // Note, do not close the connection,
                // if you do the grid won't be able to display.
                // For production code you probably want to make
                // your result set (rs) a class level variable

                //cmd.Connection.Close();
                
            }
            return rs;
            
        }

        public static SqlCeDataAdapter ExecSql_SqlCeDataAdapter(string strSql)
        {
            SqlCeCommand cmd = null;
            try
            {
                cmd = CreateCommand(strSql, CommandType.Text);
                g_SqlCeDataAdapter = new SqlCeDataAdapter(cmd);
            }
            catch (SqlCeException sqlex)
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
            return g_SqlCeDataAdapter;
        }
        
        
        public static DataSet ExecSql_DataSet(string strSql)
        {
            SqlCeCommand cmd = null;
            DataSet ds = new DataSet();
            try
            {
                cmd = CreateCommand(strSql, CommandType.Text);
                g_SqlCeDataAdapter = new SqlCeDataAdapter(cmd);
                g_SqlCeDataAdapter.Fill(ds);
            }
            catch (SqlCeException sqlex)
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

        /// <summary>
        /// Update data from dataset into database
        /// </summary>
        /// <param name="ds"></param>
        public static void ExecSql_UpdateDataSet(DataSet ds)
        {
            try
            {
                g_SqlCeDataAdapter.Update(ds);
                ds.AcceptChanges();
            }
            catch (SqlCeException sqlex)
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


        public static object ExecSql_DataValue(string strSql)
        {
            SqlCeCommand cmd = null;
            object result;
            try
            {
                cmd = CreateCommand(strSql, CommandType.Text);
                cmd.Transaction = cmd.Connection.BeginTransaction();
                result = cmd.ExecuteScalar();
                cmd.Transaction.Commit();
            }
            catch (SqlCeException sqlex)
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

        
    }
}