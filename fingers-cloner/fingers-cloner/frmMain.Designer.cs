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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblUserHand = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlUserHand = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnNewModel = new System.Windows.Forms.Button();
            this.cbxModele = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lblUserHand
            // 
            this.lblUserHand.AutoSize = true;
            this.lblUserHand.Location = new System.Drawing.Point(9, 9);
            this.lblUserHand.Name = "lblUserHand";
            this.lblUserHand.Size = new System.Drawing.Size(63, 13);
            this.lblUserHand.TabIndex = 0;
            this.lblUserHand.Text = "Votre main :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(415, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Main à recopier";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnlUserHand
            // 
            this.pnlUserHand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlUserHand.Location = new System.Drawing.Point(12, 25);
            this.pnlUserHand.Name = "pnlUserHand";
            this.pnlUserHand.Size = new System.Drawing.Size(400, 400);
            this.pnlUserHand.TabIndex = 6;
            this.pnlUserHand.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlUserHand_Paint);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Location = new System.Drawing.Point(418, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(400, 400);
            this.panel2.TabIndex = 7;
            // 
            // btnNewModel
            // 
            this.btnNewModel.Location = new System.Drawing.Point(12, 431);
            this.btnNewModel.Name = "btnNewModel";
            this.btnNewModel.Size = new System.Drawing.Size(200, 23);
            this.btnNewModel.TabIndex = 8;
            this.btnNewModel.Text = "Enregistrez un nouveau modèle";
            this.btnNewModel.UseVisualStyleBackColor = true;
            this.btnNewModel.Click += new System.EventHandler(this.btnNewModel_Click);
            // 
            // cbxModele
            // 
            this.cbxModele.FormattingEnabled = true;
            this.cbxModele.Location = new System.Drawing.Point(618, 433);
            this.cbxModele.Name = "cbxModele";
            this.cbxModele.Size = new System.Drawing.Size(200, 21);
            this.cbxModele.TabIndex = 9;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 466);
            this.Controls.Add(this.cbxModele);
            this.Controls.Add(this.btnNewModel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlUserHand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblUserHand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Finger\'s cloner";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblUserHand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlUserHand;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnNewModel;
        private System.Windows.Forms.ComboBox cbxModele;
    }
}

