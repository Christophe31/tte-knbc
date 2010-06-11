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
		/// <summary>
		/// This method return logged User
		/// </summary>
		/// <returns>current user related datas</returns>
		[OperationContract]
		UserData getUserData();
		
		#region Lecture d'évènements
			/// <summary>
			/// this function will allow you to get all the event related to a specific planning.
			/// </summary>
			/// <param name="Planning">Id of the planning</param>
			/// <param name="Start">start date of the selection range</param>
			/// <param name="Stop">end date of the selection range</param>
			/// <returns>All the events from the planning.</returns>
			[OperationContract]
			EventData[] getEvents(int Planning, DateTime Start, DateTime Stop);
			/// <summary>
			/// This method get the mandatory data from the Speaker to warn others to not plan him courses here
			/// </summary>
			/// <param name="ID">Speaker Id</param>
			/// <param name="Start">start date of the selection range</param>
			/// <param name="Stop">end date of the selection range</param>
			/// <returns></returns>
			[OperationContract]
			EventData[] getSpeakerEvents(int ID, DateTime Start, DateTime Stop);
			[OperationContract]
			EventData[] getSpeakingEvents(int ID, DateTime Start, DateTime Stop);
			/// <summary>
			/// To save bandwidth, this method will say if you already have an up to date planning before yo download all events.
			/// </summary>
			/// <param name="Planning">Id of the planning to Check</param>
			/// <param name="LastUpdate">client last update</param>
			/// <returns>is client cache up to date</returns>
			[OperationContract]
			bool isPlanningUpToDate(int Planning, DateTime LastUpdate);
		#endregion
		#region completion
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
			ModalityData[] getModalities();
			[OperationContract]
			IdName[] getSpeakers();
			[OperationContract]
			string[] getSpeakerBilan();
			[OperationContract]
			Dictionary<IdName, string[]> getCampusLocationsTree();
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
			string addEvent(EventData Event);
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
