using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace BusinessLayer
{
	[DataContract]
	public class EventData:object
	{
		[DataContract]
		public enum TypeEnum
        {
			[EnumMember]
            University,
			[EnumMember]
            Campus,
			[EnumMember]
            Period,
			[EnumMember]
            Class,
			[EnumMember]
            User
        }

		public EventData()
		{
		}


		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public TypeEnum? Type { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public IdName ParentPlanning { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[DataMember]
		public int Id { get; set; }
		
		/// <summary>
		/// Date de Début de l'évènement.
		/// </summary>
		[DataMember]
		public DateTime Start { get; set; }

		/// <summary>
		/// Date de End de l'évènement
		/// </summary>
		[DataMember]
		public DateTime End { get; set; }

		/// <summary>
		/// Obligation D'afficher l'évènement dans le calandrier
		/// </summary>
		[DataMember]
		public bool Mandatory { get; set; }

		/// <summary>
		/// Salle ou Place où se déroullera l'évènement
		/// </summary>
		[DataMember]
		public string Place { get; set; }

		/// <summary>
		/// Name de la matière
		/// </summary>
		[DataMember]
		public IdName Subject { get; set; }

		/// <summary>
		/// Type d'évènement (Distanciel, Présentiel...)
		/// </summary>
		[DataMember]
		public IdName Modality { get; set; }

		/// <summary>
		/// Titre/Name de l'évènement
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Nom du prof
		/// </summary>
		[DataMember]
		public IdName Speaker { get; set; }
	}
}