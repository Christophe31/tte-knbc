using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.BusinessLayer;

namespace Client.BusinessLayer
{
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

        /// <summary>
        /// String equivalence of events types
        /// </summary>
        private SortedDictionary<Type, string> eventTypeNames;
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
