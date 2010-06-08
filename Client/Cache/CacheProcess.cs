using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Client.BusinessLayer;
using System.Threading;
using System.Runtime.Serialization;
using System.ServiceModel.Description;
using System.ServiceModel;
using DDay.iCal;



namespace Client
{
	internal class CacheProcess
	{
		public BusinessServiceClient Server;
		public BusinessWebService.BusinessLayerClient ServerBL2;
		public BusinessWebService.RoleData[] UserRoles;
		public bool ServerReachable { get { return Server.State==System.ServiceModel.CommunicationState.Opened; } }
		new System.Runtime.Serialization.NetDataContractSerializer formatter = new System.Runtime.Serialization.NetDataContractSerializer();
		DDay.iCal.Serialization.iCalendar.iCalendarSerializer calSerializer = new DDay.iCal.Serialization.iCalendar.iCalendarSerializer();
		#region delegate
		public delegate EventData[] EventsGetterId(int Id, DateTime Start, DateTime Stop);
		public delegate EventData[] EventsGetter(DateTime Start, DateTime Stop);
		public delegate void CacheGetterId(EventsGetterId foo, IdName idn);
		public delegate void CacheGetter(EventsGetter foo, string name);
		#endregion
		#region singleton
			static protected CacheProcess self;
			protected CacheProcess()
			{
				ServerBL2 = new BusinessWebService.BusinessLayerClient();
				Server = new BusinessServiceClient();

				
				Server.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
				Server.ClientCredentials.UserName.UserName = "popi";
				Server.ClientCredentials.UserName.Password = "popi";

				ServerBL2.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode= System.ServiceModel.Security.X509CertificateValidationMode.None;
				ServerBL2.ClientCredentials.UserName.UserName = "popi";
				ServerBL2.ClientCredentials.UserName.Password = "popi";
				//Server.Open();
				ServerBL2.Open();
				UserRoles=ServerBL2.getUserRoles();
				ServerBL2.getEvents(8, DateTime.Now, DateTime.Now);
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
			public void RefreshCache(EventsGetterId eventGetter, IdName idn)
			{
				new CacheGetterId(refreshCache).BeginInvoke(eventGetter, idn, null,null);
			}
			public void RefreshCache(EventsGetter eventGetter, string name)
			{
				new CacheGetter(refreshCache).BeginInvoke(eventGetter, name, null, null);
			}

			private void refreshCache(EventsGetterId eventGetter,IdName idn)
            {
                iCalendar iCal = new iCalendar();
				EventData[] tmp = eventGetter(idn.Id, DateTime.MinValue, DateTime.MaxValue);
				foreach (var i in tmp )
				{
					i.AddEventToCalendar(ref iCal); 
				}
                iCal.AddProperty("X-LastUpdate", DateTime.Now.ToString());
				calSerializer.Serialize(iCal,fileNameFromIdName(idn));
			}

            public string fileNameFromIdName(IdName idn)
            {
                return "cache_" + idn.Name + "_" + idn.Id + @".ics";
            }

             [Obsolete]
			private void refreshCache(EventsGetter eventGetter, string name)
	        {
                iCalendar iCal = new iCalendar();
                foreach (var i in eventGetter( DateTime.MinValue, DateTime.MaxValue))
                {
                    i.AddEventToCalendar(ref iCal);
                }
                iCal.AddProperty("X-LastUpdate", DateTime.Now.ToString());
				calSerializer.Serialize(iCal,"cache__"+ name + @".ics");
             }
		#endregion
		
	}
}
