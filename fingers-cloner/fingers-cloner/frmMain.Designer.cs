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
            this.pnlModelHand = new System.Windows.Forms.Panel();
            this.btnNewModel = new System.Windows.Forms.Button();
            this.cbxModele = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPercentage = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
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
            // pnlModelHand
            // 
            this.pnlModelHand.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlModelHand.Location = new System.Drawing.Point(418, 25);
            this.pnlModelHand.Name = "pnlModelHand";
            this.pnlModelHand.Size = new System.Drawing.Size(400, 400);
            this.pnlModelHand.TabIndex = 7;
            this.pnlModelHand.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlModelHand_Paint);
            // 
            // btnNewModel
            // 
            this.btnNewModel.Enabled = false;
            this.btnNewModel.Location = new System.Drawing.Point(12, 431);
            this.btnNewModel.Name = "btnNewModel";
            this.btnNewModel.Size = new System.Drawing.Size(397, 23);
            this.btnNewModel.TabIndex = 0;
            this.btnNewModel.Text = "Enregistrer ma position actuelle";
            this.btnNewModel.UseVisualStyleBackColor = true;
            this.btnNewModel.Click += new System.EventHandler(this.btnNewModel_Click);
            // 
            // cbxModele
            // 
            this.cbxModele.FormattingEnabled = true;
            this.cbxModele.Location = new System.Drawing.Point(180, 460);
            this.cbxModele.Name = "cbxModele";
            this.cbxModele.Size = new System.Drawing.Size(229, 21);
            this.cbxModele.Sorted = true;
            this.cbxModele.TabIndex = 2;
            this.cbxModele.SelectedIndexChanged += new System.EventHandler(this.cbxModele_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 463);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(166, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Sélectionner un modèle existant : ";
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(570, 431);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(248, 45);
            this.trackBar1.TabIndex = 1;
            this.trackBar1.TickFrequency = 10;
            this.trackBar1.Value = 50;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(415, 436);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Niveau de précision attendu : ";
            // 
            // lblPercentage
            // 
            this.lblPercentage.AutoSize = true;
            this.lblPercentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPercentage.Location = new System.Drawing.Point(514, 458);
            this.lblPercentage.Name = "lblPercentage";
            this.lblPercentage.Size = new System.Drawing.Size(41, 20);
            this.lblPercentage.TabIndex = 13;
            this.lblPercentage.Text = "50%";
            this.lblPercentage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnNewModel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 493);
            this.Controls.Add(this.cbxModele);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblPercentage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.btnNewModel);
            this.Controls.Add(this.pnlModelHand);
            this.Controls.Add(this.pnlUserHand);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblUserHand);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.Text = "Finger\'s cloner";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblUserHand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlUserHand;
        private System.Windows.Forms.Panel pnlModelHand;
        private System.Windows.Forms.Button btnNewModel;
        private System.Windows.Forms.ComboBox cbxModele;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPercentage;
    }
}

