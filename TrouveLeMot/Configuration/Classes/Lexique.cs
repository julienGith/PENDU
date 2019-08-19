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
   public class Lexique : List<string>
    {
        public Lexique(){ }
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
        public void LoadXML(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Lexique));
            FileStream file = new FileStream(path, FileMode.Open);
            serializer.Deserialize(file);
            file.Close();
        }
        #endregion
    }
}
