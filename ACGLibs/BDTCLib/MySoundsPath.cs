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
    public class MySoundsPath : BaseMyFile
    {
        public string _D3DSoundXmlFile;
        public MySoundsPath(MyLastSaban objMyLastSaBan, string key, string value)
            : base(key, value)
        {
            _D3DSoundXmlFile = objMyLastSaBan._mySaBanDirFullPath + "\\D3DSounds.xml";
        }

        /// <summary>
        /// Kiểm tra các file âm thanh có dùng trong kịch bản không
        /// </summary>
        public void BindSoundFiles_ListView(ListView listViewSound)
        {
            //Kiểm tra xem thư mục sound có tồn tại không
            if (!Directory.Exists(FULLPATH_FILE))
                return;

            //Kiểm tra file xml chứa thông tin file sound đang dùng trong kịch bản
            if (!File.Exists(_D3DSoundXmlFile))
                return;

            listViewSound.Items.Clear();

            DirectoryInfo di = new DirectoryInfo(FULLPATH_FILE);
            FileInfo[] fileInfoArray = di.GetFiles("*.*");

            //Load file D3DModels.xml trong thư mục kịch bản
            XmlDocument d3DsoundDocument = new XmlDocument();
            d3DsoundDocument.Load(_D3DSoundXmlFile);
            XmlNode nodeModels = d3DsoundDocument.ChildNodes[0];
            string warning = "";
            foreach (FileInfo fileInfo in fileInfoArray)
            {
                //Add into listview
                ListViewItem item = new ListViewItem(fileInfo.Name);
                string fullPath = fileInfo.DirectoryName + "\\" + fileInfo.Name;
                if (File.Exists(fullPath)) //Nếu file ảnh tồn tại
                {
                    //Size
                    FileInfo f = new FileInfo(fullPath);
                    if (f.Length == 0)
                    {
                        //Size
                        item.SubItems.Add(f.Length.ToString("0"));
                        item.ForeColor = Color.Blue;
                        warning = "Lỗi size = 0";
                        //Full path
                        item.SubItems.Add(fullPath);
                    }
                    else
                    {
                        //Size
                        item.SubItems.Add(f.Length.ToString("#,###"));
                        //Full path
                        item.SubItems.Add(fullPath);
                        //Kiểm tra ký hiệu có dùng trong kịch bản không (yêu cầu là đã rebuild rồi)
                        if (nodeModels.InnerXml.Contains(fileInfo.Name))
                        {
                            warning = "Đang dùng";
                        }
                        else
                        {
                            warning = "Không dùng";
                            //Nếu không phải báo lỗi thì tô màu khác
                            if (item.ForeColor != Color.Red)
                                item.ForeColor = Color.Orange;
                        }
                    }
                    
                    item.SubItems.Add(warning);
                }
                else
                {
                    //Size
                    item.SubItems.Add("");
                    //Full path
                    item.SubItems.Add(fullPath);

                    //Kiểm tra ký hiệu có dùng trong kịch bản không (yêu cầu là đã rebuild rồi)
                    if (nodeModels.InnerXml.Contains(fileInfo.Name))
                    {
                        warning = "Đang dùng";
                    }
                    else
                    {
                        warning = "Không dùng";
                        //Nếu không phải báo lỗi thì tô màu khác
                        if (item.ForeColor != Color.Red)
                            item.ForeColor = Color.Orange;
                    }

                    item.SubItems.Add(warning);
                }

                listViewSound.Items.Add(item);
            }
        }
    }
}
