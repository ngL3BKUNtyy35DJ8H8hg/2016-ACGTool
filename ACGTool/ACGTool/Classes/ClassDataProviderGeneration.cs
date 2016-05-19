using System;
using System.Collections.Generic;
using System.Text;

namespace ACGTool.Classes
{
    public class ClassDataProviderGeneration
    {
        public string[] _ADDCODEHERE = {
            "//ADD CODE HERE 1\n",
            "//ADD CODE HERE 2\n",
            "//ADD CODE HERE 3\n",
            "//ADD CODE HERE 4\n",
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
        public virtual string Template1(string type, string name, bool isComma)
        {
            return string.Empty;
        }
        #endregion
    }

    public class ClassDataProviderGenerationVB : ClassDataProviderGeneration
    {
        public override string InitClass(string table_name)
        {
            string initCode = "";
            string name = table_name.ToUpper().Remove(0, 3);

            initCode += "#Region \"" + name + ": Auto Code\"\n";

            //Add
            initCode += AddTab(1) + "Public MustOverride Sub " + "Add" + name + "(";
            initCode += _ADDCODEHERE[0];
            initCode += ")\n";

            //Update
            initCode += AddTab(1) + "Public MustOverride Sub " + "Update" + name + "(";
            initCode += _ADDCODEHERE[1];
            initCode += ")\n";

            //Delete
            initCode += AddTab(1) + "Public MustOverride Sub " + "Delete" + name + "(";
            initCode += _ADDCODEHERE[2];
            initCode += ")\n";

            //Get
            initCode += AddTab(1) + "Public MustOverride Function " + "Get" + name + "(";
            initCode += _ADDCODEHERE[3];
            initCode += ") As IDataReader\n";

            //GetAll
            initCode += AddTab(1) + "Public MustOverride Function " + "GetAll" + name + "() As IDataReader\n";

            //GetAll DataSet
            initCode += AddTab(1) + "Public MustOverride Function " + "GetAll" + name + "DataSet() As DataSet\n";

            //End Region
            initCode += "#End Region\n";
            return initCode;
        }

        #region "Templates"
        public override string Template1(string type, string name, bool isComma)
        {
            string attCode = "";
            if (isComma)
                attCode += "ByVal " + name + " As " + type + ", ";
            else
                attCode += "ByVal " + name + " As " + type;
            return attCode;
        }
        #endregion
    }

    public class ClassDataProviderGenerationCSharp : ClassDataProviderGeneration
    {
        public override string InitClass(string table_name)
        {
            string initCode = "";
            string name = table_name.ToUpper().Remove(0, 3);

            initCode += "#region " + name + ": Auto Code\n";

            //Add
            initCode += AddTab(1) + "public abstrac void " + "Add" + name + "(";
            initCode += _ADDCODEHERE[0];
            initCode += ")\n";

            //Update
            initCode += AddTab(1) + "public abstrac void " + "Update" + name + "(";
            initCode += _ADDCODEHERE[1];
            initCode += ")\n";

            //Delete
            initCode += AddTab(1) + "public abstrac void " + "Delete" + name + "(";
            initCode += _ADDCODEHERE[2];
            initCode += ")\n";

            //Get
            initCode += AddTab(1) + "public abstrac " + "IDataReader " + "Get" + name + "(";
            initCode += _ADDCODEHERE[3];
            initCode += ")\n";

            //GetAll
            initCode += AddTab(1) + "public abstrac " + "IDataReader " + "GetAll" + name + "(){\n";

            //GetAll DataSet
            initCode += AddTab(1) + "public abstrac " + "IDataReader " + "GetAll" + name + "DataSet()\n";

            //End Region
            initCode += "#endregion\n";
            return initCode;
        }

        #region "Templates"
        public override string Template1(string type, string name, bool isComma)
        {
            string attCode = "";
            if (isComma)
                attCode += type + " " + name + ", ";
            else
                attCode += type + " " + name;
            return attCode;
        }
        #endregion
    }
}
