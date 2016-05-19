using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ConfigBDTC
{
    public partial class splash : Form
    {
        public splash()
        {
            InitializeComponent();
        }

        private void splash_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            progressBar1.Maximum = 8;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Step = 1;
                progressBar1.PerformStep();
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        public void CloseSplash() 
        { 
            Invoke((MethodInvoker)delegate 
            { this.Close(); }); 
        }
    }
}
