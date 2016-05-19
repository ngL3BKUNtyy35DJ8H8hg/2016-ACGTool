using System;
using System.Collections.Generic;
using System.Text;

namespace ACGTool.Classes
{
    class ClassSQLDataProviderGeneration
    {
        //Using for add code here
        public string[] _ADDCODEHERE = {
            "//ADD CODE HERE 1\n",
            "//ADD CODE HERE 2\n",
            "//ADD CODE HERE 3\n",
            "//ADD CODE HERE 4\n",
            "//ADD CODE HERE 5\n",
            "//ADD CODE HERE 6\n",
            "//ADD CODE HERE 7\n",
            "//ADD CODE HERE 8\n",
        };

        public string AddTab(int num)
        {
            string tab = "";
            for (int i = 0; i < num; i++)
                tab += "\t";
            return tab;
        }

        public string InitClass(string table_name)
        {
            string initCode = "";
            string name = table_name.ToUpper().Remove(0, 3);

            initCode += "#Region \"" + name + ": Auto Code\"\n";

            //Add
            initCode += AddTab(1) + "Public Overrides Sub " + "Add" + name + "( _\n";
            initCode += AddTab(1) + _ADDCODEHERE[0];
            initCode += ")\n";
            initCode += AddTab(2) + "SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & \"" + "sp_Add" + name + "\", _\n";
            initCode += AddTab(2) + _ADDCODEHERE[1];
            initCode += ")\n";
            initCode += AddTab(1) + "End Sub\n\n";

            //Update
            initCode += AddTab(1) + "Public Overrides Sub " + "Update" + name + "( _\n";
            initCode += AddTab(1) + _ADDCODEHERE[2];
            initCode += ")\n";
            initCode += AddTab(2) + "SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & \"" + "sp_Update" + name + "\", _\n";
            initCode += AddTab(2) + _ADDCODEHERE[3];
            initCode += ")\n";
            initCode += AddTab(1) + "End Sub\n\n";

            //Delete
            initCode += AddTab(1) + "Public Overrides Sub " + "Delete" + name + "(";
            initCode += _ADDCODEHERE[4];
            initCode += ")\n";
            initCode += AddTab(2) + "SqlHelper.ExecuteNonQuery(ConnectionString, DatabaseOwner & ObjectQualifier & \"" + "sp_Delete" + name + "\", _\n";
            initCode += AddTab(2) + _ADDCODEHERE[5];
            initCode += ")\n";
            initCode += AddTab(1) + "End Sub\n\n";

            //Get
            initCode += AddTab(1) + "Public Overrides Function " + "Get" + name + "(";
            initCode += _ADDCODEHERE[6];
            initCode += ") As IDataReader\n";
            initCode += AddTab(2) + "Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & \"" + "sp_Get" + name + "\", _\n";
            initCode += AddTab(2) + _ADDCODEHERE[7];
            initCode += "), IDataReader)\n";
            initCode += AddTab(1) + "End Function\n\n";

            //GetAll
            initCode += AddTab(1) + "Public Overrides Function " + "GetAll" + name + "() As IDataReader\n";
            initCode += AddTab(2) + "Return CType(SqlHelper.ExecuteReader(ConnectionString, DatabaseOwner & ObjectQualifier & \"" + "sp_GetAll" + name + "\"), IDataReader)\n";
            initCode += AddTab(1) + "End Function\n\n";

            //GetAll DataSet
            initCode += AddTab(1) + "Public Overrides Function " + "GetAll" + name + "DataSet() As DataSet\n";
            initCode += AddTab(2) + "Dim sql As String\n";
            initCode += AddTab(2) + "sql = \"Select * from " + table_name + "\"\n";
            initCode += AddTab(2) + "Return SqlHelper.ExecuteDataset(ConnectionString, CommandType.Text, sql)\n";
            initCode += AddTab(1) + "End Function\n\n";

            //End Region
            initCode += "#End Region\n";
            return initCode;
        }

        #region "Templates"
        public string Template1(string type, string name, bool isComma)
        {
            string attCode = "";
            if (isComma)
                attCode += "ByVal " + name + " As " + type + ", ";
            else
                attCode += "ByVal " + name + " As " + type;
            return attCode;
        }

        public string Template2(string name, int allow_null, bool isComma)
        {
            string attCode = "";
            if (isComma)
                if (allow_null == 1)
                    attCode += "GetNull(" + name + "), ";
                else
                    attCode += name + ", ";
            else
                if (allow_null == 1)
                    attCode += "GetNull(" + name + ")";
                else
                    attCode += name;
            return attCode;
        }

        public string Template3(string attribute, bool isComma)
        {
            string attCode = "";
            if (isComma)
                attCode += attribute + ", ";
            else
                attCode += attribute;
            return attCode;
        }
        #endregion
    }
}
