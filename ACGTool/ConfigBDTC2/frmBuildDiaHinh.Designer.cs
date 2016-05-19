namespace ConfigBDTC
{
    partial class frmBuildDiaHinh
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
            this.Label2 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.txtSaBanName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtImageFile = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtXYZFile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBuild = new System.Windows.Forms.Button();
            this.btPathBrowse = new System.Windows.Forms.Button();
            this.btImageFileBrowse = new System.Windows.Forms.Button();
            this.btnXYZFileBrowse = new System.Windows.Forms.Button();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Location = new System.Drawing.Point(17, 41);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(57, 13);
            this.Label2.TabIndex = 17;
            this.Label2.Text = "Save Path";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(97, 38);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(419, 20);
            this.txtFilePath.TabIndex = 18;
            // 
            // txtSaBanName
            // 
            this.txtSaBanName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSaBanName.Location = new System.Drawing.Point(97, 12);
            this.txtSaBanName.Name = "txtSaBanName";
            this.txtSaBanName.Size = new System.Drawing.Size(237, 20);
            this.txtSaBanName.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(17, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Saban Name";
            // 
            // txtImageFile
            // 
            this.txtImageFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtImageFile.Location = new System.Drawing.Point(97, 64);
            this.txtImageFile.Name = "txtImageFile";
            this.txtImageFile.Size = new System.Drawing.Size(419, 20);
            this.txtImageFile.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(17, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 17;
            this.label3.Text = "Image File";
            // 
            // txtXYZFile
            // 
            this.txtXYZFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXYZFile.Location = new System.Drawing.Point(97, 90);
            this.txtXYZFile.Name = "txtXYZFile";
            this.txtXYZFile.Size = new System.Drawing.Size(419, 20);
            this.txtXYZFile.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(17, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "XYZ File";
            // 
            // btnBuild
            // 
            this.btnBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuild.Location = new System.Drawing.Point(486, 116);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(74, 23);
            this.btnBuild.TabIndex = 20;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // btPathBrowse
            // 
            this.btPathBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btPathBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btPathBrowse.Location = new System.Drawing.Point(522, 36);
            this.btPathBrowse.Name = "btPathBrowse";
            this.btPathBrowse.Size = new System.Drawing.Size(38, 23);
            this.btPathBrowse.TabIndex = 21;
            this.btPathBrowse.Text = "...";
            this.btPathBrowse.UseVisualStyleBackColor = true;
            this.btPathBrowse.Click += new System.EventHandler(this.btPathBrowse_Click);
            // 
            // btImageFileBrowse
            // 
            this.btImageFileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btImageFileBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btImageFileBrowse.Location = new System.Drawing.Point(522, 62);
            this.btImageFileBrowse.Name = "btImageFileBrowse";
            this.btImageFileBrowse.Size = new System.Drawing.Size(38, 23);
            this.btImageFileBrowse.TabIndex = 21;
            this.btImageFileBrowse.Text = "...";
            this.btImageFileBrowse.UseVisualStyleBackColor = true;
            this.btImageFileBrowse.Click += new System.EventHandler(this.btImageFileBrowse_Click);
            // 
            // btnXYZFileBrowse
            // 
            this.btnXYZFileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXYZFileBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXYZFileBrowse.Location = new System.Drawing.Point(522, 88);
            this.btnXYZFileBrowse.Name = "btnXYZFileBrowse";
            this.btnXYZFileBrowse.Size = new System.Drawing.Size(38, 23);
            this.btnXYZFileBrowse.TabIndex = 21;
            this.btnXYZFileBrowse.Text = "...";
            this.btnXYZFileBrowse.UseVisualStyleBackColor = true;
            this.btnXYZFileBrowse.Click += new System.EventHandler(this.btnXYZFileBrowse_Click);
            // 
            // frmBuildDiaHinh
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 150);
            this.Controls.Add(this.btnXYZFileBrowse);
            this.Controls.Add(this.btImageFileBrowse);
            this.Controls.Add(this.btPathBrowse);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtXYZFile);
            this.Controls.Add(this.txtImageFile);
            this.Controls.Add(this.txtSaBanName);
            this.Controls.Add(this.txtFilePath);
            this.Name = "frmBuildDiaHinh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Build DiaHinh";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtFilePath;
        internal System.Windows.Forms.TextBox txtSaBanName;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox txtImageFile;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.TextBox txtXYZFile;
        internal System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBuild;
        internal System.Windows.Forms.Button btPathBrowse;
        internal System.Windows.Forms.Button btImageFileBrowse;
        internal System.Windows.Forms.Button btnXYZFileBrowse;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}