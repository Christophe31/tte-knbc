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
	/// La fenetre de tests à Christophe, merci de ne pas en faire n'importe quoi.
	/// </summary>
	public partial class ApiTestWindow : Window
	{
		protected BusinessLayerClient Api;

		public ApiTestWindow()
		{
			InitializeComponent();
			Api = new BusinessLayerClient();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{

		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			this.button1.Content = Api.getEventsByCampus("Toulouse", new DateTime(1999, 12, 5), new DateTime(2999, 12, 5), new DateTime(1999, 12, 5)).First().Matiere;

		}
	}
}
