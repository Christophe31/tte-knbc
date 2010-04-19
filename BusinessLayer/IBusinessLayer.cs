using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BusinessLayer
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBusinessLayer" in both code and config file together.
	[ServiceContract]
	public interface IBusinessLayer
	{
		[OperationContract]
		List<EventData> getEventsData_Campus(string CampusName, DateTime Start, DateTime Stop, DateTime LastUpdate=DateTime.MinValue);

		[OperationContract]
		List<EventData> getEventsData_University(DateTime Start, DateTime Stop, DateTime LastUpdate = DateTime.MinValue);
		[OperationContract]
		List<EventData> getEventsData_Periode(string PromoPeriodeName, DateTime Start, DateTime Stop, DateTime LastUpdate = DateTime.MinValue);
		[OperationContract]
		List<EventData> getEventsData_Class(string ClassName, string CampusName, string PromoPeriodeName, DateTime Start, DateTime Stop, DateTime LastUpdate = DateTime.MinValue);
	}
}
