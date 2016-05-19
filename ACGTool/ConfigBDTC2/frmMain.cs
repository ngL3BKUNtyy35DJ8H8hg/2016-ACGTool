using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConfigBDTC
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void buildDiaHinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBuildDiaHinh frm = new frmBuildDiaHinh();
            frm.MdiParent = this;
            frm.Show();
        }

        private void configDiaHinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfigDiaHinhFile frm = new frmConfigDiaHinhFile();
            frm.MdiParent = this;
            frm.Show();
        }

        private void bDTCActionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BDTCAction frm = new BDTCAction();
            frm.MdiParent = this;
            frm.Show();
        }

        private void bDTCTimeLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BDTCTimeLine frm = new BDTCTimeLine();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
