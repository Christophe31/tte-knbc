using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        
    }
}
