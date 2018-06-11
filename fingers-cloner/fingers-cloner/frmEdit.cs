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
        public frmEdit(string name, string description)
        {
            InitializeComponent();
            tbxName.Text = name;
            tbxDescription.Text = description;
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
    }
}
