using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataAccessLayer;

namespace BusinessLayer
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BusinessLayer" in code, svc and config file together.
	[UserContextBehavior]
	public class BusinessLayer : IBusinessLayer
	{
		Entities db = new Entities();
		#region IBusinessLayer Members

		RoleData[] IBusinessLayer.logIn(string UserLogin, string UserPassword)
		{
			if (db.User.Any(p => p.Login == UserLogin && p.Password == UserPassword))
			{
				OperationContext.Current.Extensions.Add(new UserOperationContext());
				UserOperationContext.Current.UserProperty = db.User.Where(p => p.Login == UserLogin && p.Password == UserPassword).
					Select(p => new {id= p.Id, name=p.Planning.Name, cid=p.Planning.ParentPlanning.Id, cname=p.Planning.ParentPlanning.Name}).ToArray().Select(p=>UserData.UD(p.id,p.name,IdName.IN(p.cid,p.cname))).First();

				var t = db.Role.Where(p => p.UserRef.Login == UserLogin && p.UserRef.Password == UserPassword)
					.Select(p => new
					{
						id = p.Target,
						type =p.Planning.Type
					}
				);
				return t.ToArray().Select(p=> RoleData.RD(p.id,
					(p.type.Value == 1 ?
							RoleData.RoleType.Administrator :
							(p.type.Value == 2 ?
								RoleData.RoleType.CampusManager :
								RoleData.RoleType.Speaker
							)
						))).ToArray();
			}
			throw new FaultException("Unknown Username or Incorrect Password");
		}

		EventData[] IBusinessLayer.getEvents(int Planning, DateTime Start, DateTime Stop)
		{
			if (Planning == UserOperationContext.Current.UserProperty.Id || db.Planning.Where(p=>p.Id==Planning).Any(p=>p.Type!=5))
			{
 				return db.Planning.Where(p=>p.Id==Planning).Single().Events.
					Select(p=> 
						new {
							ev = p ,
							sub= p.Modality.OnSubject.Name,
							mod= p.Modality.Name,
							spkr=p.SpeakerRef.Name,
							typ=p.PlaningRef.Type
					}).ToArray().Select(p=>EventData.ED(p.ev, p.sub.ToString(), p.mod.ToString(), p.spkr, p.typ)).ToArray();
			};
			throw new FaultException("try to access to a private planning");
		}

		bool IBusinessLayer.isPlanningUpToDate(int Planning, DateTime LastUpdate)
		{
			throw new NotImplementedException();
		}

		IdName[] IBusinessLayer.getCampuses()
		{
			throw new NotImplementedException();
		}

		IdName[] IBusinessLayer.getClasses()
		{
			throw new NotImplementedException();
		}

		IdName[] IBusinessLayer.getPromotions()
		{
			throw new NotImplementedException();
		}

		IdName[] IBusinessLayer.getPeriods()
		{
			throw new NotImplementedException();
		}

		SubjectData[] IBusinessLayer.getSubjects()
		{
			throw new NotImplementedException();
		}

		IdName[] IBusinessLayer.getUsers()
		{
			throw new NotImplementedException();
		}

		IdName[] IBusinessLayer.getSpeakers()
		{
			throw new NotImplementedException();
		}

		IdName IBusinessLayer.getUniversity()
		{
			throw new NotImplementedException();
		}

		Dictionary<IdName, Dictionary<IdName, IdName[]>> IBusinessLayer.getCampusPeriodClassTree()
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.addUser(UserData User)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.addCampus(string CampusName)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.addClass(ClassData Class)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.addPromotion(string PromotionName)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.addSubject(SubjectData Subject)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.addPeriod(PeriodData Period)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.grantRole(int UserId, int? Target)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.addEvent(EventData Event)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.addPrivateEvent(EventData Event)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.setUser(UserData UD)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.setCampus(int Id, string CampusName)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.setClass(ClassData CD)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.setPromotion(int Id, string PromotionName)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.setSubject(SubjectData SD)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.setPeriod(PeriodData PD)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.setEvent(EventData EditedEvent)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.setPrivateEvent(EventData EditedEvent)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.delUser(int Id)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.delClass(int Id)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.delSubject(int Id)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.delPeriod(int Id)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.delRole(int User, int? Target)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.delEvent(int Id)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.delCampus(int Id)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.delPromotion(int Id)
		{
			throw new NotImplementedException();
		}

		UserData IBusinessLayer.getUser(int ID)
		{
			throw new NotImplementedException();
		}

		ClassData IBusinessLayer.getClass(int ID)
		{
			throw new NotImplementedException();
		}

		PeriodData IBusinessLayer.getPeriod(int ID)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
