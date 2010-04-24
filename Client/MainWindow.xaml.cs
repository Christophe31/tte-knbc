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

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			BusinessLayerClient bc = new BusinessLayerClient();

			this.button1.Content = bc.getEventsData_Campus("Toulouse", new DateTime(1999, 12, 5), new DateTime(2999, 12, 5), new DateTime(1999, 12, 5)).First().Matiere;
		}
	}
}
