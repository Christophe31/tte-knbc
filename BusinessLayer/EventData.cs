using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataModel;

namespace BusinessLayer
{

	[DataContract]
	public class EventData
	{
		/// <summary>
		/// Permet de créer un évènement sérialisable à partir d'une entitée évènement.
		/// </summary>
		/// <param name="EventEntity">Entrée à rendre sérialisable</param>
		public EventData(Evenement EventEntity)
		{
			this.Debut = EventEntity.Debut;
			this.Fin = EventEntity.Fin;
			this.Obligatoire = EventEntity.Obligatoire;
			this.Nom = EventEntity.Nom;
			this.Lieu = EventEntity.Lieu;
			this.Matiere = EventEntity.Matiere.Nom;
			this.Type = EventEntity.Type;
		}

		/// <summary>
		/// Date de Début de l'évènement.
		/// </summary>
		[DataMember]
		public DateTime Debut { get; set; }

		/// <summary>
		/// Date de fin de l'évènement
		/// </summary>
		[DataMember]
		public DateTime Fin { get; set; }

		/// <summary>
		/// bligation D'afficher l'évènement dans le calandrier
		/// </summary>
		[DataMember]
		public bool Obligatoire { get; set; }

		/// <summary>
		/// Salle ou lieu où se déroullera l'évènement
		/// </summary>
		[DataMember]
		public string Lieu { get; set; }

		/// <summary>
		/// Nom de la matière
		/// </summary>
		[DataMember]
		public string Matiere { get; set; }

		/// <summary>
		/// Type d'évènement (Distanciel, Présentiel...)
		/// </summary>
		[DataMember]
		public string Type { get; set; }

		/// <summary>
		/// Titre/nom de l'évènement
		/// </summary>
		[DataMember]
		public string Nom { get; set; }
	}
}