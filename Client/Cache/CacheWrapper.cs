using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.BusinessLayer;


namespace Client
{
    public class CacheWrapper
	{
		CacheProcess cacheProcess;
		public BusinessServiceClient Server;
		public CacheWrapper()
		{
			cacheProcess = CacheProcess.Current;
			Server = cacheProcess.Server;
		}


		#region Lecture d'évènements
		public EventData[] getEventsByCampus(int CampusId, DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return Server.getEventsByCampus(CampusId, Start, Stop, LastUpdate);
		}
		public EventData[] getEventsByUniversity(DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return Server.getEventsByUniversity(Start, Stop, LastUpdate);
		}
		public EventData[] getEventsByPeriod(int PeriodId, DateTime Start, DateTime Stop, DateTime LastUpdate) 
		{
			return Server.getEventsByPeriod(PeriodId, Start, Stop, LastUpdate);
		}
		public EventData[] getEventsByClass(int ClassId, DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return Server.getEventsByClass(ClassId, Start, Stop, LastUpdate);
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
		public string[] getModalities()
		{
			return Server.getModalities();
		}
        public Dictionary<IdName, Dictionary<IdName, IdName[]>> getCampusPeriodClassTree()
		{
			return Server.getIdCampusPeriodClassTree();
		}
		#endregion

    }
}
