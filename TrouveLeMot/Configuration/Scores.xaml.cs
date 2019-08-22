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
            AfficherPseudo();
        }
        Joueur joueur = new Joueur();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void AfficherPseudo()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Joueur.xml");
            XmlNodeList xn = doc.SelectNodes("//Pseudo");
            foreach(XmlNode xnode in xn)
            {
                ListPseudo.Items.Add(xnode);
            }
            
             
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          
        }
    }
}
