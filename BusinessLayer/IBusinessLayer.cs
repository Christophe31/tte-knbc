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
		List<EventData> getEventsData_Campus(string CampusName);
		[OperationContract]
		List<EventData> getEventsData_University();
		[OperationContract]
		List<EventData> getEventsData_Periode(string PromoPeriodeName);
		[OperationContract]
		List<EventData> getEventsData_Class(string Campus);
		[OperationContract]
		List<EventData> getEventsData_Campus(string Campus);
		[OperationContract]
		List<EventData> getEventsData_Campus(string Campus);
		[OperationContract]
		List<EventData> getEventsData_Campus(string Campus);
	}
}
