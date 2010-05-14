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
	/// Pour regenerer le BusinessLayerWraper.cs:
	/// .\Client>"c:\Program Files\Microsoft SDKs\Windows\v6.0A\Bin\SvcUtil.exe" http://localhost:1620/BusinessLayer.svc?wsdl
	/// (note: le fichier ne sera pas écrasé automatiquement car j'ai changé la convention de nomage en ajoutant "wrapper")
	/// </summary>
	public partial class ApiTestWindow : Window
	{
		protected int compteur;
		protected delegate string[] foo();
		protected CacheWrapper Api;

		public ApiTestWindow()
		{
			compteur = 0;
			InitializeComponent();
			DateSlider.Maximum = 200;
			DateSlider.Minimum = 0;
			Api = new CacheWrapper() ;
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
            Dictionary<string, Dictionary<string, string[]>> truc = null;//Api.getCampusPeriodClassTree();
			Dictionary<string[], ComboBox> Complete0 = new Dictionary<string[], ComboBox>();
			Complete0.Add(truc.Keys.ToArray(),CampusBox);
			Complete0.Add(truc.Values.SelectMany(p=>p.Keys).Distinct().ToArray(),PeriodeBox);
			Complete0.Add(truc.Values.SelectMany(p => p.Values.SelectMany(g=>g)).ToArray(), ClasseBox);

			Dictionary<foo,ComboBox> Complete = new Dictionary<foo,ComboBox>();
			Complete.Add(Api.getCampusNames,CampusBox);
			Complete.Add(Api.getClassesNames, ClasseBox);
			Complete.Add(Api.getPeriodsNames, PeriodeBox);
			Complete.Add(Api.getPromotionsNames, PromotionBox);
			//this.SimpleTestButton.Content = Api.getEventsByCampus("Toulouse", new DateTime(1999, 12, 5), new DateTime(2999, 12, 5), new DateTime(1999, 12, 5)).First().Matiere;
			foreach (var kv in Complete0)
			{
				kv.Value.Items.Clear();
				foreach (string s in kv.Key)
				{
					kv.Value.Items.Add(s);
				}
			}
			ErrorLabel.Content = (++compteur).ToString()+ "Completion completed!";

		}

		private void AddUserButton_Click(object sender, RoutedEventArgs e)
		{
			this.ErrorLabel.Content = (++compteur).ToString() +
				Api.Server.addUser(this.TextArg1.Text, this.TextArg2.Text, this.ClasseBox.SelectedItem.ToString());
		}

		private void AddCampusButton_Click(object sender, RoutedEventArgs e)
		{
			this.ErrorLabel.Content = (++compteur).ToString() +
				Api.Server.addCampus(TextArg1.Text);
		}

		private void AddClasseButton_Click(object sender, RoutedEventArgs e)
		{
			this.ErrorLabel.Content = (++compteur).ToString() +
				Api.Server.addClass(TextArg1.Text, CampusBox.SelectedItem.ToString(), "Semestre 1");
		}

		private void AddPromoButton_Click(object sender, RoutedEventArgs e)
		{
			this.ErrorLabel.Content = (++compteur).ToString() +
				Api.Server.addPromotion(TextArg1.Text);
		}

		private void AddPeriodeButton_Click(object sender, RoutedEventArgs e)
		{
			this.ErrorLabel.Content = (++compteur).ToString() +
				Api.Server.addPeriod(TextArg1.Text, PromotionBox.SelectedItem.ToString(), DateTime.Now.AddMonths(-(int)DateSlider.Value), DateTime.Now.AddMonths((int)DateSlider.Value));

		}

		private void AddEventButton_Click(object sender, RoutedEventArgs e)
		{
			this.ErrorLabel.Content = (++compteur).ToString() +
				Api.Server.addEventToUniversity(TextArg1.Text, DateTime.Now.AddHours(-(int)DateSlider.Value), DateTime.Now.AddHours((int)DateSlider.Value), false, "admin", "salle1");

		}

		private void GrantRightButton_Click(object sender, RoutedEventArgs e)
		{
			this.ErrorLabel.Content = (++compteur).ToString() +
				Api.Server.grantNewRight("",  CampusBox.SelectedIndex.ToString());

		}

	}
}
