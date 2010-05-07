using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client.BusinessLayer;


namespace Client
{
	internal class CacheProcess
	{
		#region singleton
			static protected CacheProcess self;
			protected CacheProcess()
			{
				Server = new BusinessServiceClient();
			}
			static public CacheProcess getCacheProcess()
			{
				if (self != null)
					return self;
				self = new CacheProcess();
				return self;
			}
		#endregion
		public BusinessServiceClient Server;

	}
}
