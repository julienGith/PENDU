using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TrouveLeMot
{
    [Serializable()]
    public class Options
    {
        private int _nombreManches;
        private int _temps;
        private int _nbPoinPerdus;
        private int _nbEssais;

        public int NombreManches { get; set; }
        public int Temps { get; set; }
        public int NbPoinPerdus { get; set; }
        public int NbEssais { get; set; }

        public Options() { }
        public Options(int NombreManches= 1, int Temps= 60, int NbPoinPerdus= 0, int NbEssais=7)
        {
            this.NombreManches = NombreManches;
            this.Temps = Temps;
            this.NbPoinPerdus = NbPoinPerdus;
            this.NbEssais = NbEssais;
        }
        public void SaveXML(string path)
        {
            XmlSerializer writer = new XmlSerializer(typeof(Options));
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            writer.Serialize(file, this);
            file.Close();
        }
        public void LoadXML(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Options));
            FileStream file = new FileStream(path, FileMode.Open);
            serializer.Deserialize(file);
            file.Close();
        }
    }
}
