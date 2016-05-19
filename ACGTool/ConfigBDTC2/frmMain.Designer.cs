namespace ConfigBDTC
{
    partial class frmMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.functionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildDiaHinhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configDiaHinhToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bDTCActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bDTCTimeLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.functionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(865, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // functionToolStripMenuItem
            // 
            this.functionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.buildDiaHinhToolStripMenuItem,
            this.configDiaHinhToolStripMenuItem,
            this.bDTCActionsToolStripMenuItem,
            this.bDTCTimeLineToolStripMenuItem});
            this.functionToolStripMenuItem.Name = "functionToolStripMenuItem";
            this.functionToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
            this.functionToolStripMenuItem.Text = "Function";
            // 
            // buildDiaHinhToolStripMenuItem
            // 
            this.buildDiaHinhToolStripMenuItem.Name = "buildDiaHinhToolStripMenuItem";
            this.buildDiaHinhToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.buildDiaHinhToolStripMenuItem.Text = "Build DiaHinh";
            this.buildDiaHinhToolStripMenuItem.Click += new System.EventHandler(this.buildDiaHinhToolStripMenuItem_Click);
            // 
            // configDiaHinhToolStripMenuItem
            // 
            this.configDiaHinhToolStripMenuItem.Name = "configDiaHinhToolStripMenuItem";
            this.configDiaHinhToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.configDiaHinhToolStripMenuItem.Text = "Config DiaHinh";
            this.configDiaHinhToolStripMenuItem.Click += new System.EventHandler(this.configDiaHinhToolStripMenuItem_Click);
            // 
            // bDTCActionsToolStripMenuItem
            // 
            this.bDTCActionsToolStripMenuItem.Name = "bDTCActionsToolStripMenuItem";
            this.bDTCActionsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.bDTCActionsToolStripMenuItem.Text = "BDTC Actions";
            this.bDTCActionsToolStripMenuItem.Click += new System.EventHandler(this.bDTCActionsToolStripMenuItem_Click);
            // 
            // bDTCTimeLineToolStripMenuItem
            // 
            this.bDTCTimeLineToolStripMenuItem.Name = "bDTCTimeLineToolStripMenuItem";
            this.bDTCTimeLineToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.bDTCTimeLineToolStripMenuItem.Text = "BDTC TimeLine";
            this.bDTCTimeLineToolStripMenuItem.Click += new System.EventHandler(this.bDTCTimeLineToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 410);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem functionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buildDiaHinhToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configDiaHinhToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bDTCActionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bDTCTimeLineToolStripMenuItem;
    }
}