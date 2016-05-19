using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDTCLib.Scripts.Actions
{
    public class FlyAction : AbstractAction
    {
        //public string ID = "5";
        //public string Type = "Fly";
        //public string ObjName = "Dich_MayBayNB_2";
        //public string Start = "0";
        //public string Duration = "20";
        //public string Hide = "true";
        //public string SoundName = "b17-01.wav";
        //public string SoundLoop = "1";

        private List<TargetAction> TargetActions;
        public FlyAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction)
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
