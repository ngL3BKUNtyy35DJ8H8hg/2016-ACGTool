using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace BDTCLib
{
    public class MyLastSaban : BaseMyFile
    {
        public XmlNode _nodeLAST;
        public string _mySaBanDir;
        public string _mySaBanDirFullPath;
        public string _LastBdTC;
        public string _LastBdTCFullPath;
        
        public MyLastSaban(string key, string value) : base(key,value)
        {
            try
            {
                LoadLastFile();
            }
            catch (Exception)
            {
                
            }
            

            
        }

        /// <summary>
        /// Sửa đường dẫn tĩnh thuộc tính LastBdTC của file .last thành đường dẫn động chỉ lưu mỗi file name
        /// </summary>
        public void EditLastBdTCAttribute(string lastFile, string lastBdTCFile, string fileName)
        {
            //string[] paths = lastBdTCFile.Split(new char[] { '\\' });
            //string fileName = paths[paths.Length - 1];

            //Load nội dung của file .last
            StreamReader reader = new StreamReader(lastFile);
            string str = "";
            while (reader.Peek() >= 0)
            {
                str = reader.ReadToEnd();
                //sửa đường dẫn tĩnh thành đường dẫn động
                str = str.Replace(lastBdTCFile, fileName);
            }
            reader.Close();

            using (FileStream fs = new FileStream(lastFile, FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(str);
            }
        }

        /// <summary>
        /// Kiểm tra file .last
        /// </summary>
        private void LoadLastFile()
        {
            XmlDocument myDiaHinhFileDocument = new XmlDocument();
            myDiaHinhFileDocument.Load(FULLPATH_FILE);
            
            _nodeLAST = myDiaHinhFileDocument.DocumentElement;

            //Nếu thuộc tính LastBdTC chứa đường dẫn tĩnh thì sửa lai
            string[] strArr = _nodeLAST.Attributes["LastBdTC"].Value.Split(new char[] { '\\' });

            if (strArr.Length > 1)
            {
                string fileName = strArr[strArr.Length - 1];
                EditLastBdTCAttribute(FULLPATH_FILE, _nodeLAST.Attributes["LastBdTC"].Value, fileName);
                //sửa xong thì load lại
                myDiaHinhFileDocument.Load(FULLPATH_FILE);
                _nodeLAST = myDiaHinhFileDocument.DocumentElement;
            }

            //Kiểm tra sự tồn tại các giá trị
            _mySaBanDir = _nodeLAST.Attributes["mySaBanDir"].Value;
            _mySaBanDirFullPath = Path.GetFullPath(_mySaBanDir);
            _LastBdTC = _nodeLAST.Attributes["LastBdTC"].Value;
            _LastBdTCFullPath = Path.GetFullPath(_LastBdTC);

        }

        /// <summary>
        /// Kiểm tra file .last
        /// </summary>
        public void BindLastFile_ListView(ListView listViewLastConfig)
        {
            if (!File.Exists(FULLPATH_FILE))
                return;
            
            //Điều chỉnh độ rộng column của listView
            listViewLastConfig.Columns[0].Width = 100;
            listViewLastConfig.Columns[1].Width = 400;
            listViewLastConfig.Columns[2].Width = 300;
            listViewLastConfig.Columns[3].Width = 250;
            //Load thuộc tính vào listView
            foreach (XmlAttribute att in _nodeLAST.Attributes)
            {
                ListViewItem item = listViewLastConfig.Items.Add(att.Name, att.Name, -1);
                item.SubItems.Add(att.Value);
                item.SubItems.Add("");
                item.SubItems.Add("");
            }

            //Kiểm tra sự tồn tại các giá trị
            string key = "mySaBanDir";
            BDTCHelper.CheckExistLink(listViewLastConfig.Items[key]);

            key = "LastBdTC";
            BDTCHelper.CheckExistLink(listViewLastConfig.Items[key]);
        }
    }
}
