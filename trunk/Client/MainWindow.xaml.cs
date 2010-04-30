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
using BusinessLayer;

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        protected CacheBusinessWrapper Api;

		public MainWindow()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			ApiTestWindow api = new ApiTestWindow();
			api.Show();
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Api = CacheBusinessWrapper.getCacheWrapper();

            // ComboBoxes initialisation
            ViewType.SelectedIndex = 4;
            CampusName.DataContext = Api.getCampusNames();
            PeriodName.DataContext = Api.getPeriodsNames();
            ClassName.DataContext = Api.getClassesNames();
        }

        private void ViewType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ViewType.SelectedIndex;
            // University or user
            if (selected == 0 || selected == 4)
            {
                CampusName.Visibility = System.Windows.Visibility.Collapsed;
                PeriodName.Visibility = System.Windows.Visibility.Collapsed;
                ClassName.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                // Campus
                if (selected == 1)
                {
                    CampusName.Visibility = System.Windows.Visibility.Visible;
                    PeriodName.Visibility = System.Windows.Visibility.Collapsed;
                    ClassName.Visibility = System.Windows.Visibility.Collapsed;
                }
                // Period
                else if (selected == 2)
                {
                    CampusName.Visibility = System.Windows.Visibility.Collapsed;
                    PeriodName.Visibility = System.Windows.Visibility.Visible;
                    ClassName.Visibility = System.Windows.Visibility.Collapsed;
                }
                // Class
                else if (selected == 3)
                {
                    CampusName.Visibility = System.Windows.Visibility.Visible;
                    PeriodName.Visibility = System.Windows.Visibility.Visible;
                    ClassName.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }
	}
}
