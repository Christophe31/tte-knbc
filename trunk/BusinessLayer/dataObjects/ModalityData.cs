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

		/// <summary>
		/// Id.
		/// </summary>
		[DataMember]
		public int Id { get; set; }

		/// <summary>
		/// Nom de la modalité
		/// </summary>
		[DataMember]
		public string Name { get; set; }

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