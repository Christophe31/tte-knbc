using System;
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
	public class SubjectData:IdName
	{
		/// <summary>
		/// Default Constructor
		/// </summary>
		public SubjectData()
		{

		}

		/// <summary>
		/// Modalités pédagogiques.
		/// </summary>
		[DataMember]
		public ModalityData[]  Modalities { get; set; }

	}
}