using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataModel;

namespace BusinessLayer
{

	[DataContract]
	public class UserData
	{
		public UserData()
		{

		}
		/// <summary>
		/// Permet de créer un évènement sérialisable à partir d'une entitée évènement.
		/// </summary>
		/// <param name="EventEntity">Entrée à rendre sérialisable</param>
		public UserData(User UserEntity, string CampusName, string ClassName)
		{
			this.Id = UserEntity.Id;
			this.Name = UserEntity.Name;
			this.CampusName = CampusName;
			this.ClassName = ClassName;
		}
		public static UserData ED(Event UserEntity, string CampusName, string ClassName)
		{
			return new UserData
			{
				Id = UserEntity.Id,
				Name = UserEntity.Name,
				CampusName = CampusName,
				ClassName = ClassName
			};
		}
		/// <sumClassName = ClassName;mary>
		/// Date de Début de l'évènement.
		/// </summary>
		[DataMember]
		public int Id { get; set; }

		/// <summary>
		/// Date de End de l'évènement
		/// </summary>
		[DataMember]
		public string CampusName { get; set; }

		/// <summary>
		/// bligation D'afficher l'évènement dans le calandrier
		/// </summary>
		[DataMember]
		public string ClassName { get; set; }

		/// <summary>
		/// Salle ou Place où se déroullera l'évènement
		/// </summary>
		[DataMember]
		public string Name { get; set; }
	}
}