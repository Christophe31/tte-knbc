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
        protected Dictionary<IdName, Dictionary<IdName, IdName[]>> CampusPeriodClassTree;

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
			this.Dispatcher.BeginInvoke((ThreadStart)FillControls);
		}

        /// <summary>
        /// Fill ComboBoxes.
        /// </summary>
		private void FillControls()
		{
			Api = new CacheWrapper();
			CampusPeriodClassTree = Api.getCampusPeriodClassTree();

			// ComboBoxes initialisation
			ViewType.SelectedIndex = 0;

            CampusName.DataContext = Api.getCampusNames();
            PeriodName.DataContext = Api.getPeriodsNames();

            // DatePickers
            StartDate.SelectedDate = DateTime.Now.AddMonths(-12);
            EndDate.SelectedDate = DateTime.Now.AddMonths(12);

            // Events DataGrid initialisation
            EventsGrid.DataContext = GetAllEvents(DateTime.Now.AddMonths(-12), DateTime.Now.AddMonths(12));
		}

        /// <summary>
        /// Get all events for a period, including (or excluding) mandatory events.
        /// </summary>
        /// <param name="start">Start of the period</param>
        /// <param name="end">End of the period</param>
        /// <returns>An array of events</returns>
        private EventData[] GetAllEvents(DateTime start, DateTime end)
        {
            EventData[] universityEvents = Api.getEventsByUniversity(start, end, new DateTime(2010, 5, 12));
            foreach (EventData ev in universityEvents)
                ev.LinkedTo = "Université";

            return universityEvents;
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
                    IdName campus = (from c in CampusPeriodClassTree.Keys
                                                 where c.Name == CampusName.SelectedValue.ToString()
                                                 select c).FirstOrDefault();
                    IdName period = (from p in CampusPeriodClassTree[campus].Keys
                                                 where p.Name == PeriodName.SelectedValue.ToString()
                                                 select p).FirstOrDefault();
					ClassName.DataContext = CampusPeriodClassTree[campus][period];
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



	}
}
