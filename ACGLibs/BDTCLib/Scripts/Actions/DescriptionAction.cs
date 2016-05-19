using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDTCLib.Scripts.Actions
{
    public class DescriptionAction : AbstractAction
    {
        //public string ID = "1";
        //public string Type = "Description";
        //public string Start = "0";
        //public string Duration = "6";
        //public string DescText = "";
        //public string Pos = "Duoi";
        //public string SaBanHide="false";
        //public string SoundName;

        public DescriptionAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
            //ID = nodeAction.Attributes["ID"].Value;
            //Type = nodeAction.Attributes["Type"].Value;
            //Start = nodeAction.Attributes["Start"].Value;
            //Duration = nodeAction.Attributes["Duration"].Value;
            //DescText = nodeAction.Attributes["DescText"].Value;
            //Pos = nodeAction.Attributes["Pos"].Value;
            //SaBanHide = nodeAction.Attributes["SaBanHide"].Value;
            //SoundName = nodeAction.Attributes["SoundName"].Value;
        }
    }
}
