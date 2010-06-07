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
using Client.Calendar;

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        #region attributes
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
        //private double HourControlsWidth = 0;
        //private int HourControlsCount = 0;
        #endregion

        #region Window init
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
            ViewType.DataContext = EventType.EventTypeNames;
			ViewType.SelectedIndex = 0;

            CampusName.DataContext = Api.Server.getIdCampusNames(); ;
            CampusName.SelectedIndex = 0;
            PeriodName.DataContext = Api.Server.getIdPeriodsNames();
            PeriodName.SelectedIndex = 0;

            // DatePickers
            StartDate.SelectedDate = DateTime.Now.AddMonths(-12);
            EndDate.SelectedDate = DateTime.Now.AddMonths(12);

            // Draw Grids
            DayGrid.Children.Add(GetHoursGrid());

            // Events DataGrid initialisation
            RefreshAllEvents();
		}

        /// <summary>
        /// Get a grid containing hours strings and lines (to place in the first column of a grid).
        /// It will be placed in the first cell of the parent grid.
        /// </summary>
        /// <returns>The hours grid</returns>
        public Grid GetHoursGrid()
        {
            Grid grid = new Grid();
            grid.ShowGridLines = true;
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            Grid.SetColumn(grid, 0);
            Grid.SetRow(grid, 0);

            // Draw hours strings
            for (int i = 0; i < 24; i++)
            {
                // Create the row
                grid.RowDefinitions.Add(new RowDefinition());

                // Create the TextBlock
                TextBlock tb = new TextBlock();
                tb.Text = i.ToString() + ":00";
                tb.Margin = new Thickness(0,0,5,0);

                Grid.SetColumn(tb, 0);
                Grid.SetRow(tb, i);
                grid.Children.Add(tb);
            }

            //((ScrollViewer)grid.Parent).ScrollToVerticalOffset(grid.Height * 7 / 24);
            return grid;
        }
        #endregion

        #region Refreshes
        /// <summary>
        /// Refresh the events list, for the period specified in the GUI, including (or excluding) mandatory events.
        /// </summary>
        private void RefreshAllEvents()
        {
            DateTime start = StartDate.SelectedDate.GetValueOrDefault();
            DateTime end = EndDate.SelectedDate.GetValueOrDefault();

            EventData.TypeEnum viewType = EventData.TypeEnum.University;
            if (ViewType.SelectedValue != null)
                viewType = ((KeyValuePair<EventData.TypeEnum, string>) ViewType.SelectedValue).Key;

            // University
            AllEvents = Api.getEventsByUniversity(start, end)
                .Where(p => p.Mandatory
                    || viewType == EventData.TypeEnum.University
                    || ShowOptionalUniversityEvents.IsChecked.GetValueOrDefault()).ToList();

            // Campus
			if ((viewType == EventData.TypeEnum.Campus
				|| viewType == EventData.TypeEnum.Class
				|| viewType == EventData.TypeEnum.User)
                && CampusName.SelectedValue != null)
            {
                AllEvents.AddRange(Api.getEventsByCampus(CampusName.SelectedValue as IdName, start, end)
                    .Where(p => p.Mandatory
                        || viewType == EventData.TypeEnum.Campus
                        || ShowOptionalCampusEvents.IsChecked.GetValueOrDefault()));
            }

            // Period
            if ((viewType == EventData.TypeEnum.Period
                || viewType == EventData.TypeEnum.Class
                || viewType == EventData.TypeEnum.User)
                && PeriodName.SelectedValue != null)
            {
                AllEvents.AddRange(Api.getEventsByPeriod(PeriodName.SelectedValue as IdName, start, end)
                    .Where(p => p.Mandatory
                        || viewType == EventData.TypeEnum.Period
                        || ShowOptionalPeriodEvents.IsChecked.GetValueOrDefault()));
            }

            // Class
            if ((viewType == EventData.TypeEnum.Class || viewType == EventData.TypeEnum.User)
                && ClassName.SelectedValue != null)
            {
                AllEvents.AddRange(Api.getEventsByClass(ClassName.SelectedValue as IdName, start, end)
                    .Where(p => p.Mandatory
                        || viewType == EventData.TypeEnum.Class
                        || ShowOptionalClassEvents.IsChecked.GetValueOrDefault()));
            }

            // User
            if (viewType == EventData.TypeEnum.User)
            {
                AllEvents.AddRange(Api.getPrivateEvents(start, end));
            }

            // Refresh views
            EventsGrid.DataContext = AllEvents;
            RefreshDayGrid();
        }

        /// <summary>
        /// The ClassName ComboBox is filled in function of the selected campus and period.
        /// </summary>
        private void RefreshClassName()
        {
            if (ViewType.SelectedValue != null && ((KeyValuePair<EventData.TypeEnum, string>)ViewType.SelectedValue).Key == EventData.TypeEnum.Class)
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

        /// <summary>
        /// Draws day events in a grid (the inner grid, corresponding to one day).
        /// </summary>
        /// <param name="grid">Grid which will display events</param>
        /// <param name="date">Date to draw</param>
        public void DrawDay(Grid grid, DateTime date)
        {
            ScrollViewer sw = (ScrollViewer)((Grid)grid.Parent).Parent;
            ((Grid)grid.Parent).Measure(new Size(sw.ActualWidth, (double)new GridSizeConverter().Convert(sw.ActualHeight, null, null, null)));
            grid.Children.Clear();
            double gridHeight = ((Grid)grid.Parent).ActualHeight;
            double gridWidth = grid.ActualWidth;

            if (AllEvents != null)
            {
                var dayEvents = AllEvents
                    .Where(p => p.Start < date.AddDays(1) && p.End > date)
                    .OrderBy(p => p.Start);

                // Set the maximum nomber of neighbours events of each event
                for (int i = 0; i < 24; i++)
                {
                    var hourEvents = dayEvents.Where(p => p.StartHour <= i && p.EndHour >= i+1);
                    int eventsCount = hourEvents.Count();
                    foreach (EventData ev in hourEvents)
                    {
                        ev.EventIndex = -1;
                        ev.MaxNeighboursEvents = ev.MaxNeighboursEvents < eventsCount ? eventsCount : ev.MaxNeighboursEvents;
                    }
                }

                // Normalise the MaxNeighboursEvents of each event (previous events can be incorrect)
                int currentIndex = 0;
                for (int i = 0; i < 24; i++)
                {
                    var hourEvents = dayEvents.Where(p => p.StartHour <= i && p.EndHour >= i+1);
                    if (hourEvents.Count() > 0)
                    {
                        int maxEventsCount = hourEvents.Max(p => p.MaxNeighboursEvents);
                        foreach (EventData ev in hourEvents)
                        {
                            if (currentIndex >= maxEventsCount)
                                currentIndex = 0;

                            if (ev.EventIndex == -1)
                            {
                                ev.EventIndex = currentIndex;
                                currentIndex++;
                            }

                            ev.MaxNeighboursEvents = maxEventsCount;
                        }
                    }
                }

                foreach (EventData ev in dayEvents)
                {
                    // Normalise start and end of the event
                    ev.Start = ev.Start < date ? date : ev.Start;
                    ev.End = ev.End > date.AddDays(1) ? date.AddDays(1) : ev.End;

                    // Draw the control
                    EventControl evc = new EventControl();
                    evc.DataContext = ev;

                    double eventSize = gridWidth / ev.MaxNeighboursEvents;

                    evc.Margin = new Thickness(
                        2 + ev.EventIndex * eventSize,
                        2 + gridHeight * ev.Start.Hour / 24,
                        2 + eventSize * (ev.MaxNeighboursEvents - 1 - ev.EventIndex),
                        2 + gridHeight * (24 - ev.End.Hour) / 24);

                    grid.Children.Add(evc);
                }
            }
        }

        public void RefreshDayGrid()
        {
            DrawDay(DayContentGrid, new DateTime(2010, 10, 10));
        }

        private void DayGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshDayGrid();
        }
        #endregion

        #region User actions
        /// <summary>
        /// Called when an item is selected in ComboBoxes.
        /// Display and hide required ComboBoxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = ((KeyValuePair<EventData.TypeEnum, string>)ViewType.SelectedValue).Key;
            // University or user
            if (selected == EventData.TypeEnum.University || selected == EventData.TypeEnum.User)
            {
                CampusName.Visibility = System.Windows.Visibility.Collapsed;
                PeriodName.Visibility = System.Windows.Visibility.Collapsed;
                ClassName.Visibility = System.Windows.Visibility.Collapsed;
            }
            // Campus
            else if (selected == EventData.TypeEnum.Campus)
            {
                CampusName.Visibility = System.Windows.Visibility.Visible;
                PeriodName.Visibility = System.Windows.Visibility.Collapsed;
                ClassName.Visibility = System.Windows.Visibility.Collapsed;
            }
            // Period
            else if (selected == EventData.TypeEnum.Period)
            {
                CampusName.Visibility = System.Windows.Visibility.Collapsed;
                PeriodName.Visibility = System.Windows.Visibility.Visible;
                ClassName.Visibility = System.Windows.Visibility.Collapsed;
            }
            // Class
            else if (selected == EventData.TypeEnum.Class)
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

        private void ShowOptionalEvents_Changed(object sender, RoutedEventArgs e)
        {
            RefreshAllEvents();
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
        #endregion

        #region mugimasaka
        #region Tableaux IdNames
        IdName[] classList = null;
        IdName[] userList = null;
        IdName[] campusList = null;
        IdName[] periodList = null;
        IdName[] promoList = null;
        #endregion

        private void tcOnglets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*if (sender == e.OriginalSource)
            {
                if (tcOnglets.SelectedIndex == 4)
                //On charge la ComboBox
                classList = Api.Server.getIdClassesNames().ToArray();
                cbUsers_Class.DataContext = classList;

                userList = Api.Server.getIdUsersNames().ToArray();
                cbUsers_Users.DataContext = userList;

                //On prépare la StatusBar
                sbStatusText.Foreground = new SolidColorBrush(Colors.Green); ;
                sbStatusText.Text = "Prêt";

            }
             */
        }

        #endregion
    }
}
