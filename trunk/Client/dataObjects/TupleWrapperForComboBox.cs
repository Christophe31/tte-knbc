using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
	public class TupleWrapperForComboBox
	{
		public int Id;
		public string Name;
		public TupleWrapperForComboBox(Tuple<int,string> t)
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
