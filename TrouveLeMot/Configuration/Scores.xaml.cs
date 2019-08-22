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
            //joueur.LoadXML(@"joueur.xml");
            //AfficherPseudo();
            //AfficherScore();
            AfficherSP();


        }
        ListJoueurs joueurs = new ListJoueurs();
        Joueur joueur = new Joueur();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        //private void AfficherPseudo()
        //{
        //    ListPseudo.Items.Add(joueur.Pseudo);
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(@"Joueur.xml");
        //    XmlNodeList xn = doc.SelectNodes("//Pseudo");
        //    foreach (XmlNode xnode in xn)
        //    {
        //       ListPseudo.Items.Add(xnode);
        //    }
        //}
        //private void AfficherScore()
        //{
        //    //ListPoints.Items.Add(joueur.Score);
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(@"Joueur.xml");
        //    XmlNodeList xn = doc.SelectNodes("//Score");
        //    foreach (XmlNode xnode in xn)
        //    {
        //        ListPoints.Items.Add(xnode);
        //    }
        //}

        private void AfficherSP()
        {
            //joueurs.ReadTxt("Liste_des_joueurs.txt");
            FileStream fsr = new FileStream("Liste_des_joueurs.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            StreamReader sr = new StreamReader(fsr);
            string enr = sr.ReadLine();
            string[] chaine = enr.Split(';');

            while (enr != null)
            {
                Joueur joueur = new Joueur();
                joueur.Pseudo = chaine[0];
                joueur.Score = int.Parse(chaine[1]);
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
    }
}
