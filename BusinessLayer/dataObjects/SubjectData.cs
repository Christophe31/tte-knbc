using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataModel;

namespace BusinessLayer
{

	[DataContract]
	public class SubjectData:IdName
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
			return new SubjectData(SubjectEntity);
		}

		/// <summary>
		/// Campus de la Subjecte
		/// </summary>
		[DataMember]
		public string Modality { get; set; }

		[DataMember]
		public ModalityData[]  Modalities { get; set; }


		/// <summary>
		/// Id de la periode
		/// </summary>
		[DataMember]
		public int Hours { get; set; }
	}
}