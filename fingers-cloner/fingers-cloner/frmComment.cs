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
    public partial class frmComment : Form
    {
        private string _description;
        public string Description { get => _description; set => _description = value; }

        public frmComment()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Description = tbxDescription.Text;
        }

        private void tbxDescription_TextChanged(object sender, EventArgs e)
        {
            if (tbxDescription.Text.Length <= 0)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }
        }
    }
}
