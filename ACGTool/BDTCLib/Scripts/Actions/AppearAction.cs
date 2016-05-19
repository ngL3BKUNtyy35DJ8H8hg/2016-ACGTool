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
    public abstract class AbstractAppearAction : AbstractAction
    {
        //public string ID = "1";
        //public string Type = "Unhide";
        //public string ObjName = "Dich_XeTang_CayDua1";
        //public string Start = "0";
        //public string Duration = "0.5";
        //public string Steps = "30";
        //public string Partial = "1";
        //public string Hide = "false";
        //public string SoundName = "";
        //public string SoundLoop = "0";
        //public string Repeat = "1";

        public AbstractAppearAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
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
    public class AppearAction : AbstractAppearAction
    {
        public AppearAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction)
            : base(objScriptXmlFile, nodeAction)
        {

        }
    }

    /// <summary>
    /// Hiệu ứng xuất hiện từ phải qua trái
    /// </summary>
    public class AppearLeftAction : AbstractAppearAction
    {
        public AppearLeftAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
            
        }
    }

    /// <summary>
    /// Hiệu ứng xuất hiện từ trên xuống dưới
    /// </summary>
    public class AppearDownAction : AbstractAppearAction
    {
        public AppearDownAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
            
        }
    }

    /// <summary>
    /// Hiệu ứng xuất hiện từ dưới lên trên
    /// </summary>
    public class AppearTopAction : AbstractAppearAction
    {
        public AppearTopAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
            
        }
    }

    /// <summary>
    /// Hiệu ứng xuất hiện rõ dần (dùng cho ký hiệu 2D)
    /// </summary>
    public class AppearAAction : AbstractAppearAction
    {
        public AppearAAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
            
        }
    }

    /// <summary>
    /// Hiệu ứng xuất hiện rõ dần (dùng cho ký hiệu 3D)
    /// </summary>
    public class UnhideAction : AbstractAppearAction
    {
        public UnhideAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction) : base(objScriptXmlFile, nodeAction)
        {
            
        }
    }
}
