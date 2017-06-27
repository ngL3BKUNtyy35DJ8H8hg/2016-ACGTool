using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using BDTCLib;

namespace BDTCLib
{
    public class DiaHinh
    {
        public string _myCurrentDirectory;
        public string _myDefaFileName;
        public string _myKHCnnString;
        public string _myMapGst;
        public string _myMapNhoGst;

        public string _mySaiSo;
        public string _myTinhChinhGocQuay;

        public string _myLastSaban;
        
        public string _myD3DModelMeshFile;
        public string _myBillboardMeshFile;
        public string _mySpriteTexsFile;
        
        public string _myImagesPath;
        public string _mySoundsPath;
        
        public string _myMuiTenFile;
        public string _myExplodeFile;
        
        public string _myTextureFile;
        public string _myMap1X;
        public string _myMap1Y;
        public string _myMap2X;
        public string _myMap2Y;
        public string _myGridDataFile;
        public string _myGridDataType = "xyz";

        public string _myGRID_WIDTH;
        public string _myGRID_HEIGHT;
        public string _mySCALE_FACTOR;
        public string _myLightDir;

        private XmlDocument _objDiaHinhDoc;
        
        public MyDefaFileName _objMyDefaFileName;
        public MyKHCnnString _objMyKHCnnString;
        public MyMapGst _objMyMapGst;
        public MyMapNhoGst _objMyMapNhoGst;
        public MyLastSaban _objMyLastSaban;
        public MyD3DModelMeshFile _objMyD3DModelMeshFile;
        public MyBillboardMeshFile _objMyBillboardMeshFile;
        public MySpriteTexsFile _objMySpriteTexsFile;
        public MyImagesPath _objMyImagesPath;
        public MySoundsPath _objMySoundsPath;
        public MyMuiTenFile _objMyMuiTenFile;
        public MyExplodeFile _objMyExplodeFile;
        public MyTextureFile _objMyTextureFile;
        public MyGridDataFile _objMyGridDataFile;

        private string _filePath;
        public DiaHinh(string filePath)
        {
            _filePath = filePath;
            LoadDiaHinh();
            //LoadDiaHinh(_filePath);
        }

        /// <summary>
        /// Khởi tạo load File .diahinh
        /// </summary>
        private void LoadDiaHinh()
        {
            _objDiaHinhDoc = new XmlDocument();
            _objDiaHinhDoc.Load(_filePath);
            XmlNode node;
            node = _objDiaHinhDoc.DocumentElement;
            if (node.Attributes.Count == 0)
            {
                //MessageBox.Show("Không phải file cấu hình .diahinh. Chọn lại file cấu hình");
                return;
            }
            
            foreach (XmlAttribute att in node.Attributes)
            {
                switch (att.Name)
                {
                    case "myCurrentDirectory":
                        _myCurrentDirectory = att.Value;
                        //change current directory to use the Path.GetFullPath funtion exactly
                        try
                        {
                            Environment.CurrentDirectory = _myCurrentDirectory;
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                        
                        break;
                    case "myDefaFileName":
                        _myDefaFileName = att.Value;
                        _objMyDefaFileName = new MyDefaFileName(att.Name, att.Value);
                        break;
                    case "myKHCnnString":
                        _myKHCnnString = att.Value;
                        _objMyKHCnnString = new MyKHCnnString(att.Name,att.Value);
                        break;
                    case "myMapGst":
                        _myMapGst = att.Value;
                        _objMyMapGst = new MyMapGst(att.Name, att.Value);
                        break;
                    case "myMapNhoGst":
                        _myMapNhoGst = att.Value;
                        _objMyMapNhoGst = new MyMapNhoGst(att.Name, att.Value);
                        break;
                    case "mySaiSo":
                        _mySaiSo = att.Value;
                        break;
                    case "myTinhChinhGocQuay":
                        _myTinhChinhGocQuay = att.Value;
                        break;
                    case "myLastSaBan":
                        _myLastSaban = att.Value;
                        _objMyLastSaban = new MyLastSaban(att.Name, att.Value);
                        break;
                    case "myD3DModelMeshFile":
                        _myD3DModelMeshFile = att.Value;
                        _objMyD3DModelMeshFile = new MyD3DModelMeshFile(_objMyLastSaban, att.Name, att.Value);
                        break;
                    case "myBillboardMeshFile":
                        _myBillboardMeshFile = att.Value;
                        _objMyBillboardMeshFile = new MyBillboardMeshFile(att.Name, att.Value);
                        break;
                    case "mySpriteTexsFile":
                        _mySpriteTexsFile = att.Value;
                        _objMySpriteTexsFile = new MySpriteTexsFile(att.Name, att.Value);
                        break;
                    case "myImagesPath":
                        _myImagesPath = att.Value;
                        _objMyImagesPath = new MyImagesPath(att.Name, att.Value);
                        break;
                    case "mySoundsPath":
                        _mySoundsPath = att.Value;
                        _objMySoundsPath = new MySoundsPath(_objMyLastSaban, att.Name, att.Value);
                        break;
                    case "myMuiTenFile":
                        _myMuiTenFile = att.Value;
                        _objMyMuiTenFile = new MyMuiTenFile(att.Name, att.Value);
                        break;
                    case "myExplodeFile":
                        _myExplodeFile = att.Value;
                        _objMyExplodeFile = new MyExplodeFile(att.Name, att.Value);
                        break;
                    case "myTextureFile":
                        _myTextureFile = att.Value;
                        _objMyTextureFile = new MyTextureFile(att.Name, att.Value);
                        break;
                    case "myMap1X":
                        _myMap1X = att.Value;
                        break;
                    case "myMap1Y":
                        _myMap1Y = att.Value;
                        break;
                    case "myMap2X":
                        _myMap2X = att.Value;
                        break;
                    case "myMap2Y":
                        _myMap2Y = att.Value;
                        break;
                    case "myGridDataFile":
                        _myGridDataFile = att.Value;
                        _objMyGridDataFile = new MyGridDataFile(att.Name, att.Value);
                        break;
                    case "myGridDataType":
                        _myGridDataType = att.Value;
                        break;
                    case "myGRID_WIDTH":
                        _myGRID_WIDTH = att.Value;
                        break;
                    case "myGRID_HEIGHT":
                        _myGRID_HEIGHT = att.Value;
                        break;
                    case "mySCALE_FACTOR":
                        _mySCALE_FACTOR = att.Value;
                        break;
                    case "myLightDir":
                        _myLightDir = att.Value;
                        break;
                }
            }
        }


        /// <summary>
        /// Kiểm tra file .diahinh có hợp lệ không
        /// </summary>
        /// <returns></returns>
        public bool Validate_DiaHinhFile()
        {
            XmlNode node;
            node = _objDiaHinhDoc.DocumentElement;
            if (node.Attributes.Count == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Cập nhật chuỗi trong nội dung File địa hình
        /// Chú ý: Không dùng cập nhật bằng XML vì nó tự động reset lại format file .diahinh
        /// </summary>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void UpdateDiaHinhFileContent(string oldValue, string newValue)
        {
            //Cách 1:
            //string myCurrentDirectory = string.Format("myCurrentDirectory=\"{0}\"",editPath);
            StreamReader reader = new StreamReader(_filePath);
            string str = "";
            while (reader.Peek() >= 0)
            {
                str = reader.ReadToEnd();
                str = str.Replace(oldValue, newValue);
            }
            reader.Close();

            using (FileStream fs = new FileStream(_filePath, FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(str);
            }

            //Cách 2: Lưu dạng này sẽ bỏ qua các ký hiệu ENTER (\n)
            //XmlDocument myDiaHinhFileDocument = new XmlDocument();
            //myDiaHinhFileDocument.Load(_filePath);
            //XmlNode nodePara = myDiaHinhFileDocument.DocumentElement;
            //nodePara.Attributes["myCurrentDirectory"].Value = editPath;
            //myDiaHinhFileDocument.Save(_filePath);
        }

        public void BindSoundFiles_ListView(ListView listViewSound)
        {
            _objMySoundsPath.BindSoundFiles_ListView(listViewSound);
        }

        public void BindD3DSpriteTexs_ListView(ListView listViewD3DSpriteTexs)
        {
            _objMySpriteTexsFile.BindD3DSpriteTexs_ListView(listViewD3DSpriteTexs);
        }
        public void BindD3DModelMesh_ListView(ListView listViewD3DModelMesh)
        {
            _objMyD3DModelMeshFile.BindD3DModelMesh_ListView(listViewD3DModelMesh);
        }

        public void BindGstMap_ListView(ListView listViewGstMap)
        {
            _objMyMapGst.BindGstMap_ListView(listViewGstMap);
        }

        public void BindDataGrid_ListView(ListView listViewXYZ)
        {

            //Load vào listview
            _objMyGridDataFile.BindDataGrid_ListView(listViewXYZ);

            //Kiểm tra giá trị khi đọc file .xyz so với file .diahinh
            double myMap1X = double.Parse(_myMap1X);
            double myMap1Y = double.Parse(_myMap1Y);
            double myMap2X = double.Parse(_myMap2X);
            double myMap2Y = double.Parse(_myMap2Y);

            double myGRID_WIDTH = double.Parse(_myGRID_WIDTH);
            double myGRID_HEIGHT = double.Parse(_myGRID_HEIGHT);
            double xyzGRID_WIDTH = double.Parse(listViewXYZ.Items["myGRID_WIDTH"].SubItems[1].Text);
            double xyzGRID_HEIGHT = double.Parse(listViewXYZ.Items["myGRID_HEIGHT"].SubItems[1].Text);
            
            //Kiểm tra width, height
            if (myGRID_WIDTH != xyzGRID_WIDTH - 1 && myGRID_WIDTH != xyzGRID_WIDTH)
            {
                listViewXYZ.Items["myGRID_WIDTH"].SubItems[2].Text = "xem lại myGRID_WIDTH của file .diahinh";
                listViewXYZ.Items["myGRID_WIDTH"].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                listViewXYZ.Items["myGRID_WIDTH"].SubItems[2].Text = "OK";
            }

            if (myGRID_HEIGHT != xyzGRID_HEIGHT - 1 && myGRID_HEIGHT != xyzGRID_HEIGHT)
            {
                listViewXYZ.Items["myGRID_HEIGHT"].SubItems[2].Text = "xem lại myGRID_HEIGHT của file .diahinh";
                listViewXYZ.Items["myGRID_HEIGHT"].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                listViewXYZ.Items["myGRID_HEIGHT"].SubItems[2].Text = "OK";
            }

            //Khung tọa độ .xyz phải bao khung bản đồ (để vẽ được độ cao 3D)
            if (myMap1X > double.Parse(listViewXYZ.Items["RightLon"].SubItems[1].Text))
            {
                listViewXYZ.Items["RightLon"].SubItems[2].Text = string.Format("Right Lon của file .xyz phải >= {0}", _myMap1X);
                listViewXYZ.Items["RightLon"].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                listViewXYZ.Items["RightLon"].SubItems[2].Text = string.Format("OK >= {0}", _myMap1X);
            }

            if (myMap1Y > double.Parse(listViewXYZ.Items["TopLat"].SubItems[1].Text))
            {
                listViewXYZ.Items["TopLat"].SubItems[2].Text = string.Format("Top Lat của file .xyz phải >= {0}", _myMap1Y);
                listViewXYZ.Items["TopLat"].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                listViewXYZ.Items["TopLat"].SubItems[2].Text = string.Format("OK >= {0}", _myMap1Y);
            }

            if (myMap2X < double.Parse(listViewXYZ.Items["LeftLon"].SubItems[1].Text))
            {
                listViewXYZ.Items["LeftLon"].SubItems[2].Text = string.Format("Left Lon của file .xyz phải <= {0}", _myMap2X);
                listViewXYZ.Items["LeftLon"].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                listViewXYZ.Items["LeftLon"].SubItems[2].Text = string.Format("OK <= {0}", _myMap2X);
            }

            if (myMap2Y < double.Parse(listViewXYZ.Items["BottomLat"].SubItems[1].Text))
            {
                listViewXYZ.Items["BottomLat"].SubItems[2].Text = string.Format("Bottom Lat của file .xyz phải <= {0}", _myMap2Y);
                listViewXYZ.Items["BottomLat"].ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                listViewXYZ.Items["BottomLat"].SubItems[2].Text = string.Format("OK <= {0} ", _myMap2Y);
            }
        }

    }
}
