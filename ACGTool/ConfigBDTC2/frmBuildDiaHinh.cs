using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BDTCLib;
using FolderLib;

namespace ConfigBDTC
{
    public partial class frmBuildDiaHinh : Form
    {
        public frmBuildDiaHinh()
        {
            InitializeComponent();
        }

        private void btPathBrowse_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtFilePath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btImageFileBrowse_Click(object sender, EventArgs e)
        {
            //mo form cho nguoi ta chon co so du lieu
            OpenFileDialog1.Filter = "Image File(*.jpg)|*.jpg";
            txtImageFile.Text = "";
            OpenFileDialog1.ShowDialog();
            txtImageFile.Text = OpenFileDialog1.FileName;
        }

        private void btnXYZFileBrowse_Click(object sender, EventArgs e)
        {
            //mo form cho nguoi ta chon co so du lieu
            OpenFileDialog1.Filter = "XYZ File(*.xyz)|*.xyz";
            txtXYZFile.Text = "";
            OpenFileDialog1.ShowDialog();
            txtXYZFile.Text = OpenFileDialog1.FileName;
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            //Read Image File
            Bitmap imageInfo = new Bitmap(txtImageFile.Text);    
            
            //Read XYZ File
            MyGridDataFile gridDataFile = new MyGridDataFile();
            gridDataFile.LoadXYZFile(txtXYZFile.Text);

            //================================================================
            //Copy thư mục Resources sang
            string SrcResourcesPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\RESOURCES";
            string DesResourcesPath = txtFilePath.Text + @"\" + txtSaBanName.Text;
            if (Directory.Exists(DesResourcesPath))
            {
                MessageBox.Show(string.Format("Đường dẫn thư mục {0} đã tồn tại", DesResourcesPath));
                return;
            }
            FileProcessor.DirectoryCopy(SrcResourcesPath, DesResourcesPath, true);

            //================================================================
            //Copy thư mục template sang
            string SrcFolderPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\TEMPLATE";
            string DesFolderPath = txtFilePath.Text + @"\" + txtSaBanName.Text + @"\" + txtSaBanName.Text;
            if (Directory.Exists(DesFolderPath))
            {
                MessageBox.Show(string.Format("Đường dẫn thư mục {0} đã tồn tại", DesFolderPath));
                return;
            }
            FileProcessor.DirectoryCopy(SrcFolderPath, DesFolderPath, true);

            //Copy Image File
            FileInfo imageFile = new FileInfo(txtImageFile.Text);
            imageFile.CopyTo(DesFolderPath + "\\BanDo\\" + imageFile.Name, true);
            //Copy XYZ File
            FileInfo xyzFile = new FileInfo(txtXYZFile.Text);
            xyzFile.CopyTo(DesFolderPath + "\\BanDo\\" + xyzFile.Name, true);

            //================================================================
            //Update .diahinh
            string diaHinhFile = DesFolderPath + "\\Config.diahinh";
            StreamReader reader = new StreamReader(diaHinhFile);
            string diaHinhFileContent = "";
            while (reader.Peek() >= 0)
            {
                diaHinhFileContent = reader.ReadToEnd();
                //Replace
                diaHinhFileContent = diaHinhFileContent.Replace("[FOLDER_PATH]", DesFolderPath.Replace(@"\\",@"\"));

                diaHinhFileContent = diaHinhFileContent.Replace("[IMAGE_FILE]", imageFile.Name);

                diaHinhFileContent = diaHinhFileContent.Replace("[LEFT_LON]", gridDataFile.LongitudeGridValues[0]);

                diaHinhFileContent = diaHinhFileContent.Replace("[RIGHT_LON]", gridDataFile.LongitudeGridValues[gridDataFile.LongitudeGridValues.Count-1]);

                diaHinhFileContent = diaHinhFileContent.Replace("[TOP_LAT]", gridDataFile.LatitudeGridValues[0]);

                diaHinhFileContent = diaHinhFileContent.Replace("[BOTTOM_LAT]", gridDataFile.LatitudeGridValues[gridDataFile.LatitudeGridValues.Count - 1]);

                diaHinhFileContent = diaHinhFileContent.Replace("[XYZ_FILE]", xyzFile.Name);

                diaHinhFileContent = diaHinhFileContent.Replace("[GRID_WIDTH]", (gridDataFile.myGRID_WIDTH - 1).ToString());

                diaHinhFileContent = diaHinhFileContent.Replace("[GRID_HEIGHT]", (gridDataFile.myGRID_HEIGHT - 1).ToString());
                
            }
            reader.Close();

            using (FileStream fs = new FileStream(diaHinhFile, FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(diaHinhFileContent);
            }
            

            //================================================================
            //Update .tab
            string tabFile = DesFolderPath + "\\BanDo\\BanDo.tab";
            reader = new StreamReader(tabFile);
            string tabFileContent = "";
            while (reader.Peek() >= 0)
            {
                tabFileContent = reader.ReadToEnd();

                //Replace
                tabFileContent = tabFileContent.Replace("[IMAGE_FILE]", imageFile.Name);

                tabFileContent = tabFileContent.Replace("[LEFT_LON]", gridDataFile.LongitudeGridValues[0]);

                tabFileContent = tabFileContent.Replace("[RIGHT_LON]", gridDataFile.LongitudeGridValues[gridDataFile.LongitudeGridValues.Count - 1]);

                tabFileContent = tabFileContent.Replace("[TOP_LAT]", gridDataFile.LatitudeGridValues[0]);

                tabFileContent = tabFileContent.Replace("[BOTTOM_LAT]", gridDataFile.LatitudeGridValues[gridDataFile.LatitudeGridValues.Count - 1]);

                tabFileContent = tabFileContent.Replace("[IMAGE_WIDTH]", (imageInfo.Width - 1).ToString());

                tabFileContent = tabFileContent.Replace("[IMAGE_HEIGTH]", (imageInfo.Height - 1).ToString());

            }
            reader.Close();

            using (FileStream fs = new FileStream(tabFile, FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(tabFileContent);
            }

            //================================================================
            //Update .gst
            string gstFile = DesFolderPath + "\\BanDo\\BanDo.gst";
            reader = new StreamReader(gstFile);
            string gstFileContent = "";
            while (reader.Peek() >= 0)
            {
                gstFileContent = reader.ReadToEnd();
                //Replace
                gstFileContent = gstFileContent.Replace("[LEFT_LON]", gridDataFile.LongitudeGridValues[0]);
                gstFileContent = gstFileContent.Replace("[TOP_LAT]", gridDataFile.LatitudeGridValues[0]);
            }
            reader.Close();

            using (FileStream fs = new FileStream(gstFile, FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(gstFileContent);
            }

            //================================================================
            //Update .bdtc
            string bdtcFile = DesFolderPath + "\\New.bdtc";
            reader = new StreamReader(bdtcFile);
            string bdtcFileContent = "";
            while (reader.Peek() >= 0)
            {
                bdtcFileContent = reader.ReadToEnd();
                //Replace
                bdtcFileContent = bdtcFileContent.Replace("[LEFT_LON]", gridDataFile.LongitudeGridValues[0]);
                bdtcFileContent = bdtcFileContent.Replace("[TOP_LAT]", gridDataFile.LatitudeGridValues[0]);
            }
            reader.Close();

            using (FileStream fs = new FileStream(bdtcFile, FileMode.Create, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                sw.WriteLine(bdtcFileContent);
            }

            MessageBox.Show("Đã tạo thành công!");
        }

        
    }
}
