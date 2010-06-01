using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataModel;

namespace BusinessLayer
{

	[DataContract]
	public class ClassData
	{
		public ClassData()
		{

		}
		/// <summary>
		/// Permet de créer un évènement sérialisable à partir d'une entitée évènement.
		/// </summary>
		/// <param name="EventEntity">Entrée à rendre sérialisable</param>
		public ClassData(Class ClassEntity, IdName Campus, IdName Period)
		{
			this.Id = ClassEntity.Id;
			this.Name = ClassEntity.Name;
			this.Campus = Campus;
			this.Period = Period;
		}
		public static ClassData CD(Class ClassEntity, IdName Campus, IdName Period)
		{
			return new ClassData(ClassEntity, Campus, Period);
		}
		/// <sumClassName = ClassName;mary>
		/// Id.
		/// </summary>
		[DataMember]
		public int Id { get; set; }

		/// <summary>
		/// Campus de la classe
		/// </summary>
		[DataMember]
		public IdName Campus { get; set; }

		/// <summary>
		/// Nom de la classe
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Id de la periode
		/// </summary>
		[DataMember]
		public IdName Period { get; set; }
	}
}