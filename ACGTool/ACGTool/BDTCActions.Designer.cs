﻿namespace ACGTool
{
    partial class BDTCAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BDTCAction));
            this.OpenFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel8 = new System.Windows.Forms.Panel();
            this.comboBoxActionFilter = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtObjName = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btBrowse = new System.Windows.Forms.Button();
            this.buttonScript = new System.Windows.Forms.Button();
            this.openFileDialogScriptPath = new System.Windows.Forms.OpenFileDialog();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.dataGridViewAttributes = new System.Windows.Forms.DataGridView();
            this.Select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Attribute = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBoxReplaceText = new System.Windows.Forms.CheckBox();
            this.btnUpdateAll = new System.Windows.Forms.Button();
            this.labelReplaceWith = new System.Windows.Forms.Label();
            this.labelFindWhat = new System.Windows.Forms.Label();
            this.txtReplaceWith = new System.Windows.Forms.TextBox();
            this.txtFindWhat = new System.Windows.Forms.TextBox();
            this.treeViewScript = new System.Windows.Forms.TreeView();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.listBoxActions = new System.Windows.Forms.ListBox();
            this.richTextBoxScript = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnSaveXml = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.tabPageCheckXmlFiles = new System.Windows.Forms.TabPage();
            this.listViewXmlFiles = new System.Windows.Forms.ListView();
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnCheckXmlFiles = new System.Windows.Forms.Button();
            this.btnDeleteXmlFile = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelAction = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnDefaultValues = new System.Windows.Forms.Button();
            this.comboBoxDichTa = new System.Windows.Forms.ComboBox();
            this.panel8.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttributes)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPageCheckXmlFiles.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.comboBoxActionFilter);
            this.panel8.Controls.Add(this.label8);
            this.panel8.Controls.Add(this.label7);
            this.panel8.Controls.Add(this.txtObjName);
            this.panel8.Controls.Add(this.Label2);
            this.panel8.Controls.Add(this.txtFilePath);
            this.panel8.Controls.Add(this.btBrowse);
            this.panel8.Controls.Add(this.buttonScript);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel8.Location = new System.Drawing.Point(0, 0);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1175, 77);
            this.panel8.TabIndex = 1;
            // 
            // comboBoxActionFilter
            // 
            this.comboBoxActionFilter.FormattingEnabled = true;
            this.comboBoxActionFilter.Items.AddRange(new object[] {
            "Description dưới",
            "Description giữa",
            "Description trên",
            "Bombard",
            "Shoot",
            "FocusAt",
            "Fly",
            "Move",
            "CornerTitle",
            "Explode",
            "ExplodeDcl"});
            this.comboBoxActionFilter.Location = new System.Drawing.Point(392, 42);
            this.comboBoxActionFilter.Name = "comboBoxActionFilter";
            this.comboBoxActionFilter.Size = new System.Drawing.Size(168, 21);
            this.comboBoxActionFilter.TabIndex = 24;
            this.comboBoxActionFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxActionFilter_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(349, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Action";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(58, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(51, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "ObjName";
            // 
            // txtObjName
            // 
            this.txtObjName.Location = new System.Drawing.Point(115, 42);
            this.txtObjName.Name = "txtObjName";
            this.txtObjName.Size = new System.Drawing.Size(204, 20);
            this.txtObjName.TabIndex = 22;
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
            this.txtFilePath.Size = new System.Drawing.Size(922, 20);
            this.txtFilePath.TabIndex = 19;
            // 
            // btBrowse
            // 
            this.btBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowse.Location = new System.Drawing.Point(1043, 9);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(38, 23);
            this.btBrowse.TabIndex = 20;
            this.btBrowse.Text = "...";
            this.btBrowse.UseVisualStyleBackColor = true;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // buttonScript
            // 
            this.buttonScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonScript.Location = new System.Drawing.Point(575, 40);
            this.buttonScript.Name = "buttonScript";
            this.buttonScript.Size = new System.Drawing.Size(76, 23);
            this.buttonScript.TabIndex = 15;
            this.buttonScript.Text = "Load ";
            this.buttonScript.UseVisualStyleBackColor = true;
            this.buttonScript.Click += new System.EventHandler(this.buttonScript_Click);
            // 
            // openFileDialogScriptPath
            // 
            this.openFileDialogScriptPath.FileName = "openFileDialogScriptPath";
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "Folder.ICO");
            this.ImageList1.Images.SetKeyName(1, "item.png");
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPageCheckXmlFiles);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1175, 564);
            this.tabControl1.TabIndex = 18;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1167, 538);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Script files";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1161, 532);
            this.splitContainer1.SplitterDistance = 234;
            this.splitContainer1.TabIndex = 19;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.dataGridViewAttributes);
            this.splitContainer3.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.treeViewScript);
            this.splitContainer3.Size = new System.Drawing.Size(1161, 234);
            this.splitContainer3.SplitterDistance = 705;
            this.splitContainer3.TabIndex = 1;
            // 
            // dataGridViewAttributes
            // 
            this.dataGridViewAttributes.AllowUserToAddRows = false;
            this.dataGridViewAttributes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridViewAttributes.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridViewAttributes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAttributes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Select,
            this.Attribute,
            this.Value});
            this.dataGridViewAttributes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewAttributes.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewAttributes.Name = "dataGridViewAttributes";
            this.dataGridViewAttributes.Size = new System.Drawing.Size(705, 134);
            this.dataGridViewAttributes.TabIndex = 2;
            this.dataGridViewAttributes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewAttributes_CellContentClick);
            this.dataGridViewAttributes.CurrentCellChanged += new System.EventHandler(this.dataGridViewAttributes_CurrentCellChanged);
            // 
            // Select
            // 
            this.Select.HeaderText = "Select";
            this.Select.Name = "Select";
            this.Select.Width = 43;
            // 
            // Attribute
            // 
            this.Attribute.HeaderText = "Attribute";
            this.Attribute.Name = "Attribute";
            this.Attribute.Width = 71;
            // 
            // Value
            // 
            this.Value.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBoxDichTa);
            this.panel1.Controls.Add(this.checkBoxReplaceText);
            this.panel1.Controls.Add(this.btnDefaultValues);
            this.panel1.Controls.Add(this.btnUpdateAll);
            this.panel1.Controls.Add(this.labelReplaceWith);
            this.panel1.Controls.Add(this.labelFindWhat);
            this.panel1.Controls.Add(this.txtReplaceWith);
            this.panel1.Controls.Add(this.txtFindWhat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(705, 100);
            this.panel1.TabIndex = 1;
            // 
            // checkBoxReplaceText
            // 
            this.checkBoxReplaceText.AutoSize = true;
            this.checkBoxReplaceText.Location = new System.Drawing.Point(6, 17);
            this.checkBoxReplaceText.Name = "checkBoxReplaceText";
            this.checkBoxReplaceText.Size = new System.Drawing.Size(90, 17);
            this.checkBoxReplaceText.TabIndex = 1;
            this.checkBoxReplaceText.Text = "Replace Text";
            this.checkBoxReplaceText.UseVisualStyleBackColor = true;
            this.checkBoxReplaceText.CheckedChanged += new System.EventHandler(this.checkBoxReplaceText_CheckedChanged);
            // 
            // btnUpdateAll
            // 
            this.btnUpdateAll.Location = new System.Drawing.Point(102, 11);
            this.btnUpdateAll.Name = "btnUpdateAll";
            this.btnUpdateAll.Size = new System.Drawing.Size(82, 23);
            this.btnUpdateAll.TabIndex = 0;
            this.btnUpdateAll.Text = "Update All";
            this.btnUpdateAll.UseVisualStyleBackColor = true;
            this.btnUpdateAll.Click += new System.EventHandler(this.btnUpdateAll_Click);
            // 
            // labelReplaceWith
            // 
            this.labelReplaceWith.AutoSize = true;
            this.labelReplaceWith.BackColor = System.Drawing.Color.Transparent;
            this.labelReplaceWith.Location = new System.Drawing.Point(10, 71);
            this.labelReplaceWith.Name = "labelReplaceWith";
            this.labelReplaceWith.Size = new System.Drawing.Size(69, 13);
            this.labelReplaceWith.TabIndex = 21;
            this.labelReplaceWith.Text = "Replace with";
            // 
            // labelFindWhat
            // 
            this.labelFindWhat.AutoSize = true;
            this.labelFindWhat.BackColor = System.Drawing.Color.Transparent;
            this.labelFindWhat.Location = new System.Drawing.Point(10, 43);
            this.labelFindWhat.Name = "labelFindWhat";
            this.labelFindWhat.Size = new System.Drawing.Size(53, 13);
            this.labelFindWhat.TabIndex = 21;
            this.labelFindWhat.Text = "Find what";
            // 
            // txtReplaceWith
            // 
            this.txtReplaceWith.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReplaceWith.Location = new System.Drawing.Point(88, 68);
            this.txtReplaceWith.Name = "txtReplaceWith";
            this.txtReplaceWith.Size = new System.Drawing.Size(602, 20);
            this.txtReplaceWith.TabIndex = 22;
            // 
            // txtFindWhat
            // 
            this.txtFindWhat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFindWhat.Location = new System.Drawing.Point(88, 40);
            this.txtFindWhat.Name = "txtFindWhat";
            this.txtFindWhat.Size = new System.Drawing.Size(602, 20);
            this.txtFindWhat.TabIndex = 22;
            // 
            // treeViewScript
            // 
            this.treeViewScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewScript.HideSelection = false;
            this.treeViewScript.Location = new System.Drawing.Point(0, 0);
            this.treeViewScript.Name = "treeViewScript";
            this.treeViewScript.Size = new System.Drawing.Size(452, 234);
            this.treeViewScript.TabIndex = 18;
            this.treeViewScript.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewScript_AfterSelect);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.listBoxActions);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.richTextBoxScript);
            this.splitContainer2.Panel2.Controls.Add(this.panel2);
            this.splitContainer2.Size = new System.Drawing.Size(1161, 294);
            this.splitContainer2.SplitterDistance = 130;
            this.splitContainer2.TabIndex = 40;
            // 
            // listBoxActions
            // 
            this.listBoxActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxActions.FormattingEnabled = true;
            this.listBoxActions.HorizontalScrollbar = true;
            this.listBoxActions.Location = new System.Drawing.Point(0, 0);
            this.listBoxActions.Name = "listBoxActions";
            this.listBoxActions.Size = new System.Drawing.Size(1161, 130);
            this.listBoxActions.TabIndex = 0;
            this.listBoxActions.SelectedIndexChanged += new System.EventHandler(this.listBoxActions_SelectedIndexChanged);
            // 
            // richTextBoxScript
            // 
            this.richTextBoxScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxScript.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxScript.HideSelection = false;
            this.richTextBoxScript.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxScript.Name = "richTextBoxScript";
            this.richTextBoxScript.Size = new System.Drawing.Size(1082, 160);
            this.richTextBoxScript.TabIndex = 39;
            this.richTextBoxScript.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBox1);
            this.panel2.Controls.Add(this.btnSaveXml);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(1082, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(79, 160);
            this.panel2.TabIndex = 40;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(6, 17);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(68, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Reset ID";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnSaveXml
            // 
            this.btnSaveXml.Location = new System.Drawing.Point(6, 40);
            this.btnSaveXml.Name = "btnSaveXml";
            this.btnSaveXml.Size = new System.Drawing.Size(65, 23);
            this.btnSaveXml.TabIndex = 0;
            this.btnSaveXml.Text = "Save Xml";
            this.btnSaveXml.UseVisualStyleBackColor = true;
            this.btnSaveXml.Click += new System.EventHandler(this.btnSaveXml_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBoxLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1167, 538);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLog.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxLog.HideSelection = false;
            this.richTextBoxLog.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(1161, 532);
            this.richTextBoxLog.TabIndex = 40;
            this.richTextBoxLog.Text = "";
            // 
            // tabPageCheckXmlFiles
            // 
            this.tabPageCheckXmlFiles.Controls.Add(this.listViewXmlFiles);
            this.tabPageCheckXmlFiles.Controls.Add(this.panel7);
            this.tabPageCheckXmlFiles.Location = new System.Drawing.Point(4, 22);
            this.tabPageCheckXmlFiles.Name = "tabPageCheckXmlFiles";
            this.tabPageCheckXmlFiles.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCheckXmlFiles.Size = new System.Drawing.Size(1167, 538);
            this.tabPageCheckXmlFiles.TabIndex = 3;
            this.tabPageCheckXmlFiles.Text = "Check Xml Files";
            this.tabPageCheckXmlFiles.UseVisualStyleBackColor = true;
            // 
            // listViewXmlFiles
            // 
            this.listViewXmlFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14});
            this.listViewXmlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewXmlFiles.FullRowSelect = true;
            this.listViewXmlFiles.GridLines = true;
            this.listViewXmlFiles.Location = new System.Drawing.Point(3, 54);
            this.listViewXmlFiles.Name = "listViewXmlFiles";
            this.listViewXmlFiles.Size = new System.Drawing.Size(1161, 481);
            this.listViewXmlFiles.TabIndex = 23;
            this.listViewXmlFiles.UseCompatibleStateImageBehavior = false;
            this.listViewXmlFiles.View = System.Windows.Forms.View.Details;
            this.listViewXmlFiles.SelectedIndexChanged += new System.EventHandler(this.listViewNotUsingXml_SelectedIndexChanged);
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "File";
            this.columnHeader12.Width = 235;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Full Path";
            this.columnHeader13.Width = 461;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Status";
            this.columnHeader14.Width = 107;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnCheckXmlFiles);
            this.panel7.Controls.Add(this.btnDeleteXmlFile);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(3, 3);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(1161, 51);
            this.panel7.TabIndex = 22;
            // 
            // btnCheckXmlFiles
            // 
            this.btnCheckXmlFiles.Location = new System.Drawing.Point(5, 12);
            this.btnCheckXmlFiles.Name = "btnCheckXmlFiles";
            this.btnCheckXmlFiles.Size = new System.Drawing.Size(174, 23);
            this.btnCheckXmlFiles.TabIndex = 21;
            this.btnCheckXmlFiles.Text = "Check Xml Files";
            this.btnCheckXmlFiles.UseVisualStyleBackColor = true;
            this.btnCheckXmlFiles.Click += new System.EventHandler(this.btnCheckXmlFiles_Click_1);
            // 
            // btnDeleteXmlFile
            // 
            this.btnDeleteXmlFile.Location = new System.Drawing.Point(205, 12);
            this.btnDeleteXmlFile.Name = "btnDeleteXmlFile";
            this.btnDeleteXmlFile.Size = new System.Drawing.Size(174, 23);
            this.btnDeleteXmlFile.TabIndex = 20;
            this.btnDeleteXmlFile.Text = "Xóa file .xml không dùng";
            this.btnDeleteXmlFile.UseVisualStyleBackColor = true;
            this.btnDeleteXmlFile.Click += new System.EventHandler(this.btnDeleteXmlFile_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tabControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 77);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1175, 564);
            this.panel3.TabIndex = 18;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelAction});
            this.statusStrip1.Location = new System.Drawing.Point(0, 641);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1175, 22);
            this.statusStrip1.TabIndex = 19;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelAction
            // 
            this.toolStripStatusLabelAction.Name = "toolStripStatusLabelAction";
            this.toolStripStatusLabelAction.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabelAction.Text = "toolStripStatusLabel1";
            // 
            // btnDefaultValues
            // 
            this.btnDefaultValues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDefaultValues.Location = new System.Drawing.Point(608, 6);
            this.btnDefaultValues.Name = "btnDefaultValues";
            this.btnDefaultValues.Size = new System.Drawing.Size(82, 23);
            this.btnDefaultValues.TabIndex = 0;
            this.btnDefaultValues.Text = "Defaults";
            this.btnDefaultValues.UseVisualStyleBackColor = true;
            this.btnDefaultValues.Click += new System.EventHandler(this.btnDefaultValues_Click);
            // 
            // comboBoxDichTa
            // 
            this.comboBoxDichTa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxDichTa.FormattingEnabled = true;
            this.comboBoxDichTa.Items.AddRange(new object[] {
            "Ta",
            "Dich"});
            this.comboBoxDichTa.Location = new System.Drawing.Point(482, 7);
            this.comboBoxDichTa.Name = "comboBoxDichTa";
            this.comboBoxDichTa.Size = new System.Drawing.Size(121, 21);
            this.comboBoxDichTa.TabIndex = 23;
            // 
            // BDTCAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1175, 663);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.statusStrip1);
            this.Name = "BDTCAction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BDTCTimeLine";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.BDTCAction_Load);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAttributes)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPageCheckXmlFiles.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.OpenFileDialog OpenFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.OpenFileDialog openFileDialogScriptPath;
        internal System.Windows.Forms.Button buttonScript;
        internal System.Windows.Forms.ImageList ImageList1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TreeView treeViewScript;
        private System.Windows.Forms.Panel panel3;
        internal System.Windows.Forms.Label Label2;
        internal System.Windows.Forms.TextBox txtFilePath;
        internal System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.TextBox txtObjName;
        private System.Windows.Forms.ComboBox comboBoxActionFilter;
        internal System.Windows.Forms.Label label8;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ListBox listBoxActions;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView dataGridViewAttributes;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnUpdateAll;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSaveXml;
        private System.Windows.Forms.RichTextBox richTextBoxScript;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAction;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBoxReplaceText;
        internal System.Windows.Forms.Label labelReplaceWith;
        internal System.Windows.Forms.Label labelFindWhat;
        internal System.Windows.Forms.TextBox txtReplaceWith;
        internal System.Windows.Forms.TextBox txtFindWhat;
        private System.Windows.Forms.TabPage tabPageCheckXmlFiles;
        private System.Windows.Forms.ListView listViewXmlFiles;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnDeleteXmlFile;
        private System.Windows.Forms.Button btnCheckXmlFiles;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Select;
        private System.Windows.Forms.DataGridViewTextBoxColumn Attribute;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.Button btnDefaultValues;
        private System.Windows.Forms.ComboBox comboBoxDichTa;
    }
}