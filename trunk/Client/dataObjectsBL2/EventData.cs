using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DDay.iCal;

namespace Client.BL2
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
        /// <summary>
        /// Defines to which type of element this event is linked (University, Campus, Period, Class or Private)
        /// </summary>
        //public EventType.Type Type { get; set; }

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


        public void AddEventToCalendar(ref iCalendar ic) 
        {
            Event e=ic.Create<Event>();
			e.Start = new iCalDateTime(this.Start);
			e.End = new iCalDateTime(this.End);
            e.Name = this.Name;
            e.AddProperty(new CalendarProperty("X-Id",  this.Id));
            e.AddProperty(new CalendarProperty("X-Mandatory", this.Mandatory));
            e.AddProperty(new CalendarProperty("X-Modality", this.Modality));
            e.AddProperty(new CalendarProperty("X-Place", this.Place));
            e.AddProperty(new CalendarProperty("X-Speaker", this.Speaker));
            e.AddProperty(new CalendarProperty("X-Subject", this.Subject));    
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
				Api.Server.addEventToUniversity(Name, Start, End, Mandatory, Speaker, Place);
            }

            // Event edition
            else
            {
				Api.ServerBL2.setEvent(this);
            }
        }

        #endregion


		#region IEncodableDataType Members

		public string Encoding
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		#endregion

		#region ICalendarDataType Members

		public ICalendarObject AssociatedObject
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public IICalendar Calendar
		{
			get { throw new NotImplementedException(); }
		}

		public Type GetValueType()
		{
			throw new NotImplementedException();
		}

		public string Language
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void SetValueType(string type)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region ICalendarParameterListContainer Members

		public ICalendarParameterList Parameters
		{
			get { throw new NotImplementedException(); }
		}

		#endregion

		#region ICopyable Members

		public T Copy<T>()
		{
			throw new NotImplementedException();
		}

		public void CopyFrom(ICopyable obj)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IServiceProvider Members

		public object GetService(Type serviceType)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IComparable<IDateTime> Members

		public int CompareTo(IDateTime other)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region IFormattable Members

		public string ToString(string format, IFormatProvider formatProvider)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
