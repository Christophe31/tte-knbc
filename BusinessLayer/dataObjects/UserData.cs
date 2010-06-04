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
		public UserData(User UserEntity, IdName Class)
		{
			this.Id = UserEntity.Id;
			this.Name = UserEntity.Name;
			this.Class = Class;
			this.Password = null;
		}

		public static UserData UD(User UserEntity, IdName Class)
		{
			return new UserData(UserEntity, Class);
		}
		public static UserData UD(int Id , string Name, IdName Class)
		{
			return new UserData() 
			{
				Id=Id, 
				Class=Class,
				Name=Name
			};
		}

		/// <sumClassName = ClassName;mary>
		/// Date de Début de l'évènement.
		/// </summary>
		[DataMember]
		public int Id { get; set; }

		/// <summary>
		/// nom de la classe de l'utilisateur
		/// </summary>
		[DataMember]
		public IdName Class { get; set; }

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