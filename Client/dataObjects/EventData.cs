using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DDay.iCal;

namespace Client.BusinessLayer
{
    public partial class EventData : object, System.Runtime.Serialization.IExtensibleDataObject, INotifyPropertyChanged
    {
        /// <summary>
        /// Defines to which type of element this event is linked.
        /// For example: University, Campus, Period, Class or Private.
        /// </summary>
        public String LinkedTo { get; set; }

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
            e.Start.Value = this.Start;
            e.End.Value=this.End;
            e.Name = this.Name;
            e.AddProperty(new CalendarProperty("Id",  this.Id));
            e.AddProperty(new CalendarProperty("Mandatory", this.Mandatory));
            e.AddProperty(new CalendarProperty("Modality", this.Modality));
            e.AddProperty(new CalendarProperty("Place", this.Place));
            e.AddProperty(new CalendarProperty("Speaker", this.Speaker));
            e.AddProperty(new CalendarProperty("Subject", this.Subject));    
        }
    }
}
