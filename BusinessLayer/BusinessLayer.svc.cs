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

		IdName[] IBusinessLayer.getPeriods()
		{
			return db.Planning.Where(p => p.Type == null)
				.Select(p => new IdName() { Id = p.Id, Name = p.Name }).ToArray();
		}

		SubjectData[] IBusinessLayer.getSubjects()
		{
			return db.Modality.Where(p => p.OnSubject == null && p.Hours == null).
				Select(p => new SubjectData()
				{
					Id = p.Id,
					Modalities = p.Modalitys.Select(m => new ModalityData() { Id = m.Id, Name = m.Name, Hours = m.Hours }).ToArray(),
					Name = p.Name
				}).ToArray();
		}

		IdName[] IBusinessLayer.getUsers()
		{
			return db.Planning.Where(p => p.Type ==(int)EventData.TypeEnum.User)
				.Select(p => new IdName() { Id = p.Id, Name = p.Name }).ToArray();
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
			var usr = db.User.Where(p => p.Id == Id).First();
			var plan = usr.Planning;
			foreach (Role r in usr.Roles)
			{
				db.Role.DeleteObject(r);
			}
			foreach(Event e in plan.Events.Concat(plan.OwnedEvents).Distinct())
			{
				e.PlaningRef.LastChange = DateTime.Now;
				db.Event.DeleteObject(e);
			}
			db.User.DeleteObject(usr);
			db.Planning.DeleteObject(plan);
			db.SaveChanges();
			return "ok";

		}

		string IBusinessLayer.delClass(int Id)
		{
			var clas = db.Class.Where(p => p.Id == Id).First();
			var plan = clas.Planning;
			if(plan.ChildrenPlannings.Count>0)
				return "D'autres objets en dépendent, merci de les suprimmer en premier lieu";
			db.Class.DeleteObject(clas);
			db.Planning.DeleteObject(plan);
			return "ok";
			
		}

		string IBusinessLayer.delSubject(int Id)
		{
			var sub = db.Modality.Where(m => m.Id == Id).First();
			foreach (Event e in sub.Modalitys.SelectMany(p => p.Events))
			{
				e.PlaningRef.LastChange = DateTime.Now;
				db.Event.DeleteObject(e);
			}
			foreach (Modality m in sub.Modalitys)
				db.Modality.DeleteObject(m);
			db.Modality.DeleteObject(sub);
			db.SaveChanges();
			return "ok";
		}

		string IBusinessLayer.delPeriod(int Id)
		{
			var per= db.Period.Where(p => p.Id == Id).First();
			var plan = per.Planning;
			if (plan.PeriodClasses.Count>0)
				return "D'autres objests en dépendent, merci de les suprimmer en premier lieu";
			db.Period.DeleteObject(per);
			db.Planning.DeleteObject(plan);
			db.SaveChanges();
			return "ok";

		}

		string IBusinessLayer.delRole(int Id)
		{
			db.Role.DeleteObject(db.Role.Where(r => r.Id == Id).First());
			db.SaveChanges();
			return "ok";
		}

		string IBusinessLayer.delEvent(int Id)
		{
			var ev=db.Event.Where(p=>p.Id==Id).First();
			ev.PlaningRef.LastChange = DateTime.Now;
			db.Event.DeleteObject(ev);
			db.SaveChanges();
			return "ok";
		}

		string IBusinessLayer.delCampus(int Id)
		{
			var c = db.Planning.Where(camp => camp.Id == Id);
			return "ok";
		}

		string IBusinessLayer.delPromotion(int Id)
		{
			var plan = db.Planning.Where(p => p.Id == Id).First();
			if (plan.ChildrenPlannings.Count > 0)
				return "D'autres objets en dépendent, merci de les suprimmer en premier lieu";
			db.Planning.DeleteObject(plan);
			db.SaveChanges();
			return "ok";
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
