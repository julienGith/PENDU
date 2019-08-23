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


        public Jeu()
        {
            InitializeComponent();
            options.LoadXML(@"Options.xml");
            Chrono();
            nbLettres();
            IsNextEnable();
            RecupPseudo();

        }
        /// <summary>
        /// Methodes
        /// </summary>
        #region
            private void IsBtnTryEnable()
        {
            if (!string.IsNullOrEmpty(txtBjoueur.Text))
            {
                //btnTry.IsEnabled = true;
            }
        }
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
        private void IsNextEnable()
        {
            if (int.Parse(txtBmanche.Text) == int.Parse(txtBnbManches.Text))
            {
                btnNext.IsEnabled = false;
            }
        }
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
        private void nbLettres()
        {

            for (int i = 1; i <= txtBmotCach.Text.Length; i++)
            {
                string temp = txtBlettres.Text;
                txtBlettres.Text = temp + "-";
            }
        }
        private void TxtBjoueur_TextChange()
        { txtBjoueur.Text = "Entrez un mot ou des lettres et tentez"; }
        private void TxtBjoueur_TextChange2()
        { txtBjoueur.Text = ""; }

        #endregion
        /// <summary>
        /// Evènements
        /// </summary>
        #region
        private void chrono_Tick(object sender, EventArgs e)
                {
                    j++;
                    txtBcompteur.Text = j.ToString();
                }
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
                private void TxtBcompteur_TextChanged(object sender, TextChangedEventArgs e)
                {
                    if (txtBcompteur.Text == options.Temps.ToString())
                    {
                        chrono.Stop();
                        lblWinOrLose.Content = "Perdu ! temps écoulé. Il fallait trouver : " + txtBmotCach.Text;


                    }
                }
        #endregion
        /// <summary>
        /// Evènement bouttons
        /// </summary>
        #region
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            ;
            j = 0;
            chrono.Start();
            i = 0;
            txtBessai.Text = i.ToString();
            btnTry.IsEnabled = true;
            lblWinOrLose.Content = "";
            txtBlettres.Text = "";
            txtBjoueur.Text = "Mot caché trouvé ? tapez le ici et tentez";
            if (int.Parse(txtBmanche.Text) == int.Parse(txtBnbManches.Text) - 1)
            {
                btnNext.IsEnabled = false;
            }

            //if (i < int.Parse(txtBnbManches.Text))
            //{
            if (atrouver.Count > 0)
            {
                atrouver.Remove(txtBmotCach.Text);
                atrouver.SaveXML(@"mots_choisis.xml");
            }
            txtBmotCach.Text = atrouver.MotCach;
            txtBmanche.Text = (++k).ToString();
            //}

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
            char[] tabMotJoueur = txtBjoueur.Text.ToCharArray();
            char[] tabMotPendu = txtBlettres.Text.ToCharArray();
            string motJoueur = txtBjoueur.Text;
            string motCach = txtBmotCach.Text;
            string motPendu = txtBlettres.Text;
            int indexJoueur = motJoueur.IndexOf(txtBjoueur.Text);
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
                if (txtBessai.Text == (options.NbEssais - 1).ToString())
                {
                    lblWinOrLose.Content = "Perdu ! Il fallait trouver : " + txtBmotCach.Text;
                }
                if (i < int.Parse(txtBnbEssais.Text))
                {
                    txtBessai.Text = (++i).ToString();
                }
            }
            
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

        private void TxtBjoueur_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBjoueur.Clear();

        }
    }
}

