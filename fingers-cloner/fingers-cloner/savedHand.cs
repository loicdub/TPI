using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// References to add
using Leap;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

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

        List<savedHand> allPosition;
        
        public savedHand() { }

        public savedHand(List<Vector> fingers, Vector palm, string name, string description)
        {
            this.Fingers = fingers;
            this.Palm = palm;
            this.Name = name;
            this.Description = description;
        }

        public void serialize(string fileName, savedHand currentHand)
        {
            allPosition = deserialize(fileName);
            allPosition.Add(currentHand);

            XmlSerializer serializer = new XmlSerializer(typeof(List<savedHand>));
            StreamWriter file = new StreamWriter(fileName);
            serializer.Serialize(file, allPosition);
            file.Close();
        }

        public List<savedHand> deserialize(string fileName)
        {
            allPosition = new List<savedHand>();

            if (File.Exists(fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<savedHand>));
                StreamReader file = new StreamReader(fileName);
                allPosition = (List<savedHand>)serializer.Deserialize(file);
                file.Close();
            }

            return allPosition;
        }
    }
}
