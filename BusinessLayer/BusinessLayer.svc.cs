using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BusinessLayer
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BusinessLayer" in code, svc and config file together.
	public class BusinessLayer : IBusinessLayer
	{
		#region IBusinessLayer
		public List<EventData> getEventsData_Campus(string CampusName)
		{
			return null;
		}
		public List<EventData> getEventsData_University()
		{
			return null;
		}
		public List<EventData> getEventsData_Periode(string PromoPeriodeName)
		{
			return null;
		}
		public List<EventData> getEventsData_Class(string ClassName, string CampusName, string PromoPeriodeName)
		{
			return null;
		}
		#endregion
	}
}
