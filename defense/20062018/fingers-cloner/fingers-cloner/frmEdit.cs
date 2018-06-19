/*
 * Author  : Dubas Loïc
 * Class   : I.FA-P3B
 * School  : CFPT-I
 * Date    : June 2018
 * Descr.  : edit the current loaded position
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
    public partial class frmEdit : Form
    {
        #region initialization
        // name and picture of the modele
        string nameHandToEdit;
        string imageHandToEdit;
        // the hand to edit and the updated picture
        MyHand handToEdit;
        Bitmap loadedPicture;
        string imageAsString;

        // Initialize serialization functions
        Serialization serialization;
        #endregion

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="modelHand">the position to edit</param>
        public frmEdit(MyHand modelHand)
        {
            InitializeComponent();

            handToEdit = modelHand;
            nameHandToEdit = modelHand.Name;
            imageHandToEdit = modelHand.Image;

            serialization = new Serialization();
            
            tbxName.Text = modelHand.Name;
            tbxDescription.Text = modelHand.Description;
        }

        /// <summary>
        /// validation is possible only if the textbox of the name isn't empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// validation is possible only if the textbox of the description isn't empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// open file dialog choose a picture and transform it in string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// edit the hand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnValidate_Click(object sender, EventArgs e)
        {
            handToEdit.Name = tbxName.Text;
            handToEdit.Description = tbxDescription.Text;
            if (loadedPicture == null)
            {
                imageAsString = imageHandToEdit;
            }
            else
            {
                handToEdit.Image = imageAsString;
            }

            serialization.deletePosition(nameHandToEdit);
            serialization.serialize(handToEdit);
        }
    }
}
