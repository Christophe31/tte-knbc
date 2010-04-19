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
		public EventData(Evenement e)
		{
			this.Debut = e.Debut;
			this.Fin = e.Fin;
			this.Obligatoire = e.Obligatoire;
			this.Nom = e.Nom;
			this.Lieu = e.Lieu;
			this.Matiere = e.Matiere.Nom;
			this.Type = e.Type;
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