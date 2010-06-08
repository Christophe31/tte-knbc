using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataModel;

namespace BusinessLayer
{

	[DataContract]
	public class ModalityData:IdName
	{
		public ModalityData()
		{
		}

		/// <summary>
		/// Lien vers le sujet
		/// </summary>
		[DataMember]
		public int SubjectId { get; set; }
		
		/// <summary>
		/// Id de la periode
		/// </summary>
		[DataMember]
		public int Hours { get; set; }
	}
}