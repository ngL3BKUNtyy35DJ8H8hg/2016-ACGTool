using System;
using System.Collections.Generic;
using System.Text;
using System.Data;


namespace ACGTool.Classes
{
    public class ClassInfoGeneration
    {
        //Using for AddObject store procedure
        public string _ADDCODEHERE1 = "//ADD CODE HERE 1\n";
        public string _ADDCODEHERE2 = "//ADD CODE HERE 2\n";
        public string _ADDCODEHERE3 = "//ADD CODE HERE 3\n";
        public string _ADDCODEHERE4 = "//ADD CODE HERE 4\n";
        public string _ADDCODEHERE5 = "//ADD CODE HERE 5\n";
        public string _ADDCODEHERE6 = "//ADD CODE HERE 6\n";

        public string AddTab(int num)
        {
            string tab = "";
            for (int i = 0; i < num; i++)
                tab += "\t";
            return tab;
        }

        public string GetAttribute(string name)
        {
            return "_" + name;
        }

        public virtual string InitClass(string table_name)
        {
            return String.Empty;
        }

        public virtual string DeclareAttribute(string type, string name, string des)
        {
            return string.Empty;
        }

        public virtual string DeclareProperty(string type, string name, string des)
        {
            return string.Empty;
        }
    }

    public class ClassInfoGenerationCSharp : ClassInfoGeneration
    {
        public override string InitClass(string table_name)
        {
            string initCode = "";
            initCode += "using System; \n";
            initCode += "using System.Data; \n";
            initCode += "using System.Data.SqlClient; \n";
            initCode += "using System.Text; \n\n";

            //Add Class Info
            initCode += "public class " + table_name.ToUpper().Remove(0, 3) + "Info{" + "\n\n";
            initCode += "#region Attributes \n";
            initCode += _ADDCODEHERE1;
            initCode += "#endregion\n\n";
            initCode += "#region Properties \n";
            initCode += _ADDCODEHERE2;
            initCode += "#endregion\n\n";

            //End Class
            initCode += "}\n";

            return initCode;
        }

        public override string DeclareAttribute(string type, string name, string des)
        {
            string attCode = "";
            if (des != "")
                attCode = AddTab(1) + "// " + des + "\n";
            attCode += AddTab(1) + "private " + type + " _" + name + ";\n";
            return attCode;
        }

        public override string DeclareProperty(string type, string name, string des)
        {
            string propertyCode = "";
            propertyCode = "\n";
            if (des != "")
            {
                propertyCode += AddTab(1) + "/// <summary>\n";
                propertyCode += AddTab(1) + "/// " + des + "\n";
                propertyCode += AddTab(1) + "/// </summary>\n";
                propertyCode += AddTab(1) + "/// <value></value>\n";
                propertyCode += AddTab(1) + "/// <returns></returns>\n";
                propertyCode += AddTab(1) + "/// <remarks></remarks>\n";
            }

            propertyCode += AddTab(1) + "public " + type + " " + name.ToUpper() + "\n";
            propertyCode += AddTab(1) + "{ \n";
            //Get property
            propertyCode += AddTab(2) + "get { return " + GetAttribute(name) + ";} \n";
            //Set property
            propertyCode += AddTab(2) + "set {  " + GetAttribute(name) + " = value;} \n";

            //End property
            propertyCode += AddTab(1) + "} \n";

            return propertyCode;
        }
    }

    public class ClassInfoGenerationVBDotNet : ClassInfoGeneration
    {
        public override string InitClass(string table_name)
        {
            string initCode = "";
            initCode += "Imports System \n";
            initCode += "Imports System.Data \n";
            initCode += "Imports System.Data.SqlClient \n";
            initCode += "Imports System.Text \n";

            //Add Class Info
            initCode += "\nPublic Class " + table_name.ToUpper().Remove(0, 3) + "Info" + "\n\n";
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
                attCode = AddTab(1) + "' " + des + "\n";
            attCode += AddTab(1) + "Private " + "_" + name + " As " + type + "\n";
            return attCode;
        }

        public override string DeclareProperty(string type, string name, string des)
        {
            string propertyCode = "";
            propertyCode = "\n";
            if (des != "")
            {
                propertyCode += AddTab(1) + "''' <summary>\n";
                propertyCode += AddTab(1) + "''' " + des + "\n";
                propertyCode += AddTab(1) + "''' </summary>\n";
                propertyCode += AddTab(1) + "''' <value></value>\n";
                propertyCode += AddTab(1) + "''' <returns></returns>\n";
                propertyCode += AddTab(1) + "''' <remarks></remarks>\n";
            }

            propertyCode += AddTab(1) + "Public Property " + name.ToUpper() + "() As " + type + "\n";
            //Set property
            propertyCode += AddTab(2) + "Get \n";
            propertyCode += AddTab(3) + "Return " + GetAttribute(name) + "\n";
            propertyCode += AddTab(2) + "End Get \n";

            //Get property
            propertyCode += AddTab(2) + "Set (ByVal value As " + type + ") \n";
            propertyCode += AddTab(3) + GetAttribute(name) + " = value \n";
            propertyCode += AddTab(2) + "End Set \n";

            //End property
            propertyCode += AddTab(1) + "End Property \n";

            return propertyCode;
        }
    }
}
