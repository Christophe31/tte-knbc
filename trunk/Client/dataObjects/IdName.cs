using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.BusinessLayer
{
	public partial class IdName : object, System.Runtime.Serialization.IExtensibleDataObject
	{
		public override string ToString()
		{
			return this.Name;
		}
	}
}
