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
using System.IO;

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

            savedPositions = new Serialization();
            paint = new Paint();
            paint.GetPanelSize(pnlUserHand.Width, pnlUserHand.Height);

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
                    //pnlUserHand.BackColor = panelColor(userFingersColor);
                    ControlPaint.DrawBorder(e.Graphics, this.pnlUserHand.ClientRectangle, panelColor(userFingersColor), ButtonBorderStyle.Solid);
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
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
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

        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            paint.GetPanelSize(pnlUserHand.Width, pnlUserHand.Height);
            pnlModelHand.Invalidate();
        }

        #region edition
        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmEdit edit = new frmEdit(modeleHand.Name, modeleHand.Description);

            edit.ShowDialog();

            if (edit.DialogResult == DialogResult.OK)
            {
                updateCombobox();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult delete = MessageBox.Show("Êtes-vous sûr de vouloir supprimer la position " + modeleHand.Name + " ?", "Supprimer une position", MessageBoxButtons.YesNo);

            if (delete == DialogResult.Yes)
            {
                //do something
            }

            updateModele();
        }
        #endregion

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
            if (modeleHand.Image != null)
            {
                pbxModele.Image = stringToImage(modeleHand.Image);
            }
            else
            {
                pbxModele.Image = Properties.Resources.no_image_available;
            }

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

        private Color panelColor(List<Color> fingersColor)
        {
            Color panelColor = new Color();
            int totalColorValue = 0;
            int averageColorValue;

            for (int i = 0; i < fingersColor.Count; i++)
            {
                if (fingersColor[i] == Color.Green)
                {
                    totalColorValue += 3;
                }
                else if (fingersColor[i] == Color.Orange)
                {
                    totalColorValue += 2;
                }
                else if (fingersColor[i] == Color.Red)
                {
                    totalColorValue += 1;
                }
                else
                {
                    totalColorValue += 0;
                }
            }

            averageColorValue = totalColorValue / 5;

            if (averageColorValue == 3)
            {
                panelColor = Color.Green;
            }
            else if (averageColorValue >= 2)
            {
                panelColor = Color.Orange;
            }
            else if (averageColorValue >= 1)
            {
                panelColor = Color.Red;
            }
            else if (averageColorValue == 0)
            {
                panelColor = Color.Black;
            }

            return panelColor;
        }

        private System.Drawing.Image stringToImage(string stringImage)
        {
            System.Drawing.Image image;

            Byte[] stringAsByte = Convert.FromBase64String(stringImage);
            MemoryStream memstr = new MemoryStream(stringAsByte);

            image = System.Drawing.Image.FromStream(memstr);

            return image;
        }
        #endregion
    }
}
