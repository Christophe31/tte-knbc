﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Client.BusinessLayer;
using System.Threading;
using System.Runtime.Serialization;
using DDay.iCal;


namespace Client
{
	internal class CacheProcess
	{
		public BusinessServiceClient Server;
		public bool ServerReachable { get { return Server.State==System.ServiceModel.CommunicationState.Opened; } }
		System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
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
				Server = new BusinessServiceClient();
				//Server.ClientCredentials.ServiceCertificate.SetDefaultCertificate();
				Server.ClientCredentials.UserName.UserName = "admin";
				Server.ClientCredentials.UserName.Password = "motdepasse";
				Server.ChannelFactory.Credentials.UserName.UserName = "admin";
				Server.ChannelFactory.Credentials.UserName.Password = "motdepasse";
				Server.Open();
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
			private void WriteToFile(ISerializable obj, string fileName)
			{
				Stream str = File.OpenWrite(fileName);
				formatter.Serialize(str, obj);
				str.Close();
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
				EventData[]tmp = eventGetter(idn.Id, DateTime.MinValue, DateTime.MaxValue);
				foreach (var i in tmp )
				{
					i.AddEventToCalendar(ref iCal); 
				}
                iCal.AddProperty("X-LastUpdate", DateTime.Now.ToString());
				calSerializer.Serialize(iCal,"cache__"+idn.Name + "-" + idn.Id + @".ics");
			}

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
