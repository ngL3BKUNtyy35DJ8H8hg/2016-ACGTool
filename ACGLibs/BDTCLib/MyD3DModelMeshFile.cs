using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace BDTCLib
{
    
    public class MyD3DModelMeshFile : BaseMyFile
    {
        private string _D3DModelXmlFile;
        private List<string> _D3DModelUsedFiles = new List<string>();
        public MyD3DModelMeshFile(MyLastSaban objMyLastSaBan, string key, string value) : base(key,value)
        {
            //Lấy đường dẫn file D3DModels.xml, file này chứa các file 3D đang dùng trong kịch bản
            _D3DModelXmlFile = objMyLastSaBan._mySaBanDirFullPath + "\\D3DModels.xml";
        }
        
        /// <summary>
        /// Hàm kiểm tra file D3DModelMesh.xml
        /// </summary>
        public void BindD3DModelMesh_ListView(ListView listViewD3DModelMesh)
        {
            if (!File.Exists(FULLPATH_FILE))
                return;

            //Kiểm tra tồn tại file D3DModels.xml trong thư mục kịch bản
            //Thường là phải build xong mới có file này
            if (!File.Exists(_D3DModelXmlFile))
                return;

            listViewD3DModelMesh.Items.Clear();

            XmlDocument D3DModelMeshFileDocument = new XmlDocument();
            //Load file D3DModelMesh.xml
            D3DModelMeshFileDocument.Load(FULLPATH_FILE);
            
            //Load file D3DModels.xml trong thư mục kịch bản
            XmlDocument D3DModelsDocument = new XmlDocument();
            D3DModelsDocument.Load(_D3DModelXmlFile);
            XmlNode nodeModels = D3DModelsDocument.ChildNodes[0];
            foreach (XmlNode node in D3DModelMeshFileDocument.ChildNodes)
            {
                if (node.Name == "Flags")
                {
                    foreach (XmlNode node1 in node.ChildNodes)
                    {
                        //Bỏ qua comment của xml
                        if (node1.Name == "#comment")
                            continue;


                        string warning = "Không tồn tại";
                        try
                        {
                            //Lấy đường dẫn full các file 3D suy ra từ nơi chứa file .exe
                            _D3DModelXmlFile = Path.GetFullPath(node1.Attributes["XFile"].Value);
                        }
                        catch
                        {
                            warning = string.Format("Xem lại cấu hình đường dẫn của ký hiệu {0}: {1}",
                                          node1.Attributes["Name"].Value,
                                          node1.Attributes["XFile"].Value);
                        }

                        //Add into listview
                        ListViewItem item = new ListViewItem(node1.Attributes["Name"].Value);
                       
                        //Notes: Kiểm tra path của các file trong .xml
                        if (File.Exists(_D3DModelXmlFile)) //Nếu file ảnh tồn tại
                        {
                            //Size
                            FileInfo f = new FileInfo(_D3DModelXmlFile);
                            if (f.Length == 0)
                            {
                                //Size
                                item.SubItems.Add(f.Length.ToString("0"));
                                item.ForeColor = Color.Red;
                                warning = "Lỗi size = 0";
                            }
                            else
                            {
                                //Size
                                item.SubItems.Add(f.Length.ToString("#,###"));
                                warning = "OK";
                            }
                            
                            //XValue
                            item.SubItems.Add(node1.Attributes["XFile"].Value);
                            //Fullpath
                            item.SubItems.Add(_D3DModelXmlFile);
                            item.SubItems.Add(warning);
                        }
                        else
                        {
                            //Size
                            item.SubItems.Add("");
                            //XValue
                            item.SubItems.Add(node1.Attributes["XFile"].Value);
                            //Fullpath
                            item.SubItems.Add(_D3DModelXmlFile);
                            item.ForeColor = Color.Red;
                            item.SubItems.Add(warning);
                        }

                        //Kiểm tra ký hiệu có dùng trong kịch bản không (yêu cầu là đã rebuild rồi)
                        if (nodeModels.InnerXml.Contains(node1.Attributes["Name"].Value))
                        {
                            item.SubItems.Add("Đang dùng");
                            //List file đang dùng
                            if (!_D3DModelUsedFiles.Contains(node1.Attributes["XFile"].Value))
                                _D3DModelUsedFiles.Add(_D3DModelXmlFile);

                        }
                        else
                        {
                            item.SubItems.Add("Không dùng");
                            //Nếu không phải báo lỗi thì tô màu khác
                            if (item.ForeColor != Color.Red)
                                item.ForeColor = Color.Orange;

                        }

                        item = listViewD3DModelMesh.Items.Add(item);
                    }
                }
            }
        }
    }
}
