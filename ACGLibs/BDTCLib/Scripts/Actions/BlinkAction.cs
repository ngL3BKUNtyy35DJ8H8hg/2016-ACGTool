using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDTCLib.Scripts.Actions
{
    public class BlinkAction : AbstractAction
    {
        //public string ID = "4";
        //public string Type = "Blink";
        //public string ObjName = "Dich_fHQĐB";
        //public string Start = "Stop(3)";
        //public string Duration = "5";
        //public string Speed = "1";
        //public string SoundName = "";
        //public string SoundLoop = "0";
        public BlinkAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
        }
    }
}
