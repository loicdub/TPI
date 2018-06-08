/*
 * Author  : Dubas Loïc
 * Class   : I.FA-P3B
 * School  : CFPT-I
 * Date    : June 2018
 * Descr.  : Drawing circle and line functions
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
        Vector palmStabPos;

        // Dimensions of the panel
        private int _panelWidth;
        private int _panelHeight;
        public int PanelWidth { get => _panelWidth; set => _panelWidth = value; }
        public int PanelHeight { get => _panelHeight; set => _panelHeight = value; }

        // hand to draw
        private MyHand _hand;
        private List<Vector> fingersStabPos;
        public MyHand Hand { get => _hand; set => _hand = value; }
        public List<Vector> FingersStabPos { get => fingersStabPos; set => fingersStabPos = value; }
        #endregion

        /// <summary>
        /// Paint constructor
        /// </summary>
        /// <param name="panelWidth">Panel width</param>
        /// <param name="panelHeight">Panel height</param>
        public Paint(int panelWidth, int panelHeight)
        {
            this.PanelWidth = panelWidth;
            this.PanelHeight = panelHeight;

            palmStabPos = new Vector((PanelWidth / 2), 0, (PanelHeight - CIRCLESIZE));
        }

        #region drawing
        /// <summary>
        /// Draw the hand of the user
        /// </summary>
        /// <param name="e">Paint event</param>
        /// <param name="fingersPanelPos">Position of the fingers based on panel dimensions</param>
        public void paintHand(PaintEventArgs e, MyHand hand)
        {
            this.Hand = hand;
            fingersStabPos = normToPalmStabPos();

            this.DrawEllipseRectangle(e, Convert.ToInt32(palmStabPos.x), Convert.ToInt32(palmStabPos.z));
            for (int i = 0; i < fingersStabPos.Count; i++)
            {
                this.DrawEllipseRectangle(e, Convert.ToInt32(fingersStabPos[i].x), Convert.ToInt32(fingersStabPos[i].z));
                this.DrawLinePoint(e, Convert.ToInt32(fingersStabPos[i].x), Convert.ToInt32(fingersStabPos[i].z));
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
            Pen blackPen = new Pen(Color.Black, 3);

            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(x - (CIRCLESIZE / 2), z - (CIRCLESIZE / 2), CIRCLESIZE, CIRCLESIZE);

            // Draw ellipse to screen.
            e.Graphics.DrawEllipse(blackPen, rect);
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
            Pen blackPen = new Pen(Color.Black, 3);

            // Create points that define line.
            Point point1 = new Point(Convert.ToInt32(palmStabPos.x), Convert.ToInt32(palmStabPos.z));
            Point point2 = new Point(x, z);

            // Draw line to screen.
            e.Graphics.DrawLine(blackPen, point1, point2);
        }
        #endregion

        /// <summary>
        /// Calculate the position on the panel with the normalized vector
        /// </summary>
        /// <returns>A list of vector with the finger's position to the palm</returns>
        public List<Vector> normToPalmStabPos()
        {
            float scaleFactor = PanelHeight + CIRCLESIZE;
            List<Vector> fingersStabPos = new List<Vector>();
            Vector originToPalm = new Vector(Hand.PalmNormPos.x, 0, Hand.PalmNormPos.z);
            List<Vector> originToFingers = new List<Vector>();

            for (int i = 0; i < Hand.FingersNormPos.Count; i++)
            {
                originToFingers.Add(new Vector(Hand.FingersNormPos[i].x, 0, Hand.FingersNormPos[i].z));

                fingersStabPos.Add(new Vector((-originToPalm + originToFingers[i]) * scaleFactor + palmStabPos));
            }

            return fingersStabPos;
        }
    }
}
