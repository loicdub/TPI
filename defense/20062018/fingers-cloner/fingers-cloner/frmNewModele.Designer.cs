﻿namespace fingers_cloner
{
    partial class frmNewModele
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
            this.pnlModele = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxModeleName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.lblFileName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnlModele
            // 
            this.pnlModele.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlModele.Location = new System.Drawing.Point(12, 12);
            this.pnlModele.Name = "pnlModele";
            this.pnlModele.Size = new System.Drawing.Size(400, 400);
            this.pnlModele.TabIndex = 0;
            this.pnlModele.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlModele_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 421);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(199, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nom de la position (24 caractères max) : ";
            // 
            // tbxModeleName
            // 
            this.tbxModeleName.Location = new System.Drawing.Point(215, 418);
            this.tbxModeleName.MaxLength = 24;
            this.tbxModeleName.Name = "tbxModeleName";
            this.tbxModeleName.Size = new System.Drawing.Size(197, 20);
            this.tbxModeleName.TabIndex = 0;
            this.tbxModeleName.TextChanged += new System.EventHandler(this.tbxModeleName_TextChanged);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Enabled = false;
            this.btnSave.Location = new System.Drawing.Point(12, 473);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(400, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Enregistrer la position";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(12, 444);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(199, 23);
            this.btnLoadImage.TabIndex = 0;
            this.btnLoadImage.Text = "Charger une image (optionnel)...";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // lblFileName
            // 
            this.lblFileName.Location = new System.Drawing.Point(217, 449);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(195, 13);
            this.lblFileName.TabIndex = 2;
            this.lblFileName.Text = "lblFileName";
            this.lblFileName.Visible = false;
            // 
            // frmNewModele
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 508);
            this.Controls.Add(this.lblFileName);
            this.Controls.Add(this.btnLoadImage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbxModeleName);
            this.Controls.Add(this.pnlModele);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNewModele";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Créer un nouveau modèle";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlModele;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxModeleName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoadImage;
        private System.Windows.Forms.Label lblFileName;
    }
}