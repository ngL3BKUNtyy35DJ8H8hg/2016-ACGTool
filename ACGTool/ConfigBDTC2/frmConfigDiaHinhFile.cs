using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using BDTCLib;

namespace ConfigBDTC
{
    public partial class frmConfigDiaHinhFile : Form
    {
        private DiaHinh _objDiaHinh;

        public frmConfigDiaHinhFile()
        {
            InitializeComponent();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            //mo form cho nguoi ta chon co so du lieu
            OpenFileDialog1.Filter = "DiaHinh File(*.diahinh)|*.diahinh";
            if (Properties.Settings.Default.RecentDiaHinhFile != "")
                OpenFileDialog1.InitialDirectory = Path.GetDirectoryName(Properties.Settings.Default.RecentDiaHinhFile);
            
            txtFilePath.Text = "";
            OpenFileDialog1.ShowDialog();
            txtFilePath.Text = OpenFileDialog1.FileName;
            Properties.Settings.Default.RecentDiaHinhFile = OpenFileDialog1.FileName;
            Properties.Settings.Default.Save();

            //btnCheckDiaHinhFile_Click(null, null);
            btnCheckDiaHinhFile.PerformClick();
        }

        private void frmConfigDiaHinhFile_Load(object sender, EventArgs e)
        {
            txtAccessFile.Text = Properties.Settings.Default.RecentAccessFile;
            txtFilePath.Text = Properties.Settings.Default.RecentDiaHinhFile;
        }

        private void btnCheckDiaHinhFile_Click(object sender, EventArgs e)
        {
            if (txtFilePath.Text == string.Empty)
            {
                MessageBox.Show("Chưa chọn file .diahinh");
                return;
            }

            if(!File.Exists(txtFilePath.Text))
            {
                MessageBox.Show("Đường dẫn không tồn tại");
                return;
            }
            _objDiaHinh = new DiaHinh(txtFilePath.Text);

            listViewDiaHinh.Items.Clear();
            listViewD3DSpriteTexs.Items.Clear();
            listViewLastConfig.Items.Clear();
            XmlDocument myDiaHinhFileDocument = new XmlDocument();
            myDiaHinhFileDocument.Load(txtFilePath.Text);    

            XmlNode node;
            node = myDiaHinhFileDocument.DocumentElement;
            listViewDiaHinh.Columns[0].Width = 100;
            listViewDiaHinh.Columns[1].Width = 400;
            listViewDiaHinh.Columns[2].Width = 300;
            listViewDiaHinh.Columns[3].Width = 250;
            foreach (XmlAttribute att in node.Attributes)
            {
                ListViewItem item = listViewDiaHinh.Items.Add(att.Name, att.Name, -1);
                item.SubItems.Add(att.Value);
                item.SubItems.Add("");
                item.SubItems.Add("");
            }

            if (listViewDiaHinh.Items.Count == 0)
            {
                MessageBox.Show("Không phải file cấu hình .diahinh. Chọn lại file cấu hình");
                return;
            }

            
            //myCurrentDirectory
            string key = "myCurrentDirectory";
            string path = listViewDiaHinh.Items[key].SubItems[1].Text;
            if (!txtFilePath.Text.Contains(path))
            {
                listViewDiaHinh.Items[key].SubItems[3].Text = string.Format("Sai đường dẫn so với \"{1}\". Nhấn nút \"Edit myCurrentDirectory\" để sửa.", key,
                                                                      txtFilePath.Text);
                listViewDiaHinh.Items[key].ForeColor = Color.Red;
                btnRepairMyCurrentDirectory.Enabled = true;
                return;
            }
            else
            {
                btnRepairMyCurrentDirectory.Enabled = false;
                listViewDiaHinh.Items[key].ForeColor = Color.Black;
            }

            //change current directory to use the Path.GetFullPath funtion exactly
            //Environment.CurrentDirectory = path;

            //myDefaFileName = "..\Defas\Defas.def"
            key = "myDefaFileName";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);

            //myKHCnnString="Provider=Microsoft.Jet.OLEDB.4.0;Data Source=E:\SaBanXYZ\Data\CacKyHieu.mdb"
            key = "myKHCnnString";
            //CheckExistLink(key, true);
            string value = listViewDiaHinh.Items[key].SubItems[1].Text.Replace("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", "");
            listViewDiaHinh.Items[key].SubItems[2].Text = Path.GetFullPath(value);
            if (!File.Exists(listViewDiaHinh.Items[key].SubItems[2].Text))
            {
                listViewDiaHinh.Items[key].SubItems[3].Text = string.Format("Sai đường dẫn \"{0}\"", listViewDiaHinh.Items[key].SubItems[2].Text);
                listViewDiaHinh.Items[key].ForeColor = Color.Red;
            }
            else
            {
                listViewDiaHinh.Items[key].SubItems[3].Text = "OK";
                listViewDiaHinh.Items[key].ForeColor = Color.Black;
            }
                
            //myMapGst="BanDo\BanDo.gst"                                 
            key = "myMapGst";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);
            //Đọc file .gst để lấy thông tin đăng ký tọa độ file image trong file .tab


            //myMapNhoGst="BanDo\BanDoNho.gst"
            key = "myMapNhoGst";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);

            //myLastSaBan="bCOI_COIDKZ_XYZ.last"
            key = "myLastSaBan";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);
            //CheckLastFile();
                

            //myD3DModelMeshFile="MeshDef\D3DModelMesh.xml"			
            key = "myD3DModelMeshFile";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);
            

            //myBillboardMeshFile="MeshDef\D3DBillboardMesh.xml"		
            key = "myBillboardMeshFile";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);

            //mySpriteTexsFile="MeshDef\D3DSpriteTexs.xml"			
            key = "mySpriteTexsFile";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);
                

            //myImagesPath="images" 
            key = "myImagesPath";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);

            //mySoundsPath="..\sounds"
            key = "mySoundsPath";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);
            
            //myMuiTenFile="..\Defas\MuiTen.txt" 
            key = "myMuiTenFile";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);

            //myExplodeFile="..\Defas\Explode.txt" 
            key = "myExplodeFile";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);

            //myTextureFile="BanDo\Background.jpg"        
            key = "myTextureFile";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);

            //myGridDataFile="BanDo\GridData.txt"           
            key = "myGridDataFile";
            BDTCHelper.CheckExistLink(listViewDiaHinh.Items[key]);

            try
            {
                _objDiaHinh._objMyLastSaban.BindLastFile_ListView(listViewLastConfig);

                _objDiaHinh.BindD3DModelMesh_ListView(listViewD3DModelMesh);

                _objDiaHinh.BindD3DSpriteTexs_ListView(listViewD3DSpriteTexs);

                _objDiaHinh.BindSoundFiles_ListView(listViewSound);

                _objDiaHinh.BindGstMap_ListView(listViewGstMap);
                
                _objDiaHinh.BindDataGrid_ListView(listViewXYZ);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                //MessageBox.Show(string.Format("Xem lại file .diahinh {0}", txtFilePath.Text));

                //return;
            }
        }

        private void btnRepairMyCurrentDirectory_Click(object sender, EventArgs e)
        {
            string newCurrentDirectory = Path.GetDirectoryName(txtFilePath.Text);
            _objDiaHinh.UpdateDiaHinhFileContent(_objDiaHinh._myCurrentDirectory, newCurrentDirectory);
            //Kiểm tra lại đường dẫn sau khi sửa
            btnCheckDiaHinhFile.PerformClick();
        }

        private void btnRemoveD3DModelMesh_Click(object sender, EventArgs e)
        {
            //Cách 2: Lưu dạng này sẽ bỏ qua các ký hiệu ENTER (\n)
            string keyMyD3DModelMeshFile = "myD3DModelMeshFile";
            string filePath = listViewDiaHinh.Items[keyMyD3DModelMeshFile].SubItems[2].Text;
            XmlDocument imagesDocument = new XmlDocument();
            //Load file mySpriteTexs trong thuộc tính mySpriteTexsFile
            imagesDocument.Load(filePath);
            List<XmlNode> deletedNodes = new List<XmlNode>();
            foreach (XmlNode node in imagesDocument.ChildNodes)
            {
                if (node.Name == "Flags")
                {
                    foreach (XmlNode node1 in node.ChildNodes)
                    {
                        //Bỏ qua comment của xml
                        if (node1.Name == "#comment")
                            continue;

                        try
                        {
                            //Lấy đường dẫn full các file 3D suy ra từ nơi chứa file .exe
                            string fullPath = Path.GetFullPath(node1.Attributes["XFile"].Value);

                            //Nếu file đó không tồn tại thì remove
                            if (!File.Exists(fullPath))
                            {
                                deletedNodes.Add(node1);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                }
            }

            XmlNode nodeFlags = imagesDocument.ChildNodes[0];
            foreach (XmlNode delNode in deletedNodes)
            {
                nodeFlags.RemoveChild(delNode);
            }
            imagesDocument.Save(filePath);
        }

        private void btnRebuild_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteSoundFile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn xóa những file âm thanh không dùng?", "Cảnh báo", MessageBoxButtons.YesNo) == 
                System.Windows.Forms.DialogResult.Yes)
            {
                foreach (ListViewItem item in listViewSound.Items)
                {
                    if (item.SubItems[2].Text == "Không dùng")
                    {
                        File.Delete(item.SubItems[1].Text);
                    }
                }
            }
            _objDiaHinh.BindSoundFiles_ListView(listViewSound);
            //CheckSoundFiles();
        }

        private void btnRemoveNotUsed3DFile_Click(object sender, EventArgs e)
        {
            bool isDeleteXFile = false;
            if (checkBoxDeleteXFile.Checked)
            {
                if (MessageBox.Show("Bạn có chắc xóa luôn cả file .X?", "Cảnh báo", MessageBoxButtons.YesNo) ==
                    System.Windows.Forms.DialogResult.Yes)
                    isDeleteXFile = true;
            }

            if (!isDeleteXFile)
                return;

            //Cách 2: Lưu dạng này sẽ bỏ qua các ký hiệu ENTER (\n)
            string keyMyD3DModelMeshFile = "myD3DModelMeshFile";
            string filePath = listViewDiaHinh.Items[keyMyD3DModelMeshFile].SubItems[2].Text;
            XmlDocument imagesDocument = new XmlDocument();
            //Load file mySpriteTexs trong thuộc tính mySpriteTexsFile
            imagesDocument.Load(filePath);
            List<XmlNode> deletedNodes = new List<XmlNode>();

            //Lấy danh sách các cấu hình file dùng và các file không dùng
            Dictionary<string, ListViewItem> usedFiles = new Dictionary<string, ListViewItem>();
            Dictionary<string, ListViewItem> notUsedFiles = new Dictionary<string, ListViewItem>();
            foreach (ListViewItem item in listViewD3DModelMesh.Items)
            {
                if (item.SubItems[2].Text == "Không dùng")
                    notUsedFiles.Add(item.SubItems[0].Text, item);
                else
                    usedFiles.Add(item.SubItems[0].Text, item);
            }

            foreach (XmlNode node in imagesDocument.ChildNodes)
            {
                if (node.Name == "Flags")
                {
                    foreach (XmlNode node1 in node.ChildNodes)
                    {
                        //Bỏ qua comment của xml
                        if (node1.Name == "#comment")
                            continue;

                        try
                        {
                            
                            //Lấy đường dẫn full các file 3D suy ra từ nơi chứa file .exe
                            string fullPath = Path.GetFullPath(node1.Attributes["XFile"].Value);
                            if (notUsedFiles.ContainsKey(node1.Attributes["Name"].Value))
                            {
                                ListViewItem item = notUsedFiles[node1.Attributes["Name"].Value];

                            }
                             
                            //Nếu file đó không tồn tại thì remove
                            if (!File.Exists(fullPath))
                            {
                                deletedNodes.Add(node1);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                }
            }

            XmlNode nodeFlags = imagesDocument.ChildNodes[0];
            foreach (XmlNode delNode in deletedNodes)
            {
                nodeFlags.RemoveChild(delNode);
            }
            imagesDocument.Save(filePath);
        }

        
        private void btnRepairMyKHCnnString_Click(object sender, EventArgs e)
        {
            if (txtAccessFile.Text != "")
            {
                //string absFile = fileDialog.FileName;
                //string curFolder = listViewDiaHinh.Items["myCurrentDirectory"].SubItems[1].Text;
                //string relFile = BDTCHelper.ConvertAbsoluteToRelativePath(absFile, curFolder);

                string oldValue = listViewDiaHinh.Items["myKHCnnString"].SubItems[1].Text;
                string newValue = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + txtAccessFile.Text;

                _objDiaHinh.UpdateDiaHinhFileContent(oldValue, newValue);
                btnCheckDiaHinhFile.PerformClick();
            }
        }

        private void btnRepairMyMapNhoGst_Click(object sender, EventArgs e)
        {
            _objDiaHinh.UpdateDiaHinhFileContent(listViewDiaHinh.Items["myMapNhoGst"].SubItems[1].Text,
                                       listViewDiaHinh.Items["myMapGst"].SubItems[1].Text);
            //Kiểm tra lại đường dẫn sau khi sửa
            btnCheckDiaHinhFile.PerformClick();
        }

        private void btnEditMapCoord_Click(object sender, EventArgs e)
        {

        }

        private void btnBrowseAccessFile_Click(object sender, EventArgs e)
        {
            //mo form cho nguoi ta chon co so du lieu
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (Properties.Settings.Default.RecentAccessFile != "")
                fileDialog.InitialDirectory = Path.GetDirectoryName(Properties.Settings.Default.RecentAccessFile);
            fileDialog.Filter = "Access File(*.mdb)|*.mdb";
            fileDialog.ShowDialog();
            if (fileDialog.FileName != "")
            {
                txtAccessFile.Text = fileDialog.FileName;
                Properties.Settings.Default.RecentAccessFile = txtAccessFile.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void btnCheckScripts_Click(object sender, EventArgs e)
        {

        }
    }
}