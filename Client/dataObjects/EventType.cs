using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.BusinessLayer;

namespace Client.BusinessLayer
{
	/// <summary>
	/// Stores informations to manage and display event types.
    /// It allows to define to what an event is linked to (campus, period...)
	/// </summary>
    public static class EventType
    {
        private static SortedDictionary<EventData.TypeEnum, string> eventTypeNames;
		/// <summary>
		/// String equivalence of events types.
		/// </summary>
		public static SortedDictionary<EventData.TypeEnum, string> EventTypeNames
        {
            get
            {
                if (eventTypeNames == null)
                    eventTypeNames = new SortedDictionary<EventData.TypeEnum, string>
                    {
                        { EventData.TypeEnum.University, "Université" },
                        { EventData.TypeEnum.Campus, "Campus" },
                        { EventData.TypeEnum.Period, "Période" },
                        { EventData.TypeEnum.Class, "Classe" },
                        { EventData.TypeEnum.User, "Utilisateur" }
                    };
                return eventTypeNames;
            }
        }
    }
}
