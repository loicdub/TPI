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
        // Fixed circle size
        const int CIRCLESIZE = 50;

        // Palm location in the panel
        private Vector _palmStabPos;
        public Vector PalmStabPos { get => _palmStabPos; set => _palmStabPos = value; }

        public Paint(Vector palmStabPos)
        {
            this.PalmStabPos = palmStabPos;
        }


        /// <summary>
        /// Draw the hand of the user
        /// </summary>
        /// <param name="e">Paint event</param>
        /// <param name="fingersPalmPos">Position of the fingers from the palm position</param>
        public void paintHand(PaintEventArgs e, List<Vector> fingersPalmPos)
        {
            this.DrawEllipseRectangle(e, Convert.ToInt32(PalmStabPos.x), Convert.ToInt32(PalmStabPos.y));
            for (int i = 0; i < fingersPalmPos.Count; i++)
            {
                this.DrawEllipseRectangle(e, Convert.ToInt32(fingersPalmPos[i].x), Convert.ToInt32(fingersPalmPos[i].y));
                this.DrawLinePoint(e, Convert.ToInt32(fingersPalmPos[i].x), Convert.ToInt32(fingersPalmPos[i].y));
            }
        }

        #region drawing
        /// <summary>
        /// Draw a circle at a certain location
        /// </summary>
        /// <param name="e">Paint event</param>
        /// <param name="x">Horizonzal coordinate of finger/palm</param>
        /// <param name="y">Vertical coordinate of finger/palm</param>
        private void DrawEllipseRectangle(PaintEventArgs e, int x, int y)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(x - (CIRCLESIZE / 2), y - (CIRCLESIZE / 2), CIRCLESIZE, CIRCLESIZE);

            // Draw ellipse to screen.
            e.Graphics.DrawEllipse(blackPen, rect);
        }

        /// <summary>
        /// Draw a line beteween two points (center of palm to finger)
        /// </summary>
        /// <param name="e">Paint event</param>
        /// <param name="x">Horizontal coordinate of finger</param>
        /// <param name="y">Vertical coordinate of finger</param>
        private void DrawLinePoint(PaintEventArgs e, int x, int y)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create points that define line.
            Point point1 = new Point(Convert.ToInt32(PalmStabPos.x), Convert.ToInt32(PalmStabPos.y));
            Point point2 = new Point(x, y);

            // Draw line to screen.
            e.Graphics.DrawLine(blackPen, point1, point2);
        }
        #endregion
    }
}
