using System.Collections.Generic;
using DienTapLib;
using System.Drawing;

namespace DangKyModels
{
    partial class Form1
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.btnLoadPara = new System.Windows.Forms.Button();
            this.Splitter1 = new System.Windows.Forms.Splitter();
            this.Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Panel1.Controls.Add(this.btnLoadPara);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.Panel1.Location = new System.Drawing.Point(0, 0);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(293, 47);
            this.Panel1.TabIndex = 0;
            // 
            // btnLoadPara
            // 
            this.btnLoadPara.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadPara.Location = new System.Drawing.Point(10, 10);
            this.btnLoadPara.Name = "btnLoadPara";
            this.btnLoadPara.Size = new System.Drawing.Size(92, 23);
            this.btnLoadPara.TabIndex = 24;
            this.btnLoadPara.Text = "Sa bàn khác";
            this.btnLoadPara.UseVisualStyleBackColor = true;
            this.btnLoadPara.Click += new System.EventHandler(this.btnLoadPara_Click);
            // 
            // Splitter1
            // 
            this.Splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.Splitter1.Location = new System.Drawing.Point(-3, 0);
            this.Splitter1.Name = "Splitter1";
            this.Splitter1.Size = new System.Drawing.Size(3, 47);
            this.Splitter1.TabIndex = 1;
            this.Splitter1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(293, 47);
            this.Controls.Add(this.Splitter1);
            this.Controls.Add(this.Panel1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel Panel1;

        private System.Windows.Forms.Splitter Splitter1;

        private System.Windows.Forms.Button btnLoadPara;

        private List<CModelDef> ModelDefs;

        private CModelDef CurrDef;

        private double myPixelsPerGridX = 0;

        private double myPixelsPerGridY = 0;

        private CTerrain myDiaHinh;

        private Bitmap m_TexImage;

        private float[,] heightData;
      
    }
}