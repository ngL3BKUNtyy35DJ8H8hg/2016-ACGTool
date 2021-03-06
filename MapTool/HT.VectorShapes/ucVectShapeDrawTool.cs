﻿using System;
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
    public partial class ucVectShapeDrawTool : UserControl
    {
        protected ucVectShapes s;

        public ucVectShapeDrawTool()
        {
            InitializeComponent();
        }

        public void setVectShape(ucVectShapes inS)
        {
            this.s = inS;
            // mi registro all'evento 
            this.s.optionChanged += new VectShapeOptionChanged(OnOptionChange);
            this.s.objectSelected += new ObjectSelected(OnObjectSelected);

        }

        private void OnObjectSelected(object sender, PropertyEventArgs e)
        {
            this.RedoBtn.Enabled = e.Redoable;
            this.UndoBtn.Enabled = e.Undoable;

            // managmet of 2Front,2back,Delete and Copy buttons
            if (e.ele.Length > 0 )
            {
                toolStripButton1.Enabled = true;
                toolStripButton2.Enabled = true;
                toolStripButton3.Enabled = true;
                toolStripButton4.Enabled = true;
                toolStripSplitButton1.Enabled = true;
            }
            else
            {
                toolStripButton1.Enabled = false;
                toolStripButton2.Enabled = false;
                toolStripButton3.Enabled = false;
                toolStripButton4.Enabled = false;
                toolStripSplitButton1.Enabled = false;
            }
            if (e.ele.Length > 1 )
                GroupBtn.Enabled=true;
            else
                GroupBtn.Enabled = false;
        }

        private void OnOptionChange(object sender, VectShapeOptionEventArgs e)
        { 
            if (e.option == enumDrawingOption.NONE)
            {
                SelectBtn.PerformClick();
            }
        }

        /// <summary>
        /// 2014-11-15:
        /// Cập nhật trạng thái
        /// </summary>
        private void deselectAll(object sender)
        {
            SelectBtn.Checked = false;
            RectBtn.Checked = false;
            RRectBtn.Checked = false;
            CirciBtn.Checked = false;
            LineBtn.Checked = false;
            ImageBtn.Checked = false;
            ArcBtn.Checked = false;
            PolyBtn.Checked = false;
            CurveBtn.Checked = false;
            GraphBtn.Checked = false;

            textMnu.BackColor = RRectBtn.BackColor;
            RTFBtn.Checked = false;
            SimpleTextBtn.Checked = false;

            //=============================================
            //2014-11-15:
            if (sender is ToolStripButton)
                ((ToolStripButton)sender).Checked = true;
           
            this.s.ChangeCursor();
            //=============================================
        }

        #region "Select a shape to draw"

        private void SelectBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            Helper.ResetStatus(enumActionStatus.SELSHAPE);
        }

        private void RectBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            Helper.ResetStatus(enumDrawingOption.DR);
        }

        private void LineBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            Helper.ResetStatus(enumDrawingOption.LI);
        }

        private void CirciBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            Helper.ResetStatus(enumDrawingOption.ELL);
        }

        private void RRectBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            Helper.ResetStatus(enumDrawingOption.DRR);
        }


        private void ImageBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            Helper.ResetStatus(enumDrawingOption.IB);
        }

        private void ArcBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            Helper.ResetStatus(enumDrawingOption.ARC);

        }

        private void PolyBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            Helper.ResetStatus(enumDrawingOption.POLY);
        }

        private void CurveBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            Helper.ResetStatus(enumDrawingOption.PEN);
        }

        private void SimpleTextBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            textMnu.BackColor = Color.LightBlue;
            Helper.ResetStatus(enumDrawingOption.STB);
        }

        private void RTFBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            textMnu.BackColor = Color.LightBlue;
            Helper.ResetStatus(enumDrawingOption.TB);
        }

        private void GraphBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            Helper.ResetStatus(enumDrawingOption.GRAPH);
        }

        private void NewPolyBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            Helper.ResetStatus(enumDrawingOption.NEWPOLY);
        }

        private void RedimBtn_Click(object sender, EventArgs e)
        {
            deselectAll(sender);
            if (Helper.ActionStatus == enumActionStatus.REDIMSHAPE)
            {
                RedimBtn.Checked = false;
                SelectBtn.PerformClick();
            }
            else
            {
                RedimBtn.Checked = true;
                Helper.ResetStatus(enumActionStatus.REDIMSHAPE);
            }
            s.Refresh();
        }
        #endregion


        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (s.Saver()) { }
        }

        private void LoadBtn_Click(object sender, EventArgs e)
        {
            if (s.Loader()) { }
        }

        private void PreviewBtn_Click(object sender, EventArgs e)
        {
            this.s.anteprimaStampa(1f);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.s.gridSize = 0;
            this.s.Refresh();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.s.gridSize = System.Convert.ToInt16(toolStripMenuItem3.Text);
            this.s.Refresh();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            this.s.gridSize = System.Convert.ToInt16(toolStripMenuItem4.Text);
            this.s.Refresh();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.s.gridSize = System.Convert.ToInt16(toolStripMenuItem5.Text);
            this.s.Refresh();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.s.gridSize = System.Convert.ToInt16(toolStripMenuItem6.Text);
            this.s.Refresh();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            this.s.gridSize = System.Convert.ToInt16(toolStripMenuItem7.Text);
            this.s.Refresh();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            this.s.gridSize = System.Convert.ToInt16(toolStripMenuItem8.Text);
            this.s.Refresh();
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            this.s.gridSize = System.Convert.ToInt16(toolStripMenuItem9.Text);
            this.s.Refresh();
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            
            if (toolStripTextBox1.Text != "")
            {
                foreach (char c in toolStripTextBox1.Text)
                {
                    if (!Char.IsNumber(c))
                        return;
                }
                this.s.gridSize = System.Convert.ToInt16(toolStripTextBox1.Text);
                this.s.Refresh();
            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.s.primoPiano();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.s.secondoPiano();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.s.rmSelected();
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            if (s.Loader()) { }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (s.Saver()) { }
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            this.s.anteprimaStampa(1f);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.s.Stampa();
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.s.cpSelected();
        }

        private void UndoBtn_Click(object sender, EventArgs e)
        {
            this.s.Undo();
            this.RedoBtn.Enabled = this.s.RedoEnabled();
            this.UndoBtn.Enabled = this.s.UndoEnabled();
        }

        private void RedoBtn_Click(object sender, EventArgs e)
        {
            this.s.Redo();
            this.RedoBtn.Enabled = this.s.RedoEnabled();
            this.UndoBtn.Enabled = this.s.UndoEnabled();

        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            if (s.LoadObj()) { }
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {
            if (s.SaveSelected()) { }
        }

        private void objToolStripMenuItem_Click(object sender, EventArgs e)
        {
            s.groupSelected();
            //this.s.Refresh();

        }

        private void deGroupBtn_Click(object sender, EventArgs e)
        {
            s.deGroupSelected();
            //this.s.Refresh();

        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            this.s.mergePolygons();
        }

        private void delPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.s.delPoints();
        }

        private void extPointsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.s.extPoints();
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.s.XMirror();
        }

        private void yToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.s.YMirror();
        }

        private void xYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.s.Mirror();
        }

        private void addArcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.s.linkNodes();
        }

        private void delNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.s.delNodes();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
        
    }
}
