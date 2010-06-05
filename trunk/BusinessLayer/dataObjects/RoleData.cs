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
		/// <summary>
		/// Différents rôles ou statuts des utilisateurs.
		/// </summary>
		[DataContract]
		public enum RoleType
		{
			[EnumMember]
			Administrator,
			[EnumMember]
			CampusManager,
			[EnumMember]
			Speaker
		}

		public RoleData() 
		{ 
		}
		
		public RoleData(int? TargetId,RoleType type)
		{
			this.TargetId = TargetId;
			this.Role = Role;
		}

		public static RoleData RD(int? TargetId, RoleType type)
		{
			return new RoleData(TargetId, type);
		}

		/// <summary>
		/// indique le type de rôle
		/// </summary>
		[DataMember]
		public RoleType Role { get; set; }

		/// <summary>
		/// pointe vers l'unniversté pour un admin, 
		/// vers un campus pour un campus mannager ou
		/// vaut null pour un speaker
		/// </summary>
		[DataMember]
		public int? TargetId { get; set; }
	}
}