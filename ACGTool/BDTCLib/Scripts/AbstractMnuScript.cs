using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using BDTCLib.Scripts.Actions;

namespace BDTCLib.Scripts
{
    /// <summary>
    /// Thông tin của một item <Script> </Script> trong file MyMnu.xml
    /// <Script ID="1" ScrptFile="KichBan\2_TCCBCD\03_TCSDLL_Mui2_ThuyetMinh.xml" Start="0" />
    /// hoặc
    /// <Script ID="1" MnuItemRef="Mnu_1" Start="0" />
    /// </summary>
    public abstract class AbstractMnuScript
    {
        public string ID;
        public string Start;

        public TimerCalc ObjTimerCalc;
        public XmlNode CurrentXmlNode;

        private MnuItem _objMnuItem;
        public MnuItem ObjMnuItem
        {
            get { return _objMnuItem; }
            set { _objMnuItem = value; }
        }

        /// <summary>
        /// Với mỗi <Script> ta sẽ tính thời gian bắt đầu và thời gian kết thúc thực sự
        /// Sẽ có 2 loại script như sau:
        /// Loại 1: <Script ID="1" ScrptFile="05_Scripts_DienBien\TH4-DDVH\07-Ngay20-11\01-05h30\01_D7-AnDoiHinh.xml" Start="0" />
        /// Loại 2: <Script ID="1" MnuItemRef="Mnu_GD2_6.2.1" Start="0" />
        /// </summary>
        /// <returns></returns>
        public List<string> CalculateScriptTimer()
        {
            List<string> errList = new List<string>();
            AbstractMnuScript objRefMnuScript;
            try
            {
                ObjTimerCalc = new TimerCalc();
                string msg = ObjTimerCalc.CalculateTimer(Start);
                if (msg != "")
                {
                    msg = string.Format("Lỗi {0} ở Script {1} của MnuItem {2}", msg, this.ToString(),
                                        this.ObjMnuItem.ToString());
                    errList.Add(msg);
                    return errList;
                }
                //Test
#if DEBUG
                if (ObjMnuItem.ID == "Mnu_2")
                {
                    ObjMnuItem.ID = "Mnu_2";
                }

#endif

                
                    if (ObjTimerCalc.IDRefValue != "") //Nếu tồn tại ID Ref trong Start
                    {
                        //Kiểm tra <Script> ID Ref có tồn tại trong Dictionary không
                        if (!ObjMnuItem.MnuScriptDict.ContainsKey(ObjTimerCalc.IDRefValue))
                        {
                            msg = string.Format("Start={0} của Script {1} chứa ID không tồn tại trong MenuItem {2}", Start, this.ToString(),
                                                ObjMnuItem.ToString());
                            errList.Add(msg);
                            return errList;
                        }

                        objRefMnuScript = ObjMnuItem.MnuScriptDict[ObjTimerCalc.IDRefValue];
                        //====================================================================
                        //Kiểm tra Action của IDRefValue đã được tính thời gian chưa
                        errList.AddRange(ObjMnuItem.CheckPendingMnuScript(ref objRefMnuScript));
                        if (errList.Count > 0)
                            return errList;

                        //Nếu thời gian bắt đầu chạy cùng với script ID Ref
                        if (Start.Contains("Start"))
                        {
                            ObjTimerCalc.Start = objRefMnuScript.ObjTimerCalc.Start + ObjTimerCalc.AddTime;
                        }
                        else //Nếu thời gian bắt đầu sau khi script ID Ref kết thúc
                        {
                            ObjTimerCalc.Start = objRefMnuScript.ObjTimerCalc.Stop + ObjTimerCalc.AddTime;
                        }
                    }
                    else //Nếu chỉ chứa giá trị thời gian thông thường
                    {
                        ObjTimerCalc.Start = ObjTimerCalc.AddTime;
                    }

                    //Tính duration và stop end của một <Script>
                    errList.AddRange(CalculateDuration());
            }
            catch (Exception ex)
            {
                if (errList.Count == 0)
                    throw ex;
            }

            return errList;
        }

        /// <summary>
        /// Tính duration và stop end của một <Script>
        /// </summary>
        public abstract List<string> CalculateDuration();

        public override string ToString()
        {
            if (this.CurrentXmlNode != null)
                return BDTCHelper.GetCurrentXMLContent(this.CurrentXmlNode).Replace("</Script>", "");
            return "<Script> is null";
        }
    }
}
