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
		DateTime sqlMin = new DateTime(1753, 1, 2);
		DateTime sqlMax = new DateTime(9998, 12, 31);
		Entities db = new Entities();

		public void centerToSqlDate(ref DateTime d)
		{
			d = ((d < sqlMin) ? sqlMin : ((d > sqlMax) ? sqlMax : d));
		}
		public void centerToSqlDate(ref DateTime d1, ref DateTime d2)
		{
			d1 = ((d1 < sqlMin) ? sqlMin : ((d1 > sqlMax) ? sqlMax : d1));
			d2 = ((d2 < sqlMin) ? sqlMin : ((d2 > sqlMax) ? sqlMax : d2));
		}

		#region IBusinessLayer Members

		RoleData[] IBusinessLayer.logIn(string UserLogin, string UserPassword)
		{
			if (db.User.Any(p => p.Login == UserLogin && p.Password == UserPassword))
			{
				OperationContext.Current.Extensions.Add(new UserOperationContext());
				UserOperationContext.Current.UserProperty = db.User.Where(p => p.Login == UserLogin && p.Password == UserPassword).
					Select(p => new {id= p.Id, name=p.Planning.Name, cid=p.Planning.ParentPlanning.Id, cname=p.Planning.ParentPlanning.Name}).ToArray().Select(p=>UserData.UD(p.id,p.name,IdName.IN(p.cid,p.cname))).First();

				return db.Role.Where(p => p.UserRef.Login == UserLogin && p.UserRef.Password == UserPassword)
					.Select(p => new RoleData()
					{
						TargetId = p.Planning.Id,
						Role=	p.Planning.Type == (int)EventData.TypeEnum.University ? RoleData.RoleType.Administrator :
								p.Planning.Type == (int)EventData.TypeEnum.Campus     ? RoleData.RoleType.CampusManager :
								RoleData.RoleType.Speaker 
					}
				).ToArray();
			}
			throw new FaultException("Unknown Username or Incorrect Password");
		}

		EventData[] IBusinessLayer.getEvents(int Planning, DateTime Start, DateTime Stop)
		{
			centerToSqlDate(ref Start, ref Stop);
			if (Planning == UserOperationContext.Current.UserProperty.Id || db.Planning.Where(p=>p.Id==Planning).Any(p=>p.Type!=5))
			{
 				return db.Planning.Where(p=>p.Id==Planning).Single().Events.Where(p=>p.Start < Stop && p.End > Start ).
					Select(p=> 
						new EventData(){
							Id = p.Id,
							Start = p.Start,
							End = p.End,
							Mandatory = p.Mandatory,
							Name = p.Name,
							Place = p.Place,
							Subject = p.Modality.OnSubject.Name,
							Modality = p.Modality.Name,
							Speaker = p.SpeakerRef.Name,
							Type = (EventData.TypeEnum)p.PlaningRef.Type
					}).ToArray();
			};
			throw new FaultException("try to access to a non accessible private planning");
		}

		bool IBusinessLayer.isPlanningUpToDate(int Planning, DateTime LastUpdate)
		{
			centerToSqlDate(ref LastUpdate);
			return db.Planning.Any(p=>p.Id == Planning && ((p.LastChange??DateTime.Now) >= LastUpdate));
		}

		IdName[] IBusinessLayer.getPlannings(EventData.TypeEnum Type)
		{
			return db.Planning.Where(p => p.Type == (int)Type)
				.Select(p => new IdName() {Id=p.Id, Name=p.Name}).ToArray();
		}

		IdName IBusinessLayer.getUniversity()
		{
			return db.Planning.Where(p => p.Type == (int)EventData.TypeEnum.University)
				.Select(p => new IdName() { Id = p.Id, Name = p.Name }).First();
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
			return db.Role.Where(p => p.Planning == null).Select(p => new IdName() {Id=p.User, Name=p.UserRef.Planning.Name }).ToArray();
		}

		Dictionary<IdName, Dictionary<IdName, IdName[]>> IBusinessLayer.getCampusPeriodClassTree()
		{
			return db.Planning.Where(
								p => p.Type == (int)EventData.TypeEnum.Campus &&
									p.ChildrenPlannings.Any(
										cl => cl.Type == (int)EventData.TypeEnum.Class &&
										cl.Class.Period != null
									)
								)
				.Select(camp => new
				{
					campus = (new IdName() { Id = camp.Id, Name = camp.Name }),
					val = (db.Planning.Where(p=> p.Type == (int)EventData.TypeEnum.Period &&
										p.PeriodClasses.Any(cl=>cl.Planning.ParentPlanning.Id==camp.Id))
							.Select(p=>new{
									period = new IdName(){Id=p.Id,Name=p.Name},
									cls= p.PeriodClasses.Select(cl => new IdName() {Id=cl.Id, Name=cl.Planning.Name}).ToArray()
									}
								)
							)
				})
				.ToDictionary(
					p => p.campus,
					p => p.val.ToDictionary(f=>f.period,
							f => f.cls
					)
				);
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
