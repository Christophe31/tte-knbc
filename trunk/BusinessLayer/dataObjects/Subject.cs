using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataModel;

namespace BusinessLayer
{

	[DataContract]
	public class SubjectData
	{
		public SubjectData()
		{

		}
		/// <summary>
		/// Permet de créer un évènement sérialisable à partir d'une entitée évènement.
		/// </summary>
		/// <param name="EventEntity">Entrée à rendre sérialisable</param>
		public SubjectData(Subject SubjectEntity)
		{
			this.Id = SubjectEntity.Id;
			this.Name = SubjectEntity.Name;
			this.Modality = SubjectEntity.Modality;
			this.Hours = SubjectEntity.Hours;
		}
		public static SubjectData SD(Subject SubjectEntity)
		{
			return new SubjectData
			{
				Id = SubjectEntity.Id,
				Name = SubjectEntity.Name,
				Modality = SubjectEntity.Modality,
				Hours = SubjectEntity.Hours
			};
		}
		/// <sumSubjectName = SubjectName;mary>
		/// Id.
		/// </summary>
		[DataMember]
		public int Id { get; set; }

		/// <summary>
		/// Campus de la Subjecte
		/// </summary>
		[DataMember]
		public string Modality { get; set; }

		/// <summary>
		/// Nom de la Subjecte
		/// </summary>
		[DataMember]
		public string Name { get; set; }

		/// <summary>
		/// Id de la periode
		/// </summary>
		[DataMember]
		public int Hours { get; set; }
	}
}