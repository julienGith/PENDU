using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
    ///  A faire mettre à jour le code de lecture des fichiers de sauvegarde de toutes les classes.
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
        /// METHODES
        /// </summary>
        #region
        /// <summary>
        /// Retraite le mot saisie.
        /// Applique la méthode ajoutMot au mot retraité.
        /// </summary>
        public string RetraiterMot(string mot)
        {
            mot = mot.Normalize(NormalizationForm.FormD);
            StringBuilder motCanonique = new StringBuilder();
            foreach (char caractere in mot)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(caractere);
                if (uc != UnicodeCategory.NonSpacingMark && char.IsLetter(caractere))
                {
                    motCanonique.Append(caractere);
                }
            }
            AjoutMot(motCanonique.ToString().ToUpper());
            return (motCanonique.ToString().ToUpper());

        }
        /// <summary>
        /// Méthode enable/disable des différents boutons.
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
        #endregion
        /// <summary>
        /// Méthode permetant la lecture du fichier XML contenant les mots du lexique et l'ajout de son contenu dans la listBox lexique.
        /// </summary>
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
        /// <summary>
        /// Méthode permetant la lecture du fichier XML contenant les mots choisis pour le jeu et l'ajout de son contenu dans la listBox mots à trouver.
        /// </summary>
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
        /// <summary>
        /// Méthode vérifiant la validité de la saisie de l'utilisateur afin de renseigner le lexique avec des mots valides.
        /// </summary>
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
        /// <summary>
        /// Méthode permettant l'ajout du mot saisie par l'utilisateur dans la listBox lexique et dans le fichier du XML contenant le lexique.
        /// </summary>
        private void AjoutMot(string mot)
        {
            listBoxLex.Items.Add(mot);
            lexique.Ajouter(mot);
            lexique.SaveXML(@"test.xml");
            txtBoxMot.Clear();
        }
        /// <summary>
        /// Méthode permettant le retrait d'un mot de la listBox lexique et du fichier XML contenant le lexique.
        /// </summary>
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
        /// <summary>
        /// Tri des mots du lexique en utilisant un lecteur de fichier XML qui lit le fichier XML contenant le lexique et retourne les mots de taille comprise entre 8 et 10 caractères.
        /// </summary>
        private void NiveauDifficile()
        {
            niveau.Difficile = true;
            listBoxLex.Items.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"test.xml");
            XmlNodeList Xn = doc.SelectNodes("//string");
            foreach (XmlNode xNode in Xn)
            {
                if (xNode.InnerText.Length > 7 & xNode.InnerText.Length < 11 & !listBoxLex.Items.Contains(xNode.InnerText))
                {
                    listBoxLex.Items.Add(xNode.InnerText);
                }
            }
        }
        /// <summary>
        /// Tri des mots du lexique en utilisant un lecteur de fichier XML qui lit le fichier XML contenant le lexique et retourne les mots de taille supèrieur à 10 caractères.
        /// </summary>
        private void NiveauExpert()
        {
            niveau.Expert = true;
            listBoxLex.Items.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"test.xml");
            XmlNodeList Xn = doc.SelectNodes("//string");
            foreach (XmlNode xNode in Xn)
            {
                if (xNode.InnerText.Length > 11 & !listBoxLex.Items.Contains(xNode.InnerText))
                {
                    listBoxLex.Items.Add(xNode.InnerText);
                }
            }
        }
        private void NiveauPerso()
        {
            niveau.Perso = true;
            EnableBtn();
            ChargeLexique();
        }
        /// <summary>
        /// Tri des mots du lexique en utilisant un lecteur de fichier XML qui lit le fichier XML contenant le lexique et retourne les mots de taille comprise entre 5 et 8 caractères.
        /// </summary>
        private void NiveauFacile()
        {
            niveau.Facile = true;
            listBoxLex.Items.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(@"test.xml");
            XmlNodeList Xn = doc.SelectNodes("//string");
            foreach (XmlNode xNode in Xn)
            {
                if (xNode.InnerText.Length > 4 & xNode.InnerText.Length < 9 & !listBoxLex.Items.Contains(xNode.InnerText))
                {
                    listBoxLex.Items.Add(xNode.InnerText);
                }
            }
        }
        /// <summary>
        /// Ajoute un mot du lexique à la liste des mots à trouver.
        /// La liste des mots à trouver et le lexique sont sauvergardé lors de l'ajout.
        /// </summary>
        private void Transfert()
        {

            if (!listBoxCible.Items.Contains(listBoxLex.SelectedItem) && listBoxLex.SelectedItem != null)
            {

                listBoxCible.Items.Add(listBoxLex.SelectedItem);
                lexique.Remove(listBoxLex.SelectedItem.ToString());
                lexique.SaveXML(@"test.xml");
                atrouver.Ajouter(listBoxLex.SelectedItem.ToString());
                atrouver.SaveXML(@"mots_choisis.xml");

                listBoxLex.Items.Remove(listBoxLex.SelectedItem);
            }

        }
        /// <summary>
        /// Transfère un mot de la liste des mots à trouver vers la liste du lexique.
        /// La liste des mots à trouver et le lexique sont sauvergardé lors de l'ajout.
        /// </summary>
        private void Suppr()
        {
            if (!listBoxLex.Items.Contains(listBoxCible.SelectedItem) && listBoxCible.SelectedItem != null)
            {
                lexique.Ajouter(listBoxCible.SelectedItem.ToString());
                lexique.SaveXML(@"test.xml");
                listBoxLex.Items.Add(listBoxCible.SelectedItem);
                atrouver.Remove(listBoxCible.SelectedItem.ToString());
                atrouver.SaveXML(@"mots_choisis.xml");
                listBoxCible.Items.Remove(listBoxCible.SelectedItem);
            }
        }
        /// <summary>
        /// Permet d'éviter une exeption en cas de tentative de retrait si la liste est vide. Le bouton est alors désactivé.
        /// </summary>
        private void SupprDisable()
        {
            if (listBoxCible.Items.Count < 1)
            {
                btnSupr.IsEnabled = false;
            }
        }
        #endregion
        /// <summary>
        /// BOUTTONS 
        /// Le bouton "Vérifier l'orthographe" n'est pas présent : il permet un changement de focus qui déclenche la vérification orthographique.
        /// </summary>
        #region
        /// <summary>
        /// Le bouton alimente le lexique en tenant compte de la validité de la saisie.
        /// Applique la méthode retraitement du mot.
        /// Le e.Handled à true permet de bloquer l'ajout si elle n'est pas valide.
        /// </summary>
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string mot = txtBoxMot.Text;
            
            if (isSaisieValid(txtBoxMot.Text))
            {
                RetraiterMot(mot);
            }
            e.Handled = true;
        }
        /// <summary>
        /// Ferme la fenêtre de configuration.
        /// </summary>
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Transfert d'un mot du lexique vers la liste des mots à trouver.
        /// </summary>
        private void BtnTransfert_Click(object sender, RoutedEventArgs e)
        {
            Transfert();
        }
        /// <summary>
        /// Retrait de tout les mots de la liste des mots à trouver.
        /// Les mots retournent dans la liste lexique. Ecrasement du fichier contenant la liste des mots à trouver.
        /// </summary>
        private void BtnSupprTout_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in listBoxCible.Items)
            {
                if (item != null && !listBoxLex.Items.Contains(item))
                {
                    listBoxLex.Items.Add(item);
                    lexique.Ajouter(item.ToString());
                    lexique.SaveXML(@"test.xml");
                }
            }
            foreach (var item in listBoxLex.Items)
            {
                listBoxCible.Items.Remove(item);
                atrouver.Remove(item.ToString());
                atrouver.SaveXML(@"mots_choisis.xml");
            }
        }
        /// <summary>
        /// Ajout de tout les mots de la liste lexique dans la liste des mots à trouver.
        /// Sauvergarde la liste des mots à trouver.
        /// </summary>
        private void BtnAddTout_Click(object sender, RoutedEventArgs e)
        {

            foreach (var item in listBoxLex.Items)
            {
                if (item != null && !listBoxCible.Items.Contains(item))
                {
                    listBoxCible.Items.Add(item);
                    atrouver.Ajouter(item.ToString());
                    atrouver.SaveXML(@"mots_choisis.xml");

                }
            }
            foreach (var item in listBoxCible.Items)
            {
                listBoxLex.Items.Remove(item);
                lexique.Remove(item.ToString());
                lexique.SaveXML(@"test.xml");
            }
        }
        /// <summary>
        /// Supprime un mot de la liste des mots à trouver et l'ajoute à la liste lexique.
        /// </summary>
        private void BtnSupr_Click(object sender, RoutedEventArgs e)
        {
            Suppr();
        }
        /// <summary>
        /// Supprime un mot de liste lexique et enregistre le nouveau lexique.
        /// </summary>
        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            RetireMot();
        }
        /// <summary>
        /// Radio bouttons permettant de passer de l'étape "lexique" à l'étape "Difficulté".
        /// Désactive/Active les bouttons concernés.
        /// </summary>
        #region
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
        #endregion
        /// <summary>
        /// Boutons numériques up and down. Boutons en provenance du dépots de contrôle xceed wpf toolkit.
        /// </summary>
        #region
        /// <summary>
        /// numérique up and down déterminant le nombre de manches. valeur par défaut 1 et max 5 déterminé dans MainWindow.xalm.
        /// </summary>   
        private void NupManches_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            options.NombreManches = nupManches.Value.Value;
            options.SaveXML(@"Options.xml");
        }
        /// <summary>
        /// numérique up and down déterminant le nombre d'essais. valeur par défaut 7 et pas de valeur max déterminé dans MainWindow.xalm.
        /// </summary> 
        private void NupEssais_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            options.NbEssais = nupEssais.Value.Value;
            options.SaveXML(@"Options.xml");
        }
        /// <summary>
        /// numérique up and down déterminant la durée totale d'une manche. valeur par défaut 60, valeur max 600, incrément de 10 déterminé dans MainWindow.xalm.
        /// </summary> 
        private void NupDurée_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            options.Temps = nupDurée.Value.Value;
            options.SaveXML(@"Options.xml");
        }
        /// <summary>
        /// numérique up and down déterminant le nombre points perdus par essai. valeur par défaut 0 et max 3 déterminé dans MainWindow.xalm.
        /// </summary> 
        private void NupPtPerdu_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            options.NbPoinPerdus = nupPtPerdu.Value.Value;
            options.SaveXML(@"Options.xml");
        }
        #endregion
        /// <summary>
        /// Bouton de tri alphabétique sur la liste lexique.
        /// </summary>
        private void BtnTri_Click(object sender, RoutedEventArgs e)
        {
            listBoxLex.Items.SortDescriptions.Add(
            new System.ComponentModel.SortDescription("",
            System.ComponentModel.ListSortDirection.Ascending));
        }
        // A implémenter afin de simplifier le code
        //private void RbNiveau()
        //{
        //    RadioButton radioBtn = this.stkPdifficulté.Children.OfType<RadioButton>() 
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
        /// <summary>
        /// EVENEMENT
        /// </summary>
        #region
        /// <summary>
        /// Evite une exception si l'utilisateur tente de supprimer un mot de la liste des mots à trouver alors que la liste est vide.
        /// Désactive le bouton quand la liste est vide.
        /// </summary>
        private void ListBoxCible_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SupprDisable();
        }
        /// <summary>
        /// Permet la suppression du texte de la textBox joueur quand la textbox à le focus.
        /// </summary>
        private void TxtBoxMot_GotFocus(object sender, RoutedEventArgs e)
        {
            txtBoxMot.Clear();
        }
        /// <summary>
        /// Radio bouttons permettant le tri des mots en fonction de leur taille.
        /// </summary>
        #region
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
        #endregion
        #endregion
    }
}
