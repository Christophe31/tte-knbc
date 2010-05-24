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
		public EventData[] getEventsByCampus(int CampusId, DateTime Start, DateTime Stop)
		{
            if (cacheProcess.ServerReachable.Item1)
            {
                if (!Server.isUpToDateByCampus(CampusId , DateTime.Now /*LastUpdate*/))
                {
                    return Server.getEventsByCampus(CampusId, Start, Stop);
                }

            }
			return Server.getEventsByCampus(CampusId, Start, Stop);
		}
		public EventData[] getEventsByUniversity(DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return Server.getEventsByUniversity(Start, Stop);
		}
		public EventData[] getEventsByPeriod(int PeriodId, DateTime Start, DateTime Stop) 
		{
			return Server.getEventsByPeriod(PeriodId, Start, Stop);
		}
		public EventData[] getEventsByClass(int ClassId, DateTime Start, DateTime Stop)
		{
			return Server.getEventsByClass(ClassId, Start, Stop);
		}
		public EventData[] getPrivateEvents(DateTime Start, DateTime Stop)
		{
			return Server.getPrivateEvents(Start, Stop);
		}
		#endregion
		#region completion
		public string[] getCampusNames()
		{
			return Server.getCampusNames();
		}
		public IdName[] getIdClassesNames()
		{
			return Server.getIdClassesNames();
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
