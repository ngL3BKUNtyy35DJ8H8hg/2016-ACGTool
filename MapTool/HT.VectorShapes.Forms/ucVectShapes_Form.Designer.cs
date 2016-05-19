namespace HT.VectorShapes.Forms
{
    partial class ucVectShapes_Form
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
            this.SuspendLayout();
            // 
            // ucVectShapeMap
            // 
            this.AllowDrop = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Name = "ucVectShapes_Form";
            this.Size = new System.Drawing.Size(334, 312);
            this.Load += new System.EventHandler(this.ucVectShapes_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucVectShapes_Paint);
            this.Resize += new System.EventHandler(this.ucVectShapes_Resize);
            this.ResumeLayout(false);

        }

        #endregion






    }
}
