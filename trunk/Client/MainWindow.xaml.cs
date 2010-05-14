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
using Client.BusinessLayer;

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        protected CacheWrapper Api;
        protected Dictionary<Tuple<int, string>, Dictionary<Tuple<int, string>, Tuple<int, string>[]>> CampusPeriodClassTree;

		public MainWindow()
		{
			InitializeComponent();
		}

        /// <summary>
        /// Called when the window is ready.
        /// Launches a thread which will fill ComboBoxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			new Thread((ThreadStart)FillCombobox).Start();
        }

		private void FillCombobox()
		{
			this.Dispatcher.BeginInvoke((ThreadStart)FillComboboxes);
		}

        /// <summary>
        /// Fill ComboBoxes.
        /// </summary>
		private void FillComboboxes()
		{
			Api = new CacheWrapper();
			CampusPeriodClassTree = Api.getCampusPeriodClassTree();

			// ComboBoxes initialisation
			ViewType.SelectedIndex = 0;
            CampusName.DataContext = (from tuple in CampusPeriodClassTree.Keys
                                      select tuple.Item2).ToList();
			PeriodName.DataContext = Api.getPeriodsNames();
			//ClassName.DataContext = Api.getClassesNames();
            RefreshClassName();

            // Events DataGrid initialisation
            EventsGrid.DataContext = Api.getEventsByUniversity(DateTime.Now.AddDays(-3), DateTime.Now.AddDays(3), new DateTime(2010, 5, 12));
		}

        /// <summary>
        /// Called when an item is selected in ComboBoxes.
        /// Display and hide required ComboBoxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    RefreshClassName();

                    CampusName.Visibility = System.Windows.Visibility.Visible;
                    PeriodName.Visibility = System.Windows.Visibility.Visible;
                    ClassName.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

        private void CampusName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshClassName();
        }
        private void PeriodName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshClassName();
        }

        /// <summary>
        /// The ClassName ComboBox is filled in function of the selected campus and period.
        /// </summary>
        private void RefreshClassName()
        {
            if (ViewType.SelectedIndex == 3)
            {
				try
				{
					//ClassName.DataContext = CampusPeriodClassTree[(string)CampusName.SelectedValue][(string)PeriodName.SelectedValue];
				}
				catch (KeyNotFoundException)
				{
					ClassName.DataContext = null;
				}
				catch (ArgumentNullException)
				{
					ClassName.DataContext = null;
				}
            }
        }

		private void button1_Click_1(object sender, RoutedEventArgs e)
		{
			MainAdmin fenetreAdmin = new MainAdmin();
			fenetreAdmin.Show();
		}

		private void button1_Click(object sender, RoutedEventArgs e)
		{
			ApiTestWindow api = new ApiTestWindow();
			api.Show();
		}

	}
}
