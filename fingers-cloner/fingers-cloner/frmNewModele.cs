/*
 * Author  : Dubas Loïc
 * Class   : I.FA-P3B
 * School  : CFPT-I
 * Date    : June 2018
 * Descr.  : Create a new modele, with a name and a description
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
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace fingers_cloner
{
    public partial class frmNewModele : Form
    {
        #region initialization
        // Initialize Leap Motion
        LeapController leapController;

        // Initialize Paint class to draw circles and lines
        Paint paint;

        // Stabilized palm position
        public Vector palmStabPos;

        // name and description
        string name;
        string description;

        // serialize file name
        string fileSerial = "serialized-position.xml";
        #endregion

        public frmNewModele()
        {
            InitializeComponent();
            DoubleBuffered = true;
            leapController = new LeapController(pnlModele.Width, pnlModele.Height);
            palmStabPos = new Vector((pnlModele.Width / 2), ((pnlModele.Height * 3) / 4), 0);
            paint = new Paint(palmStabPos);
        }

        // enable save button if there is a name to it
        private void tbxModeleName_TextChanged(object sender, EventArgs e)
        {
            if (tbxModeleName.Text.Length <= 0)
            {
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }
        }

        // draw hand if there is one
        private void pnlModele_Paint(object sender, PaintEventArgs e)
        {
                paint.paintHand(e, leapController.FingersPalmPos);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            frmComment comment = new frmComment();
            name = tbxModeleName.Text;
            comment.ShowDialog();

            if (comment.DialogResult == DialogResult.OK)
            {
                description = comment.Description;
                savedHand currentPosition = new savedHand(leapController.FingersPalmPos, leapController.PalmStabPos, name, description);

                currentPosition.serialize(fileSerial, currentPosition);

                this.Close();
            }
        }
    }
}
