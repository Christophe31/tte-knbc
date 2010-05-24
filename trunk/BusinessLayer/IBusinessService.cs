﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BusinessLayer
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IBusinessLayer" in both code and config file together.
	[ServiceContract]
	public interface IBusinessService
	{
		#region Lecture d'évènements
			[OperationContract]
			EventData[] getEventsByCampus(int CampusId, DateTime Start, DateTime Stop, DateTime LastUpdate);
			[OperationContract]
			EventData[] getEventsByUniversity(DateTime Start, DateTime Stop, DateTime LastUpdate);
			[OperationContract]
			EventData[] getEventsByPeriod(int PeriodId, DateTime Start, DateTime Stop, DateTime LastUpdate);
			[OperationContract]
			EventData[] getEventsByClass(int ClassId, DateTime Start, DateTime Stop, DateTime LastUpdate);
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
			[OperationContract]
			string[] getModalities();
			[OperationContract]
			Dictionary<string, Dictionary<string, string[]>> getCampusPeriodClassTree();
        
			///note de christophe: ce message est de mugimasaka
			///tu veux faire quoi de l'association derierre?
			///si tu veux computer quelquechose entre l'entrée et la sortie, c'est que c'est au serveur (moi) de le faire ^^
			///cette partie là pourait être gérée avec du cache mais ça ne me parait pas pertinant si c'est pour ta prtie uniquement
			//Fonctions de modification à implémenter
            //Si tu veux remplacer les champs par des id je te laisse faire
            //C'est juste pour te donner une idée globale des fonctions qu'il me faut

            //Permet de récupérer la classe d'un utilisateur
            //string getClassOfUser(string username);

            //Permet de récupérer le campus d'une classe
            //string getCampusOfClass(string classname);

            //Permet de récupérer la période d'une classe
            //string getPeriodOfClass(string classname);

            //Permet de récupérer la date de début (DateTime), la date de fin (DateTime) et la promotion d'une période
            //Je te laisse l'implementer comme tu veux (genre en une ou trois fonctions)
		#endregion
		#region Identified completion
			[OperationContract]
			IdName[] getIdCampusNames();
			[OperationContract]
			IdName[] getIdClassesNames();
			[OperationContract]
			IdName[] getIdPromotionsNames();
			[OperationContract]
			IdName[] getIdPeriodsNames();
			[OperationContract]
			Tuple<int, string, string>[] getIdSubjectsNamesModality();
			[OperationContract]
			IdName[] getIdUsersNames();
			[OperationContract]
			Dictionary<IdName, Dictionary<IdName, IdName[]>> getIdCampusPeriodClassTree();
		#endregion
		#region add
			[OperationContract]
			string addUser(string UserName, string UserPassword, string UserClassName);
			[OperationContract]
			string addCampus(string CampusName);
			[OperationContract]
			string addClass(string ClassName, string CampusName, string PeriodeName);
			[OperationContract]
			string addPromotion(string PromotionName);
			[OperationContract]
			string addSubject(string SubjectName, int Hours, string Modality);
			[OperationContract]
			string addPeriod(string PeriodName, string PromotionName, DateTime PeriodStart, DateTime PeriodEnd);
			[OperationContract]
			string grantNewRight(string UserName, string CampusName);
			[OperationContract]
			string addEventToCampus(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string CampusName, string Place);
			[OperationContract]
			string addEventToPeriode(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string PeriodeName, string Place);
			[OperationContract]
			string addEventToClass(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string ClassName, string Subject, string Modality, string Place);
			[OperationContract]
			string addEventToUniversity(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string Place);
			[OperationContract]
			string addEventToUser(string EventName, DateTime Start, DateTime End, bool Mandatory, string Place);
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
			string setEvent( EventData EditedEvent);
		#endregion
		#region del
			[OperationContract]
			string delUser(int Id);
			[OperationContract]
			string delCampus(int Id);
			[OperationContract]
			string delClass(int Id);
			[OperationContract]
			string delPromotion(int Id);
			[OperationContract]
			string delSubject(int Id);
			[OperationContract]
			string delPeriod(int Id);
			[OperationContract]
			string delRight(string UserName, string CampusName);
			[OperationContract]
			string delEventToCampus(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string CampusName, string Place);
			[OperationContract]
			string delEventToPeriode(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string PeriodeName, string Place);
			[OperationContract]
			string delEventToClass(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string ClassName, string Subject, string Modality, string Place);
			[OperationContract]
			string delEventToUniversity(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string Place);
			[OperationContract]
			string delEventToUser(string EventName, DateTime Start, DateTime End, bool Mandatory, string Place);
		#endregion
		#region get Current
			[OperationContract]
			UserData getUser(int ID);
			[OperationContract]
			ClassData getClass(int ID);
			[OperationContract]
			PeriodData getPeriod(int ID);
			[OperationContract]
			SubjectData getSubject(int ID);
		#endregion
	}
}