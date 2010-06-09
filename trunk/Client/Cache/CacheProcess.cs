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
		public bool ServerReachable 
		{
			get 
			{
				if (Server!=null)
					return Server.State==System.ServiceModel.CommunicationState.Opened;
				return false;
			} 
		}
		System.Runtime.Serialization.NetDataContractSerializer formatter = new System.Runtime.Serialization.NetDataContractSerializer();
		DDay.iCal.Serialization.iCalendar.iCalendarSerializer calSerializer = new DDay.iCal.Serialization.iCalendar.iCalendarSerializer();
		#region delegate
		public delegate void CacheGetter(IdName idn);
		#endregion
		#region singleton
			static protected CacheProcess self;
			protected CacheProcess()
			{
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

		#region foos
			public void WriteToFile(object obj, string fileName)
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
				new CacheGetter(refreshCache).BeginInvoke(idn,null,null);
			}

		private void refreshCache(IdName idn)
            {
                iCalendar iCal = new iCalendar();
				EventData[] tmp = Server.getEvents(idn.Id, DateTime.MinValue, DateTime.MaxValue);
				
			}

            public string fileNameFromIdName(IdName idn)
            {
                return "cache_" + idn.Name + "_" + idn.Id + @".ics";
            }
		#endregion
		
	}
}
