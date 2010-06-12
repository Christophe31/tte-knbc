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
using Client.BusinessService;
using Client.Calendar;

namespace Client
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
    {
        #region Properties
        private IdName _selectedPlanning;
        public IdName SelectedPlanning
        {
            get
            {
                if (ViewType.SelectedValue != null)
                {
                    EventData.TypeEnum viewType = ((KeyValuePair<EventData.TypeEnum, string>) ViewType.SelectedValue).Key;
                    if (viewType == EventData.TypeEnum.University)
                        _selectedPlanning = Api.getPlannings(EventData.TypeEnum.University).First() as IdName;
                    else if (viewType == EventData.TypeEnum.Campus && CampusName.SelectedValue != null)
                        _selectedPlanning = CampusName.SelectedValue as IdName;
                    else if (viewType == EventData.TypeEnum.Period && PeriodName.SelectedValue != null)
                        _selectedPlanning = PeriodName.SelectedValue as IdName;
                    else if (viewType == EventData.TypeEnum.Class && ClassName.SelectedValue != null)
                        _selectedPlanning = ClassName.SelectedValue as IdName;
                    else if (viewType == EventData.TypeEnum.User)
                        _selectedPlanning = Api.CurrentUser as IdName;

                    _selectedPlanning = new IdName() { Id = _selectedPlanning.Id, Name = _selectedPlanning.Name };
                }
                else
                    EventData.SelectedPlanning = new IdName() { Id = 0, Name = "No planning" };

                return _selectedPlanning;
            }
        }
        #endregion Properties

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
        /// <summary>
        /// Event currently selected (for edition).
        /// </summary>
        protected EventData selectedEvent;
        #endregion

        #region Window init
		delegate Login loginConstructorDelegate(MainWindow m);
        public MainWindow()
		{
			this.Visibility = System.Windows.Visibility.Hidden;
			InitializeComponent();
			new Login(this); 
		}

        /// <summary>
        /// Called when the window is ready.
        /// Launches a thread which will fill ComboBoxes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			Api = new CacheWrapper();
			new Thread((ThreadStart)FillCombobox).Start();
        }

		private void FillCombobox()
		{
			while (!Api.ServerAvailable&&!Api.CacheAvailable)
				Thread.Sleep(700);
			this.Dispatcher.BeginInvoke((ThreadStart)FillControls);
		}

        /// <summary>
        /// Fill ComboBoxes.
        /// </summary>
		private void FillControls()
		{
            CampusPeriodClassTree = Api.getCampusPeriodClassTree();

            DisplayedDate.SelectedDate = DateTime.Today;

            // Draw Grids
            ((ScrollViewer)DayGrid.Parent).ScrollToVerticalOffset(DayGrid.Height * 7 / 24);
            DayGrid.Children.Add(GetHoursGrid());
            ((ScrollViewer)WeekGrid.Parent).ScrollToVerticalOffset(WeekGrid.Height * 7 / 24);
            WeekGrid.Children.Add(GetHoursGrid());

			// ComboBoxes initialisation
            CampusName.DataContext = Api.getPlannings(EventData.TypeEnum.Campus);
            PeriodName.DataContext = Api.getPlannings(EventData.TypeEnum.Period);
            ViewType.DataContext = EventType.EventTypeNames;
			ViewType.SelectedIndex = 4;
            for (int i = 0; i < 24; i++)
            {
                EditEvent_StartHour.Items.Add(i);
                EditEvent_EndHour.Items.Add(i);
			}

            // Set a null event
            EditEvent_Reset_Click(null, null);
            
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

            return grid;
        }
        #endregion

        #region Refreshes
        /// <summary>
        /// Refresh the events list, for the period specified in the GUI, including (or excluding) mandatory events.
        /// </summary>
        private void RefreshAllEvents()
		{
            // Refresh selected planning
            IdName selectedPlanning = SelectedPlanning;

            DateTime start = DisplayedDate.SelectedDate.GetValueOrDefault().AddMonths(-6);
            DateTime end = DisplayedDate.SelectedDate.GetValueOrDefault().AddMonths(6);

            EventData.TypeEnum viewType = EventData.TypeEnum.University;
            if (ViewType.SelectedValue != null)
                viewType = ((KeyValuePair<EventData.TypeEnum, string>) ViewType.SelectedValue).Key;

            // University
			AllEvents = Api.getEvents(Api.getPlannings(EventData.TypeEnum.University).First().Id, start, end)
                .Where(p => p.Mandatory
                    || viewType == EventData.TypeEnum.University
                    || ShowOptionalUniversityEvents.IsChecked.GetValueOrDefault()).ToList();

            // Campus
			if ((viewType == EventData.TypeEnum.Campus
				|| viewType == EventData.TypeEnum.Class
				|| viewType == EventData.TypeEnum.User)
                && CampusName.SelectedValue != null)
            {
                AllEvents.AddRange(Api.getEvents(CampusName.SelectedValue as IdName, start, end)
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
                AllEvents.AddRange(Api.getEvents(PeriodName.SelectedValue as IdName, start, end)
                    .Where(p => p.Mandatory
                        || viewType == EventData.TypeEnum.Period
                        || ShowOptionalPeriodEvents.IsChecked.GetValueOrDefault()));
            }

            // Class
            if ((viewType == EventData.TypeEnum.Class || viewType == EventData.TypeEnum.User)
                && ClassName.SelectedValue != null)
            {
                AllEvents.AddRange(Api.getEvents(ClassName.SelectedValue as IdName, start, end)
                    .Where(p => p.Mandatory
                        || viewType == EventData.TypeEnum.Class
                        || ShowOptionalClassEvents.IsChecked.GetValueOrDefault()));
            }

            // User
            if (viewType == EventData.TypeEnum.User)
            {
                AllEvents.AddRange(Api.getEvents(Api.CurrentUser,start, end));
            }

            // Refresh views
            EventsGrid.DataContext = AllEvents;
            RefreshDayGrid();
            RefreshWeekGrid();
            RefreshMonthGrid();
        }

        /// <summary>
        /// The ClassName ComboBox is filled in function of the selected campus and period.
        /// </summary>
        private void RefreshClassName()
        {
            var viewType = (KeyValuePair<EventData.TypeEnum, string>)ViewType.SelectedValue;
            if (ViewType.SelectedValue != null
                && CampusName.SelectedValue != null
                && PeriodName.SelectedValue != null
                && (viewType.Key == EventData.TypeEnum.Class || viewType.Key == EventData.TypeEnum.User))
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
                    ClassName.DataContext = new IdName[]{};
                }
                catch (ArgumentNullException)
                {
					ClassName.DataContext = new IdName[] { };
                }
				if (ClassName.DataContext == null) ClassName.DataContext = new IdName[] { };
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
                    evc.MouseLeftButtonUp += new MouseButtonEventHandler(EventControl_MouseLeftButtonUp);

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

        private void EventControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is TextBlock)
                selectedEvent = ((TextBlock)sender).DataContext as EventData;
            else if (sender is EventControl)
                selectedEvent = ((EventControl)sender).DataContext as EventData;
            FillEditEventToolbar();
        }

        public void RefreshDayGrid()
        {
            DrawDay(DayContentGrid, DisplayedDate.SelectedDate.GetValueOrDefault());
        }

        private void DayGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshDayGrid();
        }

        public void RefreshWeekGrid()
        {
            DateTime selectedDate = DisplayedDate.SelectedDate.HasValue ? DisplayedDate.SelectedDate.GetValueOrDefault() : DateTime.Today;
            DrawDay(MondayContentGrid, selectedDate.AddDays(1-(int)selectedDate.DayOfWeek));
            DrawDay(TuesdayContentGrid, selectedDate.AddDays(2-(int)selectedDate.DayOfWeek));
            DrawDay(WednesdayContentGrid, selectedDate.AddDays(3-(int)selectedDate.DayOfWeek));
            DrawDay(ThursdayContentGrid, selectedDate.AddDays(4-(int)selectedDate.DayOfWeek));
            DrawDay(FridayContentGrid, selectedDate.AddDays(5-(int)selectedDate.DayOfWeek));
            DrawDay(SaturdayContentGrid, selectedDate.AddDays(6-(int)selectedDate.DayOfWeek));
            DrawDay(SundayContentGrid, selectedDate.AddDays(-(int)selectedDate.DayOfWeek));
        }

        private void WeekGrid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshWeekGrid();
        }

        private void DayGridPreviousDay_Click(object sender, RoutedEventArgs e)
        {
            DisplayedDate.SelectedDate = DisplayedDate.SelectedDate.GetValueOrDefault().AddDays(-1);
        }

        private void DayGridNextDay_Click(object sender, RoutedEventArgs e)
        {
            DisplayedDate.SelectedDate = DisplayedDate.SelectedDate.GetValueOrDefault().AddDays(1);
        }

        public void RefreshMonthGrid()
        {
            if (AllEvents != null)
            {
                // Remove old elements
                MonthGrid.Children.Clear();

                // Remove grid lines
                MonthGrid.RowDefinitions.Clear();

                // Set columns headers
                RowDefinition header = new RowDefinition();
                header.Height = new GridLength();
                MonthGrid.RowDefinitions.Add(header);
                string[] days = new string[] { "Lundi", "Mardi", "Mercredi", "Jeudi", "Vendredi", "Samedi", "Dimanche" };
                for (int i = 0; i < 7; i++)
                {
                    Label day = new Label();
                    day.Content = days[i];
                    day.FontWeight = FontWeights.Bold;
                    day.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    Grid.SetColumn(day, i);
                    Grid.SetRow(day, 0);
                    MonthGrid.Children.Add(day);
                }

                DateTime selectedDate = DisplayedDate.SelectedDate.HasValue ? DisplayedDate.SelectedDate.GetValueOrDefault() : DateTime.Today;

                // Current row
                int row = 1;
                MonthGrid.RowDefinitions.Add(new RowDefinition());
                // For each day of month
                DateTime end = selectedDate.AddDays(-selectedDate.Day).AddMonths(1);
                for (DateTime i = selectedDate.AddDays(1 - selectedDate.Day); i <= end; i = i.AddDays(1))
                {
                    int col = i.DayOfWeek == DayOfWeek.Sunday ? 6 : ((int)i.DayOfWeek) - 1;
                    // Create and place a StackPanel
                    StackPanel sp = new StackPanel();
                    Grid.SetRow(sp, row);
                    Grid.SetColumn(sp, col);
                    Grid.SetZIndex(sp, 1);
                    MonthGrid.Children.Add(sp);

                    // Day number displayed in each cell
                    TextBlock day = new TextBlock();
                    day.Text = i.Day.ToString();
                    
                    day.FontSize *= 1.5;
                    day.Foreground = Brushes.DarkGray;

                    Grid.SetRow(day, row);
                    Grid.SetColumn(day, col);
                    day.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                    day.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                    day.Margin = new Thickness(3);
                    MonthGrid.Children.Add(day);

                    // Place events in the StackPanel
                    foreach (EventData ev in AllEvents.Where(p => p.StartDate <= i && p.EndDate >= i).OrderBy(p => p.StartHour))
                    {
                        Border b = new Border();
                        b.Background = ev.BackgroundColor;
                        b.BorderBrush = ev.BorderColor;
                        b.BorderThickness = new Thickness(1);
                        b.Margin = new Thickness(1,1,1,0);

                        TextBlock tb = new TextBlock();
                        tb.Text = ev.Start.ToShortTimeString() + " - " + (String.IsNullOrEmpty(ev.Name) ? ev.Subject.Name : ev.Name);

                        tb.DataContext = ev;
                        tb.MouseLeftButtonUp += new MouseButtonEventHandler(EventControl_MouseLeftButtonUp);

                        b.Child = tb;
                        sp.Children.Add(b);
                    }

                    // Change of line if needed
                    if (i.DayOfWeek == DayOfWeek.Sunday)
                    {
                        MonthGrid.RowDefinitions.Add(new RowDefinition());
                        row++;
                    }

                }
            }
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
            bool enableEditor = false;
            // University
            if (selected == EventData.TypeEnum.University)
            {
                CampusName.Visibility = System.Windows.Visibility.Collapsed;
                PeriodName.Visibility = System.Windows.Visibility.Collapsed;
                ClassName.Visibility = System.Windows.Visibility.Collapsed;
                enableEditor = Api.CurrentUser.Roles.Any(p => p.Role == RoleData.RoleType.Administrator);
            }
            // Campus
            else if (selected == EventData.TypeEnum.Campus)
            {
                CampusName.Visibility = System.Windows.Visibility.Visible;
                PeriodName.Visibility = System.Windows.Visibility.Collapsed;
                ClassName.Visibility = System.Windows.Visibility.Collapsed;
                enableEditor = Api.CurrentUser.Roles.Any(p => p.Role == RoleData.RoleType.Administrator
                    || p.Role == RoleData.RoleType.CampusManager);
            }
            // Period
            else if (selected == EventData.TypeEnum.Period)
            {
                CampusName.Visibility = System.Windows.Visibility.Collapsed;
                PeriodName.Visibility = System.Windows.Visibility.Visible;
                ClassName.Visibility = System.Windows.Visibility.Collapsed;
                enableEditor = Api.CurrentUser.Roles.Any(p => p.Role == RoleData.RoleType.Administrator);
            }
            // Class
            else if (selected == EventData.TypeEnum.Class)
            {
                RefreshClassName();

                CampusName.Visibility = System.Windows.Visibility.Visible;
                PeriodName.Visibility = System.Windows.Visibility.Visible;
                ClassName.Visibility = System.Windows.Visibility.Visible;
                enableEditor = Api.CurrentUser.Roles.Any(p => p.Role == RoleData.RoleType.Administrator
                    || p.Role == RoleData.RoleType.CampusManager);
            }
            // User
            else if (selected == EventData.TypeEnum.User)
            {
                CampusName.Visibility = System.Windows.Visibility.Collapsed;
                PeriodName.Visibility = System.Windows.Visibility.Collapsed;
                ClassName.Visibility = System.Windows.Visibility.Collapsed;

                IdName className = Api.CurrentUser.Class;

                if (className != null)
                {
                    IdName campusName = CampusPeriodClassTree.Where(
                                                    dc => dc.Value.Any(
                                                        dp => dp.Value.Any(
                                                            cl => cl.Id == Api.CurrentUser.Class.Id)))
                                                            .Select(p => p.Key).FirstOrDefault();
                    IdName periodName = CampusPeriodClassTree[campusName].Where(
                        dp => dp.Value.Any(
                            cl => cl.Id == Api.CurrentUser.Class.Id))
                            .Select(p => p.Key).FirstOrDefault();

                    CampusName.SelectedIndex = Array.FindIndex((IdName[])CampusName.DataContext, p => p.Id == campusName.Id);
                    PeriodName.SelectedIndex = Array.FindIndex((IdName[])PeriodName.DataContext, p => p.Id == periodName.Id);
                    ClassName.SelectedIndex = Array.FindIndex((IdName[])ClassName.DataContext ?? new IdName[] { }, p => p.Id == className.Id);
                }
                else
                {
                    CampusName.SelectedIndex = -1;
                    PeriodName.SelectedIndex = -1;
                    ClassName.SelectedIndex = -1;
                }

                enableEditor = true;
            }

            EditEvent.Visibility = enableEditor ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
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

        private void DisplayedDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshDayGrid();
            RefreshWeekGrid();
            RefreshMonthGrid();
        }
        #endregion


        private void DayContentGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            Grid day = (Grid)sender;
            Grid week = ((Grid)day.Parent);

            int col = Grid.GetColumn(day);

            week.ColumnDefinitions[col].Width = new GridLength(3, GridUnitType.Star);
            RefreshWeekGrid();
        }

        private void DayContentGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            Grid day = (Grid)sender;
            Grid week = ((Grid)day.Parent);

            int col = Grid.GetColumn(day);
            
            week.ColumnDefinitions[col].Width = new GridLength(1, GridUnitType.Star);
            RefreshWeekGrid();
        }

		private void iCal_export_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
			dlg.Filter = "ICalendar files (*.ics)|*.ics|All files (*.*)|*.*";
			dlg.AddExtension = true;
			dlg.ValidateNames = true;
			dlg.ShowDialog(this);
			try
			{
				DDay.iCal.Serialization.iCalendar.iCalendarSerializer calSerializer = new DDay.iCal.Serialization.iCalendar.iCalendarSerializer();
				DDay.iCal.iCalendar ical = new DDay.iCal.iCalendar();
				foreach( EventData even in Api.getEvents(this.SelectedPlanning,DateTime.MinValue,DateTime.MaxValue))
					even.AddEventToCalendar(ref ical);
				calSerializer.Serialize(ical, dlg.FileName);
			}
			catch
			{
				MessageBox.Show("Une erreur est survenue...", "et mince...", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void iCal_import_Click(object sender, RoutedEventArgs e)
		{
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.Filter = "ICalendar files (*.ics)|*.ics|All files (*.*)|*.*";
			dlg.Title="Calendar Import";
			dlg.CheckFileExists = true;
			dlg.ShowDialog();
			string s = "Rapport:";
			try
			{
				var calendars = DDay.iCal.iCalendar.LoadFromFile(dlg.FileName);

				foreach (EventData even in calendars.First().Events.Select(
							p => EventData.CreateFromICalEvent(p)
							))
				{
					even.Type = ((KeyValuePair<EventData.TypeEnum, string>)ViewType.SelectedValue).Key;
					even.ParentPlanning = SelectedPlanning;
					s += "\r\n" + Api.Server.addEvent(even);
				}
			}
			catch
			{
				MessageBox.Show("Une erreur est survenue...", "et mince...", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
			finally 
			{ 
				MessageBox.Show(s); 
			}
		}
		
		private void report_Click(object sender, RoutedEventArgs e)
		{
			 //Api.Server.getSubjects().Select(sub=>sub.Modalities.Select(mod=>sub.Name+";"+mod.Name+";"+mod.Hours+";"+Api.Server.SpeakingEvents);
			
		   //string[] colums= new string { "Subject", "Modality", "", "" };
		}

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
			Api.Disconnect();
			this.Visibility = System.Windows.Visibility.Hidden;
			new Login(this);

        }

        private void FillEditEventToolbar()
        {
            if (Api.ServerAvailable)
            {
                EditEvent_Type.Content = selectedEvent.LinkedTo;
                EditEvent_Mandatory.IsChecked = selectedEvent.Mandatory;
                EditEvent_Name.Text = selectedEvent.Name;
                EditEvent_Place.Text = selectedEvent.Place;
                EditEvent_Speaker.DataContext = Api.Server.getSubjects();
                EditEvent_Speaker.SelectedValue = selectedEvent.Speaker;
                EditEvent_Subject.DataContext = Api.getSubjects();
                if ((EditEvent_Subject.DataContext as SubjectData[]).Count() > 0)
                {
                    EditEvent_Subject.SelectedValue = selectedEvent.Subject;
                    EditEvent_Modality.SelectedValue = selectedEvent.Modality;
                }
                EditEvent_StartDate.SelectedDate = selectedEvent.StartDate;
                EditEvent_StartHour.SelectedValue = selectedEvent.StartHour;
                EditEvent_EndDate.SelectedDate = selectedEvent.EndDate;
                EditEvent_EndHour.SelectedValue = selectedEvent.EndHour;
            }
        }

        private void EditEvent_Subject_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SubjectData subject = (SubjectData)EditEvent_Subject.SelectedValue;
            if (subject != null)
                EditEvent_Modality.DataContext = subject.Modalities;
        }

        private void EventsGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            selectedEvent = EventsGrid.SelectedValue as EventData;
            if (selectedEvent != null)
                FillEditEventToolbar();
        }

        private void EditEvent_Reset_Click(object sender, RoutedEventArgs e)
        {
            EventData.TypeEnum type = EventData.TypeEnum.User;
            if (ViewType.SelectedValue != null)
                type = (EventData.TypeEnum)((KeyValuePair<EventData.TypeEnum, string>)ViewType.SelectedValue).Key;

            selectedEvent = new EventData()
            {
                Type = type,
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };
            FillEditEventToolbar();
        }

        private void EditEvent_Apply_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEvent != null && Api.ServerAvailable)
            {
                selectedEvent.Mandatory = EditEvent_Mandatory.IsChecked.GetValueOrDefault();
                selectedEvent.Name = EditEvent_Name.Text;
                selectedEvent.Place = EditEvent_Place.Text = selectedEvent.Place;
                selectedEvent.Speaker = (IdName)EditEvent_Speaker.SelectedValue;
                selectedEvent.Subject = (IdName)EditEvent_Subject.SelectedValue;
                selectedEvent.Modality = (IdName)EditEvent_Modality.SelectedValue;

                selectedEvent.ParentPlanning = SelectedPlanning;
                
                selectedEvent.StartDate = EditEvent_StartDate.SelectedDate.GetValueOrDefault();
                selectedEvent.StartHour = (int)EditEvent_StartHour.SelectedValue;
                selectedEvent.EndDate = EditEvent_EndDate.SelectedDate.GetValueOrDefault();
                selectedEvent.EndHour = (int)EditEvent_EndHour.SelectedValue;

                if (selectedEvent.Id == 0)
                    Api.Server.addEvent(selectedEvent);
                else
                    Api.Server.setEvent(selectedEvent);

                RefreshAllEvents();
            }
        }

        private void EditEvent_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEvent.Id != 0 && Api.ServerAvailable)
            {
                Api.Server.delEvent(selectedEvent.Id);
                RefreshAllEvents();
            }
        }
    }
}
