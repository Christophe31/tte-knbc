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
		#region Lecture d'évènements
			[OperationContract]
			EventData[] getEventsByCampus(string CampusName, DateTime Start, DateTime Stop, DateTime LastUpdate);
			[OperationContract]
			EventData[] getEventsByUniversity(DateTime Start, DateTime Stop, DateTime LastUpdate);
			[OperationContract]
			EventData[] getEventsByPeriod(string PeriodName, DateTime Start, DateTime Stop, DateTime LastUpdate);
			[OperationContract]
			EventData[] getEventsByClass(string ClassName, DateTime Start, DateTime Stop, DateTime LastUpdate);
			[OperationContract]
			EventData[] getPrivateEvents(DateTime Start, DateTime Stop, DateTime LastUpdate);
		#endregion
		#region completion
			[OperationContract]
			string[] getCampusNames();
			[OperationContract]
			string[] getClassesNames();
			[OperationContract]
			string[] getPromotionsNames();
			[OperationContract]
			string[] getPeriodsNames();
			[OperationContract]
			string[] getSubjectsNames();
			[OperationContract]
			string[] getUsersNames();
		#endregion
		#region ecriture
			[OperationContract]
			string addUser(string UserName, string UserPassword, string UserClassName);
			[OperationContract]
			string addCampus(string CampusName);
			[OperationContract]
			string addClass(string ClassName, string CampusName, string PeriodeName);
			[OperationContract]
			string addPromotion(string PromotionName);
			[OperationContract]
			string addSubject(string SubjectName, int Hours);
			[OperationContract]
			string addPeriod(string PeriodName, string PromotionName, DateTime PeriodStart, DateTime PeriodEnd);
			[OperationContract]
			string grantNewRight(string UserName, int Type, string CampusName);
			[OperationContract]
			string addEvent(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string CampusName, string PeriodName, string SubjectName, string Type, string Place);
		#endregion
	}
}
