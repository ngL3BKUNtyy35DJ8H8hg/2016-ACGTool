using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace BDTCLib.Scripts
{
    /// <summary>
    /// Thông tin của một item <Script> </Script> trong file MyMnu.xml
    /// <Script ID="1" MnuItemRef="Mnu_1" Start="0" />
    /// </summary>
    public class MnuItemRefScript : AbstractMnuScript
    {
        public string MnuItemRef;

        /// <summary>
        /// MnuItem tham chiếu
        /// Ví dụ <Script ID="1" MnuItemRef="Mnu_1" Start="0" /> thì MnuItem là Mnu_1
        /// </summary>
        private MnuItem _objMnuItem_Ref;
        public MnuItem ObjMnuItem_Ref
        {
            get { return _objMnuItem_Ref; }
            set { _objMnuItem_Ref = value; }
        }

        //Lưu ID và file Script xml
        public MnuItemRefScript(MnuItem objMnuItem, XmlNode nodeScript)
        {
            try
            {
                //Lưu XmlNode hiện tại
                CurrentXmlNode = nodeScript;

                ObjMnuItem = objMnuItem;

                ID = nodeScript.Attributes["ID"].Value;
                MnuItemRef = nodeScript.Attributes["MnuItemRef"].Value;
                Start = nodeScript.Attributes["Start"].Value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Load MnuItem tham chiếu theo giá trị MnuItemRef.
        /// Ví dụ <Script ID="1" MnuItemRef="Mnu_1" Start="0" /> thì load MnuItem tham chiếu là Mnu_1
        /// </summary>
        /// <returns></returns>
        public string LoadMnuItemRef()
        {
            string result = "";
            //Kiểm tra MnuItemRef có trong MnuItem chưa
            if (ObjMnuItem.ObjMyMnu.MnuItemDict.ContainsKey(MnuItemRef))
            {
                //Load MnuItem tham chiếu
                _objMnuItem_Ref = ObjMnuItem.ObjMyMnu.MnuItemDict[MnuItemRef];
            }
            else
            {
                result = string.Format("MnuItemRef {0} của {1} không tồn tại.", MnuItemRef,
                                                  BDTCHelper.GetCurrentXMLContent(CurrentXmlNode));
            }
            return result;
        }


        /// <summary>
        /// Loafd MnuItem tham chiếu theo giá trị MnuItemRef vào treeview
        /// </summary>
        /// <param name="tvNode"></param>
       public void BindMnuItem_TreeNode(TreeNode tvNode)
       {
           string key = string.Format("{0}-->{1}", tvNode.Name, this.ObjMnuItem_Ref.ID);
           //string text = string.Format("<{0}> - {1}", key, BDTCHelper.GetCurrentXMLContent(ObjMnuItem_Ref.CurrentXmlNode));
           string text = ObjMnuItem_Ref.ToString();
           TreeNode tvNodeMnuItem = tvNode.Nodes.Add(key, text);
           tvNodeMnuItem.Tag = this.ObjMnuItem_Ref;
           BDTCHelper.FormatFolderNode(ref tvNodeMnuItem);

           ////Load trực tiếp các <Script> bên trong của MnuItem
           //foreach (KeyValuePair<string, AbstractMnuScript> pairMnuItemRef in ObjMnuItem_Ref.MnuScriptDict)
           //{
           //    TreeNode tvNodeMnuItemRefScript = tvNodeMnuItem.Nodes.Add(string.Format("{0}.{1}", tvNodeMnuItem.Name, pairMnuItemRef.Key), BDTCHelper.GetCurrentXMLContent(pairMnuItemRef.Value.CurrentXmlNode));
           //    tvNodeMnuItemRefScript.Tag = pairMnuItemRef.Value;
           //    //Nếu là MnuItemRef thì load tiếp 
           //    if (pairMnuItemRef.Value is MnuItemRefScript)
           //    {
           //        BDTCHelper.FormatFolderNode(ref tvNodeMnuItemRefScript);
           //        ((MnuItemRefScript)pairMnuItemRef.Value).BindMnuItem_TreeNode(tvNodeMnuItemRefScript);
           //    }
           //    else
           //    {
           //        BDTCHelper.FormatLeafNode(ref tvNodeMnuItemRefScript);
           //    }
           //}
       }

       /// <summary>
       /// Với mỗi <Script> có MnuItemRef ta sẽ tính thời gian bắt đầu và thời gian kết thúc thực sự
       /// ** Dùng cho trường hợp <Script ID="2" MnuItemRef="Mnu_1.5.1" Start="0" />
       /// </summary>
       /// <returns></returns>
       public override List<string> CalculateDuration()
       {
           //string msg = "";
           List<string> errList = new List<string>();
           try
           {
               //Nếu chưa tính thời gian của MnuItem Ref thì tính trước
               if (_objMnuItem_Ref.Duration == -1)
               {
                   errList.AddRange(ObjMnuItem_Ref.CalculateMnuItemDuration());
                   if (errList.Count > 0)
                       return errList;
               }

               ObjTimerCalc.Duration = ObjMnuItem_Ref.Duration;
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
       //        return this.CurrentXmlNode.OuterXml;
       //    }

       //    return value;
       //}
    }
}
