using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace BDTCLib
{
    public class PointInfo
    {
        public string Lon;
        public string Lat;
        public int Height;
        public int Width;
        public string Label;
    }

    public class MyMapGst : BaseMyFile
    {
        //"\TABLE\1\FILE" = "TrangLon.tab"
        //"\TABLE\1\FILE" = "C:\\TrangLon.tab"
        public TabFileInfo _objTabFileInfo;

        public MyMapGst(string key, string value) : base(key,value)
        {
            
        }

        //private string _tabFilePath = ""; //Lưu tên file .tab

        /// <summary>
        /// Đọc các file .tab trong file .gst
        /// </summary>
        /// <returns></returns>
        private List<string> GetTabFile()
        {
            List<string> tabFileList = new List<string>();
            StreamReader reader = new StreamReader(FULLPATH_FILE);
            string content = "";
            while (reader.Peek() >= 0)
            {
                content = reader.ReadLine();
                if (content.Contains("\\FILE\" = \""))
                {
                    string[] items = content.Split(new char[] {'='});
                    tabFileList.Add(items[1].Replace("\"", ""));
                }
            }
            reader.Close();
            return tabFileList;
        }

        /// <summary>
        /// Kiểm tra đường dẫn file .tab trong file .gst map
        /// chỉ là lưu tên file (ví dụ: TrangLon.tab) chứ không phải full path (ví dụ: C:\\Map\\TrangLon.tab)
        /// </summary>
        /// <returns></returns>
        private bool ValidateFileTab(string tabFilePath)
        {
            string[] items = tabFilePath.Split(new char[] { '\\' });
            if (items.Length == 1)
                return true;
            return false;
        }

        public void BindGstMap_ListView(ListView listViewGstMap)
        {
            listViewGstMap.Items.Clear();
            //Kiểm tra tồn tại file .gst map
            ListViewItem item = new ListViewItem("File Map");
            item.SubItems.Add(PATH_FILE);
            if (!File.Exists(FULLPATH_FILE))
            {
                item.SubItems.Add("Không tồn tại");
                item.ForeColor = Color.Red;
                listViewGstMap.Items.Add(item);
                return;
            }
            listViewGstMap.Items.Add(item);

            //=========================================================
            //Đọc file map .gst
            List<string> tabFileList = GetTabFile();

            //Nếu tồn tại hơn 1 file .tab
            if (tabFileList.Count > 1)
            {
                item = new ListViewItem("Tab File");
                item.SubItems.Add(VALUE_NAME);
                item.SubItems.Add(string.Format("File {0} đang cấu hình hơn 2 file .tab", VALUE_NAME));
                item.ForeColor = Color.Red;
                listViewGstMap.Items.Add(item);

                foreach (var tabFile in tabFileList)
                {
                    item = new ListViewItem("Tab File");
                    item.SubItems.Add(tabFile);
                    item.ForeColor = Color.Red;
                    listViewGstMap.Items.Add(item);
                }

                return;
            }

            string tabFilePath = tabFileList[0];
            //=========================================================
            //Kiểm tra đường dẫn file .tab hợp lệ
            item = new ListViewItem("Tab File");
            item.SubItems.Add(tabFilePath);
            if (!ValidateFileTab(tabFilePath))
            {
                item.SubItems.Add("Đường dẫn này phải là tên của file .tab");
                item.ForeColor = Color.Red;
                listViewGstMap.Items.Add(item);
                return;
            }
            listViewGstMap.Items.Add(item);

            tabFilePath = Path.GetDirectoryName(FULLPATH_FILE) + "\\" + tabFilePath.Trim();
            //Kiểm tra file .tab có tồn tại không
            if (!File.Exists(tabFilePath))
            {
                item.SubItems.Add("File .tab không tồn tại");
                item.ForeColor = Color.Red;
                listViewGstMap.Items.Add(item);
                return;
            }

            _objTabFileInfo = new TabFileInfo(tabFilePath);
            _objTabFileInfo.BindTabInfo_ListView(listViewGstMap);
        }
    }

    public class TabFileInfo
    {
        //Đường dẫn file ảnh
        public string ImagePath;

        //Thông tin của file ảnh
        public Bitmap ImageInfo;
        public Dictionary<string, PointInfo> PointInfoDict;

        public TabFileInfo(string tabFilePath)
        {
            try
            {
                PointInfoDict = new Dictionary<string, PointInfo>();

                //Kiểm tra file .tab có tồn tại không
                if (!File.Exists(tabFilePath))
                    return;

                //====================================================
                //Đọc nội dung file .tab
                StreamReader reader = new StreamReader(tabFilePath);
                string fileContent = "";
                while (reader.Peek() >= 0)
                {
                    //str = reader.ReadLine();
                    fileContent = reader.ReadToEnd();
                    
                }
                reader.Close();

                //====================================================
                //Đọc tên File ảnh trong file .tab
                Regex r1 = new Regex(@"File ""(?<File>.+\.[A-Za-z0-9]+)""");
                //Đọc thông tin file .tab
                MatchCollection mc1 = r1.Matches(fileContent);
                foreach (Match m in mc1)
                {
                    ImagePath = Path.GetDirectoryName(tabFilePath) + "\\" + m.Groups["File"].Value;
                    if (File.Exists(ImagePath))
                    {
                        ImageInfo = new Bitmap(ImagePath);    
                    }
                }

                //====================================================
                //Đọc các thông tin vị trí tọa độ đăng ký trong file .tab
                Regex r2 = new Regex(@"\((?<Lon>\d+\.\d+), (?<Lat>\d+\.\d+)\) \((?<Width>\d+), (?<Height>\d+)\) Label ""(?<Label>.+)""");
                MatchCollection mc2 = r2.Matches(fileContent);
                foreach (Match m in mc2)
                {
                    PointInfo obj = new PointInfo();
                    obj.Lon = m.Groups["Lon"].Value;
                    obj.Lat = m.Groups["Lat"].Value;
                    obj.Height = int.Parse(m.Groups["Height"].Value);
                    obj.Width = int.Parse(m.Groups["Width"].Value);
                    obj.Label = m.Groups["Label"].Value;
                    PointInfoDict.Add(obj.Label, obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        /// <summary>
        /// Kiểm tra file tab có hợp lệ không
        /// </summary>
        /// <returns></returns>
        public void BindTabInfo_ListView(ListView listViewGstMap)
        {
            //==============================================================
            //Load ImageFile
            ListViewItem item = new ListViewItem("Image File");
            item.SubItems.Add(ImagePath);
            //Kiểm tra file ảnh trong file .tab có tồn tại không
            if (!File.Exists(ImagePath))
            {
                item.SubItems.Add("Không tồn tại");
                item.ForeColor = Color.Red;
            }
            else
            {
                item.SubItems.Add("OK");
            }
            listViewGstMap.Items.Add(item);

            //==============================================================
            //Load các tọa độ đăng ký vào
            //Left Longitude
            item = new ListViewItem("Left Longitude");
            item.SubItems.Add(PointInfoDict["NW"].Lon.ToString());
            //==============================================================
            //Kiểm tra thông tin đăng ký tọa độ đúng chưa
            if (PointInfoDict["NW"].Lon != PointInfoDict["SW"].Lon
                || PointInfoDict["NE"].Lon != PointInfoDict["SE"].Lon
                || PointInfoDict["NW"].Lat != PointInfoDict["NE"].Lat
                || PointInfoDict["SW"].Lat != PointInfoDict["SE"].Lat)
            {
                item.SubItems.Add("Tọa độ đăng ký trong .tab không đúng");
                item.ForeColor = Color.Red;
            }
            else if (float.Parse(PointInfoDict["NW"].Lon) > float.Parse(PointInfoDict["NE"].Lon)) //Kiểm tra logic tọa độ
            {
                item.SubItems.Add("Lon của NW phải < Lon của NE");
                item.ForeColor = Color.Red;
            }
            
            listViewGstMap.Items.Add(item);

            //Right Longitude
            item = new ListViewItem("Right Longitude");
            item.SubItems.Add(PointInfoDict["NE"].Lon.ToString());
            listViewGstMap.Items.Add(item);

            //Top Latitude
            item = new ListViewItem("Top Latitude");
            item.SubItems.Add(PointInfoDict["NW"].Lat.ToString());
            //Kiểm tra logic tọa độ
            if (float.Parse(PointInfoDict["NW"].Lat) < float.Parse(PointInfoDict["SW"].Lat)) 
            {
                item.SubItems.Add("Lat của NW phải > Lat của SW");
                item.ForeColor = Color.Red;
            }
            listViewGstMap.Items.Add(item);

            //Bottom Latitude
            item = new ListViewItem("Bottom Latitude");
            item.SubItems.Add(PointInfoDict["SW"].Lat.ToString());
            listViewGstMap.Items.Add(item);


            //Nếu file ảnh không tồn tại thì return
            if (ImageInfo == null)
                return;

            //==============================================================
            //Height của file ảnh
            item = new ListViewItem("Height");
            item.SubItems.Add(ImageInfo.Height.ToString());
            //Kiểm tra kích thước file ảnh đúng với trong file .tab không
            if (PointInfoDict["SE"].Height != ImageInfo.Height)
            {
                item.SubItems.Add("Kích thước Height trong .tab khác với kích thước Height file ảnh");
                item.ForeColor = Color.Red;
            }
            listViewGstMap.Items.Add(item);

            //==============================================================
            //Height trong file .tab
            item = new ListViewItem("Height in .Tab File");
            item.SubItems.Add(PointInfoDict["SE"].Height.ToString());
            if (PointInfoDict["SE"].Height != ImageInfo.Height)
            {
                item.ForeColor = Color.Red;
            }
            listViewGstMap.Items.Add(item);

            //==============================================================
            //Width của file ảnh
            item = new ListViewItem("Width");
            item.SubItems.Add(ImageInfo.Width.ToString());
            //Kiểm tra kích thước file ảnh đúng với trong file .tab không
            if (PointInfoDict["SE"].Width != ImageInfo.Width)
            {
                item.SubItems.Add("Kích thước Width trong .tab khác với kích thước Width file ảnh");
                item.ForeColor = Color.Red;
            }
            listViewGstMap.Items.Add(item);

            //==============================================================
            //Width trong file .tab
            item = new ListViewItem("Width in .Tab File");
            item.SubItems.Add(PointInfoDict["SE"].Width.ToString());
            if (PointInfoDict["SE"].Width != ImageInfo.Width)
            {
                item.ForeColor = Color.Red;
            }
            listViewGstMap.Items.Add(item);
        }
    }

   
}
