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

		public RoleData(IdName Campus,RoleType type)
		{
			this.Campus = Campus;
			this.Role = Role;
		}

		public static RoleData RD(IdName Campus, RoleType type)
		{
			return new RoleData(Campus, type);
		}

		[DataMember]
		public RoleType Role { get; set; }


		[DataMember]
		public IdName Campus { get; set; }
	}
}