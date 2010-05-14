using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace BusinessLayer
{
	[DataContract]
	public class IdName
	{

		[DataMember]
		public int Id{get;set;}
		[DataMember]
		public string Name { get; set; }

		public static IdName IN(int i,string s)
		{
			return new IdName(i,s);
		}
		public IdName(int i,string s)
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
