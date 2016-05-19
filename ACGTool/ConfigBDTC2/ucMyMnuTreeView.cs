using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using BDTCLib;
using BDTCLib.Scripts;

namespace ConfigBDTC
{
    public partial class ucMyMnuTreeView : UserControl
    {
        public MyMnu _objMyMnu;

        public ucMyMnuTreeView()
        {
            InitializeComponent();
            InitTreeView();
        }

        private void InitTreeView()
        {
             //Thời gian bắt đầu của mỗi file script
            treeViewScript.ImageList = ImageList1;
            //Cho phép hiện tooltips
            treeViewScript.ShowNodeToolTips = true;

            
        }

        public List<string> LoadMyMnu_TreeView(MyMnu objMyMnu)
        {
            List<string> errList = new List<string>();
            TreeNode tvNodeMnuItem, tvNodeScript;
            try
            {
                _objMyMnu = objMyMnu;
                treeViewScript.Nodes.Clear();
                foreach (KeyValuePair<string, MnuItem> pairMnuItem in _objMyMnu.MnuItemDict)
                {
                    //tvNodeMnuItem = treeViewScript.Nodes.Add(pairMnuItem.Key, BDTCHelper.GetCurrentXMLContent(pairMnuItem.Value.CurrentXmlNode).Replace("</MnuItem>", ""));
                    tvNodeMnuItem = treeViewScript.Nodes.Add(pairMnuItem.Key, pairMnuItem.Value.ToString());
                    tvNodeMnuItem.Tag = pairMnuItem.Value;
                    BDTCHelper.FormatNode(ref tvNodeMnuItem);
                    //foreach (KeyValuePair<string, AbstractMnuScript> pairScript in pairMnuItem.Value.MnuScriptDict)
                    //{
                    //    tvNodeScript = tvNodeMnuItem.Nodes.Add(string.Format("{0}.{1}", pairMnuItem.Key, pairScript.Key), BDTCHelper.GetCurrentXMLContent(pairScript.Value.CurrentXmlNode));
                    //    tvNodeScript.Tag = pairScript.Value;
                    //    //Nếu là MnuItemRef thì load tiếp 
                    //    if (pairScript.Value is MnuItemRefScript)
                    //    {
                    //        BDTCHelper.FormatFolderNode(ref tvNodeScript);
                    //        ((MnuItemRefScript)pairScript.Value).BindMnuItem_TreeNode(tvNodeScript);
                    //    }
                    //    else //Nếu là ScrptFile
                    //    {
                    //        BDTCHelper.FormatLeafNode(ref tvNodeScript);
                    //        ScrptFileScript objScrptFileScript = ((ScrptFileScript) pairScript.Value);
                    //        //Load nội dung của file Script
                    //        errList.AddRange(objScrptFileScript.LoadContentXmlFile());
                    //        foreach (var pairAction in objScrptFileScript.ObjScriptXmlFile.ActionDict)
                    //        {
                    //            //Load toàn bộ nội dung các action của file script vào treeview
                    //            TreeNode tvNodeAction = tvNodeScript.Nodes.Add(pairAction.Key, Path.GetFileName(pairAction.Key));
                    //            tvNodeAction.Tag = pairAction.Value;
                    //            BDTCHelper.FormatLeafNode(ref tvNodeAction);    
                    //        }
                            
                    //    }
                    //}
                }
                treeViewScript.Focus();
                treeViewScript.SelectedNode = treeViewScript.Nodes[0];
                return errList;
            }
            catch (Exception ex)
            {
                if(errList.Count == 0)
                    throw ex;
            }
            return errList;
        }

        public TreeViewEventHandler treeViewMyMnu_AfterSelectEvent;
        private void treeViewMyMnu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewMyMnu_AfterSelectEvent != null)
            {
                treeViewMyMnu_AfterSelectEvent(sender, e);
            }
        }
    }
}
