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
using System.Xml;

namespace fingers_cloner
{
    public partial class frmMain : Form
    {
        private Controller controller = new Controller();
        public List<Label> fingersLbl;
        public frmMain()
        {
            InitializeComponent();
            DoubleBuffered = true;
            controller.EventContext = WindowsFormsSynchronizationContext.Current;
            controller.FrameReady += newFrameHandler;

            fingersLbl = new List<Label>();
            fingersLbl.Add(lblThumbPos);
            fingersLbl.Add(lblIndexPos);
            fingersLbl.Add(lblMiddlePos);
            fingersLbl.Add(lblRingPos);
            fingersLbl.Add(lblPinkyPos);
        }

        /// <summary>
        /// Refresh the fingers info on every frame of the Leap Motion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        public void newFrameHandler(object sender, FrameEventArgs eventArgs)
        {
            Frame frame = eventArgs.frame;

            if (frame.Hands.Count > 0)
            {
                List<Hand> hands = frame.Hands;
                Hand firstHand = hands[0];
                List<Finger> fingers = firstHand.Fingers;
                List<Vector> fingersPos = new List<Vector>();

                Vector palmStabPos = firstHand.PalmPosition.Normalized;
                lblPalmPos.Text = Convert.ToString(palmStabPos);

                for (int i = 0; i < fingers.Count; i++)
                {
                    fingersPos.Add(new Vector(fingers[i].StabilizedTipPosition.Normalized));

                    fingersLbl[i].Text = Convert.ToString(normalizedToFormPos(fingersPos[i]));
                }
            }
        }

        private Vector normalizedToFormPos(Vector normalizedPos)
        {
            Vector formPos = new Vector();

            formPos.x = normalizedPos.x * this.Width;
            formPos.y = (1 - normalizedPos.y) * this.Height;

            if (formPos.x < 0)
            {
                formPos.x = 0;
            }

            if (formPos.y < 0)
            {
                formPos.y = 0;
            }

            return formPos;
        }

        private void DrawEllipseRectangle(PaintEventArgs e, int x, int y)
        {
            // Create pen.
            Pen blackPen = new Pen(Color.Black, 3);

            // Create rectangle for ellipse.
            Rectangle rect = new Rectangle(x, y, 50, 50);

            // Draw ellipse to screen.
            e.Graphics.DrawEllipse(blackPen, rect);
        }
    }
}
