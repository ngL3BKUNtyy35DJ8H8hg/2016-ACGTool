using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

using System.Xml.Serialization;
using System.Xml;
using System.Web;
using ACGTool.Classes;

namespace ACGTool
{
    
    public partial class frmCopyPasteBDTC : Form
    {
        int srcIndex, desIndex;
        //bool isSelected = true; //=False để không sử dụng listBox1_SelectedIndexChanged
        XmlDocument mySrcBDTCFileDocument;
        XmlDocument mySrcXmlDocument;

        XmlDocument myDesBDTCFileDocument;
        XmlDocument myDesXmlDocument;
        public frmCopyPasteBDTC()
        {
            InitializeComponent();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            //mo form cho nguoi ta chon co so du lieu
            OpenFileDialog1.Filter = "Excel File(*.BDTC)|*.BDTC;|All files (*.*)|*.*";
            txtFileSrc.Text = "";
            OpenFileDialog1.ShowDialog();
            txtFileSrc.Text = OpenFileDialog1.FileName;
        }

        private void btnLoadScr_Click(object sender, EventArgs e)
        {
            string tempFile = "temp.xml";
            if (txtFileSrc.Text == string.Empty)
                return;

            mySrcBDTCFileDocument = new XmlDocument();
            mySrcBDTCFileDocument.Load(txtFileSrc.Text);

            int count = 0;
            XmlNode node;
            node = mySrcBDTCFileDocument.DocumentElement;
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
                            richTextBoxSrc.Text = att.Value;
                        }
                    }
                }

            //Ghi file xml temp
            StreamWriter writer = new StreamWriter(tempFile);
            writer.Write(richTextBoxSrc.Text);
            writer.Close();

            //Doc file xml temp vua ghi de lay danh sach cac ky hieu
            mySrcXmlDocument = new XmlDocument();
            mySrcXmlDocument.Load(tempFile);
            //XmlNode node;
            node = mySrcXmlDocument.DocumentElement;
            listBoxSrc.Items.Clear();
            foreach (XmlNode node1 in node.ChildNodes)
                if (node1.Name == "KyHieu")
                {
                    foreach (XmlAttribute att in node1.Attributes)
                    {
                        if (att.Name == "Desc")
                        {
                            listBoxSrc.Items.Add(att.Value);
                        }
                    }
                }
            //Save file xml to richtextboxview view xml format
            mySrcXmlDocument.Save(tempFile);

            //Xóa file xml temp
            File.Delete(tempFile);

            srcIndex = desIndex;
        }

       
        private void btnSaveDesFile_Click(object sender, EventArgs e)
        {
            if (richTextBoxDes.Text != string.Empty)
            {
                XmlNode node;
                node = myDesBDTCFileDocument.DocumentElement;
                foreach (XmlNode node1 in node.ChildNodes)
                    if (node1.Name == "Slide" || node1.Name == "Page")
                    {
                        foreach (XmlAttribute att in node1.Attributes)
                        {
                            if (att.Name == "Symbols")
                            {
                                att.Value = richTextBoxDes.Text;
                            }
                        }
                    }
                myDesBDTCFileDocument.Save(txtFileDes.Text);
                MessageBox.Show("Save successfull!");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxSrc.SelectedIndex > -1)
            {
                XmlNode node;
                node = mySrcXmlDocument.DocumentElement;
                node = node.ChildNodes[listBoxSrc.SelectedIndex];
                string strFind = "<KyHieu", strEnd = "</KyHieu>";
                if (node.Name == "KyHieu")
                {
                    //str = node1.;
                    foreach (XmlAttribute att in node.Attributes)
                    {
                        strFind += " " + att.Name + "=\"" + att.Value + "\"";
                    }
                    strFind += ">";
                }
                int indexToText, endIndex;
                //Tìm theo toàn bộ element trước để lấy index trước
                indexToText = richTextBoxSrc.Find(strFind, 0, -1, RichTextBoxFinds.WholeWord);
                //Sau đó tìm chính xác từ đó tại vị trí index vừa có
                //indexToText = richTextBoxSrc.Find(listBoxSrc.SelectedItem.ToString(), indexToText, -1, RichTextBoxFinds.WholeWord);
                endIndex = richTextBoxSrc.Find(strEnd, indexToText, -1, RichTextBoxFinds.WholeWord);
                //richtextbox origion
                richTextBoxSrc.SelectionStart = indexToText;
                //richTextBoxSrc.SelectionLength = listBoxSrc.SelectedItem.ToString().Length;
                richTextBoxSrc.SelectionLength = endIndex + strEnd.Length - indexToText;
                richTextBoxSrc.ScrollToCaret();
                richTextBoxSrc.Focus();
            }
        }

        private void btBrowseSave_Click(object sender, EventArgs e)
        {
            //mo form cho nguoi ta chon co so du lieu
            OpenFileDialog1.Filter = "Excel File(*.BDTC)|*.BDTC;|All files (*.*)|*.*";
            txtFileDes.Text = "";
            OpenFileDialog1.ShowDialog();
            txtFileDes.Text = OpenFileDialog1.FileName;
        }

        private void BDTCTool_Load(object sender, EventArgs e)
        {
            txtFileDes.Text = txtFileSrc.Text = System.Environment.CurrentDirectory + "\\" + "New.BDTC";
            lblDesResult.Text = lblSrcResult.Text = "";
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxSrc.SelectedIndex > -1)
            {
                textBoxFind.Text = listBoxSrc.SelectedItem.ToString();
                textBoxDesFind.Text = textBoxFind.Text;
                buttonDesFind_Click(null, null);
            }
        }

        private void btnSrcFind_Click(object sender, EventArgs e)
        {
            //Nếu textBoxFind khác empty thì chỉ reset những tên trùng với textboxFind
            if (textBoxFind.Text == string.Empty)
            {
                //MessageBox.Show("Nhập giá trị cần tìm vào.");
                srcIndex = -1;
                textBoxFind.Focus();
                return;
            }

            //listBoxSrc.Items.Clear();
            bool isFind = false;
            
            // Set our intial index variable to -1.
            int index;
            //posSrc = -1;
            if (textBoxFind.Text.Length != 0)
            {
                //// Loop through and find each item that matches the search string.
                //do
                //{
                    // Retrieve the item based on the previous index found. Starts with -1 which searches start.
                    index = listBoxSrc.FindString(textBoxFind.Text, srcIndex);
                    // If no item is found that matches exit.
                    if (index > 0)
                    {
                        isFind = true;
                        srcIndex = index;
                        //Add to listbox2
                        listBoxSrc.SelectedIndex = srcIndex;
                    }
                    else
                    {
                        //if index <= x then exit loop
                        srcIndex = -1;
                        listBoxSrc.SelectedIndex = srcIndex;
                    }
                //} while (srcIndex != -1);
            }
            //Nếu không tìm thấy thì báo
            if (!isFind)
            {
                //MessageBox.Show("Source Find: Không tìm thấy ký hiệu có tên " + textBoxFind.Text);
                lblSrcResult.Text = "Không tìm thấy ký hiệu";
            }
            else
            {
                lblSrcResult.Text = "";
            }

        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxDes.SelectedIndex > -1)
            {
                textBoxFind.Text = listBoxDes.SelectedItem.ToString();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index2 = -1;
            //Xác định index của listbox2 từ đó suy ra index của listbox1
            index2 = listBoxDes.SelectedIndex;

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
                    index = listBoxSrc.FindString(textBoxFind.Text, x);
                    // If no item is found that matches exit.
                    if (index > x)
                    {
                        //Giảm index2 cho đến -1 thì dừng
                        index2--;
                        if (index2 == -1)
                        {
                            //Xác định được index
                            listBoxSrc.SelectedIndex = index;
                            return;
                        }
                        x = index;
                    }
                    else //if index <= x then exit loop
                        x = -1;

                } while (x != -1);
            }

        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            
        }

        private void btnLoadDes_Click(object sender, EventArgs e)
        {
            string tempFile = "temp.xml";
            if (txtFileDes.Text == string.Empty)
                return;

            myDesBDTCFileDocument = new XmlDocument();
            myDesBDTCFileDocument.Load(txtFileDes.Text);

            int count = 0;
            XmlNode node;
            node = myDesBDTCFileDocument.DocumentElement;
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
                            richTextBoxDes.Text = att.Value;
                        }
                    }
                }

            //Ghi file xml temp
            StreamWriter writer = new StreamWriter(tempFile);
            writer.Write(richTextBoxDes.Text);
            writer.Close();

            //Doc file xml temp vua ghi de lay danh sach cac ky hieu
            myDesXmlDocument = new XmlDocument();
            myDesXmlDocument.Load(tempFile);
            //XmlNode node;
            node = myDesXmlDocument.DocumentElement;
            listBoxDes.Items.Clear();
            foreach (XmlNode node1 in node.ChildNodes)
                if (node1.Name == "KyHieu")
                {
                    foreach (XmlAttribute att in node1.Attributes)
                    {
                        if (att.Name == "Desc")
                        {
                            listBoxDes.Items.Add(att.Value);
                        }
                    }
                }
            //Save file xml to richtextboxview view xml format
            myDesXmlDocument.Save(tempFile);

            //Xóa file xml temp
            File.Delete(tempFile);
        }

        private void listBoxDes_DoubleClick(object sender, EventArgs e)
        {
            if (listBoxDes.SelectedIndex > -1)
            {
                textBoxDesFind.Text = listBoxDes.SelectedItem.ToString();
                textBoxFind.Text = textBoxDesFind.Text;
                btnSrcFind_Click(null,null);
            }
        }

        private void listBoxDes_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (listBoxDes.SelectedIndex > -1)
            {
                XmlNode node;
                node = myDesXmlDocument.DocumentElement;
                node = node.ChildNodes[listBoxDes.SelectedIndex];
                string strFind = "<KyHieu", strEnd = "</KyHieu>";
                if (node.Name == "KyHieu")
                {
                    //str = node1.;
                    foreach (XmlAttribute att in node.Attributes)
                    {
                        strFind += " " + att.Name + "=\"" + att.Value + "\"";
                    }
                    strFind += ">";
                }
                int indexToText, endIndex;
                //Tìm theo toàn bộ element trước để lấy index trước
                indexToText = richTextBoxDes.Find(strFind, 0, -1, RichTextBoxFinds.WholeWord);
                //Sau đó tìm chính xác từ đó tại vị trí index vừa có
                //indexToText = richTextBoxDes.Find(listBoxDes.SelectedItem.ToString(), indexToText, -1, RichTextBoxFinds.WholeWord);
                endIndex = richTextBoxDes.Find(strEnd, indexToText, -1, RichTextBoxFinds.WholeWord);
                //richtextbox origion
                richTextBoxDes.SelectionStart = indexToText;
                //richTextBoxDes.SelectionLength = listBoxDes.SelectedItem.ToString().Length;
                richTextBoxDes.SelectionLength = endIndex + strEnd.Length - indexToText;
                richTextBoxDes.ScrollToCaret();
                richTextBoxDes.Focus();
            }
        }

        private void buttonDesFind_Click(object sender, EventArgs e)
        {
            //Nếu textBoxFind khác empty thì chỉ reset những tên trùng với textboxFind
            if (textBoxDesFind.Text == string.Empty)
            {
                //MessageBox.Show("Nhập giá trị cần tìm vào.");
                desIndex = -1;
                textBoxDesFind.Focus();
                return;
            }

            //listBoxDes.Items.Clear();
            bool isFind = false;
          
            // Set our intial index variable to -1.
            int index;
            //x = -1;
            if (textBoxDesFind.Text.Length != 0)
            {
                //// Loop through and find each item that matches the search string.
                //do
                //{
                    // Retrieve the item based on the previous index found. Starts with -1 which searches start.
                    index = listBoxDes.FindString(textBoxDesFind.Text, desIndex);
                    // If no item is found that matches exit.
                    if (index > 0)
                    {
                        isFind = true;
                        desIndex = index;
                        //Add to listbox2
                        listBoxDes.SelectedIndex = desIndex;
                    }
                    else //if index <= x then exit loop
                    {
                        desIndex = -1;
                        listBoxDes.SelectedIndex = desIndex;
                    }
                //} while (x != -1);
            }
            //Nếu không tìm thấy thì báo
            if (!isFind)
            {
                //MessageBox.Show("Des Find: Không tìm thấy ký hiệu có tên " + textBoxDesFind.Text);
                lblDesResult.Text = "Không tìm thấy ký hiệu";
            }
            else
            {
                lblDesResult.Text = "";
            }
        }

        private void btnSaveSrcFile_Click(object sender, EventArgs e)
        {
            if (richTextBoxSrc.Text != string.Empty)
            {
                XmlNode node;
                node = mySrcBDTCFileDocument.DocumentElement;
                foreach (XmlNode node1 in node.ChildNodes)
                    if (node1.Name == "Slide" || node1.Name == "Page")
                    {
                        foreach (XmlAttribute att in node1.Attributes)
                        {
                            if (att.Name == "Symbols")
                            {
                                att.Value = richTextBoxSrc.Text;
                            }
                        }
                    }
                mySrcBDTCFileDocument.Save(txtFileSrc.Text);
                MessageBox.Show("Save successfull!");
            }
        }

        private void buttonDi_Click(object sender, EventArgs e)
        {
            if (richTextBoxSrc.SelectedText != string.Empty)
            {
                richTextBoxSrc.Copy();
                XmlNode node;
                node = myDesXmlDocument.DocumentElement;
                node = node.ChildNodes[listBoxDes.SelectedIndex];
                string strFind = "</KyHieus>";
                //Tìm theo toàn bộ element trước để lấy index trước
                int indexToText = richTextBoxDes.Find(strFind, 0, -1, RichTextBoxFinds.WholeWord);
                //richtextbox origion
                richTextBoxDes.SelectionStart = indexToText;
                //richTextBoxDes.SelectionLength = listBoxDes.SelectedItem.ToString().Length;
                richTextBoxDes.SelectionLength = 0;
                richTextBoxDes.ScrollToCaret();
                richTextBoxDes.Focus();
                richTextBoxDes.Paste();
            }
        }

        private void buttonVe_Click(object sender, EventArgs e)
        {
            if (richTextBoxDes.SelectedText != string.Empty)
            {
                richTextBoxSrc.Copy();
                XmlNode node;
                node = mySrcXmlDocument.DocumentElement;
                node = node.ChildNodes[listBoxSrc.SelectedIndex];
                string strFind = "</KyHieus>";
                //Tìm theo toàn bộ element trước để lấy index trước
                int indexToText = richTextBoxSrc.Find(strFind, 0, -1, RichTextBoxFinds.WholeWord);
                //richtextbox origion
                richTextBoxSrc.SelectionStart = indexToText;
                richTextBoxSrc.SelectionLength = 0;
                richTextBoxSrc.ScrollToCaret();
                richTextBoxSrc.Focus();
                richTextBoxSrc.Paste();
            }
        }
    }
}