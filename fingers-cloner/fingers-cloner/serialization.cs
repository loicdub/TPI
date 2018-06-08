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
    public class Serialization
    {
        #region Intialization
        // serialize file name
        private string _fileName = "serialized-position.xml";

        // store all positions serialized
        List<MyHand> allPositions;

        // Hand to serialize
        private MyHand _handToSerialize;
        internal MyHand HandToSerialize { get => _handToSerialize; set => _handToSerialize = value; }
        #endregion

        public Serialization() { }

        public Serialization(MyHand handToSerialize)
        {
            this.HandToSerialize = handToSerialize;
        }

        public void serialize(MyHand Hand)
        {
            allPositions = deserialize();
            allPositions.Add(Hand);

            XmlSerializer serializer = new XmlSerializer(typeof(List<MyHand>));
            StreamWriter file = new StreamWriter(_fileName);
            serializer.Serialize(file, allPositions);
            file.Close();
        }

        public List<MyHand> deserialize()
        {
            allPositions = new List<MyHand>();

            if (File.Exists(_fileName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<MyHand>));
                StreamReader file = new StreamReader(_fileName);
                allPositions = (List<MyHand>)serializer.Deserialize(file);
                file.Close();
            }

            return allPositions;
        }
    }
}
