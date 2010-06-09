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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using Client.BusinessService;

namespace Client
{
    /// <summary>
    /// Interaction logic for Administration.xaml
    /// </summary>
    public partial class Administration : UserControl
    {


        #region Definitions

        protected CacheWrapper Api;
        public IdName[] promoList = null;
        public SubjectData[] subjectsList = null;
        public IdName[] campusList = null;
        public IdName[] periodsList = null;
        public IdName[] classesList = null;
        public IdName[] usersList = null;

        public Administration()
        {
            InitializeComponent();
			Api = new CacheWrapper();
		}
        public IdName[] promoList_Period = null;
        public IdName[] campusList_Class = null;
        public IdName[] periodsList_Class = null;
        public IdName[] rightsList_Users = null;
        public IdName[] classesList_Users = null;

        #endregion
        
        #region Error bar
        public void spawnErrorBar(string message, Boolean isError)
        {
            //On affiche la barre d'erreurs
            ErrorBar.Visibility = Visibility.Visible;
            Uri uri = null;
            SolidColorBrush macouleur = null;

            //Si c'est un message d'erreur...
            if (isError)
            {
                //On change le fond, la bordure et l'image de la barre d'erreur
                macouleur = new SolidColorBrush(System.Windows.Media.Brushes.Orange.Color);
                uri = new Uri(@"..\Icons\error32.png", UriKind.Relative);
                ErrorBorder.BorderBrush = System.Windows.Media.Brushes.Orange;
            }
            else //Si c'est un message d'information
            {
                //On change le fond, la bordure et l'image de la barre d'information
                macouleur = new SolidColorBrush(System.Windows.Media.Brushes.LimeGreen.Color);
                uri = new Uri(@"..\Icons\accept32.png", UriKind.Relative);
                ErrorBorder.BorderBrush = System.Windows.Media.Brushes.DarkGreen;
            }

            //On applique les changements que l'on vient de faire
            ImageSource imgSource = new BitmapImage(uri);
            ErrorIcon.Source = imgSource;
            ErrorMessage.Content = message;
            macouleur.Opacity = 0.5;
            ErrorBar.Background = macouleur;

        }
        #endregion

        #region Chargement du contrôle

        //Au chargement du contrôle d'administration, on charge toutes les combobox
        private void AdministrationControl_Loaded(object sender, RoutedEventArgs e)
        {
			while (Api.Server == null)
			{
				Api.RelinkServer();
				Thread.Sleep(500);
			}

            refreshPromotions();
            //subjectsList = new SubjectData[] { new SubjectData() { Id = 0, Name = "Nouvelle Matière" } }.Concat(Api.ServerBL2.getSubjects()).ToArray();
            //cbSubjects_Subjects.DataContext = subjectsList;
            //cbSubjects_Subjects.SelectedIndex = 0;
            refreshCampus();
            refreshPeriods();
            refreshClasses();
            refreshUsers();
        }

        #endregion

        #region Promotions

        //Rafraichissement des différents contrôles
        private void refreshPromotions()
        {
            Api.Server.getPromotions().ToArray();
            promoList = new IdName[] { new IdName() { Id = 0, Name = "Nouvelle Promotion" } }.Concat(Api.Server.getPromotions()).ToArray();
            cbPromo_Promotions.DataContext = promoList;
            cbPromo_Promotions.SelectedIndex = 0;
        }

        //Lorsque l'utilisateur choisit une promotion
        private void cbPromo_Promotions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPromo_Promotions.SelectedIndex > 0)
            {
                //On met a jour le textbox
                tbPromo_Name.Text = cbPromo_Promotions.SelectedItem.ToString();
            }
        }

        //L'utilisateur a cliqué sur "Ajouter/Modifier"
        private void bPromo_AddMod_Click(object sender, RoutedEventArgs e)
        {
            //S'il s'agit d'une modification...
            if (cbPromo_Promotions.SelectedIndex > 0)
            {
                //On récupère l'id de la promotion sélectionnée
                int idPromo = promoList[cbPromo_Promotions.SelectedIndex].Id;

                //On vérifie que la promotion entrée est valide
                if (tbPromo_Name.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer une promotion valide!", true);
                    return;
                }

                //On tente de modifier la promotion
                string returnValue = Api.Server.setPromotion(idPromo, tbPromo_Name.Text);

                //Si la modification s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Promotion "+cbPromo_Promotions.SelectedItem.ToString()+" modifiée avec succès!", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }
            }
            else if(cbPromo_Promotions.SelectedIndex==0)//S'il s'agit d'un ajout
            {
                //On vérifie que la promotion entrée est valide
                if (tbPromo_Name.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer une promotion valide!", true);
                    return;
                }

                //On tente d'insérer la promotion
                string returnValue = Api.Server.addPromotion(tbPromo_Name.Text);

                //Si l'insertion s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Promotion "+tbClasses_Name.Text+" insérée avec succès!", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }
            }
 
            //On rafraichit les contrôles
            refreshPromotions();
        }

        //L'utilisateur a cliqué sur "Supprimer"
        private void bPromo_Del_Click(object sender, RoutedEventArgs e)
        {
            //S'il n'y a pas de promotion sélectionnée
            if (cbPromo_Promotions.SelectedIndex < 1)
            {
                spawnErrorBar("Choisissez d'abord une promotion à supprimer!", true);
                return;
            }

            //On récupère l'id de la promotion sélectionnée
            int idPromo = promoList[cbPromo_Promotions.SelectedIndex].Id;

            //On tente de supprimer la promotion
            string returnValue = Api.Server.delPromotion(idPromo);

            //Si la suppression s'est correctement déroulée
            if (returnValue.Equals("ok"))
            {
                //On change la StatusBar
                spawnErrorBar("Promotion"+cbPromo_Promotions.SelectedItem.ToString()+" supprimée avec succès!", false);
            }
            else
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                spawnErrorBar(returnValue, true);
            }

            //On rafraichit les contrôles
            refreshPromotions();
        }

        #endregion

        #region Campus

        //Rafraichissement des différents contrôles
        private void refreshCampus()
        {
            campusList = new IdName[] { new IdName() { Id = 0, Name = "Nouveau Campus" } }.Concat(Api.Server.getPlannings(EventData.TypeEnum.Campus)).ToArray();
            cbCampus_Campus.DataContext = campusList;
            cbCampus_Campus.SelectedIndex = 0;
        }

        //Lorsque l'utilisateur choisit un campus
        private void cbCampus_Campus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCampus_Campus.SelectedIndex > 0)
            {
                //On met a jour le textbox
                tbCampus_Name.Text = cbCampus_Campus.SelectedItem.ToString();
            }
        }

        //L'utilisateur a cliqué sur "Ajouter/Modifier"
        private void bCampus_AddMod_Click(object sender, RoutedEventArgs e)
        {
            //S'il s'agit d'une modification...
            if (cbCampus_Campus.SelectedIndex > 0)
            {
                //On récupère l'id du campus sélectionné
                int idCampus = campusList[cbCampus_Campus.SelectedIndex].Id;

                //On vérifie que le campus entré est valide
                if (tbCampus_Name.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer un campus valide!", true);
                    return;
                }

                //On tente de modifier le campus
                string returnValue = Api.Server.setCampus(idCampus, tbCampus_Name.Text);

                //Si la modification s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Campus "+cbCampus_Campus.SelectedItem.ToString()+" modifié avec succès!", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }
            }
            else if (cbCampus_Campus.SelectedIndex == 0)//S'il s'agit d'un ajout
            {
                //On vérifie que le campus entré est valide
                if (tbCampus_Name.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer un campus valide!", true);
                    return;
                }

                //On tente d'insérer le campus
                string returnValue = Api.Server.addCampus(tbCampus_Name.Text);

                //Si l'insertion s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Campus "+tbCampus_Name.Text+" inséré avec succès!", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }
            }
        }

        //L'utilisateur a cliqué sur "Supprimer"
        private void bCampus_Del_Click(object sender, RoutedEventArgs e)
        {

            promoList = new IdName[] { new IdName() { Id = 0, Name = "Nouvelle Promotion" } }.Concat(Api.Server.getPromotions()).ToArray();
            cbPromo_Promotions.DataContext = promoList;
            //S'il n'y a pas de campus sélectionné           
            if (cbCampus_Campus.SelectedIndex < 1)
            {
                spawnErrorBar("Choisissez d'abord un campus à supprimer!", true);
                return;
            }

            //On récupère l'id du campus sélectionné
            int idCampus = campusList[cbCampus_Campus.SelectedIndex].Id;

            //On tente de supprimer le campus
            string returnValue = Api.Server.delCampus(idCampus);

            //Si la suppression s'est correctement déroulée
            if (returnValue.Equals("ok"))
            {
                //On change la StatusBar
                spawnErrorBar("Campus "+cbCampus_Campus.SelectedItem.ToString()+" supprimé avec succès!", false);
            }
            else
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                spawnErrorBar(returnValue, true);
            }

            //On rafraichit les contrôles
            refreshCampus();
        }

        #endregion

        #region Périodes

        //Rafraichissement des différents contrôles
        private void refreshPeriods()
        {
            periodsList = new IdName[] { new IdName() { Id = 0, Name = "Nouvelle Période" } }.Concat(Api.Server.getPlannings(EventData.TypeEnum.Period)).ToArray();
            cbPeriods_Period.DataContext = periodsList;
            cbPeriods_Period.SelectedIndex = 0;

            promoList_Period = Api.Server.getPromotions().ToArray();
            cbPeriods_Promotion.DataContext = promoList_Period;
        }

        //Lorsque l'utilisateur choisit une période
        private void cbPeriods_Period_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbCampus_Campus.SelectedIndex > 0)
            {
                //On met a jour le textbox
                tbPeriods_Name.Text = cbPeriods_Period.SelectedItem.ToString();

                //On récupère l'id de la période sélectionnée
                int idPeriod = periodsList[cbPeriods_Period.SelectedIndex].Id;

                //On récupère les informations de cette période
                PeriodData myPeriod=Api.Server.getPeriod(idPeriod);

                //Et on les affiche
                tbPeriods_StartDate.Text = myPeriod.Start.ToString();
                tbPeriods_EndDate.Text = myPeriod.End.ToString();

                int index = 0;
                //Pour chaque promotion existante
                foreach (IdName myPromotion in promoList_Period)
                {
                    //Si la promotion est celle de notre période
                    if (myPromotion.Id==(myPeriod.Promotion.Id))
                    {
                        //On sort de la boucle
                        break;
                    }
                    index++;
                }

                //On sélectionne la promotion correspondante dans la combobox
                cbPeriods_Promotion.SelectedIndex = index;
            }
        }

        //L'utilisateur a cliqué sur "Ajouter/Modifier"
        private void bPeriods_AddMod_Click(object sender, RoutedEventArgs e)
        {
            //S'il s'agit d'une modification...
            if (cbPeriods_Period.SelectedIndex > 0)
            {
                //On récupère l'id de la période sélectionnée
                int idPeriod = periodsList[cbPeriods_Period.SelectedIndex].Id;

                //On vérifie que la période entrée est valide
                if (tbPeriods_Name.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer un nom de période valide!", true);
                    return;
                }

                DateTime StartHour, EndHour;

                //On récupère les heures de début et de fin
                try
                {
                    StartHour = DateTime.Parse(tbPeriods_StartDate.Text);
                    EndHour = DateTime.Parse(tbPeriods_EndDate.Text);
                }
                catch (FormatException)
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer des dates valides (exemple: \"22/03/2010 15:28\")", true);
                    return;
                }

                //On vérifie que la date de fin est bien supérieure à celle du début
                if (StartHour >= EndHour)
                {
                    //On change la StatusBar
                    spawnErrorBar("La date de fin doit être supérieure à celle de début.", true);
                    return;
                }

                //On récupère la promotion de la période
                if (cbPeriods_Promotion.SelectedIndex < 0)
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez choisir une promotion pour la période!", true);
                    return;
                }

                //Création de notre PeriodData pour la modification
                PeriodData myPeriod = new PeriodData();
                myPeriod.Id = idPeriod;
                myPeriod.Name = tbPeriods_Name.Text;
                myPeriod.Start = StartHour;
                myPeriod.End = EndHour;
                myPeriod.Promotion = promoList_Period[cbPeriods_Promotion.SelectedIndex];

                //On tente de modifier la période
                string returnValue = Api.Server.setPeriod(myPeriod);

                //Si la modification s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Période "+cbPeriods_Period.SelectedItem.ToString()+" modifiée avec succès!", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }
            }
            else if (cbPeriods_Period.SelectedIndex == 0)//S'il s'agit d'un ajout
            {
                //On vérifie que la période entrée est valide
                if (tbPeriods_Name.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer un nom de période valide!", true);
                    return;
                }

                DateTime StartHour, EndHour;

                //On récupère les heures de début et de fin
                try
                {
                    StartHour = DateTime.Parse(tbPeriods_StartDate.Text);
                    EndHour = DateTime.Parse(tbPeriods_EndDate.Text);
                }
                catch (FormatException)
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer des dates valides (exemple: \"22/03/2010 15:28\")", true);
                    return;
                }

                //On vérifie que la date de fin est bien supérieure à celle du début
                if (StartHour >= EndHour)
                {
                    //On change la StatusBar
                    spawnErrorBar("La date de fin doit être supérieure à celle de début.", true);
                    return;
                }

                //On récupère la promotion de la période
                if (cbPeriods_Promotion.SelectedIndex < 0)
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez choisir une promotion pour la période!", true);
                    return;
                }

                //Création de notre PeriodData pour l'insertion
                PeriodData myPeriod = new PeriodData();
                myPeriod.Name = tbPeriods_Name.Text;
                myPeriod.Start = StartHour;
                myPeriod.End = EndHour;
                myPeriod.Promotion = promoList_Period[cbPeriods_Promotion.SelectedIndex];

                //On tente d'insérer la période
                string returnValue = Api.Server.addPeriod(myPeriod);

                //Si l'insertion s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Période insérée "+tbPeriods_Name.Text+" avec succès!", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }
            }

            //On rafraichit les contrôles
            refreshPeriods();
        }

        //L'utilisateur a cliqué sur "Supprimer"
        private void bPeriods_Del_Click(object sender, RoutedEventArgs e)
        {
            //S'il n'y a pas de période sélectionnée
            if (cbPeriods_Period.SelectedIndex < 1)
            {
                spawnErrorBar("Choisissez d'abord une période à supprimer!", true);
                return;
            }

            //On récupère l'id de la période sélectionnée
            int idPeriod = periodsList[cbPeriods_Period.SelectedIndex].Id;

            //On tente de supprimer la période
            string returnValue = Api.Server.delPeriod(idPeriod);

            //Si la suppression s'est correctement déroulée
            if (returnValue.Equals("ok"))
            {
                //On change la StatusBar
                spawnErrorBar("Période "+cbPeriods_Period.SelectedItem.ToString()+" supprimée avec succès!", false);
            }
            else
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                spawnErrorBar(returnValue, true);
            }

            //On rafraichit les contrôles
            refreshPeriods();
        }

        #endregion

        #region Classes

        //Rafraichissement des différents contrôles
        private void refreshClasses()
        {
            classesList = new IdName[] { new IdName() { Id = 0, Name = "Nouvelle Classe" } }.Concat(Api.Server.getPlannings(EventData.TypeEnum.Class)).ToArray();
            cbClasses_Classes.DataContext = classesList;
            cbClasses_Classes.SelectedIndex = 0;

            campusList_Class = Api.Server.getPlannings(EventData.TypeEnum.Campus).ToArray();
            cbClasses_Campus.DataContext = campusList_Class;

            periodsList_Class = Api.Server.getPlannings(EventData.TypeEnum.Period).ToArray();
            cbClasses_Period.DataContext = periodsList_Class;
        }

        //Lorsque l'utilisateur choisit une classe
        private void cbClasses_Classes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbClasses_Classes.SelectedIndex > 0)
            {
                //On met a jour le textbox
                tbClasses_Name.Text = cbClasses_Classes.SelectedItem.ToString();

                //On récupère l'id de la classe
                int idClass = classesList[cbClasses_Classes.SelectedIndex].Id;

                //On récupère ses informations
                ClassData myClass = Api.Server.getClass(idClass);

                int index = 0;

                //Pour chaque campus
                foreach (IdName myCampus in campusList_Class)
                {
                    //Si le campus est celui de notre classe
                    if (myClass.Campus.Id == myCampus.Id)
                    {
                        //On sort de la boucle
                        break;
                    }
                    index++;
                }

                //On sélectionne le campus de notre classe dans la combobox
                cbClasses_Campus.SelectedIndex=index;


                index=0;

                //Pour chaque période
                foreach (IdName myPeriod in periodsList_Class)
                {
                    //Si la période est celle de notre classe
                    if (myClass.Period.Id == myPeriod.Id)
                    {
                        //On sort de la boucle
                        break;
                    }
                    index++;
                }

                //On sélectionne la période de notre classe dans la combobox
                cbClasses_Period.SelectedIndex=index;
            }

        }

        //L'utilisateur a cliqué sur "Ajouter/Modifier"
        private void bClasses_AddMod_Click(object sender, RoutedEventArgs e)
        {
            //S'il s'agit d'une modification...
            if (cbClasses_Classes.SelectedIndex > 0)
            {
                //On récupère l'id de la classe sélectionnée
                int idClass = classesList[cbClasses_Classes.SelectedIndex].Id;

                //On vérifie que la classe entrée est valide
                if (tbClasses_Name.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer un nom de classe valide!", true);
                    return;
                }

                //On vérifie qu'un campus et une période ont été entrés
                if (cbClasses_Campus.SelectedIndex < 0 || cbClasses_Period.SelectedIndex < 0)
                {
                    //On change la StatusBar
                    spawnErrorBar("Choissiez un Campus et une Période pour la classe!", true);
                    return;
                }

                //On prépare notre classe
                ClassData myClass = new ClassData();
                myClass.Campus = campusList_Class[cbClasses_Campus.SelectedIndex];
                myClass.Period = periodsList_Class[cbClasses_Period.SelectedIndex];
                myClass.Name = tbClasses_Name.Text;
                myClass.Id = idClass;

                //On essaie de modifier la classe
                string returnValue = Api.Server.setClass(myClass);

                //Si la modification s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Classe "+cbClasses_Classes.SelectedItem.ToString()+" modifiée avec succès!", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }
            }
            else if (cbClasses_Classes.SelectedIndex == 0)//S'il s'agit d'un ajout
            {
                //On vérifie que la classe entrée est valide
                if (tbClasses_Name.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer un nom de classe valide!", true);
                    return;
                }


                //On vérifie qu'un campus et une période ont été entrés
                if (cbClasses_Campus.SelectedIndex < 0 || cbClasses_Period.SelectedIndex < 0)
                {
                    //On change la StatusBar
                    spawnErrorBar("Choissiez un Campus et une Période pour la classe!", true);
                    return;
                }

                //On prépare notre nouvelle classe
                ClassData myClass = new ClassData();
                myClass.Campus = campusList_Class[cbClasses_Campus.SelectedIndex];
                myClass.Period = periodsList_Class[cbClasses_Period.SelectedIndex];
                myClass.Name = tbClasses_Name.Text;

                //On essaie d'insérer la classe
                string returnValue = Api.Server.addClass(myClass);

                //Si l'insertion s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Classe "+tbClasses_Name.Text+" insérée avec succès!", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }
            }

            //On rafraichit les contrôles
            refreshClasses();
        }

        //L'utilisateur a cliqué sur "Supprimer"
        private void bClasses_Del_Click(object sender, RoutedEventArgs e)
        {
            //S'il n'y a pas de classe sélectionnée
            if (cbClasses_Classes.SelectedIndex < 1)
            {
                spawnErrorBar("Choisissez d'abord une classe à supprimer!", true);
                return;
            }

            //On récupère l'id de la classe sélectionnée
            int idClass = classesList[cbClasses_Classes.SelectedIndex].Id;

            //On tente de supprimer la classe
            string returnValue = Api.Server.delClass(idClass);

            //Si la suppression s'est correctement déroulée
            if (returnValue.Equals("ok"))
            {
                //On change la StatusBar
                spawnErrorBar("Classe " + cbClasses_Classes.SelectedItem.ToString() + " supprimée avec succès!", false);
            }
            else
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                spawnErrorBar(returnValue, true);
            }
        }

        #endregion

        #region Utilisateurs

        //Rafraichissement des différents contrôles
        private void refreshUsers()
        {
            usersList = new IdName[] { new IdName() { Id = 0, Name = "Nouvel Utilisateur" } }.Concat(Api.Server.getUsers()).ToArray();
            cbUsers_Users.DataContext = usersList;
            cbUsers_Users.SelectedIndex = 0;

            //rightsList_Users=Api.Server.get... //watchme

            classesList_Users = new IdName[] { new IdName() { Id = 0, Name = "Aucune" } }.Concat(Api.Server.getPlannings(EventData.TypeEnum.Class)).ToArray();
            cbUsers_Class.DataContext = classesList_Users;
        }

        //Si la checkbox du password est cochée
        private void cbUsers_GenPass_Checked(object sender, RoutedEventArgs e)
        {
            tbUsers_Pass.IsEnabled = false;
        }

        //Si la checkbox du password est décochée
        private void cbUsers_GenPass_Unchecked(object sender, RoutedEventArgs e)
        {
            tbUsers_Pass.IsEnabled = true;
        }

        //L'utilisateur a cliqué sur "Ajouter/Modifier"
        private void bUsers_AddMod_Click(object sender, RoutedEventArgs e)
        {
            //S'il s'agit d'une modification...
            if (cbClasses_Classes.SelectedIndex > 0)
            {
                //On récupère l'id de la classe sélectionnée
                int idClass = classesList[cbClasses_Classes.SelectedIndex].Id;

                //On vérifie que la classe entrée est valide
                if (tbClasses_Name.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer un nom de classe valide!", true);
                    return;
                }

                //On vérifie qu'un campus et une période ont été entrés
                if (cbClasses_Campus.SelectedIndex < 0 || cbClasses_Period.SelectedIndex < 0)
                {
                    //On change la StatusBar
                    spawnErrorBar("Choissiez un Campus et une Période pour la classe!", true);
                    return;
                }

                //On prépare notre classe
                ClassData myClass = new ClassData();
                myClass.Campus = campusList_Class[cbClasses_Campus.SelectedIndex];
                myClass.Period = periodsList_Class[cbClasses_Period.SelectedIndex];
                myClass.Name = tbClasses_Name.Text;
                myClass.Id = idClass;

                //On essaie de modifier la classe
                string returnValue = Api.Server.setClass(myClass);

                //Si la modification s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Classe " + cbClasses_Classes.SelectedItem.ToString() + " modifiée avec succès!", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }
            }
            else if (cbUsers_Users.SelectedIndex == 0)//S'il s'agit d'un ajout
            {
                string password, passwordHashed;

                //On vérifie que l'utilisateur entré est valide
                if (tbUsers_Name.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer un nom d'utilisateur valide!", true);
                    return;
                }

                //On vérifie que le login entré est valide
                if (tbUsers_Login.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer un login valide!", true);
                    return;
                }

                //On récupère le mot de passe
                if (cbUsers_GenPass.IsChecked == true) //Si l'administrateur veut une génération aléatoire du mot de passe...
                {
                    password = RandomPassword.Generate(8, 10);
                }
                else //Si l'administrateur a défini lui même un mot de passe...
                {
                    if (tbUsers_Pass.Text.Trim().Equals(""))
                    {
                        //On change la StatusBar
                        spawnErrorBar("Veuillez choisir un mot de passe valide!", true);
                        return;
                    }
                    password = tbUsers_Pass.Text;
                }

                //On hashe le mot de passe
                passwordHashed = RandomPassword.HashString(password);

                //watchme
            }

            //On rafraichit les contrôles
            refreshUsers();
        }

        #endregion

        

        

        

        

        

        

        

        

        

        

        



        

        


        
    }
}
