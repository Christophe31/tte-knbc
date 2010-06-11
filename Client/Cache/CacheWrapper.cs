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
		public Client.BusinessService.BusinessLayerClient Server { get { return cacheProcess.Server;} }
		public bool ServerAvailable { get { return cacheProcess.ServerAvailable; } }
		public bool CacheAvailable { get { return cacheProcess.CacheAvailable; } }
		public UserData CurrentUser;
		public bool TryAutolog { 
			get { 
				if (cacheProcess.CurrentUser.Password == null) 
					return false;
				return cacheProcess.logToWebService(cacheProcess.CurrentUser.Login, cacheProcess.CurrentUser.Password);
			} 
		}
		public CacheWrapper()
		{
			cacheProcess = CacheProcess.Current;
			CurrentUser = cacheProcess.CurrentUser;

		}
		public bool logCacheProcess(string login, string password, bool savePassword)
		{
			var b = cacheProcess.logToWebService(login, password);
			if (b)
			{
				if(savePassword)
				{ cacheProcess.CurrentUser.Password = password;
				cacheProcess.CurrentUser.Login = login;
				};
				cacheProcess.WriteToFile(cacheProcess.CurrentUser, cacheProcess.otherFileNames["CurrentUser"]);
			}
			return b;
		}

		#region Lecture d'évènements
		public EventData[] getEvents(IdName Planning, DateTime Start, DateTime Stop)
		{
			string fname = cacheProcess.fileNameFromIdName(Planning);
			Tuple<DateTime, EventData[]> t;
			if (System.IO.File.Exists(fname) )
			{
				t=cacheProcess.ReadFromFile(fname) as Tuple<DateTime,EventData[]>;
				if (cacheProcess.ServerAvailable && !Server.isPlanningUpToDate(Planning, t.Item1))
				{
					t = new Tuple<DateTime,EventData[]>(DateTime.Now, Server.getEvents(Planning, Start, Stop));
					cacheProcess.RefreshCache(Planning);
					return t.Item2;
				}
				return (cacheProcess.ReadFromFile(fname) as Tuple<DateTime, EventData[]>).Item2.Where(ev => ev.Start < Stop && ev.End > Start).ToArray();
			}
			if (cacheProcess.ServerAvailable)
			{
				t = new Tuple<DateTime,EventData[]>(DateTime.Now,Server.getEvents(Planning, Start, Stop));
				cacheProcess.WriteToFile(t, fname);
				return t.Item2;
			}
			throw new Exception("No cache file, neither connexion to Web Service available");
		}
		#endregion
		#region completion

		public IdName[] getPlannings(EventData.TypeEnum type)
		{
			string fname = cacheProcess.otherFileNames[Enum.GetName(type.GetType(),type)];

			if (cacheProcess.ServerAvailable)
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
			string fname = cacheProcess.otherFileNames["Promotion"];
			if (cacheProcess.ServerAvailable)
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
			string fname = cacheProcess.otherFileNames["Subject"];

			if (cacheProcess.ServerAvailable)
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
			string fname = cacheProcess.otherFileNames["Modalities"];

			if (cacheProcess.ServerAvailable)
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
			string fname = cacheProcess.otherFileNames["CampusPeriodClassTree"];

			if (cacheProcess.ServerAvailable)
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
		#endregion
	}
}
