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
using System.Xml;

namespace fingers_cloner
{
    public partial class frmMain : Form
    {
        #region initialization
        // Initialize Leap Motion
        LeapController leapController;

        // Initialize Paint class to draw
        Paint paint;

        // Precision setted by trackbar
        int precision;

        // serialize/deserialize saved positions
        savedHand saveHand;
        string fileSerial = "serialized-position.xml";
        List<savedHand> allPositions;
        savedHand currentModele;
        List<Vector> currentModeleFingers;
        #endregion

        public frmMain()
        {
            InitializeComponent();
            DoubleBuffered = true;
            leapController = new LeapController(pnlUserHand.Width, pnlUserHand.Height);
            paint = new Paint(pnlUserHand.Width, pnlUserHand.Height);
            saveHand = new savedHand();
            currentModele = (savedHand)cbxModele.SelectedItem;
            updateCombobox();
            precision = trackBar1.Value;
        }

        // Refresh panel on each tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            pnlUserHand.Invalidate();
            pnlModelHand.Invalidate();
        }

        // Draw a circle to each finger and palm center on their location
        // Draw a line between the circle representing the palm and the ones for the fingers
        private void pnlUserHand_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                paint.paintHand(e, leapController.FingersPanelPos);
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
                paint.paintHand(e, currentModeleFingers);
                lblDescription.Visible = true;
            }
        }

        private void cbxModele_SelectedIndexChanged(object sender, EventArgs e)
        {
            // set selected item in combobox as the current model
            currentModele = (savedHand)cbxModele.SelectedItem;
            lblDescription.Text = currentModele.Description;
            // create list to store finger's position
            currentModeleFingers = new List<Vector>();

            for (int i = 0; i < currentModele.Fingers.Count; i++)
            {
                // add the finger's position to draw
                currentModeleFingers.Add(leapController.normalizedToPanel(currentModele.Fingers[i]));
            }
        }

        // Open a new form to create a new modele
        private void btnNewModel_Click(object sender, EventArgs e)
        {
            frmNewModele newModele = new frmNewModele(leapController.FingersNormPos, leapController.PalmNormPos);

            newModele.ShowDialog();

            if (newModele.DialogResult == DialogResult.OK)
            {
                updateCombobox();
            }
        }

        private void updateCombobox()
        {
            // get all saved position
            allPositions = saveHand.deserialize(fileSerial);

            // add all position to combobox
            cbxModele.DataSource = allPositions;
            cbxModele.DisplayMember = "Name";

            // if combobox isn't empty, select first of the list
            if (cbxModele.Items.Count >= 1)
            {
                cbxModele.SelectedIndex = 0;
                cbxModele.Enabled = true;
            }
            else
            {
                // if combobox is empty, disable combobox
                cbxModele.Enabled = false;
            }
        }

        // Choose the precision required to accept a position
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            lblPercentage.Text = Convert.ToString(trackBar1.Value) + "%";
            precision = trackBar1.Value;
        }
    }
}
