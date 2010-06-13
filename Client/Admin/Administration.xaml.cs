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
		public Administration()
		{
			InitializeComponent();

            //Instanciation du pointeur vers le serveur distant
			Api = new CacheWrapper();
		}


        #region Definitions - Ensemble des différents objets utilisés par l'interface d'administration
        protected CacheWrapper Api;
        public IdName[] promoList = null;
        public SubjectData[] subjectsList = null;
        public IdName[] campusList = null;
        public IdName[] periodsList = null;
        public IdName[] classesList = null;
        public IdName[] usersList = null;
        public IdName[] promoList_Period = null;
        public IdName[] campusList_Class = null;
        public IdName[] periodsList_Class = null;
        public IdName[] rightsList_Users = null;
        public IdName[] classesList_Users = null;
        public  List<ModalityData> modalityInformations=new List<ModalityData>();
        #region Utilisateurs
        public UserData userInformations=null;
        public List<RoleData> rolesInformations = new List<RoleData>(); //Rôles de l'utilisateur actuel
        public IdName[] universityList = null;
        public IdName[] campusListRights = null;
        #endregion
        #endregion

        #region Status Bar - Barre d'état affichant les messages de réussite ou d'erreur lors de la manipulation de l'interface d'administration

        //Gestion de la barre
        public void spawnErrorBar(string message, Boolean isError)
        {
            //On affiche la barre d'état
            ErrorBar.Visibility = Visibility.Visible;
            ErrorBorder.Visibility = Visibility.Visible;
            Uri uri = null;
            SolidColorBrush macouleur = null;

            //Si c'est un message d'erreur...
            if (isError)
            {
                //On change le fond, la bordure et l'image de la barre
                macouleur = new SolidColorBrush(System.Windows.Media.Brushes.Orange.Color);
                uri = new Uri(@"..\Icons\error32.png", UriKind.Relative);
                ErrorBorder.BorderBrush = System.Windows.Media.Brushes.Orange;
            }
            else //Si c'est un message d'information / de réussite
            {
                //On change le fond, la bordure et l'image de la barre
                macouleur = new SolidColorBrush(System.Windows.Media.Brushes.LimeGreen.Color);
                uri = new Uri(@"..\Icons\accept32.png", UriKind.Relative);
                ErrorBorder.BorderBrush = System.Windows.Media.Brushes.DarkGreen;
            }

            //On applique les changements que l'on vient de faire
            ImageSource imgSource = new BitmapImage(uri);
            ErrorIcon.Source = imgSource;
            ErrorMessage.Text = message;
            macouleur.Opacity = 0.5;
            ErrorBar.Background = macouleur;
            ErrorBorder.BorderThickness = new Thickness(1);
        }

        //Permet de fermer la barre de Statut
        private void CloseErrorBar_Click(object sender, RoutedEventArgs e)
        {
            ErrorBar.Visibility = System.Windows.Visibility.Collapsed;
            ErrorBorder.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion

        #region Chargement du contrôle - Routines lancées au démarrage de l'UserControl ou servant à sa gestion

        //Au chargement du contrôle d'administration, on charge toutes les combobox
        private void AdministrationControl_Loaded(object sender, RoutedEventArgs e)
        {
			((ThreadStart)waitServeReady).BeginInvoke(null, null);
        }

        //Permet de gérer la connection avec le serveur distant
		void waitServeReady()
		{
			while (!Api.ServerAvailable)
			{
				while (!Api.ServerAvailable)
				{
					Thread.Sleep(500);
				}
				Thread.Sleep(1000);
			}
			
			this.Dispatcher.BeginInvoke((ThreadStart)refreshAllControls);
		}
 
        //Rafraichissement complet de la fenêtre d'Administration
		void refreshAllControls()
        {
                refreshPromotions();
                refreshSubjects();
                refreshCampus();
                refreshPeriods();
                refreshPromo_Periods();
                refreshPeriods_Classes();
                refreshClasses();
                refreshCampus_Classes();
                refreshUsers();
                refreshClasses_Users();
		}

        #region Gestion des PreviewKeyUp

        private void tbPromo_Name_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                bPromo_AddMod_Click(null, null);
                bPromo_AddMod.Focus();
            }
        }

        private void tbSubjects_Name_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                bSubjects_AddMod_Click(null, null);
                bSubjects_AddMod.Focus();
            }
        }

        private void tbCampus_Name_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                bCampus_AddMod_Click(null, null);
                bCampus_AddMod.Focus();
            }
        }

        #endregion

        #endregion

        #region Promotions

        //Rafraichissement des différents contrôles
        private void refreshPromotions()
        {
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

                    //On rafraichit la combobox des Promotions dans le groupbox Périodes
                    refreshPromo_Periods();
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

                    //On rafraichit la combobox des Promotions dans le groupbox Périodes
                    refreshPromo_Periods();
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
                spawnErrorBar("Promotion "+cbPromo_Promotions.SelectedItem.ToString()+" supprimée avec succès!", false);

                //On rafraichit la combobox des Promotions dans le groupbox Périodes
                promoList_Period = Api.Server.getPromotions().ToArray();
                cbPeriods_Promotion.DataContext = promoList_Period;
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

                    //On rafraichit la combobox des campus dans le groupbox Classes
                    refreshCampus_Classes();
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

                    //On rafraichit la combobox des campus dans le groupbox Classes
                    refreshCampus_Classes();
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }
            }

            //On rafraichit les contrôles
            refreshCampus();
        }

        //L'utilisateur a cliqué sur "Supprimer"
        private void bCampus_Del_Click(object sender, RoutedEventArgs e)
        {
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

                //On rafraichit la combobox des campus dans le groupbox Classes
                refreshCampus_Classes();
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

        public void refreshPromo_Periods()
        {
            promoList_Period = Api.Server.getPromotions().ToArray();
            cbPeriods_Promotion.DataContext = promoList_Period;
        }

        //Rafraichissement des différents contrôles
        private void refreshPeriods()
        {
            periodsList = new IdName[] { new IdName() { Id = 0, Name = "Nouvelle Période" } }.Concat(Api.Server.getPlannings(EventData.TypeEnum.Period)).ToArray();
            cbPeriods_Period.DataContext = periodsList;
            cbPeriods_Period.SelectedIndex = 0;

            
        }

        //Lorsque l'utilisateur choisit une période
        private void cbPeriods_Period_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbPeriods_Period.SelectedIndex > 0)
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

                    //On rafraichit la combobox des périodes dans le groupbox classes
                    refreshPeriods_Classes();
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

                    //On rafraichit la combobox des périodes dans le groupbox classes
                    refreshPeriods_Classes();
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

                //On rafraichit la combobox des périodes dans le groupbox classes
                    refreshPeriods_Classes();
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

        private void refreshCampus_Classes()
        {
            campusList_Class = Api.Server.getPlannings(EventData.TypeEnum.Campus).ToArray();
            cbClasses_Campus.DataContext = campusList_Class;
        }

        private void refreshPeriods_Classes()
        {
            periodsList_Class = Api.Server.getPlannings(EventData.TypeEnum.Period).ToArray();
            cbClasses_Period.DataContext = periodsList_Class;
        }

        //Rafraichissement des différents contrôles
        private void refreshClasses()
        {
            classesList = new IdName[] { new IdName() { Id = 0, Name = "Nouvelle Classe" } }.Concat(Api.Server.getPlannings(EventData.TypeEnum.Class)).ToArray();
            cbClasses_Classes.DataContext = classesList;
            cbClasses_Classes.SelectedIndex = 0;
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

                    //On rafraichit la combobox des classes dans le groupbox Utilisateurs
                    refreshClasses_Users();
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
                    spawnErrorBar("Choisissez un Campus et une Période pour la classe!", true);
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

                    //On rafraichit la combobox des classes dans le groupbox Utilisateurs
                    refreshClasses_Users();
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

                //On rafraichit la combobox des classes dans le groupbox Utilisateurs
                refreshClasses_Users();
            }
            else
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                spawnErrorBar(returnValue, true);
            }
        }

        #endregion

        #region Matières

        //Rafraichissement des différents contrôles
        private void refreshSubjects()
        {
            subjectsList = new SubjectData[] { new SubjectData() { Id = 0, Name = "Nouvelle Matière" } }.Concat(Api.Server.getSubjects()).ToArray();
            cbSubjects_Subjects.DataContext = subjectsList;
            cbSubjects_Subjects.SelectedIndex = 0;

        }
        
        //Rafraichissement de la DataGrid des modalités
        private void refreshModalityGrid()
        {
            ModalityGrid.DataContext=modalityInformations;
        }

        //L'utilisateur a cliqué sur "Ajouter/Modifier"
        private void bSubjects_AddMod_Click(object sender, RoutedEventArgs e)
        {
            //S'il s'agit d'un ajout
            if (cbSubjects_Subjects.SelectedIndex == 0)
            {
                //On vérifie que le nom de la matière est valide
                if (tbSubjects_Name.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer une matière valide!", true);
                    return;
                }

                //On prépare notre matière
                SubjectData mySubject = new SubjectData();
                mySubject.Name = tbSubjects_Name.Text;
                if (modalityInformations.Count!=0)
                {

                    mySubject.Modalities = modalityInformations.ToArray();
                }
                else
                {
                    mySubject.Modalities = null;
                }

                //On essaie d'insérer la matière
                string returnValue = Api.Server.addSubject(mySubject);

                //Si l'insertion s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Matière " + tbSubjects_Name.Text + " insérée avec succès!", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }

                //On rafraichit les contrôles
                refreshSubjects();

            }
            else if (cbSubjects_Subjects.SelectedIndex > 0) //S'il s'agit d'une modification
            {
                //On récupère l'id de la matière actuelle
                int idSubject = subjectsList[cbSubjects_Subjects.SelectedIndex].Id;

                //On vérifie que le nom de la matière est valide
                if (tbSubjects_Name.Text.Trim().Equals(""))
                {
                    //On change la StatusBar
                    spawnErrorBar("Veuillez entrer une matière valide!", true);
                    return;
                }

                //On prépare notre matière
                SubjectData mySubject = new SubjectData();
                mySubject.Name = tbSubjects_Name.Text;
                mySubject.Id = idSubject;
                if (modalityInformations.Count != 0)
                {

                    mySubject.Modalities = modalityInformations.ToArray();
                }
                else
                {
                    mySubject.Modalities = null;
                }

                //On essaie de modifier la matière
                string returnValue = Api.Server.setSubject(mySubject);

                //Si la modification s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Matière " + cbSubjects_Subjects.SelectedItem.ToString() + " modifiée avec succès!", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }

                //On rafraichit les contrôles
                refreshSubjects();
            }
        }

        //L'utilisateur a cliqué sur "Supprimer"
        private void bSubjects_Del_Click(object sender, RoutedEventArgs e)
        {
            //Si l'utilisateur n'a pas choisi de matière à supprimer
            if (cbSubjects_Subjects.SelectedIndex < 1)
            {
                //On change la StatusBar
                spawnErrorBar("Selectionnez d'abord une matière à supprimer!", true);
                return;
            }

            //On récupère l'id de la matière sélectionnée
            int idSubjet=subjectsList[cbSubjects_Subjects.SelectedIndex].Id;

            //On tente de supprimer la matière
            string returnValue = Api.Server.delSubject(idSubjet);

            //Si la suppression s'est correctement déroulée
            if (returnValue.Equals("ok"))
            {
                //On change la StatusBar
                spawnErrorBar("Matière " + cbSubjects_Subjects.SelectedItem.ToString() + " supprimée avec succès!", false);
            }
            else
            {
                //On change la StatusBar avec le message d'erreur renvoyé
                spawnErrorBar(returnValue, true);
            }

            //On rafraichit les contrôles
            refreshSubjects();
        }

        //Lorsque l'utilisateur choisit une matière
        private void cbSubjects_Subjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Si c'est une nouvelle matière
            if (cbSubjects_Subjects.SelectedIndex == 0)
            {
                //On initialise une nouvelle liste de modalités
                modalityInformations = new List<ModalityData>();

                //On rafraichit la Modalitygrid
                refreshModalityGrid();

            }
            else if (cbSubjects_Subjects.SelectedIndex > 0) //Si c'est une matière existante
            {
                //On rafraichit le nom de la matière
                tbSubjects_Name.Text = cbSubjects_Subjects.SelectedItem.ToString();

                //On récupère les modalités de la matière actuelle
                if (subjectsList[cbSubjects_Subjects.SelectedIndex].Modalities != null)
                {

                    modalityInformations = subjectsList[cbSubjects_Subjects.SelectedIndex].Modalities.ToList();
                }
                else
                {
                    modalityInformations = new List<ModalityData>();
                }

                //On rafraichit la Modalitygrid
                refreshModalityGrid();
            }
        }

        #endregion

        #region Utilisateurs

        private void refreshClasses_Users()
        {
            classesList_Users = new IdName[] { new IdName() { Id = 0, Name = "Aucune" } }.Concat(Api.Server.getPlannings(EventData.TypeEnum.Class)).ToArray();
            cbUsers_Class.DataContext = classesList_Users;
            cbUsers_Class.SelectedIndex = 0;
        }

        //Rafraichissement des contrôles
        public void refreshUsers()
        {
            usersList = new IdName[] { new IdName() { Id = 0, Name = "Nouvel Utilisateur" } }.Concat(Api.Server.getUsers()).ToArray();
            cbUsers_Users.DataContext = usersList;
            cbUsers_Users.SelectedIndex = 0;
        }

        //Si l'utilisateur à choisi... un utilisateur
        private void cbUsers_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Si l'utilisateur est déjà dans la base
            if (cbUsers_Users.SelectedIndex > 0)
            {
                //On récupère ses informations
                userInformations = Api.Server.getUser(usersList[cbUsers_Users.SelectedIndex].Id);

                //On peuple les contrôles
                tbUsers_Login.Text = userInformations.Login;
                tbUsers_Name.Text = userInformations.Name;

                //Si l'utilisateur fait parti d'une classe
                if (userInformations.Class != null)
                {

                    int index = 0;
                    //On cherche l'index de la classe
                    foreach (IdName classe in classesList_Users)
                    {
                        if (classe.Id == userInformations.Class.Id)
                        {
                            break;
                        }
                        index++;
                    }

                    //On sélectionne la classe de l'utilisateur
                    cbUsers_Class.SelectedIndex = index;
                }

                //S'il a des rôles
                if (userInformations.Roles != null)
                {
                    //On les récupère
                    rolesInformations = userInformations.Roles.ToList();

                    //On récupère la liste des universités
                    universityList = Api.Server.getPlannings(EventData.TypeEnum.University);

                    //Et celle des campus
                    campusListRights = Api.getPlannings(EventData.TypeEnum.Campus);

                    //Pour chaque rôle
                    foreach (RoleData myRole in rolesInformations)
                    {
                        
                        if (myRole.Role == RoleData.RoleType.Administrator) //Si le rôle est "Administrateur"
                        {
                            //On remplit les champs manquants
                            myRole.RoleName = "Administrateur";

                            //Pour chaque université
                            foreach (IdName university in universityList)
                            {
                                if (university.Id == myRole.TargetId) //Si c'est l'université que l'on recherche
                                {
                                    //On récupère son nom
                                    myRole.TargetName = university.Name;
                                    break;
                                }
                            }
                        }
                        else if (myRole.Role == RoleData.RoleType.CampusManager) //Si c'est le rôle Campus Manager
                        {
                            //On remplit les champs manquants
                            myRole.RoleName = "Campus Manager";

                            //Pour chaque campus
                            foreach (IdName campus in campusListRights)
                            {
                                if (campus.Id == myRole.TargetId) //Si c'est le campus que l'on recherche
                                {
                                    //On récupère son nom
                                    myRole.TargetName = campus.Name;
                                    break;
                                }
                            }
                        }
                        else //Si c'est le rôle "Speaker"
                        {
                            //On remplit les champs manquants
                            myRole.RoleName = "Intervenant";
                        }
                    }
                }
                else
                {
                    //On initialise une nouvelle liste de rôles
                    rolesInformations = new List<RoleData>();
                }

                //On rafraichit la DataGrid des rôles
                refreshRoleGrid();
            }
            else if (cbUsers_Users.SelectedIndex == 0) //Si c'est la création d'un nouvel utilisateur
            {
                //On initialise une nouvelle liste de rôles
                rolesInformations = new List<RoleData>();

                //On rafraichit la DataGrid des rôles
                refreshRoleGrid();
            }
        }

        //Lorsque l'utilisateur choisit un rôle
        private void cbUsers_Rights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Si c'est le rôle "Administrateur"
            if (cbUsers_Rights.SelectedIndex == 1)
            {

                //On charge la deuxieme combobox avec les universités
                universityList = Api.Server.getPlannings(EventData.TypeEnum.University);
                cbUsers_RightsType.DataContext = universityList;

                //On affiche la deuxième combobox des rôles
                cbUsers_RightsType.Visibility = Visibility.Visible;
            }
            else if (cbUsers_Rights.SelectedIndex == 2) //Si c'est le rôle "Campus Manager"
            {
                //On charge la deuxieme combobox avec les campus
                campusListRights = Api.getPlannings(EventData.TypeEnum.Campus);
                cbUsers_RightsType.DataContext = campusListRights;

                //On affiche la deuxième combobox des rôles
                cbUsers_RightsType.Visibility = Visibility.Visible;

            }
            else if (cbUsers_Rights.SelectedIndex == 3) //Si c'est le rôle "Speaker"
            {
                //On ajoute le rôle à notre liste de rôles
                rolesInformations.Add(new RoleData() { Id = 0, Role = RoleData.RoleType.Speaker, RoleName = "Intervenant", TargetName = "", TargetId = null });

                //On revient sur "Ajouter un rôle"
                cbUsers_Rights.SelectedIndex = 0;

                //On cache la deuxième combobox des rôles
                cbUsers_RightsType.Visibility = Visibility.Hidden;

                //On rafraichit la DataGrid des rôles
                refreshRoleGrid();
            }
            else if (cbUsers_Rights.SelectedIndex == 0) //Si l'utilisateur reclique sur "Ajouter un rôle"
            {
                if (cbUsers_RightsType != null) //Pour éviter le chargement lors de l'init du UserControl
                {
                    //On cache la deuxième combobox des rôles
                    cbUsers_RightsType.Visibility = Visibility.Hidden;
                }
            }
        }

        //Lorsque l'utilisateur choisit une cible pour le rôle
        private void cbUsers_RightsType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Si le rôle était "Administrateur"
            if (cbUsers_Rights.SelectedIndex == 1)
            {
                //On ajoute le nouveau rôle
                rolesInformations.Add(new RoleData() { Id = 0, Role = RoleData.RoleType.Administrator, RoleName = "Administrateur", TargetId = universityList[cbUsers_RightsType.SelectedIndex].Id, TargetName = cbUsers_RightsType.SelectedItem.ToString() });

                //On se remet sur "Ajouter un rôle"
                cbUsers_Rights.SelectedIndex = 0;
                cbUsers_RightsType.SelectedIndex = -1;

                //On rafraichit la DataGrid des rôles
                refreshRoleGrid();
            }
            else if (cbUsers_Rights.SelectedIndex == 2) //Si le rôle est "Campus Manager"
            {
                //On ajoute le nouveau rôle
                rolesInformations.Add(new RoleData() { Id = 0, Role = RoleData.RoleType.CampusManager, RoleName = "Campus Manager", TargetId = campusListRights[cbUsers_RightsType.SelectedIndex].Id, TargetName = cbUsers_RightsType.SelectedItem.ToString() });

                //On se remet sur "Ajouter un rôle"
                cbUsers_Rights.SelectedIndex = 0;
                cbUsers_RightsType.SelectedIndex = -1;

                //On rafraichit la DataGrid des rôles
                refreshRoleGrid();
            }
        }

        //Si l'utilisateur souhaite supprimer un rôle
        private void bUsers_DelRight_Click(object sender, RoutedEventArgs e)
        {
            //On récupère le rôle sélectionné
            RoleData myRole = (RoleData)RolesGrid.SelectedItem;

            //S'il n'y avait pas de rôle sélectionné, on affiche un message d'erreur
            if (myRole == null)
            {
                spawnErrorBar("Selectionnez d'abord un rôle dans la liste pour le supprimer!", true);
                return;
            }

            //Sinon, on le supprime et on met à jour l'affichage de la DataGrid
            rolesInformations.Remove(myRole);
            refreshRoleGrid();
        }

        //Rafraichissement de la DataGrid des rôles
        public void refreshRoleGrid()
        {
            RolesGrid.DataContext = rolesInformations.ToArray(); ;
        }

        //Si l'utilisateur choisit "Ajouter/Modifier"
        private void bUsers_AddMod_Click(object sender, RoutedEventArgs e)
        {
            //S'il s'agit d'une modification...
            if (cbUsers_Users.SelectedIndex > 0)
            {
                //On récupère l'id de l'utilisateur
                int idUser = usersList[cbUsers_Users.SelectedIndex].Id;

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

                //On prépare notre utilisateur
                UserData myUser = new UserData();
                myUser.Name = tbUsers_Name.Text;
                myUser.Login = tbUsers_Login.Text;
                myUser.Password = passwordHashed;
                myUser.Id = idUser;

                if (rolesInformations.Count() == 0)
                {
                    myUser.Roles = null;
                }
                else
                {
                    myUser.Roles = rolesInformations.ToArray();
                }

                //Si aucune classe n'a été choisie
                if (cbUsers_Class.SelectedIndex < 1)
                {
                    myUser.Class = null;
                }
                else //Sinon, on la récupère
                {
                    myUser.Class = classesList_Users[cbUsers_Class.SelectedIndex].Id;
                }

                //On tente de modifier l'utilisateur
                string returnValue = Api.Server.setUser(myUser);

                //Si la modification s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Utilisateur modifié avec succès! (Nom: \"" + cbUsers_Users.SelectedItem.ToString() + "\", Login: \"" + tbUsers_Login.Text + "\", Password: \"" + password + "\")", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }

                //On rafraichit les contrôles
                refreshUsers();

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

                //On prépare notre utilisateur
                UserData myUser = new UserData();
                myUser.Name = tbUsers_Name.Text;
                myUser.Login = tbUsers_Login.Text;
                myUser.Password = passwordHashed;
                myUser.Id = 0;

                //S'il n'y a pas de rôles à envoyer
                if (rolesInformations.Count() == 0)
                {
                    myUser.Roles = null; //On met à null
                }
                else
                {
                    myUser.Roles = rolesInformations.ToArray(); //Sinon, on les envoie
                }

                

                //Si aucune classe n'a été choisie
                if (cbUsers_Class.SelectedIndex < 1)
                {
                    myUser.Class = null;
                }
                else //Sinon, on la récupère
                {
                    myUser.Class = classesList_Users[cbUsers_Class.SelectedIndex];
                }

                //On tente d'ajouter l'utilisateur
                string returnValue = Api.Server.addUser(myUser);

                //Si l'ajout s'est correctement déroulé
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Utilisateur inséré avec succès! (Nom: \"" + tbUsers_Name.Text + "\", Login: \"" + tbUsers_Login.Text + "\", Password: \"" + password + "\")", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }

                //On rafraichit les contrôles
                refreshUsers();
            }
        }

        //Si l'utilisateur choisit "Supprimer"
        private void bUsers_Del_Click(object sender, RoutedEventArgs e)
        {
            if (cbUsers_Users.SelectedIndex > 0)
            {
                //On récupère l'id de l'utilisateur
                int idUser = usersList[cbUsers_Users.SelectedIndex].Id;

                //On tente de le supprimer
                string returnValue = Api.Server.delUser(idUser);

                //Si la suppression s'est correctement déroulée
                if (returnValue.Equals("ok"))
                {
                    //On change la StatusBar
                    spawnErrorBar("Utilisateur supprimé avec succès! (Nom: \"" + cbUsers_Users.SelectedItem.ToString() + "\")", false);
                }
                else
                {
                    //On change la StatusBar avec le message d'erreur renvoyé
                    spawnErrorBar(returnValue, true);
                }

                //On rafraichit les contrôles
                refreshUsers();
            }
            else
            {
                spawnErrorBar("Sélectionnez d'abord un utilisateur!", true);
            }
        }

        //Si l'utilisateur coche la checkbox du password
        private void cbUsers_GenPass_Checked(object sender, RoutedEventArgs e)
        {
            tbUsers_Pass.IsEnabled = false;
        }

        //Si l'utilisateur décoche la checkbox du password
        private void cbUsers_GenPass_Unchecked(object sender, RoutedEventArgs e)
        {
            tbUsers_Pass.IsEnabled = true;
        }

        #endregion

    }
}
