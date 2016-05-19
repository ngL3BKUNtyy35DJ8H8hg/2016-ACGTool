using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Xml.Serialization;
using System.Xml;
using System.Web;
using ACGTool.Classes;

namespace ACGTool
{
    public partial class frmRebuildBDTC : Form
    {

        XmlDocument myConfigFileDocument;
        //XmlDocument myXmlDocument;

        string myCurrentDirectory;
        string myD3DModelMeshFile;

        public frmRebuildBDTC()
        {
            InitializeComponent();
        }

        private void btnRebuild_Click(object sender, EventArgs e)
        {
            string fileName = "test.txt";
            string sourcePath = @"C:\Users\Public\TestFolder";
            string targetPath = @"C:\Users\Public\TestFolder\SubDir";

            // Use Path class to manipulate file and directory paths.
            string sourceFile = System.IO.Path.Combine(sourcePath, fileName);
            string destFile = System.IO.Path.Combine(targetPath, fileName);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(targetPath))
            {
                System.IO.Directory.CreateDirectory(targetPath);
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(sourceFile, destFile, true);

            // To copy all the files in one directory to another directory.
            // Get the files in the source folder. (To recursively iterate through
            // all subfolders under the current directory, see
            // "How to: Iterate Through a Directory Tree.")
            // Note: Check for target path was performed previously
            //       in this code example.
            if (System.IO.Directory.Exists(sourcePath))
            {
                string[] files = System.IO.Directory.GetFiles(sourcePath);

                // Copy the files and overwrite destination files if they already exist.
                foreach (string s in files)
                {
                    // Use static Path methods to extract only the file name from the path.
                    fileName = System.IO.Path.GetFileName(s);
                    destFile = System.IO.Path.Combine(targetPath, fileName);
                    System.IO.File.Copy(s, destFile, true);
                }
            }
            else
            {
                Console.WriteLine("Source path does not exist!");
            }

            // Keep console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            //mo form cho nguoi ta chon co so du lieu
            OpenFileDialog1.Filter = "Config File(*.diahinh)|*.diahinh;|All files (*.*)|*.*";
            txtFilePath.Text = "";
            OpenFileDialog1.ShowDialog();
            txtFilePath.Text = OpenFileDialog1.FileName;
        }

        private void btnLoadFile_Click(object sender, EventArgs e)
        {
            //string tempFile = "temp.xml";
            if (txtFilePath.Text == string.Empty)
                return;

            myConfigFileDocument = new XmlDocument();
            myConfigFileDocument.Load(txtFilePath.Text);

            XmlNode node;
            node = myConfigFileDocument.DocumentElement;
            richTextBoxConfig.Text = node.InnerXml;
            foreach (XmlNode node1 in node.ChildNodes)
            {
                if (node1.Name == "CT")
                {
                    foreach (XmlAttribute att in node1.Attributes)
                    {
                        if (att.Name == "myCurrentDirectory")
                        {
                            myCurrentDirectory = att.Value;
                        }
                        else if (att.Name == "myD3DModelMeshFile")
                        {
                            myD3DModelMeshFile = att.Value;
                        }
                    }
                }
                else if (node1.Name == "BDTC")
                {

                }
                else if (node1.Name == "SABAN")
                {

                }
            }
        }
    }
}