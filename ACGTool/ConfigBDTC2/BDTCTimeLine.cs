using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using BDTCLib;
using BDTCLib.Scripts;
using BDTCLib.Scripts.Actions;
using Braincase.GanttChart;
using System.Linq;

namespace ConfigBDTC
{
    public partial class BDTCTimeLine : Form
    {
        private ucMyMnuTreeView _objUcMyMnuTreeView;
        private ucScriptXmlFileTreeView _objUcScriptXmlFileTreeView;
        private ucBDTCActions _objUcBDTCActions;

        public BDTCTimeLine()
        {
            InitializeComponent();
            _objUcBDTCActions = new ucBDTCActions();
            _objUcBDTCActions.Dock = DockStyle.Fill;
            tabPageBDTCActions.Controls.Add(_objUcBDTCActions);


            // Attach event listeners for events we are interested in
            chartMyMnu.TaskMouseOver += new EventHandler<TaskMouseEventArgs>(_mChart_TaskMouseOver);
            chartMyMnu.TaskMouseOut += new EventHandler<TaskMouseEventArgs>(_mChart_TaskMouseOut);
            chartMyMnu.TaskSelected += new EventHandler<TaskMouseEventArgs>(_mChart_TaskSelected);
            //objChart.PaintOverlay += _mOverlay.ChartOverlayPainter;
            chartMyMnu.AllowTaskDragDrop = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                richTextBoxMyMnuLog.ForeColor = Color.Red;

                _objUcMyMnuTreeView = new ucMyMnuTreeView();
                _objUcMyMnuTreeView.Dock = DockStyle.Fill;
                splitContainerMyMnuTreeView.Panel1.Controls.Add(_objUcMyMnuTreeView);
                splitContainerMyMnuTreeView.SplitterDistance = 500;
                _objUcMyMnuTreeView.treeViewMyMnu_AfterSelectEvent = new TreeViewEventHandler(treeViewMyMnu_AfterSelect);

                _objUcScriptXmlFileTreeView = new ucScriptXmlFileTreeView();
                _objUcScriptXmlFileTreeView.Dock = DockStyle.Fill;
                splitContainerActions.Panel1.Controls.Add(_objUcScriptXmlFileTreeView);
                splitContainerActions.SplitterDistance = 300;

                _objUcScriptXmlFileTreeView.treeViewScript_AfterSelectEvent = new TreeViewEventHandler(treeViewScript_AfterSelect);
                if (Properties.Settings.Default.RecentDiaHinhFile != "")
                {
                    FileAttributes attr = File.GetAttributes(Properties.Settings.Default.RecentDiaHinhFile);
                    //detect whether its a directory or file
                    if ((attr & FileAttributes.Directory) != FileAttributes.Directory) //It isn't a folder
                    {
                        txtFilePath.Text = Properties.Settings.Default.RecentDiaHinhFile;
                        if (File.Exists(txtFilePath.Text))
                        {
                            LoadFile(txtFilePath.Text);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private splash sp;
        private void DoSplash()
        {
            sp = new splash();
            sp.ShowDialog();

        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                //mo form cho nguoi ta chon co so du lieu
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "DiaHinh File(*.diahinh)|*.diahinh";
                txtFilePath.Text = "";
                if (Properties.Settings.Default.RecentDiaHinhFile != "")
                    openFileDialog1.InitialDirectory = Path.GetDirectoryName(Properties.Settings.Default.RecentDiaHinhFile);
                openFileDialog1.ShowDialog();
                txtFilePath.Text = openFileDialog1.FileName;
                Properties.Settings.Default.RecentDiaHinhFile = txtFilePath.Text;
                Properties.Settings.Default.Save();

                btnReload.PerformClick();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        
        private void LoadFile(string filePath)
        {
            try
            {
                richTextBoxMyMnuLog.Text = "";
                TimeLineHelper._objDiaHinh = new DiaHinh(filePath);
                TimeLineHelper._objMyMnu = new MyMnu(TimeLineHelper._objDiaHinh);
                List<string> errList = TimeLineHelper._objMyMnu.LoadXmlContent();

                //Load MnyMnu into TreeView
                _objUcMyMnuTreeView.LoadMyMnu_TreeView(TimeLineHelper._objMyMnu);

                //Load Script Files into TreeView
                _objUcScriptXmlFileTreeView.LoadXmlFiles_TreeView(ref TimeLineHelper._objMyMnu);

                if (errList.Count > 0)
                {
                    richTextBoxMyMnuLog.Text += string.Join("\n", errList.ToArray()) ;
                }
                else
                {
                    if (richTextBoxMyMnuLog.Text == "")
                    {
                        richTextBoxMyMnuLog.Text += "Load successful!";
                    }
                }

                _objUcBDTCActions.InitMyMnu(TimeLineHelper._objMyMnu);

            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            


        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(DoSplash));
            th.Start();
            try
            {
                LoadFile(txtFilePath.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            if (sp != null)
            {
                sp.CloseSplash();
            }
            else
            {
                th.Abort();
            }
        }

        /// <summary>
        /// Dùng cho ScriptTreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewScript_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                ScriptXmlFile objScriptXmlFile = (ScriptXmlFile)e.Node.Tag;
                //TimeLineHelper.LoadScriptFileContent_TimeLine(objScriptXmlFile, _mChart);    
            }
        }

        /// <summary>
        /// Load nội dung của một MnuItem <MnuItem ID="Mnu_1.3.2" Name="3_Ta_Mui3_LuiRa.xml" PosX="1000" PosY="220" Width="170" Height="20" Title="">
        /// </summary>
        /// <param name="isRoot">Cho biết MnuItem này nằm ở root của TreeView hay được load từ <Script> có MnuItemRef</param>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<string> LoadMnuItemNode(TreeNode node)
        {
            List<string> errList = new List<string>();
            MnuItem objMnuItem = (MnuItem)node.Tag;
            try
            {
                if (node.Nodes.Count == 0)
                {
                    errList.AddRange(objMnuItem.LoadScripts());
                    if (errList.Count > 0)
                    {
                        richTextBoxMyMnuLog.Text = string.Join("\n", errList.ToArray()) + "\n";
                        return errList;
                    }

                    errList.AddRange(objMnuItem.CalculateMnuItemDuration());
                    if (errList.Count > 0)
                    {
                        richTextBoxMyMnuLog.Text = string.Join("\n", errList.ToArray()) + "\n";
                        return errList;
                    }

                    //Add các <Script> vào treeview
                    objMnuItem.BindMnuItem_TreeNode(node);

                    //Tiếp tục load nội dung bên trong của MnuItem vào TreeView
                    foreach (TreeNode childNode in node.Nodes)
                    {
                        if (childNode.Tag is MnuItemRefScript)
                        {
                            errList.AddRange(LoadMnuItemRefScriptNode(childNode));
                        }
                        else if (childNode.Tag is ScrptFileScript)
                        {
                            errList.AddRange(LoadScrptFileScriptNode(childNode));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                richTextBoxMyMnuLog.Text = string.Format("{0}: {1}", objMnuItem.ToString(), ex.Message);
            }
            return errList;
        }

        /// <summary>
        /// Load nội dung của script dạng <Script ID="2" ScrptFile="KichBan_DienBien\3_DienBien\03_NoSung\03_Ta_Mui3_LuiRa.xml" Start="1" />
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<string> LoadScrptFileScriptNode(TreeNode node)
        {
            List<string> errList = new List<string>();
            ScrptFileScript objScrptFileScript = (ScrptFileScript)node.Tag;
            try
            {
                if (node.Nodes.Count == 0)
                {
                    //Load nội dung của file Script
                    errList.AddRange(objScrptFileScript.LoadContentXmlFile());
                    if (errList.Count > 0)
                    {
                        richTextBoxMyMnuLog.Text = string.Join("\n", errList.ToArray()) + "\n";
                        return errList;
                    }

                    //Tính thời gian chạy của các <Script>
                    errList.AddRange(objScrptFileScript.CalculateScriptTimer());
                    if (errList.Count > 0)
                    {
                        richTextBoxMyMnuLog.Text = string.Join("\n", errList.ToArray()) + "\n";
                        return errList;
                    }

                    BDTCHelper.FormatLeafNode(ref node);
                    //Load nội dung các action của file script .xml
                    foreach (var pairAction in objScrptFileScript.ObjScriptXmlFile.ActionDict)
                    {
                        //Load toàn bộ nội dung các action của file script vào treeview
                        string key = string.Format("{0}-->{1}", node.Name, pairAction.Key);
                        //string text = string.Format("<{0}> - {1}", key, pairAction.Value);
                        string text = pairAction.Value.ToString();
                        TreeNode tvNodeAction = node.Nodes.Add(key, text);
                        tvNodeAction.Tag = pairAction.Value;
                        BDTCHelper.FormatLeafNode(ref tvNodeAction);
                    }
                }

                
            }
            catch (Exception ex)
            {
                richTextBoxMyMnuLog.Text = string.Format("{0}: {1}", objScrptFileScript.ToString(), ex.Message);
            }

            return errList;
        }

        /// <summary>
        /// Load treenode của <Script> có chứa MnuItemRef <Script ID="1" MnuItemRef="Mnu_1.3.1" Start="0" />
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private List<string> LoadMnuItemRefScriptNode(TreeNode node)
        {
            List<string> errList = new List<string>();
            MnuItemRefScript objMnuItemRefScript = (MnuItemRefScript)node.Tag;
            MnuItem objMnuItem = (MnuItem)objMnuItemRefScript.ObjMnuItem_Ref;
            try
            {
                if (node.Nodes.Count == 0)
                {
                    //Tính thời gian của 
                    errList.AddRange(objMnuItem.CalculateMnuItemDuration());
                    if (errList.Count > 0)
                    {
                        richTextBoxMyMnuLog.Text = string.Join("\n", errList.ToArray()) + "\n";
                        return errList;
                    }
                    //Load vào treeview
                    objMnuItemRefScript.BindMnuItem_TreeNode(node);

                    // Load nội dung của MnuItem được tham chiếu trong MnuItemRef 
                    // Ví dụ: script <Script ID="1" MnuItemRef="Mnu_1.3.1" Start="0" />
                    // Trong đó, MnuItem được tham chiếu trong MnuItemRef là "Mnu_1.3.1"
                    // MnuItem <MnuItem ID="Mnu_1.3.1" Name="3_Ta_Mui12_NoSung.xml" PosX="1000" PosY="200" Width="196" Height="20" Title="">
                    if (node.Nodes[0].Tag is MnuItem)
                    {
                        errList.AddRange(LoadMnuItemNode(node.Nodes[0]));
                    }
                }
            }
            catch (Exception ex)
            {
                richTextBoxMyMnuLog.Text = string.Format("{0}: {1}", objMnuItem.ToString(), ex.Message);
            }

            return errList;
        }

        private Dictionary<string, MyTask> _taskDict = new Dictionary<string, MyTask>();
        /// <summary>
        /// Dùng cho ScriptTreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeViewMyMnu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            List<string> errList = new List<string>();
            TreeNode node = e.Node;
            //node.Nodes.Clear();
            if (node.Tag is MnuItem && node.Parent == null)
            {
                //Copy mới đối tượng để add các <Script> vào với thời gian start sẽ thay đổi
                MnuItem objMnuItem = (MnuItem)node.Tag;
                node.Tag = objMnuItem;
                errList.AddRange(LoadMnuItemNode(node));

                //TimeLineHelper.LoadMnuItemContent_TimeLine(objMnuItem, chartMyMnu);
                _taskDict = new Dictionary<string, MyTask>();
                ProjectManager mManager = new ProjectManager();
                TimeLineHelper.LoadMnuItemContent_TimeLine(mManager, chartMyMnu, _taskDict, node, 0);
                TimeLineHelper.InitChart(mManager, chartMyMnu);

                foreach (var task in _taskDict)
                {
                    chartMyMnu.SetToolTip(task.Value, string.Join(", ", mManager.ResourcesOf(task.Value).Select(x => (x as MyResource).Name)));
                }
            }
            else if (node.Tag is ScrptFileScript)
            {
                //ScrptFileScript objScrptFileScript = ObjectCopier.Clone((ScrptFileScript)node.Tag);
                //errList.AddRange(LoadScrptFileScriptNode(node));
                //ScriptXmlFile objScriptXmlFile = objScrptFileScript.ObjScriptXmlFile;
                ////Hiển thị timeline
                //TimeLineHelper.LoadScriptFileContent_TimeLine(objScriptXmlFile, chartMyMnu);

                chartMyMnu.ScrollTo(_taskDict[node.Name]);
            }
            else if (node.Tag is MnuItemRefScript)
            {
                //MnuItemRefScript objMnuItemRefScript = ObjectCopier.Clone((MnuItemRefScript)node.Tag);
                //errList.AddRange(LoadMnuItemRefScriptNode(node));
                //TimeLineHelper.LoadMnuItemContent_TimeLine(objMnuItem, chartMyMnu);

                chartMyMnu.ScrollTo(_taskDict[node.Name]);
            }
        }

        void _mChart_TaskSelected(object sender, TaskMouseEventArgs e)
        {
            //_mTaskGrid.SelectedObjects = _mChart.SelectedTasks.Select(x => _mManager.IsPart(x) ? _mManager.SplitTaskOf(x) : x).ToArray();
            //_mResourceGrid.Items.Clear();
            //_mResourceGrid.Items.AddRange(_mManager.ResourcesOf(e.Task).Select(x => new ListViewItem(((MyResource)x).Name)).ToArray());
        }

        void _mChart_TaskMouseOut(object sender, TaskMouseEventArgs e)
        {
            //lblStatus.Text = "";
            _mChart.Invalidate();
        }

        void _mChart_TaskMouseOver(object sender, TaskMouseEventArgs e)
        {
            //lblStatus.Text = string.Format("{0} to {1}", _mManager.GetDateTime(e.Task.Start).ToLongDateString(), _mManager.GetDateTime(e.Task.End).ToLongDateString());
            _mChart.Invalidate();
        }

        

       
    }
}
