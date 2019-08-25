using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace Configuration
{
    /// <summary>
    /// Logique d'interaction pour Scores.xaml
    /// </summary>
    public partial class Scores : Window
    {
        public Scores()
        {
            InitializeComponent();
            AfficherSP();
            //AfficheJoueurs();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// A faire liaison des données avec le databinding sur la listview du xalm.
        /// Reprise de la méthode readtext de la classe liste joueur.
        /// Permet la lecture du fichier texte contenant la liste des joueurs.
        /// Créé des chaines en découpant la ligne lu à chaque ";".
        /// Récupère les pseudo et les scores dans un tableau de chaine.
        /// Affecte des valeurs au champs de la classe joueur.
        /// Tant que le reader trouve du texte le processus continue.
        /// Affiche les donnée dans des listesBox.
        /// </summary>
        /// <param name="path"></param>
        private void AfficherSP()
        {
            FileStream fsr = new FileStream("Liste_des_joueurs.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fsr);
            string enr = sr.ReadLine();
           
            ListJoueurs joueurs = new ListJoueurs();
            while (enr != null)
            {
                string[] chaine = enr.Split(';');
                Joueur joueur = new Joueur();
                joueur.Pseudo = chaine[0];
                joueur.Score = int.Parse(chaine[1]);
                joueurs.Add(joueur);
                DataContext = this;
                enr = sr.ReadLine();
                ListPseudo.Items.Add(joueur.Pseudo.ToString());
                ListPoints.Items.Add(joueur.Score.ToString());

                if (enr != null)
                {
                    chaine = enr.Split(';');
                }
            }
            fsr.Close();
            sr.Close();
        }
        ////Alimenter la liste joueurs.
        //public void AfficheJoueurs()
        //{
        //    ListJoueurs joueurs = new ListJoueurs();
        //    joueurs.ReadTxt("Liste_des_joueurs.txt");
        //    DataContext = this;

        //}

    }
}
