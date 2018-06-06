/*
 * Author  : Dubas Loïc
 * Class   : I.FA-P3B
 * School  : CFPT-I
 * Date    : June 2018
 * Descr.  : Detect hand and calculate finger's position
 * Version : 1.0 
 * Ext. dll: LeapCSharp.NET4.5
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// References to add
using Leap;
using System.Xml;

namespace fingers_cloner
{
    class LeapController : Controller
    {
        // Initialize Paint class to draw
        Paint paint;

        #region getters
        // List of detected hands and the first detected hand
        private List<Hand> _hands;
        private Hand _firstHand;

        // List of all the detected fingers
        private List<Finger> _fingers;

        // Fingers raw, normalized and to the palm location
        private List<Vector> _fingersPos;
        private List<Vector> _fingersNormPos;
        private List<Vector> _fingersPalmPos;

        // Palm raw, normalized and stabilized location
        private Vector _palmPos;
        private Vector _palmNormPos;
        private Vector _palmStabPos;

        // Size of the panel
        private int _panelWidth;
        private int _panelHeight;
        #endregion

        #region setters
        public List<Finger> Fingers { get => _fingers; set => _fingers = value; }
        public List<Vector> FingersPos { get => _fingersPos; set => _fingersPos = value; }
        public List<Vector> FingersNormPos { get => _fingersNormPos; set => _fingersNormPos = value; }
        public List<Vector> FingersPalmPos { get => _fingersPalmPos; set => _fingersPalmPos = value; }
        public Vector PalmPos { get => _palmPos; set => _palmPos = value; }
        public Vector PalmNormPos { get => _palmNormPos; set => _palmNormPos = value; }
        public Vector PalmStabPos { get => _palmStabPos; set => _palmStabPos = value; }
        public List<Hand> Hands { get => _hands; set => _hands = value; }
        public Hand FirstHand { get => _firstHand; set => _firstHand = value; }
        #endregion

        public LeapController(int panelWidth, int panelHeight)
        {
            EventContext = WindowsFormsSynchronizationContext.Current;
            FrameReady += newFrameHandler;

            // Stabilized palm position
            this._panelWidth = panelWidth;
            this._panelHeight = panelHeight;
            PalmStabPos = new Vector((_panelWidth / 2), ((_panelHeight / 4) * 3), 0);

            paint = new Paint(PalmStabPos);            
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
                Hands = frame.Hands;
                FirstHand = Hands[0];

                Fingers = FirstHand.Fingers;
                FingersPos = new List<Vector>();
                FingersNormPos = new List<Vector>();
                FingersPalmPos = new List<Vector>();

                PalmPos = FirstHand.PalmPosition;
                PalmNormPos = iBox.NormalizePoint(PalmPos);

                for (int i = 0; i < Fingers.Count; i++)
                {
                    FingersPos.Add(Fingers[i].StabilizedTipPosition);
                    FingersNormPos.Add(iBox.NormalizePoint(FingersPos[i]));
                    FingersPalmPos.Add(stabilizedToPalmPos(FingersPos[i]));
                }
            }
        }

        // Calculate fingers location compared to the palm location
        private Vector stabilizedToPalmPos(Vector stabilizedPos)
        {
            Vector fingerPalmPos;

            float fingersPalmDiffX = stabilizedPos.x - PalmPos.x;
            float fingersPalmDiffY = stabilizedPos.y - PalmPos.y;

            float fingerStabPosX = PalmStabPos.x - 2 * fingersPalmDiffX;
            float fingerStabPosY = PalmStabPos.y - 2 * fingersPalmDiffY;

            fingerPalmPos = new Vector(fingerStabPosX, fingerStabPosY, 0);

            return fingerPalmPos;
        }
    }
}
