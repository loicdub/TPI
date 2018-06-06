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

        // Stabilized palm position
        public Vector palmStabPos;
        #endregion

        public frmMain()
        {
            InitializeComponent();
            DoubleBuffered = true;
            leapController = new LeapController(pnlUserHand.Width, pnlUserHand.Height);
            palmStabPos = new Vector((pnlUserHand.Width / 2), ((pnlUserHand.Height * 3) / 4), 0);
            paint = new Paint(palmStabPos);
        }

        // Refresh panel on each tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            pnlUserHand.Invalidate();
        }

        // Draw a circle to each finger and palm center on their location
        // Draw a line between the circle representing the palm and the ones for the fingers
        private void pnlUserHand_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                paint.paintHand(e, leapController.FingersPalmPos);
                lblUserHand.Text = "Votre main :";
                btnNewModel.Enabled = true;
            }
            catch (Exception)
            {
                lblUserHand.Text = "Pas de main détectée !";
                btnNewModel.Enabled = false;
            }
        }

        // Open a new form to create a new modele
        private void btnNewModel_Click(object sender, EventArgs e)
        {
            frmNewModele newModele = new frmNewModele();

            newModele.ShowDialog();
        }

        // Choose the precision required to accept a position
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            lblPercentage.Text = Convert.ToString(trackBar1.Value) + "%";
        }
    }
}