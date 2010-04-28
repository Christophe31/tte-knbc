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

            Api = new BusinessLayerClient();

            CampusList.DataContext = Api.getCampusNames();
            PeriodList.DataContext = Api.getPeriodsNames();
            ClassList.DataContext = Api.getClassesNames();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			ApiTestWindow api = new ApiTestWindow();
			api.Show();
		}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
	}
}
