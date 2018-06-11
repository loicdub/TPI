namespace fingers_cloner
{
    partial class frmEdit
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.tbxDescription = new System.Windows.Forms.TextBox();
            this.btnImage = new System.Windows.Forms.Button();
            this.btnValidate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nom :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Description :";
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(84, 12);
            this.tbxName.MaxLength = 24;
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(188, 20);
            this.tbxName.TabIndex = 2;
            this.tbxName.TextChanged += new System.EventHandler(this.tbxName_TextChanged);
            // 
            // tbxDescription
            // 
            this.tbxDescription.Location = new System.Drawing.Point(84, 38);
            this.tbxDescription.Multiline = true;
            this.tbxDescription.Name = "tbxDescription";
            this.tbxDescription.Size = new System.Drawing.Size(188, 128);
            this.tbxDescription.TabIndex = 3;
            this.tbxDescription.TextChanged += new System.EventHandler(this.tbxDescription_TextChanged);
            // 
            // btnImage
            // 
            this.btnImage.Location = new System.Drawing.Point(15, 172);
            this.btnImage.Name = "btnImage";
            this.btnImage.Size = new System.Drawing.Size(257, 23);
            this.btnImage.TabIndex = 5;
            this.btnImage.Text = "Choisir une image...";
            this.btnImage.UseVisualStyleBackColor = true;
            // 
            // btnValidate
            // 
            this.btnValidate.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnValidate.Enabled = false;
            this.btnValidate.Location = new System.Drawing.Point(15, 201);
            this.btnValidate.Name = "btnValidate";
            this.btnValidate.Size = new System.Drawing.Size(257, 23);
            this.btnValidate.TabIndex = 6;
            this.btnValidate.Text = "Valider";
            this.btnValidate.UseVisualStyleBackColor = true;
            // 
            // frmEdit
            // 
            this.AcceptButton = this.btnImage;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 236);
            this.Controls.Add(this.btnValidate);
            this.Controls.Add(this.btnImage);
            this.Controls.Add(this.tbxDescription);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Editer la position";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.TextBox tbxDescription;
        private System.Windows.Forms.Button btnImage;
        private System.Windows.Forms.Button btnValidate;
    }
}