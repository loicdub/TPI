/*
 * Author  : Dubas Loïc
 * Class   : I.FA-P3B
 * School  : CFPT-I
 * Date    : June 2018
 * Descr.  : Drawing hand, circle and line functions
 * Version : 1.0 
 * Ext. dll: LeapCSharp.NET4.5
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// References to add
using System.Drawing;
using System.Windows.Forms;
using Leap;

namespace fingers_cloner
{
    class Paint
    {
        #region Initialization
        // Fixed circle size
        const int CIRCLESIZE = 50;

        // Palm fixed location in the panel
        Vector palmPanelPos;

        // Dimensions of the panel
        private int _panelWidth;
        private int _panelHeight;
        public int PanelWidth { get => _panelWidth; set => _panelWidth = value; }
        public int PanelHeight { get => _panelHeight; set => _panelHeight = value; }

        // hand to draw
        private MyHand _hand;
        private List<Vector> _fingersPanelPos;
        private List<Vector> _modelePanelPos;
        public MyHand Hand { get => _hand; set => _hand = value; }
        public List<Vector> FingersPanelPos { get => _fingersPanelPos; set => _fingersPanelPos = value; }
        public List<Vector> ModelePanelPos { get => _modelePanelPos; set => _modelePanelPos = value; }
        #endregion

        /// <summary>
        /// Paint constructor
        /// </summary>
        /// <param name="panelWidth">Panel width</param>
        /// <param name="panelHeight">Panel height</param>
        public Paint() { }

        /// <summary>
        /// get the panel size
        /// </summary>
        /// <param name="panelWidth">Panel width</param>
        /// <param name="panelHeight">Panel height</param>
        public void GetPanelSize(int panelWidth, int panelHeight)
        {
            this.PanelWidth = panelWidth;
            this.PanelHeight = panelHeight;

            palmPanelPos = new Vector((PanelWidth / 2), 0, (PanelHeight - CIRCLESIZE));
        }

        #region drawing black
        /// <summary>
        /// Draw a hand
        /// </summary>
        /// <param name="e">paint event</param>
        /// <param name="hand">hand to paint</param>
        public void paintHand(PaintEventArgs e, MyHand hand)
        {
            this.Hand = hand;
            FingersPanelPos = normToPalmPanelPos();

            this.DrawEllipseRectangle(e, Convert.ToInt32(palmPanelPos.x), Convert.ToInt32(palmPanelPos.z));
            for (int i = 0; i < FingersPanelPos.Count; i++)
            {
                this.DrawEllipseRectangle(e, Convert.ToInt32(FingersPanelPos[i].x), Convert.ToInt32(FingersPanelPos[i].z));
                this.DrawLinePoint(e, Convert.ToInt32(FingersPanelPos[i].x), Convert.ToInt32(FingersPanelPos[i].z));
            }
        }

        /// <summary>
        /// Draw a circle at a certain location
        /// </summary>
        /// <param name="e">Paint event</param>
        /// <param name="x">Horizonzal coordinate of finger/palm</param>
        /// <param name="z">Vertical coordinate of finger/palm</param>
        private void DrawEllipseRectangle(PaintEventArgs e, int x, int z)
        {
            // Create pen.
            Pen Pen = new Pen(Color.Black, 3);

            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(x - (CIRCLESIZE / 2), z - (CIRCLESIZE / 2), CIRCLESIZE, CIRCLESIZE);

            // Draw ellipse to screen.
            e.Graphics.DrawEllipse(Pen, rect);
        }

        /// <summary>
        /// Draw a line beteween two points (center of palm to finger)
        /// </summary>
        /// <param name="e">Paint event</param>
        /// <param name="x">Horizontal coordinate of finger</param>
        /// <param name="z">Vertical coordinate of finger</param>
        private void DrawLinePoint(PaintEventArgs e, int x, int z)
        {
            // Create pen.
            Pen Pen = new Pen(Color.Black, 3);

            // Create points that define line.
            Point point1 = new Point(Convert.ToInt32(palmPanelPos.x), Convert.ToInt32(palmPanelPos.z));
            Point point2 = new Point(x, z);

            // Draw line to screen.
            e.Graphics.DrawLine(Pen, point1, point2);
        }
        #endregion

        #region drawing colors
        /// <summary>
        /// draw user's hand in color
        /// </summary>
        /// <param name="e">paint event</param>
        /// <param name="hand">hand of user</param>
        /// <param name="colors">list of colors of user's finger</param>
        public void paintHandColor(PaintEventArgs e, MyHand hand, List<Color> colors)
        {
            this.Hand = hand;
            FingersPanelPos = normToPalmPanelPos();

            this.DrawEllipseRectangle(e, Convert.ToInt32(palmPanelPos.x), Convert.ToInt32(palmPanelPos.z));
            for (int i = 0; i < FingersPanelPos.Count; i++)
            {
                this.DrawEllipseRectangleColor(e, Convert.ToInt32(FingersPanelPos[i].x), Convert.ToInt32(FingersPanelPos[i].z), colors[i]);
                this.DrawLinePointColor(e, Convert.ToInt32(FingersPanelPos[i].x), Convert.ToInt32(FingersPanelPos[i].z), colors[i]);
            }
        }

        /// <summary>
        /// Draw a circle at a certain 
        /// </summary>
        /// <param name="e">Paint event</param>
        /// <param name="x">Horizonzal coordinate of finger/palm</param>
        /// <param name="z">Vertical coordinate of finger/palm</param>
        /// <param name="penColor">color of the finger</param>
        private void DrawEllipseRectangleColor(PaintEventArgs e, int x, int z, Color penColor)
        {
            // Create pen.
            Pen Pen = new Pen(penColor, 3);

            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(x - (CIRCLESIZE / 2), z - (CIRCLESIZE / 2), CIRCLESIZE, CIRCLESIZE);

            // Draw ellipse to screen.
            e.Graphics.DrawEllipse(Pen, rect);
        }

        /// <summary>
        /// Draw a line beteween two points (center of palm to finger)
        /// </summary>
        /// <param name="e">Paint event</param>
        /// <param name="x">Horizontal coordinate of finger</param>
        /// <param name="z">Vertical coordinate of finger</param>
        /// <param name="penColor">color of the finger</param>
        private void DrawLinePointColor(PaintEventArgs e, int x, int z, Color penColor)
        {
            // Create pen.
            Pen Pen = new Pen(penColor, 3);

            // Create points that define line.
            Point point1 = new Point(Convert.ToInt32(palmPanelPos.x), Convert.ToInt32(palmPanelPos.z));
            Point point2 = new Point(x, z);

            // Draw line to screen.
            e.Graphics.DrawLine(Pen, point1, point2);
        }
        #endregion

        #region transform norm to panel position
        /// <summary>
        /// Calculate the position on the panel with the normalized vector
        /// </summary>
        /// <returns>A list of vector with the finger's position to the palm</returns>
        public List<Vector> normToPalmPanelPos()
        {
            float scaleFactor = PanelHeight + CIRCLESIZE;
            List<Vector> fingersPanelPos = new List<Vector>();
            Vector originToPalm = new Vector(Hand.PalmNormPos.x, 0, Hand.PalmNormPos.z);
            List<Vector> originToFingers = new List<Vector>();

            for (int i = 0; i < Hand.FingersNormPos.Count; i++)
            {
                originToFingers.Add(new Vector(Hand.FingersNormPos[i].x, 0, Hand.FingersNormPos[i].z));

                fingersPanelPos.Add(new Vector((-originToPalm + originToFingers[i]) * scaleFactor + palmPanelPos));
            }

            return fingersPanelPos;
        }

        /// <summary>
        /// Calculate panel position of the modele hand
        /// </summary>
        /// <param name="modele">the current modele</param>
        /// <returns>A list of positions</returns>
        public List<Vector> normToPalmPanelModelePos(MyHand modele)
        {
            float scaleFactor = PanelHeight + CIRCLESIZE;
            List<Vector> modelePanelPos = new List<Vector>();
            Vector originToPalm = new Vector(modele.PalmNormPos.x, 0, modele.PalmNormPos.z);
            List<Vector> originToFingers = new List<Vector>();

            for (int i = 0; i < modele.FingersNormPos.Count; i++)
            {
                originToFingers.Add(new Vector(modele.FingersNormPos[i].x, 0, modele.FingersNormPos[i].z));

                modelePanelPos.Add(new Vector((-originToPalm + originToFingers[i]) * scaleFactor + palmPanelPos));
            }

            ModelePanelPos = modelePanelPos;

            return modelePanelPos;
        }
        #endregion
    }
}
