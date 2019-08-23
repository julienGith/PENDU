using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Configuration
{/// <summary>
/// Liste des mots à trouver "Mots" qui dérive de List.
/// </summary>
    [Serializable()]
    public class Mots : List<string>
    {
        private int _nombreMots;
        private string _motCach;
        public int NombreMots
        { get; set; }
        /// <summary>
        /// Choisi un mot aléatoirement dans la liste des mots à trouver.
        /// </summary>
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
        /// <summary>
        /// Methode pour ajouter un mot à la liste des mots à trouver si il n'est pas déjà présent.
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
        /// <summary>
        /// Permet de retirer un mot de la liste des mot a trouver.
        /// Evite une exception en laissant un mot dans la liste.
        /// </summary>
        /// <param name="mot"></param>
        public new void Remove(string mot)
        {
            if (this.Count>1)
            {
                base.Remove(mot);
            }
        }
        /// <summary>
        /// Methodes de sauvegarde/chargement du Lexique.
        /// </summary>
        /// <param name="path"></param>
        #region
        public void SaveXML(string path)
        {
            XmlSerializer writer = new XmlSerializer(typeof(Mots));
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            writer.Serialize(file, this);
            file.Close();
        }
        /// <summary>
        /// A faire intégrer à la méthodes le remplissage de la liste lexique. La méthode lit mais n'enregistre rien dans la liste.
        /// </summary>
        public void LoadXML(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Mots));
            FileStream file = new FileStream(path, FileMode.Open);
            serializer.Deserialize(file);
            file.Close();
        }
        #endregion
    }
}