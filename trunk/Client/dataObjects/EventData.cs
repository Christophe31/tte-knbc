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

        public SolidColorBrush BorderColor
        {
            get
            {
                // Modality specified
                string[] modalities = SubjectData.PresetModalities;
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
        public IdName[] Speakers
        {
            get
            {
                if (_speakers == null)
                    _speakers = new CacheWrapper().Server.getSpeakers();
                return _speakers;
            }
        }

        public int SpeakerIndex
        {
            get
            {
                return Array.FindIndex(Speakers, p => p.Id == Speaker.Id);
            }
        }

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
			try
			{
				ED.Id = int.Parse(p.UID);
			}
			catch (FormatException)
			{
				ED.Id = 0;
			}
			ED.Start = p.Start.Date;
			ED.End = p.End.Date;
			ED.Mandatory = p.Priority == 1;
			ED.Place = p.Location;
			ED.Name = p.Name;
			ED.Speaker = p.Organizer.CommonName;
			ED.Modality = p.Properties.Where(f => f.Key == "X-MODALITY").First().Value.ToString();
			ED.Subject = p.Properties.Where(f => f.Key == "X-SUBJECT").First().Value.ToString();
			return ED;
        }

        public void AddEventToCalendar(ref iCalendar ic) 
        {
            Event e=ic.Create<Event>();
			e.UID = this.Id.ToString();
			e.Start = new iCalDateTime(this.Start);
			e.End = new iCalDateTime(this.End);
            e.Name = this.Name;
			e.Priority = this.Mandatory ? 1 : 0;
			e.Location = this.Place;
			e.Organizer = new Organizer(){CommonName=this.Speaker};
			e.Summary=this.Name;
            e.AddProperty(new CalendarProperty("X-MODALITY", this.Modality));
            e.AddProperty(new CalendarProperty("X-SUBJECT", this.Subject));
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
            // Event creation
            if (Id == 0)
            {
				//Api.Server.addEventToUniversity(Name, Start, End, Mandatory, Speaker, Place);
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
