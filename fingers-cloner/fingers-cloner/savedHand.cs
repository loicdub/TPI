using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// References to add
using Leap;
using System.Xml;

namespace fingers_cloner
{
    [Serializable]
    public class savedHand
    {
        private List<Vector> _fingers;
        private Vector _palm;
        private string _name;
        private string _description;

        public List<Vector> Fingers { get => _fingers; set => _fingers = value; }
        public Vector Palm { get => _palm; set => _palm = value; }
        public string Name { get => _name; set => _name = value; }
        public string Description { get => _description; set => _description = value; }

        public savedHand() { }

        public savedHand(List<Vector> fingers, Vector palm, string name, string description)
        {
            this.Fingers = fingers;
            this.Palm = palm;
            this.Name = name;
            this.Description = description;
        }
    }
}
