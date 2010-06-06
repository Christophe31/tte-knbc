using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataModel;

namespace BusinessLayer
{

	[DataContract]
	public class ModalityData
	{
		public ModalityData()
		{
		}

		/// <sumSubjectName = SubjectName;mary>
		/// Id.
		/// </summary>
		[DataMember]
		public int Id { get; set; }

		/// <summary>
		/// Nom de la Subjecte
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Id de la periode
		/// </summary>
		[DataMember]
		public int Hours { get; set; }
	}
}