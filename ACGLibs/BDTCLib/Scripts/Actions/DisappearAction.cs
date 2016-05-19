using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDTCLib.Scripts.Actions
{
    /// <summary>
    /// Xử lý hiệu ứng xuất hiện
    /// </summary>
    public abstract class AbstractDisappearAction : AbstractAction
    {
        //public string ID = "7";
        //public string Type = "Disappear";
        //public string ObjName = "Dich_CongSu_DoiCayDua";
        //public string Start = "Stop(6)";
        //public string Duration = "0.5";
        //public string Steps = "30";
        //public string Partial = "1";
        //public string Hide = "false";
        //public string SoundName = "";
        //public string SoundLoop = "0";
        //public string Repeat = "1";


        protected AbstractDisappearAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction)
            : base(objScriptXmlFile, nodeAction)
        {
            //ID = nodeAction.Attributes["ID"].Value;
            //Type = nodeAction.Attributes["Type"].Value;
            //ObjName = nodeAction.Attributes["ObjName"].Value;
            //Start = nodeAction.Attributes["Start"].Value;
            //Duration = nodeAction.Attributes["Duration"].Value;
            //Steps = nodeAction.Attributes["Steps"].Value;
            //Partial = nodeAction.Attributes["Partial"].Value;
            //Hide = nodeAction.Attributes["Partial"].Value;
            //SoundName = nodeAction.Attributes["Partial"].Value;
            //SoundLoop = nodeAction.Attributes["Partial"].Value;
            //Repeat = nodeAction.Attributes["Partial"].Value;
        }
    }

    /// <summary>
    /// Hiệu ứng xuất hiện từ phải qua trái
    /// </summary>
    public class DisappearAction : AbstractDisappearAction
    {
        public DisappearAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
        }
    }

    /// <summary>
    /// Hiệu ứng xuất hiện từ phải qua trái
    /// </summary>
    public class DisappearLeftAction : AbstractDisappearAction
    {
        public DisappearLeftAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
        }
    }

    /// <summary>
    /// Hiệu ứng xuất hiện từ trên xuống dưới
    /// </summary>
    public class DisappearDownAction : AbstractDisappearAction
    {
        public DisappearDownAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
        }
    }

    /// <summary>
    /// Hiệu ứng xuất hiện từ dưới lên trên
    /// </summary>
    public class DisappearTopAction : AbstractDisappearAction
    {
        public DisappearTopAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
        }
    }

    /// <summary>
    /// Hiệu ứng xuất hiện rõ dần (dùng cho ký hiệu 2D)
    /// </summary>
    public class DisappearAAction : AbstractDisappearAction
    {
        public DisappearAAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
        }
    }

    /// <summary>
    /// Hiệu ứng xuất hiện rõ dần (dùng cho ký hiệu 3D)
    /// </summary>
    public class HideAction : AbstractDisappearAction
    {
        public HideAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
        }
    }
}
