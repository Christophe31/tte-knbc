using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataModel;

namespace BusinessLayer
{

	[DataContract]
	public class RoleData
	{

		public enum RoleType
		{
			Administrator,
			CampusManager,
			Speaker
		}

		public RoleData()
		{

		}

		public static RoleData RD()
		{
			return new RoleData();
		}

		[DataMember]
		public RoleType Role { get; set; }


		[DataMember]
		public int? CampusId { get; set; }
	}
}