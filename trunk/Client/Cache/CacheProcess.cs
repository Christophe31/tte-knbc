using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Client.BusinessService;
using System.Threading;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.ServiceModel;
using DDay.iCal;
using System.ServiceModel.Security;

namespace Client
{
	internal class CacheProcess
	{
		public BusinessService.BusinessLayerClient Server;
		public BusinessService.UserData CurrentUser;
		public delegate void CacheGetter(IdName idn);
		public delegate void FileWriter(object obj, string fileName);
		public FileWriter writeToFile;
		public bool ServerAvailable
		{
			get
			{
				if (Server != null)
					return Server.State == System.ServiceModel.CommunicationState.Opened;
				return false;
			}
		}
		public bool CacheAvailable { get { return (new string[] {"CampusPeriodClassTree","CurrentUser", "University", "Campus", "Period"}).All(kv => File.Exists(otherFileNames[kv])); } }
		System.Runtime.Serialization.NetDataContractSerializer formatter = new System.Runtime.Serialization.NetDataContractSerializer();
		#region singleton
		static protected CacheProcess self;
		protected CacheProcess()
		{
			writeToFile = WriteToFile;
			initCacheVars();
		}

		public bool logToWebService(string login, string password)
		{
			try
			{
				Server = new BusinessLayerClient();
				Server.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
				Server.ClientCredentials.UserName.UserName = login;
				Server.ClientCredentials.UserName.Password = password;
				Server.Open();
				CurrentUser = Server.getUserData();
				CurrentUser.Password = password;
				return true;
			}
			catch (MessageSecurityException)
			{
				return false;
			}
		}

		static public CacheProcess Current
		{
			get
			{
				if (self != null)
					return self;
				self = new CacheProcess();
				return self;
			}
		}
		#endregion

		#region File Naming Region

		string extention = ".cache";
		public Dictionary<string, string> otherFileNames;
		private void initCacheVars()
		{
			otherFileNames = new Dictionary<string, string> 
				{
					{"Promotion"				,"PromotionList"+extention},
					{"Subject"					,"SubjectList"+extention},
					{"Modality"					,"ModalityList"+extention},
					{"CampusPeriodClassTree"	,"CampusPeriodClassTree"+extention},
					{"CurrentUser"				,"CurrentUser"+extention}
				};
			foreach (var s in EventType.EventTypeNames.Select(kv => kv.Key))
			{
				otherFileNames.Add(Enum.GetName(s.GetType(), s), Enum.GetName(s.GetType(), s) + "List" + extention);
			}
		}

		public string fileNameFromIdName(IdName idn)
		{
			return idn.Name + "_" + idn.Id.ToString() + extention;
		}

		#endregion

		#region foos
		private void WriteToFile(object obj, string fileName)
		{
			Stream str = File.OpenWrite(fileName);
			formatter.Serialize(str, obj);
			str.Close();
		}
		public object ReadFromFile(string fileName)
		{
			Stream str = File.OpenRead(fileName);
			object o = formatter.ReadObject(str);
			return o;
		}
		public void RefreshCache(IdName idn)
		{
			((CacheGetter)refreshCache).BeginInvoke(idn, null, null);
		}

		private void refreshCache(IdName idn)
		{
			WriteToFile(Server.getEvents(idn.Id, DateTime.MinValue, DateTime.MaxValue), fileNameFromIdName(idn));
		}
		#endregion
	}
}
