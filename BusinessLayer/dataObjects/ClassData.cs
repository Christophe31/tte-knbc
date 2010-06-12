using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataAccessLayer;

namespace BusinessLayer
{
	/// <summary>
	/// class representation
	/// </summary>
	[DataContract]
	public class ClassData:IdName
	{
		/// <summary>
		/// Contsructeur par défaut.
		/// </summary>
		public ClassData()
		{
		}

		/// <summary>
		/// Campus de la classe
		/// </summary>
		[DataMember]
		public IdName Campus { get; set; }

		/// <summary>
		/// Id de la periode
		/// </summary>
		[DataMember]
		public IdName Period { get; set; }
	}
}