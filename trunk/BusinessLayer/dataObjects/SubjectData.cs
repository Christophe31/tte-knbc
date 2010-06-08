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
		/// Campus de la Subjecte
		/// </summary>
		[DataMember]
		public string Modality { get; set; }

		[DataMember]
		public ModalityData[]  Modalities { get; set; }


		/// <summary>
		/// Id de la periode
		/// </summary>
		[DataMember]
		public int Hours { get; set; }
	}
}