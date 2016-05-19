﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MapTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmCreateBooms());
            //Application.Run(new frmBDTCViewer());
            //Application.Run(new frmMain());
            Application.Run(new frmBDTCShapeViewer());
            //Application.Run(new frmVectShapes_Form());
            
        }
    }
}
