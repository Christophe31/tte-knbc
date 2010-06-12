using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace BusinessLayer
{
	/// <summary>
	/// Classe de représentation d'un évènement
	/// </summary>
	[DataContract]
	public class EventData:object
	{
		/// <summary>
		/// Enum indicant le planning parrent.
		/// </summary>
		[DataContract]
		public enum TypeEnum
        {
			/// <summary>
			/// Représente l'université
			/// </summary>
			[EnumMember]
            University,
			/// <summary>
			/// Campus
			/// </summary>
			[EnumMember]
            Campus,
			/// <summary>
			/// Période
			/// </summary>
			[EnumMember]
            Period,
			/// <summary>
			/// Classe
			/// </summary>
			[EnumMember]
            Class,
			/// <summary>
			/// Utilisateur
			/// </summary>
			[EnumMember]
            User
        }

		/// <summary>
		/// constructeur par défaut
		/// </summary>
		public EventData()
		{
		}


		/// <summary>
		/// Type du planning parent.
		/// </summary>
		[DataMember]
		public TypeEnum? Type { get; set; }

		/// <summary>
		/// Id et Nom du planning parent
		/// </summary>
		[DataMember]
		public IdName ParentPlanning { get; set; }

		/// <summary>
		/// Identifiant
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