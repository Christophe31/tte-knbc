﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataAccessLayer;

namespace BusinessLayer
{
	/// <summary>
	/// 
	/// </summary>
	[DataContract]
	public class RoleData
	{
		/// <summary>
		/// Différents rôles ou statuts des utilisateurs.
		/// </summary>
		[DataContract]
		public enum RoleType
		{
			/// <summary>
			/// 
			/// </summary>
			[EnumMember]
			Administrator,
			/// <summary>
			/// 
			/// </summary>
			[EnumMember]
			CampusManager,
			/// <summary>
			/// 
			/// </summary>
			[EnumMember]
			Speaker
		}
		/// <summary>
		/// Constructeur par défaut.
		/// </summary>
		public RoleData() 
		{ 
		}

		/// <summary>
		/// indique le type de rôle
		/// </summary>
		[DataMember]
		public RoleType Role { get; set; }

		/// <summary>
		/// indique l'id rôle
		/// </summary>
		[DataMember]
		public int Id { get; set; }

		/// <summary>
		/// pointe vers l'unniversté pour un admin, 
		/// vers un campus pour un campus mannager ou
		/// vaut null pour un speaker
		/// </summary>
		[DataMember]
		public int? TargetId { get; set; }
	}
}