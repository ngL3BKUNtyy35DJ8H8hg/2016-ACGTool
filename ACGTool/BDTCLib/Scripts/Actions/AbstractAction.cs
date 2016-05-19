using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDTCLib.Scripts.Actions
{
    /// <summary>
    /// Thông tin của một <Action> </Action> trong một file script .xml
    /// </summary>
    public abstract class AbstractAction
    {
        public string ID = "5";
        public string Type = "Move";
        public string ObjName = "Dich_XeTang_CayDua1";

        //=====================================
        //Thuộc tính dùng riêng cho ExplodeDcl Action
        public string ImageFile = "explosion1.bmp";
        public string Width = "2";
        public string Height = "2";
        public string ShiftZ = "1";
        //=====================================

        public string Start = "0";
        public string Speed = "80";
        public string Duration = "0.5";
        public string dAngle = "-45";
        public string Steps = "30";
        public string Partial = "1";
        public string Hide = "false";
        public string SoundName = "";
        public string SoundLoop = "0";
        public string ExplID = "";
        public string Repeat = "1";

        //Thuộc tính dùng riêng cho Descrtipion Action
        public string DescText = "";
        public string Pos = "Duoi";
        public string SaBanHide = "false";
        //=====================================

        //=====================================
        //Thuộc tính dùng riêng cho CornerTitle Action
        public string CornerText = "";
        //=====================================

        //=====================================
        //Thuộc tính dùng riêng cho FocusAt Action
        public string CenterX = "0.7220888";
        public string CenterY = "0.7769972";
        public string cameraPosX = "0";
        public string cameraPosY = "-39";
        public string cameraPosZ = "-19.5";
        public string angleZ = "6.259984";
        public string angleX = "-0.3387986";
        //=====================================

        //=====================================
        //Biến dùng để xử lý
        private BDTCLib.Scripts.ScriptXmlFile _objScriptXmlFile;
        public BDTCLib.Scripts.ScriptXmlFile ObjScriptXmlFile
        {
            get { return _objScriptXmlFile; }
            set { _objScriptXmlFile = value; }
        }

        //Lưu giá trị XmlNode hiện tại
        public XmlNode CurrentXmlNode;

        public TimerCalc ObjTimerCalc;

        public AbstractAction(ScriptXmlFile objScriptXmlFile, XmlNode nodeAction)
        {
            _objScriptXmlFile = objScriptXmlFile;
            CurrentXmlNode = nodeAction;

            if (nodeAction.Attributes["ID"] != null)
                ID = nodeAction.Attributes["ID"].Value;
            if (nodeAction.Attributes["Type"] != null)
                Type = nodeAction.Attributes["Type"].Value;
            if (nodeAction.Attributes["ObjName"] != null)
                ObjName = nodeAction.Attributes["ObjName"].Value;
            
            if (nodeAction.Attributes["ImageFile"] != null)
                ImageFile = nodeAction.Attributes["ImageFile"].Value;
            if (nodeAction.Attributes["Width"] != null)
                Width = nodeAction.Attributes["Width"].Value;
            if (nodeAction.Attributes["Height"] != null)
                Height = nodeAction.Attributes["Height"].Value;
            if (nodeAction.Attributes["ShiftZ"] != null)
                ShiftZ = nodeAction.Attributes["ShiftZ"].Value;
            
            if (nodeAction.Attributes["Start"] != null)
                Start = nodeAction.Attributes["Start"].Value;
            if (nodeAction.Attributes["Duration"] != null)
                Duration = nodeAction.Attributes["Duration"].Value;
            if (nodeAction.Attributes["dAngle"] != null)
                dAngle = nodeAction.Attributes["dAngle"].Value;
            if (nodeAction.Attributes["Steps"] != null)
                Steps = nodeAction.Attributes["Steps"].Value;
            if (nodeAction.Attributes["Partial"] != null)
                Partial = nodeAction.Attributes["Partial"].Value;
            if (nodeAction.Attributes["Hide"] != null)
                Hide = nodeAction.Attributes["Hide"].Value;
            if (nodeAction.Attributes["SoundName"] != null)
                SoundName = nodeAction.Attributes["SoundName"].Value;
            if (nodeAction.Attributes["SoundLoop"] != null)
                SoundLoop = nodeAction.Attributes["SoundLoop"].Value;
            if (nodeAction.Attributes["ExplID"] != null)
                ExplID = nodeAction.Attributes["ExplID"].Value;
            if (nodeAction.Attributes["Repeat"] != null)
                Repeat = nodeAction.Attributes["Repeat"].Value;
            
            if (nodeAction.Attributes["DescText"] != null)
                DescText = nodeAction.Attributes["DescText"].Value;
            if (nodeAction.Attributes["Pos"] != null)
                Pos = nodeAction.Attributes["Pos"].Value;
            if (nodeAction.Attributes["SaBanHide"] != null)
                SaBanHide = nodeAction.Attributes["SaBanHide"].Value;

            if (nodeAction.Attributes["CenterX"] != null)
                CenterX = nodeAction.Attributes["CenterX"].Value;
            if (nodeAction.Attributes["CenterY"] != null)
                CenterY = nodeAction.Attributes["CenterY"].Value;
            if (nodeAction.Attributes["cameraPosX"] != null)
                cameraPosX = nodeAction.Attributes["cameraPosX"].Value;
            if (nodeAction.Attributes["cameraPosY"] != null)
                cameraPosY = nodeAction.Attributes["cameraPosY"].Value;
            if (nodeAction.Attributes["cameraPosZ"] != null)
                cameraPosZ = nodeAction.Attributes["cameraPosZ"].Value;
            if (nodeAction.Attributes["angleZ"] != null)
                angleZ = nodeAction.Attributes["angleZ"].Value;
            if (nodeAction.Attributes["angleX"] != null)
                angleX = nodeAction.Attributes["angleX"].Value;

        }

        /// <summary>
        /// Với mỗi Action trong xml ta sẽ tính được thời gian bắt đầu và thời gian kết thúc thực sự
        /// </summary>
        /// <returns></returns>
        public string CalculateActionTimer()
        {
            string msg = "";
            AbstractAction objRefAction;
            try
            {
                ObjTimerCalc = new TimerCalc();
                msg = ObjTimerCalc.CalculateTimer(Start);
                if (msg != "")
                {
                    msg = string.Format("Lỗi {0} ở Action {1} của file {2}", msg, this.ToString(),
                                        this.ObjScriptXmlFile.FilePath);
                    return msg;
                }

                if (ObjTimerCalc.IDRefValue != "") //Nếu tồn tại ID Ref trong Start
                {
                    //Kiểm tra <Script> ID Ref có tồn tại trong Dictionary không
                    if (!_objScriptXmlFile.ActionDict.ContainsKey(ObjTimerCalc.IDRefValue))
                    {
                        msg = string.Format("Start={0} của Action {1} chứa ID không tồn tại trong file Script {2}", Start, this.ToString(),
                                            _objScriptXmlFile.FilePath);
                        return msg;
                    }

                    objRefAction = _objScriptXmlFile.ActionDict[ObjTimerCalc.IDRefValue];

                    //====================================================================
                    //Kiểm tra Action của IDRefValue đã được tính thời gian chưa
                    msg = _objScriptXmlFile.CheckPendingAction(ref objRefAction);
                    if (msg != "")
                        return msg;

                    //====================================================================
                    //Nếu thời gian bắt đầu chạy cùng với script ID Ref
                    if (Start.Contains("Start"))
                    {
                        ObjTimerCalc.Start = objRefAction.ObjTimerCalc.Start + ObjTimerCalc.AddTime;
                    }
                    else //Nếu thời gian bắt đầu sau khi script ID Ref kết thúc
                    {
                        ObjTimerCalc.Start = objRefAction.ObjTimerCalc.Stop + ObjTimerCalc.AddTime;
                    }
                }
                else //Nếu chỉ chứa giá trị thời gian thông thường
                {
                    ObjTimerCalc.Start = ObjTimerCalc.AddTime;
                }
                if (this.Duration != "")
                {
                    ObjTimerCalc.Duration = float.Parse(this.Duration);
                    //Nếu có số lần lặp lại 
                    if (this.Repeat != "")
                    {
                        if (float.Parse(this.Repeat) > 0)
                            ObjTimerCalc.Duration *= float.Parse(this.Repeat);
                    }

                    //Nếu trường hợp ẩn sau khi xuất hiện thì cộng thêm 1 khoảng this.Duration
                    if(bool.Parse(this.Hide))
                    {
                        ObjTimerCalc.Duration += float.Parse(this.Duration);
                    }
                }

                //ObjTimerCalc.Stop = ObjTimerCalc.Start + ObjTimerCalc.Duration;
            }
            catch (Exception ex)
            {
                if (msg == "")
                    throw ex;
            }
            
            return msg;
        }

        public override string ToString()
        {
            if (this.CurrentXmlNode != null)
                return BDTCHelper.GetCurrentXMLContent(this.CurrentXmlNode).Replace("</Action>", "");
            return "<Action> is null";
        }
    }

}
