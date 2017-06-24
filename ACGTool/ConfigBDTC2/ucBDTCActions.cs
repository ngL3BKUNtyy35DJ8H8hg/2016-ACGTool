using System;
using System.Collections;
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
using System.Diagnostics;

namespace ConfigBDTC
{
    public partial class ucBDTCActions : UserControl
    {
       private XmlDocument _myMnuDoc;

        // Populate the list box using an array as DataSource.
        private ArrayList actionList = new ArrayList();

        private DiaHinh _objDiaHinh;
        public MyMnu _objMyMnu;

        public ucBDTCActions()
        {
            InitializeComponent();
            
        }

        public void InitMyMnu(MyMnu objMyMnu)
        {
            _objDiaHinh = objMyMnu.ObjDiaHinh;
            _objMyMnu = objMyMnu;
        }

        private List<string> ShowAttributeList;
        private void ucBDTCActions_Load(object sender, EventArgs e)
        {
            comboBoxActionFilter.SelectedIndex = 0;
            //dataGridViewAttributes.Columns.Add("Attribute", "Attribute");

            //dataGridViewAttributes.Columns.Add("Value", "Value");
            //dataGridViewAttributes.Columns[1].Width = 250;

            checkBoxReplaceText_CheckedChanged(null, null);
        }

        private void treeViewScript_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeViewScript.SelectedNode.Level == 0)
            {
                
            }
            else if (treeViewScript.SelectedNode.Level == 1)
            {
                string[] result = treeViewScript.SelectedNode.Name.Split(new string[] { "[]" }, StringSplitOptions.None);

                XmlNode node;
                node = _myMnuDoc.DocumentElement;
                foreach (XmlNode nodeMnuItem in node.ChildNodes)
                {
                    if (nodeMnuItem.Name == "#comment")
                        continue;

                    if (nodeMnuItem.Attributes["Name"].Value == result[0])
                    {
                        richTextBoxScript.Text = nodeMnuItem.InnerXml.Replace("</Script>", "</Script>" + Environment.NewLine);
                        break;
                    }
                }
            }
            else if (treeViewScript.SelectedNode.Level == 2)
            {
                //Xử lý StreamReader để đọc file rồi mới gán vào richtextbox
                //Vì nếu dùng richTextBoxView.LoadFile để load file thì không hiển thị được tiếng việt
                string[] result = treeViewScript.SelectedNode.Name.Split(new string[] {"[]"}, StringSplitOptions.None);
                string filePath = _objDiaHinh._myCurrentDirectory + "\\" + result[1];

                //Hiển thị nội dung ở richtextbox
                StreamReader reader = new StreamReader(filePath);
                while (reader.Peek() >= 0)
                {
                    string str = reader.ReadToEnd();
                    richTextBoxScript.Text = str;
                }
                reader.Close();
                treeViewScript.Focus();

                //Nếu không phải là load toàn bộ action
                if (_isLoadComplete)
                {
                    //Load action theo xml file
                    actionList = new ArrayList();
                    LoadActionsByXmlFile(filePath);
                    if (actionList.Count > 0)
                    {
                        listBoxActions.DataSource = actionList;
                        listBoxActions.DisplayMember = "Action";
                        listBoxActions.ValueMember = "XmlFile";
                    }
                }
            }
        }

        /// <summary>
        /// Cho biết trạng thái là load toàn bộ các action trong các file .xml luôn 
        /// hay chỉ load riêng một file .xml
        /// = false: tức là cho phép load các action theo từng file .xml riêng
        /// = true: tức là cho phép load toàn bộ action của tất cả các file .xml
        /// </summary>
        private bool _isLoadComplete = false;

        private void buttonScript_Click(object sender, EventArgs e)
        {
            dataGridViewAttributes.Rows.Clear();
            listBoxActions.DataSource = null;
            listViewXmlFiles.Items.Clear();
            treeViewScript.Nodes.Clear();


            //Set trạng thái là chưa load toàn bộ action của các file .xml
            _isLoadComplete = false;
            //Lưu các action hiển thị trong listbox
            actionList = new ArrayList();

            //Thời gian bắt đầu của mỗi file script
            treeViewScript.ImageList = ImageList1;
            //Cho phép hiện tooltips
            treeViewScript.ShowNodeToolTips = true;

            //Đọc nội dung xml của file MyMnu.xml
            _myMnuDoc = new XmlDocument();
            _myMnuDoc.Load(_objMyMnu.MyMnuXmlFile);
            XmlNode node = _myMnuDoc.DocumentElement;
            
            TreeNode tvNodeMnu, tvNodeMnuItem, tvNodeScript;
            
            //Lấy tên của MnuItem rồi Add vào node cha
            tvNodeMnu = treeViewScript.Nodes.Add(node.Name, node.Name);
            BDTCHelper.FormatNode(ref tvNodeMnu);

            //Với mỗi MnuItem
            foreach (XmlNode nodeMnuItem in node.ChildNodes)
                if (nodeMnuItem.Name == "MnuItem")
                {
                    //==========================================
                    //Khởi tạo treeview
                    //==========================================
                    //richTextBoxLog.Text += string.Format("Khởi tạo treeview cho MnuItem {0}", nodeMnuItem.Attributes["Name"].Value) + Environment.NewLine;
                    
                    //Lấy tên của MnuItem rồi Add vào node cha
                    string keyNode = nodeMnuItem.Attributes["Name"].Value;
                    tvNodeMnuItem = tvNodeMnu.Nodes.Add(keyNode, nodeMnuItem.Attributes["Name"].Value);
                    BDTCHelper.FormatNode(ref tvNodeMnuItem);

                    //Lấy ID của mỗi script thuộc MnuItem rồi Add vào node con
                    foreach (XmlNode nodeScript in nodeMnuItem.ChildNodes)
                        if (nodeScript.Name == "Script")
                        {
                            //Kiểm tra file có tồn tại thuộc tính ID không
                            if (nodeScript.Attributes["ID"] == null)
                            {
                                //Nếu script có attribute là ScrptFile
                                if (nodeScript.Attributes["ScrptFile"] != null)
                                {
                                    //Kiểm tra xem nếu tồn tại nhiều Script trong một MnuItem thì bắt buộc phải tồn tại ID
                                    if (nodeMnuItem.ChildNodes.Count > 1)
                                    {
                                        string err = string.Format("Script \"{0}\" của MnuItem \"{1}\" chưa có thuộc tính ID",
                                                          nodeScript.Attributes["ScrptFile"].Value,
                                                          nodeMnuItem.Attributes["Name"].Value);
                                        MessageBox.Show(err);
                                        //richTextBoxLog.Text += err + Environment.NewLine;
                                        return;
                                    }
                                }
                            }

                            //Nếu tồn tại đường dẫn chứa file .xml
                            if (nodeScript.Attributes["ScrptFile"] != null)
                            {
                                //Lấy đường dẫn 
                                string filePath = _objDiaHinh._myCurrentDirectory + "\\" + nodeScript.Attributes["ScrptFile"].Value.Trim();
                                //Kiểm tra file có tồn tại hay không
                                if (!File.Exists(filePath))
                                {
                                    string err = string.Format("File {0} không tồn tại", filePath);
                                    MessageBox.Show(err);
                                    //richTextBoxLog.Text += err + Environment.NewLine;
                                    return;
                                }

                                //Thêm vào treeview những file .xml thỏa điều kiện filter
                                keyNode = string.Format("{0}[]{1}", nodeMnuItem.Attributes["Name"].Value,
                                                        nodeScript.Attributes["ScrptFile"].Value);
                                //Nếu treeview chưa có thì add vào
                                if (!treeViewScript.Nodes.ContainsKey(keyNode))
                                {
                                    tvNodeScript = tvNodeMnuItem.Nodes.Add(keyNode,
                                                                           nodeScript.Attributes["ScrptFile"].Value);
                                    //Hiển thị màu sắc trạng thái lọc các file .xml theo Action cần lọc
                                    //Nếu màu gray thì tức là file .xml không chứa các action đó
                                    if (LoadActionsByXmlFile(filePath))
                                    {
                                        BDTCHelper.FormatLeafNode(ref tvNodeScript);
                                    }
                                    else
                                    {
                                        BDTCHelper.FormatLeafNode(ref tvNodeScript, Color.Gray);
                                    }
                                }
                            }
                        }
                    
                }

            if (actionList.Count > 0)
            {
                listBoxActions.DataSource = actionList;
                listBoxActions.DisplayMember = "Action";
                listBoxActions.ValueMember = "XmlFile";
            }

            treeViewScript.ExpandAll();
            treeViewScript.Focus();
            treeViewScript.SelectedNode = treeViewScript.Nodes[0];

            //Set trạng thái là đã load toàn bộ action của các file .xml
            _isLoadComplete = true;

            
        }

        private bool FilterAction(XmlNode nodeAction)
        {
            bool result = false;
#if DEBUG
            if (nodeAction.Attributes["Type"].Value != "Description")
                result = false;
#endif

            //Kiểm tra xem Text combobox có chứa giá trị của attribute "Type" không
            if (comboBoxActionFilter.SelectedIndex <= 2
                && nodeAction.Attributes["Type"].Value == "Description")
            {
                //Kiem tra xem thuoc tinh Pos co ton tai khong
                if (nodeAction.Attributes["Pos"] != null)
                {
                    //Kiểm tra xem nếu index combobox là Description giữa 
                    if (comboBoxActionFilter.SelectedIndex == 0 && nodeAction.Attributes["Pos"].Value == "Duoi")
                        result = true;
                    else if (comboBoxActionFilter.SelectedIndex == 1 && nodeAction.Attributes["Pos"].Value == "Giua")
                        result = true;
                    else if (comboBoxActionFilter.SelectedIndex == 2 &&
                             nodeAction.Attributes["Pos"].Value == "Tren")
                        result = true;
                }
                else //Description dưới không có attribute Pos
                {
                    if (comboBoxActionFilter.SelectedIndex == 0)
                        result = true;
                }
            }
            else if (comboBoxActionFilter.Text == nodeAction.Attributes["Type"].Value) //các thuộc tinh khác Description
            {
                result = true;
            }
            return result;
        }
        /// <summary>
        /// Load Actions by XML File
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool LoadActionsByXmlFile(string filePath)
        {
            //Cho biết file xml có chứa action cần filter không
            bool isExist = false;
            //Kiểm tra action filter
            if (comboBoxActionFilter.SelectedIndex >= 0)
            {
                //Lưu lại xml của file script
                XmlDocument scriptDoc = new XmlDocument();
                scriptDoc.Load(filePath);
                //Kiểm tra file này có chứa thuyết minh không?
                XmlNode scriptNode = scriptDoc.DocumentElement;
                //Với mỗi Action
                foreach (XmlNode nodeAction in scriptNode.ChildNodes)
                    if (nodeAction.Name == "Action")
                    {
                        //if (comboBoxActionFilter.Text.Contains(nodeAction.Attributes["Type"].Value.ToString()))
                        if (FilterAction(nodeAction))
                        {
                            //Đối với những Action có thuộc tính ObjName
                            if (nodeAction.Attributes["ObjName"] != null)
                            {
                                //Nếu có nhập giá trị filter ObjName
                                if (!string.IsNullOrEmpty(txtObjName.Text))
                                {
                                    //Kiểm tra ObjName Attribute có thỏa điều kiện filter
                                    if (nodeAction.Attributes["ObjName"].Value.Contains(txtObjName.Text))
                                    {
                                        actionList.Add(new ClassAction(nodeAction.OuterXml, filePath));
                                        isExist = true;
                                    }
                                }
                                else
                                {
                                    actionList.Add(new ClassAction(nodeAction.OuterXml, filePath));
                                    isExist = true;
                                }
                            }
                            else
                            {
                                actionList.Add(new ClassAction(nodeAction.OuterXml, filePath));
                                isExist = true;
                            }
                        }
                    }
            }
            return isExist;
        }

        #region "treeViewScript"

        #region "treeViewScript Functions"

        

        #endregion

       

        #endregion

        private void listBoxActions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxActions.SelectedIndex != -1)
            {
                
                dataGridViewAttributes.Rows.Clear();
                ClassAction objClassAction = (ClassAction)listBoxActions.SelectedItem;
                string filePath = objClassAction.XmlFile;
                string outerXml = objClassAction.Action;
                toolStripStatusLabelAction.Text = filePath;
                //Lưu lại xml của file script
                XmlDocument scriptDoc = new XmlDocument();
                scriptDoc.Load(filePath);
                //Kiểm tra file này có chứa thuyết minh không?
                XmlNode scriptNode = scriptDoc.DocumentElement;
                //Với mỗi Action
                foreach (XmlNode nodeAction in scriptNode.ChildNodes)
                    if (nodeAction.Name == "Action")
                    {
                        if (FilterAction(nodeAction) 
                            && nodeAction.OuterXml == outerXml)
                        {
                            foreach (XmlAttribute att in nodeAction.Attributes)
                            {
                                if (ShowAttributeList.Contains(att.Name))
                                    dataGridViewAttributes.Rows.Add(false, att.Name, att.Value);
                            }
                        }
                    }
            }
        }

        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            string att = "";
            if (checkBoxReplaceText.Checked)
            {
                if (txtFindWhat.Text == "")
                {
                    MessageBox.Show("Find what is empty!");
                    return;
                }

                if (txtReplaceWith.Text == "")
                {
                    MessageBox.Show("Replace with is empty!");
                    return;
                }

                //Lấy thuộc tính đang chọn trong dataGridViewAttributes
                att = dataGridViewAttributes.Rows[dataGridViewAttributes.CurrentCell.RowIndex].Cells["Attribute"].Value.ToString();
                //Lấy giá trị cần replace trong xml
            }

            
            foreach (var item in listBoxActions.Items)
            {
                ClassAction objClassAction = (ClassAction)item;
                string filePath = objClassAction.XmlFile;
                string outerXml = objClassAction.Action;
                XmlDocument scriptDoc = new XmlDocument();
                scriptDoc.Load(filePath);
                XmlNode scriptNode = scriptDoc.DocumentElement;
                //Với mỗi Action trong file xml
                foreach (XmlNode nodeAction in scriptNode.ChildNodes)
                {
                    if (nodeAction.Name == "Action")
                    {
                        if (FilterAction(nodeAction) && nodeAction.OuterXml == outerXml)
                        {
                            //Nếu là thay giá trị bằng tìm kiếm trong action
                            if (checkBoxReplaceText.Checked)
                            {
                                //Lấy giá trị cần replace trong xml
                                string value = nodeAction.Attributes[att].Value;
                                //Thay thế giá trị mới
                                nodeAction.Attributes[att].Value = value.Replace(txtFindWhat.Text, txtReplaceWith.Text);
                            }
                            else //Nếu là chọn cụ thể attribute cần thay
                            {
                                foreach (DataGridViewRow row in dataGridViewAttributes.Rows)
                                {
                                    if ((bool)row.Cells["Select"].Value)
                                    {
                                        att = row.Cells["Attribute"].Value.ToString();
                                        string value = row.Cells["Value"].Value != null ? row.Cells["Value"].Value.ToString() : string.Empty;
                                        nodeAction.Attributes[att].Value = value;
                                    }
                                }

                                //Nếu Action là FocusAt thì set mặc định duration là 0.5
                                if (nodeAction.Attributes["Type"].Value == "FocusAt")
                                {
                                    if (nodeAction.Attributes["Duration"] == null)
                                    {
                                        XmlAttribute durationNode = scriptDoc.CreateAttribute("Duration");
                                        durationNode.Value = "0.5";
                                        nodeAction.Attributes.InsertAfter(durationNode, nodeAction.Attributes["Start"]);
                                    }
                                }
                            }

                        }
                        //Lưu lại file .xml sau khi cập nhật
                        scriptDoc.Save(filePath);
                    }
                }
            }
            buttonScript_Click(null, null);
            checkBoxReplaceText.Checked = false;
            MessageBox.Show("Update all complete!");

        }

        private void comboBoxActionFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxActionFilter.SelectedIndex <= 2){
                //Description dưới, giữa, trên
                ShowAttributeList = new List<string> { "Type", "DescText", "SoundName" };
            }
            else if (comboBoxActionFilter.SelectedIndex == 3){ 
                //Bombard
                ShowAttributeList = new List<string> { "Type", "ImageFile","Width", "Height", "Duration", "Speed", "dAngle", "SoundName", "SoundLoop", "ExplID", "Repeat" };
            }
            else if (comboBoxActionFilter.SelectedIndex == 4)
            {
                //<Action ID="16" Type="Shoot" ObjName="Ta_PhucKich" ImageFile="Ta_Dan.bmp" Width="0.2" Height="0.2" Start="0" Duration="0.4" Speed="0.4" SoundName="gun_shotgun1.wav" SoundLoop="1" ExplID="" Repeat="5">
                //Shoot
                ShowAttributeList = new List<string> { "Type", "ImageFile", "Width", "Height", "Duration", "Speed", "SoundName", "SoundLoop", "ExplID", "Repeat" };
            }
            else if (comboBoxActionFilter.SelectedIndex == 5)
            {
                //<Action ID="9" Type="FocusAt" Start="0" CenterX="0.6330777" CenterY="0.6922352" cameraPosX="0" cameraPosY="-29" cameraPosZ="-14.5" angleZ="-0.009999994" angleX="-0.3837993"></Action>
                //FocusAt
                ShowAttributeList = new List<string> { "Type", "Duration"};
            }
            else if (comboBoxActionFilter.SelectedIndex == 6)
            {
                //<Action ID="6" Type="Fly" ObjName="Dich_MayBayNB_1" Start="0" Duration="20" Hide="true" SoundName="b17-01.wav" SoundLoop="1">
                //Fly
                ShowAttributeList = new List<string> { "Type", "SoundName" };
            }
            else if (comboBoxActionFilter.SelectedIndex == 7)
            {
                //<Action ID="7" Type="Move" ObjName="Dich_XeTang_CayDua1" Start="0" Duration="20" Hide="true" SoundName="xetang.wav" SoundLoop="1">
                //Move
                ShowAttributeList = new List<string> { "Type", "SoundName"};
            }
            else if (comboBoxActionFilter.SelectedIndex == 8)
            {
                //<Action ID="3" Type="CornerTitle" Start="0" Duration="0" CornerText="Giai đoạn 1 từ 13/09 đến 05/10/1972  &#xA;     TH1: Đánh địch cơ động triển khai tiến công"></Action>
                //CornerTitle
                ShowAttributeList = new List<string> { "Type", "CornerText" };
            }
            else if (comboBoxActionFilter.SelectedIndex == 9)
            {
                //<Action ID="1" Type="Explode" ObjName="Dich_MayBayNB_5" ImageFile="explosion1.bmp" Width="3" Height="3" ShiftZ="0" Start="0" Duration="1" Speed="60" SoundName="explosion.wav" SoundLoop="0">
                //Explode
                ShowAttributeList = new List<string> { "Type", "ImageFile", "Width", "Height", "ShiftZ", "Duration", "Speed", "SoundName", "SoundLoop" };
            }
            else if (comboBoxActionFilter.SelectedIndex == 10)
            {
                //<Action ID="Boom" Type="ExplodeDcl" ImageFile="explosion1.bmp" Width="2" Height="2" ShiftZ="1" Start="0" Speed="80" Duration="0.5" SoundName="explosion.wav" SoundLoop="0">
                //ExplodeDcl
                ShowAttributeList = new List<string> { "Type", "ImageFile", "Width", "Height", "ShiftZ", "Duration", "Speed", "SoundName", "SoundLoop" };

            }
        }

        private void btnSaveXml_Click(object sender, EventArgs e)
        {
            //XmlDocument scriptDoc = new XmlDocument();
            
            if (treeViewScript.SelectedNode.Text != "")
            {
                string filePath = _objDiaHinh._myCurrentDirectory + "\\" + treeViewScript.SelectedNode.Text;
                StreamWriter writer = new StreamWriter(filePath);
                writer.Write(richTextBoxScript.Text);
                writer.Close();
            }
        }


        private void checkBoxReplaceText_CheckedChanged(object sender, EventArgs e)
        {
            
            if (checkBoxReplaceText.Checked)
            {
                listBoxActions_SelectedIndexChanged(null, null);
                dataGridViewAttributes.ReadOnly = true;
                txtFindWhat.Enabled = true;
                txtReplaceWith.Enabled = true;
            }
            else
            {
                dataGridViewAttributes.ReadOnly = false;
                txtFindWhat.Enabled = false;
                txtReplaceWith.Enabled = false;
                txtFindWhat.Text = txtReplaceWith.Text = "";
            }
        }

        private void dataGridViewAttributes_CurrentCellChanged(object sender, EventArgs e)
        {
            if (checkBoxReplaceText.Checked && dataGridViewAttributes.CurrentCell != null)
            {
                txtFindWhat.Text = dataGridViewAttributes.Rows[dataGridViewAttributes.CurrentCell.RowIndex].Cells["Value"].Value.ToString();
            }
        }

        

        

      

        /// <summary>
        /// Kiểm tra các file xml có dùng trong kịch bản không
        /// </summary>
        private void CheckXmlFiles()
        {
            listViewXmlFiles.Items.Clear();

            DirectoryInfo di = new DirectoryInfo(_objDiaHinh._objMyLastSaban._mySaBanDirFullPath);
            FileInfo[] fileInfoArray = di.GetFiles("*.*", SearchOption.AllDirectories);

            foreach (FileInfo fileInfo in fileInfoArray)
            {
                //Bỏ qua các file mặc định của chương trình
                if (fileInfo.Name == "MyMnu.xml" || 
                    fileInfo.Name == "D3DBillboards.xml" || 
                    fileInfo.Name == "D3DModels.xml" || 
                    fileInfo.Name == "D3DSounds.xml" || 
                    fileInfo.Name == "D3DTexObjs.xml" || 
                    fileInfo.Name == "DienTapMap.jpg" || 
                    fileInfo.Name.Contains("TexObj"))
                    continue;
                
                //Add into listview
                ListViewItem item = new ListViewItem(fileInfo.Name, fileInfo.Name);
                //Full path
                item.SubItems.Add(fileInfo.DirectoryName + "\\" + fileInfo.Name);

                //Kiểm tra fileInfo có dùng trong kịch bản không
                if (_objMyMnu.ScriptXmlFileDict.ContainsKey(fileInfo.FullName))
                {
                    item.SubItems.Add("Đang dùng");
                }
                else
                {
                    item.SubItems.Add("Không dùng");
                    //Nếu không phải báo lỗi thì tô màu khác
                    if (item.ForeColor != Color.Red)
                        item.ForeColor = Color.Orange;
                }

                listViewXmlFiles.Items.Add(item);
            }
        }

        private void btnCheckXmlFiles_Click(object sender, EventArgs e)
        {
            //Kiểm tra các file xml có dùng trong kịch bản không
            CheckXmlFiles();
        }

        private void btnDeleteXmlFile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa những file .xml (hoặc file .*) không dùng?", "Cảnh báo", MessageBoxButtons.YesNo) ==
                System.Windows.Forms.DialogResult.Yes)
            {
                foreach (ListViewItem item in listViewXmlFiles.Items)
                {
                    if (item.SubItems[2].Text == "Không dùng")
                        File.Delete(item.SubItems[1].Text);
                }
            }

            CheckXmlFiles();
        }

        private void btnDefaultValues_Click(object sender, EventArgs e)
        {
            Dictionary<string, string> DefaultValues = new Dictionary<string, string>();
            if (comboBoxActionFilter.SelectedIndex <= 2)
            {
                //Description dưới, giữa, trên
                //ShowAttributeList = new List<string> { "Type", "DescText", "SoundName" };
               
            }
            else if (comboBoxActionFilter.SelectedIndex == 3)
            {
                //Bombard
                //"Type", "ImageFile", "Width", "Height", "Duration", "Speed", "dAngle", "SoundName", "SoundLoop", "ExplID", "Repeat" 
                if (comboBoxDichTa.SelectedIndex == 0)
                {
                    DefaultValues.Add("ImageFile", "Ta_DanCoi.png");
                    DefaultValues.Add("Width", "1");
                    DefaultValues.Add("Height", "0.5");
                    DefaultValues.Add("Duration", "2");
                    DefaultValues.Add("Speed", "2");
                    DefaultValues.Add("dAngle", "-45");
                    DefaultValues.Add("SoundName", "gun_shotgun1.wav");
                    DefaultValues.Add("SoundLoop", "1");
                    DefaultValues.Add("ExplID", "Boom");
                    DefaultValues.Add("Repeat", "1");
                }
                else
                {
                    DefaultValues.Add("ImageFile", "Dich_DanCoi.png");
                    DefaultValues.Add("Width", "0.5");
                    DefaultValues.Add("Height", "0.5");
                    DefaultValues.Add("Duration", "2");
                    DefaultValues.Add("Speed", "2");
                    DefaultValues.Add("dAngle", "-45");
                    DefaultValues.Add("SoundName", "gun_shotgun1.wav");
                    DefaultValues.Add("SoundLoop", "1");
                    DefaultValues.Add("ExplID", "Boom");
                    DefaultValues.Add("Repeat", "1");
                }

                
            }
            else if (comboBoxActionFilter.SelectedIndex == 4)
            {
                //<Action ID="16" Type="Shoot" ObjName="Ta_PhucKich" ImageFile="Ta_Dan.bmp" Width="0.2" Height="0.2" Start="0" Duration="0.4" Speed="0.4" SoundName="gun_shotgun1.wav" SoundLoop="1" ExplID="" Repeat="5">
                //Shoot
                //ShowAttributeList = new List<string> { "Type", "ImageFile", "Width", "Height", "Duration", "Speed", "SoundName", "SoundLoop", "ExplID", "Repeat" };
                if (comboBoxDichTa.SelectedIndex == 0)
                {
                    DefaultValues.Add("ImageFile", "Ta_Dan.bmp");
                    DefaultValues.Add("Width", "0.2");
                    DefaultValues.Add("Height", "0.2");
                    DefaultValues.Add("Duration", "0.4");
                    DefaultValues.Add("Speed", "0.4");
                    DefaultValues.Add("SoundName", "gun_shotgun1.wav");
                    DefaultValues.Add("SoundLoop", "1");
                    DefaultValues.Add("ExplID", "");
                    DefaultValues.Add("Repeat", "5");
                }
                else
                {
                    DefaultValues.Add("ImageFile", "Dich_Dan.bmp");
                    DefaultValues.Add("Width", "0.4");
                    DefaultValues.Add("Height", "0.2");
                    DefaultValues.Add("Duration", "0.4");
                    DefaultValues.Add("Speed", "0.4");
                    DefaultValues.Add("SoundName", "gun_shotgun1.wav");
                    DefaultValues.Add("SoundLoop", "1");
                    DefaultValues.Add("ExplID", "");
                    DefaultValues.Add("Repeat", "5");
                }
                
            }
            else if (comboBoxActionFilter.SelectedIndex == 5)
            {
                //<Action ID="9" Type="FocusAt" Start="0" CenterX="0.6330777" CenterY="0.6922352" cameraPosX="0" cameraPosY="-29" cameraPosZ="-14.5" angleZ="-0.009999994" angleX="-0.3837993"></Action>
                //FocusAt
                //ShowAttributeList = new List<string> { "Type", "Duration" };
            }
            else if (comboBoxActionFilter.SelectedIndex == 6)
            {
                //<Action ID="6" Type="Fly" ObjName="Dich_MayBayNB_1" Start="0" Duration="20" Hide="true" SoundName="b17-01.wav" SoundLoop="1">
                //Fly
                //ShowAttributeList = new List<string> { "Type", "SoundName" };
            }
            else if (comboBoxActionFilter.SelectedIndex == 7)
            {
                //<Action ID="7" Type="Move" ObjName="Dich_XeTang_CayDua1" Start="0" Duration="20" Hide="true" SoundName="xetang.wav" SoundLoop="1">
                //Move
                //ShowAttributeList = new List<string> { "Type", "SoundName" };
            }
            else if (comboBoxActionFilter.SelectedIndex == 8)
            {
                //<Action ID="3" Type="CornerTitle" Start="0" Duration="0" CornerText="Giai đoạn 1 từ 13/09 đến 05/10/1972  &#xA;     TH1: Đánh địch cơ động triển khai tiến công"></Action>
                //CornerTitle
                ShowAttributeList = new List<string> { "Type", "CornerText" };
            }
            else if (comboBoxActionFilter.SelectedIndex == 9)
            {
                //<Action ID="1" Type="Explode" ObjName="Dich_MayBayNB_5" ImageFile="explosion1.bmp" Width="1" Height="1" ShiftZ="0" Start="0" Duration="1" Speed="60" SoundName="explosion.wav" SoundLoop="0">
                //ShowAttributeList = new List<string> { "Type", "ImageFile", "Width", "Height", "ShiftZ", "Duration", "Speed", "SoundName", "SoundLoop" };
                DefaultValues.Add("ImageFile", "explosion1.bmp");
                DefaultValues.Add("Width", "1");
                DefaultValues.Add("Height", "1");
                DefaultValues.Add("ShiftZ", "0");
                DefaultValues.Add("Duration", "5");
                DefaultValues.Add("Speed", "60");
                DefaultValues.Add("SoundName", "explosion.wav");
                DefaultValues.Add("SoundLoop", "1");
            }
            else if (comboBoxActionFilter.SelectedIndex == 10)
            {
                //<Action ID="Boom" Type="ExplodeDcl" ImageFile="explosion1.bmp" Width="2" Height="2" ShiftZ="1" Start="0" Speed="80" Duration="0.5" SoundName="explosion.wav" SoundLoop="0">
                //ExplodeDcl
                //ShowAttributeList = new List<string> { "Type", "ImageFile", "Width", "Height", "ShiftZ", "Duration", "Speed", "SoundName", "SoundLoop" };
                DefaultValues.Add("ImageFile", "explosion1.bmp");
                DefaultValues.Add("Width", "2");
                DefaultValues.Add("Height", "2");
                DefaultValues.Add("ShiftZ", "2");
                DefaultValues.Add("Duration", "0.5");
                DefaultValues.Add("Speed", "80");
                DefaultValues.Add("SoundName", "explosion.wav");
                DefaultValues.Add("SoundLoop", "0");
            }

            if (DefaultValues.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridViewAttributes.Rows)
                {
                    string key = row.Cells["Attribute"].Value.ToString();
                    if (DefaultValues.ContainsKey(key))
                        row.Cells["Value"].Value = DefaultValues[key];
                }
            }
        }

        private void btnOpenNotepad_Click(object sender, EventArgs e)
        {
            string[] result = treeViewScript.SelectedNode.Name.Split(new string[] { "[]" }, StringSplitOptions.None);
            string filePath = _objDiaHinh._myCurrentDirectory + "\\" + result[1];
            Process.Start("notepad++.exe", filePath);
        }
    }
}
