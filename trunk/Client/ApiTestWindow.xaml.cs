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
			this.SimpleTestButton.Content = Api.getEventsByCampus("Toulouse", new DateTime(1999, 12, 5), new DateTime(2999, 12, 5), new DateTime(1999, 12, 5)).First().Matiere;
			foreach (string s in Api.getCampusNames())
			{
				this.CampusBox.Items.Add(s);
			}
			foreach (string s in Api.getClassesNames())
			{
				this.ClasseBox.Items.Add(s);
			}
			foreach (string s in Api.getPromotionsNames())
			{
				this.PromotionBox.Items.Add(s);
			}
		}

		private void AddUserButton_Click(object sender, RoutedEventArgs e)
		{
			this.ErrorLabel.Content = Api.addUser(this.TextArg1.Text, this.TextArg2.Text, this.ClasseBox.SelectedItem.ToString());
		}

		private void AddCampusButton_Click(object sender, RoutedEventArgs e)
		{
			this.ErrorLabel.Content = Api.addCampus(TextArg1.Text);
		}

		private void AddClasseButton_Click(object sender, RoutedEventArgs e)
		{
			this.ErrorLabel.Content = Api.addClasse(TextArg1.Text, CampusBox.SelectedItem.ToString(), "Semestre 1");
		}

		private void AddPromoButton_Click(object sender, RoutedEventArgs e)
		{
			this.ErrorLabel.Content = Api.addPromotion(TextArg1.Text);
		}
	}
}
