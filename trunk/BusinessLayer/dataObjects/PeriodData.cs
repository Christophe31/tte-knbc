using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataAccessLayer;

namespace BusinessLayer
{
	/// <summary>
	/// Period
	/// </summary>
	[DataContract]
	public class PeriodData:IdName
	{
		public PeriodData()
		{

		}

		/// <summary>
		/// Promotion de la Periode
		/// </summary>
		[DataMember]
		public IdName Promotion { get; set; }


		/// <summary>
		/// Date de début de la periode
		/// </summary>
		[DataMember]
		public DateTime Start { get; set; }
		/// <summary>
		/// Date de fin de la periode
		/// </summary>
		[DataMember]
		public DateTime End { get; set; }
	}
}