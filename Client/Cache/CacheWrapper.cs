using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.BusinessService;


namespace Client
{
    public class CacheWrapper
	{
		CacheProcess cacheProcess;
		public Client.BusinessService.BusinessLayerClient Server;
		public CacheWrapper()
		{
			cacheProcess = CacheProcess.Current;
			Server = cacheProcess.Server;
		}

		#region Lecture d'évènements
		public EventData[] getEvents(IdName Planning, DateTime Start, DateTime Stop)
		{
            if (System.IO.File.Exists(cacheProcess.fileNameFromIdName(Planning)))
            {
                var calendars = DDay.iCal.iCalendar.LoadFromFile(cacheProcess.fileNameFromIdName(Planning));
                var lastUpdate = DateTime.Parse(calendars.Select(p => p.Properties.Where(f => f.Key == "X-LASTUPDATE").FirstOrDefault()).Where(f=>f!=null).First().Value.ToString());
                if (cacheProcess.ServerReachable && !Server.isPlanningUpToDate(Planning, lastUpdate))
                {
                    cacheProcess.RefreshCache((CacheProcess.EventsGetterId)Server.getEvents, Planning);
                    return Server.getEvents(Planning, Start, Stop);
                }

                else
                {
                    return calendars.First().Events.Select(
                        p => EventData.CreateFromICalEvent(p)
                        ).ToArray();
                }
            }

            else
            {
                if (cacheProcess.ServerReachable)
                {
                    cacheProcess.RefreshCache((CacheProcess.EventsGetterId)Server.getEvents, Planning);
                    return Server.getEvents(Planning, Start, Stop);
                }
                else
                {
                    throw new Exception("No cache file, neither connexion to Web Service available");
                }
            }
		}
		#endregion
		#region completion

		public IdName[] getPlannings(EventData.TypeEnum type)
		{
			string fname = EventType.EventTypeNames[type]+"List.cache";

			if (cacheProcess.ServerReachable)
			{
				IdName[] t = Server.getPlannings(type);
				cacheProcess.WriteToFile(t, fname);
				return t;
			}
			if (System.IO.File.Exists(fname))
			{
				return cacheProcess.ReadFromFile(fname) as IdName[];
			}
			throw new Exception("No cache file, neither connexion to Web Service available");
	
		}
		public IdName[] getPromotions()
		{
			return Server.getPromotions();
		}
		public SubjectData[] getSubjects()
		{
			return Server.getSubjects();
		}
		/*
		public string[] getModalities()
		{
			return Server.getModalities();
		}
		*/
        public Dictionary<BusinessService.IdName, Dictionary<BusinessService.IdName, BusinessService.IdName[]>> getCampusPeriodClassTree()
		{
			return Server.getCampusPeriodClassTree();
		}
		#endregion
    }
}
