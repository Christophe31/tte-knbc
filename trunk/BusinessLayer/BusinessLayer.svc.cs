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
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
	public partial class BusinessLayer : IBusinessLayer
	{
		DateTime sqlMin = new DateTime(1753, 1, 2);
		DateTime sqlMax = new DateTime(9998, 12, 31);
		Entities db = new Entities();

		string currentUserLogin
		{
			get { return OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name; }
		}
		public void centerToSqlDate(ref DateTime d)
		{
			d = ((d < sqlMin) ? sqlMin : ((d > sqlMax) ? sqlMax : d));
		}
		public void centerToSqlDate(ref DateTime d1, ref DateTime d2)
		{
			d1 = ((d1 < sqlMin) ? sqlMin : ((d1 > sqlMax) ? sqlMax : d1));
			d2 = ((d2 < sqlMin) ? sqlMin : ((d2 > sqlMax) ? sqlMax : d2));
		}

		bool isUserAdmin()
		{
			return getUserRoles().Any(r => r.TargetId == db.Planning.Where(u => u.Type == (int)EventData.TypeEnum.University).First().Id);
		}
		Planning getUserPlanning() 
		{
			return db.User.Where(u => u.Login == currentUserLogin).First().Planning;
		}

		#region IBusinessLayer Members
		public RoleData[] getUserRoles()
		{
			return db.Role.Where(p => p.UserRef.Login == currentUserLogin)
				.Select(p => new
				{
					TargetId = p.Planning.Id,
					Role=	p.Planning.Type
				}
			).ToArray().Select(p => new RoleData()
			{
				TargetId = p.TargetId,
				Role = p.Role.HasValue ? RoleData.RoleType.Speaker :
						p.Role.Value == (int)EventData.TypeEnum.University ? RoleData.RoleType.Administrator :
						RoleData.RoleType.CampusManager
			}).ToArray();
		}
		EventData[] IBusinessLayer.getEvents(int Planning, DateTime Start, DateTime Stop)
		{
			centerToSqlDate(ref Start, ref Stop);
			if (  db.Planning.Where(p=>p.Id==Planning).Any(p=>p.Type!=5) || getUserPlanning().Id == Planning )
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
							Subject = p.Modality==null?null:p.Modality.OnSubject.Name,
							Modality = p.Modality==null?null:p.Modality.Name,
							Speaker = p.SpeakerRef==null?null:p.SpeakerRef.Name,
							Type = (EventData.TypeEnum)p.PlaningRef.Type
					}).ToArray();
			};
			throw new FaultException("try to access to a non valid or a non accessible planning");
		}
		EventData[] IBusinessLayer.getSpeakerEvents(int ID, DateTime Start, DateTime Stop)
		{
			return db.Planning.Where(p => p.Id == ID).Single().Events.
				Where(p => p.Start < Stop && p.End > Start && p.Mandatory==true).
					Select(p =>
						new EventData()
						{
							Start = p.Start,
							End = p.End,
							Name = "Indisponnible",
							Speaker = p.PlaningRef.Name,
							Type = EventData.TypeEnum.User
						}).ToArray();
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
		IdName[] IBusinessLayer.getPromotions()
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
					Modalities = p.Modalitys.Select(m => new ModalityData() { Id = m.Id,SubjectId=p.Id, Name = m.Name, Hours = m.Hours.GetValueOrDefault() }).ToArray(),
					Name = p.Name
				}).ToArray();
		}
		IdName[] IBusinessLayer.getUsers()
		{
			if(getUserRoles().Count()>0)
				return db.Planning.Where(p => p.Type ==(int)EventData.TypeEnum.User)
					.Select(p => new IdName() { Id = p.Id, Name = p.Name }).ToArray();
			return new IdName[] {};
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
									cls= p.PeriodClasses.Select(cl => new IdName() {Id=cl.Id, Name=cl.Planning.Name})
									}
								)
							)
				})
				.ToDictionary(
					p => p.campus,
					p => p.val.ToDictionary(f=>f.period,
							f => f.cls.ToArray()
					)
				);
		}
		string IBusinessLayer.addUser(UserData User)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
			if (User.Password == null)
				return "veuillez definir un mot de passe";
			if (User.Name == null)
				return "veillez spécifier un nom d'utilisateur";
			if (db.User.Any(u => User.Login == u.Login))
				return "cet utilisateur existe déjà";
			if (!(User.Class.Id == 0) || !db.Class.Any(c => User.Class.Id == c.Id))
				return "veuillez définir une classe valide";
			Planning plan = new Planning() 
			{
				 Name = User.Name,
				 ParentPlanning = db.Planning.Where(cl=>cl.Id==User.Class.Id).FirstOrDefault(),
				 LastChange = DateTime.Now,
				 Type = (int)EventData.TypeEnum.User
			};
			db.Planning.AddObject(plan);
			db.User.AddObject(new User(){
				Planning = plan,
				Password = User.Password,
				Login = User.Login
			});
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.addCampus(string CampusName)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
			db.Planning.AddObject( new Planning(){
				Name = CampusName,
				LastChange = DateTime.Now,
				Type = (int)EventData.TypeEnum.Campus,
				ParentPlanning = db.Planning.Where(u => u.Type == (int)EventData.TypeEnum.University).First()
			});
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.addClass(ClassData Class)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
			if (db.Planning.Any(p => p.Type == (int)EventData.TypeEnum.Campus && Class.Campus.Id == p.Id) && db.Planning.Any(p => p.Type == (int)EventData.TypeEnum.Period && Class.Period.Id == p.Id))
				return "Class or Campus link invalid";
			if (db.Planning.Any(p => p.Id == Class.Campus.Id && p.ChildrenPlannings.Any(c => c.Name == Class.Name)))
				return "Une classe ave ce nom existe déjà sur ce campus";
			Planning plan = new Planning()
			{
				Name=Class.Name,
				ParentPlanning = db.Planning.Where(p => Class.Campus.Id == p.Id).First(),
				LastChange = DateTime.Now,
				Type = (int)EventData.TypeEnum.Class
			};
			db.Planning.AddObject(plan);
			db.Class.AddObject(new DataAccessLayer.Class()
			{
				PeriodPlanning = db.Planning.Where(p => Class.Period.Id == p.Id).First(),
				Planning = plan
			});
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.addPromotion(string PromotionName)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
			if (db.Planning.Any(p => p.Type == null && p.Name == PromotionName))
				return "cette promotion existe déjà";
			db.Planning.AddObject( new Planning(){
				Name = PromotionName,
				LastChange = null,
				Type = null,
				ParentPlanning = db.Planning.Where(u => u.Type == (int)EventData.TypeEnum.University).First()
			});
			db.SaveChanges();
			return "ok";
			throw new NotImplementedException();
		}
		string IBusinessLayer.addSubject(SubjectData Subject)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
			Modality sub = new Modality()
			{
				Name=Subject.Name,
				Hours=0
			};
			db.Modality.AddObject(sub);
			foreach (ModalityData m in Subject.Modalities)
			{
				Modality mod = new Modality()
				{
					Name = m.Name,
					OnSubject = sub,
					Hours=m.Hours
				};
				db.Modality.AddObject(mod);
			}
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.addPeriod(PeriodData period)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
			var plan=new Planning()
			{
				Type=(int)EventData.TypeEnum.Period,
				Name=period.Name, 
				LastChange=DateTime.Now, 
				ParentPlanning=db.Planning.First(p=>p.Name==period.PromotionName&&p.Type==null)
			};
			db.Planning.AddObject(plan);
			db.Period.AddObject(new Period()
			{
				Planning=plan,
				Start=period.Start,
				End=period.End
			});
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.grantRole(int UserId, int? Target)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
			if (!(db.User.Any(u => u.Id == UserId)
				&&
				(!Target.HasValue
					||
					db.Planning.Any(p =>
						p.Id == Target
						&&
						(p.Type == (int)EventData.TypeEnum.Campus
						||
						p.Type == (int)EventData.TypeEnum.University)
					))))
				return "Utilisateur ou cible du droit invalide";
			db.Role.AddObject(new Role() { Target = Target, UserRef=db.User.First(u=>u.Id==UserId) });
			return "ok";
		}

		string IBusinessLayer.addEvent(EventData even, int PlanningId,int? SpeakerId, int? ModalityId)
		{
			
			 new Event() 
			{
				Name=even.Name,
				OwnerRef=db.User.First(u=>u.Login==currentUserLogin).Planning,
				Place=even.Place,
				End=even.End,
				Start=even.Start,
				SpeakerRef=db.User.First(u=>u.Id==SpeakerId).Planning
			};
			return "ok";
		}

		string IBusinessLayer.addPrivateEvent(EventData Event)
		{
			throw new NotImplementedException();
		}

		string IBusinessLayer.setUser(UserData UD)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";

			throw new NotImplementedException();
		}

		string IBusinessLayer.setCampus(int Id, string CampusName)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";

			throw new NotImplementedException();
		}

		string IBusinessLayer.setClass(ClassData CD)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";

			throw new NotImplementedException();
		}

		string IBusinessLayer.setPromotion(int Id, string PromotionName)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";

			throw new NotImplementedException();
		}

		string IBusinessLayer.setSubject(SubjectData SD)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";

			throw new NotImplementedException();
		}

		string IBusinessLayer.setPeriod(PeriodData PD)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";

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
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
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
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
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
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
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
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
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
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
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
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
			var c = db.Planning.Where(camp => camp.Id == Id);
			return "ok";
		}
		string IBusinessLayer.delPromotion(int Id)
		{
			if (!isUserAdmin())
				return "Vous devez être administrateur pour faire ça.";
			var plan = db.Planning.Where(p => p.Id == Id).First();
			if (plan.ChildrenPlannings.Count > 0)
				return "D'autres objets en dépendent, merci de les suprimmer en premier lieu";
			db.Planning.DeleteObject(plan);
			db.SaveChanges();
			return "ok";
		}
		UserData IBusinessLayer.getUser(int ID)
		{
			if(getUserRoles().Count()>0)
				return db.User.Where(u => u.Id == ID)
					.Select(u => new UserData()
					{Class = new IdName()
							{
								Id = u.Planning.ParentPlanning.Parent ?? 0,
								Name = u.Planning.ParentPlanning.Name
							},
						Id = ID,
						Name = u.Planning.Name,
						Login = u.Login,
						Password=null
					}).First();
			return null;
		}
		ClassData IBusinessLayer.getClass(int ID)
		{
			if (getUserRoles().Count() > 0)
				return db.Class.Where(c => c.Id == ID).
					Select(c => new ClassData()
					{
						Id = ID, 
						Name = c.Planning.Name, 
						Campus = new IdName() { 
							Id = c.Planning.ParentPlanning.Id, 
							Name = c.Planning.ParentPlanning.Name },
						Period = new IdName() {
							Id = c.PeriodPlanning.Id, 
							Name = c.PeriodPlanning.Name } 
					}).First();
			return null;
		}
		PeriodData IBusinessLayer.getPeriod(int ID)
		{
			if (getUserRoles().Count() > 0)
				return db.Period.Where(p => p.Id == ID)
					.Select(p => new PeriodData() 
					{
 						Id=ID,
						Name=p.Planning.Name,
						PromotionName=p.Planning.ParentPlanning.Name,
						Start=p.Start,
						End=p.End
					}).First();
			return null;
		}
		#endregion
	}
}