using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DDay.iCal;
using System.Windows.Media;

namespace Client.BusinessService
{
	/// <summary>
	/// This object represents all the events attributes to displays in the calandar, 
	/// 
	/// It implements those interfaces:
	/// IExtensibleDataObject, allow us to extend a serializable object
	/// INotifyPropertyChanged,
	/// IEditableObject
	/// </summary>
    public partial class EventData : object, System.Runtime.Serialization.IExtensibleDataObject, INotifyPropertyChanged, IEditableObject
    {
        #region Properties
        /// <summary>
        /// Returns the string corresponding to the Type property
        /// </summary>
        public string LinkedTo
        {
            get
            {
                return EventType.EventTypeNames[Type.Value];
            }
        }

        /// <summary>
        /// Gets or sets the hour component of the Start date
        /// </summary>
        public int StartHour
        {
            get
            {
                return Start.Hour;
            }
            set
            {
                Start = Start.Date;
                Start.AddHours(value);
                OnPropertyChanged("Start");
            }
        }

        /// <summary>
        /// Gets or sets the date component of the Start date
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return Start.Date;
            }
            set
            {
                int hour = Start.Hour;
                Start = value.Date;
                Start.AddHours(hour);
                OnPropertyChanged("Start");
            }
        }

        /// <summary>
        /// Gets or sets the hour component of the End date
        /// </summary>
        public int EndHour
        {
            get
            {
                return End.Hour;
            }
            set
            {
                End = End.Date;
                End.AddHours(value);
                OnPropertyChanged("End");
            }
        }

        /// <summary>
        /// Gets or sets the date component of the End date
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                return End.Date;
            }
            set
            {
                int hour = End.Hour;
                End = value.Date;
                End.AddHours(hour);
                OnPropertyChanged("End");
            }
        }

        /// <summary>
        /// Get or set the maximum number of simultaneous events for this event
        /// </summary>
        public int MaxNeighboursEvents { get; set; }

        /// <summary>
        /// Index of the current event in the displayed day (must be refreshed for each day drawing)
        /// </summary>
        public int EventIndex { get; set; }

        /// <summary>
        /// Get a color for borders around the event, depending on its modality or its type.
        /// </summary>
        public SolidColorBrush BorderColor
        {
            get
            {
                // Modality specified
                string[] modalities = SubjectData.PresetModalities;
				if (Modality != null)
				{
					if (Modality == modalities[0])
						return Brushes.MediumTurquoise;
					else if (Modality == modalities[1])
						return Brushes.PaleTurquoise;
					else if (Modality == modalities[2])
						return Brushes.PaleGreen;
					else if (Modality == modalities[3])
						return Brushes.Lime;
					else if (Modality == modalities[4])
						return Brushes.Orange;
					else if (Modality == modalities[5])
						return Brushes.Red;
				}
				// Fallback to planning type
				else if (Type == TypeEnum.University)
					return Brushes.Chartreuse;
				else if (Type == TypeEnum.Campus)
					return Brushes.MediumSpringGreen;
				else if (Type == TypeEnum.Period)
					return Brushes.PowderBlue;
				else if (Type == TypeEnum.Class)
					return Brushes.Cyan;
				else if (Type == TypeEnum.User)
					return Brushes.Silver;

                return Brushes.White;
            }
        }
        
        /// <summary>
        /// Get a color for background of the event (it is the border color, but lighter).
        /// </summary>
        public SolidColorBrush BackgroundColor
        {
            get
            {
                SolidColorBrush color = new SolidColorBrush(BorderColor.Color);
                color.Opacity = 0.1;
                return color;
            }
        }

        private static IdName[] _speakers = null;
        /// <summary>
        /// Get a table of available speakers.
        /// </summary>
        public IdName[] Speakers
        {
            get
            {
                if (_speakers == null)
                    _speakers = new CacheWrapper().Server.getSpeakers();
                return _speakers;
            }
        }

        /// <summary>
        /// Get the index of the current speaker in the table Speakers.
        /// </summary>
        public int SpeakerIndex
        {
            get
            {
                return Array.FindIndex(Speakers, p => p.Id == Speaker.Id);
            }
        }

        private static SubjectData[] _subjects = null;
        /// <summary>
        /// Get a table of available subjects.
        /// </summary>
        public SubjectData[] Subjects
        {
            get
            {
                if (_subjects == null)
                    _subjects = new CacheWrapper().getSubjects();
                return _subjects;
            }
        }

        /// <summary>
        /// Get the index of the current subject in the table Subjects.
        /// </summary>
        public int SubjectIndex
        {
            get
            {
                return Array.FindIndex(Subjects, p => p.Id == Subject.Id);
            }
        }

        /// <summary>
        /// Get a table of available modalities for the current subject.
        /// </summary>
        public ModalityData[] Modalities
        {
            get
            {
                return Subjects[SubjectIndex] == null ? null : Subjects[SubjectIndex].Modalities;
            }
        }

        /// <summary>
        /// Get the index of the current modality in the available modalities.
        /// </summary>
        public int ModalityIndex
        {
            get
            {
                return Array.FindIndex(Modalities, p => p.Id == Modality.Id);
            }
        }

        /// <summary>
        /// Get or set the selected planning.
        /// </summary>
        public static IdName SelectedPlanning { get; set; }

        #endregion Properties

        // Create the OnPropertyChanged method to raise the event
		protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public EventData()
        {
        }

        public static EventData CreateFromICalEvent(IEvent p)
        {
            EventData ED = new EventData();
			ED.Start = p.Start.Date;
			ED.End = p.End.Date;
			ED.Mandatory = p.Priority == 1;
			ED.Place = p.Location;
			ED.Name = p.Summary;ED.Speaker = new IdName();ED.Modality = new IdName();ED.Subject = new IdName();
			try
			{
				ED.Speaker.Id = int.Parse(p.Organizer.Encoding);
				ED.Speaker.Name = p.Organizer.CommonName;
				ED.Modality.Id = int.Parse(ED.Modality = p.Properties.Where(f => f.Key == "X-MODALITY-ID").First().Value.ToString());
				ED.Modality.Name = p.Properties.Where(f => f.Key == "X-MODALITY-NAME").First().Value.ToString();
				ED.Subject.Id = int.Parse(p.Properties.Where(f => f.Key == "X-SUBJECT-ID").First().Value.ToString());
				ED.Subject.Name = p.Properties.Where(f => f.Key == "X-SUBJECT-NAME").First().Value.ToString();
			}
			catch { }
			return ED;
        }

        public void AddEventToCalendar(ref iCalendar ic) 
        {
            Event e=ic.Create<Event>();
			e.Start = new iCalDateTime(this.Start);
			e.End = new iCalDateTime(this.End);
            e.Name = "VEVENT";
			e.Priority = this.Mandatory ? 1 : 0;
			e.Location = this.Place;
			e.Summary=this.Name;
			if (Speaker!=null)
				e.Organizer = new Organizer(){CommonName=this.Speaker.Name, Encoding=this.Speaker.Id.ToString()};
			if (Modality != null && Subject != null)
			{
				e.AddProperty(new CalendarProperty("X-MODALITY-NAME", this.Modality.Name));
				e.AddProperty(new CalendarProperty("X-MODALITY-ID", this.Modality.Name));
				e.AddProperty(new CalendarProperty("X-SUBJECT-NAME", this.Subject.Id.ToString()));
				e.AddProperty(new CalendarProperty("X-SUBJECT-ID", this.Subject.Id.ToString()));
				e.Description = this.Modality + " " + this.Subject;
			}
			else
			{
				e.Description = this.Name;
			}
			

        }


        #region IEditableObject Members

        void IEditableObject.BeginEdit()
        {
        }

        void IEditableObject.CancelEdit()
        {
        }

        void IEditableObject.EndEdit()
        {
            CacheWrapper Api = new CacheWrapper();
            ParentPlanning = SelectedPlanning;
            // Event creation
            if (Id == 0)
            {
                Api.Server.addEvent(this);
            }

            // Event edition
            else
            {
				Api.Server.setEvent(this);
            }
        }

        #endregion
	}
}
