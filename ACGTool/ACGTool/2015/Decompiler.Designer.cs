namespace ACGTool
{
    partial class Decompiler
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTest = new System.Windows.Forms.Button();
            this.checkBoxForeach = new System.Windows.Forms.CheckBox();
            this.checkBoxTryCatch = new System.Windows.Forms.CheckBox();
            this.btnDecompiler = new System.Windows.Forms.Button();
            this.txtDirectory = new System.Windows.Forms.TextBox();
            this.btnOutput = new System.Windows.Forms.Button();
            this.btnDirectory = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btBrowse = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.richTextBoxBeforeDecompiler = new System.Windows.Forms.RichTextBox();
            this.tabControlResult = new System.Windows.Forms.TabControl();
            this.tabPageCSFile = new System.Windows.Forms.TabPage();
            this.richTextBoxCSFile = new System.Windows.Forms.RichTextBox();
            this.tabPageDesignCSFile = new System.Windows.Forms.TabPage();
            this.richTextBoxDesignCSFile = new System.Windows.Forms.RichTextBox();
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControlResult.SuspendLayout();
            this.tabPageCSFile.SuspendLayout();
            this.tabPageDesignCSFile.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnTest);
            this.panel1.Controls.Add(this.checkBoxForeach);
            this.panel1.Controls.Add(this.checkBoxTryCatch);
            this.panel1.Controls.Add(this.btnDecompiler);
            this.panel1.Controls.Add(this.txtDirectory);
            this.panel1.Controls.Add(this.btnOutput);
            this.panel1.Controls.Add(this.btnDirectory);
            this.panel1.Controls.Add(this.txtOutput);
            this.panel1.Controls.Add(this.txtFilePath);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.labelResult);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btBrowse);
            this.panel1.Controls.Add(this.Label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1013, 142);
            this.panel1.TabIndex = 0;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(354, 94);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 25;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // checkBoxForeach
            // 
            this.checkBoxForeach.AutoSize = true;
            this.checkBoxForeach.Location = new System.Drawing.Point(222, 100);
            this.checkBoxForeach.Name = "checkBoxForeach";
            this.checkBoxForeach.Size = new System.Drawing.Size(65, 17);
            this.checkBoxForeach.TabIndex = 24;
            this.checkBoxForeach.Text = "Foreach";
            this.checkBoxForeach.UseVisualStyleBackColor = true;
            // 
            // checkBoxTryCatch
            // 
            this.checkBoxTryCatch.AutoSize = true;
            this.checkBoxTryCatch.Location = new System.Drawing.Point(121, 100);
            this.checkBoxTryCatch.Name = "checkBoxTryCatch";
            this.checkBoxTryCatch.Size = new System.Drawing.Size(69, 17);
            this.checkBoxTryCatch.TabIndex = 24;
            this.checkBoxTryCatch.Text = "TryCatch";
            this.checkBoxTryCatch.UseVisualStyleBackColor = true;
            // 
            // btnDecompiler
            // 
            this.btnDecompiler.Location = new System.Drawing.Point(8, 95);
            this.btnDecompiler.Name = "btnDecompiler";
            this.btnDecompiler.Size = new System.Drawing.Size(75, 23);
            this.btnDecompiler.TabIndex = 0;
            this.btnDecompiler.Text = "Decompiler";
            this.btnDecompiler.UseVisualStyleBackColor = true;
            this.btnDecompiler.Click += new System.EventHandler(this.btnDecompiler_Click);
            // 
            // txtDirectory
            // 
            this.txtDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDirectory.Location = new System.Drawing.Point(94, 38);
            this.txtDirectory.Name = "txtDirectory";
            this.txtDirectory.ReadOnly = true;
            this.txtDirectory.Size = new System.Drawing.Size(844, 20);
            this.txtDirectory.TabIndex = 22;
            // 
            // btnOutput
            // 
            this.btnOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOutput.Location = new System.Drawing.Point(954, 62);
            this.btnOutput.Name = "btnOutput";
            this.btnOutput.Size = new System.Drawing.Size(47, 23);
            this.btnOutput.TabIndex = 23;
            this.btnOutput.Text = "...";
            this.btnOutput.UseVisualStyleBackColor = true;
            this.btnOutput.Click += new System.EventHandler(this.btnOutput_Click);
            // 
            // btnDirectory
            // 
            this.btnDirectory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDirectory.Location = new System.Drawing.Point(954, 35);
            this.btnDirectory.Name = "btnDirectory";
            this.btnDirectory.Size = new System.Drawing.Size(47, 23);
            this.btnDirectory.TabIndex = 23;
            this.btnDirectory.Text = "...";
            this.btnDirectory.UseVisualStyleBackColor = true;
            this.btnDirectory.Click += new System.EventHandler(this.btnDirectory_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Location = new System.Drawing.Point(94, 64);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ReadOnly = true;
            this.txtOutput.Size = new System.Drawing.Size(844, 20);
            this.txtOutput.TabIndex = 22;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(94, 12);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(844, 20);
            this.txtFilePath.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(8, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "Choose folder";
            // 
            // labelResult
            // 
            this.labelResult.AutoSize = true;
            this.labelResult.BackColor = System.Drawing.Color.Transparent;
            this.labelResult.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelResult.Location = new System.Drawing.Point(11, 121);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(37, 13);
            this.labelResult.TabIndex = 21;
            this.labelResult.Text = "Result";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(8, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "Output .cs file";
            // 
            // btBrowse
            // 
            this.btBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowse.Location = new System.Drawing.Point(954, 9);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(47, 23);
            this.btBrowse.TabIndex = 23;
            this.btBrowse.Text = "...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Location = new System.Drawing.Point(8, 15);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(76, 13);
            this.Label2.TabIndex = 21;
            this.Label2.Text = "Choose .cs file";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 142);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.richTextBoxBeforeDecompiler);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControlResult);
            this.splitContainer1.Size = new System.Drawing.Size(1013, 378);
            this.splitContainer1.SplitterDistance = 495;
            this.splitContainer1.TabIndex = 1;
            // 
            // richTextBoxBeforeDecompiler
            // 
            this.richTextBoxBeforeDecompiler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxBeforeDecompiler.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxBeforeDecompiler.Name = "richTextBoxBeforeDecompiler";
            this.richTextBoxBeforeDecompiler.Size = new System.Drawing.Size(495, 378);
            this.richTextBoxBeforeDecompiler.TabIndex = 0;
            this.richTextBoxBeforeDecompiler.Text = "";
            // 
            // tabControlResult
            // 
            this.tabControlResult.Controls.Add(this.tabPageCSFile);
            this.tabControlResult.Controls.Add(this.tabPageDesignCSFile);
            this.tabControlResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlResult.Location = new System.Drawing.Point(0, 0);
            this.tabControlResult.Name = "tabControlResult";
            this.tabControlResult.SelectedIndex = 0;
            this.tabControlResult.Size = new System.Drawing.Size(514, 378);
            this.tabControlResult.TabIndex = 3;
            // 
            // tabPageCSFile
            // 
            this.tabPageCSFile.Controls.Add(this.richTextBoxCSFile);
            this.tabPageCSFile.Location = new System.Drawing.Point(4, 22);
            this.tabPageCSFile.Name = "tabPageCSFile";
            this.tabPageCSFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCSFile.Size = new System.Drawing.Size(506, 352);
            this.tabPageCSFile.TabIndex = 0;
            this.tabPageCSFile.Text = ".cs File";
            this.tabPageCSFile.UseVisualStyleBackColor = true;
            // 
            // richTextBoxCSFile
            // 
            this.richTextBoxCSFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxCSFile.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxCSFile.Name = "richTextBoxCSFile";
            this.richTextBoxCSFile.Size = new System.Drawing.Size(500, 346);
            this.richTextBoxCSFile.TabIndex = 2;
            this.richTextBoxCSFile.Text = "";
            // 
            // tabPageDesignCSFile
            // 
            this.tabPageDesignCSFile.Controls.Add(this.richTextBoxDesignCSFile);
            this.tabPageDesignCSFile.Location = new System.Drawing.Point(4, 22);
            this.tabPageDesignCSFile.Name = "tabPageDesignCSFile";
            this.tabPageDesignCSFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDesignCSFile.Size = new System.Drawing.Size(506, 352);
            this.tabPageDesignCSFile.TabIndex = 1;
            this.tabPageDesignCSFile.Text = ".design.cs File";
            this.tabPageDesignCSFile.UseVisualStyleBackColor = true;
            // 
            // richTextBoxDesignCSFile
            // 
            this.richTextBoxDesignCSFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDesignCSFile.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxDesignCSFile.Name = "richTextBoxDesignCSFile";
            this.richTextBoxDesignCSFile.Size = new System.Drawing.Size(500, 346);
            this.richTextBoxDesignCSFile.TabIndex = 3;
            this.richTextBoxDesignCSFile.Text = "";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 520);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1013, 22);
            this.statusStrip1.TabIndex = 25;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // Decompiler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1013, 542);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Decompiler";
            this.Text = "Decompiler";
            this.Load += new System.EventHandler(this.Decompiler_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControlResult.ResumeLayout(false);
            this.tabPageCSFile.ResumeLayout(false);
            this.tabPageDesignCSFile.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.TextBox txtFilePath;
        internal System.Windows.Forms.Button btBrowse;
        internal System.Windows.Forms.Label Label2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox richTextBoxBeforeDecompiler;
        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button btnDecompiler;
        private System.Windows.Forms.RichTextBox richTextBoxCSFile;
        internal System.Windows.Forms.TextBox txtDirectory;
        internal System.Windows.Forms.Button btnDirectory;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBoxForeach;
        private System.Windows.Forms.CheckBox checkBoxTryCatch;
        internal System.Windows.Forms.Button btnOutput;
        internal System.Windows.Forms.TextBox txtOutput;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.TabControl tabControlResult;
        private System.Windows.Forms.TabPage tabPageCSFile;
        private System.Windows.Forms.TabPage tabPageDesignCSFile;
        private System.Windows.Forms.RichTextBox richTextBoxDesignCSFile;
    }
}