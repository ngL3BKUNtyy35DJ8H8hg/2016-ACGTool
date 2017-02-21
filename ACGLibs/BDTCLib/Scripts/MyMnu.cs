using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDTCLib.Scripts
{
    /// <summary>
    /// Thông tin của file MyMnu.xml
    /// </summary>
    public class MyMnu
    {
        public string MyMnuXmlFile;
        public Dictionary<string, MnuItem> MnuItemDict;
        public Dictionary<string, MnuItem> PendingMnuItemDict;

        private XmlDocument _objMyMnuDoc;
        public XmlNode CurrentXmlNode;

        //Lưu danh sách các file script .xml (một file script .xml có thể xuất hiện nhiều lần ở các MnuItem)
        public Dictionary<string, ScriptXmlFile> ScriptXmlFileDict;
        public List<string> _xmlFileList = new List<string>();

        public MyMnu(DiaHinh objDiaHinh)
        {
            _objDiaHinh = objDiaHinh;
            MyMnuXmlFile = _objDiaHinh._objMyLastSaban._mySaBanDirFullPath + "\\MyMnu.xml";
            LoadXmlFiles();
        }

        private DiaHinh _objDiaHinh;
        public BDTCLib.DiaHinh ObjDiaHinh
        {
            get { return _objDiaHinh; }
            set { _objDiaHinh = value; }
        }

        public List<string> LoadXmlFiles()
        {
            XmlNode nodeMnu;
            try
            {
                _objMyMnuDoc = new XmlDocument();
                _objMyMnuDoc.Load(MyMnuXmlFile);
                nodeMnu = _objMyMnuDoc.DocumentElement;
                XmlNodeList nodeList = nodeMnu.SelectNodes("//Script");

                foreach (XmlNode nodeMnuItem in nodeList)
                {
                    if (nodeMnuItem.Attributes["ScrptFile"] != null)
                    {
                        string path = _objDiaHinh._myCurrentDirectory + "\\" + nodeMnuItem.Attributes["ScrptFile"].Value;
                        //Nếu file chưa tồn tại thì tạo mới
                        if (!File.Exists(path))
                        {
                            // Create a file to write to.
                            using (StreamWriter sw = File.CreateText(path))
                            {
                                sw.WriteLine("<Actions>");
                                sw.WriteLine(string.Format("<Action ID=\"1\" Type=\"CornerTitle\" Start=\"0\" Duration=\"0\" CornerText=\"{0}\"></Action>", Path.GetFileName(path)));
                                sw.WriteLine(string.Format("<Action ID=\"2\" Type=\"CornerTitle\" Start=\"0\" Duration=\"0\" CornerText=\"{0}\"></Action>", Path.GetFileName(path)));
                                sw.WriteLine("</Actions>");
                            }
                        }
                        if (!_xmlFileList.Contains(path))
                            _xmlFileList.Add(path);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _xmlFileList;
        }

        /// <summary>
        /// Load nội dung của file MyMnu.xml
        /// </summary>
        /// <returns></returns>
        public List<string> LoadXmlContent()
        {
            List<string> errList = new List<string>();
            MnuItem objMnuItem;
            XmlNode nodeMnu;
            int currentIndex = 0;
            string ID = "";
            try
            {
                _objMyMnuDoc = new XmlDocument();
                _objMyMnuDoc.Load(MyMnuXmlFile);
                nodeMnu = _objMyMnuDoc.DocumentElement;
                CurrentXmlNode = nodeMnu;

                //=============================================================
                //Xử lý load <MnuItem> trước
                MnuItemDict = new Dictionary<string, MnuItem>();
                PendingMnuItemDict = new Dictionary<string, MnuItem>();

                foreach (XmlNode nodeMnuItem in nodeMnu.ChildNodes)
                    if (nodeMnuItem.Name == "MnuItem")
                    {
                        //============================================
                        //Kiểm tra logic
                        currentIndex++;
                        //Kiểm tra tồn tại ID không
                        if (nodeMnuItem.Attributes["ID"] == null)
                        {
                            errList.Add(string.Format("MnuItem {0} chưa có thuộc tính ID", BDTCHelper.GetCurrentXMLContent(nodeMnuItem).Replace("</MnuItem>", "")));
                            //Gán temp ID
                            ID = string.Format("TempID{0}", currentIndex);
                        }
                        else
                        {
                            ID = nodeMnuItem.Attributes["ID"].Value;
                            if (MnuItemDict.ContainsKey(ID))
                            {
                                errList.Add(string.Format("ID = {0} của Script {1} đã tồn tại.", ID,
                                                          BDTCHelper.GetCurrentXMLContent(nodeMnuItem)));
                                //Gán ID khác do đã bị trùng
                                ID = string.Format("Temp{0}", currentIndex);
                            }
                        }

                        //============================================
                        //Khởi tạo đối tượng
                        objMnuItem = new MnuItem(this, nodeMnuItem);
                        //Lưu vào Dictionary nếu không trùng ID
                        MnuItemDict.Add(ID, objMnuItem);
                    }


                //=============================================================
                //Với mỗi MnuItem, tiếp tục load <Script> bên trong
                ScriptXmlFileDict = new Dictionary<string, ScriptXmlFile>();
                foreach (KeyValuePair<string, MnuItem> pairMnuItem in MnuItemDict)
                {
                    //Lấy lại MnuItem trong Dictionary
                    objMnuItem = pairMnuItem.Value;
                    List<string> errMnuItemList = objMnuItem.LoadScripts();
                    if (errMnuItemList.Count > 0)
                    {
                        errList.AddRange(errMnuItemList);
                    }
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
        /// 2017-02-18
        /// Rename ObjName của các file script .xml
        /// </summary>
        /// <returns></returns>
        public void RenameObjNameInScriptXmlFiles(string oldObjName, string newObjName)
        {
            try
            {
                foreach (string filepath in _xmlFileList)
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(filepath);
                    XmlNode nodeMnu = doc.DocumentElement;
                    XmlNodeList actionNodeList = nodeMnu.SelectNodes("//Action[@ObjName='" + oldObjName + "']");
                    foreach (XmlNode nodeMnuItem in actionNodeList)
                    {
                        nodeMnuItem.Attributes["ObjName"].Value = newObjName;
                    }
                    doc.Save(filepath);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        ///// <summary>
        ///// Kiểm tra Action xử lý có tham chiếu đến ID Ref bị pending không
        ///// </summary>
        ///// <returns></returns>
        //public List<string> CheckPendingMnuItem(ref MnuItem objRefMnuItem)
        //{
        //    //string msg = "";
        //    List<string> errList = new List<string>();
        //    try
        //    {
        //        //msg = string.Format("ID={0} phải nằm dưới ID={1} để tránh bị vòng lặp không giới hạn", ID, ObjTimerCalc.IDRefValue);
        //        //return msg;

        //        //Kiểm tra xem action này đã có trong pending Dict chưa
        //        //Nếu có rồi tức là xuất hiện vòng lặp treo khi các ID ref tham chiếu lẫn nhau
        //        if (!PendingMnuItemDict.ContainsKey(objRefMnuItem.ID))
        //        {
        //            //Kiểm tra Action của IDRefValue đã được tính thời gian chưa (nếu rồi thì Start >= 0)
        //            if (objRefMnuItem.Duration == -1)
        //            {
        //                PendingMnuItemDict.Add(objRefMnuItem.ID, objRefMnuItem);
        //                //Tính thời gian xử lý của pending action
        //                errList.AddRange(objRefMnuItem.CalculateMnuItemDuration());
        //                //Nếu xử lý tính toán thời gian xong thì bỏ pending đi
        //                if (errList.Count == 0)
        //                    PendingMnuItemDict.Remove(objRefMnuItem.ID);
        //            }
        //        }
        //        else
        //        {
        //            //Xuất hiện lỗi vòng lặp
        //            if (PendingMnuItemDict.Count > 0)
        //            {
        //                string msg = "MnuItem bị xuất hiện vòng lặp tham chiếu giữa các MnuItem khác: ";
        //                List<string> arrID = PendingMnuItemDict.Keys.ToList();
        //                arrID.Add(arrID[0]);
        //                msg += String.Join("-->", arrID);
        //                errList.Add(msg);
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (errList.Count == 0)
        //            throw ex;
        //    }

        //    return errList;
        //}
    }

}
