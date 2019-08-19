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
using TrouveLeMot;

namespace Configuration
{
    /// <summary>
    /// Logique d'interaction pour AccueilJeu.xaml
    /// </summary>
    public partial class AccueilJeu : Window
    {
        public AccueilJeu()
        {
            InitializeComponent();
            
        }
        Joueur joueur = new Joueur();
        

        private void BtnOptions_Click(object sender, RoutedEventArgs e)
        {
            MainWindow option = new MainWindow();
            option.ShowDialog();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            Jeu jeu = new Jeu();
            jeu.ShowDialog();
        }

        private void TxtBpseudo_TextChanged(object sender, TextChangedEventArgs e)
        {
            joueur.Pseudo = txtBpseudo.Text;
            joueur.SaveXML(@"Joueur.xml");
        }
    }
}
