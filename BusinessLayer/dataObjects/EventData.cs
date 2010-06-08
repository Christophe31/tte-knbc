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
		/// Permet de créer un évènement sérialisable à partir d'une entitée évènement.
		/// </summary>
		/// <param name="EventEntity">Entrée à rendre sérialisable</param>
		public EventData(Event EventEntity, string Subject,string Modality, string Speaker, TypeEnum? aType)
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
		public static EventData ED(Event EventEntity, string Subject, string Modality, string Speaker, TypeEnum aType)
		{
			return new EventData(EventEntity, Subject, Modality, Speaker, aType);
		}



		public EventData(DataAccessLayer.Event EventEntity, string Subject, string Modality, string Speaker, TypeEnum? aType)
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
		public static EventData ED(DataAccessLayer.Event EventEntity, string Subject, string Modality, string Speaker,int? atype)
		{
			
			return new EventData(EventEntity, Subject, Modality, Speaker, 
				atype==1? TypeEnum.University:
					atype==2?TypeEnum.Campus:
						atype==3?TypeEnum.Class:
							atype==4?TypeEnum.Period:
								TypeEnum.User);
		}

		[DataMember]
		public TypeEnum? Type { get; set; }

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
		public string Subject { get; set; }

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
		public string Speaker { get; set; }
	}
}