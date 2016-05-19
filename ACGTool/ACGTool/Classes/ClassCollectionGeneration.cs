using System;
using System.Collections.Generic;
using System.Text;

namespace ACGTool.Classes
{
    public class ClassCollectionGeneration
    {
        //Using for AddObject store procedure
        public string _ADDCODEHERE1 = "//ADD CODE HERE 1\n";
        public string _ADDCODEHERE2 = "//ADD CODE HERE 2\n";
        public string _ADDCODEHERE3 = "//ADD CODE HERE 3\n";

        public string GetTypeMap(string typedb)
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
                    stype = "System.DateTime";
                    break;
                case "smallint":
                case "tinyint":
                    stype = "System.Int16";
                    break;
                case "float":
                    stype = "Single";
                    break;
                case "bit":
                    stype = "Boolean";
                    break;
            }
            return stype;
        }

        public string InitClass(string table_name)
        {
            string initCode = "";
            initCode += "Imports System \n";
            initCode += "Imports System.Data \n";
            initCode += "Imports System.Data.SqlClient \n";
            initCode += "Imports System.Text \n";
            initCode += "\nPublic Class " + table_name.ToUpper().Remove(0, 3) + "Collection" + "\n";
            initCode += "#Region \"Attributes\" \n";
            initCode += _ADDCODEHERE1;
            initCode += "#End Region\n";
            initCode += "#Region \"Properties\" \n";
            initCode += _ADDCODEHERE2;
            initCode += "#End Region\n";
            initCode += "End Class";
            return initCode;
        }
    }
}
