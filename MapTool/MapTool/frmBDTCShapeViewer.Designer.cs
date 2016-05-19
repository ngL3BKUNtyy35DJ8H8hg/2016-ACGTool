namespace MapTool
{
    partial class frmBDTCShapeViewer
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.xPanderPanelList1 = new BSE.Windows.Forms.XPanderPanelList();
            this.xPanderPanel3DCoordinate = new BSE.Windows.Forms.XPanderPanel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtX3D = new System.Windows.Forms.TextBox();
            this.txtY3D = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtSceenX = new System.Windows.Forms.TextBox();
            this.txtScreenY = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.txtLongitude = new System.Windows.Forms.TextBox();
            this.txtLatitude = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.xPanderPanel4 = new BSE.Windows.Forms.XPanderPanel();
            this.ucMilSymbols1 = new HT.MilSymbols.ucMilSymbols();
            this.xPanderPanel5 = new BSE.Windows.Forms.XPanderPanel();
            this.chkShowGridXYZ = new System.Windows.Forms.CheckBox();
            this.btnDemoUcVectShapes_Form = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.ucVectShapes_Map1 = new HT.VectorShapes.Maps.ucVectShapes_Map();
            this.ucToolStrip1 = new HT.VectorShapes.Maps.ucMapToolStrip();
            this.ucVectShapeToolBox1 = new HT.VectorShapes.Maps.ucMapVectShapeToolBox();
            this.statusStripMap = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelCoordinate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelCoordinate2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelAltitude = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3DCoord = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelScreenCoord = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btBrowse = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStripMap = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopyLonLatCoord = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopyScreenCoord = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCopy3DCoord = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCustomLineCap = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.xPanderPanelList1.SuspendLayout();
            this.xPanderPanel3DCoordinate.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.xPanderPanel4.SuspendLayout();
            this.xPanderPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.statusStripMap.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStripMap.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 55);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.xPanderPanelList1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.statusStripMap);
            this.splitContainer1.Size = new System.Drawing.Size(1080, 430);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 0;
            // 
            // xPanderPanelList1
            // 
            this.xPanderPanelList1.CaptionStyle = BSE.Windows.Forms.CaptionStyle.Normal;
            this.xPanderPanelList1.Controls.Add(this.xPanderPanel3DCoordinate);
            this.xPanderPanelList1.Controls.Add(this.xPanderPanel4);
            this.xPanderPanelList1.Controls.Add(this.xPanderPanel5);
            this.xPanderPanelList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanderPanelList1.GradientBackground = System.Drawing.Color.Empty;
            this.xPanderPanelList1.Location = new System.Drawing.Point(0, 0);
            this.xPanderPanelList1.Name = "xPanderPanelList1";
            this.xPanderPanelList1.PanelColors = null;
            this.xPanderPanelList1.Size = new System.Drawing.Size(300, 430);
            this.xPanderPanelList1.TabIndex = 0;
            this.xPanderPanelList1.Text = "xPanderPanelList1";
            // 
            // xPanderPanel3DCoordinate
            // 
            this.xPanderPanel3DCoordinate.CaptionFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.xPanderPanel3DCoordinate.Controls.Add(this.groupBox5);
            this.xPanderPanel3DCoordinate.Controls.Add(this.groupBox4);
            this.xPanderPanel3DCoordinate.Controls.Add(this.groupBox3);
            this.xPanderPanel3DCoordinate.CustomColors.BackColor = System.Drawing.SystemColors.Control;
            this.xPanderPanel3DCoordinate.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.xPanderPanel3DCoordinate.CustomColors.CaptionCheckedGradientBegin = System.Drawing.Color.Empty;
            this.xPanderPanel3DCoordinate.CustomColors.CaptionCheckedGradientEnd = System.Drawing.Color.Empty;
            this.xPanderPanel3DCoordinate.CustomColors.CaptionCheckedGradientMiddle = System.Drawing.Color.Empty;
            this.xPanderPanel3DCoordinate.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel3DCoordinate.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel3DCoordinate.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanel3DCoordinate.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.xPanderPanel3DCoordinate.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanel3DCoordinate.CustomColors.CaptionPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.xPanderPanel3DCoordinate.CustomColors.CaptionPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.xPanderPanel3DCoordinate.CustomColors.CaptionPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.xPanderPanel3DCoordinate.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.xPanderPanel3DCoordinate.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.xPanderPanel3DCoordinate.CustomColors.CaptionSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.xPanderPanel3DCoordinate.CustomColors.CaptionSelectedText = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel3DCoordinate.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel3DCoordinate.CustomColors.FlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanel3DCoordinate.CustomColors.FlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanel3DCoordinate.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.xPanderPanel3DCoordinate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel3DCoordinate.Image = null;
            this.xPanderPanel3DCoordinate.Name = "xPanderPanel3DCoordinate";
            this.xPanderPanel3DCoordinate.Size = new System.Drawing.Size(300, 25);
            this.xPanderPanel3DCoordinate.TabIndex = 1;
            this.xPanderPanel3DCoordinate.Text = "3D Coordinate";
            this.xPanderPanel3DCoordinate.ToolTipTextCloseIcon = null;
            this.xPanderPanel3DCoordinate.ToolTipTextExpandIconPanelCollapsed = null;
            this.xPanderPanel3DCoordinate.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtX3D);
            this.groupBox5.Controls.Add(this.txtY3D);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Location = new System.Drawing.Point(11, 226);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(275, 83);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "3D Coordinate";
            // 
            // txtX3D
            // 
            this.txtX3D.Location = new System.Drawing.Point(41, 26);
            this.txtX3D.Name = "txtX3D";
            this.txtX3D.Size = new System.Drawing.Size(140, 20);
            this.txtX3D.TabIndex = 1;
            // 
            // txtY3D
            // 
            this.txtY3D.Location = new System.Drawing.Point(40, 51);
            this.txtY3D.Name = "txtY3D";
            this.txtY3D.Size = new System.Drawing.Size(140, 20);
            this.txtY3D.TabIndex = 1;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "X";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 51);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(14, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Y";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtSceenX);
            this.groupBox4.Controls.Add(this.txtScreenY);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(11, 127);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(274, 82);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Screen Coordinate";
            // 
            // txtSceenX
            // 
            this.txtSceenX.Location = new System.Drawing.Point(45, 23);
            this.txtSceenX.Name = "txtSceenX";
            this.txtSceenX.Size = new System.Drawing.Size(140, 20);
            this.txtSceenX.TabIndex = 1;
            // 
            // txtScreenY
            // 
            this.txtScreenY.Location = new System.Drawing.Point(44, 48);
            this.txtScreenY.Name = "txtScreenY";
            this.txtScreenY.Size = new System.Drawing.Size(140, 20);
            this.txtScreenY.TabIndex = 1;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 26);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "X";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(11, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(14, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Y";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnFind);
            this.groupBox3.Controls.Add(this.txtLongitude);
            this.groupBox3.Controls.Add(this.txtLatitude);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(12, 40);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(282, 80);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Longitute, Latitude";
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(210, 47);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(59, 23);
            this.btnFind.TabIndex = 2;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // txtLongitude
            // 
            this.txtLongitude.Location = new System.Drawing.Point(72, 23);
            this.txtLongitude.Name = "txtLongitude";
            this.txtLongitude.Size = new System.Drawing.Size(133, 20);
            this.txtLongitude.TabIndex = 1;
            // 
            // txtLatitude
            // 
            this.txtLatitude.Location = new System.Drawing.Point(71, 48);
            this.txtLatitude.Name = "txtLatitude";
            this.txtLatitude.Size = new System.Drawing.Size(133, 20);
            this.txtLatitude.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 26);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Longitude";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(11, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Latitude";
            // 
            // xPanderPanel4
            // 
            this.xPanderPanel4.CaptionFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.xPanderPanel4.Controls.Add(this.ucMilSymbols1);
            this.xPanderPanel4.CustomColors.BackColor = System.Drawing.SystemColors.Control;
            this.xPanderPanel4.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.xPanderPanel4.CustomColors.CaptionCheckedGradientBegin = System.Drawing.Color.Empty;
            this.xPanderPanel4.CustomColors.CaptionCheckedGradientEnd = System.Drawing.Color.Empty;
            this.xPanderPanel4.CustomColors.CaptionCheckedGradientMiddle = System.Drawing.Color.Empty;
            this.xPanderPanel4.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel4.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel4.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanel4.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.xPanderPanel4.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanel4.CustomColors.CaptionPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.xPanderPanel4.CustomColors.CaptionPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.xPanderPanel4.CustomColors.CaptionPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.xPanderPanel4.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.xPanderPanel4.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.xPanderPanel4.CustomColors.CaptionSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.xPanderPanel4.CustomColors.CaptionSelectedText = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel4.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel4.CustomColors.FlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanel4.CustomColors.FlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanel4.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.xPanderPanel4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel4.Image = null;
            this.xPanderPanel4.Name = "xPanderPanel4";
            this.xPanderPanel4.Size = new System.Drawing.Size(300, 25);
            this.xPanderPanel4.TabIndex = 3;
            this.xPanderPanel4.Text = "Symbols";
            this.xPanderPanel4.ToolTipTextCloseIcon = null;
            this.xPanderPanel4.ToolTipTextExpandIconPanelCollapsed = null;
            this.xPanderPanel4.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // ucMilSymbols1
            // 
            this.ucMilSymbols1.A4 = true;
            this.ucMilSymbols1.BackColor = System.Drawing.Color.White;
            this.ucMilSymbols1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.ucMilSymbols1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMilSymbols1.dx = 0;
            this.ucMilSymbols1.dy = 0;
            this.ucMilSymbols1.gridSize = 0;
            this.ucMilSymbols1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.ucMilSymbols1.Location = new System.Drawing.Point(1, 25);
            this.ucMilSymbols1.Name = "ucMilSymbols1";
            this.ucMilSymbols1.ShowDebug = false;
            this.ucMilSymbols1.Size = new System.Drawing.Size(298, 0);
            this.ucMilSymbols1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.ucMilSymbols1.TabIndex = 0;
            this.ucMilSymbols1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.ucMilSymbols1.Zoom = 1F;
            // 
            // xPanderPanel5
            // 
            this.xPanderPanel5.CaptionFont = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.xPanderPanel5.Controls.Add(this.btnCustomLineCap);
            this.xPanderPanel5.Controls.Add(this.chkShowGridXYZ);
            this.xPanderPanel5.Controls.Add(this.btnDemoUcVectShapes_Form);
            this.xPanderPanel5.CustomColors.BackColor = System.Drawing.SystemColors.Control;
            this.xPanderPanel5.CustomColors.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(184)))), ((int)(((byte)(184)))));
            this.xPanderPanel5.CustomColors.CaptionCheckedGradientBegin = System.Drawing.Color.Empty;
            this.xPanderPanel5.CustomColors.CaptionCheckedGradientEnd = System.Drawing.Color.Empty;
            this.xPanderPanel5.CustomColors.CaptionCheckedGradientMiddle = System.Drawing.Color.Empty;
            this.xPanderPanel5.CustomColors.CaptionCloseIcon = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel5.CustomColors.CaptionExpandIcon = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel5.CustomColors.CaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanel5.CustomColors.CaptionGradientEnd = System.Drawing.SystemColors.ButtonFace;
            this.xPanderPanel5.CustomColors.CaptionGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanel5.CustomColors.CaptionPressedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.xPanderPanel5.CustomColors.CaptionPressedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.xPanderPanel5.CustomColors.CaptionPressedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(204)))), ((int)(((byte)(255)))));
            this.xPanderPanel5.CustomColors.CaptionSelectedGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.xPanderPanel5.CustomColors.CaptionSelectedGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.xPanderPanel5.CustomColors.CaptionSelectedGradientMiddle = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(255)))));
            this.xPanderPanel5.CustomColors.CaptionSelectedText = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel5.CustomColors.CaptionText = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel5.CustomColors.FlatCaptionGradientBegin = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.xPanderPanel5.CustomColors.FlatCaptionGradientEnd = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.xPanderPanel5.CustomColors.InnerBorderColor = System.Drawing.SystemColors.Window;
            this.xPanderPanel5.Expand = true;
            this.xPanderPanel5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xPanderPanel5.Image = null;
            this.xPanderPanel5.Name = "xPanderPanel5";
            this.xPanderPanel5.Size = new System.Drawing.Size(300, 380);
            this.xPanderPanel5.TabIndex = 4;
            this.xPanderPanel5.Text = "xPanderPanel5";
            this.xPanderPanel5.ToolTipTextCloseIcon = null;
            this.xPanderPanel5.ToolTipTextExpandIconPanelCollapsed = null;
            this.xPanderPanel5.ToolTipTextExpandIconPanelExpanded = null;
            // 
            // chkShowGridXYZ
            // 
            this.chkShowGridXYZ.AutoSize = true;
            this.chkShowGridXYZ.Location = new System.Drawing.Point(4, 61);
            this.chkShowGridXYZ.Name = "chkShowGridXYZ";
            this.chkShowGridXYZ.Size = new System.Drawing.Size(99, 17);
            this.chkShowGridXYZ.TabIndex = 1;
            this.chkShowGridXYZ.Text = "Show Grid XYZ";
            this.chkShowGridXYZ.UseVisualStyleBackColor = true;
            this.chkShowGridXYZ.CheckedChanged += new System.EventHandler(this.chkShowGridXYZ_CheckedChanged);
            // 
            // btnDemoUcVectShapes_Form
            // 
            this.btnDemoUcVectShapes_Form.Location = new System.Drawing.Point(3, 32);
            this.btnDemoUcVectShapes_Form.Name = "btnDemoUcVectShapes_Form";
            this.btnDemoUcVectShapes_Form.Size = new System.Drawing.Size(188, 23);
            this.btnDemoUcVectShapes_Form.TabIndex = 0;
            this.btnDemoUcVectShapes_Form.Text = "Demo ucVectShapes_Form";
            this.btnDemoUcVectShapes_Form.UseVisualStyleBackColor = true;
            this.btnDemoUcVectShapes_Form.Click += new System.EventHandler(this.btnDemoUcVectShapes_Form_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.ucVectShapes_Map1);
            this.splitContainer2.Panel1.Controls.Add(this.ucToolStrip1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.ucVectShapeToolBox1);
            this.splitContainer2.Size = new System.Drawing.Size(776, 408);
            this.splitContainer2.SplitterDistance = 522;
            this.splitContainer2.TabIndex = 13;
            // 
            // ucVectShapes_Map1
            // 
            this.ucVectShapes_Map1.A4 = true;
            this.ucVectShapes_Map1.AllowDrop = true;
            this.ucVectShapes_Map1.BackColor = System.Drawing.SystemColors.Control;
            this.ucVectShapes_Map1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.ucVectShapes_Map1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVectShapes_Map1.dx = 0;
            this.ucVectShapes_Map1.dy = 0;
            this.ucVectShapes_Map1.gridSize = 0;
            this.ucVectShapes_Map1.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
            this.ucVectShapes_Map1.Location = new System.Drawing.Point(0, 26);
            this.ucVectShapes_Map1.MapZoom = 1F;
            this.ucVectShapes_Map1.Name = "ucVectShapes_Map1";
            this.ucVectShapes_Map1.ShowDebug = false;
            this.ucVectShapes_Map1.Size = new System.Drawing.Size(522, 382);
            this.ucVectShapes_Map1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            this.ucVectShapes_Map1.TabIndex = 1;
            this.ucVectShapes_Map1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.ucVectShapes_Map1.Zoom = 1F;
            // 
            // ucToolStrip1
            // 
            this.ucToolStrip1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ucToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ucToolStrip1.Name = "ucToolStrip1";
            this.ucToolStrip1.Size = new System.Drawing.Size(522, 26);
            this.ucToolStrip1.TabIndex = 0;
            // 
            // ucVectShapeToolBox1
            // 
            this.ucVectShapeToolBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucVectShapeToolBox1.Location = new System.Drawing.Point(0, 0);
            this.ucVectShapeToolBox1.Name = "ucVectShapeToolBox1";
            this.ucVectShapeToolBox1.Size = new System.Drawing.Size(250, 408);
            this.ucVectShapeToolBox1.TabIndex = 1;
            // 
            // statusStripMap
            // 
            this.statusStripMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelCoordinate,
            this.toolStripStatusLabelCoordinate2,
            this.toolStripStatusLabelAltitude,
            this.toolStripStatusLabel3DCoord,
            this.toolStripStatusLabelScreenCoord});
            this.statusStripMap.Location = new System.Drawing.Point(0, 408);
            this.statusStripMap.Name = "statusStripMap";
            this.statusStripMap.Size = new System.Drawing.Size(776, 22);
            this.statusStripMap.TabIndex = 12;
            this.statusStripMap.Text = "statusStrip1";
            // 
            // toolStripStatusLabelCoordinate
            // 
            this.toolStripStatusLabelCoordinate.AutoSize = false;
            this.toolStripStatusLabelCoordinate.Name = "toolStripStatusLabelCoordinate";
            this.toolStripStatusLabelCoordinate.Size = new System.Drawing.Size(150, 17);
            this.toolStripStatusLabelCoordinate.Text = "Map Coordinate";
            this.toolStripStatusLabelCoordinate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelCoordinate2
            // 
            this.toolStripStatusLabelCoordinate2.AutoSize = false;
            this.toolStripStatusLabelCoordinate2.Name = "toolStripStatusLabelCoordinate2";
            this.toolStripStatusLabelCoordinate2.Size = new System.Drawing.Size(150, 17);
            this.toolStripStatusLabelCoordinate2.Text = "Map Coordinate 2";
            this.toolStripStatusLabelCoordinate2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabelAltitude
            // 
            this.toolStripStatusLabelAltitude.AutoSize = false;
            this.toolStripStatusLabelAltitude.Name = "toolStripStatusLabelAltitude";
            this.toolStripStatusLabelAltitude.Size = new System.Drawing.Size(100, 17);
            this.toolStripStatusLabelAltitude.Text = "Zoom";
            this.toolStripStatusLabelAltitude.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel3DCoord
            // 
            this.toolStripStatusLabel3DCoord.AutoSize = false;
            this.toolStripStatusLabel3DCoord.Name = "toolStripStatusLabel3DCoord";
            this.toolStripStatusLabel3DCoord.Size = new System.Drawing.Size(180, 17);
            this.toolStripStatusLabel3DCoord.Text = "3D Coord";
            this.toolStripStatusLabel3DCoord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolStripStatusLabel3DCoord.ToolTipText = "dfdsf";
            // 
            // toolStripStatusLabelScreenCoord
            // 
            this.toolStripStatusLabelScreenCoord.AutoSize = false;
            this.toolStripStatusLabelScreenCoord.Name = "toolStripStatusLabelScreenCoord";
            this.toolStripStatusLabelScreenCoord.Size = new System.Drawing.Size(180, 17);
            this.toolStripStatusLabelScreenCoord.Text = "Screen Coord";
            this.toolStripStatusLabelScreenCoord.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtFilePath);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.btBrowse);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1080, 55);
            this.panel1.TabIndex = 21;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.Location = new System.Drawing.Point(105, 20);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(919, 20);
            this.txtFilePath.TabIndex = 18;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.Color.Transparent;
            this.label23.Location = new System.Drawing.Point(12, 24);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(88, 13);
            this.label23.TabIndex = 15;
            this.label23.Text = "Chọn file .diahinh";
            // 
            // btBrowse
            // 
            this.btBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowse.Location = new System.Drawing.Point(1030, 18);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(38, 23);
            this.btBrowse.TabIndex = 17;
            this.btBrowse.Text = "...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // contextMenuStripMap
            // 
            this.contextMenuStripMap.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopyLonLatCoord,
            this.toolStripMenuItemCopyScreenCoord,
            this.toolStripMenuItemCopy3DCoord});
            this.contextMenuStripMap.Name = "contextMenuStripMap";
            this.contextMenuStripMap.Size = new System.Drawing.Size(179, 70);
            // 
            // toolStripMenuItemCopyLonLatCoord
            // 
            this.toolStripMenuItemCopyLonLatCoord.Name = "toolStripMenuItemCopyLonLatCoord";
            this.toolStripMenuItemCopyLonLatCoord.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItemCopyLonLatCoord.Text = "Copy tọa độ LonLat";
            this.toolStripMenuItemCopyLonLatCoord.Click += new System.EventHandler(this.toolStripMenuItemCopyLonLatCoord_Click);
            // 
            // toolStripMenuItemCopyScreenCoord
            // 
            this.toolStripMenuItemCopyScreenCoord.Name = "toolStripMenuItemCopyScreenCoord";
            this.toolStripMenuItemCopyScreenCoord.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItemCopyScreenCoord.Text = "Copy tọa độ Screen";
            this.toolStripMenuItemCopyScreenCoord.Click += new System.EventHandler(this.toolStripMenuItemCopyScreenCoord_Click);
            // 
            // toolStripMenuItemCopy3DCoord
            // 
            this.toolStripMenuItemCopy3DCoord.Name = "toolStripMenuItemCopy3DCoord";
            this.toolStripMenuItemCopy3DCoord.Size = new System.Drawing.Size(178, 22);
            this.toolStripMenuItemCopy3DCoord.Text = "Copy tọa độ 3D";
            this.toolStripMenuItemCopy3DCoord.Click += new System.EventHandler(this.toolStripMenuItemCopy3DCoord_Click);
            // 
            // btnCustomLineCap
            // 
            this.btnCustomLineCap.Location = new System.Drawing.Point(11, 84);
            this.btnCustomLineCap.Name = "btnCustomLineCap";
            this.btnCustomLineCap.Size = new System.Drawing.Size(75, 23);
            this.btnCustomLineCap.TabIndex = 2;
            this.btnCustomLineCap.Text = "CustomLineCap";
            this.btnCustomLineCap.UseVisualStyleBackColor = true;
            this.btnCustomLineCap.Click += new System.EventHandler(this.btnCustomLineCap_Click);
            // 
            // frmBDTCShapeViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1080, 485);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "frmBDTCShapeViewer";
            this.Text = "BDTC Viewer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmBDTCShapeViewer_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.xPanderPanelList1.ResumeLayout(false);
            this.xPanderPanel3DCoordinate.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.xPanderPanel4.ResumeLayout(false);
            this.xPanderPanel5.ResumeLayout(false);
            this.xPanderPanel5.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.statusStripMap.ResumeLayout(false);
            this.statusStripMap.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStripMap.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private BSE.Windows.Forms.XPanderPanelList xPanderPanelList1;
        private BSE.Windows.Forms.XPanderPanel xPanderPanel3DCoordinate;
        private BSE.Windows.Forms.XPanderPanel xPanderPanel4;
        private BSE.Windows.Forms.XPanderPanel xPanderPanel5;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtX3D;
        private System.Windows.Forms.TextBox txtY3D;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtSceenX;
        private System.Windows.Forms.TextBox txtScreenY;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtLongitude;
        private System.Windows.Forms.TextBox txtLatitude;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label23;
        internal System.Windows.Forms.Button btBrowse;
        internal System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.StatusStrip statusStripMap;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCoordinate;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelCoordinate2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAltitude;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3DCoord;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelScreenCoord;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private HT.VectorShapes.Maps.ucMapVectShapeToolBox ucVectShapeToolBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripMap;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopyLonLatCoord;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopyScreenCoord;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy3DCoord;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.CheckBox chkShowGridXYZ;
        private System.Windows.Forms.Button btnDemoUcVectShapes_Form;
        private HT.VectorShapes.Maps.ucMapToolStrip ucToolStrip1;
        private HT.VectorShapes.Maps.ucVectShapes_Map ucVectShapes_Map1;
        private HT.MilSymbols.ucMilSymbols ucMilSymbols1;
        private System.Windows.Forms.Button btnCustomLineCap;
    }
}