using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.BusinessLayer;
using System.Threading;
using DDay.iCal;
using DDay.iCal.Serialization.iCalendar;

namespace Client
{
	internal class CacheProcess
	{
        public delegate EventData[] EventsGetter(int Id, DateTime Start, DateTime Stop, DateTime LastUpdate);
        public List<Tuple<EventsGetter,int,string>> ToDoListId;
        public BusinessServiceClient Server;
		public Tuple<bool,DateTime> ServerReachable;
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
				ServerReachable = new Tuple<bool, DateTime>(true, DateTime.Now);
				ToDoListId = new List<Tuple<EventsGetter, int, string>>();
				this.Run();
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
			Thread Reactor;
			public void Run()
			{
				if (Reactor==null)
					Reactor = new Thread((ThreadStart)this.LaunchReactor);
				if (!Reactor.IsAlive)
					Reactor.Start();
			}
			private void LaunchReactor()
			{	while (true)
				{
					if (ServerReachable.Item1 && (ServerReachable.Item2-DateTime.Now).Minutes>3)             
                        ServerReachable = new Tuple<bool, DateTime>(Server.State == System.ServiceModel.CommunicationState.Opened,DateTime.Now);
                    if (ToDoListId.Count>0)
                    
					Thread.Sleep(2000);
			}}
            public void RunToDoListId()
            {
                foreach (var e in ToDoListId)
                {
                    iCalendar iCal = new iCalendar();
                    foreach (var i in e.Item1(e.Item2, DateTime.MinValue, DateTime.MaxValue, DateTime.MinValue))
                    {
                        i.AddEventToCalendar(ref iCal);
                    }
                    iCal.AddProperty("LastUpdate", DateTime.Now.ToString());
                    
                    iCalendarSerializer serializer = new iCalendarSerializer(iCal);
                    serializer.Serialize(e.Item3+"\\"+e.Item2.ToString()+@".ics");

                }
            }
		#endregion

	}
}
