using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDTCLib.Scripts.Actions
{
    public class ShootAction : AbstractAction
    {
        //public string ID = "5";
        //public string Type = "Shoot";
        //public string ObjName = "Dich_CongSu_DoiCayDua";
        //public string ImageFile = "Ta_Dan.bmp";
        //public string Width = "0.4";
        //public string Height = "0.2";
        //public string Start = "Start(3)";
        //public string Duration = "0.4";
        //public string Speed = "0.4";
        //public string SoundName = "gun_shotgun1_nho.wav";
        //public string SoundLoop = "1";
        //public string ExplID = "";
        //public string Repeat = "5";

        public ShootAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
        }
    }
}
