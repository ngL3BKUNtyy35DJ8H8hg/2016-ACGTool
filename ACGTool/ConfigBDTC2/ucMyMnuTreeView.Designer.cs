namespace ConfigBDTC
{
    partial class ucMyMnuTreeView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMyMnuTreeView));
            this.treeViewScript = new System.Windows.Forms.TreeView();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // treeViewScript
            // 
            this.treeViewScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewScript.Location = new System.Drawing.Point(0, 0);
            this.treeViewScript.Name = "treeViewScript";
            this.treeViewScript.Size = new System.Drawing.Size(350, 438);
            this.treeViewScript.TabIndex = 19;
            this.treeViewScript.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewMyMnu_AfterSelect);
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList1.Images.SetKeyName(0, "Folder.ICO");
            this.ImageList1.Images.SetKeyName(1, "item.png");
            // 
            // ucMyMnuTreeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.treeViewScript);
            this.Name = "ucMyMnuTreeView";
            this.Size = new System.Drawing.Size(350, 438);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView treeViewScript;
        internal System.Windows.Forms.ImageList ImageList1;
    }
}
