using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Client.BusinessLayer;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainAdmin.xaml
    /// </summary>
    public partial class MainAdmin : Window
    {
        protected CacheWrapper Api;

        public MainAdmin()
        {
            InitializeComponent();
            Api = new CacheWrapper();
        }

        /*
         * Pourquoi un onglet Ajouter et modifier/supprimer pour chaque catégorie ?
         * Il suffirait d'avoir une page pour les trois opérations, qui partageraient les mêmes champs.
         * Tu mettrais dans la ComboBox une valeur "Ajouter" par défaut, et tu regarderais si cette valeur
         * est sélectionnée ou non pour choisir l'action à effectuer. ça permet de supprimer une ligne d'onglets,
         * et éventuellement d'intégrer ta fenêtre à la fenêtre principale, dans un onglet "Administration".
         */

        #region Tuple utilisés par l'interface
            IdName[] classList = null;
			IdName[] userList = null;
			IdName[] campusList = null;
			IdName[] periodList = null;
			IdName[] promoList = null;
        #endregion
        #region Onglet "Utilisateurs"
        #region Gestion des onglets

            //Un sous-onglet dans "Utilisateurs" a été choisi
            private void tcOngletsUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == e.OriginalSource)
            {
                if (tcOngletsUsers.SelectedIndex == 0)
                {
                    //On charge la ComboBox
                    //cbUserAdd_Class.DataContext = Api.getClassesNames();
                    classList = Api.Server.getIdClassesNames().ToArray();
                    cbUserAdd_Class.DataContext = classList;
                }
                else if (tcOngletsUsers.SelectedIndex == 1)
                {
                    //cbUserChange_Username.DataContext = Api.getUsersNames();
					userList = Api.Server.getIdUsersNames().ToArray();
                    cbUserChange_Username.DataContext = userList;
                    //cbUserChange_Class.DataContext = Api.getClassesNames();
					classList = Api.Server.getIdClassesNames().ToArray();
                    cbUserChange_Class.DataContext = classList;

                }

                //On prépare la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Prêt";

            }
        }

        #endregion
        #region Onglet "Utilisateurs -> Ajouter"

        //Si la checkbox de l'onglet "Utilisateurs->Ajout" est cochée
        private void cbUserAdd_Password_Checked(object sender, RoutedEventArgs e)
        {
            tbUserAdd_Password.IsEnabled = false;
        }

        //Si la checkbox de l'onglet "Utilisateurs->Ajout" est décochée
        private void cbUserAdd_Password_Unchecked(object sender, RoutedEventArgs e)
        {
            tbUserAdd_Password.IsEnabled = true;
        }

        //On va insérer l'utilisateur
        private void bUserAdd_AddUser_Click(object sender, RoutedEventArgs e)
        {
            string username, password, passwordHashed;

            //On vérifie que l'utilisateur entré n'est pas vide
            if (tbUserAdd_Username.Text.Trim().Equals(""))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Veuillez entrer un nom d'utilisateur valide!";
                return;
            }

            //Et qu'une classe a été choisie
            if (cbUserAdd_Class.SelectedIndex < 0)
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Veuillez choisir une classe!";
                return;
            }

            username = tbUserAdd_Username.Text;

            //On récupère le mot de passe
            if (cbUserAdd_Password.IsChecked == true) //Si l'administrateur veut une génération aléatoire du mot de passe...
            {
                password = RandomPassword.Generate(8, 10);
            }
            else //Si l'administrateur a défini lui même un mot de passe...
            {
                if (tbUserAdd_Password.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                    sbStatusText.Text = "Veuillez choisir un mot de passe valide!";
                    return;
                }
                password = tbUserAdd_Password.Text;
            }

            //On hashe le mot de passe
            passwordHashed = RandomPassword.HashString(password);
            //MessageBox.Show(username + " " + passwordHashed + " " + cbUserAdd_Class.SelectedItem.ToString());


            //On tente d'insérer l'utilisateur
            string returnValue = Api.Server.addUser(username, passwordHashed, cbUserAdd_Class.SelectedItem.ToString());

            //Si l'insertion s'est correctement déroulée
            if (returnValue.Equals("ok"))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Utilisateur inséré avec succès! (Username: \"" + username + "\", Password: \"" + password + "\")";
            }
            else
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = returnValue;
            }
        }

        #endregion
        #region Onglet "Utilisateurs -> Modifier / Supprimer"

        //Si la checkbox de l'onglet "Utilisateurs->Modifier" est cochée
        private void cbUserChange_Password_Checked(object sender, RoutedEventArgs e)
        {
            tbUserChange_Password.IsEnabled = false;
        }

        //Si la checkbox de l'onglet "Utilisateurs->Modifier" est décochée
        private void cbUserChange_Password_Unchecked(object sender, RoutedEventArgs e)
        {
            tbUserChange_Password.IsEnabled = true;
        }

        //Si l'administrateur souhaite modifier un utilisateur
        private void bUserChange_ChangeUser_Click(object sender, RoutedEventArgs e)
        {
            string username, password, passwordHashed;

            //On vérifie que l'utilisateur entré n'est pas vide
            if (tbUserChange_Username.Text.Trim().Equals(""))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Veuillez entrer un nom d'utilisateur valide!";
                return;
            }

            //Et qu'une classe a été choisie
            if (cbUserChange_Class.SelectedIndex < 0)
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Veuillez choisir une classe!";
                return;
            }

            username = tbUserChange_Username.Text;

            //On récupère le mot de passe
            if (cbUserChange_Password.IsChecked == true) //Si l'administrateur veut une génération aléatoire du mot de passe...
            {
                password = RandomPassword.Generate(8, 10);
            }
            else //Si l'administrateur a défini lui même un mot de passe...
            {
                if (tbUserChange_Password.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                    sbStatusText.Text = "Veuillez choisir un mot de passe valide!";
                    return;
                }
                password = tbUserChange_Password.Text;
            }

            //On hashe le mot de passe
            passwordHashed = RandomPassword.HashString(password);
            MessageBox.Show(password);
            MessageBox.Show(passwordHashed);

            //On tente de modifier l'utilisateur
            //string returnValue = Api.ChangeUser(username, passwordHashed, cbUserAdd_Class.SelectedItem.ToString());

            /*
            //Si la modification s'est correctement déroulée
            if (returnValue.Equals("ok"))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Utilisateur modifié avec succès! (Username: \"" + username + "\", Password: \"" + password + "\")";
            }
            else
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = returnValue;
            }
            */
        }

        //Si l'administrateur souhaite supprimer un utilisateur
        private void bUserChange_DelUser_Click(object sender, RoutedEventArgs e)
        {
            if (cbUserChange_Username.SelectedIndex >= 0)
            {
                //On récupère l'id de l'utilisateur actuel à partir du Tuple
                int idUser = userList[cbUserChange_Username.SelectedIndex].Id;

                //On tente de supprimer l'utilisateur
                string returnValue = Api.Server.delUser(idUser);

                //Si la suppression s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                    sbStatusText.Text = "Utilisateur \""+cbUserChange_Username.SelectedItem.ToString()+"\"supprimé avec succès!";
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                    sbStatusText.Text = returnValue;
                }

                //On rafraichit la combobox des utilisateurs
                userList = Api.Server.getIdUsersNames().ToArray();
                cbUserChange_Username.DataContext = userList;

                //On met à zéro la combox des classes et le textbox de l'utilisateur
                tbUserChange_Username.Text = "";
                cbUserChange_Class.SelectedIndex = -1;

            }

        }

        #endregion
        #endregion
        #region Onglet "Campus"
        #region Gestion des onglets

        //Un sous-onglet dans "Campus" a été choisi
        private void tcOngletsCampus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == e.OriginalSource)
            {

                if (tcOngletsCampus.SelectedIndex == 1)
                {
                    //cbCampusChange_Campus.DataContext = Api.getCampusNames();
					campusList = Api.Server.getIdCampusNames().ToArray();
                    cbCampusChange_Campus.DataContext = campusList;
                }

                //On prépare la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Prêt";

            }
        }

        #endregion
        #region Onglet "Campus -> Ajouter"

        //Si l'utilisateur a cliqué sur la liste des utilisateurs
        private void cbUserChange_Username_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Si un utilisateur a été choisi
            if (cbUserChange_Username.SelectedIndex >= 0)
            {
                //On met à jour ses propriétés (sauf le mot de passe)
                tbUserChange_Username.Text = cbUserChange_Username.SelectedItem.ToString();
                //GetCampusForUser(user)

            }
        }

        //On va ajouter un campus
        private void bCampusAdd_Click(object sender, RoutedEventArgs e)
        {
            //On vérifie si un nom de campus a bien été rentré
            if (tbCampusAdd.Text.Trim().Equals(""))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Veuillez choisir un nom de campus correct!";
                return;
            }

            //On essaie d'insérer le Campus
            string returnValue = Api.Server.addCampus(tbCampusAdd.Text);

            //Si l'insertion a fonctionnée
            if (returnValue.Equals("ok"))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Campus créé avec succès!";
            }
            else //Sinon...
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = returnValue;
            }
        }

        #endregion
        #region Onglet "Campus -> Modifier / Supprimer"

            //Si l'utilisateur choisit un Campus
            private void cbCampusChange_Campus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCampusChange_Campus.SelectedIndex >= 0)
            {
                //On change la TextBox
                tbCampusChange_Campus.Text = cbCampusChange_Campus.SelectedItem.ToString();
            }
        }

            //Si l'administrateur souhaite supprimer un Campus
            private void bCampusChange_Del_Click(object sender, RoutedEventArgs e)
        {
            if (cbCampusChange_Campus.SelectedIndex >= 0)
            {
                //On récupère l'id du campus actuel à partir du Tuple
                int idCampus = campusList[cbCampusChange_Campus.SelectedIndex].Id;

                //On tente de supprimer le campus
                string returnValue = Api.Server.delCampus(idCampus);


                //Si la suppression s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                    sbStatusText.Text = "Campus \"" + cbCampusChange_Campus.SelectedItem.ToString()+ "\"supprimé avec succès!";
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                    sbStatusText.Text = returnValue;
                }

                //On met à jour la combobox des campus
				campusList = Api.Server.getIdCampusNames().ToArray();
                cbCampusChange_Campus.DataContext = campusList;

                //On remet à zéro le textbox du campus
                tbCampusChange_Campus.Text = "";
            }
        }

        #endregion
        #endregion
        #region Onglet "Classes"
        #region Gestion des onglets
        //Un sous-onglet dans "Classes" a été choisi
        private void tcOngletsClasses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == e.OriginalSource)
            {
                if (tcOngletsClasses.SelectedIndex == 0)
                {
                    //On charge les ComboBox
                    //cbClassAdd_Campus.DataContext = Api.getCampusNames();
					campusList = Api.Server.getIdCampusNames().ToArray();
                    cbClassAdd_Campus.DataContext = campusList;
                    //cbClassAdd_Period.DataContext = Api.getPeriodsNames();
					periodList = Api.Server.getIdPeriodsNames().ToArray();
                    cbClassAdd_Period.DataContext = periodList;

                }
                else if (tcOngletsClasses.SelectedIndex == 1)
                {
                    //On charge les ComboBox
                    //cbClassChange_Class.DataContext = Api.getClassesNames();
					classList = Api.Server.getIdClassesNames().ToArray();
                    cbClassChange_Class.DataContext = classList;
                    //cbClassChange_Campus.DataContext = Api.getCampusNames();
					campusList = Api.Server.getIdCampusNames().ToArray();
                    cbClassChange_Campus.DataContext = campusList;
                    //cbClassChange_Period.DataContext = Api.getPeriodsNames();
					periodList = Api.Server.getIdPeriodsNames().ToArray();
                    cbClassChange_Period.DataContext = periodList;
                }

                //On prépare la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Prêt";

            }
        }

        #endregion
        #region Onglet "Classes -> Ajouter"

        private void bClassAdd_Add_Click(object sender, RoutedEventArgs e)
        {
            //On vérifie qu'un nom de classe a bien été rentré
            if (tbClassAdd_ClassName.Text.Trim().Equals(""))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Veuillez choisir un nom de classe correct!";
                return;
            }

            //On vérifié qu'un campus et une classe ont bien été rentrés
            if (cbClassAdd_Campus.SelectedIndex < 0 || cbClassAdd_Period.SelectedIndex < 0)
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Choissiez un Campus et une Période pour la classe!";
                return;
            }

            //On essaie d'insérer la classe
			string returnValue = Api.Server.addClass(tbClassAdd_ClassName.Text, cbClassAdd_Campus.SelectedItem.ToString(), cbClassAdd_Period.SelectedItem.ToString());

            //Si l'insertion a fonctionnée
            if (returnValue.Equals("ok"))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Classe créée avec succès!";
            }
            else //Sinon...
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = returnValue;
            }
        }

        #endregion
        #region Onglet "Classes -> Modifier"

        //Si l'utilisateur choisit une classe
        private void cbClassChange_Class_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbClassChange_Class.SelectedIndex >= 0)
            {
                //On rafraichit le textbox
                tbClassChange_Class.Text = cbClassChange_Class.SelectedItem.ToString();
            }
        }

        //Si l'administrateur souhaite supprimer une classe
        private void bClassChange_Del_Click(object sender, RoutedEventArgs e)
        {
            if (cbClassChange_Class.SelectedIndex >= 0)
            {
                //On récupère l'id de la classe actuelle à partir du Tuple
                int idClass = classList[cbClassChange_Class.SelectedIndex].Id;

                //On tente de supprimer le campus
                string returnValue = Api.Server.delClass(idClass);


                //Si la suppression s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                    sbStatusText.Text = "Classe \"" + cbClassChange_Class.SelectedItem.ToString() + "\"supprimée avec succès!";
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                    sbStatusText.Text = returnValue;
                }

                //On met à jour la combobox des classes
				classList = Api.Server.getIdClassesNames().ToArray();
                cbClassChange_Class.DataContext = classList;

                //On met à zéro la combobox des campus
                cbClassChange_Campus.SelectedIndex = -1;

                //On met à zéro la combobox des périodes
                cbClassChange_Period.SelectedIndex = -1;

                //On remet à zéro le textbox du campus
                tbCampusChange_Campus.Text = "";
            }
        }

        #endregion


        #endregion
        #region Onglet "Promotions"
        #region Gestion des onglets

        private void tcOngletsPromotions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == e.OriginalSource)
            {
                if (tcOngletsPromotions.SelectedIndex == 1)
                {
                    //On charge la ComboBox
                    //cbPromoChange_Promo.DataContext = Api.getPromotionsNames();
					promoList = Api.Server.getIdPromotionsNames().ToArray();
                    cbPromoChange_Promo.DataContext = promoList;
                }

                //On prépare la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Prêt";

            }

        }

        #endregion
        #region Onglet "Promotions -> Ajouter"

        private void bPromoAdd_Add_Click(object sender, RoutedEventArgs e)
        {

            //On vérifie que la promotion entrée est valide
            if (tbPromoAdd_Promo.Text.Trim().Equals(""))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Veuillez entrer une promotion valide!";
                return;
            }

            //On tente d'insérer la promotion
			string returnValue = Api.Server.addPromotion(tbPromoAdd_Promo.Text);

            //Si l'insertion s'est correctement déroulée
            if (returnValue.Equals("ok"))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Promotion insérée avec succès!";
            }
            else
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = returnValue;
            }
        }

        #endregion
        #region Onglet "Promotions -> Modifier / Supprimer"

        //Si l'utilisateur choisit une Promotion
        private void cbPromoChange_Promo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPromoChange_Promo.SelectedIndex >= 0)
            {
                //On rafraichit la textbox
                tbPromoChange_Promo.Text = cbPromoChange_Promo.SelectedItem.ToString();
            }
        }

        //Si l'administrateur souhaite supprimer une promotion
        private void bPromoChange_Del_Click(object sender, RoutedEventArgs e)
        {
            if (cbPromoChange_Promo.SelectedIndex >= 0)
            {
                //On récupère l'id de la promotion actuelle à partir du Tuple
                int idPromo = promoList[cbPromoChange_Promo.SelectedIndex].Id;

                //On tente de supprimer la promotion
                string returnValue = Api.Server.delPromotion(idPromo);

                //Si la suppression s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                    sbStatusText.Text = "Promotion \"" + cbPromoChange_Promo.SelectedItem.ToString() + "\"supprimée avec succès!";
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                    sbStatusText.Text = returnValue;
                }

                //On rafraichit la combobox des promotion
				promoList = Api.Server.getIdPromotionsNames().ToArray();
                cbPromoChange_Promo.DataContext = promoList;

                //On met à zéro le textbox de la promotion
                tbPromoChange_Promo.Text = "";
            }
        }

        #endregion
        #endregion
        #region Onglet "Matières"
        #region Gestion des onglets
        private void tcOngletsSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == e.OriginalSource)
            {
                if (tcOngletsSubjects.SelectedIndex == 1)
                {
                    //On charge la ComboBox
                    cbSubjectChange_Subj.DataContext = Api.getSubjectsNames();
                }

                //On prépare la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Prêt";

            }
        }
        #endregion
        #region Onglet "Matières -> Ajouter"

        //Lorsque l'utilisateur souhaite ajouter une matière
        private void bSubjectAdd_Add_Click(object sender, RoutedEventArgs e)
        {
            //On vérifie que la matière entrée est valide
            if (tbSubjectAdd_Subj.Text.Trim().Equals(""))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Veuillez entrer une matière valide!";
                return;
            }

            int hours;

            //On récupère le nombre d'heures
            try
            {
                hours = int.Parse(tbSubjectAdd_Hours.Text);
            }
            catch (FormatException)
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Veuillez entrer un nombre d'heures valides!";
                return;
            }

            //On tente d'insérer la matière
			string returnValue = Api.Server.addSubject(tbSubjectAdd_Subj.Text, hours, Api.getModalities().First());

            //Si l'insertion s'est correctement déroulée
            if (returnValue.Equals("ok"))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Matière insérée avec succès!";
            }
            else
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = returnValue;
            }
        }

        #endregion
        #region Onglet "Matières -> Modifier / Supprimer"
        #endregion
        #endregion
        #region Onglet "Périodes"
        #region Gestion des onglets
        private void tcOngletsPeriods_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == e.OriginalSource)
            {
                if (tcOngletsSubjects.SelectedIndex == 0)
                {
                    //On charge la ComboBox
                    cbPeriodAdd_Promo.DataContext = Api.getIdPromotionsNames();
                }
                else if (tcOngletsSubjects.SelectedIndex == 1)
                {
                    //On charge la ComboBox
                    cbPeriodChange_Period.DataContext = Api.getIdPromotionsNames();
                }

                //On prépare la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Prêt";
            }
        }
        #endregion
        #region Onglet "Périodes -> Ajouter"
        //Si l'utilisateur souhaite ajouter une période
        private void bPeriodAdd_Add_Click(object sender, RoutedEventArgs e)
        {
            //On vérifie que la matière entrée est valide
            if (tbPeriodAdd_Period.Text.Trim().Equals(""))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Veuillez entrer une période valide!";
                return;
            }


            DateTime StartHour, EndHour;

            //On récupère les heures de début et de fin
            try
            {
                StartHour = DateTime.Parse(tbPeriodAdd_DStart.Text);
                EndHour = DateTime.Parse(tbPeriodAdd_DEnd.Text);
            }
            catch (FormatException)
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Veuillez entrer des dates valides (exemple: \"22/03/2010 15:28\")";
                return;
            }

            //On vérifie que la date de fin est bien supérieure à celle du début
            if (StartHour >= EndHour)
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "La date de fin doit être supérieure à celle de début.";
                return;
            }


            //On récupère la promotion de la période
            if (cbPeriodAdd_Promo.SelectedIndex < 0)
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = "Veuillez choisir une promotion pour la période!";
                return;
            }

            //On tente d'insérer la période
			string returnValue = Api.Server.addPeriod(tbPeriodAdd_Period.Text, cbPeriodAdd_Promo.SelectedItem.ToString(), StartHour, EndHour);

            //Si l'insertion s'est correctement déroulée
            if (returnValue.Equals("ok"))
            {
                //On change la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Période insérée avec succès!";
            }
            else
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                sbStatusText.Text = returnValue;
            }
        }
        #endregion
        #region Onglet "Périodes -> Modifier / Supprimer"
        //Si l'utilisateur clique sur la combobox des périodes
        private void cbPeriodChange_Period_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Si une période a été sélectionnée
            if (cbPeriodChange_Period.SelectedIndex >= 0)
            {
                //On met à jour la textbox
                tbPeriodChange_Period.Text = cbPeriodChange_Period.SelectedItem.ToString();
            }
        }
        #endregion

        
        #endregion
    }
}