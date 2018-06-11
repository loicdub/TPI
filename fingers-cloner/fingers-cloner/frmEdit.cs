using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fingers_cloner
{
    public partial class frmEdit : Form
    {
        #region initialization
        // name, description and picture of the model
        string name;
        string description;
        Bitmap loadedPicture;
        string imageAsString;
        #endregion
        public frmEdit(string name, string description)
        {
            InitializeComponent();
            this.name = name;
            this.description = description;

            tbxName.Text = this.name;
            tbxDescription.Text = this.description;
        }

        private void tbxName_TextChanged(object sender, EventArgs e)
        {
            if (tbxName.Text.Length <= 0)
            {
                btnValidate.Enabled = false;
            }
            else
            {
                btnValidate.Enabled = true;
            }
        }

        private void tbxDescription_TextChanged(object sender, EventArgs e)
        {
            if (tbxDescription.Text.Length <= 0)
            {
                btnValidate.Enabled = false;
            }
            else
            {
                btnValidate.Enabled = true;
            }
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = "C:\\Users";
            ofd.Filter = "Image files (*.png, *.jpg, *.jpeg, *.gif, *.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                loadedPicture = new Bitmap(ofd.FileName);
                lblFileName.Text = ofd.SafeFileName;
                lblFileName.Visible = true;
                TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
                imageAsString = Convert.ToBase64String((Byte[])converter.ConvertTo(loadedPicture, typeof(Byte[])));
            }
        }
    }
}
