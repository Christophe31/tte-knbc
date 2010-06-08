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
using Client.BusinessService;

namespace Client
{
    /// <summary>
    /// Interaction logic for Administration.xaml
    /// </summary>
    public partial class Administration : UserControl
    {
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

        #region Error bar
        public void spawnErrorBar(string message, Boolean isError)
        {
            ErrorBar.Visibility = Visibility.Visible;
            Uri uri = null;

            SolidColorBrush macouleur = null;
            


            if (isError)
            {
                //ErrorBar.Background = System.Windows.Media.Brushes.Orange;
                macouleur = new SolidColorBrush(System.Windows.Media.Brushes.Orange.Color);
                uri = new Uri(@"..\Icons\error32.png", UriKind.Relative);
            }
            else
            {
                //ErrorBar.Background = System.Windows.Media.Brushes.LimeGreen;

                macouleur = new SolidColorBrush(System.Windows.Media.Brushes.LimeGreen.Color);
                uri = new Uri(@"..\Icons\accept32.png", UriKind.Relative);
            }

            ImageSource imgSource = new BitmapImage(uri);
            ErrorIcon.Source = imgSource;
            ErrorMessage.Content = message;
            macouleur.Opacity = 0.5;
            ErrorBar.Background = macouleur;

        }
        #endregion


        #region Promotions
        //L'utilisateur a cliqué sur "Ajout/Modification"
            private void bPromo_AddMod_Click(object sender, RoutedEventArgs e)
            {
                if (ErrorBar.Visibility == Visibility.Hidden)
                {
                    spawnErrorBar("Et bah tout roule jacky!", false);

                }
                else
                {
                    ErrorBar.Visibility = Visibility.Hidden;
                }

                //S'il s'agit d'un ajout
                if (cbPromo_Promotions.SelectedIndex == 0)
                {
                    //On vérifie que la promotion n'est pas vide
                    if (tbPromo_Name.Text.Trim().Equals(""))
                    {
                        //On change la StatusBar
                        //sbStatusText.Foreground = new SolidColorBrush(Colors.Red); ;
                        //sbStatusText.Text = "Veuillez entrer un nom d'utilisateur valide!";
                        //return;
                    }
                }
                else //S'il s'agit d'une modification
                {

                }
            }
        #endregion

        private void cbClasses_Classes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbPromo_Promotions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AdministrationControl_Loaded(object sender, RoutedEventArgs e)
        {
            promoList = new IdName[] { new IdName() { Id = 0, Name = "Nouvelle Promotion" } }.Concat(Api.Server.getPromotions()).ToArray();
            cbPromo_Promotions.DataContext = promoList;
            cbPromo_Promotions.SelectedIndex = 0;

            //subjectsList = new SubjectData[] { new SubjectData() { Id = 0, Name = "Nouvelle Matière" } }.Concat(Api.ServerBL2.getSubjects()).ToArray();
            //cbSubjects_Subjects.DataContext = subjectsList;
            //cbSubjects_Subjects.SelectedIndex = 0;

            campusList = new IdName[] { new IdName() { Id = 0, Name = "Nouveau Campus" } }.Concat(Api.Server.getPlannings(EventData.TypeEnum.Campus)).ToArray();
            cbCampus_Campus.DataContext = campusList;
            cbCampus_Campus.SelectedIndex = 0;

            periodsList = new IdName[] { new IdName() { Id = 0, Name = "Nouvelle Période" } }.Concat(Api.Server.getPlannings(EventData.TypeEnum.Period)).ToArray();
            cbPeriods_Period.DataContext = periodsList;
            cbPeriods_Period.SelectedIndex = 0;
            
            classesList = new IdName[] { new IdName() { Id = 0, Name = "Nouvelle Classe" } }.Concat(Api.Server.getPlannings(EventData.TypeEnum.Class)).ToArray();
            cbClasses_Classes.DataContext = classesList;
            cbClasses_Classes.SelectedIndex = 0;

            usersList = new IdName[] { new IdName() { Id = 0, Name = "Nouvel Utilisateur" } }.Concat(Api.Server.getUsers()).ToArray();
            cbUsers_Users.DataContext = usersList;
            cbUsers_Users.SelectedIndex = 0;
        }

        private void bPromo_Del_Click(object sender, RoutedEventArgs e)
        {
            if (ErrorBar.Visibility == Visibility.Hidden)
            {
                spawnErrorBar("Erreur lors de l'ajout d'un utilisateur blah blah", true);
            }
            else
            {
                ErrorBar.Visibility = Visibility.Hidden;
            }
        }
    }
}
