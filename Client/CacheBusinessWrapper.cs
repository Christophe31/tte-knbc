using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
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
		EventData[] getEventsByCampus(string CampusName, DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return Server.getEventsByCampus(CampusName, Start, Stop, LastUpdate);
		}
		EventData[] getEventsByUniversity(DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return Server.getEventsByUniversity(Start, Stop, LastUpdate);
		}
		EventData[] getEventsByPeriod(string PeriodName, DateTime Start, DateTime Stop, DateTime LastUpdate) 
		{
			return Server.getEventsByPeriod(PeriodName, Start, Stop, LastUpdate);
		}
		EventData[] getEventsByClass(string ClassName, DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return Server.getEventsByClass(ClassName, Start, Stop, LastUpdate);
		}
		EventData[] getPrivateEvents(DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return Server.getPrivateEvents(Start, Stop, LastUpdate);
		}
		#endregion
		#region completion
		string[] getCampusNames()
		{
			return Server.getCampusNames();
		}
		string[] getClassesNames()
		{
			return Server.getClassesNames();
		}
		string[] getPromotionsNames()
		{
			return Server.getPromotionsNames();
		}
		string[] getPeriodsNames()
		{
			return Server.getPeriodsNames();
		}
		string[] getSubjectsNames()
		{
			return Server.getSubjectsNames();
		}
		string[] getUsersNames()
		{
			return Server.getUsersNames();
		}
		string[] getEventsTypes()
		{
			return Server.getEventsTypes();
		}
		Dictionary<string, Dictionary<string, string[]>> getCampusPeriodClassTree()
		{
			return Server.getCampusPeriodClassTree();
		}
		#endregion

    }
}
