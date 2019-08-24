using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using TrouveLeMot;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Text;

namespace Configuration
{
    /// <summary>
    /// Logique d'interaction pour Jeu.xaml
    /// A faire mettre à jour le code de lecture des fichiers de sauvegarde de toutes les classes.
    /// </summary>
    public partial class Jeu : Window
    {
        Mots atrouver = new Mots();
        Options options = new Options();
        Joueur joueur = new Joueur();
        ListJoueurs joueurs = new ListJoueurs();
        DispatcherTimer chrono = new DispatcherTimer();
        int i = 0;
        int j = 0;
        int k = 1;
        /// <summary>
        /// Regroupe  les methodes exécutées à l'ouverture de la fenêtre jeu.
        /// </summary>
        public Jeu()
        {
            InitializeComponent();
            options.LoadXML(@"Options.xml");
            Chrono();
            nbLettres();
            IsNextEnable();
            RecupPseudo();
            GenereClavier();
        }
        

        private void GenereClavier()
        {
            string alpha = "abcdefghijklmnopqrstuvwxyz";
            int margeHaut = 15; int margeGauche = 15;
            Thickness myThickness = new Thickness();
            myThickness.Left = margeGauche;
            myThickness.Top = margeHaut;
            foreach (char item in alpha)
            {
                Button button = new Button();
                button.Width =25;
                button.Height = 25;
                button.Click += Button_Click;
                button.Content = item;
                
                button.Tag = item;
                button.Margin = myThickness;
                wPclavier.Children.Add(button);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            char[] tabMotCach = txtBmotCach.Text.ToCharArray();
            List<int> stockPos = new List<int>();
            string chaine = ((Button)sender).Content.ToString();
            char lettre = chaine[0];
            string temp = txtBlettres.Text;
                for (int i = 0; i < tabMotCach.Length; i++)
                {
                    if (lettre == tabMotCach[i])
                    {
                        stockPos.Add(i);
                    }
                }
                foreach (int item in stockPos)
                {
                    StringBuilder str = new StringBuilder(temp);
                    str[item] = lettre;
                    temp = str.ToString();
                    txtBlettres.Text = temp;
                }
                           
            ((Button)sender).IsEnabled = false;
            txtBpenalty.Text = ((int.Parse(txtBessai.Text) - 1) * options.NbPoinPerdus).ToString();
            if (txtBessai.Text == (options.NbEssais - 1).ToString())
            {
                lblWinOrLose.Content = "Perdu ! Il fallait trouver : " + txtBmotCach.Text;
                wPclavier.IsEnabled = false;
                btnTry.IsEnabled = false;
            }
            if (i < int.Parse(txtBnbEssais.Text))
            {
                txtBessai.Text = (++i).ToString();
            }
            if (txtBmotCach.Text == txtBlettres.Text)
            {
                lblWinOrLose.Content = "Bravo ! Vous avez trouvé le mot caché";
                chrono.Stop();
                int score = options.Temps - int.Parse(txtBcompteur.Text) - int.Parse(txtBpenalty.Text);
                lblScore.Content = score.ToString();
                joueur.Score += score;
                lblScorePartie.Content = joueur.Score;
                joueur.SaveXML(@"Joueur.xml");
                joueurs.Add(joueur);
                joueurs.SaveText("Liste_des_joueurs.txt");
                joueurs.SaveXML(@"ListeJoueurs.xml");
                btnTry.IsEnabled = false;
                txtBlettres.Clear();
            }

        }

        /// <summary>
        /// Methodes
        /// </summary>
        #region
        private void RecupPseudo()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load(@"Joueur.xml");
            XmlNodeList xn = doc.SelectNodes("//Pseudo");
            foreach (XmlNode xnode in xn)
            {
                joueur.Pseudo = xnode.InnerText.ToString();
            }
        }
        /// <summary>
        /// Désactive le bouton manche suivante quand la dernière manche est atteinte.
        /// </summary>
        private void IsNextEnable()
        {
            if (int.Parse(txtBmanche.Text) == int.Parse(txtBnbManches.Text))
            {
                btnNext.IsEnabled = false;
            }
        }
        /// <summary>
        /// Déclenche le chrono. Lie la propriété tick du chrono à l'évènement délégué chrono_tick. Détermine le temps entre 2 ticks.
        /// </summary>
        private void Chrono()
        {
            chrono.Start();
            chrono.Tick += chrono_Tick;
            chrono.Interval = new TimeSpan(0, 0, 1);
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
        }
        /// <summary>
        /// Remplie la texteBox lettres avec autant de tirets que de lettres dans le mot caché.
        /// </summary>
        private void nbLettres()
        {
            for (int i = 1; i <= txtBmotCach.Text.Length; i++)
            {
                string temp = txtBlettres.Text;
                txtBlettres.Text = temp + "-";
            }
        }
        #endregion
        /// <summary>
        /// Evènements
        /// </summary>
        #region
        /// <summary>
        /// Permet l'incrémentation de j à chaque tick du chrono. Affiche le chrono (i) dans la texteBox compteur.
        /// </summary>
        private void chrono_Tick(object sender, EventArgs e)
        {
            j++;
            txtBcompteur.Text = j.ToString();
        }
        /// <summary>
        /// Remplie la texteBox motcach avec un mot pris au hazard dans la liste des mots à trouver.
        /// Le mot est supprimé afin de ne pas être réutilisé.
        /// La liste des mots à trouver est sauvegardée.
        /// </summary>
        private void TxtBmotCach_TextChanged(object sender, TextChangedEventArgs e)
        {

            ChargeMots();
            txtBmotCach.Text = atrouver.MotCach;
            atrouver.Remove(txtBmotCach.Text);
            atrouver.SaveXML(@"mots_choisis.xml");
        }

        private void TxtBnbManches_TextChanged(object sender, TextChangedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Options.xml");
            XmlNodeList Xn = doc.SelectNodes("//NombreManches");
            foreach (XmlNode xNode in Xn)
            {
                options.NombreManches = int.Parse(xNode.InnerText);

            }
            txtBnbManches.Text = options.NombreManches.ToString();

        }
        /// <summary>
        /// Affiche l'int k dans la texteBox manche.
        /// </summary>
        private void TxtBmanche_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBmanche.Text = k.ToString();
        }
        private void TxtBnbEssais_TextChanged(object sender, TextChangedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Options.xml");
            XmlNodeList Xn = doc.SelectNodes("//NbEssais");
            foreach (XmlNode xNode in Xn)
            {
                options.NbEssais = int.Parse(xNode.InnerText);

            }
            txtBnbEssais.Text = options.NbEssais.ToString();

        }
        /// <summary>
        /// Affiche l'int i dans la texteBox essai.
        /// </summary>
        private void TxtBessai_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtBessai.Text = i.ToString();
        }
        private void TxtBtemps_TextChanged(object sender, TextChangedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Options.xml");
            XmlNodeList Xn = doc.SelectNodes("//Temps");
            foreach (XmlNode xNode in Xn)
            {
                options.Temps = int.Parse(xNode.InnerText);

            }
            txtBtemps.Text = options.Temps.ToString();
        }
        /// <summary>
        /// Permet l'affichage d'un message indique que la manche est perdue car le temps imparti est écoulé.
        /// Désactive le bouton tenter afin que le joueur ne puisse pas poursuivre.
        /// A faire supprimer la concaténation de la variable txtBmotCach.Text.
        /// </summary>
        private void TxtBcompteur_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtBcompteur.Text == options.Temps.ToString())
            {
                chrono.Stop();
                lblWinOrLose.Content = "Perdu ! temps écoulé. Il fallait trouver :" + txtBmotCach.Text;
                btnTry.IsEnabled = false;
            }
        }
        #endregion
        /// <summary>
        /// Evènement bouttons
        /// </summary>
        #region
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            j = 0;
            chrono.Start();
            i = 0;
            txtBessai.Text = i.ToString();
            btnTry.IsEnabled = true;
            lblWinOrLose.Content = "";
            txtBlettres.Text = "";
            txtBjoueur.Text = "Mot caché trouvé ? Tapez le ici et tentez";
            wPclavier.IsEnabled = true;
            if (int.Parse(txtBmanche.Text) == int.Parse(txtBnbManches.Text) - 1)
            {
                btnNext.IsEnabled = false;
            }
            if (atrouver.Count > 0)
            {
                atrouver.Remove(txtBmotCach.Text);
                atrouver.SaveXML(@"mots_choisis.xml");
            }
            txtBmotCach.Text = atrouver.MotCach;
            txtBmanche.Text = (++k).ToString();
            txtBlettres.Clear();
            for (int i = 1; i <= txtBmotCach.Text.Length; i++)
            {
                string temp = txtBlettres.Text;
                txtBlettres.Text = temp + "-";
            }
        }

        private void BtnTry_Click(object sender, RoutedEventArgs e)
        {
            txtBpenalty.Text = ((int.Parse(txtBessai.Text)-1) * options.NbPoinPerdus).ToString();
            char[] tabMotCach = txtBmotCach.Text.ToCharArray();
            List<int> stockPos = new List<int>();
            string temp = txtBlettres.Text;

            if (!string.IsNullOrEmpty(txtBjoueur.Text))
            {
                char lettreJoueur = txtBjoueur.Text[0];
                for (int i = 0; i < tabMotCach.Length; i++)
                {
                    if (lettreJoueur == tabMotCach[i])
                    {
                        stockPos.Add(i);
                    }
                }
                foreach (int item in stockPos)
                {
                    StringBuilder str = new StringBuilder(temp);
                    str[item] = lettreJoueur;
                    temp = str.ToString();
                    txtBlettres.Text = temp;
                }
                int penalty = int.Parse(txtBessai.Text);
                if (txtBjoueur.Text == txtBmotCach.Text)
                {
                    lblWinOrLose.Content = "Bravo ! Vous avez trouvé le mot caché";
                    chrono.Stop();
                    int score = options.Temps - int.Parse(txtBcompteur.Text) - int.Parse(txtBpenalty.Text);
                    lblScore.Content = score.ToString();
                    joueur.Score += score;
                    lblScorePartie.Content = joueur.Score;
                    joueur.SaveXML(@"Joueur.xml");
                    joueurs.Add(joueur);
                    joueurs.SaveText("Liste_des_joueurs.txt");
                    joueurs.SaveXML(@"ListeJoueurs.xml");
                    btnTry.IsEnabled = false;
                    txtBlettres.Clear();
                    txtBlettres.Text = txtBjoueur.Text;
                }
                if (txtBessai.Text == (options.NbEssais).ToString())
                {
                    lblWinOrLose.Content = "Perdu ! Il fallait trouver : " + txtBmotCach.Text;
                }
                if (i < int.Parse(txtBnbEssais.Text))
                {
                    txtBessai.Text = (++i).ToString();
                }
            }
            
        }
        private void BtnQuitter_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        private void TxtBpenalty_TextChanged(object sender, TextChangedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Options.xml");
            XmlNodeList Xn = doc.SelectNodes("//NbPoinPerdus");
            foreach (XmlNode xNode in Xn)
            {
                options.NbPoinPerdus = int.Parse(xNode.InnerText);

            }
            txtBpenalty.Text = ((int.Parse(txtBessai.Text) + 1) * options.NbPoinPerdus).ToString();
        }
        /// <summary>
        /// Supprime le texte de la texteBox joueur lorsqu'elle est sélectionnée.
        /// </summary>
        private void TxtBjoueur_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBjoueur.Clear();
        }
    }
}

