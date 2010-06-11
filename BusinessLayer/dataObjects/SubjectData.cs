using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataAccessLayer;

namespace BusinessLayer
{

	[DataContract]
	public class SubjectData:IdName
	{
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