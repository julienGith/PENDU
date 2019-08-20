using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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
using System.Windows.Threading;


namespace Configuration
{
    /// <summary>
    /// Logique d'interaction pour Jeu.xaml
    /// </summary>
    public partial class Jeu : Window
    {
        Mots atrouver = new Mots();
        Options options = new Options();
        DispatcherTimer chrono = new DispatcherTimer();
        int i = 1;
        int j = 0;


        public Jeu()
        {
            InitializeComponent();
            options.LoadXML(@"Options.xml");
            Chrono();
            nbLettres();
            TxtBjoueur_TextChange();

        }
        /// <summary>
        /// Methodes
        /// </summary>
        #region
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
            txtBmotCach.Text = atrouver.MotCach;
            atrouver.Remove(txtBmotCach.Text);
            atrouver.SaveXML(@"mots_choisis.xml");
            lblnbLettres.Content = "Le mot fait : " + txtBmotCach.Text.Length + " lettres.";
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
            txtBmanche.Text = i.ToString();
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
                lblWinOrLose.Content = "Perdu ! temps écoulé. Il fallait trouver : " + txtBmotCach.Text; ;
            }
        }
        #endregion
        /// <summary>
        /// Evènement bouttons
        /// </summary>
        #region
        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {


            atrouver.SaveXML(@"mots_choisis.xml");
            InitializeComponent();
            options.LoadXML(@"Options.xml");
            txtBmotCach.Text = atrouver.MotCach;
            atrouver.Remove(txtBmotCach.Text);
            //Chrono();
            //chrono.Stop();
            //chrono.Start();

            lblWinOrLose.Content = "";
            //txtBlettres.Text = "a";

            txtBnote.Text = "Aidez-vous en formant des mots avec les lettres trouvées. Les lettres trouvées peuvent être présentes plusieurs fois dans le mot caché.";
            lblnbLettres.Content = ("Le mot fait :{0} lettres.", txtBmotCach.Text.Length);

            //for (int i = 0; i <= txtBmotCach.Text.Length+1; i++)
            //{
            //    txtBlettres.Text = "-";
            //    string temp = txtBlettres.Text;
            //    txtBlettres.Text = temp + "-";
            //}


            txtBjoueur.Text = "Entrez un mot ou des lettres et tentez";

            if (i < int.Parse(txtBnbManches.Text))
            {
                if (atrouver.Count > 0)
                {
                    atrouver.Remove(txtBmotCach.Text);
                }
                txtBmotCach.Text = atrouver.MotCach;
                txtBmanche.Text = (++i).ToString();

            }

        }






        private void BtnTry_Click(object sender, RoutedEventArgs e)
        {
            char[] tabMotCach = txtBmotCach.Text.ToCharArray();
            char[] tabMotJoueur = txtBjoueur.Text.ToCharArray();
            char[] tabMotPendu = txtBlettres.Text.ToCharArray();
            string motJoueur = txtBjoueur.Text;
            string motCach = txtBmotCach.Text;
            string motPendu = txtBlettres.Text;
            int indexJoueur = motJoueur.IndexOf(txtBjoueur.Text);

            //char[] caractMotCach = tabMotJoueur;
            //char[] caractMotPendu = tabMotPendu;


            foreach (char caractMotCach in tabMotJoueur)
            {
                if (tabMotCach.Contains(caractMotCach) & !txtBlettres.Text.Contains(caractMotCach.ToString()))
                {

                    //string motJoueur = txtBjoueur.Text;
                    int indexCach = motCach.IndexOf(caractMotCach.ToString());
                    int indexPendu = indexCach;
                    //
                    lblTrouveLettres.Content += /*indexJoueur+*/ caractMotCach.ToString() + (indexCach + 1) + "," + indexPendu;
                    //if (MotJoueur.equals(MotCach))      



                    foreach (char caractMotJoueur in tabMotJoueur)
                    {

                        if (motJoueur[indexJoueur].Equals(motCach[indexCach]))
                        {

                            txtBlettres.Text = txtBmotCach.Text + "tamere" + (indexCach) + indexJoueur + indexPendu;
                        }
                        //int indexCach = motCach.IndexOf(tabMotCach.ToString());

                        //if (indexCach!=0)/*motJoueur[indexJoueur] == motCach[indexCach]*/
                        //{
                        //    txtBlettres.Text = txtBmotCach.Text + "tamere"+indexCach;
                        //}
                    }

                }
            }




            int penalty = int.Parse(txtBessai.Text);

            if (txtBjoueur.Text == txtBmotCach.Text)
            {
                lblWinOrLose.Content = "Bravo ! Vous avez trouvé le mot caché";
                chrono.Stop();
                int score = options.Temps - int.Parse(txtBcompteur.Text) - penalty;
                lblScore.Content = score.ToString();
            }
            if (txtBessai.Text == txtBnbEssais.Text)
            {
                lblWinOrLose.Content = "Perdu ! Il fallait trouver : " + txtBmotCach.Text;
            }
            if (i < int.Parse(txtBnbEssais.Text))
            {
                txtBessai.Text = (++i).ToString();
            }
        }


        #endregion

        private void TxtBlettres_TextChanged(object sender, TextChangedEventArgs e)
        {
            modifIndex();
        }

        private void modifIndex()
        {


        }

        private void TxtBnote_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtBjoueur_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtBjoueur_MouseEnter(object sender, MouseEventArgs e)
        {
            TxtBjoueur_TextChange2();
        }

        private void TxtBjoueur_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TxtBjoueur_TextChange2();
        }

        private void TxtBpenalty_TextChanged(object sender, TextChangedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"Options.xml");
            XmlNodeList Xn = doc.SelectNodes("//NbPoinPerdus");
            foreach (XmlNode xNode in Xn)
            {
                options.NbPoinPerdus = int.Parse(xNode.InnerText);

            }
            txtBpenalty.Text = (int.Parse(txtBessai.Text) * options.NbPoinPerdus).ToString();
        }
    }


}

