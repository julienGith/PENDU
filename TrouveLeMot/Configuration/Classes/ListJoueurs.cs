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
        /// <summary>
        /// Permet la lecture du fichier texte contenant la liste des joueurs.
        /// Créé des chaines en découpant la ligne lu à chaque ";".
        /// Récupère les pseudo et les scores dans un tableau de chaine.
        /// Affecte des valeurs au champs de la classe joueur.
        /// </summary>
        /// <param name="path"></param>
        public void ReadTxt(string path)
        {
            FileStream fsr = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fsr);

            while (sr.Peek() >= 0)
            {
                string enr = sr.ReadLine();
                string[] chaine = enr.Split(';');

                    Joueur joueur = new Joueur();
                    joueur.Pseudo = chaine[0];
                    joueur.Score = int.Parse(chaine[1]);
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
