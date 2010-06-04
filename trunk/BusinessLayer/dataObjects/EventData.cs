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
		public enum TypeEnum
        {
            University,
            Campus,
            Period,
            Class,
            User
        }

		public EventData()
		{

		}
		/// <summary>
		/// Permet de créer un évènement sérialisable à partir d'une entitée évènement.
		/// </summary>
		/// <param name="EventEntity">Entrée à rendre sérialisable</param>
		public EventData(Event EventEntity, IdName Subject,string Modality, IdName Speaker, TypeEnum aType)
		{
			this.Id = EventEntity.Id;
			this.Start = EventEntity.Start;
			this.End = EventEntity.End;
			this.Mandatory = EventEntity.Mandatory;
			this.Name = EventEntity.Name;
			this.Place = EventEntity.Place;
			this.Subject = Subject;
			this.Modality = Modality;
			this.Speaker = Speaker;
			this.Type = aType;
		}

		/// <summary>
		/// pour économiser le mot clef new dans les requetes déjà longues
		/// </summary>
		/// <returns>Nouvelle instance</returns>
		public static EventData ED(Event EventEntity, IdName Subject, string Modality, IdName Speaker, TypeEnum aType)
		{
			return new EventData (EventEntity, Subject, Modality, Speaker, aType);
		}

		[DataMember]
		public TypeEnum Type { get; set; }

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
		public IdName Subject { get; set; }

		/// <summary>
		/// Type d'évènement (Distanciel, Présentiel...)
		/// </summary>
		[DataMember]
		public string Modality { get; set; }

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