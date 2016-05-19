using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACGTool
{
    public partial class frmReadXYZ : Form
    {
        public frmReadXYZ()
        {
            InitializeComponent();
            toolStripProgressBar1.Visible = false;
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                //mo form cho nguoi ta chon co so du lieu
                OpenFileDialog1.Filter = "XYZ File(*.xyz)|*.xyz";
                txtFilePath.Text = "";
                OpenFileDialog1.ShowDialog();
                txtFilePath.Text = OpenFileDialog1.FileName;
                ReadXYZFile(txtFilePath.Text);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private List<string> latitudeGridValues = new List<string>();
        private List<string> longitudeGridValues = new List<string>();
        private Dictionary<string, string> altitudeGridValues = new Dictionary<string, string>();
        private void ReadXYZFile(string filepath)
        {
            FileInfo File = new FileInfo(filepath);
            richTextBoxView.Text = File.OpenText().ReadToEnd();

            int counter = 0;
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(filepath);
            

            while ((line = file.ReadLine()) != null)
            {
                string[] values = line.Split(new char[] {','});
                if (values.Length == 3)
                {
                    if (!longitudeGridValues.Contains(values[0]))
                        longitudeGridValues.Add(values[0]);
                    if (!latitudeGridValues.Contains(values[1]))
                        latitudeGridValues.Add(values[1]);
                    if (!altitudeGridValues.Keys.Contains(line))
                        altitudeGridValues.Add(line, values[2]);
                }
            }
            labelInfo.Text = string.Format("GRID_WIDTH = {0} GRID_HEIGHT = {1}", longitudeGridValues.Count,
                                           latitudeGridValues.Count);

            toolStripProgressBar1.Maximum = 100;
            file.Close();

        }

        delegate void SetValueCallback(Control ctrl, string text);

        private void SetValue(Control ctrl, string text)
        {
            if (ctrl.InvokeRequired)
            {
                ctrl.BeginInvoke(new SetValueCallback(SetValue), ctrl, text);
            }
            else
            {
                richTextBox1.Text += text;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            int i = 1;
            int total = altitudeGridValues.Count;
            //for (int i = 0; i < total; i++)
            //{
            //    KeyValuePair<string, string> altitudeGridValue = altitudeGridValues[]
            //    double value = double.Parse(altitudeGridValues[i].Value);
            //    string valueStr = altitudeGridValues.Keys[].Replace(string.Format(",{0}", altitudeGridValues[i].Value), string.Format(",{0}\n", value.ToString("#.##0")));
            //    SetValue(richTextBox1, valueStr);
            //    int percent = (int)((float)i / total) * 10;
            //    backgroundWorker1.ReportProgress(percent * 10);
                
            //}
            foreach (var altitudeGridValue in altitudeGridValues)
            {
                double value = double.Parse(altitudeGridValue.Value);
                string valueStr = altitudeGridValue.Key.Replace(string.Format(",{0}", altitudeGridValue.Value), string.Format(",{0}\n", value.ToString("#.##0")));
                SetValue(richTextBox1, valueStr);
                int percent = (int)((float)i / total) * 10;
                backgroundWorker1.ReportProgress(percent * 10);
                i++;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            toolStripProgressBar1.Visible = false;
        }

        private void btnFormatGrid_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
            btnCancel.Visible = true;
            toolStripProgressBar1.Visible = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
            btnCancel.Visible = false;
        }

        private void txtFilePath_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}
