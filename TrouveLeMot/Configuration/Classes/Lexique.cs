using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace TrouveLeMot
{/// <summary>
/// Lexique "Lexique" qui dérive de List.
/// </summary>
    [Serializable()]
   public class Lexique : List<string>
    {
        /// <summary>
        /// Constructeur par défaut.
        /// </summary>
        public Lexique(){ }
        /// <summary>
        /// Methode pour ajouter un mot au Lexique si il n'est pas déjà présent.
        /// </summary>
        /// <param name="mot"></param>
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
            base.Remove(mot);
        }
        #region 
        /// <summary>
        /// Methodes de sauvegarde/chargement du Lexique.
        /// </summary>
        /// <param name="path"></param>
        public void SaveXML(string path)
        {
            XmlSerializer writer = new XmlSerializer(typeof(Lexique));
            FileStream file = new FileStream(path,FileMode.Create,FileAccess.Write);
            writer.Serialize(file, this);
            file.Close();
        }
        /// <summary>
        /// A faire intégrer à la méthodes le remplissage de la liste lexique. La méthode lit mais n'enregistre rien dans la liste.
        /// </summary>
        /// <param name="path"></param>
        public void LoadXML(string path)
        {
            //XmlSerializer serializer = new XmlSerializer(typeof(Lexique));
            //FileStream file = new FileStream(path, FileMode.Open);
            //serializer.Deserialize(file);

            XmlDocument doc = new XmlDocument();
            doc.Load(@"test.xml");

            XmlNodeList Xn = doc.SelectNodes("//string");
            foreach (XmlNode xNode in Xn)
            {
                base.Add(xNode.InnerText.ToString());
            }
            
            //file.Close();
        }
        #endregion
    }
}
