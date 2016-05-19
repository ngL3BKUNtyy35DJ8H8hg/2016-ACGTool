namespace ConfigBDTC
{
    partial class BDTCTimeLine
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
            this.panel8 = new System.Windows.Forms.Panel();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.btBrowse = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainerMyMnu = new System.Windows.Forms.SplitContainer();
            this.splitContainerMyMnuTreeView = new System.Windows.Forms.SplitContainer();
            this.chartMyMnu = new Braincase.GanttChart.Chart();
            this.richTextBoxMyMnuLog = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.splitContainerActions = new System.Windows.Forms.SplitContainer();
            this._mChart = new Braincase.GanttChart.Chart();
            this.richTextBoxActionLog = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPageBDTCActions = new System.Windows.Forms.TabPage();
            this.panel8.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMyMnu)).BeginInit();
            this.splitContainerMyMnu.Panel1.SuspendLayout();
            this.splitContainerMyMnu.Panel2.SuspendLayout();
            this.splitContainerMyMnu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMyMnuTreeView)).BeginInit();
            this.splitContainerMyMnuTreeView.Panel2.SuspendLayout();
            this.splitContainerMyMnuTreeView.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerActions)).BeginInit();
            this.splitContainerActions.Panel2.SuspendLayout();
            this.splitContainerActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.Label2);
            this.panel8.Controls.Add(this.txtFilePath);
            this.panel8.Controls.Add(this.btnReload);
            this.panel8.Controls.Add(this.btBrowse);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(831, 46);
            this.panel8.TabIndex = 2;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Location = new System.Drawing.Point(10, 15);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(99, 13);
            this.Label2.TabIndex = 18;
            this.Label2.Text = "Choose .diahinh file";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(115, 12);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(595, 20);
            this.txtFilePath.TabIndex = 19;
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReload.Location = new System.Drawing.Point(760, 10);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(67, 23);
            this.btnReload.TabIndex = 20;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // btBrowse
            // 
            this.btBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowse.Location = new System.Drawing.Point(716, 10);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(38, 23);
            this.btBrowse.TabIndex = 20;
            this.btBrowse.Text = "...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPageBDTCActions);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 46);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(831, 454);
            this.tabControl1.TabIndex = 19;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainerMyMnu);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(823, 428);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "MyMnu";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainerMyMnu
            // 
            this.splitContainerMyMnu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMyMnu.Location = new System.Drawing.Point(3, 3);
            this.splitContainerMyMnu.Name = "splitContainerMyMnu";
            this.splitContainerMyMnu.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMyMnu.Panel1
            // 
            this.splitContainerMyMnu.Panel1.Controls.Add(this.splitContainerMyMnuTreeView);
            // 
            // splitContainerMyMnu.Panel2
            // 
            this.splitContainerMyMnu.Panel2.Controls.Add(this.richTextBoxMyMnuLog);
            this.splitContainerMyMnu.Size = new System.Drawing.Size(817, 422);
            this.splitContainerMyMnu.SplitterDistance = 272;
            this.splitContainerMyMnu.TabIndex = 0;
            // 
            // splitContainerMyMnuTreeView
            // 
            this.splitContainerMyMnuTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMyMnuTreeView.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMyMnuTreeView.Name = "splitContainerMyMnuTreeView";
            // 
            // splitContainerMyMnuTreeView.Panel2
            // 
            this.splitContainerMyMnuTreeView.Panel2.Controls.Add(this.chartMyMnu);
            this.splitContainerMyMnuTreeView.Size = new System.Drawing.Size(817, 272);
            this.splitContainerMyMnuTreeView.SplitterDistance = 433;
            this.splitContainerMyMnuTreeView.TabIndex = 0;
            // 
            // chartMyMnu
            // 
            this.chartMyMnu.AllowTaskDragDrop = false;
            this.chartMyMnu.BarSpacing = 46;
            this.chartMyMnu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartMyMnu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chartMyMnu.Location = new System.Drawing.Point(0, 0);
            this.chartMyMnu.Margin = new System.Windows.Forms.Padding(0);
            this.chartMyMnu.Name = "chartMyMnu";
            this.chartMyMnu.Padding = new System.Windows.Forms.Padding(5);
            this.chartMyMnu.Size = new System.Drawing.Size(380, 272);
            this.chartMyMnu.TabIndex = 6;
            // 
            // richTextBoxMyMnuLog
            // 
            this.richTextBoxMyMnuLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxMyMnuLog.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxMyMnuLog.HideSelection = false;
            this.richTextBoxMyMnuLog.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxMyMnuLog.Name = "richTextBoxMyMnuLog";
            this.richTextBoxMyMnuLog.Size = new System.Drawing.Size(817, 146);
            this.richTextBoxMyMnuLog.TabIndex = 41;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.splitContainer3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(823, 428);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Script Files";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainerActions);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.richTextBoxActionLog);
            this.splitContainer3.Size = new System.Drawing.Size(817, 422);
            this.splitContainer3.SplitterDistance = 355;
            this.splitContainer3.TabIndex = 1;
            // 
            // splitContainerActions
            // 
            this.splitContainerActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerActions.Location = new System.Drawing.Point(0, 0);
            this.splitContainerActions.Name = "splitContainerActions";
            // 
            // splitContainerActions.Panel2
            // 
            this.splitContainerActions.Panel2.Controls.Add(this._mChart);
            this.splitContainerActions.Size = new System.Drawing.Size(817, 355);
            this.splitContainerActions.SplitterDistance = 422;
            this.splitContainerActions.TabIndex = 1;
            // 
            // _mChart
            // 
            this._mChart.AllowTaskDragDrop = false;
            this._mChart.BarSpacing = 46;
            this._mChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mChart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._mChart.Location = new System.Drawing.Point(0, 0);
            this._mChart.Margin = new System.Windows.Forms.Padding(0);
            this._mChart.Name = "_mChart";
            this._mChart.Padding = new System.Windows.Forms.Padding(5);
            this._mChart.Size = new System.Drawing.Size(391, 355);
            this._mChart.TabIndex = 5;
            // 
            // richTextBoxActionLog
            // 
            this.richTextBoxActionLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxActionLog.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxActionLog.ForeColor = System.Drawing.Color.Red;
            this.richTextBoxActionLog.HideSelection = false;
            this.richTextBoxActionLog.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxActionLog.Name = "richTextBoxActionLog";
            this.richTextBoxActionLog.Size = new System.Drawing.Size(817, 63);
            this.richTextBoxActionLog.TabIndex = 41;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(823, 428);
            this.tabPage4.TabIndex = 4;
            this.tabPage4.Text = "Edit Action";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPageBDTCActions
            // 
            this.tabPageBDTCActions.Location = new System.Drawing.Point(4, 22);
            this.tabPageBDTCActions.Name = "tabPageBDTCActions";
            this.tabPageBDTCActions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBDTCActions.Size = new System.Drawing.Size(823, 428);
            this.tabPageBDTCActions.TabIndex = 5;
            this.tabPageBDTCActions.Text = "BDTC Actions";
            this.tabPageBDTCActions.UseVisualStyleBackColor = true;
            // 
            // BDTCTimeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 500);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel8);
            this.Name = "BDTCTimeLine";
            this.Text = "BDTC Timeline";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainerMyMnu.Panel1.ResumeLayout(false);
            this.splitContainerMyMnu.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMyMnu)).EndInit();
            this.splitContainerMyMnu.ResumeLayout(false);
            this.splitContainerMyMnuTreeView.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMyMnuTreeView)).EndInit();
            this.splitContainerMyMnuTreeView.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainerActions.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerActions)).EndInit();
            this.splitContainerActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel8;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtFilePath;
        internal System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer splitContainerMyMnu;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPageBDTCActions;
        private System.Windows.Forms.RichTextBox richTextBoxMyMnuLog;
        internal System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.SplitContainer splitContainerActions;
        private Braincase.GanttChart.Chart _mChart;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.RichTextBox richTextBoxActionLog;
        private System.Windows.Forms.SplitContainer splitContainerMyMnuTreeView;
        private Braincase.GanttChart.Chart chartMyMnu;
    }
}

