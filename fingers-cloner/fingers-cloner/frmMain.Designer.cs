namespace fingers_cloner
{
    partial class frmMain
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblPalmPos = new System.Windows.Forms.Label();
            this.lblThumbPos = new System.Windows.Forms.Label();
            this.lblIndexPos = new System.Windows.Forms.Label();
            this.lblMiddlePos = new System.Windows.Forms.Label();
            this.lblRingPos = new System.Windows.Forms.Label();
            this.lblPinkyPos = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPalmPos
            // 
            this.lblPalmPos.AutoSize = true;
            this.lblPalmPos.Location = new System.Drawing.Point(12, 9);
            this.lblPalmPos.Name = "lblPalmPos";
            this.lblPalmPos.Size = new System.Drawing.Size(30, 13);
            this.lblPalmPos.TabIndex = 0;
            this.lblPalmPos.Text = "Palm";
            // 
            // lblThumbPos
            // 
            this.lblThumbPos.AutoSize = true;
            this.lblThumbPos.Location = new System.Drawing.Point(12, 36);
            this.lblThumbPos.Name = "lblThumbPos";
            this.lblThumbPos.Size = new System.Drawing.Size(40, 13);
            this.lblThumbPos.TabIndex = 1;
            this.lblThumbPos.Text = "Thumb";
            // 
            // lblIndexPos
            // 
            this.lblIndexPos.AutoSize = true;
            this.lblIndexPos.Location = new System.Drawing.Point(12, 68);
            this.lblIndexPos.Name = "lblIndexPos";
            this.lblIndexPos.Size = new System.Drawing.Size(33, 13);
            this.lblIndexPos.TabIndex = 2;
            this.lblIndexPos.Text = "Index";
            // 
            // lblMiddlePos
            // 
            this.lblMiddlePos.AutoSize = true;
            this.lblMiddlePos.Location = new System.Drawing.Point(12, 106);
            this.lblMiddlePos.Name = "lblMiddlePos";
            this.lblMiddlePos.Size = new System.Drawing.Size(38, 13);
            this.lblMiddlePos.TabIndex = 3;
            this.lblMiddlePos.Text = "Middle";
            // 
            // lblRingPos
            // 
            this.lblRingPos.AutoSize = true;
            this.lblRingPos.Location = new System.Drawing.Point(12, 151);
            this.lblRingPos.Name = "lblRingPos";
            this.lblRingPos.Size = new System.Drawing.Size(29, 13);
            this.lblRingPos.TabIndex = 4;
            this.lblRingPos.Text = "Ring";
            // 
            // lblPinkyPos
            // 
            this.lblPinkyPos.AutoSize = true;
            this.lblPinkyPos.Location = new System.Drawing.Point(12, 197);
            this.lblPinkyPos.Name = "lblPinkyPos";
            this.lblPinkyPos.Size = new System.Drawing.Size(33, 13);
            this.lblPinkyPos.TabIndex = 5;
            this.lblPinkyPos.Text = "Pinky";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(761, 487);
            this.Controls.Add(this.lblPinkyPos);
            this.Controls.Add(this.lblRingPos);
            this.Controls.Add(this.lblMiddlePos);
            this.Controls.Add(this.lblIndexPos);
            this.Controls.Add(this.lblThumbPos);
            this.Controls.Add(this.lblPalmPos);
            this.Name = "frmMain";
            this.Text = "Finger\'s cloner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblPalmPos;
        private System.Windows.Forms.Label lblThumbPos;
        private System.Windows.Forms.Label lblIndexPos;
        private System.Windows.Forms.Label lblMiddlePos;
        private System.Windows.Forms.Label lblRingPos;
        private System.Windows.Forms.Label lblPinkyPos;
    }
}

