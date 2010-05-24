using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DDay.iCal;
namespace Client.BusinessLayer
{
    public partial class EventData : object, System.Runtime.Serialization.IExtensibleDataObject
    {
        /// <summary>
        /// Defines to which type of element this event is linked.
        /// For example: University, Campus, Period, Class or Private.
        /// </summary>
        public String LinkedTo { get; set; }
        
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
