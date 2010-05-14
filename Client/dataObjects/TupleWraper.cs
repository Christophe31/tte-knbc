using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
	public class TupleWraper
	{
		public int Id;
		public string Name;
		public TupleWraper(Tuple<int,string> t)
		{
			Id = t.Item1;
			Name = t.Item2;
		}
		public override string ToString()
		{
			return Name;
		}
	}
}
