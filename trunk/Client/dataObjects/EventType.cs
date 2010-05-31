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
        /// <summary>
        /// Enumeration of objects an event can be linked to.
        /// </summary>
        public enum Type
        {
            University,
            Campus,
            Period,
            Class,
            User
        }

        private static SortedDictionary<Type, string> eventTypeNames;
		/// <summary>
		/// String equivalence of events types.
		/// </summary>
		public static SortedDictionary<Type, string> EventTypeNames
        {
            get
            {
                if (eventTypeNames == null)
                    eventTypeNames = new SortedDictionary<Type, string>
                    {
                        { Type.University, "Université" },
                        { Type.Campus, "Campus" },
                        { Type.Period, "Période" },
                        { Type.Class, "Classe" },
                        { Type.User, "Utilisateur" }
                    };
                return eventTypeNames;
            }
        }
    }
}
