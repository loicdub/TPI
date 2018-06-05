using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// ref to add
using Leap;
using System.Diagnostics;
using System.Xml;

namespace fingers_cloner
{
    public partial class frmNewModele : Form
    {
        private Controller controller = new Controller();

        public const int CIRCLESIZE = 50;
        public List<Finger> fingers;

        public List<Vector> fingersPos;
        public List<Vector> fingersNormPos;
        public List<Vector> fingersPalmPos;

        public Vector palmPos;
        public Vector palmNormPos;
        public Vector palmStabPos;

        public frmNewModele()
        {
            InitializeComponent();
            DoubleBuffered = true;
            controller.EventContext = WindowsFormsSynchronizationContext.Current;
            controller.FrameReady += newFrameHandler;
        }

        /// <summary>
        /// Refresh the fingers info on every frame of the Leap Motion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void newFrameHandler(object sender, FrameEventArgs eventArgs)
        {
            Frame frame = eventArgs.frame;
            InteractionBox iBox = frame.InteractionBox;

            if (frame.Hands.Count > 0)
            {
                List<Hand> hands = frame.Hands;
                Hand firstHand = hands[0];

                fingers = firstHand.Fingers;
                fingersPos = new List<Vector>();
                fingersNormPos = new List<Vector>();
                fingersPalmPos = new List<Vector>();

                palmPos = firstHand.PalmPosition;
                palmNormPos = iBox.NormalizePoint(palmPos);
                palmStabPos.x = (pnlModele.Width / 2) - CIRCLESIZE;
                palmStabPos.y = ((pnlModele.Height / 4) * 3) - CIRCLESIZE;

                for (int i = 0; i < fingers.Count; i++)
                {
                    fingersPos.Add(fingers[i].StabilizedTipPosition);
                    fingersNormPos.Add(iBox.NormalizePoint(fingersPos[i]));
                    fingersPalmPos.Add(stabilizedToPalmPos(fingersPos[i]));
                }
            }
        }

        private Vector stabilizedToPalmPos(Vector stabilizedPos)
        {
            Vector fingerPalmPos;

            float fingersPalmDiffX = stabilizedPos.x - palmPos.x;
            float fingersPalmDiffY = stabilizedPos.y - palmPos.y;

            float fingerStabPosX = palmStabPos.x - 2 * fingersPalmDiffX;
            float fingerStabPosY = palmStabPos.y - 2 * fingersPalmDiffY;

            fingerPalmPos = new Vector(fingerStabPosX, fingerStabPosY, 0);

            return fingerPalmPos;
        }

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

        private void pnlModele_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                DrawEllipseRectangle(e, Convert.ToInt32(palmStabPos.x), Convert.ToInt32(palmStabPos.y));
                for (int i = 0; i < fingersPalmPos.Count; i++)
                {
                    DrawEllipseRectangle(e, Convert.ToInt32(fingersPalmPos[i].x), Convert.ToInt32(fingersPalmPos[i].y));
                    DrawLinePoint(e, Convert.ToInt32(fingersPalmPos[i].x) + (CIRCLESIZE / 2), Convert.ToInt32(fingersPalmPos[i].y) + (CIRCLESIZE / 2));
                }
            }
            catch (Exception){}
        }

        private void DrawEllipseRectangle(PaintEventArgs e, int x, int y)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(x, y, CIRCLESIZE, CIRCLESIZE);

            // Draw ellipse to screen.
            e.Graphics.DrawEllipse(blackPen, rect);
        }

        public void DrawLinePoint(PaintEventArgs e, int x, int y)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create points that define line.
            Point point1 = new Point(Convert.ToInt32(palmStabPos.x) + (CIRCLESIZE / 2), Convert.ToInt32(palmStabPos.y) + (CIRCLESIZE / 2));
            Point point2 = new Point(x, y);

            // Draw line to screen.
            e.Graphics.DrawLine(blackPen, point1, point2);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pnlModele.Invalidate();
        }
    }
}
