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
		public bool ServerAvailable{get{return cacheProcess.ServerReachable;}}
		public UserData CurrentUser;
		public CacheWrapper()
		{
			cacheProcess = CacheProcess.Current;
			Server = cacheProcess.Server;
			CurrentUser = cacheProcess.CurrentUser;
		}
		public bool logCacheProcess(string login, string password)
		{
			return cacheProcess.logToWebService(login, password);
		}
 
		#region Lecture d'évènements
		public EventData[] getEvents(IdName Planning, DateTime Start, DateTime Stop)
		{
			string fname = Planning + "-" + Planning.Id.ToString() + ".cache";
			string lufname = Planning + "-" + Planning.Id.ToString() + ".lucache";
			if (System.IO.File.Exists(fname) && System.IO.File.Exists(lufname))
			{

				if (cacheProcess.ServerReachable && !Server.isPlanningUpToDate(Planning, ((DateTime)cacheProcess.ReadFromFile(lufname))))
				{
					EventData[] t = Server.getEvents(Planning, Start, Stop);
					cacheProcess.RefreshCache(Planning);
					cacheProcess.WriteToFile(DateTime.Now, lufname);
					return t;
				}
				return cacheProcess.ReadFromFile(fname) as EventData[];
			}
			if (cacheProcess.ServerReachable )
			{
				EventData[] t = Server.getEvents(Planning, Start, Stop);
				cacheProcess.WriteToFile(t, fname);
				return t;
			}
			throw new Exception("No cache file, neither connexion to Web Service available");
/*            if (System.IO.File.Exists(cacheProcess.fileNameFromIdName(Planning)))
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
		*/}
		#endregion
		#region completion

		public IdName[] getPlannings(EventData.TypeEnum type)
		{
			string fname = Enum.GetName(typeof(EventData.TypeEnum) ,type)+"List.cache";

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
			string fname = "Promotion" + "List.cache";

			if (cacheProcess.ServerReachable)
			{
				IdName[] t = Server.getPromotions();
				cacheProcess.WriteToFile(t, fname);
				return t;
			}
			if (System.IO.File.Exists(fname))
			{
				return cacheProcess.ReadFromFile(fname) as IdName[];
			}
			throw new Exception("No cache file, neither connexion to Web Service available");
		}

		public SubjectData[] getSubjects()
		{
			string fname = "Subject" + "List.cache";

			if (cacheProcess.ServerReachable)
			{
				SubjectData[] t = Server.getSubjects();
				cacheProcess.WriteToFile(t, fname);
				return t;
			}
			if (System.IO.File.Exists(fname))
			{
				return cacheProcess.ReadFromFile(fname) as SubjectData[];
			}
			throw new Exception("No cache file, neither connexion to Web Service available");
		}
		public ModalityData[] getModalities()
		{
			string fname = "Modalities" + "List.cache";

			if (cacheProcess.ServerReachable)
			{
				ModalityData[] t = Server.getModalities();
				cacheProcess.WriteToFile(t, fname);
				return t;
			}
			if (System.IO.File.Exists(fname))
			{
				return cacheProcess.ReadFromFile(fname) as ModalityData[];
			}
			throw new Exception("No cache file, neither connexion to Web Service available");
		}

        public Dictionary<IdName, Dictionary<IdName, IdName[]>> getCampusPeriodClassTree()
		{
			string fname = "CampusPeriodClassTree" + ".cache";

			if (cacheProcess.ServerReachable)
			{
				Dictionary<IdName, Dictionary<IdName, IdName[]>> t = Server.getCampusPeriodClassTree();
				cacheProcess.WriteToFile(t, fname);
				return t;
			}
			if (System.IO.File.Exists(fname))
			{
				return cacheProcess.ReadFromFile(fname) as Dictionary<IdName, Dictionary<IdName, IdName[]>>;
			}
			throw new Exception("No cache file, neither connexion to Web Service available");
		}
		public IdName getUniversity()
		{
			string fname = "University" + ".cache";

			if (cacheProcess.ServerReachable)
			{
				IdName t = Server.getUniversity();
				cacheProcess.WriteToFile(t, fname);
				return t;
			}
			if (System.IO.File.Exists(fname))
			{
				return cacheProcess.ReadFromFile(fname) as IdName;
			}
			throw new Exception("No cache file, neither connexion to Web Service available");
		}
		#endregion
	}
}
