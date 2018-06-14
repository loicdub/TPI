/*
 * Author  : Dubas Loïc
 * Class   : I.FA-P3B
 * School  : CFPT-I
 * Date    : June 2018
 * Descr.  : Set the description of the position to save
 * Version : 1.0 
 * Ext. dll: LeapCSharp.NET4.5
 */
 
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
        #region initialization
        // description of the position to save
        private string _description;
        public string Description { get => _description; set => _description = value; }
        #endregion

        /// <summary>
        /// default constructor
        /// </summary>
        public frmComment()
        {
            InitializeComponent();
        }

        /// <summary>
        /// save description text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Description = tbxDescription.Text;
        }

        /// <summary>
        /// Disable the save button if description is empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
