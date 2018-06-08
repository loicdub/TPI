/*
 * Author  : Dubas Loïc
 * Class   : I.FA-P3B
 * School  : CFPT-I
 * Date    : June 2018
 * Descr.  : Store hand data
 * Version : 1.0 
 * Ext. dll: LeapCSharp.NET4.5
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// References to add
using Leap;

namespace fingers_cloner
{
    public class MyHand
    {
        #region Initialization
        // get
        private string _name;
        private string _description;
        private Vector _palmNormPos;
        private List<Vector> _fingersNormPos;
        // set
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }
        public Vector PalmNormPos { get => _palmNormPos; set => _palmNormPos = value; }
        public List<Vector> FingersNormPos { get => _fingersNormPos; set => _fingersNormPos = value; }
        #endregion

        /// <summary>
        /// default constructor
        /// </summary>
        public MyHand() { }

        /// <summary>
        /// MyHand constructor
        /// </summary>
        /// <param name="palmPosNorm">Normalized position of the palm</param>
        /// <param name="fingersPosNorm">List of normalized position of the fingers</param>
        public MyHand(Vector palmPosNorm, List<Vector> fingersPosNorm)
        {
            this.PalmNormPos = palmPosNorm;
            this.FingersNormPos = fingersPosNorm;
        }

        /// <summary>
        /// MyHand constructor
        /// </summary>
        /// <param name="name">Name of the position</param>
        /// <param name="description">Description of the position</param>
        /// <param name="palmPosNorm">Normalized position of the palm</param>
        /// <param name="fingersPosNorm">List of normalized position of the fingers</param>
        public MyHand(string name, string description, Vector palmPosNorm, List<Vector> fingersPosNorm)
        {
            this.Name = name;
            this.Description = description;
            this.PalmNormPos = palmPosNorm;
            this.FingersNormPos = fingersPosNorm;
        }
    }
}
