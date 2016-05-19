using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;

namespace ACGTool
{
    public partial class ViewPhysicalDevice : Form
    {
        public ViewPhysicalDevice()
        {
            InitializeComponent();
        }

        public string GetPhysicalDevicePath(char DriveLetter)
        {
            ManagementClass devs = new ManagementClass(@"Win32_Diskdrive");
            {
                ManagementObjectCollection moc = devs.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    foreach (ManagementObject b in mo.GetRelated("Win32_DiskPartition"))
                    {
                        foreach (ManagementBaseObject c in b.GetRelated("Win32_LogicalDisk"))
                        {
                            string DevName = string.Format("{0}", c["Name"]);
                            if (DevName[0] == DriveLetter)
                                return string.Format("{0}", mo["DeviceId"]);
                        }
                    }
                }
            }
            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = GetPhysicalDevicePath(textBox1.Text.ToCharArray()[0]);
        }
    }
}
