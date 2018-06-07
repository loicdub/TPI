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
        // finger's and palm's normalized position (set)
        private Vector _palmNormPos;
        private List<Vector> _fingersNormPos;
        private List<Vector> _fingerPanelPos;

        // finger's and palm's normalized position (get)
        public Vector PalmNormPos { get => _palmNormPos; set => _palmNormPos = value; }
        public List<Vector> FingersNormPos { get => _fingersNormPos; set => _fingersNormPos = value; }
        public List<Vector> FingerPanelPos { get => _fingerPanelPos; set => _fingerPanelPos = value; }

        // Initialize Leap Motion
        LeapController leapController;

        // Initialize Paint class to draw circles and lines
        Paint paint;

        // name and description of the model
        string name;
        string description;

        // serialize file name
        string fileSerial = "serialized-position.xml";        
        #endregion

        /// <summary>
        /// create new modele form
        /// </summary>
        /// <param name="fingersNormPos">finger's normalized position</param>
        /// <param name="palmNormPos">palm's normalized position</param>
        public frmNewModele(List<Vector> fingersNormPos, Vector palmNormPos)
        {
            InitializeComponent();
            DoubleBuffered = true;
            leapController = new LeapController(pnlModele.Width, pnlModele.Height);
            paint = new Paint(pnlModele.Width, pnlModele.Height);
            this.FingersNormPos = fingersNormPos;
            this.PalmNormPos = palmNormPos;
            
            FingerPanelPos = new List<Vector>();

            for (int i = 0; i < FingersNormPos.Count; i++)
            {
                FingerPanelPos.Add(leapController.normalizedToPanel(FingersNormPos[i]));
            }
        }

        // draw hand if there is one
        private void pnlModele_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                paint.paintHand(e, FingerPanelPos);
            }
            catch (Exception)
            {
                NoHandDetected();
            }
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Open a new form to add a description
            frmComment comment = new frmComment();
            name = tbxModeleName.Text;
            comment.ShowDialog();

            // when click on ok of the comment form
            if (comment.DialogResult == DialogResult.OK)
            {
                // add description, name, fingers and palm position to savedHand object
                description = comment.Description;
                savedHand currentPosition = new savedHand(FingersNormPos, PalmNormPos, name, description);

                // serialize the savedHand object
                currentPosition.serialize(fileSerial, currentPosition);

                // Close comment and newModele form
                this.Close();
            }
        }

        /// <summary>
        /// if there is no hand detected on the Leap, user is informed and send back to main form
        /// </summary>
        private void NoHandDetected() {
            MessageBox.Show("Aucune main détectée. Veuillez réessayer.");
            this.Close();
        }
    }
}
