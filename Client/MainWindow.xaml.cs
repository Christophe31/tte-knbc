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
        protected BusinessLayerClient Api;

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
            Api = new BusinessLayerClient();

            ViewType.SelectedIndex = 4;


        }

        private void ViewType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ViewType.SelectedIndex;
            // University or user
            if (selected == 0 || selected == 4)
            {
                ViewName.Visibility = System.Windows.Visibility.Collapsed;
                ViewName.DataContext = null;
            }
            else
            {
                // Campus
                if (selected == 1)
                    ViewName.DataContext = Api.getCampusNames();
                // Period
                else if (selected == 2)
                    ViewName.DataContext = Api.getPeriodsNames();
                // Class
                else if (selected == 3)
                    ViewName.DataContext = Api.getClassesNames();

                ViewName.Visibility = System.Windows.Visibility.Visible;
                ViewName.SelectedIndex = 0;
            }
        }
	}
}
