using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace BDTCLib.Scripts
{
    /// <summary>
    /// Thông tin của một <MnuItem> </MnuItem> trong file MyMnu.xml
    /// <MnuItem ID="Mnu_1" Name="1. Tình hình chung" PosX="10" PosY="80" Width="143" Height="20" Title="">
    /// </summary>
    [Serializable]
    public class MnuItem
    {
        public string ID;
        public string Name;
        public float PosX;
        public float PosY;
        public float Width;
        public float Height;
        public string Title;

        public XmlNode CurrentXmlNode;

        public Dictionary<string, AbstractMnuScript> MnuScriptDict;
        //Danh sách các MnuItem chưa xử lý tính toán thời gian xong
        //Dùng cho trường hợp một <Script> có MnuItemRef đang tham chiếu đến <MnuItem> khác
        //Mà <MnuItem> đó lại chưa tính toán thời gian
        public Dictionary<string, AbstractMnuScript> PendingMnuScriptDict;

        private MyMnu _objMyMnu;
        public MyMnu ObjMyMnu
        {
            get { return _objMyMnu; }
            set { _objMyMnu = value; }
        }

        public MnuItem(MyMnu objMyMnu, XmlNode MnuItemNode)
        {
            try
            {
                CurrentXmlNode = MnuItemNode;
                _objMyMnu = objMyMnu;

                ID = MnuItemNode.Attributes["ID"].Value;
                Name = MnuItemNode.Attributes["Name"].Value;
                PosX = float.Parse(MnuItemNode.Attributes["PosX"].Value);
                PosY = float.Parse(MnuItemNode.Attributes["PosY"].Value);
                Width = float.Parse(MnuItemNode.Attributes["Width"].Value);
                Height = float.Parse(MnuItemNode.Attributes["Height"].Value);
                if (MnuItemNode.Attributes["Title"] != null)
                    Title = MnuItemNode.Attributes["Title"].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        /// <summary>
        /// Load các <Script></Script> trong <MnuItem></MnuItem>
        /// Và lưu vào Dictionary
        /// </summary>
        public List<string> LoadScripts()
        {
            List<string> errList = new List<string>();
            int currentIndex = 0;
            string valueId = "";
            try
            {
                MnuScriptDict = new Dictionary<string, AbstractMnuScript>();
                //Khởi tạo dict lưu danh sách MnuItemRef tham chiếu trong một MnuItem
                //PendingMnuItemRefDict = new Dictionary<string, MnuItem>();
                //PendingMnuItemRefDict.Add(this.ID, this);
                //Lấy ID của mỗi script thuộc MnuItem rồi Add vào node con
                foreach (XmlNode nodeScript in CurrentXmlNode.ChildNodes)
                    if (nodeScript.Name == "Script")
                    {
                        //============================================
                        //Kiểm tra logic
                        currentIndex++;
                        //Kiểm tra tồn tại ID không
                        if (nodeScript.Attributes["ID"] == null)
                        {
                            errList.Add(string.Format("Script {0} của MnuItem \"{1}\" chưa có thuộc tính ID",
                                                    BDTCHelper.GetCurrentXMLContent(nodeScript), BDTCHelper.GetCurrentXMLContent(CurrentXmlNode).Replace("</MnuItem>", "")));
                            //Gán temp ID do không tồn tại
                            valueId = string.Format("Temp{0}", currentIndex);
                        }
                        else
                        {
                            valueId = nodeScript.Attributes["ID"].Value;
                            //Kiểm tra ID có bị trùng không
                            if (MnuScriptDict.ContainsKey(valueId))
                            {
                                errList.Add(string.Format("ID = {0} của Script {1} đã tồn tại.", valueId,
                                                          BDTCHelper.GetCurrentXMLContent(nodeScript)));
                                //Gán ID khác do đã bị trùng
                                valueId = string.Format("Temp{0}", currentIndex);
                            }
                        }

                        //Nếu có lỗi
                        if (errList.Count > 0)
                            return errList;

                        //============================================
                        //Nếu script có attribute là ScrptFile
                        if (nodeScript.Attributes["ScrptFile"] != null)
                        {
                            ScrptFileScript objMnuScript = new ScrptFileScript(this, nodeScript);
                            //Trường hợp Script chứa File thì load nội dung của file Xml đó
                            errList.AddRange(((ScrptFileScript)objMnuScript).LoadContentXmlFile());
                            
                            //Lưu vào Dictionary theo ID key
                            MnuScriptDict.Add(valueId, objMnuScript);
                        }
                        else //Nếu là Script có attribute MnuItemRef
                        {
                            string msg = "";
                            MnuItemRefScript objMnuItemRefScript = new MnuItemRefScript(this, nodeScript);

                            //Kiểm tra xem MnuItemRef có gọi lại Id của MnuItem cha không. Nếu phải thì báo lỗi
                            // Ví dụ: một MnuItem như sau sẽ bị vòng lặp khi MnuItem Mnu_1.5 gọi lại chính Mnu_1.5 bên trong script Id=3
                            // <MnuItem ID="Mnu_1.5" Name="5_HLCV.xml" PosX="20" PosY="225" Width="98" Height="20" Title="">
                            // <Script ID="1" MnuItemRef="Mnu_1.5.1" Start="0" />
                            // <Script ID="2" MnuItemRef="Mnu_1.5.2" Start="Stop(2)" />
                            // <Script ID="3" MnuItemRef="Mnu_1.5" Start="0" />
                            // </MnuItem>
                            if (objMnuItemRefScript.MnuItemRef == this.ID)
                            {
                                msg = string.Format("MnuItem {0} bị gọi lại trong bên trong Script {1}", this.ToString(), objMnuItemRefScript.ToString());
                                errList.Add(msg);
                                return errList;
                            }
                            
                            //Load nội dung của MnuItemRef (load MnuItem được tham chiếu)
                            msg = objMnuItemRefScript.LoadMnuItemRef();
                            if (msg != "")
                            {
                                errList.Add(msg);
                                return errList;
                            }
                         
                            //Lưu vào Dictionary theo ID key
                            MnuScriptDict.Add(valueId, objMnuItemRefScript);
                        }
                    }
            }
            catch (Exception ex)
            {
                //Nếu chưa có lỗi nào mà bị exception thì throw
                if (errList.Count == 0)
                    throw ex;
            }
            return errList;
        }

        
        /// <summary>
        /// Kiểm tra Action xử lý có tham chiếu đến ID Ref bị pending không
        /// </summary>
        /// <returns></returns>
        public List<string> CheckPendingMnuScript(ref AbstractMnuScript objRefAction)
        {
            //string msg = "";
            List<string> errList = new List<string>();
            try
            {
                //====================================================================
                if (objRefAction.ObjTimerCalc == null)
                {
                    objRefAction.ObjTimerCalc = new TimerCalc();
                }

                //msg = string.Format("ID={0} phải nằm dưới ID={1} để tránh bị vòng lặp không giới hạn", ID, ObjTimerCalc.IDRefValue);
                //return msg;

                //Kiểm tra xem action này đã có trong pending Dict chưa
                //Nếu có rồi tức là xuất hiện vòng lặp treo khi các ID ref tham chiếu lẫn nhau
                if (!PendingMnuScriptDict.ContainsKey(objRefAction.ID))
                {
                    //Kiểm tra Action của IDRefValue đã được tính thời gian chưa (nếu rồi thì Start >= 0)
                    if (objRefAction.ObjTimerCalc.Start == -1)
                    {
                        PendingMnuScriptDict.Add(objRefAction.ID, objRefAction);
                        //Tính thời gian xử lý của pending action
                        errList.AddRange(objRefAction.CalculateScriptTimer());
                        //Nếu xử lý tính toán thời gian xong thì bỏ pending đi
                        if (errList.Count == 0)
                            PendingMnuScriptDict.Remove(objRefAction.ID);
                    }
                }
                else
                {
                    //Xuất hiện lỗi vòng lặp
                    if (PendingMnuScriptDict.Count > 0)
                    {
                        string msg = string.Format("<MnuItem> ID = {0} xuất hiện vòng lặp tham chiếu giữa các ID của các <Script>: ", ID);
                        List<string> arrID = PendingMnuScriptDict.Keys.ToList();
                        arrID.Add(arrID[0]);
                        msg += String.Join("-->", arrID);
                        errList.Add(msg);
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


        //Biến lưu thông tin tổng thời gian chạy của một <MnuItem>
        public float Duration = -1;
        /// <summary>
        /// Tính thời gian chạy của từng <Script> trong một MnuItem.
        /// Sau đó tính tổng thời gian chạy (duration) của một MnuItem.
        /// Nếu tính đã tính đúng hoặc tính xong thì duration phải >=0
        /// Nếu duration =-1 tức là chưa tính được
        /// </summary>
        /// <returns></returns>
        public List<string> CalculateMnuItemDuration()
        {
            //string msg = "";
            List<string> errList = new List<string>();
            float minStartTime = float.MaxValue; //lưu thời gian bắt đầu sớm nhất
            float maxEndTime = -1; //lưu thời gian kết thúc cuối cùng của một action

            Duration = -1; //duration sẽ bằng maxEndTime - minStartTime
            PendingMnuScriptDict = new Dictionary<string, AbstractMnuScript>();
            AbstractMnuScript objMnuScript;
            try
            {
                //Với mỗi <Script> ta tính từng thời gian kết thúc và chọn thời gian kết thúc lớn nhất làm durationn của một MnuItem
                foreach (KeyValuePair<string, AbstractMnuScript> pairMnuScript in MnuScriptDict)
                {
                    objMnuScript = pairMnuScript.Value;
                    errList.AddRange(objMnuScript.CalculateScriptTimer());
                    if (errList.Count > 0)
                        return errList;
                    
                    if (minStartTime > objMnuScript.ObjTimerCalc.Start)
                        minStartTime = objMnuScript.ObjTimerCalc.Start;
                    
                    if (maxEndTime < objMnuScript.ObjTimerCalc.Stop)
                        maxEndTime = objMnuScript.ObjTimerCalc.Stop;
                }
                Duration = maxEndTime - minStartTime;

            }
            catch (Exception ex)
            {
                if (errList.Count == 0)
                    throw ex;
            }

            return errList;
        }

        /// <summary>
        /// Load các <Script> trong MnuItem vào TreeView
        /// </summary>
        /// <param name="tvNode"></param>
        public void BindMnuItem_TreeNode(TreeNode tvNode)
        {
            foreach (KeyValuePair<string, AbstractMnuScript> pairScript in MnuScriptDict)
            {
                string key = string.Format("{0}-->{1}", tvNode.Name, pairScript.Key);
                //string text = string.Format("<{0}> - {1}", key, BDTCHelper.GetCurrentXMLContent(pairScript.Value.CurrentXmlNode));
                string text = pairScript.Value.ToString();
                
                TreeNode tvNodeScript = tvNode.Nodes.Add(key, text);
                tvNodeScript.Tag = pairScript.Value;
                BDTCHelper.FormatLeafNode(ref tvNodeScript);
            }
        }

        public override string ToString()
        {
            string value = "<MnuItem> is null";
            if (this.CurrentXmlNode != null)
            {
                //return BDTCHelper.GetCurrentXMLContent(this.CurrentXmlNode).Replace("</MnuItem>", "");
                value = string.Format("<MnuItem ID=\"{0}\"  Name=\"{1}\">", this.CurrentXmlNode.Attributes["ID"].Value,
                                      this.CurrentXmlNode.Attributes["Name"].Value);
            }

            return value;
        }
    }
}
