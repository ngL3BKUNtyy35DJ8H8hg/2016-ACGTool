using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BDTCLib
{
    public class MyGridDataFile : BaseMyFile
    {
        public int myGRID_HEIGHT;
        public int myGRID_WIDTH;
        public List<string> LatitudeGridValues = new List<string>();
        public List<string> LongitudeGridValues = new List<string>();
        public Dictionary<string, string> AltitudeGridValues = new Dictionary<string, string>();

        public MyGridDataFile(string key, string value)
            : base(key, value)
        {
            
        }

        public MyGridDataFile()
        {

        }

        /// <summary>
        /// Đọc file xyz
        /// </summary>
        /// <param name="filepath"></param>
        public void LoadXYZFile(string filepath)
        {
            FileInfo File = new FileInfo(filepath);
            string line;

            // Read the file and display it line by line.
            StreamReader file = new System.IO.StreamReader(filepath);

            while ((line = file.ReadLine()) != null)
            {
                string[] values = line.Split(new char[] { ',' });
                if (values.Length == 3)
                {
                    if (!LongitudeGridValues.Contains(values[0]))
                        LongitudeGridValues.Add(values[0]);
                    if (!LatitudeGridValues.Contains(values[1]))
                        LatitudeGridValues.Add(values[1]);
                    if (!AltitudeGridValues.Keys.Contains(line))
                        AltitudeGridValues.Add(line, values[2]);
                }
            }

            myGRID_WIDTH = LongitudeGridValues.Count;
            myGRID_HEIGHT = LatitudeGridValues.Count;
            file.Close();

        }

        public void BindDataGrid_ListView(ListView listViewXYZ)
        {
            listViewXYZ.Items.Clear();
            ListViewItem item = new ListViewItem("XYZ File");
            item.SubItems.Add(PATH_FILE);
            if (!File.Exists(FULLPATH_FILE))
            {
                item.SubItems.Add("Không tồn tại");
                item.ForeColor = Color.Red;
                listViewXYZ.Items.Add(item);
            }
            listViewXYZ.Items.Add(item);

            LoadXYZFile(FULLPATH_FILE);

            item = new ListViewItem("myGRID_WIDTH");
            item.Name = "myGRID_WIDTH";
            item.SubItems.Add(myGRID_WIDTH.ToString());
            item.SubItems.Add("");
            listViewXYZ.Items.Add(item);

            item = new ListViewItem("myGRID_HEIGHT");
            item.Name = "myGRID_HEIGHT";
            item.SubItems.Add(myGRID_HEIGHT.ToString());
            item.SubItems.Add("");
            listViewXYZ.Items.Add(item);

            item = new ListViewItem("Left Lon");
            item.Name = "LeftLon";
            item.SubItems.Add(LongitudeGridValues[0]);
            item.SubItems.Add("");
            listViewXYZ.Items.Add(item);

            item = new ListViewItem("Top Lat");
            item.Name = "TopLat";
            item.SubItems.Add(LatitudeGridValues[0]);
            item.SubItems.Add("");
            listViewXYZ.Items.Add(item);

            item = new ListViewItem("Right Lon");
            item.Name = "RightLon";
            item.SubItems.Add(LongitudeGridValues[LongitudeGridValues.Count - 1]);
            item.SubItems.Add("");
            listViewXYZ.Items.Add(item);

            item = new ListViewItem("Bottom Lat");
            item.Name = "BottomLat";
            item.SubItems.Add(LatitudeGridValues[LatitudeGridValues.Count - 1]);
            item.SubItems.Add("");
            listViewXYZ.Items.Add(item);
        }
    }
}
