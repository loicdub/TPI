/*
 * Author  : Dubas Loïc
 * Class   : I.FA-P3B
 * School  : CFPT-I
 * Date    : June 2018
 * Descr.  : show user's hand and model's hand (coming soon)
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
// References to add
using Leap;

namespace fingers_cloner
{
    public partial class frmMain : Form
    {
        #region Initialization
        // Initialize Leap Motion
        LeapController leapController;

        // Initialize Paint class to draw
        Paint paint;

        // Initialize Hand class to store hands position info
        MyHand userHand;
        MyHand modeleHand;

        // Precision setted by trackbar
        int precision;

        // serialize/deserialize saved positions
        Serialization savedPositions;
        List<MyHand> allPositions;
        #endregion

        public frmMain()
        {
            InitializeComponent();
            DoubleBuffered = true;

            leapController = new LeapController();
            paint = new Paint(pnlUserHand.Width, pnlUserHand.Height);

            savedPositions = new Serialization();

            updateCombobox();
            

            precision = trackBar1.Value;
        }

        // Refresh panel on each tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            userHand = leapController.UserHand;

            pnlUserHand.Invalidate();
        }

        // Draw a circle to each finger and palm center on their location
        // Draw a line between the circle representing the palm and the ones for the fingers
        private void pnlUserHand_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                paint.paintHand(e, userHand);
                lblUserHand.Text = "Votre main :";
                btnNewModel.Enabled = true;
            }
            catch (Exception)
            {
                lblUserHand.Text = "Pas de main détectée !";
                btnNewModel.Enabled = false;
            }
        }

        private void pnlModelHand_Paint(object sender, PaintEventArgs e)
        {
            // if combobox isn't empty, show selected model's description and position
            if (cbxModele.Items.Count > 0)
            {
                paint.paintHand(e, modeleHand);
                lblName.Visible = true;
                lblDescription.Visible = true;
            }
        }

        private void cbxModele_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateModele();
        }

        // Open a new form to create a new modele
        private void btnNewModel_Click(object sender, EventArgs e)
        {
            frmNewModele newModele = new frmNewModele(userHand);

            newModele.ShowDialog();

            if (newModele.DialogResult == DialogResult.OK)
            {
                updateCombobox();
            }
        }

        /// <summary>
        /// Update combobox with the latest saved positions
        /// </summary>
        private void updateCombobox()
        {
            // get all saved position
            allPositions = savedPositions.deserialize();

            // add all position to combobox
            cbxModele.DataSource = allPositions;
            cbxModele.DisplayMember = "Name";

            // if combobox isn't empty, select first of the list
            if (cbxModele.Items.Count >= 1)
            {
                cbxModele.SelectedIndex = 0;
                cbxModele.Enabled = true;
                updateModele();
            }
            else
            {
                // if combobox is empty, disable combobox
                cbxModele.Enabled = false;
            }
        }

        /// <summary>
        /// Met à jour le modèle sélectionné, affiche son nom, sa description et rafraîchit le panel
        /// </summary>
        private void updateModele() {
            modeleHand = (MyHand)cbxModele.SelectedItem;

            lblName.Text = modeleHand.Name;
            lblDescription.Text = modeleHand.Description;

            pnlModelHand.Invalidate();
        }

        // Choose the precision required to accept a position
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            lblPercentage.Text = Convert.ToString(trackBar1.Value) + "%";
            precision = trackBar1.Value;
        }
    }
}
