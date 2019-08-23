using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using TrouveLeMot;

namespace Configuration
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ChargeMots();
        }
        Lexique lexique = new Lexique();
        Mots atrouver = new Mots();
        Niveau niveau = new Niveau();
        Manche manche = new Manche();
        Options options = new Options();
        /// <summary>
        /// Méthodes
        /// </summary>
        #region
        private void EnableBtn()
        {
            btnAdd.IsEnabled = true;
            btnRemove.IsEnabled = true;
            btnOrthographe.IsEnabled = true;
        }
        private void DisableBtn()
        {
            btnAdd.IsEnabled = false;
            btnRemove.IsEnabled = false;
            btnOrthographe.IsEnabled = false;
        }

        private void EnableRadioNiveau()
        {
            rBtnFacile.IsEnabled = true;
            rBtnDifficile.IsEnabled = true;
            rBtnExpert.IsEnabled = true;
        }

        private void DisableRadioNiveau()
        {
            rBtnFacile.IsEnabled = false;
            rBtnDifficile.IsEnabled = false;
            rBtnExpert.IsEnabled = false;
        }




        private void EnableTransfert()
        {
            btnTransfert.IsEnabled = true;
            btnAddTout.IsEnabled = true;
            btnSupprTout.IsEnabled = true;
            btnSupr.IsEnabled = true;
        }

        private void DisableTransfert()
        {
            btnTransfert.IsEnabled = false;
            btnAddTout.IsEnabled = false;
            btnSupprTout.IsEnabled = false;
            btnSupr.IsEnabled = false;
        }

        private void ChargeLexique()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"test.xml");
            XmlNodeList Xn = doc.SelectNodes("//string");
            foreach (XmlNode xNode in Xn)
            {
                listBoxLex.Items.Add(xNode.InnerText);
                lexique.Ajouter(xNode.InnerText);
            }
        }
        private void ChargeMots()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@"mots_choisis.xml");
            XmlNodeList Xn = doc.SelectNodes("//string");
            foreach (XmlNode xNode in Xn)
            {
                listBoxCible.Items.Add(xNode.InnerText);
                atrouver.Ajouter(xNode.InnerText);
            }
        }
        private bool isSaisieValid(string mot)
        {
            for (int i = 0; i < mot.Length; i++)
            {
                if (char.IsDigit(mot[i]))
                {
                    return false;
                }
            }
            if (listBoxLex.Items.Contains(mot))
            {
                return false;
            }
            if (System.Text.RegularExpressions.Regex.IsMatch(mot, "[/!@#?/}[}{]"))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(mot))
            {
                return false;
            }
            if (mot.Length < 5)
            {
                return false;
            }
            if (listBoxCible.Items.Contains(mot))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void AjoutMot(string mot)
        {
            listBoxLex.Items.Add(mot);
            lexique.Ajouter(mot);
            lexique.SaveXML(@"test.xml");
            txtBoxMot.Clear();
        }
        private void RetireMot()
        {
            if (listBoxLex.SelectedItem != null)
            {
                object selected = listBoxLex.SelectedItem.ToString();
                lexique.Remove(selected.ToString());
                lexique.SaveXML(@"test.xml");
                listBoxLex.Items.Remove(listBoxLex.SelectedItem);
            }
        }
        private void NiveauDifficile()
        {
            niveau.Difficile = true;
            //DisableBtn();
            listBoxLex.Items.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"test.xml");
            XmlNodeList Xn = doc.SelectNodes("//string");
            foreach (XmlNode xNode in Xn)
            {
                if (xNode.InnerText.Length > 7 & xNode.InnerText.Length < 11 & !listBoxLex.Items.Contains(xNode.InnerText))
                {
                    listBoxLex.Items.Add(xNode.InnerText);
                    //lexique.SaveXML(@"selection_niveau.xml");
                }
            }
        }
        private void NiveauExpert()
        {
            niveau.Expert = true;
            //DisableBtn();
            listBoxLex.Items.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"test.xml");
            XmlNodeList Xn = doc.SelectNodes("//string");
            foreach (XmlNode xNode in Xn)
            {
                if (xNode.InnerText.Length > 11 & !listBoxLex.Items.Contains(xNode.InnerText))
                {
                    listBoxLex.Items.Add(xNode.InnerText);
                    //lexique.SaveXML(@"selection_niveau.xml");
                }
            }
        }
        private void NiveauPerso()
        {
            niveau.Perso = true;
            EnableBtn();
            ChargeLexique();
        }
        private void NiveauFacile()
        {
            niveau.Facile = true;
            //DisableBtn();
            listBoxLex.Items.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"test.xml");
            XmlNodeList Xn = doc.SelectNodes("//string");
            foreach (XmlNode xNode in Xn)
            {
                if (xNode.InnerText.Length > 4 & xNode.InnerText.Length < 9 & !listBoxLex.Items.Contains(xNode.InnerText))
                {
                    listBoxLex.Items.Add(xNode.InnerText);
                    //lexique.SaveXML(@"selection_niveau.xml");
                }
            }
        }
        private void Transfert()
        {

            if (!listBoxCible.Items.Contains(listBoxLex.SelectedItem) && listBoxLex.SelectedItem != null)
            {
                listBoxCible.Items.Add(listBoxLex.SelectedItem);
                atrouver.Ajouter(listBoxLex.SelectedItem.ToString());
                atrouver.SaveXML(@"mots_choisis.xml");
                listBoxLex.Items.Remove(listBoxLex.SelectedItem);
            }

        }
        private void Suppr()
        {
            if (listBoxCible.SelectedItem != null)
            {
                listBoxLex.Items.Add(listBoxCible.SelectedItem);
                atrouver.Remove(listBoxCible.SelectedItem.ToString());
                atrouver.SaveXML(@"mots_choisis.xml");
                listBoxCible.Items.Remove(listBoxCible.SelectedItem);
            }
        }
        private void SupprDisable()
        {
            if (listBoxCible.Items.Count < 1)
            {
                btnSupr.IsEnabled = false;
            }
        }
        #endregion
        /// <summary>
        /// Bouttons
        /// </summary>
        #region
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string mot = txtBoxMot.Text;
            if (isSaisieValid(mot))
            {
                AjoutMot(mot);
            }
            e.Handled = true;
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            //XmlDocument doc = new XmlDocument();
            //doc.Save(@"mots_choisis.xml");
            this.Close();
        }
        private void BtnTransfert_Click(object sender, RoutedEventArgs e)
        {
            Transfert();
        }
        private void BtnSupr_Click(object sender, RoutedEventArgs e)
        {
            Suppr();
        }
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            RetireMot();
        }

        private void RBtnDifficile_Checked(object sender, RoutedEventArgs e)
        {
            NiveauDifficile();
        }

        private void RBtnExpert_Checked(object sender, RoutedEventArgs e)
        {
            NiveauExpert();
        }

        private void RBtnPerso_Checked(object sender, RoutedEventArgs e)
        {
            NiveauPerso();
        }

        private void RBtnFacile_Checked(object sender, RoutedEventArgs e)
        {
            NiveauFacile();
        }
        // A implémenter afin de simplifier le code
        //private void RbNiveau()
        //{
        //    RadioButton radioBtn = new RadioButton();
        //    switch (radioBtn.Name)
        //    {
        //        case "RBtnFacile":
        //            NiveauFacile();
        //            break;

        //        case "RBtnPerso":
        //            NiveauPerso();
        //            break;

        //        case "RBtnExpert":
        //            NiveauExpert();
        //            break;

        //        case "RBtnDifficile":
        //            NiveauDifficile();
        //            break;
        //    }
        //}
        #endregion

        private void NupManches_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //int manches = nupManches.Value.Value;
            options.NombreManches = nupManches.Value.Value;
            options.SaveXML(@"Options.xml");
        }
        private void NupEssais_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            options.NbEssais = nupEssais.Value.Value;
            options.SaveXML(@"Options.xml");
        }
        private void ListBoxCible_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SupprDisable();
            //options.LoadXML(@"mots_choisis.xml");
        }
        private void NupDurée_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            options.Temps = nupDurée.Value.Value;
            options.SaveXML(@"Options.xml");
        }

        private void BtnTri_Click(object sender, RoutedEventArgs e)
        {
            listBoxLex.Items.SortDescriptions.Add(
            new System.ComponentModel.SortDescription("",
            System.ComponentModel.ListSortDirection.Ascending));
        }

        private void NupPtPerdu_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            options.NbPoinPerdus = nupPtPerdu.Value.Value;
            options.SaveXML(@"Options.xml");
        }

        private void RBtnLexic_Checked(object sender, RoutedEventArgs e)
        {
            EnableBtn();
            DisableTransfert();
            DisableRadioNiveau();
            listBoxLex.Items.Clear();
            ChargeLexique();
        }

        private void RBtnDifficult_Checked(object sender, RoutedEventArgs e)
        {
            EnableRadioNiveau();
            EnableTransfert();
            rBtnFacile.IsChecked = true;
            DisableBtn();
            NiveauFacile();
        }

        private void BtnSupprTout_Click(object sender, RoutedEventArgs e)
        {
            foreach (var itemListSuppr in listBoxCible.Items)
            {
                if (itemListSuppr != null && !listBoxLex.Items.Contains(itemListSuppr))
                {
                    listBoxLex.Items.Add(itemListSuppr);
                    atrouver.Remove(itemListSuppr.ToString());
                    atrouver.SaveXML(@"mots_choisis.xml");
                }
            }
            foreach (var itemListSuppr2 in listBoxLex.Items)
            {
                listBoxCible.Items.Remove(itemListSuppr2);
            }

        }

        private void BtnAddTout_Click(object sender, RoutedEventArgs e)
        {
            foreach (var itemList in listBoxLex.Items)
            {
                if (itemList != null && !listBoxCible.Items.Contains(itemList))
                {
                    listBoxCible.Items.Add(itemList);
                    atrouver.Ajouter(itemList.ToString());
                    atrouver.SaveXML(@"mots_choisis.xml");
                }
            }
            foreach (var itemList2 in listBoxCible.Items)
            {
                listBoxLex.Items.Remove(itemList2);
            }
        }

        private void GroupBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void TxtBoxMot_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            txtBoxMot.Text = "";
        }
    }
}
