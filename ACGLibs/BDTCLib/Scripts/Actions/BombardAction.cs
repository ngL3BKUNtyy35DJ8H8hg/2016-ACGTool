using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDTCLib.Scripts.Actions
{
    public class BombardAction : AbstractAction
    {
        //public string ID = "1";
        //public string Type = "Bombard";
        //public string ObjName = "Dich_PhaoNgoaiBien_1";
        //public string ImageFile = "Dich_DanCoi.png";
        //public string Width = "1";
        //public string Height = "1";
        //public string Start = "0";
        //public string Duration = "3";
        //public string Speed = "3";
        //public string dAngle = "-45";
        //public string SoundName = "gun_shotgun1.wav";
        //public string SoundLoop = "1";
        //public string ExplID = "Boom";
        //public string Repeat = "1";

        public BombardAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
        }
    }
}
