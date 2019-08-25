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
using TrouveLeMot;

namespace Configuration
{
    /// <summary>
    /// Logique d'interaction pour AccueilJeu.xaml
    /// </summary>
    public partial class AccueilJeu : Window
    {
        Mots atrouver = new Mots();
        
        Joueur joueur = new Joueur();
        
        public AccueilJeu()
        {
            InitializeComponent();
            ChargeMots();
        }


        private void ChargeMots()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"mots_choisis.xml");
            XmlNodeList Xn = doc.SelectNodes("//string");
            foreach (XmlNode xNode in Xn)
            {
                atrouver.Ajouter(xNode.InnerText);
            }
            if (atrouver.Count == 0)
            {
                btnStart.IsEnabled = false;
            }
            if (atrouver.Count != 0)
            {
                btnStart.IsEnabled = true;
            }
        }

        private void BtnOptions_Click(object sender, RoutedEventArgs e)
        {
            MainWindow option = new MainWindow();
            btnStart.IsEnabled = true;
            option.ShowDialog();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"mots_choisis.xml");
            XmlNodeList Xn = doc.SelectNodes("//string");
            foreach (XmlNode xNode in Xn)
            {
                atrouver.Ajouter(xNode.InnerText);
            }
            if (atrouver.Count == 0)
            {
                txtBlkAccueil.Text = "Aucun mot à trouver choisi. Configurer la partie.";
            }
            if (atrouver.Count != 0)
            {
                Jeu jeu = new Jeu();
                jeu.ShowDialog();
            }
        }

        private void TxtBpseudo_TextChanged(object sender, TextChangedEventArgs e)
        {
            joueur.Pseudo = txtBpseudo.Text;
            joueur.SaveXML(@"Joueur.xml");
        }

        private void BtnScores_Click(object sender, RoutedEventArgs e)
        {
            Scores scores = new Scores();
            scores.ShowDialog();
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TxtBpseudo_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBpseudo.Clear();
        }

    }
}
