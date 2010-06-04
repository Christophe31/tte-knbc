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
		public IdName Campus { get; set; }
	}
}