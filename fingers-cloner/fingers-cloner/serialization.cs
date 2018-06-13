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
        // serialize file, directory name and file path
        private string _positionName;
        private string _dirName;
        private string _filePath;

        // store all positions serialized
        List<MyHand> allPositions;

        // store all the files name
        List<string> allFilesName;

        // hand to serialize
        private MyHand _handToSerialize;
        internal MyHand HandToSerialize { get => _handToSerialize; set => _handToSerialize = value; }
        public string PositionName { get => _positionName; set => _positionName = value; }
        // directory name and file path
        public string DirName { get => _dirName; set => _dirName = value; }
        public string FilePath { get => _filePath; set => _filePath = value; }
        #endregion

        /// <summary>
        /// default constructor - initialize directory name
        /// </summary>
        public Serialization()
        {
            DirName = "serial";

            Path.GetFileName(DirName);
        }
        
        /// <summary>
        /// serialize a given MyHand object
        /// </summary>
        /// <param name="Hand">the hand to serialize</param>
        public void serialize(MyHand Hand)
        {
            PositionName = Hand.Name;
            FilePath = DirName + "/" + PositionName + ".xml";

            XmlSerializer serializer = new XmlSerializer(typeof(MyHand));
            StreamWriter file = new StreamWriter(FilePath);
            serializer.Serialize(file, Hand);
            file.Close(); 
        }

        /// <summary>
        /// deserialize all xml files in serial directory
        /// </summary>
        /// <returns>a list of all the serialize hands</returns>
        public List<MyHand> deserialize()
        {
            allPositions = new List<MyHand>();
            allFilesName = getFilesName();
            
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

        /// <summary>
        /// get all the files name in the serial directory
        /// </summary>
        /// <returns>a list of all the names of the positions</returns>
        public List<string> getFilesName()
        {
            allFilesName = new List<string>();
            
            foreach (string fileName in Directory.GetFiles(DirName))
            {
                allFilesName.Add(fileName);
            }

            return allFilesName;
        }

        /// <summary>
        /// delete a saved position
        /// </summary>
        /// <param name="posName">the name of the position to delete</param>
        public void deletePosition(string posName) {
            FilePath = DirName + "/" + posName + ".xml";

            File.Delete(FilePath);
        }
    }
}
