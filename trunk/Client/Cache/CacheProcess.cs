using System;
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
        public delegate EventData[] EventsGetterId(int Id, DateTime Start, DateTime Stop);
        public List<Tuple<EventsGetterId,IdName>> ToDoListId;
        public delegate EventData[] EventsGetter(DateTime Start, DateTime Stop);
        public List<Tuple<EventsGetter, string>> ToDoList;
        public BusinessServiceClient Server;
		public bool ServerReachable { get { return Server.State==System.ServiceModel.CommunicationState.Opened; } }
		Thread reactor;
		System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
		DDay.iCal.Serialization.iCalendar.iCalendarSerializer calSerializer = new DDay.iCal.Serialization.iCalendar.iCalendarSerializer();
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

				ToDoListId = new List<Tuple<EventsGetterId, IdName>>();
                ToDoList = new List<Tuple<EventsGetter, string>>();
                reactor=new Thread((ThreadStart)LaunchReactor);
				reactor.Start();
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

		#region Reactor
			private void LaunchReactor()
			{	while (true)
				{
					if (ToDoListId.Count > 0)
						new Thread((ThreadStart)RunToDoListId).Start();
					if (ToDoList.Count > 0)
						new Thread((ThreadStart)RunToDoList).Start();
					Thread.Sleep(2000);
			}}
		#endregion

		#region foos
			public void RunToDoListId()
            {
                foreach (var e in ToDoListId)
                {
                    iCalendar iCal = new iCalendar();
                    foreach (var i in e.Item1(e.Item2.Id, DateTime.MinValue, DateTime.MaxValue))
                    {
                        i.AddEventToCalendar(ref iCal);
                    }
                    iCal.AddProperty("LastUpdate", DateTime.Now.ToString());
					calSerializer.Serialize(iCal,"cache\\"+e.Item2.Name + "-" + e.Item2.Id + @".ics");
                }
			}
			private void WriteToFile(ISerializable obj, string fileName)
			{
				Stream str = File.OpenWrite(fileName);
				formatter.Serialize(str, obj);
				str.Close();
			}
            public void RunToDoList()
            {
                foreach (var e in ToDoList)
                {
                    iCalendar iCal = new iCalendar();
                    foreach (var i in e.Item1( DateTime.MinValue, DateTime.MaxValue))
                    {
                        i.AddEventToCalendar(ref iCal);
                    }
                    iCal.AddProperty("LastUpdate", DateTime.Now.ToString());
					calSerializer.Serialize(iCal,"cache\\"+ e.Item2 + @".ics");
                }
             }
		#endregion
		
	}
}
