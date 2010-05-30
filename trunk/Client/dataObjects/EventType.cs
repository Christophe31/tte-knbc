using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.BusinessLayer;

namespace Client.BusinessLayer
{
	/// <summary>
	/// should it be static (or singleton) for meaning purposes?
	/// because here it means 1 event type in RAM and a StoredDictionnary 
	/// (which will always be the same) by event in memory...
	/// 
	/// </summary>
    public class EventType
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

		// to switch the comment looks ugly but it's better to document public element than privates one.
        private readonly SortedDictionary<Type, string> eventTypeNames;
		/// <summary>
		/// String equivalence of events types
		/// http://jefferytay.wordpress.com/2009/04/16/performance-of-generics-sorteddictionary-and-dictionary/
		/// why a SortedDictionary?
		/// </summary>
		public SortedDictionary<Type, string> EventTypeNames
        {
            get
            {
                return eventTypeNames;
            }
        }

        public EventType()
        {
            eventTypeNames = new SortedDictionary<Type, string>
            {
                { Type.University, "Université" },
                { Type.Campus, "Campus" },
                { Type.Period, "Période" },
                { Type.Class, "Classe" },
                { Type.User, "Utilisateur" }
            };
        }
    }
}
