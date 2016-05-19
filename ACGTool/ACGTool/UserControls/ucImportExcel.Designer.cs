namespace ACGTool.UserControls
{
    partial class ucImportExcel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.comboBoxSheet = new System.Windows.Forms.ComboBox();
            this.txtColumnStart = new System.Windows.Forms.TextBox();
            this.txtColumnCount = new System.Windows.Forms.TextBox();
            this.txtRowStart = new System.Windows.Forms.TextBox();
            this.txtRowCount = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.btBrowse = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnSaveInDatabase = new System.Windows.Forms.Button();
            this.btnReadExcel = new System.Windows.Forms.Button();
            this.dataGridViewExcel = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBarImport = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabelPercentage = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExcel)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.comboBoxSheet);
            this.splitContainer1.Panel1.Controls.Add(this.txtColumnStart);
            this.splitContainer1.Panel1.Controls.Add(this.txtColumnCount);
            this.splitContainer1.Panel1.Controls.Add(this.txtRowStart);
            this.splitContainer1.Panel1.Controls.Add(this.txtRowCount);
            this.splitContainer1.Panel1.Controls.Add(this.label10);
            this.splitContainer1.Panel1.Controls.Add(this.label11);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.label9);
            this.splitContainer1.Panel1.Controls.Add(this.txtTableName);
            this.splitContainer1.Panel1.Controls.Add(this.label7);
            this.splitContainer1.Panel1.Controls.Add(this.label12);
            this.splitContainer1.Panel1.Controls.Add(this.btBrowse);
            this.splitContainer1.Panel1.Controls.Add(this.label6);
            this.splitContainer1.Panel1.Controls.Add(this.txtFilePath);
            this.splitContainer1.Panel1.Controls.Add(this.btnSaveInDatabase);
            this.splitContainer1.Panel1.Controls.Add(this.btnReadExcel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridViewExcel);
            this.splitContainer1.Size = new System.Drawing.Size(647, 501);
            this.splitContainer1.SplitterDistance = 123;
            this.splitContainer1.TabIndex = 1;
            // 
            // comboBoxSheet
            // 
            this.comboBoxSheet.FormattingEnabled = true;
            this.comboBoxSheet.Location = new System.Drawing.Point(75, 37);
            this.comboBoxSheet.Name = "comboBoxSheet";
            this.comboBoxSheet.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSheet.TabIndex = 20;
            this.comboBoxSheet.SelectedIndexChanged += new System.EventHandler(this.comboBoxSheet_SelectedIndexChanged);
            // 
            // txtColumnStart
            // 
            this.txtColumnStart.Location = new System.Drawing.Point(283, 37);
            this.txtColumnStart.Name = "txtColumnStart";
            this.txtColumnStart.Size = new System.Drawing.Size(38, 20);
            this.txtColumnStart.TabIndex = 19;
            this.txtColumnStart.Text = "1";
            this.txtColumnStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtColumnCount
            // 
            this.txtColumnCount.Location = new System.Drawing.Point(283, 64);
            this.txtColumnCount.Name = "txtColumnCount";
            this.txtColumnCount.Size = new System.Drawing.Size(38, 20);
            this.txtColumnCount.TabIndex = 19;
            this.txtColumnCount.Text = "0";
            this.txtColumnCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRowStart
            // 
            this.txtRowStart.Location = new System.Drawing.Point(421, 37);
            this.txtRowStart.Name = "txtRowStart";
            this.txtRowStart.Size = new System.Drawing.Size(38, 20);
            this.txtRowStart.TabIndex = 19;
            this.txtRowStart.Text = "2";
            this.txtRowStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRowCount
            // 
            this.txtRowCount.Location = new System.Drawing.Point(421, 64);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.Size = new System.Drawing.Size(38, 20);
            this.txtRowCount.TabIndex = 19;
            this.txtRowCount.Text = "0";
            this.txtRowCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(204, 40);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Column Start";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(179, 67);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Number of columns";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(356, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Row start";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(327, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "Number of rows";
            // 
            // txtTableName
            // 
            this.txtTableName.Location = new System.Drawing.Point(79, 99);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(85, 20);
            this.txtTableName.TabIndex = 19;
            this.txtTableName.Text = "Temp";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Table Name";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(34, 40);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(35, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "Sheet";
            // 
            // btBrowse
            // 
            this.btBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowse.Location = new System.Drawing.Point(597, 11);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(38, 23);
            this.btBrowse.TabIndex = 17;
            this.btBrowse.Text = "...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(17, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Excel File";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(75, 11);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(516, 20);
            this.txtFilePath.TabIndex = 16;
            // 
            // btnSaveInDatabase
            // 
            this.btnSaveInDatabase.Location = new System.Drawing.Point(187, 97);
            this.btnSaveInDatabase.Name = "btnSaveInDatabase";
            this.btnSaveInDatabase.Size = new System.Drawing.Size(195, 23);
            this.btnSaveInDatabase.TabIndex = 0;
            this.btnSaveInDatabase.Text = "Save as a new table in Database";
            this.btnSaveInDatabase.UseVisualStyleBackColor = true;
            this.btnSaveInDatabase.Click += new System.EventHandler(this.btnSaveInDatabase_Click);
            // 
            // btnReadExcel
            // 
            this.btnReadExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadExcel.Location = new System.Drawing.Point(561, 40);
            this.btnReadExcel.Name = "btnReadExcel";
            this.btnReadExcel.Size = new System.Drawing.Size(74, 23);
            this.btnReadExcel.TabIndex = 0;
            this.btnReadExcel.Text = "Read Excel";
            this.btnReadExcel.UseVisualStyleBackColor = true;
            this.btnReadExcel.Click += new System.EventHandler(this.btnReadExcel_Click);
            // 
            // dataGridViewExcel
            // 
            this.dataGridViewExcel.AllowUserToAddRows = false;
            this.dataGridViewExcel.AllowUserToDeleteRows = false;
            this.dataGridViewExcel.AllowUserToResizeRows = false;
            this.dataGridViewExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewExcel.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewExcel.Name = "dataGridViewExcel";
            this.dataGridViewExcel.Size = new System.Drawing.Size(647, 374);
            this.dataGridViewExcel.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBarImport,
            this.toolStripStatusLabelPercentage});
            this.statusStrip1.Location = new System.Drawing.Point(0, 501);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(647, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBarImport
            // 
            this.toolStripProgressBarImport.Name = "toolStripProgressBarImport";
            this.toolStripProgressBarImport.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabelPercentage
            // 
            this.toolStripStatusLabelPercentage.Name = "toolStripStatusLabelPercentage";
            this.toolStripStatusLabelPercentage.Size = new System.Drawing.Size(0, 17);
            // 
            // ucImportExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "ucImportExcel";
            this.Size = new System.Drawing.Size(647, 523);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewExcel)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox comboBoxSheet;
        private System.Windows.Forms.TextBox txtColumnStart;
        private System.Windows.Forms.TextBox txtColumnCount;
        private System.Windows.Forms.TextBox txtRowStart;
        private System.Windows.Forms.TextBox txtRowCount;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label12;
        internal System.Windows.Forms.Button btBrowse;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Button btnSaveInDatabase;
        private System.Windows.Forms.Button btnReadExcel;
        private System.Windows.Forms.DataGridView dataGridViewExcel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBarImport;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPercentage;
    }
}
