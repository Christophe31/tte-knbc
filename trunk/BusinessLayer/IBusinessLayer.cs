using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
	[ServiceContract]
	interface IBusinessLayer
	{
		[OperationContract]
		RoleData[] getUserRoles();
		
		#region Lecture d'évènements
			[OperationContract]
			EventData[] getEvents(int Planning, DateTime Start, DateTime Stop);
			[OperationContract]
			EventData[] getSpeakerEvents(int ID, DateTime Start, DateTime Stop);
			[OperationContract]
			bool isPlanningUpToDate(int Planning, DateTime LastUpdate);
		#endregion
		#region Identified completion
			[OperationContract]
			IdName[] getPlannings(EventData.TypeEnum Type);
			[OperationContract]
			IdName getUniversity();
			[OperationContract]
			IdName[] getUsers();
			[OperationContract]
			IdName[] getPromotions();
			[OperationContract]
			SubjectData[] getSubjects();
			[OperationContract]
			IdName[] getSpeakers();
			[OperationContract]
			Dictionary<IdName, Dictionary<IdName, IdName[]>> getCampusPeriodClassTree();
		#endregion
		#region add
			[OperationContract]
			string addUser(UserData User);
			[OperationContract]
			string addCampus(string CampusName);
			[OperationContract]
			string addClass(ClassData Class);
			[OperationContract]
			string addPromotion(string PromotionName);
			[OperationContract]
			string addSubject(SubjectData Subject);
			[OperationContract]
			string addPeriod(PeriodData Period);
			[OperationContract]
			string grantRole(int UserId, int? Target);
			[OperationContract]
			string addEvent(EventData Event,int PlanningId, int? SpeakerId, int? Modality);
			[OperationContract]
			string addPrivateEvent(EventData Event);
		#endregion
		#region set
			[OperationContract]
			string setUser(UserData UD);
			[OperationContract]
			string setCampus(int Id, string CampusName);
			[OperationContract]
			string setClass(ClassData CD);
			[OperationContract]
			string setPromotion(int Id, string PromotionName);
			[OperationContract]
			string setSubject(SubjectData SD);
			[OperationContract]
			string setPeriod(PeriodData PD);
			[OperationContract]
			string setEvent(EventData EditedEvent);
			[OperationContract]
			string setPrivateEvent(EventData EditedEvent);
		#endregion
		#region del
			[OperationContract]
			string delUser(int Id);
			[OperationContract]
			string delClass(int Id);
			[OperationContract]
			string delSubject(int Id);
			[OperationContract]
			string delPeriod(int Id);
			[OperationContract]
			string delRole(int Id);
			[OperationContract]
			string delEvent(int Id);
			[OperationContract]
			string delCampus(int Id);
			[OperationContract]
			string delPromotion(int Id);
		#endregion
		#region get Current
			[OperationContract]
			UserData getUser(int ID);
			[OperationContract]
			ClassData getClass(int ID);
			[OperationContract]
			PeriodData getPeriod(int ID);
		#endregion
	}
}
