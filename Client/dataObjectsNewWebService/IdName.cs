using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.BusinessService
{
	/// <summary>
	///  This class is a generic way to get an object,
	///  being able to get it and represent it for the user wit the name
	/// </summary>
	public partial class IdName : object, System.Runtime.Serialization.IExtensibleDataObject
	{
		/// <summary>
		/// This overriding allow us to put directly objects in comboboxes
		/// so it will display the name like that
		/// </summary>
		/// <returns>return the name of the IdName</returns>
		public override string ToString()
		{
			return this.Name;
		}
		public static implicit operator int(IdName idn) { return idn.Id; }
		public static implicit operator string(IdName idn) { return idn.Name; }
		public static implicit operator IdName(int idn) { return new IdName() {Id=idn ,Name=null}; }
		public static implicit operator IdName(string idn) { return new IdName() { Id = 0, Name = idn}; }
	}
}
