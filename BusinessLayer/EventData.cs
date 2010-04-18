using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace BusinessLayer
{
	[DataContract]
	public class EventData
	{
		[DataMember]
		public DateTime Debut { get; set; }
		[DataMember]
		public DateTime Fin { get; set; }
		[DataMember]
		public bool Obligatoire{ get; set; }
		[DataMember]
		public string Nom { get; set; }



	}
}