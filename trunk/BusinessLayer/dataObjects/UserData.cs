using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataAccessLayer;

namespace BusinessLayer
{

	[DataContract]
	public class UserData:IdName
	{
		public UserData()
		{

		}


		/// <summary>
		/// nom de la classe de l'utilisateur
		/// </summary>
		[DataMember]
		public IdName Class { get; set; }
	
		/// <summary>
		/// Nom de l'utilisateur
		/// </summary>
		[DataMember]
		public string Login { get; set; }

		/// <summary>
		/// Mot de passe de l'utilisateur... acessible pour les objets recus, envoyé uniquement à null.
		/// </summary>
		[DataMember]
		public string Password { get; set; }
	}
}