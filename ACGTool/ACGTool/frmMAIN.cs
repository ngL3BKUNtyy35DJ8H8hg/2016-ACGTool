using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ACGTool
{
    public partial class frmMAIN : Form
    {
        public frmMAIN()
        {
            InitializeComponent();
        }

        private void aCGToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ACGMain frm = new ACGMain();
            frm.MdiParent = this;
            frm.Show();
        }

        private void bDTCToolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BDTCTool frm = new BDTCTool();
            frm.MdiParent = this;
            frm.Show();
        }

        private void rebuildBDTCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRebuildBDTC frm = new frmRebuildBDTC();
            frm.MdiParent = this;
            frm.Show();
        }

        private void copyBDTCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCopyPasteBDTC frm = new frmCopyPasteBDTC();
            frm.MdiParent = this;
            frm.Show();
        }


        private void powerDesignerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PowerDesigner frm = new PowerDesigner();
            frm.MdiParent = this;
            frm.Show();
        }

        private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPDF frm = new frmPDF();
            frm.ShowDialog();
        }

        private void featuresToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bDTCActionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BDTCAction frm = new BDTCAction();
            frm.MdiParent = this;
            frm.Show();
        }

        private void readXYZFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReadXYZ frm = new frmReadXYZ();
            frm.MdiParent = this;
            frm.Show();
        }

        private void readmdbFilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReadAccess frm = new frmReadAccess();
            frm.MdiParent = this;
            frm.Show();
        }

        private void repositoryCodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmACG frm = new frmACG();
            frm.MdiParent = this;
            frm.Show();
        }

        private void decompilerToCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Decompiler frm = new Decompiler();
            frm.MdiParent = this;
            frm.Show();
        }

        private void viewPhysicalDeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewPhysicalDevice frm = new ViewPhysicalDevice();
            frm.ShowDialog();
        }
    }
}