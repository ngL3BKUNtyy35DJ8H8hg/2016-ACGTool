using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace ACGTool
{
    public partial class PowerDesigner : Form
    {
        public PowerDesigner()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Replace varchar type To nvarchar type
        /// </summary>
        public void ReplaceVarcharToNvarchar()
        {
            string pattern = @"varchar";
            Regex r = new Regex(pattern);
            Match m = r.Match(richTextBox1.Text);
            string find = "";
            while (m.Success)
            {
                find = m.Value;
                richTextBox1.Text = richTextBox1.Text.Replace(find, "nvarchar");
                m.NextMatch();
            }
        }

        /// <summary>
        /// Replace Description to Unicode Description
        /// </summary>
        public void ReplaceDescriptionToUnicodeDescription()
        {
            string pattern = @"\'MS_Description\'\,\s*\n\s*\'";
            Regex r = new Regex(pattern);
            Match m = r.Match(richTextBox1.Text);
            string find = "", replace = "";
            if (m.Success)
            {
                find = m.Value;
                replace = find.Insert(find.Length - 1, "N");
                richTextBox1.Text = richTextBox1.Text.Replace(find, replace);
            }
        }

        public void SaveUnicodeFile()
        {
            //richTextBox1.SaveFile(txtFilePath.Text);
            Stream myFileStream = File.Open(txtFilePath.Text, FileMode.Open);
            System.Text.Encoding encoding = System.Text.Encoding.Unicode;
            myFileStream.Write(encoding.GetBytes(richTextBox1.Text), 0, encoding.GetByteCount(richTextBox1.Text));
            myFileStream.Close();
        }
        private void btnExecute_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                ReplaceVarcharToNvarchar();

            if (checkBox2.Checked)
                ReplaceDescriptionToUnicodeDescription();

            SaveUnicodeFile();
            MessageBox.Show("Complete!");
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            //mo form cho nguoi ta chon co so du lieu
            openFileDialog1.Filter = "Text File(*.txt)|*.txt;|All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = @"E:\Data\596\";
            
            openFileDialog1.ShowDialog();
            txtFilePath.Text = openFileDialog1.FileName;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            richTextBox1.LoadFile(txtFilePath.Text, RichTextBoxStreamType.PlainText);
        }

        private void PowerDesigner_Load(object sender, EventArgs e)
        {
            txtFilePath.Text = @"E:\Data\596\crebas.sql";
        }
        
    }
}