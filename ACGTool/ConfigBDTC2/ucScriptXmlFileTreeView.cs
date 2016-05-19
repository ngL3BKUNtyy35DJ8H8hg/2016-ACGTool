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
    public partial class ucScriptXmlFileTreeView : UserControl
    {
        public MyMnu _objMyMnu;

        public ucScriptXmlFileTreeView()
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

        #region Load một thư mục vào treeview
        
        private void ListDirectory(TreeView treeView, string path)
        {
            treeView.Nodes.Clear();
            var rootDirectoryInfo = new DirectoryInfo(path);
            treeView.Nodes.Add(CreateDirectoryNode(rootDirectoryInfo));
        }

        private static TreeNode CreateDirectoryNode(DirectoryInfo directoryInfo)
        {
            TreeNode directoryNode = new TreeNode(directoryInfo.Name);
            directoryNode.Name = directoryInfo.FullName;
            foreach (var directory in directoryInfo.GetDirectories())
                directoryNode.Nodes.Add(CreateDirectoryNode(directory));
            //foreach (var file in directoryInfo.GetFiles())
            //    directoryNode.Nodes.Add(new TreeNode(file.Name));
            return directoryNode;
        }
        #endregion

        //Load danh sách các file Script vào TreeView
        public List<string> LoadXmlFiles_TreeView(ref MyMnu objMyMnu)
        {
            List<string> errList = new List<string>();
            TreeNode tvNodeMnuItem, tvNodeScript;
            try
            {
                treeViewScript.Nodes.Clear();
                _objMyMnu = objMyMnu;
                //Load toàn bộ thư mục trong đường dẫn thư mục kịch bản
                ListDirectory(treeViewScript, _objMyMnu.ObjDiaHinh._objMyLastSaban._mySaBanDirFullPath);

                foreach (KeyValuePair<string, ScriptXmlFile> pairScript in _objMyMnu.ScriptXmlFileDict)
                {
                    //Kiểm tra xem file script có nằm trong thư mục đang chọn ở treeview không
                    TreeNode[] tvNodes = treeViewScript.Nodes.Find(Path.GetDirectoryName(pairScript.Key), true);
                    if (tvNodes.Length == 1)
                    {
                        tvNodeMnuItem = tvNodes[0].Nodes.Add(pairScript.Key, Path.GetFileName(pairScript.Key));
                        tvNodeMnuItem.Tag = pairScript.Value;
                        BDTCHelper.FormatLeafNode(ref tvNodeMnuItem);    
                    }
                    
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

        public TreeViewEventHandler treeViewScript_AfterSelectEvent;
        private void treeViewScript_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(treeViewScript_AfterSelectEvent != null)
            {
                treeViewScript_AfterSelectEvent(sender, e);
            }
        }

        
    }
}
