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
    public class MySpriteTexsFile : BaseMyFile
    {
        public MySpriteTexsFile(string key, string value)
            : base(key, value)
        {
            
        }

        public void BindD3DSpriteTexs_ListView(ListView listViewD3DSpriteTexs)
        {
            if (!File.Exists(FULLPATH_FILE))
                return;

            /*Kiểm tra cấu hình file ảnh 
             * Trường hợp 1: Kiểm tra file ảnh có tồn tại không
             * Trường hợp 2: Nếu cấu hình đúng kiểm tra có file ảnh như trong đường dẫn file không
             * Trường hợp 2: Kiểm tra có thiếu cấu hình file ảnh mặc định
             */
            listViewD3DSpriteTexs.Items.Clear();
   
            //Danh sách file ảnh mặc định dùng cho kịch bản 3D
            List<string> defaultImages = new List<string>();
            //explosion1.bmp dùng trong hiệu ứng Nổ, Khai báo quả nổ
            defaultImages.Add("explosion1.bmp");
            //dan2.bmp dùng trong hiệu ứng bắn thẳng
            defaultImages.Add("dan2.bmp");
            //rocket2.png dùng trong hiệu ứng bắn cong
            defaultImages.Add("rocket2.png");

            XmlDocument imagesDocument = new XmlDocument();
            //Load file D3DSpriteTexs.xml
            imagesDocument.Load(FULLPATH_FILE);
            foreach (XmlNode node in imagesDocument.ChildNodes)
            {
                if (node.Name == "SpriteTexs")
                {
                    foreach (XmlNode node1 in node.ChildNodes)
                    {
                        //Bỏ qua comment của xml nếu có
                        if (node1.Name == "#comment")
                            continue;

                        //Lấy đường dẫn full
                        string fullPath = Path.GetFullPath(node1.Attributes["File"].Value);
                        ListViewItem item = new ListViewItem(node1.Attributes["File"].Value, node1.Attributes["File"].Value);
                        //Full Path
                        item.SubItems.Add(fullPath);
                        //Notes
                        item.SubItems.Add("");
                        item = listViewD3DSpriteTexs.Items.Add(item);

                        string[] paths = node1.Attributes["File"].Value.Split(new char[] { '\\' });
                        string fileName = paths[paths.Length - 1];
                        //Kiểm tra file ảnh tồn tại
                        if (File.Exists(fullPath))
                        {
                            //Kiểm tra xem file ảnh này có phải là file ảnh mặc định không (explosion1.bmp, dan2.bmp, rocket2.png) dùng để tạo kịch bản 3D
                            if (defaultImages.Contains(fileName))
                            {
                                item.SubItems[2].Text = "OK (file ảnh mặc định)";
                                item.ForeColor = Color.Black;
                                //nếu node này là một file ảnh mặc định thì remove file ảnh mặc định này trong defaultImages
                                defaultImages.Remove(fileName);
                            }
                            else //Nếu không phải là file ảnh mặc định thì chỉ thông báo OK
                                item.SubItems[2].Text = "OK";
                        }
                        else
                        {
                            if (defaultImages.Contains(fileName))
                            {
                                item.SubItems[2].Text = "File không tồn tại (file ảnh mặc định)";
                                defaultImages.Remove(fileName);
                            }
                            else
                            {
                                item.SubItems[2].Text = "File không tồn tại";
                            }
                            item.ForeColor = Color.Red;
                        }
                    }
                }
            }

            //Với những file ảnh mặc định còn lại trong defaultImages 
            //Thêm vào listview và thông báo thiếu file ảnh này
            for (int i = 0; i < defaultImages.Count; i++)
            {
                //Add các file ảnh mặc định còn thiếu vào listview
                ListViewItem item = new ListViewItem(defaultImages[i], defaultImages[i]);
                //Full Path
                item.SubItems.Add(defaultImages[i]);
                //Notes
                item.SubItems.Add(string.Format("Thiếu file ảnh {0} mặc định này trong cấu hình D3DSpriteTexs.xml ", defaultImages[i]));
                item = listViewD3DSpriteTexs.Items.Add(item);
                item.ForeColor = Color.Red;
            }

            //Kiểm tra xem file ảnh nào đang dùng trong kịch bản thật sự

        }
    }
}
