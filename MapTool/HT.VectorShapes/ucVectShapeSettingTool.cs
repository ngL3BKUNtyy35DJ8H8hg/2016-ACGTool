using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HT.VectorShapes.Tools;

namespace HT.VectorShapes
{
    public partial class ucVectShapeSettingTool : UserControl
    {
        protected ucVectShapes s;

        public ucVectShapeSettingTool()
        {
            InitializeComponent();
        }

        public void setVectShape(ucVectShapes inS)
        {
            this.s = inS;
        }

        private void PenColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = PenColor.BackColor;
            colorDialog1.ShowDialog(this);
            PenColor.BackColor = colorDialog1.Color;

            // add code
            this.s.setPenColor(PenColor.BackColor);
            
            this.s.Refresh();

        }

        private void FillColor_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = FillColor.BackColor;
            colorDialog1.ShowDialog(this);
            FillColor.BackColor = colorDialog1.Color;

            // add code
            this.s.setFillColor(FillColor.BackColor);


            this.s.Refresh();

        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            this.s.setPenWidth((float)System.Convert.ToDouble(toolStripMenuItem10.Text));
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            this.s.setPenWidth((float)System.Convert.ToDouble(toolStripMenuItem11.Text));
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            this.s.setPenWidth((float)System.Convert.ToDouble(toolStripMenuItem12.Text));
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            this.s.setPenWidth((float)System.Convert.ToDouble(toolStripMenuItem13.Text));
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            this.s.setPenWidth((float)System.Convert.ToDouble(toolStripMenuItem14.Text));
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {
            this.s.setPenWidth((float)System.Convert.ToDouble(toolStripMenuItem15.Text));
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            this.s.setPenWidth((float)System.Convert.ToDouble(toolStripMenuItem16.Text));
        }


        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            this.s.setFilled(toolStripMenuItem17.Checked);
        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (toolStripTextBox2.Text != "")
            {
                foreach (char c in toolStripTextBox2.Text)
                {
                    if (!Char.IsNumber(c))
                        return;
                }
                this.s.setPenWidth((float)System.Convert.ToDouble(toolStripTextBox2.Text));
                this.s.Refresh();
            }

        }

        private void iNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.s.zoomIn();

        }

        private void oUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.s.zoomOut();
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            this.s.HelpForm("");
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            this.s.Zoom = (float)System.Convert.ToDouble(toolStripMenuItem21.Text, provider);
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            this.s.Zoom = (float)System.Convert.ToDouble(toolStripMenuItem22.Text, provider);
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            this.s.Zoom = (float)System.Convert.ToDouble(toolStripMenuItem23.Text, provider);

        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            this.s.Zoom = (float)System.Convert.ToDouble(toolStripMenuItem24.Text, provider);
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            this.s.Zoom = (float)System.Convert.ToDouble(toolStripMenuItem25.Text, provider);
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            this.s.Zoom = (float)System.Convert.ToDouble(toolStripMenuItem26.Text, provider);
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            this.s.Zoom = (float)System.Convert.ToDouble(toolStripMenuItem27.Text, provider);
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            this.s.Zoom = (float)System.Convert.ToDouble(toolStripMenuItem28.Text, provider);
        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e)
        {
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            this.s.Zoom = (float)System.Convert.ToDouble(toolStripMenuItem29.Text, provider);
        }

        private void toolStripMenuItem30_Click(object sender, EventArgs e)
        {
            System.Globalization.NumberFormatInfo provider = new System.Globalization.NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            this.s.Zoom = (float)System.Convert.ToDouble(toolStripMenuItem30.Text, provider);
        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            this.s.HelpForm("You can also set zoom property clicking the canvas and changing the property grid (Zoom).");
        }

        private void toolStripDropDownButton2_Click_1(object sender, EventArgs e)
        {

        }

    }
}
