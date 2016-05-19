using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDTCLib.Scripts.Actions
{

    public class MoveAction : AbstractAction
    {
        //public string ID = "7";
        //public string Type = "Move";
        //public string ObjName = "Dich_XeTang_CayDua1";
        //public string Start = "0";
        //public string Duration = "20";
        //public string Hide = "true";
        //public string SoundName = "xetang_nho.wav";
        //public string SoundLoop = "1";

        private List<TargetAction> TargetActions;
        public MoveAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction)
            : base(objScriptXmlFile, nodeAction)
        {
            //Với mỗi Action
            TargetActions = new List<TargetAction>();
            XmlNode targetsNode = nodeAction.ChildNodes[0];
            foreach (XmlNode target in targetsNode.ChildNodes)
            {
                TargetAction objTarget = new TargetAction(target);
                TargetActions.Add(objTarget);
            }
        }
    }
}
