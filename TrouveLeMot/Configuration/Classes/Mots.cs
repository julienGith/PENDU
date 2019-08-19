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
    public class Mots : List<string>
    {
        private int _nombreMots;
        private string _motCach;
        public int NombreMots
        { get; set; }
        public string MotCach
        { get {
                Random rand = new Random();
                int aléatoire = 0;
                aléatoire = rand.Next(0, this.Count);
                if (this.Count<1)
                {

                }
                return this[aléatoire];
            } }
        public Mots() { }
        public void Ajouter(string mot)
        {
            bool trouve = false;

            foreach (string item in this)
            {
                if (item.Equals(mot))
                {
                    trouve = true;
                }
            }
            if (!trouve)
            {
                base.Add(mot);
            }
        }
        public new void Remove(string mot)
        {
            if (this.Count>1)
            {
                base.Remove(mot);
            }
            
        }
        //public void Pioche()
        //{
        //    Random rand = new Random();
        //    int aléatoire = 0;
        //    aléatoire = rand.Next(0, this.Count - 1);
        //    base.Add(this [aléatoire]);
        //    base.RemoveAt(aléatoire);
        //}

        public void SaveXML(string path)
        {
            XmlSerializer writer = new XmlSerializer(typeof(Mots));
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            writer.Serialize(file, this);
            file.Close();
        }
        public void LoadXML(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Mots));
            FileStream file = new FileStream(path, FileMode.Open);
            serializer.Deserialize(file);
            file.Close();
        }
    }
}
