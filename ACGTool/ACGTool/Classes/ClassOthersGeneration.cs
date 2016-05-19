using System;
using System.Collections.Generic;
using System.Text;

namespace ACGTool.Classes
{
    public class ClassOthersGeneration
    {
        /// <summary>
        /// Hàm kiểm tra xem column có set Identity không
        /// </summary>
        /// <param name="typedb"></param>
        /// <returns></returns>
        public bool IsIdentity(string typedb)
        {
            return typedb.Contains("identity");
        }

        /*
         * set value between class object with janus datagrid .RootTable.Columns("CDP")
         * Template: objName.name = gridName.RootTable.Columns(name)
         * Sample: objName.TEN = gridEX.RootTable.Columns("TEN")
         */
        public virtual string Template1(string attName, string leftTemplate, string middleTemplate, string rightTemplate)
        {
            return string.Empty;
        }

    }

    public class ClassOthersGenerationCSharp : ClassOthersGeneration
    {
        /*
         * set value between class object with janus datagrid .RootTable.Columns("CDP")
         * Template: objName.name = gridName.RootTable.Columns(name)
         * Sample: objName.TEN = gridEX.RootTable.Columns("TEN")
         */
        public override string Template1(string attName, string leftTemplate, string middleTemplate , string rightTemplate)
        {
            string attCode = "";
            attCode += leftTemplate + "." + attName.ToUpper() + " = " + middleTemplate + "\"" + attName + "\"" + rightTemplate + ";\n";
            return attCode;
        }
    }

    public class ClassOthersGenerationVB : ClassOthersGeneration
    {
        /*
         * set value between class object with janus datagrid .RootTable.Columns("CDP")
         * Template: objName.name = gridName.RootTable.Columns(name)
         * Sample: objName.TEN = gridEX.RootTable.Columns("TEN")
         */
        public override string Template1(string attName, string leftTemplate, string middleTemplate, string rightTemplate)
        {
            string attCode = "";
            attCode += leftTemplate + "." + attName.ToUpper() + " = " + middleTemplate + "\"" + attName + "\"" + rightTemplate + "\n";
            return attCode;
        }

    }
}
