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
        #region Initialization
        // set
        // List of detected hands and the first detected hand
        private List<Hand> _hands;
        private Hand _firstHand;

        // Palm raw, normalized and stabilized location
        private Vector _palmPos;
        private Vector _palmNormPos;

        // List of all the detected fingers
        private List<Finger> _fingers;

        // Fingers raw and normalized location
        private List<Vector> _fingersPos;
        private List<Vector> _fingersNormPos;

        // User's hand
        private MyHand _userHand;

        // get
        public List<Hand> Hands { get => _hands; set => _hands = value; }
        public Hand FirstHand { get => _firstHand; set => _firstHand = value; }
        public List<Finger> Fingers { get => _fingers; set => _fingers = value; }
        public List<Vector> FingersStabPos { get => _fingersPos; set => _fingersPos = value; }
        public List<Vector> FingersNormPos { get => _fingersNormPos; set => _fingersNormPos = value; }
        public Vector PalmPos { get => _palmPos; set => _palmPos = value; }
        public Vector PalmNormPos { get => _palmNormPos; set => _palmNormPos = value; }
        public MyHand UserHand { get => _userHand; set => _userHand = value; }
        #endregion
        
        /// <summary>
        /// Leap Motion's default constructor
        /// </summary>
        public LeapController()
        {
            EventContext = WindowsFormsSynchronizationContext.Current;
            FrameReady += newFrameHandler;
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

                for (int i = 0; i < Fingers.Count; i++)
                {
                    FingersStabPos.Add(Fingers[i].StabilizedTipPosition);
                    FingersNormPos.Add(iBox.NormalizePoint(FingersStabPos[i]));
                }

                UserHand = new MyHand(PalmNormPos, FingersNormPos);
            }
        }
    }
}
