using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDTCLib.Scripts.Actions
{
    public class CornerTitleAction : AbstractAction
    {
        //public string ID = "3";
        //public string Type = "CornerTitle";
        //public string Start = "0";
        //public string Duration = "0";
        //public string CornerText = "";

        public CornerTitleAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
            ID = nodeAction.Attributes["ID"].Value;
            Type = nodeAction.Attributes["Type"].Value;
            Start = nodeAction.Attributes["Start"].Value;
            Duration = nodeAction.Attributes["Duration"].Value;
            CornerText = nodeAction.Attributes["CornerText"].Value;
        }
    }
}
