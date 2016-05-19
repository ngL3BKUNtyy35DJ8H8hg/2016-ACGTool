using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

namespace ACGTool.Classes.LinqToSql
{
    public class RepositoryClass_VB : RepositoryClass
    {
        public RepositoryClass_VB(string dbName, DataTable db) : base(dbName, db)
        {
        
        }

        public override string InitClass()
        {
            string initCode = "";
            initCode += "Imports System \n";
            initCode += "Imports System.Data.Linq \n";
            initCode += "Imports System.Linq \n";

            //Add Class Info
            initCode += "\nPublic Class " + _dbName.ToUpper() + "Info" + "\n\n";
            initCode += "#Region \"Attributes\" \n";
            initCode += _ADDCODEHERE1;
            initCode += "#End Region\n\n";
            initCode += "#Region \"Properties\" \n";
            initCode += _ADDCODEHERE2;
            initCode += "#End Region\n\n";

            //End Class
            initCode += "End Class\n";

            return initCode;
        }

        public override string DeclareAttribute(string type, string name, string des)
        {
            string attCode = "";
            if (des != "")
                attCode = GeneralClass.AddTab(1) + "' " + des + "\n";
            attCode += GeneralClass.AddTab(1) + "Private " + "_" + name + " As " + type + "\n";
            return attCode;
        }

        public override string DeclareProperty(string type, string name, string des)
        {
            string propertyCode = "";
            propertyCode = "\n";
            if (des != "")
            {
                propertyCode += GeneralClass.AddTab(1) + "''' <summary>\n";
                propertyCode += GeneralClass.AddTab(1) + "''' " + des + "\n";
                propertyCode += GeneralClass.AddTab(1) + "''' </summary>\n";
                propertyCode += GeneralClass.AddTab(1) + "''' <value></value>\n";
                propertyCode += GeneralClass.AddTab(1) + "''' <returns></returns>\n";
                propertyCode += GeneralClass.AddTab(1) + "''' <remarks></remarks>\n";
            }

            propertyCode += GeneralClass.AddTab(1) + "Public Property " + name.ToUpper() + "() As " + type + "\n";
            //Set property
            propertyCode += GeneralClass.AddTab(2) + "Get \n";
            propertyCode += GeneralClass.AddTab(3) + "Return " + GetAttribute(name) + "\n";
            propertyCode += GeneralClass.AddTab(2) + "End Get \n";

            //Get property
            propertyCode += GeneralClass.AddTab(2) + "Set (ByVal value As " + type + ") \n";
            propertyCode += GeneralClass.AddTab(3) + GetAttribute(name) + " = value \n";
            propertyCode += GeneralClass.AddTab(2) + "End Set \n";

            //End property
            propertyCode += GeneralClass.AddTab(1) + "End Property \n";

            return propertyCode;
        }

        public override string CreateClass()
        {
            Procedure objProcedure = new ProcedureVB();
            //Init a class
            string str = InitClass();

            int index, len;
            string type, name, des;
            len = _db.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                //Get row
                DataRow row = _db.Rows[len - 1 - i];
                //Get type, name, description of attribute
                type = objProcedure.GetTypeMap(row["TYPE_NAME"].ToString());
                name = row["COLUMN_NAME"].ToString();
                des = objProcedure.GetDescription(_dbName, row["COLUMN_NAME"].ToString());

                //Add code 1
                index = str.IndexOf(_ADDCODEHERE1) +_ADDCODEHERE1.Length;
                str = str.Insert(index, DeclareAttribute(type, name, des));

                //Add code 2
                index = str.IndexOf(_ADDCODEHERE2) + _ADDCODEHERE2.Length;
                str = str.Insert(index, DeclareProperty(type, name, des));
            }

            //Remove string code 1
            index = str.IndexOf(_ADDCODEHERE1);
            str = str.Remove(index, _ADDCODEHERE1.Length);

            //Remove string code 2
            index = str.IndexOf(_ADDCODEHERE2);
            str = str.Remove(index, _ADDCODEHERE2.Length);

            return str;
        }


        public override string CreateRepositoryClass()
        {
            Procedure objProcedure = new ProcedureVB();
            //Init a class
            string str = InitClass();

            int index, len;
            string type, name, des;
            len = _db.Rows.Count;
            for (int i = 0; i < len; i++)
            {
                //Get row
                DataRow row = _db.Rows[len - 1 - i];
                //Get type, name, description of attribute
                type = objProcedure.GetTypeMap(row["TYPE_NAME"].ToString());
                name = row["COLUMN_NAME"].ToString();
                des = objProcedure.GetDescription(_dbName, row["COLUMN_NAME"].ToString());

                //Add code 1
                index = str.IndexOf(_ADDCODEHERE1) + _ADDCODEHERE1.Length;
                str = str.Insert(index, DeclareAttribute(type, name, des));

                //Add code 2
                index = str.IndexOf(_ADDCODEHERE2) + _ADDCODEHERE2.Length;
                str = str.Insert(index, DeclareProperty(type, name, des));
            }

            //Remove string code 1
            index = str.IndexOf(_ADDCODEHERE1);
            str = str.Remove(index, _ADDCODEHERE1.Length);

            //Remove string code 2
            index = str.IndexOf(_ADDCODEHERE2);
            str = str.Remove(index, _ADDCODEHERE2.Length);

            return str;
        }
    }
}
