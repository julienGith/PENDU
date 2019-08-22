using System;
using System.Collections.Generic;
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
            AfficherPseudo();
            AfficherScore();
            
        }
        Joueur joueur = new Joueur();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AfficherPseudo()
        {
            ListPseudo.Items.Add(joueur.Pseudo);
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Joueur.xml");
            XmlNodeList xn = doc.SelectNodes("//Pseudo");
            foreach (XmlNode xnode in xn)
            {
               ListPseudo.Items.Add(xnode);
            }


        }

        private void AfficherScore()
        {
            //ListPoints.Items.Add(joueur.Score);
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Joueur.xml");
            XmlNodeList xn = doc.SelectNodes("//Score");
            foreach (XmlNode xnode in xn)
            {
                ListPoints.Items.Add(xnode);
            }
        }
    }
}
