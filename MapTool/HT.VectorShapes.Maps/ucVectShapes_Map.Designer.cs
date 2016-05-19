namespace HT.VectorShapes.Maps
{
    partial class ucVectShapes_Map
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
            this.ucMap1 = new HT.VectorShapes.Maps.ucMap();
            this.SuspendLayout();
            // 
            // ucMap1
            // 
            this.ucMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucMap1.Location = new System.Drawing.Point(0, 0);
            this.ucMap1.Name = "ucMap1";
            this.ucMap1.Size = new System.Drawing.Size(334, 312);
            this.ucMap1.TabIndex = 0;
            // 
            // ucVectShapes_Map
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.ucMap1);
            this.Name = "ucVectShapes_Map";
            this.ResumeLayout(false);

        }

        #endregion

        public ucMap ucMap1;
    }
}
