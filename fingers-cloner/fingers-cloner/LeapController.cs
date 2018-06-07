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
        // Initialisation
        #region getters
        // List of detected hands and the first detected hand
        private List<Hand> _hands;
        private Hand _firstHand;

        // Palm raw, normalized and stabilized location
        private Vector _palmPos;
        private Vector _palmNormPos;

        // List of all the detected fingers
        private List<Finger> _fingers;

        // Fingers raw, normalized and to the palm location
        private List<Vector> _fingersPos;
        private List<Vector> _fingersNormPos;
        private List<Vector> _fingersPanelPos;

        // Dimensions of the panel
        private int _panelWidth;
        private int _panelHeight;
        #endregion

        #region setters
        public List<Hand> Hands { get => _hands; set => _hands = value; }
        public Hand FirstHand { get => _firstHand; set => _firstHand = value; }
        public List<Finger> Fingers { get => _fingers; set => _fingers = value; }
        public List<Vector> FingersStabPos { get => _fingersPos; set => _fingersPos = value; }
        public List<Vector> FingersNormPos { get => _fingersNormPos; set => _fingersNormPos = value; }
        public List<Vector> FingersPanelPos { get => _fingersPanelPos; set => _fingersPanelPos = value; }
        public Vector PalmPos { get => _palmPos; set => _palmPos = value; }
        public Vector PalmNormPos { get => _palmNormPos; set => _palmNormPos = value; }
        #endregion

        /// <summary>
        /// Leap default constructor
        /// </summary>
        /// <param name="panelWidth">Width of the panel</param>
        /// <param name="panelHeight">Height of the panel</param>
        public LeapController(int panelWidth, int panelHeight)
        {
            EventContext = WindowsFormsSynchronizationContext.Current;
            FrameReady += newFrameHandler;

            // Assign parameter of the panel's width and height
            this._panelWidth = panelWidth;
            this._panelHeight = panelHeight;
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

                PalmPos = FirstHand.PalmPosition;
                PalmNormPos = iBox.NormalizePoint(PalmPos);

                Fingers = FirstHand.Fingers;
                FingersStabPos = new List<Vector>();
                FingersNormPos = new List<Vector>();
                FingersPanelPos = new List<Vector>();

                for (int i = 0; i < Fingers.Count; i++)
                {
                    FingersStabPos.Add(Fingers[i].StabilizedTipPosition);
                    FingersNormPos.Add(iBox.NormalizePoint(FingersStabPos[i]));
                    FingersPanelPos.Add(normalizedToPanel(FingersNormPos[i]));
                }
            }
        }

        /// <summary>
        /// Calculate fingers location based on the palm location
        /// </summary>
        /// <param name="normalizedPos">Finger's normalized position</param>
        /// <returns>Finger's location on panel</returns>
        public Vector normalizedToPanel(Vector normalizedPos)
        {
            Vector fingerPanelPos;
            float scaleFactor = 1.4f;

            float fingersPalmDiffX = PalmNormPos.x - normalizedPos.x;
            float fingersPalmDiffZ = PalmNormPos.z - normalizedPos.z;

            float fingerNormToPalmX = PalmNormPos.x - fingersPalmDiffX;
            float fingerNormToPalmZ = (scaleFactor * PalmNormPos.z) - fingersPalmDiffZ;

            float fingerNormToPanelX = fingerNormToPalmX * _panelWidth;
            float fingerNormToPanelZ = fingerNormToPalmZ * _panelHeight;

            fingerPanelPos = new Vector(fingerNormToPanelX, 0, fingerNormToPanelZ);

            return fingerPanelPos;
        }
    }
}
