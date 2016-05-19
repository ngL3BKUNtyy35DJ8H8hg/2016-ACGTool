using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapTool
{
    public partial class frmVectShapes_Form : Form
    {
        public frmVectShapes_Form()
        {
            InitializeComponent();
            myInit();
        }

        private void myInit()
        {
            // Links toolBox 2 VectorShape
            this.ucVectShapeToolBox1.setVectShape(this.ucVectShapes_Form1);

        }

        
    }
}
