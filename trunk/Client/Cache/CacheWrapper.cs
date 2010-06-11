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
		public CacheWrapper()
		{
			cacheProcess = CacheProcess.Current;
			CurrentUser = cacheProcess.CurrentUser;
		}
		public bool logCacheProcess(string login, string password)
		{
			var b = cacheProcess.logToWebService(login, password);
			if (b)
			{
				cacheProcess.writeToFile.BeginInvoke(cacheProcess.CurrentUser, cacheProcess.otherFileNames["CurrentUser"], null, null);
			}
			return b;
		}


		#region Lecture d'évènements
		public EventData[] getEvents(IdName Planning, DateTime Start, DateTime Stop)
		{
			string fname = cacheProcess.fileNameFromIdName(Planning);
			string lufname = cacheProcess.fileNameFromIdName(Planning) + ".lu";
			if (System.IO.File.Exists(fname) && System.IO.File.Exists(lufname))
			{

				if (cacheProcess.ServerAvailable && !Server.isPlanningUpToDate(Planning, ((DateTime)cacheProcess.ReadFromFile(lufname))))
				{
					EventData[] t = Server.getEvents(Planning, Start, Stop);
					cacheProcess.RefreshCache(Planning);
					cacheProcess.writeToFile.BeginInvoke(DateTime.Now, lufname,null,null);
					return t;
				}
				return cacheProcess.ReadFromFile(fname) as EventData[];
			}
			if (cacheProcess.ServerAvailable)
			{
				EventData[] t = Server.getEvents(Planning, Start, Stop);
				cacheProcess.writeToFile.BeginInvoke(t, fname,null,null);
				return t;
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
				cacheProcess.writeToFile.BeginInvoke(t, fname,null,null);
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
				cacheProcess.writeToFile.BeginInvoke(t, fname,null,null);
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
				cacheProcess.writeToFile.BeginInvoke(t, fname,null,null);
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
				cacheProcess.writeToFile.BeginInvoke(t, fname,null, null);
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
				cacheProcess.writeToFile.BeginInvoke(t, fname,null,null);
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
