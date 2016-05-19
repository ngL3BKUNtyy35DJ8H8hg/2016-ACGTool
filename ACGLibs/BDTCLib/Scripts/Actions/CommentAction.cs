using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDTCLib.Scripts.Actions
{
    public class CommentAction : AbstractAction
    {
        //public string ID = "Ghi chú: Hướng 1";
        //public string Type = "Comment";

        public CommentAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
        }
    }
}
