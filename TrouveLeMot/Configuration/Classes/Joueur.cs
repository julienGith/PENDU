using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration
{
    [Serializable()]
    public class Joueur
    {
        private string _pseudo;
        private int _score;

        public string Pseudo { get; set; }
        public int Score { get ; set; }

        public Joueur() { }
        public Joueur(string pseudo, int score) { }

        public void SaveXML(string path)
        {
            XmlSerializer writer = new XmlSerializer(typeof(Joueur));
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            writer.Serialize(file, this);
            file.Close();
        }
        public void LoadXML(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Joueur));
            FileStream file = new FileStream(path, FileMode.Open);
            serializer.Deserialize(file);
            file.Close();
        }
    }
}
