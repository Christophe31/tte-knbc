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
		public Client.BusinessWebService.BusinessLayerClient ServerBL2;
		public CacheWrapper()
		{
			cacheProcess = CacheProcess.Current;

			ServerBL2 = cacheProcess.ServerBL2;
			Server = cacheProcess.Server;
		}


		#region Lecture d'évènements
		public EventData[] getEventsByCampus(IdName Campus, DateTime Start, DateTime Stop)
		{
            if (false)//System.IO.File.Exists(cacheProcess.fileNameFromIdName(Campus)))
            {
                var calendars = DDay.iCal.iCalendar.LoadFromFile(cacheProcess.fileNameFromIdName(Campus));
                var lastUpdate = DateTime.Parse(calendars.Select(p => p.Properties.Where(f => f.Key == "X-LASTUPDATE").FirstOrDefault()).Where(f=>f!=null).First().Value.ToString());
                if (cacheProcess.ServerReachable && !Server.isUpToDateByCampus(Campus.Id, lastUpdate))
                {
                    cacheProcess.RefreshCache((CacheProcess.EventsGetterId)Server.getEventsByCampus, Campus);
                    return Server.getEventsByCampus(Campus.Id, Start, Stop);
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
                    cacheProcess.RefreshCache((CacheProcess.EventsGetterId)Server.getEventsByCampus, Campus);
                    return Server.getEventsByCampus(Campus.Id, Start, Stop);
                }
                else
                {
                    throw new Exception("No cache file, neither connexion to Web Service available");
                }
            }
		}
		public BusinessWebService.EventData[] getEventsByUniversity(DateTime Start, DateTime Stop)
		{
            return ServerBL2.getEvents(ServerBL2.getUniversity().Id, Start, Stop);
		}
		public EventData[] getEventsByPeriod(IdName Period, DateTime Start, DateTime Stop) 
        {
            if (false)//System.IO.File.Exists(cacheProcess.fileNameFromIdName(Period)))
            {
                var calendars = DDay.iCal.iCalendar.LoadFromFile(cacheProcess.fileNameFromIdName(Period));
                var lastUpdate =
                        DateTime.Parse(
                            calendars.Select(p => p.Properties.Where(f => f.Key == "X-LASTUPDATE").FirstOrDefault()).Where(f=>f!=null).First().Value.ToString()
                        );
                if (cacheProcess.ServerReachable && !Server.isUpToDateByPeriod(Period.Id, lastUpdate))
                {
                    cacheProcess.RefreshCache((CacheProcess.EventsGetterId)Server.getEventsByPeriod, Period);
                    return Server.getEventsByPeriod(Period.Id, Start, Stop);
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
                    cacheProcess.RefreshCache((CacheProcess.EventsGetterId)Server.getEventsByPeriod, Period);
                    return Server.getEventsByPeriod(Period.Id, Start, Stop);
                }
                else
                {
                    throw new Exception("No cache file, neither connexion to Web Service available");
                }
            }
		}
		public EventData[] getEventsByClass(IdName Class, DateTime Start, DateTime Stop)
		{
            if (false)//System.IO.File.Exists(cacheProcess.fileNameFromIdName(Class)))
            {
                var calendars = DDay.iCal.iCalendar.LoadFromFile(cacheProcess.fileNameFromIdName(Class));
                var lastUpdate =
                        DateTime.Parse(
                            calendars.Select(p => p.Properties.Where(f => f.Key == "X-LASTUPDATE").First()).First().Value.ToString()
                        );
                if (cacheProcess.ServerReachable && !Server.isUpToDateByClass(Class.Id, lastUpdate))
                {
                    cacheProcess.RefreshCache((CacheProcess.EventsGetterId)Server.getEventsByClass, Class);
                    return Server.getEventsByClass(Class.Id, Start, Stop);
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
                    cacheProcess.RefreshCache((CacheProcess.EventsGetterId)Server.getEventsByClass, Class);
                    return Server.getEventsByClass(Class.Id, Start, Stop);
                }
                else
                {
                    throw new Exception("No cache file, neither connexion to Web Service available");
                }
            }
		}
		public EventData[] getPrivateEvents(DateTime Start, DateTime Stop)
		{
			return Server.getPrivateEvents(Start, Stop);
		}
		#endregion
		#region completion
		public IdName[] getCampusNames()
		{
			string fname="getCampusNames.cache";
			if (cacheProcess.ServerReachable)
			{
				IdName[] t = Server.getIdCampusNames();
				cacheProcess.WriteToFile(t,fname );
				return t;
			}
			if (System.IO.File.Exists(fname))
			{
				return cacheProcess.ReadFromFile(fname) as IdName[];
			}
			throw new Exception("No cache file, neither connexion to Web Service available");

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
        public Dictionary<BusinessWebService.IdName, Dictionary<BusinessWebService.IdName, BusinessWebService.IdName[]>> getCampusPeriodClassTree()
		{
			return ServerBL2.getCampusPeriodClassTree();
		}
		#endregion

    }
}
