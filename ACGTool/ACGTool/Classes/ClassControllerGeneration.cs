using System;
using System.Collections.Generic;
using System.Text;

namespace ACGTool.Classes
{
    public class ClassControllerGeneration
    {
        public string[] _ADDCODEHERE = {
            "//ADD CODE HERE 1\n",
            "//ADD CODE HERE 2\n",
            "//ADD CODE HERE 3\n",
            "//ADD CODE HERE 4\n",
            "//ADD CODE HERE 5\n",
            "//ADD CODE HERE 6\n",
        };

        public string AddTab(int num)
        {
            string tab = "";
            for (int i = 0; i < num; i++)
                tab += "\t";
            return tab;
        }

        public virtual string InitClass(string table_name)
        {
            return string.Empty;
        }

        #region "Templates"
        public string Template1(string attribute, bool isComma)
        {
            string attCode = "";
            if (isComma)
                attCode += "obj" + "." + attribute + ", ";
            else
                attCode += "obj" + "." + attribute;
            return attCode;
        }


        public virtual string Template2(string type, string attribute, bool isComma)
        {
            return string.Empty;
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

    public class ClassControllerGeneratioCSharp : ClassControllerGeneration
    {
        public override string InitClass(string table_name)
        {
            string initCode;
            string name = table_name.ToUpper().Remove(0, 3);

            //Class
            initCode = "#region \"" + name + ": Auto Code\"\n";

            //Add
            initCode += AddTab(1) + "public void " + "Add" + name + "(" + name + "Info " + " obj" + "){\n";
            initCode += AddTab(2) + "DataProvider.Instance().Add" + name + "( _\n";
            initCode += AddTab(2) + _ADDCODEHERE[0];
            initCode += ");\n";
            initCode += AddTab(1) + "}\n\n";

            //Update
            initCode += AddTab(1) + "public void " + "Update" + name + "(" + name + "Info " + " obj" + "){\n";
            initCode += AddTab(2) + "DataProvider.Instance().Update" + name + "( _\n";
            initCode += AddTab(2) + _ADDCODEHERE[1];
            initCode += ");\n";
            initCode += AddTab(1) + "}\n\n";

            //Delete
            initCode += AddTab(1) + "public void " + "Delete" + name + "(";
            initCode += _ADDCODEHERE[2];
            initCode += "){\n";
            initCode += AddTab(2) + "DataProvider.Instance().Delete" + name + "(";
            initCode += _ADDCODEHERE[3];
            initCode += ");\n";
            initCode += AddTab(1) + "}\n\n";

            //Get 
            initCode += AddTab(1) + "public " + name + "Info " + "Get" + name + "(";
            initCode += _ADDCODEHERE[4];
            initCode += "){" ;
            initCode += AddTab(2) + "return CType(CBO.FillObject(DataProvider.Instance()." + "Get" + name + "(";
            initCode += _ADDCODEHERE[5];
            initCode += "), GetType(" + name + "Info)), " + name + "Info);\n";
            initCode += AddTab(1) + "}\n\n";

            //GetAll
            initCode += AddTab(1) + "public "  +  "List(Of " + name + "Info)" + "GetAll" + name + "(){\n";;
            initCode += AddTab(2) + "Return CBO.FillCollection(Of " + name + "Info)(DataProvider.Instance().GetAll" + name + "())\n";
            initCode += AddTab(1) + "End Function\n\n";

            //GetAll DataSet
            initCode += AddTab(1) + "Public Function " + "GetAll" + name + "DataSet() As DataSet\n";
            initCode += AddTab(2) + "Return DataProvider.Instance().GetAll" + name + "DataSet()\n";
            initCode += AddTab(1) + "End Function\n\n";

            //End Region
            initCode += "#End Region\n";
            return initCode;
        }

        #region "Templates"
               
        public override string Template2(string type, string attribute, bool isComma)
        {
            string attCode = "";
            if (isComma)
                attCode += type + " " + attribute + ", ";
            else
                attCode += type + " " + attribute;
            return attCode;
        }
        #endregion
    }

    public class ClassControllerGenerationVB : ClassControllerGeneration 
    {
        public override string InitClass(string table_name)
        {
            string initCode;
            string name = table_name.ToUpper().Remove(0, 3);

            //Class
            initCode = "#Region \"" + name + ": Auto Code\"\n";

            //Add
            initCode += AddTab(1) + "Public Sub " + "Add" + name + "(ByVal obj As " + name + "Info)" + "\n";
            initCode += AddTab(2) + "DataProvider.Instance().Add" + name + "( _\n";
            initCode += AddTab(2) + _ADDCODEHERE[0];
            initCode += ")\n";
            initCode += AddTab(1) + "End Sub\n\n";

            //Update
            initCode += AddTab(1) + "Public Sub " + "Update" + name + "(ByVal obj As " + name + "Info)" + "\n";
            initCode += AddTab(2) + "DataProvider.Instance().Update" + name + "( _\n";
            initCode += AddTab(2) + _ADDCODEHERE[1];
            initCode += ")\n";
            initCode += AddTab(1) + "End Sub\n\n";

            //Delete
            initCode += AddTab(1) + "Public Sub " + "Delete" + name + "(";
            initCode += _ADDCODEHERE[2];
            initCode += ")\n";
            initCode += AddTab(2) + "DataProvider.Instance().Delete" + name + "(";
            initCode += _ADDCODEHERE[3];
            initCode += ")\n";
            initCode += AddTab(1) + "End Sub\n\n";

            //Get 
            initCode += AddTab(1) + "Public Function " + "Get" + name + "(";
            initCode += _ADDCODEHERE[4];
            initCode += ") As " + name + "Info\n";
            initCode += AddTab(2) + "Return CType(CBO.FillObject(DataProvider.Instance()." + "Get" + name + "(";
            initCode += _ADDCODEHERE[5];
            initCode += "), GetType(" + name + "Info)), " + name + "Info)\n";
            initCode += AddTab(1) + "End Function\n\n";

            //GetAll
            initCode += AddTab(1) + "Public Function " + "GetAll" + name + "() As List(Of " + name + "Info)\n";
            initCode += AddTab(2) + "Return CBO.FillCollection(Of " + name + "Info)(DataProvider.Instance().GetAll" + name + "())\n";
            initCode += AddTab(1) + "End Function\n\n";

            //GetAll DataSet
            initCode += AddTab(1) + "Public Function " + "GetAll" + name + "DataSet() As DataSet\n";
            initCode += AddTab(2) + "Return DataProvider.Instance().GetAll" + name + "DataSet()\n";
            initCode += AddTab(1) + "End Function\n\n";

            //End Region
            initCode += "#End Region\n";
            return initCode;
        }

        #region "Templates"
        
        public override string Template2(string type, string attribute, bool isComma)
        {
            string attCode = "";
            if (isComma)
                attCode += "ByVal " + attribute + " As " + type + ", ";
            else
                attCode += "ByVal " + attribute + " As " + type;
            return attCode;
        }

        #endregion
    }
}
