using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Xml;
using System.Web;
using System.Threading;
using BDTCLib;
using BDTCLib.Scripts;
//using System.Text.RegularExpressions;

namespace ConfigBDTC
{
    public partial class BDTCTool : Form
    {
        private XmlDocument _myBDTCFileDocument;
        private XmlDocument _myXmlDocument;

        //Tinh thoi gian chay moi script
        //SortedDictionary<string, float> _TotalTime;
        //SortedDictionary<string, float> _StartTime;

        public BDTCTool()
        {
            InitializeComponent();
        }

        //private void btBrowse_Click(object sender, EventArgs e)
        //{
        //    //mo form cho nguoi ta chon co so du lieu
        //    OpenFileDialog1.Filter = "BDTC File(*.BDTC)|*.BDTC;|All files (*.*)|*.*";
        //    txtFilePath.Text = "";
        //    OpenFileDialog1.ShowDialog();
        //    txtFilePath.Text = OpenFileDialog1.FileName;
        //}

        private void LoadBDTCFile()
        {
            string tempFile = "temp.xml";
            if (txtFilePath.Text == string.Empty)
                return;

            _myBDTCFileDocument = new XmlDocument();
            _myBDTCFileDocument.Load(txtFilePath.Text);

            int count = 0;
            XmlNode node;
            node = _myBDTCFileDocument.DocumentElement;
            foreach (XmlNode node1 in node.ChildNodes)
                if (node1.Name == "Slide" || node1.Name == "Page")
                {
                    foreach (XmlAttribute att in node1.Attributes)
                    {
                        if (att.Name == "Symbols")
                        {
                            count++;
                            if (count == 2)
                            {
                                MessageBox.Show("File .BDTC nay co 2 trang sa ban. Chuong trinh chi load Trang thu 1 len");
                                return;
                            }
                            richTextBoxOrigion.Text = att.Value;
                        }
                    }
                }

            //Ghi file xml temp
            StreamWriter writer = new StreamWriter(tempFile);
            writer.Write(richTextBoxOrigion.Text);
            writer.Close();

            //Doc file xml temp vua ghi de lay danh sach cac ky hieu
            _myXmlDocument = new XmlDocument();
            _myXmlDocument.Load(tempFile);
            //XmlNode node;
            node = _myXmlDocument.DocumentElement;
            listBox1.Items.Clear();
            foreach (XmlNode node1 in node.ChildNodes)
                if (node1.Name == "KyHieu")
                {
                    foreach (XmlAttribute att in node1.Attributes)
                    {
                        if (att.Name == "Desc")
                        {
                            listBox1.Items.Add(att.Value);
                        }
                    }
                }
            //Save file xml to richtextboxview view xml format
            _myXmlDocument.Save(tempFile);
            //Xử lý StreamReader để đọc file rồi mới gán vào richtextbox
            //Vì nếu dùng richTextBoxView.LoadFile để load file thì không hiển thị được tiếng việt
            StreamReader reader = new StreamReader(tempFile);
            while (reader.Peek() >= 0)
            {
                string str = reader.ReadToEnd();
                richTextBoxView.Text = str;
            }
            reader.Close();

            //Xóa file xml temp
            File.Delete(tempFile);
        }

        private void SaveBDTC()
        {
            //Save file BDTC
            if (richTextBoxOrigion.Text != string.Empty)
            {
                XmlElement node = _myBDTCFileDocument.DocumentElement;
                foreach (XmlNode node1 in node.ChildNodes)
                    if (node1.Name == "Slide" || node1.Name == "Page")
                    {
                        foreach (XmlAttribute att in node1.Attributes)
                        {
                            if (att.Name == "Symbols")
                            {
                                att.Value = richTextBoxOrigion.Text;
                            }
                        }
                    }
                _myBDTCFileDocument.Save(txtFilePath.Text);
            }
        }

        private void btnResetID_Click(object sender, EventArgs e)
        {
            if (textBoxReplace.Text == string.Empty)
            {
                MessageBox.Show("Nhập tên cần Reset trước.");
                textBoxReplace.Focus();
                return;
            }
            bool isFind = false;
            XmlNode rootNode, node;
            rootNode = _myXmlDocument.DocumentElement;
            int indexToText, indexToTextView;
            indexToText = indexToTextView = 0;
            string strFind = "";
            //Nếu textBoxFind khác empty thì chỉ reset những tên chứa chuỗi là textboxFind.Text
            if (textBoxFind.Text != "")
            {
                //Nếu check auto tự động tăng
                if (checkBoxAutoNumber.Checked)
                {
                    int count = 1;
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        node = rootNode.ChildNodes[i];
                        if (node.Name == "KyHieu" && node.Attributes["Desc"].Value.Contains(textBoxFind.Text))
                        {
                            //Nếu tìm thấy
                            isFind = true;

                            XmlAttribute att = node.Attributes["Desc"];
                            strFind = att.Name + "=\"" + att.Value + "\"";
                            //find text
                            indexToText = richTextBoxOrigion.Find(strFind, indexToText, -1, RichTextBoxFinds.WholeWord);
                            indexToTextView = richTextBoxView.Find(strFind, indexToTextView, -1, RichTextBoxFinds.WholeWord);
                            if (indexToText > 0)
                            {
                                //replace text in xmldocument with auto
                                att.Value = textBoxReplace.Text + count.ToString();

                                //replace text in richtextbox
                                richTextBoxOrigion.SelectedText = att.Name + "=\"" + att.Value + "\"";
                                richTextBoxView.SelectedText = att.Name + "=\"" + att.Value + "\"";

                                //replace tất cả ObjName trong các file script thỏa điều kiện
                                TimeLineHelper._objMyMnu.RenameObjNameInScriptXmlFiles(listBox1.Items[i].ToString(), att.Value);
                                //replace text in textbox
                                listBox1.Items[i] = att.Value;
                                count++;
                            }
                        }
                    }
                } //end if (checkBoxAutoNumber.Checked)
                else //Nếu không check auto tự động tăng
                {
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        node = rootNode.ChildNodes[i];

                        if (node.Name == "KyHieu" && node.Attributes["Desc"].Value.Contains(textBoxFind.Text))
                        {
                            //Nếu tìm thấy
                            isFind = true;
                            XmlAttribute att = node.Attributes["Desc"];
                            strFind = att.Name + "=\"" + att.Value + "\"";
                            //find text
                            indexToText = richTextBoxOrigion.Find(strFind, indexToText, -1, RichTextBoxFinds.WholeWord);
                            indexToTextView = richTextBoxView.Find(strFind, indexToTextView, -1, RichTextBoxFinds.WholeWord);
                            if (indexToText > 0)
                            {
                                //replace text in xmldocument without auto
                                att.Value = textBoxReplace.Text;

                                //replace text in richtextbox
                                richTextBoxOrigion.SelectedText = att.Name + "=\"" + att.Value + "\"";
                                richTextBoxView.SelectedText = att.Name + "=\"" + att.Value + "\"";

                                //replace tất cả ObjName trong các file script thỏa điều kiện
                                TimeLineHelper._objMyMnu.RenameObjNameInScriptXmlFiles(listBox1.Items[i].ToString(), att.Value);
                                //replace text in textbox
                                listBox1.Items[i] = att.Value;

                            }
                        }
                    }
                }
                //Nếu không tìm thấy thì báo
                if (!isFind)
                {
                    MessageBox.Show("Không tìm thấy ký hiệu có tên " + textBoxFind.Text);
                }
            }
            else //Nếu textBoxFind = empty thì reset toàn bộ
            {
                //Nếu check auto tự động tăng
                if (checkBoxAutoNumber.Checked)
                {
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        node = rootNode.ChildNodes[i];
                        if (node.Name == "KyHieu")
                        {
                            XmlAttribute att = node.Attributes["Desc"];
                            strFind = att.Name + "=\"" + att.Value + "\"";
                            //find text
                            indexToText = richTextBoxOrigion.Find(strFind, indexToText, -1, RichTextBoxFinds.WholeWord);
                            indexToTextView = richTextBoxView.Find(strFind, indexToTextView, -1, RichTextBoxFinds.WholeWord);
                            if (indexToText > 0)
                            {
                                //replace text in xmldocument
                                att.Value = textBoxReplace.Text + (i + 1).ToString();

                                //replace text in richtextbox
                                richTextBoxOrigion.SelectedText = att.Name + "=\"" + att.Value + "\"";
                                richTextBoxView.SelectedText = att.Name + "=\"" + att.Value + "\"";

                                //replace tất cả ObjName trong các file script thỏa điều kiện
                                TimeLineHelper._objMyMnu.RenameObjNameInScriptXmlFiles(listBox1.Items[i].ToString(), att.Value);
                                //replace text in textbox
                                listBox1.Items[i] = att.Value;
                            }
                        }
                    }
                }
                else //Nếu không check auto tự động tăng
                {
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        node = rootNode.ChildNodes[i];
                        if (node.Name == "KyHieu")
                        {
                            XmlAttribute att = node.Attributes["Desc"];
                            strFind = att.Name + "=\"" + att.Value + "\"";
                            //find text
                            indexToText = richTextBoxOrigion.Find(strFind, indexToText, -1, RichTextBoxFinds.WholeWord);
                            indexToTextView = richTextBoxView.Find(strFind, indexToTextView, -1, RichTextBoxFinds.WholeWord);
                            if (indexToText > 0)
                            {
                                //replace text in xmldocument without auto
                                att.Value = textBoxReplace.Text;

                                //replace text in richtextbox
                                richTextBoxOrigion.SelectedText = att.Name + "=\"" + att.Value + "\"";
                                richTextBoxView.SelectedText = att.Name + "=\"" + att.Value + "\"";

                                //replace tất cả ObjName trong các file script thỏa điều kiện
                                TimeLineHelper._objMyMnu.RenameObjNameInScriptXmlFiles(strFind, att.Value);
                                //replace text in textbox
                                listBox1.Items[i] = att.Value;
                            }
                        }
                    }
                }
            }

            //Save BDTC file
            SaveBDTC();

            MessageBox.Show("Reset successfull!");
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    richTextBoxOrigion.Text = System.Web.HttpUtility.HtmlEncode(richTextBoxOrigion.Text);
        //}

        //private void button2_Click(object sender, EventArgs e)
        //{
        //    richTextBoxOrigion.Text = System.Web.HttpUtility.HtmlDecode(richTextBoxOrigion.Text);
        //}

        //private void buttonSaveFile_Click(object sender, EventArgs e)
        //{
        //    if (richTextBoxOrigion.Text != string.Empty)
        //    {
        //        XmlNode node;
        //        node = _myBDTCFileDocument.DocumentElement;
        //        foreach (XmlNode node1 in node.ChildNodes)
        //            if (node1.Name == "Slide" || node1.Name == "Page")
        //            {
        //                foreach (XmlAttribute att in node1.Attributes)
        //                {
        //                    if (att.Name == "Symbols")
        //                    {
        //                        att.Value = richTextBoxOrigion.Text;
        //                    }
        //                }
        //            }
        //        _myBDTCFileDocument.Save(txtFilePathOut.Text);
        //        MessageBox.Show("Save successfull!");
        //    }
        //}

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                XmlNode node;
                node = _myXmlDocument.DocumentElement;
                node = node.ChildNodes[listBox1.SelectedIndex];
                string strFind = node.OuterXml;
                string strEnd = "</KyHieu>";

                int indexToText, endIndex;
                //Tìm theo toàn bộ element trước để lấy index trước
                indexToText = richTextBoxOrigion.Find(strFind, 0, -1, RichTextBoxFinds.WholeWord);
                //Sau đó tìm chính xác từ đó tại vị trí index vừa có
                endIndex = richTextBoxOrigion.Find(strEnd, indexToText, -1, RichTextBoxFinds.WholeWord);
                //richtextbox origion
                richTextBoxOrigion.SelectionStart = indexToText;
                richTextBoxOrigion.SelectionLength = endIndex + strEnd.Length - indexToText;
                richTextBoxOrigion.ScrollToCaret();
                richTextBoxOrigion.Focus();
                //======================================
                Clipboard.Clear();
                richTextBoxOrigion.Copy();

                //======================================
                //richtextbox view
                //Tìm theo toàn bộ element trước để lấy index trước
                strFind = "<KyHieu";

                if (node.Name == "KyHieu")
                {
                    //str = node1.;
                    foreach (XmlAttribute att in node.Attributes)
                    {
                        strFind += " " + att.Name + "=\"" + att.Value + "\"";
                    }
                    strFind += ">";
                }
                indexToText = richTextBoxView.Find(strFind, indexToText, -1, RichTextBoxFinds.WholeWord);
                //Sau đó tìm chính xác từ đó tại vị trí index vừa có
                endIndex = richTextBoxView.Find(strEnd, indexToText, -1, RichTextBoxFinds.WholeWord);
                richTextBoxView.SelectionStart = indexToText;
                richTextBoxView.SelectionLength = endIndex + strEnd.Length - indexToText;
                richTextBoxView.ScrollToCaret();
                richTextBoxView.Focus();
            }
        }

        private void BDTCTool_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                textBoxFind.Text = listBox1.SelectedItem.ToString();
            }
        }

        private void btnExportXML_Click(object sender, EventArgs e)
        {
            //Nếu textBoxFind khác empty thì chỉ reset những tên trùng với textboxFind
            if (textBoxFind.Text == string.Empty)
            {
                MessageBox.Show("Nhập giá trị cần tìm vào.");
                textBoxFind.Focus();
                return;
            }

            listBox2.Items.Clear();
            bool isFind = false;

            // Set our intial index variable to -1.
            int index, x;
            x = -1;
            if (textBoxFind.Text.Length != 0)
            {
                //// Loop through and find each item that matches the search string.
                do
                {
                    // Retrieve the item based on the previous index found. Starts with -1 which searches start.
                    if (checkBoxFindExact.Checked)
                        index = listBox1.FindStringExact(textBoxFind.Text, x);
                    else
                        index = listBox1.FindString(textBoxFind.Text, x);
                    // If no item is found that matches exit.
                    if (index > x)
                    {
                        isFind = true;
                        x = index;
                        //Add to listbox2
                        listBox2.Items.Add(listBox1.Items[x]);
                    }
                    else //if index <= x then exit loop
                        x = -1;
                } while (x != -1);
            }
            //Nếu không tìm thấy thì báo
            if (!isFind)
            {
                MessageBox.Show("Không tìm thấy ký hiệu có tên " + textBoxFind.Text);
            }

        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex > -1)
            {
                textBoxFind.Text = listBox2.SelectedItem.ToString();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index2 = -1;
            //Xác định index của listbox2 từ đó suy ra index của listbox1
            index2 = listBox2.SelectedIndex;

            // Set our intial index variable to -1.
            int index, x;
            x = -1;
            // If the search string is empty exit.
            if (textBoxFind.Text.Length != 0)
            {
                // Loop through and find each item that matches the search string.
                do
                {
                    // Retrieve the item based on the previous index found. Starts with -1 which searches start.
                    index = listBox1.FindString(textBoxFind.Text, x);
                    // If no item is found that matches exit.
                    if (index > x)
                    {
                        //Giảm index2 cho đến -1 thì dừng
                        index2--;
                        if (index2 == -1)
                        {
                            //Xác định được index
                            listBox1.SelectedIndex = index;
                            return;
                        }
                        x = index;
                    }
                    else //if index <= x then exit loop
                        x = -1;

                } while (x != -1);
            }

        }

        #region "treeViewScript"

        #region "treeViewScript Functions"

        //Add node dau tien cua treeview
        public void FormatNode(ref TreeNode node)
        {
            node.ForeColor = Color.Blue;
            node.ImageIndex = 0;
            node.SelectedImageIndex = 0;
        }

        public void FormatLeafNode(ref TreeNode node)
        {
            node.ForeColor = Color.DarkRed;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 0;
        }

        #endregion

        private void richTextBoxOrigion_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEditPos_Click(object sender, EventArgs e)
        {
            decimal dX = decimal.Parse(btnX.Text);
            decimal dY = decimal.Parse(btnY.Text);

            //Doc file xml temp vua ghi de lay danh sach cac ky hieu
            XmlNode node = _myXmlDocument.DocumentElement;
            listBox1.Items.Clear();
            foreach (XmlNode node1 in node.ChildNodes)
                if (node1.Name == "KyHieu")
                {
                    node1.Attributes["GocX"].Value = (decimal.Parse(node1.Attributes["GocX"].Value) + dX).ToString();
                    node1.Attributes["GocY"].Value = (decimal.Parse(node1.Attributes["GocY"].Value) + dY).ToString();
                }
            richTextBoxOrigion.Text = _myXmlDocument.OuterXml;

            //Save xuống file
            SaveBDTC();

            //node = _myBDTCFileDocument.DocumentElement;
            //foreach (XmlNode node1 in node.ChildNodes)
            //    if (node1.Name == "Slide" || node1.Name == "Page")
            //    {
            //        foreach (XmlAttribute att in node1.Attributes)
            //        {
            //            if (att.Name == "Symbols")
            //            {
            //                att.Value = richTextBoxOrigion.Text;
            //            }
            //        }
            //    }
            //_myBDTCFileDocument.Save(txtFilePath.Text);
            MessageBox.Show("Save successfull!");
        }

        private void btnBrowseDiaHinh_Click(object sender, EventArgs e)
        {
            try
            {
                //mo form cho nguoi ta chon co so du lieu
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.Filter = "DiaHinh File(*.diahinh)|*.diahinh";
                txtDiaHinhFilePath.Text = "";
                if (Properties.Settings.Default.RecentDiaHinhFile != "")
                    openFileDialog1.InitialDirectory = Path.GetDirectoryName(Properties.Settings.Default.RecentDiaHinhFile);
                openFileDialog1.ShowDialog();
                txtDiaHinhFilePath.Text = openFileDialog1.FileName;
                Properties.Settings.Default.RecentDiaHinhFile = txtDiaHinhFilePath.Text;
                Properties.Settings.Default.Save();

                btnReload.PerformClick();
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

        private void btnReload_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(new ThreadStart(DoSplash));
            th.Start();
            try
            {
                LoadFile(txtDiaHinhFilePath.Text);
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

        private void LoadFile(string filePath)
        {
            try
            {
                TimeLineHelper._objDiaHinh = new DiaHinh(filePath);
                TimeLineHelper._objMyMnu = new MyMnu(TimeLineHelper._objDiaHinh);
                txtFilePath.Text = TimeLineHelper._objDiaHinh._objMyLastSaban._LastBdTCFullPath;
                LoadBDTCFile();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Save BDTC file
            SaveBDTC();
        }
    }
}