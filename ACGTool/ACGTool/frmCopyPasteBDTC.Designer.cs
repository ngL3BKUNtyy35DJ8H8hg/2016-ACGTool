namespace ACGTool
{
    partial class frmCopyPasteBDTC
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
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listBoxSrc = new System.Windows.Forms.ListBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.txtFileSrc = new System.Windows.Forms.TextBox();
            this.txtFileDes = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btBrowseSrc = new System.Windows.Forms.Button();
            this.btBrowseDes = new System.Windows.Forms.Button();
            this.btnLoadScr = new System.Windows.Forms.Button();
            this.btnSaveDesFile = new System.Windows.Forms.Button();
            this.buttonDesFind = new System.Windows.Forms.Button();
            this.btnSrcFind = new System.Windows.Forms.Button();
            this.btnLoadDes = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDesFind = new System.Windows.Forms.TextBox();
            this.textBoxFind = new System.Windows.Forms.TextBox();
            this.richTextBoxSrc = new System.Windows.Forms.RichTextBox();
            this.panelSrcFill = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelDesFill = new System.Windows.Forms.Panel();
            this.richTextBoxDes = new System.Windows.Forms.RichTextBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.listBoxDes = new System.Windows.Forms.ListBox();
            this.panelSrc = new System.Windows.Forms.Panel();
            this.panelSrcTop = new System.Windows.Forms.Panel();
            this.btnSaveSrcFile = new System.Windows.Forms.Button();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.panelMid = new System.Windows.Forms.Panel();
            this.buttonVe = new System.Windows.Forms.Button();
            this.buttonDi = new System.Windows.Forms.Button();
            this.panelDes = new System.Windows.Forms.Panel();
            this.panelDesTop = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblSrcResult = new System.Windows.Forms.Label();
            this.lblDesResult = new System.Windows.Forms.Label();
            this.panelSrcFill.SuspendLayout();
            this.panelDesFill.SuspendLayout();
            this.panelSrc.SuspendLayout();
            this.panelSrcTop.SuspendLayout();
            this.panelMid.SuspendLayout();
            this.panelDes.SuspendLayout();
            this.panelDesTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBoxSrc
            // 
            this.listBoxSrc.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxSrc.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxSrc.FormattingEnabled = true;
            this.listBoxSrc.ItemHeight = 16;
            this.listBoxSrc.Location = new System.Drawing.Point(0, 0);
            this.listBoxSrc.Name = "listBoxSrc";
            this.listBoxSrc.Size = new System.Drawing.Size(94, 552);
            this.listBoxSrc.TabIndex = 36;
            this.listBoxSrc.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            this.listBoxSrc.DoubleClick += new System.EventHandler(this.listBox1_DoubleClick);
            // 
            // txtFileSrc
            // 
            this.txtFileSrc.Location = new System.Drawing.Point(8, 29);
            this.txtFileSrc.Name = "txtFileSrc";
            this.txtFileSrc.Size = new System.Drawing.Size(352, 20);
            this.txtFileSrc.TabIndex = 13;
            // 
            // txtFileDes
            // 
            this.txtFileDes.Location = new System.Drawing.Point(9, 29);
            this.txtFileDes.Name = "txtFileDes";
            this.txtFileDes.Size = new System.Drawing.Size(352, 20);
            this.txtFileDes.TabIndex = 13;
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Location = new System.Drawing.Point(5, 9);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(92, 13);
            this.Label2.TabIndex = 12;
            this.Label2.Text = "Source file .BDTC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(6, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Des file .BDTC";
            // 
            // btBrowseSrc
            // 
            this.btBrowseSrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowseSrc.Location = new System.Drawing.Point(366, 27);
            this.btBrowseSrc.Name = "btBrowseSrc";
            this.btBrowseSrc.Size = new System.Drawing.Size(38, 23);
            this.btBrowseSrc.TabIndex = 14;
            this.btBrowseSrc.Text = "...";
            this.btBrowseSrc.UseVisualStyleBackColor = true;
            this.btBrowseSrc.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // btBrowseDes
            // 
            this.btBrowseDes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowseDes.Location = new System.Drawing.Point(367, 27);
            this.btBrowseDes.Name = "btBrowseDes";
            this.btBrowseDes.Size = new System.Drawing.Size(38, 23);
            this.btBrowseDes.TabIndex = 14;
            this.btBrowseDes.Text = "...";
            this.btBrowseDes.UseVisualStyleBackColor = true;
            this.btBrowseDes.Click += new System.EventHandler(this.btBrowseSave_Click);
            // 
            // btnLoadScr
            // 
            this.btnLoadScr.Location = new System.Drawing.Point(410, 27);
            this.btnLoadScr.Name = "btnLoadScr";
            this.btnLoadScr.Size = new System.Drawing.Size(44, 23);
            this.btnLoadScr.TabIndex = 15;
            this.btnLoadScr.Text = "Load";
            this.btnLoadScr.UseVisualStyleBackColor = true;
            this.btnLoadScr.Click += new System.EventHandler(this.btnLoadScr_Click);
            // 
            // btnSaveDesFile
            // 
            this.btnSaveDesFile.Location = new System.Drawing.Point(459, 27);
            this.btnSaveDesFile.Name = "btnSaveDesFile";
            this.btnSaveDesFile.Size = new System.Drawing.Size(42, 23);
            this.btnSaveDesFile.TabIndex = 16;
            this.btnSaveDesFile.Text = "Save";
            this.btnSaveDesFile.UseVisualStyleBackColor = true;
            this.btnSaveDesFile.Click += new System.EventHandler(this.btnSaveDesFile_Click);
            // 
            // buttonDesFind
            // 
            this.buttonDesFind.Location = new System.Drawing.Point(192, 55);
            this.buttonDesFind.Name = "buttonDesFind";
            this.buttonDesFind.Size = new System.Drawing.Size(63, 23);
            this.buttonDesFind.TabIndex = 15;
            this.buttonDesFind.Text = "Find";
            this.buttonDesFind.UseVisualStyleBackColor = true;
            this.buttonDesFind.Click += new System.EventHandler(this.buttonDesFind_Click);
            // 
            // btnSrcFind
            // 
            this.btnSrcFind.Location = new System.Drawing.Point(186, 55);
            this.btnSrcFind.Name = "btnSrcFind";
            this.btnSrcFind.Size = new System.Drawing.Size(63, 23);
            this.btnSrcFind.TabIndex = 15;
            this.btnSrcFind.Text = "Find";
            this.btnSrcFind.UseVisualStyleBackColor = true;
            this.btnSrcFind.Click += new System.EventHandler(this.btnSrcFind_Click);
            // 
            // btnLoadDes
            // 
            this.btnLoadDes.Location = new System.Drawing.Point(411, 27);
            this.btnLoadDes.Name = "btnLoadDes";
            this.btnLoadDes.Size = new System.Drawing.Size(42, 23);
            this.btnLoadDes.TabIndex = 15;
            this.btnLoadDes.Text = "Load";
            this.btnLoadDes.UseVisualStyleBackColor = true;
            this.btnLoadDes.Click += new System.EventHandler(this.btnLoadDes_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(11, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Find what";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(6, 81);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(357, 14);
            this.label6.TabIndex = 12;
            this.label6.Text = "(Double click vào listbox để hiển thị giá trị chọn trên Find What)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(5, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Find what";
            // 
            // textBoxDesFind
            // 
            this.textBoxDesFind.Location = new System.Drawing.Point(70, 57);
            this.textBoxDesFind.Name = "textBoxDesFind";
            this.textBoxDesFind.Size = new System.Drawing.Size(116, 20);
            this.textBoxDesFind.TabIndex = 13;
            // 
            // textBoxFind
            // 
            this.textBoxFind.Location = new System.Drawing.Point(64, 57);
            this.textBoxFind.Name = "textBoxFind";
            this.textBoxFind.Size = new System.Drawing.Size(116, 20);
            this.textBoxFind.TabIndex = 13;
            // 
            // richTextBoxSrc
            // 
            this.richTextBoxSrc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxSrc.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxSrc.HideSelection = false;
            this.richTextBoxSrc.Location = new System.Drawing.Point(94, 0);
            this.richTextBoxSrc.Name = "richTextBoxSrc";
            this.richTextBoxSrc.Size = new System.Drawing.Size(506, 552);
            this.richTextBoxSrc.TabIndex = 19;
            this.richTextBoxSrc.Text = "";
            // 
            // panelSrcFill
            // 
            this.panelSrcFill.Controls.Add(this.splitter1);
            this.panelSrcFill.Controls.Add(this.richTextBoxSrc);
            this.panelSrcFill.Controls.Add(this.listBoxSrc);
            this.panelSrcFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSrcFill.Location = new System.Drawing.Point(0, 111);
            this.panelSrcFill.Name = "panelSrcFill";
            this.panelSrcFill.Size = new System.Drawing.Size(600, 552);
            this.panelSrcFill.TabIndex = 38;
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(94, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 552);
            this.splitter1.TabIndex = 37;
            this.splitter1.TabStop = false;
            // 
            // panelDesFill
            // 
            this.panelDesFill.Controls.Add(this.richTextBoxDes);
            this.panelDesFill.Controls.Add(this.splitter2);
            this.panelDesFill.Controls.Add(this.listBoxDes);
            this.panelDesFill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesFill.Location = new System.Drawing.Point(0, 111);
            this.panelDesFill.Name = "panelDesFill";
            this.panelDesFill.Size = new System.Drawing.Size(631, 552);
            this.panelDesFill.TabIndex = 40;
            // 
            // richTextBoxDes
            // 
            this.richTextBoxDes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxDes.HideSelection = false;
            this.richTextBoxDes.Location = new System.Drawing.Point(112, 0);
            this.richTextBoxDes.Name = "richTextBoxDes";
            this.richTextBoxDes.Size = new System.Drawing.Size(519, 552);
            this.richTextBoxDes.TabIndex = 39;
            this.richTextBoxDes.Text = "";
            // 
            // splitter2
            // 
            this.splitter2.Location = new System.Drawing.Point(109, 0);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 552);
            this.splitter2.TabIndex = 38;
            this.splitter2.TabStop = false;
            // 
            // listBoxDes
            // 
            this.listBoxDes.Dock = System.Windows.Forms.DockStyle.Left;
            this.listBoxDes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBoxDes.FormattingEnabled = true;
            this.listBoxDes.ItemHeight = 16;
            this.listBoxDes.Location = new System.Drawing.Point(0, 0);
            this.listBoxDes.Name = "listBoxDes";
            this.listBoxDes.Size = new System.Drawing.Size(109, 552);
            this.listBoxDes.TabIndex = 37;
            this.listBoxDes.SelectedIndexChanged += new System.EventHandler(this.listBoxDes_SelectedIndexChanged);
            this.listBoxDes.DoubleClick += new System.EventHandler(this.listBoxDes_DoubleClick);
            // 
            // panelSrc
            // 
            this.panelSrc.Controls.Add(this.panelSrcFill);
            this.panelSrc.Controls.Add(this.panelSrcTop);
            this.panelSrc.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSrc.Location = new System.Drawing.Point(0, 0);
            this.panelSrc.Name = "panelSrc";
            this.panelSrc.Size = new System.Drawing.Size(600, 663);
            this.panelSrc.TabIndex = 2;
            // 
            // panelSrcTop
            // 
            this.panelSrcTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelSrcTop.Controls.Add(this.btnSaveSrcFile);
            this.panelSrcTop.Controls.Add(this.btnSrcFind);
            this.panelSrcTop.Controls.Add(this.Label2);
            this.panelSrcTop.Controls.Add(this.btnLoadScr);
            this.panelSrcTop.Controls.Add(this.txtFileSrc);
            this.panelSrcTop.Controls.Add(this.btBrowseSrc);
            this.panelSrcTop.Controls.Add(this.textBoxFind);
            this.panelSrcTop.Controls.Add(this.lblSrcResult);
            this.panelSrcTop.Controls.Add(this.label6);
            this.panelSrcTop.Controls.Add(this.label1);
            this.panelSrcTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSrcTop.Location = new System.Drawing.Point(0, 0);
            this.panelSrcTop.Name = "panelSrcTop";
            this.panelSrcTop.Size = new System.Drawing.Size(600, 111);
            this.panelSrcTop.TabIndex = 0;
            // 
            // btnSaveSrcFile
            // 
            this.btnSaveSrcFile.Location = new System.Drawing.Point(460, 27);
            this.btnSaveSrcFile.Name = "btnSaveSrcFile";
            this.btnSaveSrcFile.Size = new System.Drawing.Size(47, 23);
            this.btnSaveSrcFile.TabIndex = 16;
            this.btnSaveSrcFile.Text = "Save";
            this.btnSaveSrcFile.UseVisualStyleBackColor = true;
            this.btnSaveSrcFile.Click += new System.EventHandler(this.btnSaveSrcFile_Click);
            // 
            // splitter3
            // 
            this.splitter3.Location = new System.Drawing.Point(600, 0);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(3, 663);
            this.splitter3.TabIndex = 3;
            this.splitter3.TabStop = false;
            // 
            // panelMid
            // 
            this.panelMid.Controls.Add(this.buttonVe);
            this.panelMid.Controls.Add(this.buttonDi);
            this.panelMid.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMid.Location = new System.Drawing.Point(603, 0);
            this.panelMid.Name = "panelMid";
            this.panelMid.Size = new System.Drawing.Size(42, 663);
            this.panelMid.TabIndex = 4;
            // 
            // buttonVe
            // 
            this.buttonVe.Location = new System.Drawing.Point(9, 344);
            this.buttonVe.Name = "buttonVe";
            this.buttonVe.Size = new System.Drawing.Size(24, 23);
            this.buttonVe.TabIndex = 0;
            this.buttonVe.Text = "<<";
            this.buttonVe.UseVisualStyleBackColor = true;
            this.buttonVe.Click += new System.EventHandler(this.buttonVe_Click);
            // 
            // buttonDi
            // 
            this.buttonDi.Location = new System.Drawing.Point(9, 315);
            this.buttonDi.Name = "buttonDi";
            this.buttonDi.Size = new System.Drawing.Size(24, 23);
            this.buttonDi.TabIndex = 0;
            this.buttonDi.Text = ">>";
            this.buttonDi.UseVisualStyleBackColor = true;
            this.buttonDi.Click += new System.EventHandler(this.buttonDi_Click);
            // 
            // panelDes
            // 
            this.panelDes.Controls.Add(this.panelDesFill);
            this.panelDes.Controls.Add(this.panelDesTop);
            this.panelDes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDes.Location = new System.Drawing.Point(645, 0);
            this.panelDes.Name = "panelDes";
            this.panelDes.Size = new System.Drawing.Size(631, 663);
            this.panelDes.TabIndex = 5;
            // 
            // panelDesTop
            // 
            this.panelDesTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelDesTop.Controls.Add(this.btnSaveDesFile);
            this.panelDesTop.Controls.Add(this.label3);
            this.panelDesTop.Controls.Add(this.buttonDesFind);
            this.panelDesTop.Controls.Add(this.txtFileDes);
            this.panelDesTop.Controls.Add(this.textBoxDesFind);
            this.panelDesTop.Controls.Add(this.btnLoadDes);
            this.panelDesTop.Controls.Add(this.label4);
            this.panelDesTop.Controls.Add(this.lblDesResult);
            this.panelDesTop.Controls.Add(this.label5);
            this.panelDesTop.Controls.Add(this.btBrowseDes);
            this.panelDesTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDesTop.Location = new System.Drawing.Point(0, 0);
            this.panelDesTop.Name = "panelDesTop";
            this.panelDesTop.Size = new System.Drawing.Size(631, 111);
            this.panelDesTop.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Red;
            this.label5.Location = new System.Drawing.Point(6, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(357, 14);
            this.label5.TabIndex = 12;
            this.label5.Text = "(Double click vào listbox để hiển thị giá trị chọn trên Find What)";
            // 
            // lblSrcResult
            // 
            this.lblSrcResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSrcResult.BackColor = System.Drawing.Color.Transparent;
            this.lblSrcResult.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSrcResult.ForeColor = System.Drawing.Color.Red;
            this.lblSrcResult.Location = new System.Drawing.Point(255, 60);
            this.lblSrcResult.Name = "lblSrcResult";
            this.lblSrcResult.Size = new System.Drawing.Size(328, 18);
            this.lblSrcResult.TabIndex = 12;
            this.lblSrcResult.Text = "Result";
            // 
            // lblDesResult
            // 
            this.lblDesResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDesResult.BackColor = System.Drawing.Color.Transparent;
            this.lblDesResult.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesResult.ForeColor = System.Drawing.Color.Red;
            this.lblDesResult.Location = new System.Drawing.Point(261, 59);
            this.lblDesResult.Name = "lblDesResult";
            this.lblDesResult.Size = new System.Drawing.Size(356, 18);
            this.lblDesResult.TabIndex = 12;
            this.lblDesResult.Text = "Result";
            // 
            // frmCopyPasteBDTC
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 663);
            this.Controls.Add(this.panelDes);
            this.Controls.Add(this.panelMid);
            this.Controls.Add(this.splitter3);
            this.Controls.Add(this.panelSrc);
            this.Name = "frmCopyPasteBDTC";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BDTCTool";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BDTCTool_Load);
            this.panelSrcFill.ResumeLayout(false);
            this.panelDesFill.ResumeLayout(false);
            this.panelSrc.ResumeLayout(false);
            this.panelSrcTop.ResumeLayout(false);
            this.panelSrcTop.PerformLayout();
            this.panelMid.ResumeLayout(false);
            this.panelDes.ResumeLayout(false);
            this.panelDesTop.ResumeLayout(false);
            this.panelDesTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.ListBox listBoxSrc;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        internal System.Windows.Forms.TextBox txtFileSrc;
        internal System.Windows.Forms.TextBox txtFileDes;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button btBrowseSrc;
        internal System.Windows.Forms.Button btBrowseDes;
        internal System.Windows.Forms.Button btnLoadScr;
        private System.Windows.Forms.Button btnSaveDesFile;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.TextBox textBoxFind;
        internal System.Windows.Forms.Button btnSrcFind;
        internal System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox richTextBoxSrc;
        private System.Windows.Forms.Panel panelSrcFill;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panelDesFill;
        private System.Windows.Forms.ListBox listBoxDes;
        internal System.Windows.Forms.Button btnLoadDes;
        private System.Windows.Forms.RichTextBox richTextBoxDes;
        private System.Windows.Forms.Splitter splitter2;
        internal System.Windows.Forms.Button buttonDesFind;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.TextBox textBoxDesFind;
        private System.Windows.Forms.Panel panelSrc;
        private System.Windows.Forms.Panel panelSrcTop;
        private System.Windows.Forms.Splitter splitter3;
        private System.Windows.Forms.Panel panelMid;
        private System.Windows.Forms.Panel panelDes;
        private System.Windows.Forms.Panel panelDesTop;
        private System.Windows.Forms.Button buttonVe;
        private System.Windows.Forms.Button buttonDi;
        private System.Windows.Forms.Button btnSaveSrcFile;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label lblSrcResult;
        internal System.Windows.Forms.Label lblDesResult;
    }
}