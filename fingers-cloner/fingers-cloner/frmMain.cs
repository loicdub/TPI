﻿/*
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
        List<Color> userFingersColor;

        // Precision setted by trackbar
        int precision;
        List<Vector> handsDiff;
        List<double> fingersDist;

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
                // if combobox isn't empty, compare current model with user's hand
                if (cbxModele.Items.Count > 0)
                {
                    comparePosition();
                    userFingersColor = colorIndicator();
                    paint.paintHandColor(e, userHand, userFingersColor);
                }
                else
                {
                    paint.paintHand(e, userHand);
                }

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

        // Choose the precision required to accept a position
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            lblPercentage.Text = Convert.ToString(trackBar1.Value) + "%";
            precision = trackBar1.Value;
        }

        #region functions
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
        private void updateModele()
        {
            modeleHand = (MyHand)cbxModele.SelectedItem;

            lblName.Text = modeleHand.Name;
            lblDescription.Text = modeleHand.Description;

            pnlModelHand.Invalidate();
        }

        /// <summary>
        /// Calculate distance between each fingers of user's and model's hand
        /// </summary>
        /// <returns>A list of distances between each fingers</returns>
        private List<double> comparePosition()
        {
            handsDiff = new List<Vector>();
            fingersDist = new List<double>();
            List<Vector> modelePanelPos = paint.normToPalmPanelModelePos(modeleHand);

            for (int i = 0; i < paint.FingersPanelPos.Count; i++)
            {
                handsDiff.Add(paint.FingersPanelPos[i] - modelePanelPos[i]);
                fingersDist.Add(Math.Sqrt(
                    (Math.Pow(handsDiff[i].x, 2)) + (Math.Pow(handsDiff[i].z, 2))
                    ));
            }

            return fingersDist;
        }

        /// <summary>
        /// List of color for each fingers to show how close user's hand is to model
        /// </summary>
        /// <returns>List of the colors</returns>
        private List<Color> colorIndicator()
        {
            List<Color> color = new List<Color>();
            int tolerance = 100 - this.precision;

            for (int i = 0; i < fingersDist.Count; i++)
            {
                if (fingersDist[i] < tolerance)
                {
                    color.Add(Color.Green);
                }
                else if (fingersDist[i] < (tolerance + 10))
                {
                    color.Add(Color.Orange);
                }
                else if (fingersDist[i] < (tolerance + 30))
                {
                    color.Add(Color.Red);
                }
                else
                {
                    color.Add(Color.Black);
                }
            }

            return color;
        }
        #endregion
    }
}
