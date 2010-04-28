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

namespace Client
{
    /// <summary>
    /// Interaction logic for MainAdmin.xaml
    /// </summary>
    public partial class MainAdmin : Window
    {
        protected BusinessLayerClient Api;

        public MainAdmin()
        {
            InitializeComponent();
            Api = new BusinessLayerClient();
        }

        /*
        //Si on change d'onglets...
        private void tcOnglets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == e.OriginalSource)
            {
                //Si c'est l'onglet "Utilisateurs"
                //MessageBox.Show(tcOnglets.SelectedItem.ToString());
                if (tcOnglets.SelectedItem.ToString() == "Users")
                //if (tcOnglets.SelectedIndex == 0)
                {
                    //On charge la ComboBox
                    cbUserAdd_Class.DataContext = Api.getClassesNames();

                    //On prépare la StatusBar
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                    sbStatusText.Text = "Prêt";
                }
            }
        }
        */

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
                MessageBox.Show(username + " " + passwordHashed + " " + cbUserAdd_Class.SelectedItem.ToString());


                //On tente d'insérer l'utilisateur
                string returnValue = Api.addUser(username, passwordHashed, cbUserAdd_Class.SelectedItem.ToString());

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

        #endregion
        #region Onglet "Campus -> Ajouter"

            //Si l'utilisateur a cliqué sur la liste des utilisateurs
            private void cbUserChange_Username_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                //Si un utilisateur a été choisi
                if (cbUserChange_Username.SelectedIndex > 0)
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
                string returnValue = Api.addCampus(tbCampusAdd.Text);

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
                if (cbClassAdd_Campus.SelectedIndex <= 0 || cbClassAdd_Period.SelectedIndex <= 0)
                {
                    //On change la StatusBar
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                    sbStatusText.Text = "Choissiez un Campus et une Période pour la classe!";
                    return;
                }

                //On essaie d'insérer la classe
                string returnValue = Api.addClass(tbClassAdd_ClassName.Text, cbClassAdd_Campus.SelectedItem.ToString(), cbClassAdd_Period.SelectedItem.ToString());

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

            private void tcOngletsUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if (sender == e.OriginalSource)
                {
                    if (tcOngletsUsers.SelectedIndex == 0)
                    {
                        //On charge la ComboBox
                        cbUserAdd_Class.DataContext = Api.getClassesNames();
                    }
                    else if (tcOngletsUsers.SelectedIndex == 1)
                    {
                        MessageBox.Show("Hum");
                        cbUserChange_Username.DataContext = Api.getUsersNames();
                    }

                    //On prépare la StatusBar
                    sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                    sbStatusText.Text = "Prêt";

                }
            }


    }
}