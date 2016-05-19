using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Net;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;


namespace HTLibrary
{
    public class IPInfo
    {
        public string HostName;
        public string Ip;
    }

    public static class CollectionHelper
    {
        /// <summary>
        /// Chuyển đổi kiểu List sang DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                table.Rows.Add(row);
            }

            return table;
        }

        private static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            return table;
        }

        /// <summary>
        /// Lấy địa chỉ ip máy
        /// </summary>
        /// <returns></returns>
        public static IPInfo GetIP()
        {
            IPInfo objIpInfo = new IPInfo();
            objIpInfo.HostName = System.Net.Dns.GetHostName();

            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(objIpInfo.HostName);

            IPAddress[] addr = ipEntry.AddressList;

            foreach (IPAddress ipAddress in addr)
            {
                if (IsValidIP(ipAddress.ToString()))
                    objIpInfo.Ip = ipAddress.ToString();
            }

            return objIpInfo;
        }

        /// <summary>
        /// Kiểm tra chuỗi ip đưa vào có hợp lệ không?
        /// </summary>
        /// <param name="ipaddr"></param>
        /// <returns></returns>
        private static bool IsValidIP(string ipaddr)
        {
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])" +
            @"(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";

            Regex check = new Regex(pattern);
            bool valid = false;

            if (ipaddr == "")
            {
                valid = false;
            }
            else
            {
                valid = check.IsMatch(ipaddr, 0);
            }

            return valid;
        }

        /// <summary>
        /// Kiểm tra trạng thái service
        /// </summary>
        /// <param name="SERVICENAME"></param>
        /// <returns></returns>
        public static string CheckService(string SERVICENAME)
        {
            ServiceController sc = new ServiceController(SERVICENAME);

            switch (sc.Status)
            {
                case ServiceControllerStatus.Running:
                    return "Running";
                case ServiceControllerStatus.Stopped:
                    return "Stopped";
                case ServiceControllerStatus.Paused:
                    return "Paused";
                case ServiceControllerStatus.StopPending:
                    return "Stopping";
                case ServiceControllerStatus.StartPending:
                    return "Starting";
                default:
                    return "Status Changing";
            }
        }

        //public readonly static string FORMAT_MONEY = "#,##0.##";
        //public static string FormatMoneyStyle(string value)
        //{
        //    string strFormat = "{0:" + FORMAT_MONEY + "}";
        //    return String.Format(strFormat, value);
        //}

        public static readonly string FORMAT_QUANTITY = "#,##0.##";
        //public static string FormatQuantityStyle(string value)
        //{
        //    //return value.ToString("### ### ##0.00", CultureInfo.InvariantCulture).TrimStart();
        //    string strFormat = "{0:" + FORMAT_QUANTITY + "}";
        //    return String.Format(strFormat, value);
        //}

        public static string FormatQuantityStyle(decimal value)
        {
            //return value.ToString("### ### ##0.00", CultureInfo.InvariantCulture).TrimStart();
            string strFormat = "{0:" + FORMAT_QUANTITY + "}";
            return String.Format(strFormat, value);
        }

        public static List<string> GetSqlServerList()
        {
            string myServer = Environment.MachineName;
            List<string> serverList = new List<string>();
            DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
            for (int i = 0; i < servers.Rows.Count; i++)
            {
                //if (myServer == servers.Rows[i]["ServerName"].ToString()) ///// used to get the servers in the local machine////
                //{
                if ((servers.Rows[i]["InstanceName"] as string) != null)
                    serverList.Add(servers.Rows[i]["ServerName"] + "\\" + servers.Rows[i]["InstanceName"]);
                else
                    serverList.Add(servers.Rows[i]["ServerName"].ToString());
                //}
            }
            return serverList;
        }

        public static List<string> GetDatabases(string instance)
        {
            try
            {
                List<string> dbList = new List<string>();
                Server sr = new Server(instance);
                foreach (Database db in sr.Databases)
                {
                    dbList.Add(db.Name);
                }
                return dbList;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static DataTable GetSQLServers()
        {
            try
            {
                DataTable servers = SqlDataSourceEnumerator.Instance.GetDataSources();
                return servers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region "Update Script"
        /// <summary>
        /// Query from script files
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static object ExecSql_FromScriptFile(string ConnectionString, string filepath)
        {
            //Declare a ServerConnection object variable to specify SQL authentication, login and password.
            object result;
            try
            {
                FileInfo File = new FileInfo(filepath);
                string script = File.OpenText().ReadToEnd();
                result = ExecSql_MultiQueries(ConnectionString, script);
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
        public static object ExecSql_MultiQueries(string ConnectionString, string script)
        {
            //Declare a ServerConnection object variable to specify SQL authentication, login and password.
            ServerConnection conn = new ServerConnection();
            object result;
            try
            {
                conn.ConnectionString = ConnectionString;
                Microsoft.SqlServer.Management.Smo.Server ser = new Microsoft.SqlServer.Management.Smo.Server(conn);
                result = ser.ConnectionContext.ExecuteNonQuery(script);
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

        public static T ShallowClone<T>(T srcObject) where T : class, new()
        {
            // Get the object type
            Type objectType = typeof(T);

            // Get the public properties of the object
            PropertyInfo[] propInfo = srcObject.GetType()
               .GetProperties(
                  System.Reflection.BindingFlags.Instance |
                  System.Reflection.BindingFlags.Public
               );

            // Create a new  object
            T newObject = new T();

            // Loop through all the properties and copy the information 
            // from the source object to the new instance
            foreach (PropertyInfo p in propInfo)
            {
                Type t = p.PropertyType;
                if ((t.IsValueType || t == typeof(string)) && (p.CanRead) && (p.CanWrite))
                {
                    p.SetValue(newObject, p.GetValue(srcObject, null), null);
                }
            }

            // Return the cloned object.
            return newObject;
        }
    }
}
