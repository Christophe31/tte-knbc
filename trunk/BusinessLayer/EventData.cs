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
		public EventData()
		{

		}
		/// <summary>
		/// Permet de créer un évènement sérialisable à partir d'une entitée évènement.
		/// </summary>
		/// <param name="EventEntity">Entrée à rendre sérialisable</param>
		public EventData(Event EventEntity, string NameSubject, string NameSpeaker)
		{
			this.Start = EventEntity.Start;
			this.End = EventEntity.End;
			this.Mandatory = EventEntity.Mandatory;
			this.Name = EventEntity.Name;
			this.Place = EventEntity.Place;
			this.Subject = NameSubject;
			this.Type = EventEntity.Type;
			this.Speaker = NameSpeaker;
		}
		public static EventData ED(Event EventEntity, string NameSubject, string NameSpeaker)
		{
			return new EventData {
				Start = EventEntity.Start,
				End = EventEntity.End,
				Name = EventEntity.Name,
				Place = EventEntity.Place,
				Subject = NameSubject,
				Mandatory = EventEntity.Mandatory,
				Type = EventEntity.Type,
				Speaker = NameSpeaker};
		}

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
		/// bligation D'afficher l'évènement dans le calandrier
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
		public string Subject { get; set; }

		/// <summary>
		/// Type d'évènement (Distanciel, Présentiel...)
		/// </summary>
		[DataMember]
		public string Type { get; set; }

		/// <summary>
		/// Titre/Name de l'évènement
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Nom du prof
		/// </summary>
		[DataMember]
		public string Speaker { get; set; }
	}
}