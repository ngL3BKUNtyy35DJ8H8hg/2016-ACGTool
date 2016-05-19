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
    /// </summary>
    public class ScrptFileScript : AbstractMnuScript
    {
        //Lưu giá trị thuộc tính ScrptFile
        public string ScrptFile;

        //Lưu thông tin class của file script .xml
        public ScriptXmlFile _objScriptXmlFile;
        public ScriptXmlFile ObjScriptXmlFile
        {
            get
            {
                return _objScriptXmlFile;
            }
            set { _objScriptXmlFile = value; }
        }


        public ScrptFileScript(MnuItem objMnuItem, XmlNode nodeScript)
        {
            try
            {
                //Lưu XmlNode hiện tại
                CurrentXmlNode = nodeScript;

                ObjMnuItem = objMnuItem;
                ID = nodeScript.Attributes["ID"].Value;
                ScrptFile = nodeScript.Attributes["ScrptFile"].Value;
                Start = nodeScript.Attributes["Start"].Value;

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Load nội dung của file .xml từ menu script <Script ID="1" ScrptFile="KichBan\2_TCCBCD\03_TCSDLL_Mui2_ThuyetMinh.xml" Start="0" />
        /// </summary>
        /// <returns></returns>
        public List<string> LoadContentXmlFile()
        {
            List<string> errList = new List<string>();
            if (ID == null)
            {
                
            }
            string xmlFilePath = ObjMnuItem.ObjMyMnu.ObjDiaHinh._myCurrentDirectory + "\\" + ScrptFile;
            try
            {
                //Nếu chưa tồn tại file xml trong Dictionary thì tạo mới
                if (!ObjMnuItem.ObjMyMnu.ScriptXmlFileDict.ContainsKey(xmlFilePath))
                {
                    ObjScriptXmlFile = new ScriptXmlFile(xmlFilePath);
                    errList = ObjScriptXmlFile.LoadContentXmlFile();
                    ObjMnuItem.ObjMyMnu.ScriptXmlFileDict.Add(xmlFilePath, ObjScriptXmlFile);
                }
                else
                {
                    //Nếu tồn tại file xml trong Dictionary rồi thì gán lại
                    ObjScriptXmlFile = ObjMnuItem.ObjMyMnu.ScriptXmlFileDict[xmlFilePath];
                }
            }
            catch (Exception ex)
            {
                if (errList.Count == 0)
                    throw ex;
            }

            return errList;
        }

        /// <summary>
        /// Với mỗi <Script> ta sẽ tính thời gian bắt đầu và thời gian kết thúc thực sự
        /// </summary>
        /// <returns></returns>
        public override List<string> CalculateDuration()
        {
            List<string> errList = new List<string>();
            try
            {
                //Nếu chưa tính duration của file xml thì thực hiện tính
                if (ObjScriptXmlFile.Duration == -1)
                {
                    errList.AddRange(ObjScriptXmlFile.CalculateActionTimer());
                    if (errList.Count > 0)
                        return errList;
                }
                    ObjTimerCalc.Duration = ObjScriptXmlFile.Duration;
                    //ObjTimerCalc.Stop = ObjTimerCalc.Start + ObjTimerCalc.Duration;
            }
            catch (Exception ex)
            {
                if (errList.Count == 0)
                    throw ex;
            }
            return errList;
        }

        //public override string ToString()
        //{
        //    string value = "";
        //    if (this.CurrentXmlNode != null)
        //    {
        //        //return BDTCHelper.GetCurrentXMLContent(this.CurrentXmlNode).Replace("</MnuItem>", "");
        //        value = this.CurrentXmlNode.OuterXml;

        //    }

        //    return value;
        //}
    }
}
