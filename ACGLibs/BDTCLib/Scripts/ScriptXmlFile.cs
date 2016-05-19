using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using BDTCLib.Scripts.Actions;

namespace BDTCLib.Scripts
{
    /// <summary>
    /// Thông tin của một file script .xml
    /// </summary>
    [Serializable]
    public partial class ScriptXmlFile
    {
        //Lưu nội dung xml
        private XmlDocument _objScriptXmlDocument;
        //Lưu đường dẫn file Script
        public string FilePath;

        //Danh sách các action trong file script .xml
        public Dictionary<string, Actions.AbstractAction> ActionDict;

        //Danh sách các action chưa xử lý tính toán thời gian xong
        //Dùng cho trường hợp một action đang tham chiếu đến action khác
        //Mà action đó lại chưa tính toán thời gian
        public Dictionary<string, Actions.AbstractAction> PendingActionDict;

        public ScriptXmlFile(string filePath)
        {
            FilePath = filePath;
        }

        /// <summary>
        /// Đọc toàn bộ nội dung của file script .xml
        /// </summary>
        public List<string> LoadContentXmlFile()
        {
            List<string> errList = new List<string>();
            int currentIndex = 0;
            string ID = "";
            try
            {
                if (!File.Exists(FilePath))
                {
                    errList.Add(string.Format("File script {0} không tồn tại", FilePath));
                    return errList;
                }

                ActionDict = new Dictionary<string, AbstractAction>();

                _objScriptXmlDocument = new XmlDocument();
                _objScriptXmlDocument.Load(FilePath);
                XmlNode nodeActions = _objScriptXmlDocument.DocumentElement;

                //Với mỗi Action
                foreach (XmlNode nodeAction in nodeActions.ChildNodes)
                    if (nodeAction.Name == "Action")
                    {
                        //============================================
                        //Kiểm tra logic
                        currentIndex++;
                        //Kiểm tra tồn tại ID không
                        string xmlContent = BDTCHelper.GetCurrentXMLContent(nodeAction).Replace("</Action>", "");
                        if (nodeAction.Attributes["ID"] == null)
                        {
                            errList.Add(string.Format("Action {0} trong file {1} chưa có thuộc tính ID", xmlContent, this.FilePath));
                            //Gán temp ID
                            ID = string.Format("TempID{0}", currentIndex);
                        }
                        else
                        {
                            ID = nodeAction.Attributes["ID"].Value;
                            if (ActionDict.ContainsKey(ID))
                            {
                                errList.Add(string.Format("ID = {0} của Action {1} trong file {2} đã tồn tại.", ID, xmlContent, this.FilePath));
                                //Gán ID khác do đã bị trùng
                                ID = string.Format("Temp{0}", currentIndex);
                            }
                        }

                        //============================================
                        //Khởi tạo đối tượng
                        AbstractAction objAction;
                        switch (nodeAction.Attributes["Type"].Value)
                        {
                            #region Các trường hợp case

                            case "Appear":
                                objAction = new AppearAction(this, nodeAction);
                                break;
                            case "AppearLeft":
                                objAction = new AppearLeftAction(this, nodeAction);
                                break;
                            case "AppearTop":
                                objAction = new AppearTopAction(this, nodeAction);
                                break;
                            case "AppearDown":
                                objAction = new AppearDownAction(this, nodeAction);
                                break;
                            case "AppearA":
                                objAction = new AppearAAction(this, nodeAction);
                                break;
                            case "Unhide":
                                objAction = new UnhideAction(this, nodeAction);
                                break;

                            case "Disappear":
                                objAction = new DisappearAction(this, nodeAction);
                                break;
                            case "DisappearLeft":
                                objAction = new DisappearLeftAction(this, nodeAction);
                                break;
                            case "DisappearTop":
                                objAction = new DisappearTopAction(this, nodeAction);
                                break;
                            case "DisappearDown":
                                objAction = new DisappearDownAction(this, nodeAction);
                                break;
                            case "DisappearA":
                                objAction = new DisappearAAction(this, nodeAction);
                                break;
                            case "Hide":
                                objAction = new HideAction(this, nodeAction);
                                break;

                            case "Description":
                                objAction = new DescriptionAction(this, nodeAction);
                                break;
                            case "Blink":
                                objAction = new BlinkAction(this, nodeAction);
                                break;
                            case "Comment":
                                objAction = new CommentAction(this, nodeAction);
                                break;
                            case "Bombard":
                                objAction = new BombardAction(this, nodeAction);
                                break;
                            case "ExplodeDcl":
                                objAction = new ExplodeDclAction(this, nodeAction);
                                break;
                            case "Fly":
                                objAction = new FlyAction(this, nodeAction);
                                break;
                            case "FocusAt":
                                objAction = new FocusAtAction(this, nodeAction);
                                break;
                            case "Move":
                                objAction = new MoveAction(this, nodeAction);
                                break;
                            case "Shoot":
                                objAction = new ShootAction(this, nodeAction);
                                break;
                            case "CornerTitle":
                                objAction = new CornerTitleAction(this, nodeAction);
                                break;
                            default:
                                objAction = new ShootAction(this, nodeAction);
                                break;
                            #endregion
                        }

                        //============================================
                        //Lưu vào Dictionary nếu không trùng ID
                        ActionDict.Add(ID, objAction);
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
        /// Kiểm tra Action xử lý có tham chiếu đến ID Ref bị pending không
        /// </summary>
        /// <returns></returns>
        public string CheckPendingAction(ref AbstractAction objRefAction)
        {
            string msg = "";
            //====================================================================
            if (objRefAction.ObjTimerCalc == null)
            {
                objRefAction.ObjTimerCalc = new TimerCalc();
            }

            //msg = string.Format("ID={0} phải nằm dưới ID={1} để tránh bị vòng lặp không giới hạn", ID, ObjTimerCalc.IDRefValue);
            //return msg;

            //Kiểm tra xem action này đã có trong pending Dict chưa
            //Nếu có rồi tức là xuất hiện vòng lặp treo khi các ID ref tham chiếu lẫn nhau
            if (!PendingActionDict.ContainsKey(objRefAction.ID))
            {
                //Kiểm tra Action của IDRefValue đã được tính thời gian chưa (nếu rồi thì Start >= 0)
                if (objRefAction.ObjTimerCalc.Start == -1)
                {
                    PendingActionDict.Add(objRefAction.ID, objRefAction);
                    //Tính thời gian xử lý của pending action
                    msg = objRefAction.CalculateActionTimer();
                    //Nếu xử lý tính toán thời gian xong thì bỏ pending đi
                    if (msg == "")
                       PendingActionDict.Remove(objRefAction.ID);
                }
            }
            else
            {
                //Xuất hiện lỗi vòng lặp
                if (PendingActionDict.Count > 0)
                {
                    msg = string.Format("File Script {0} bị xuất hiện vòng lặp tham chiếu giữa các ID:",
                                        FilePath);
                    List<string> arrID = PendingActionDict.Keys.ToList();
                    arrID.Add(arrID[0]);
                    msg += String.Join("-->", arrID);
                }
            }
            return msg;
        }

        //Biến lưu thông tin tổng thời gian chạy của một File
        public float Duration = -1;
        /// <summary>
        /// Với mỗi Action trong xml ta sẽ tính được thời gian bắt đầu và thời gian kết thúc thực sự
        /// </summary>
        /// <returns></returns>
        public List<string> CalculateActionTimer()
        {
            //string msg = "";
            List<string> errList = new List<string>();
            float maxEndTime = 0; //lưu thời gian kết thúc cuối cùng của một action
            Duration = 0;
            PendingActionDict = new Dictionary<string, AbstractAction>();
            
            try
            {
                //Nếu chưa load nội dung file xml thì load lại
                if (ActionDict == null)
                {
                    errList.AddRange(this.LoadContentXmlFile());
                    if (errList.Count > 0)
                        return errList;
                }

                foreach (KeyValuePair<string, AbstractAction> pairAction in ActionDict)
                {
                    AbstractAction objAction = pairAction.Value;
                    string msg = objAction.CalculateActionTimer();
                    if (msg != "")
                    {
                        errList.Add(msg);
                        return errList;    
                    }

                    maxEndTime = objAction.ObjTimerCalc.Stop;
                    if (Duration < maxEndTime)
                        Duration = maxEndTime;
                }

            }
            catch (Exception ex)
            {
                if (errList.Count == 0)
                    throw ex;
            }
            
            return errList;
        }

        public List<string> BindMnuItem_TreeNode(TreeNode tvNode)
        {

            List<string> errList = new List<string>();
            try
            {
                if (BDTCHelper.IsFilePath(FilePath))
                {
                    //Load nội dung của file Script
                    errList.AddRange(LoadContentXmlFile());
                    if (errList.Count > 0)
                    {
                        return errList;
                    }

                    foreach (var pairAction in ActionDict)
                    {
                        //Load toàn bộ nội dung các action của file script vào treeview
                        TreeNode tvNodeAction = tvNode.Nodes.Add(pairAction.Key, pairAction.Value.ToString());
                        tvNodeAction.Tag = pairAction.Value;
                        BDTCHelper.FormatLeafNode(ref tvNodeAction);
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
    }
}
