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

		public static IdName IN(int? i,string s)
		{
			if (i == null)
			{ return null; }
			return new IdName((int)i,s);
		}
		public IdName()
		{
		}

		public IdName(int i, string s)
		{
			Id = i;
			Name = s;
		}
		public override string ToString()
		{
			return Name;
		}
	}
}
