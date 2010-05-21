using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.BusinessLayer;
using System.Threading;

namespace Client
{
	internal class CacheProcess
	{
		public BusinessServiceClient Server;
		public Tuple<bool,DateTime> ServerReachable;
		#region singleton
			static protected CacheProcess self;
			protected CacheProcess()
			{
				Server = new BusinessServiceClient();
				Server.ClientCredentials.UserName.UserName = "admin";
				Server.ClientCredentials.UserName.Password = "motdepasse";
				Server.Open();
				ServerReachable = new Tuple<bool, DateTime>(true, DateTime.Now);
				//this.Run();
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
				if(!Reactor.IsAlive)
					Reactor=new Thread((ThreadStart)this.LaunchReactor);
			}
			private void LaunchReactor()
			{
				while (true)
				{
					
					Thread.Sleep(2000);
				}
			}
		#endregion

	}
}
