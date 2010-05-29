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
        /// <summary>
        /// Api allowing to manipulate data stored in the database
        /// </summary>
        protected CacheWrapper Api;
        /// <summary>
        /// Tree storing names of campuses, associated periods and classes.
        /// </summary>
        protected Dictionary<IdName, Dictionary<IdName, IdName[]>> CampusPeriodClassTree;
        /// <summary>
        /// List storing all events to be displayed.
        /// </summary>
        protected List<EventData> AllEvents;

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

            CampusName.DataContext = Api.Server.getIdCampusNames(); ;
            CampusName.SelectedIndex = 0;
            PeriodName.DataContext = Api.Server.getIdPeriodsNames();
            PeriodName.SelectedIndex = 0;

            // DatePickers
            StartDate.SelectedDate = DateTime.Now.AddMonths(-12);
            EndDate.SelectedDate = DateTime.Now.AddMonths(12);

            // Events DataGrid initialisation
            RefreshAllEvents();
            EventsGrid.DataContext = AllEvents;
		}

        /// <summary>
        /// Refresh the events list, for the period specified in the GUI, including (or excluding) mandatory events.
        /// </summary>
        private void RefreshAllEvents()
        {
            DateTime start = StartDate.SelectedDate.GetValueOrDefault();
            DateTime end = EndDate.SelectedDate.GetValueOrDefault();
            AllEvents = new List<EventData>();

            // University
            foreach (EventData ev in Api.getEventsByUniversity(start, end))
            {
                ev.LinkedTo = "Université";
                AllEvents.Add(ev);
            }

            // Campus
            if (ViewType.SelectedIndex == 1 || ViewType.SelectedIndex == 3 || ViewType.SelectedIndex == 4)
            {
                foreach (EventData ev in Api.getEventsByCampus(((IdName)CampusName.SelectedValue).Id, start, end))
                {
                    ev.LinkedTo = "Campus";
                    AllEvents.Add(ev);
                }
            }

            // Period
            if (ViewType.SelectedIndex == 2 || ViewType.SelectedIndex == 3 || ViewType.SelectedIndex == 4)
            {

                foreach (EventData ev in Api.getEventsByPeriod(((IdName)PeriodName.SelectedValue).Id, start, end))
                {
                    ev.LinkedTo = "Période";
                    AllEvents.Add(ev);
                }
            }

            // Class;
            if ((ViewType.SelectedIndex == 3 || ViewType.SelectedIndex == 4) && ClassName.SelectedValue != null)
            {
                foreach (EventData ev in Api.getEventsByClass(((IdName)ClassName.SelectedValue).Id, start, end))
                {
                    ev.LinkedTo = "Classe";
                    AllEvents.Add(ev);
                }
            }

            // User
            if (ViewType.SelectedIndex == 4)
            {
                foreach (EventData ev in Api.getPrivateEvents(start, end))
                {
                    ev.LinkedTo = "Privé";
                    AllEvents.Add(ev);
                }
            }

            // Refresh views
            if (ViewsControl.SelectedIndex == 2)
                EventsGrid.DataContext = AllEvents;
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
            // Campus
            else if (selected == 1)
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

            RefreshAllEvents();
        }

        private void CampusName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshClassName();
            RefreshAllEvents();
        }
        private void PeriodName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshClassName();
            RefreshAllEvents();
        }
        private void ClassName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshAllEvents();
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
                    ClassName.SelectedIndex = 0;
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

        /// <summary>
        /// Controls that the new value of the TextBox is a correct hour, and corrects it if required.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox box = (TextBox) sender;
            int value = 0;
            if (Int32.TryParse(box.Text, out value))
            {
                if (value < 0)
                    box.Text = "0";
                else if (value >= 24)
                    box.Text = "23";
            }
            else
                box.Text = "0";
        }

	}
}
