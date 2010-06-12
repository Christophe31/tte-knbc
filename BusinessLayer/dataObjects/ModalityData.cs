using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataAccessLayer;

namespace BusinessLayer
{
	/// <summary>
	/// Classe représentant les modalités
	/// </summary>
	[DataContract]
	public class ModalityData:IdName
	{
		/// <summary>
		/// Constructeur par défaut.
		/// </summary>
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