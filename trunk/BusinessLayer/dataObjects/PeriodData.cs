using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using DataModel;

namespace BusinessLayer
{

	[DataContract]
	public class PeriodData:IdName
	{
		public PeriodData()
		{

		}
		/// <summary>
		/// Permet de créer un évènement sérialisable à partir d'une entitée évènement.
		/// </summary>
		/// <param name="EventEntity">Entrée à rendre sérialisable</param>
		public PeriodData(Period PeriodEntity, string PromotionName)
		{
			this.Id = PeriodEntity.Id;
			this.Name = PeriodEntity.Name;
			this.PromotionName = PromotionName;
			this.Start = PeriodEntity.Start;
			this.End = PeriodEntity.End;
		}

		public static PeriodData PD(Period PeriodEntity, string PromotionName)
		{
			return new PeriodData(PeriodEntity, PromotionName);
		}

		/// <summary>
		/// Promotion de la Periode
		/// </summary>
		[DataMember]
		public string PromotionName { get; set; }


		/// <summary>
		/// Date de début de la periode
		/// </summary>
		[DataMember]
		public DateTime Start { get; set; }
		/// <summary>
		/// Date de fin de la periode
		/// </summary>
		[DataMember]
		public DateTime End { get; set; }
	}
}