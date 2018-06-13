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
        private string _positionName;
        private string _dirName;
        private string _filePath;

        // store all positions serialized
        List<MyHand> allPositions;
        List<string> allFilesName;

        // Hand to serialize
        private MyHand _handToSerialize;
        internal MyHand HandToSerialize { get => _handToSerialize; set => _handToSerialize = value; }
        public string PositionName { get => _positionName; set => _positionName = value; }
        public string DirName { get => _dirName; set => _dirName = value; }
        public string FilePath { get => _filePath; set => _filePath = value; }
        #endregion

        public Serialization()
        {
            DirName = "serial";

            Path.GetFileName(DirName);
        }

        public Serialization(MyHand handToSerialize)
        {
            this.HandToSerialize = handToSerialize;
            DirName = "serial";
        }

        public void serialize(MyHand Hand, string name)
        {
            PositionName = name;
            FilePath = DirName + "/" + PositionName + ".xml";

            XmlSerializer serializer = new XmlSerializer(typeof(MyHand));
            StreamWriter file = new StreamWriter(FilePath);
            serializer.Serialize(file, Hand);
            file.Close(); 
        }

        public List<MyHand> deserialize()
        {
            allPositions = new List<MyHand>();
            allFilesName = getFileName();
            
            if (Directory.Exists(DirName))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(MyHand));
                foreach (string position in allFilesName)
                {
                    FileStream stream = new FileStream(position, FileMode.Open);
                    allPositions.Add((MyHand)serializer.Deserialize(stream));
                    stream.Close();
                }                
            }

            return allPositions;
        }

        public List<string> getFileName()
        {
            allFilesName = new List<string>();
            
            foreach (string fileName in Directory.GetFiles(DirName))
            {
                allFilesName.Add(fileName);
            }

            return allFilesName;
        }

        public void deletePosition(string posName) {
            FilePath = DirName + "/" + posName + ".xml";

            File.Delete(FilePath);
        }
    }
}
