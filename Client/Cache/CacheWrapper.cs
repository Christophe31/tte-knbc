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
		public EventData[] getEventsByCampus(IdName Campus, DateTime Start, DateTime Stop)
		{
            return Server.getEventsByCampus(Campus.Id, Start, Stop);/*
            if (cacheProcess.ServerReachable)
            {
                if (!Server.isUpToDateByCampus(Campus.Id , DateTime.Now ))
                {
					cacheProcess.RefreshCache((CacheProcess.EventsGetterId)Server.getEventsByCampus, Campus);
                    return Server.getEventsByCampus(Campus.Id, Start, Stop);
                }

            }
			return Server.getEventsByCampus(Campus.Id, Start, Stop);
		*/}
		public EventData[] getEventsByUniversity(DateTime Start, DateTime Stop)
		{
			return Server.getEventsByUniversity(Start, Stop);
		}
		public EventData[] getEventsByPeriod(IdName Period, DateTime Start, DateTime Stop) 
		{
			return Server.getEventsByPeriod(Period.Id, Start, Stop);
		}
		public EventData[] getEventsByClass(IdName Class, DateTime Start, DateTime Stop)
		{
			return Server.getEventsByClass(Class.Id, Start, Stop);
		}
		public EventData[] getPrivateEvents(DateTime Start, DateTime Stop)
		{
			return Server.getPrivateEvents(Start, Stop);
		}
		#endregion
		#region completion
		public IdName[] getCampusNames()
		{
			return Server.getIdCampusNames();
		}
		public IdName[] getIdClassesNames()
		{
			return Server.getIdClassesNames();
		}
		public IdName[] getIdPromotionsNames()
		{
			return Server.getIdPromotionsNames();
		}
		public IdName[] getIdPeriodsNames()
		{
			return Server.getIdPeriodsNames();
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
