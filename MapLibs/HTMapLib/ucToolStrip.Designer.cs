namespace HTMapLib
{
    partial class ucToolStrip
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucToolStrip));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonSelectTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPanTool = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDistance = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonAllLayersView = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonSelectTool,
            this.toolStripButtonPanTool,
            this.toolStripButtonZoomIn,
            this.toolStripButtonZoomOut,
            this.toolStripButtonDistance,
            this.toolStripButtonAllLayersView});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(809, 26);
            this.toolStrip1.TabIndex = 10;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonSelectTool
            // 
            this.toolStripButtonSelectTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonSelectTool.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSelectTool.Image")));
            this.toolStripButtonSelectTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSelectTool.Name = "toolStripButtonSelectTool";
            this.toolStripButtonSelectTool.Size = new System.Drawing.Size(23, 23);
            this.toolStripButtonSelectTool.Text = "Mũi tên chọn";
            this.toolStripButtonSelectTool.Click += new System.EventHandler(this.toolStripButtonSelectTool_Click);
            // 
            // toolStripButtonPanTool
            // 
            this.toolStripButtonPanTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonPanTool.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonPanTool.Image")));
            this.toolStripButtonPanTool.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonPanTool.Name = "toolStripButtonPanTool";
            this.toolStripButtonPanTool.Size = new System.Drawing.Size(23, 23);
            this.toolStripButtonPanTool.Text = "Kéo màn hình";
            this.toolStripButtonPanTool.Click += new System.EventHandler(this.toolStripButtonPanTool_Click);
            // 
            // toolStripButtonZoomIn
            // 
            this.toolStripButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomIn.Image")));
            this.toolStripButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomIn.Name = "toolStripButtonZoomIn";
            this.toolStripButtonZoomIn.Size = new System.Drawing.Size(23, 23);
            this.toolStripButtonZoomIn.Text = "Phóng to";
            this.toolStripButtonZoomIn.Click += new System.EventHandler(this.toolStripButtonZoomIn_Click);
            // 
            // toolStripButtonZoomOut
            // 
            this.toolStripButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomOut.Image")));
            this.toolStripButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomOut.Name = "toolStripButtonZoomOut";
            this.toolStripButtonZoomOut.Size = new System.Drawing.Size(23, 23);
            this.toolStripButtonZoomOut.Text = "Thu nhỏ";
            this.toolStripButtonZoomOut.Click += new System.EventHandler(this.toolStripButtonZoomOut_Click);
            // 
            // toolStripButtonDistance
            // 
            this.toolStripButtonDistance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDistance.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDistance.Image")));
            this.toolStripButtonDistance.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDistance.Name = "toolStripButtonDistance";
            this.toolStripButtonDistance.Size = new System.Drawing.Size(23, 23);
            this.toolStripButtonDistance.Text = "Đo khoảng cách";
            this.toolStripButtonDistance.Click += new System.EventHandler(this.toolStripButtonDistance_Click);
            // 
            // toolStripButtonAllLayersView
            // 
            this.toolStripButtonAllLayersView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonAllLayersView.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonAllLayersView.Image")));
            this.toolStripButtonAllLayersView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonAllLayersView.Name = "toolStripButtonAllLayersView";
            this.toolStripButtonAllLayersView.Size = new System.Drawing.Size(23, 23);
            this.toolStripButtonAllLayersView.Text = "Hiển thị bản đồ mặc định";
            this.toolStripButtonAllLayersView.Click += new System.EventHandler(this.toolStripButtonAllLayersView_Click);
            // 
            // ucToolStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Name = "ucToolStrip";
            this.Size = new System.Drawing.Size(809, 26);
            this.Load += new System.EventHandler(this.ucToolStrip_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonSelectTool;
        private System.Windows.Forms.ToolStripButton toolStripButtonPanTool;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
        private System.Windows.Forms.ToolStripButton toolStripButtonDistance;
        private System.Windows.Forms.ToolStripButton toolStripButtonAllLayersView;
    }
}
