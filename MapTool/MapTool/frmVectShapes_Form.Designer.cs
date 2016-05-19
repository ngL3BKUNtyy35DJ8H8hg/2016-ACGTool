namespace MapTool
{
    partial class frmVectShapes_Form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ucVectShapes_Form1 = new HT.VectorShapes.Forms.ucVectShapes_Form();
            this.ucVectShapeToolBox1 = new HT.VectorShapes.ucVectShapeToolBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ucVectShapes_Form1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucVectShapeToolBox1);
            this.splitContainer1.Size = new System.Drawing.Size(801, 515);
            this.splitContainer1.SplitterDistance = 499;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 38;
            // 
            // ucVectShapes_Form1
            // 
            this.ucVectShapes_Form1.A4 = true;
            this.ucVectShapes_Form1.AllowDrop = true;
            this.ucVectShapes_Form1.BackColor = System.Drawing.SystemColors.Control;
            this.ucVectShapes_Form1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.ucVectShapes_Form1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVectShapes_Form1.dx = 0;
            this.ucVectShapes_Form1.dy = 0;
            this.ucVectShapes_Form1.gridSize = 0;
            this.ucVectShapes_Form1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.ucVectShapes_Form1.Location = new System.Drawing.Point(0, 0);
            this.ucVectShapes_Form1.MapZoom = 1F;
            this.ucVectShapes_Form1.Name = "ucVectShapes_Form1";
            this.ucVectShapes_Form1.ShowDebug = false;
            this.ucVectShapes_Form1.Size = new System.Drawing.Size(499, 515);
            this.ucVectShapes_Form1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.ucVectShapes_Form1.TabIndex = 0;
            this.ucVectShapes_Form1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.ucVectShapes_Form1.Zoom = 1F;
            // 
            // ucVectShapeToolBox1
            // 
            this.ucVectShapeToolBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVectShapeToolBox1.Location = new System.Drawing.Point(0, 0);
            this.ucVectShapeToolBox1.Name = "ucVectShapeToolBox1";
            this.ucVectShapeToolBox1.Size = new System.Drawing.Size(299, 515);
            this.ucVectShapeToolBox1.TabIndex = 0;
            // 
            // frmVectShapes_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 515);
            this.Controls.Add(this.splitContainer1);
            this.Name = "frmVectShapes_Form";
            this.Text = "frmVectShapes_Form";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private HT.VectorShapes.Forms.ucVectShapes_Form ucVectShapes_Form1;
        private HT.VectorShapes.ucVectShapeToolBox ucVectShapeToolBox1;
    }
}