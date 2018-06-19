/*
 * Author  : Dubas Loïc
 * Class   : I.FA-P3B
 * School  : CFPT-I
 * Date    : June 2018
 * Descr.  : show user's hand and modele's hand
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

        /// <summary>
        /// default constructor
        /// </summary>
        public frmMain()
        {
            InitializeComponent();

            // create the serial folder if not exist to store saved positions
            Directory.CreateDirectory("serial");

            DoubleBuffered = true;

            // initialize the leap controller
            leapController = new LeapController();

            // initialize serialization class
            savedPositions = new Serialization();
            // initialize paint class
            paint = new Paint();
            // send panel dimensions to paint class
            paint.GetPanelSize(pnlUserHand.Width, pnlUserHand.Height);

            updateCombobox();
            updateModele();

            // get value of trackbar
            precision = trackBar1.Value;
        }

        /// <summary>
        /// Refresh panel on each tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            userHand = leapController.UserHand;
            pnlUserHand.Invalidate();
        }

        /// <summary>
        /// Draw the user's hand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlUserHand_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                // if combobox isn't empty, compare current modele with user's hand
                if (cbxModele.Items.Count > 0)
                {
                    comparePosition();
                    userFingersColor = colorIndicator();
                    paint.paintHandColor(e, userHand, userFingersColor);

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

        /// <summary>
        /// draw modele's hand
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlModelHand_Paint(object sender, PaintEventArgs e)
        {
            // if combobox isn't empty, show selected modele's description and position
            if (cbxModele.Items.Count > 0)
            {
                paint.paintHand(e, modeleHand);
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void cbxModele_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateModele();
        }

        /// <summary>
        /// Open a new form to create a new modele
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewModel_Click(object sender, EventArgs e)
        {
            frmNewModele newModele = new frmNewModele(userHand);

            newModele.getAllPositions(allPositions);
            newModele.ShowDialog();

            if (newModele.DialogResult == DialogResult.OK)
            {
                updateCombobox();
            }
        }

        /// <summary>
        /// Choose the precision required to accept a position
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            lblPercentage.Text = Convert.ToString(trackBar1.Value) + "%";
            precision = trackBar1.Value;
        }

        /// <summary>
        /// send to paint class new dimensions of window and refresh panel of modele
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMain_SizeChanged(object sender, EventArgs e)
        {
            paint.GetPanelSize(pnlUserHand.Width, pnlUserHand.Height);
            pnlModelHand.Invalidate();
        }

        #region edition
        /// <summary>
        /// open edit window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmEdit edit = new frmEdit(modeleHand);

            edit.ShowDialog();

            if (edit.DialogResult == DialogResult.OK)
            {
                updateCombobox();
            }
        }

        /// <summary>
        /// delete current modele
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult delete = MessageBox.Show("Êtes-vous sûr de vouloir supprimer la position " + modeleHand.Name + " ?", "Supprimer une position", MessageBoxButtons.YesNo);

            if (delete == DialogResult.Yes)
            {
                savedPositions.deletePosition(modeleHand.Name);
                updateCombobox();
                updateModele();
                pnlModelHand.Invalidate();
            }
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
                // if combobox is empty, disable combobox and edition buttons
                cbxModele.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        /// <summary>
        /// Met à jour le modèle sélectionné, affiche son nom, sa description et rafraîchit le panel
        /// </summary>
        private void updateModele()
        {
            // set modele's hand to the selected modele
            modeleHand = (MyHand)cbxModele.SelectedItem;

            // if there is modele hand saved
            if (modeleHand != null)
            {
                // show name, description and picture
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
            }
            else
            {
                lblName.Text = "Aucun modèle";
                lblDescription.Text = "Aucun modèle n'est chargé. Créez-en ou sélectionnez-en un !";
                pbxModele.Image = Properties.Resources.no_image_available;
            }

            lblName.Visible = true;
            lblDescription.Visible = true;

            pnlModelHand.Invalidate();
        }

        /// <summary>
        /// Calculate distance between each fingers of user's and modele's hand
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
        /// List of color for each fingers to show how close user's hand is to modele
        /// </summary>
        /// <returns>List of the colors</returns>
        private List<Color> colorIndicator()
        {
            List<Color> color = new List<Color>();
            int tolerance = (pnlUserHand.Width / 4) - this.precision;

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

        /// <summary>
        /// set the panel border's color depending on average of user's fingers position
        /// </summary>
        /// <param name="fingersColor"></param>
        /// <returns></returns>
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

        /// <summary>
        /// transform a text as an image
        /// </summary>
        /// <param name="stringImage"></param>
        /// <returns></returns>
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
