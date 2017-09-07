using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace DangKyModels
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            this.InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string startupPath = Application.StartupPath;
            modSaBan.myCTpara = startupPath + "\\SaBan.para";
            //if (File.Exists(modSaBan.myCTpara))
            //{
            //    modSaBan.LoadLastDiaHinh(modSaBan.myCTpara);
            //}
            //if (File.Exists(modSaBan.myDiaHinhDef))
            //{
            //    this.LoadSaBan(modSaBan.myDiaHinhDef);
            //}
            //else
            //{
            string text = modSaBan.myDiaHinhDef;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.FileName = text;
            openFileDialog.Filter = "Dia hinh def file (*.diahinh)|*.diahinh|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                text = openFileDialog.FileName;
                if (text.Length > 0)
                {
                    this.LoadSaBan(text);

                    //Update lại file config
                    string oldValue = modSaBan.myCurrentDirectory;
                    string newValue = Path.GetDirectoryName(modSaBan.myDiaHinhDef);
                    UpdateDiaHinhFileContent(oldValue, newValue);

                    oldValue = modSaBan.myKHCnnString;
                    newValue = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", Application.StartupPath + "\\CacKyHieu.mdb");
                    UpdateDiaHinhFileContent(oldValue, newValue);

                    Process.Start(Application.StartupPath + "\\SaBanRun.exe");
                }
            }

            // }
            this.Close();
        }


        private void OpenSaBan()
        {
            string text = modSaBan.myDiaHinhDef;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.FileName = text;
            openFileDialog.Filter = "Dia hinh def file (*.diahinh)|*.diahinh|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                text = openFileDialog.FileName;
                if (text.Length > 0)
                {
                    this.LoadSaBan(text);
                }
            }
        }

        public void LoadSaBan(string pDefFile)
        {
            if (modSaBan.File2Para(pDefFile))// && modSaBan.ParaOK())
            {
                modSaBan.myDiaHinhDef = pDefFile;
                modSaBan.LastDiaHinh2File(modSaBan.myCTpara);
            }
        }

        private void btnLoadPara_Click(object sender, EventArgs e)
        {
            this.OpenSaBan();
        }


        public void UpdateDiaHinhFileContent(string oldValue, string newValue)
        {
            //Cách 1:
            //string myCurrentDirectory = string.Format("myCurrentDirectory=\"{0}\"",editPath);
            StreamReader reader = new StreamReader(modSaBan.myDiaHinhDef);
            string str = "";
            while (reader.Peek() >= 0)
            {
                str = reader.ReadToEnd();
                str = str.Replace(oldValue, newValue);
            }
            reader.Close();

            using (FileStream fs = new FileStream(modSaBan.myDiaHinhDef, FileMode.Create, FileAccess.Write))
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
    }
}
