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
			EventData[] getEventsByPeriode(string PromoPeriodeName, DateTime Start, DateTime Stop, DateTime LastUpdate);
			[OperationContract]
			EventData[] getEventsByClass(string ClassName, string CampusName, string PromoPeriodeName, DateTime Start, DateTime Stop, DateTime LastUpdate);
		#endregion
		#region completion
			string[] getCampusNames();
			string[] getClassesNames();
			string[] getPromotionsNames();
			string[] getPromoPeriodeNames();
		#endregion
		#region ecriture
			string addUser(string UserName, string UserPassword, string UserCampusName);
			string addCampus(string CampusName);
			string addClasse(string ClassName, string ClassCampusName, string ClassPeriode);
			string addPromotion(string UserName, string UserPassword, string UserCampusName);
			string addMatiere(string UserName, string UserPassword, string UserCampusName);
			string addPeriode(string UserName, string UserPassword, string UserCampusName);
		#endregion
	}
}
