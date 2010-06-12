using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BusinessLayer
{
	/// <summary>
	/// Classe contenant Id et nom
	/// </summary>
	[DataContract]
	public class IdName
	{
		/// <summary>
		/// Id de l'objet représenté
		/// </summary>
		[DataMember]
		public int Id{get;set;}
		/// <summary>
		/// nom de l'objet représenté
		/// </summary>
		[DataMember]
		public string Name { get; set; }


		/// <summary>
		/// Constructeur par défaut.
		/// </summary>
		public IdName()
		{
		}
	}
}
