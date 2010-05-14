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
		public UserData(User UserEntity, string CampusName, int ClassId)
		{
			this.Id = UserEntity.Id;
			this.Name = UserEntity.Name;
			this.CampusName = CampusName;
			this.ClassId = ClassId;
			this.Password = null;
		}
		public static UserData UD(User UserEntity, string CampusName, int ClassId)
		{
			return new UserData( UserEntity, CampusName, ClassId);
		}
		/// <sumClassName = ClassName;mary>
		/// Date de Début de l'évènement.
		/// </summary>
		[DataMember]
		public int Id { get; set; }

		/// <summary>
		/// Campus de l'utilisateur
		/// </summary>
		[DataMember]
		public string CampusName { get; set; }

		/// <summary>
		/// nom de la classe de l'utilisateur
		/// </summary>
		[DataMember]
		public int ClassId { get; set; }

		/// <summary>
		/// Nom de l'utilisateur
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Mot de passe de l'utilisateur... acessible pour les objets recus, envoyé uniquement à null.
		/// </summary>
		[DataMember]
		public string Password { get; set; }
	}
}