using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessLayer;

namespace Client
{
    public class CacheBusinessWrapper
	{
		#region singleton
		static protected CacheBusinessWrapper self;
		protected CacheBusinessWrapper()
		{
			Server = new BusinessLayerClient();
		}
		static public CacheBusinessWrapper getCacheWrapper()
		{
			if (self != null)
				return self;
			self = new CacheBusinessWrapper();
			return self;
		}
		#endregion
		public BusinessLayerClient Server;

		#region Lecture d'évènements
		public EventData[] getEventsByCampus(string CampusName, DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return Server.getEventsByCampus(CampusName, Start, Stop, LastUpdate);
		}
		public EventData[] getEventsByUniversity(DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return Server.getEventsByUniversity(Start, Stop, LastUpdate);
		}
		public EventData[] getEventsByPeriod(string PeriodName, DateTime Start, DateTime Stop, DateTime LastUpdate) 
		{
			return Server.getEventsByPeriod(PeriodName, Start, Stop, LastUpdate);
		}
		public EventData[] getEventsByClass(string ClassName, DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return Server.getEventsByClass(ClassName, Start, Stop, LastUpdate);
		}
		public EventData[] getPrivateEvents(DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return Server.getPrivateEvents(Start, Stop, LastUpdate);
		}
		#endregion
		#region completion
		public string[] getCampusNames()
		{
			return Server.getCampusNames();
		}
		public string[] getClassesNames()
		{
			return Server.getClassesNames();
		}
		public string[] getPromotionsNames()
		{
			return Server.getPromotionsNames();
		}
		public string[] getPeriodsNames()
		{
			return Server.getPeriodsNames();
		}
		public string[] getSubjectsNames()
		{
			return Server.getSubjectsNames();
		}
		public string[] getUsersNames()
		{
			return Server.getUsersNames();
		}
		public string[] getEventsTypes()
		{
			return Server.getEventsTypes();
		}
		public Dictionary<string, Dictionary<string, string[]>> getCampusPeriodClassTree()
		{
			return Server.getCampusPeriodClassTree();
		}
		#endregion

    }
}
