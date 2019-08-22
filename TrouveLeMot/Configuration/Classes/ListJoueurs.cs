using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration
{
    public class ListJoueurs : List<Joueur>
    {
        public override string ToString()
        {
            Joueur joueur = new Joueur();
            return  joueur.Pseudo + ";" + joueur.Score + ";";
        }
        public void SaveText(string path)
        {
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

            foreach (Joueur item in this)
            {

                sw.WriteLine(item.ToString());
            }
            sw.Close();
            fs.Close();


        }
        public void ReadTxt(string path)
        {
            FileStream fsr = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fsr);

            while (sr.Peek() >= 0)
            {
                string enr = sr.ReadLine();
                string[] chaine = enr.Split(new char[] { ';' });
                if (chaine.Length == 6)
                {
                    Joueur joueur = new Joueur();
                    joueur.Pseudo = chaine[1];
                    
                }

            }

            fsr.Close();
            sr.Close();
        }

        public void SaveXML(string path)
        {
            XmlSerializer writer = new XmlSerializer(typeof(ListJoueurs));
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            writer.Serialize(file, this);
            file.Close();
        }
        public void LoadXML(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ListJoueurs));
            FileStream file = new FileStream(path, FileMode.Open);
            serializer.Deserialize(file);
            file.Close();
        }
    }
}
