using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Objects.DataClasses;
using DataAccessLayer;

namespace BusinessLayer
{
	/// <summary>
	/// Implémentation de l'interface et du webService.
	/// </summary>
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
	public partial class BusinessLayer : IBusinessLayer, IDisposable
	{
		DateTime sqlMin = new DateTime(1753, 1, 2);
		DateTime sqlMax = new DateTime(9998, 12, 31);
		Entities db = new Entities();


		DateTime centerToSqlDate(DateTime d)
		{
			return ((d < sqlMin) ? sqlMin : ((d > sqlMax) ? sqlMax : d));
		}
		void centerToSqlDate(ref DateTime d1, ref DateTime d2)
		{
			d1 = ((d1 < sqlMin) ? sqlMin : ((d1 > sqlMax) ? sqlMax : d1));
			d2 = ((d2 < sqlMin) ? sqlMin : ((d2 > sqlMax) ? sqlMax : d2));
		}

		static string currentUserLogin
		{
			get { return OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name; }
		}
		int currentUserId { get { return db.User.First(u=>u.Login==currentUserLogin).Id; } }
		bool isUserAdmin
		{
			get{ return db.User.First(u => u.Login == currentUserLogin).Roles.Any(r => r.Target.GetValueOrDefault() == db.Planning.Where(u => u.Type == (int)EventData.TypeEnum.University).First().Id);}
		}
		EntityCollection<Role> currentUserRoles { get { return db.User.First(u => u.Login == currentUserLogin).Roles; } }
		bool validPlanning(int Id, EventData.TypeEnum? Type) 
		{
			return db.Planning.Any(p => p.Id == Id && p.Type == (int?)Type);
		}

		#region IBusinessLayer Members
		UserData IBusinessLayer.getUserData()
		{
			var usr = db.User.First(p => p.Login == currentUserLogin);
			return new UserData()
			{
				Id = usr.Planning.Id,
				Class = usr.Planning.Parent == null ? new IdName() {Id=0,Name=null } : new IdName() { Id = usr.Planning.ParentPlanning.Id, Name = usr.Planning.ParentPlanning.Name },
				Name = usr.Planning.Name,
				Roles = usr.Roles.Select(r =>
					new RoleData()
					{
						TargetId = r.Target,
						Role = r.Planning.Type.HasValue ? (r.Planning.Type.Value == (int)EventData.TypeEnum.University ? RoleData.RoleType.Administrator : RoleData.RoleType.CampusManager) : RoleData.RoleType.Speaker
					}).ToArray()
			};
		}
		EventData[] IBusinessLayer.getEvents(int Planning, DateTime Start, DateTime Stop)
		{
			centerToSqlDate(ref Start, ref Stop);
			if (  db.Planning.Any(p=>p.Id==Planning && p.Type!=5) || currentUserId == Planning )
			{
				EntityCollection<Event> t= new EntityCollection<Event>();
				if(db.Planning.Any(p=>p.Id==Planning && p.Type==5))
					t = db.Planning.Where(p => p.Id == Planning).Single().SpeakingEvents;
				return db.Planning.Where(p => p.Id == Planning).Single().Events.Concat(t).Where(p => p.Start < Stop && p.End > Start).
					Select(p =>
						new EventData()
						{
							Id = p.Id,
							Start = p.Start,
							End = p.End,
							Mandatory = p.Mandatory,
							Name = p.Name,
							Place = p.Place,
							ParentPlanning = new IdName() { Id = Planning, Name = db.Planning.First(pl => pl.Id == Planning).Name },
							Subject = p.Modality == null ? null : new IdName() { Id = p.Modality.OnSubject.Id, Name = p.Modality.OnSubject.Name },
							Modality = p.Modality == null ? null : new IdName() { Id = p.Modality.Id, Name = p.Modality.Name },
							Speaker = p.SpeakerRef == null ? null : new IdName() { Id = p.SpeakerRef.Id, Name = p.SpeakerRef.Name },
							Type = (EventData.TypeEnum)p.PlaningRef.Type
						}).ToArray();
			};
			throw new FaultException("try to access to a non valid or a non accessible planning");
		}
		EventData[] IBusinessLayer.getSpeakerEvents(int ID, DateTime Start, DateTime Stop)
		{
			return db.Planning.Where(p => p.Id == ID).Single().Events.
				Where(p => p.Start < Stop && p.End > Start && p.Mandatory == true).
					Select(p =>
						new EventData()
						{
							Start = p.Start,
							End = p.End,
							Name = "Indisponnible",
							Speaker = new IdName() { Id = p.PlaningRef.Id, Name = p.PlaningRef.Name },
							Type = EventData.TypeEnum.User
						}).ToArray();
		}
		EventData[] IBusinessLayer.getSpeakingEvents(int ID, DateTime Start, DateTime Stop)
		{

			return db.Planning.Where(p => p.Id == ID).Single().SpeakingEvents.
					Select(p =>
						new EventData()
						{
							Start = p.Start,
							End = p.End,
							Name = "Indisponnible",
							Speaker = new IdName() { Id = p.PlaningRef.Id, Name = p.PlaningRef.Name },
							Type = EventData.TypeEnum.User
						}).ToArray();
		}
		bool IBusinessLayer.isPlanningUpToDate(int Planning, DateTime LastUpdate)
		{
			var lu = centerToSqlDate(LastUpdate);
			return db.Planning.Any(p=>p.Id == Planning && (p.LastChange <= lu));
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
		Dictionary<IdName, string[]> IBusinessLayer.getCampusLocationsTree()
		{
			return db.Planning.Where(p => p.Type == (int)EventData.TypeEnum.Campus)
				.Select(p => new { camp = p, classes = p.ChildrenPlannings })
				.ToDictionary(p => new IdName() { Id = p.camp.Id, Name = p.camp.Name }
							,p=>p.classes.SelectMany(c=>c.Events).Concat(p.camp.Events).Select(e=>e.Name).ToArray()
							);
		}
		SubjectData[] IBusinessLayer.getSubjects()
		{
			return db.Modality.Where(p => p.OnSubject == null && p.Hours == null).
				Select(p => new { Id = p.Id, Name=p.Name, Modalities=p.Modalitys.Select(m => new ModalityData() { Id = m.Id, SubjectId = p.Id, Name = m.Name, Hours = m.Hours??0 }) }).
				ToArray().Select(p => new SubjectData()
				{
					Id = p.Id,
					Modalities = p.Modalities.ToArray(),
					Name = p.Name
				}).ToArray();
		}
		ModalityData[] IBusinessLayer.getModalities()
		{
			return db.Modality.Where(m => m.Hours != null).Select(m => new ModalityData() {Id=m.Id, Name=m.Name, SubjectId=m.Subject.GetValueOrDefault(), Hours=m.Hours.GetValueOrDefault()}).ToArray();
		}
		IdName[] IBusinessLayer.getUsers()
		{
			if(currentUserRoles.Count()>0)
				return db.Planning.Where(p => p.Type ==(int)EventData.TypeEnum.User)
					.Select(p => new IdName() { Id = p.Id, Name = p.Name }).ToArray();
			return new IdName[] {};
		}
		IdName[] IBusinessLayer.getSpeakers()
		{
			return db.Role.Where(p => p.Planning == null).Select(p => new IdName() {Id=p.User, Name=p.UserRef.Planning.Name }).ToArray();
		}
		string[] IBusinessLayer.getSpeakerBilan()
		{
			int usrId=currentUserId;
			string[] s=new string[] {"Classe","Sujet","Modalité","Nombre d'heure théorique"};
			return  s.Concat(db.Planning.First(usr => usrId == usr.Id).SpeakingEvents
				.Select(ev => new { modality = ev.Modality,Parent=ev.PlaningRef, courseDuration = (ev.Start - ev.End).Hours })
				.GroupBy(p => p.Parent).SelectMany(par => par
					.GroupBy(p=>p.modality).Select(mod=>  new { modality =  mod.First().modality, coursesDuration = mod.Sum(ev => ev.courseDuration) })
				.GroupBy(mod => mod.modality.Subject)
				.SelectMany(sub => sub.Select(mod =>
					mod.modality.OnSubject.Name + ";" + mod.modality.Name + ";" + mod.modality.Hours.ToString() + ";" + mod.coursesDuration.ToString())))).ToArray();
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
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			if (User.Password == null)
				return "Veuillez définir un mot de passe.";
			if (User.Name == null)
				return "Veuillez spécifier un nom d'utilisateur.";
			if (db.User.Any(u => User.Login == u.Login))
				return "Cet utilisateur existe déjà.";
			if (!(User.Class==null || ((User.Class.Id != 0) || !validPlanning(User.Class.Id, EventData.TypeEnum.Class))))
				return "Veuillez définir une classe valide.";
			Planning plan = new Planning() 
			{
				 Name = User.Name,
				 ParentPlanning =  User.Class == null ? null :db.Planning.Where(cl => cl.Id == (User.Class.Id)).FirstOrDefault(),
				 LastChange = DateTime.Now,
				 Type = (int)EventData.TypeEnum.User
			};
			db.Planning.AddObject(plan);
			db.User.AddObject(new User(){
				Planning = plan,
				Password = User.Password,
				Login = User.Login
			});
			if(User.Roles!=null)
				foreach (var role in User.Roles)
					db.Role.AddObject(new Role() 
						{
							Target = (role.TargetId.HasValue ? (validPlanning(role.TargetId.Value, EventData.TypeEnum.Campus)||validPlanning(role.TargetId.Value, EventData.TypeEnum.University) ? role.TargetId : null) : null),
							User = User.Id
						});
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.addClass(ClassData classToSet)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			if (validPlanning(classToSet.Campus.Id ,EventData.TypeEnum.Campus) && validPlanning(classToSet.Period.Id, EventData.TypeEnum.Period))
				return "Période et/ou Campus invalide(s).";
			if (db.Planning.Any(p =>classToSet.Campus!=null && p.Id == classToSet.Campus.Id && p.ChildrenPlannings.Any(c => c.Name == classToSet.Name)))
				return "Une classe avec ce nom existe déjà sur ce campus.";
			Planning plan = new Planning()
			{
				Name=classToSet.Name,
				ParentPlanning = db.Planning.Where(p => classToSet.Campus.Id == p.Id).First(),
				LastChange = DateTime.Now,
				Type = (int)EventData.TypeEnum.Class
			};
			db.Planning.AddObject(plan);
			db.Class.AddObject(new DataAccessLayer.Class()
			{
				PeriodPlanning = db.Planning.Where(p => classToSet.Period.Id == p.Id).First(),
				Planning = plan
			});
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.addPromotion(string promotionName)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			if (db.Planning.Any(p => p.Type == null && p.Name == promotionName))
				return "Cette promotion existe déjà!";
			db.Planning.AddObject( new Planning(){
				Name = promotionName,
				LastChange = null,
				Type = null,
				ParentPlanning = db.Planning.Where(u => u.Type == (int)EventData.TypeEnum.University).First()
			});
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.addCampus(string campusName)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			if (db.Planning.Any(p => p.Type == (int)EventData.TypeEnum.Campus && p.Name == campusName))
				return "Un Campus ayant ce nom existe déjà.";
			db.Planning.AddObject(new Planning()
			{
				Name = campusName,
				//LastChange = DateTime.Now,
				Type = (int)EventData.TypeEnum.Campus,
				ParentPlanning = db.Planning.Where(u => u.Type == (int)EventData.TypeEnum.University).First()
			});
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.addSubject(SubjectData subject)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			Modality sub = new Modality()
			{
				Name=subject.Name,
				Hours=null
			};
			db.Modality.AddObject(sub);
			if (subject.Modalities != null)
			{
				foreach (ModalityData m in subject.Modalities)
				{
					Modality mod = new Modality()
					{
						Name = m.Name,
						OnSubject = sub,
						Hours = m.Hours
					};
					db.Modality.AddObject(mod);
				}
			}
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.addPeriod(PeriodData period)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			var plan=new Planning()
			{
				Type=(int)EventData.TypeEnum.Period,
				Name=period.Name, 
				LastChange=DateTime.Now, 
				ParentPlanning=db.Planning.First(p=>p.Id==period.Promotion.Id&&p.Type==null)
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
		string IBusinessLayer.addEvent(EventData eventToAdd)
		{
			if (!
				(currentUserId == eventToAdd.ParentPlanning.Id ||
				isUserAdmin ||
				(db.Planning.Any(p =>
					p.Type == (int)EventData.TypeEnum.Campus &&
					currentUserRoles.Any(r => r.Target == p.Id &&
						(p.Id == eventToAdd.ParentPlanning.Id ||
						p.ChildrenPlannings.Any(cla => cla.Id == eventToAdd.ParentPlanning.Id))))))
				) 
				return "Vous devez être administrateur pour faire ça.";
			var tmp=db.Planning.Where(p=>p.Type==(int)EventData.TypeEnum.Campus && (p.Id==eventToAdd.ParentPlanning.Id ||p.ChildrenPlannings.Any(cla=>cla.Id==eventToAdd.ParentPlanning.Id)))
				.Select(camp=>camp.Events.Concat(camp.ChildrenPlannings.SelectMany(cla=>cla.Events))).FirstOrDefault();
			if (tmp != null)
			{
				string eventsInSameLocation=(eventsInSameLocation = tmp.Where(ev => ev.Start >= eventToAdd.End && ev.End <= eventToAdd.Start && ev.Place == eventToAdd.Place).SelectMany(names => names.Name + ", ") as string);
				if (eventsInSameLocation.Length > 0)
					return "La salle est déjà occupée.";
			}
			string truc="";
			if (db.User.Where(usr => usr.Id == (eventToAdd.Speaker==null?0:eventToAdd.Id)).Select(usr => usr.Planning.SpeakingEvents.Concat(usr.Planning.Events.Where(ev => ev.Mandatory))).Any(t => t.Any(ev => ev.Start >= eventToAdd.End && ev.End <= eventToAdd.Start)))
				truc = "L'intervenant est déjà occupé à cet horaire.";
			db.Event.AddObject(new Event()
			{
				Name = eventToAdd.Name,
				OwnerRef = db.User.First(u => u.Login == currentUserLogin).Planning,
				Planning = eventToAdd.ParentPlanning.Id,
				Place = eventToAdd.Place,
				End = eventToAdd.End,
				Start = eventToAdd.Start,
				Mandatory=eventToAdd.Mandatory,
				SpeakerRef = eventToAdd.Speaker == null ? null : (eventToAdd.Speaker.Id==0?null:db.User.First(u => u.Id == eventToAdd.Speaker.Id).Planning)
			});
			db.Planning.First(p => p.Id == eventToAdd.ParentPlanning.Id).LastChange = DateTime.Now;
			db.SaveChanges();
			return truc.Length==0?"ok":truc;
		}
		string IBusinessLayer.setUser(UserData userToSet)
		{
			if (!(isUserAdmin||userToSet.Id==currentUserId))
				return "Vous devez être administrateur pour faire ça.";
			User usr = db.User.First(u => u.Id == userToSet.Id);
			if ( isUserAdmin)
			{
				if(!validPlanning(userToSet.Class==null?0:userToSet.Class.Id, EventData.TypeEnum.Class))
					return "Nouvelle classe invalide.";
				if (userToSet.Login != usr.Login && !db.User.Any(u => u.Login == userToSet.Login))
					usr.Login = userToSet.Login;
				usr.Planning.Parent = userToSet.Class.Id;
				foreach(Role ro in usr.Roles)
				{
					if (!userToSet.Roles.Any(r => r.Id == ro.Id))
						db.Role.DeleteObject(ro);
				}
				foreach (RoleData ro in userToSet.Roles)
				{
					if (!usr.Roles.Any(r => r.Id == ro.Id))
						db.Role.AddObject(new Role()
						{
							Target = (ro.TargetId.HasValue ? (validPlanning(ro.TargetId.Value, EventData.TypeEnum.Campus) || validPlanning(ro.TargetId.Value, EventData.TypeEnum.University) ? ro.TargetId : null) : null),
							User = userToSet.Id
						});
				}
			}
			usr.Password = userToSet.Password;
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.setCampus(int Id, string CampusName)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			db.Planning.First(p => p.Id == Id && p.Type == (int)EventData.TypeEnum.Campus).Name = CampusName;
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.setClass(ClassData classToSet)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			if (!(validPlanning(classToSet.Campus.Id, EventData.TypeEnum.Campus) && validPlanning(classToSet.Period.Id, EventData.TypeEnum.Period)))
				return "Référence invalide!";
			var classe = db.Planning.First(p => p.Id == classToSet.Id && p.Type == (int)EventData.TypeEnum.Class);
			classe.Name = classToSet.Name;
			classe.Class.Period = classToSet.Period.Id;
			classe.Parent = classToSet.Campus.Id;
			return "ok";
		}
		string IBusinessLayer.setPromotion(int Id, string PromotionName)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			db.Planning.First(p => p.Id == Id && p.Type == null).Name = PromotionName;
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.setSubject(SubjectData subjectToSet)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			var sub = db.Modality.First(p => p.Id == subjectToSet.Id);
			foreach (Modality dbMod in sub.Modalitys.Distinct().ToArray())
			{
				var newMod=subjectToSet.Modalities.FirstOrDefault(m => m.Id == dbMod.Id);
				if (newMod==null)
					db.Modality.DeleteObject(dbMod);
				else
				{
					dbMod.Name = newMod.Name;
					dbMod.Hours = newMod.Hours;
				}
			}
			foreach (ModalityData mo in subjectToSet.Modalities)
			{
				if (!sub.Modalitys.Any(m => m.Id == mo.Id))
					db.Modality.AddObject(new Modality()
					{
						OnSubject = sub,
						Hours = mo.Hours,
						Name=mo.Name
					});
			}
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.setPeriod(PeriodData PD)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			var myPeriod=db.Planning.First(p => p.Id == PD.Id && p.Type ==(int)EventData.TypeEnum.Period);
			myPeriod.ParentPlanning=db.Planning.First(p => p.Id == PD.Promotion.Id && p.Type == null);
			myPeriod.Name = PD.Name;
			myPeriod.Period.End = PD.End;
			myPeriod.Period.Start = PD.Start;
			return "ok";
		}

		string IBusinessLayer.setEvent(EventData eventToSet)
		{
			if (!
				(currentUserId == eventToSet.ParentPlanning.Id ||
				 isUserAdmin ||  
				(db.Planning.Any(p => 
					p.Type == (int)EventData.TypeEnum.Campus && 
					currentUserRoles.Any(r => r.Target == p.Id && 
						(p.Id == eventToSet.ParentPlanning.Id ||
						p.ChildrenPlannings.Any(cla => cla.Id == eventToSet.ParentPlanning.Id))))))
				)
				return "Vous devez être administrateur pour faire ça.";
			string eventsInSameLocation = "";
			var tmp = db.Planning.Where(p => p.Type == (int)EventData.TypeEnum.Campus && (p.Id == eventToSet.ParentPlanning.Id || p.ChildrenPlannings.Any(cla => cla.Id == eventToSet.ParentPlanning.Id)))
				.Select(camp => camp.Events.Concat(camp.ChildrenPlannings.SelectMany(cla => cla.Events))).FirstOrDefault();
			if (tmp != null)
			{
				eventsInSameLocation = tmp.Where(ev => ev.Start <= eventToSet.End && ev.End >= eventToSet.Start && ev.Place == eventToSet.Place).SelectMany(names => names.Name + ", ") as string;
				if (eventsInSameLocation.Length > 0)
					return "Les salles " + eventsInSameLocation+"Sont occupées.";
			}

			db.Planning.First(pl => pl.Id == eventToSet.ParentPlanning.Id).LastChange = DateTime.Now;

			Event eve=db.Event.First(p=>eventToSet.Id == p.Id);
			eve.Subject= (eventToSet.Modality==null ? null as int? : eventToSet.Modality.Id);
			eve.Place = eventToSet.Place;
			eve.End = eventToSet.End;
			eve.Start = eventToSet.Start;
			eve.Speaker = eventToSet.Speaker == null ? null as int? : eventToSet.Speaker.Id;
			eve.Owner = currentUserId;
			eve.Mandatory = eventToSet.Mandatory;

			db.SaveChanges();
			return "ok";
		}
		
		string IBusinessLayer.delUser(int Id)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			var usr = db.User.Where(p => p.Id == Id).First();
			var plan = usr.Planning;
			foreach (Role r in usr.Roles.ToArray())
			{
				db.Role.DeleteObject(r);
			}
			foreach(Event e in plan.Events.Concat(plan.OwnedEvents).Distinct().ToArray())
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
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			var clas = db.Class.Where(p => p.Id == Id).First();
			var plan = clas.Planning;
			if(plan.ChildrenPlannings.Count>0)
				return "D'autres objets en dépendent, merci de les suprimmer en premier lieu.";
			db.Class.DeleteObject(clas);
			db.Planning.DeleteObject(plan);
			return "ok";
		}
		string IBusinessLayer.delSubject(int Id)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			var sub = db.Modality.Where(m => m.Id == Id).First();
			foreach (Event e in sub.Modalitys.SelectMany(p => p.Events))
			{
				e.PlaningRef.LastChange = DateTime.Now;
				db.Event.DeleteObject(e);
			}
			foreach (Modality m in sub.Modalitys.ToArray())
				db.Modality.DeleteObject(m);
			db.Modality.DeleteObject(sub);
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.delPeriod(int Id)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			var per= db.Period.Where(p => p.Id == Id).First();
			var plan = per.Planning;
			if (plan.PeriodClasses.Count>0)
				return "D'autres objets en dépendent, merci de les suprimmer en premier lieu.";
			db.Period.DeleteObject(per);
			db.Planning.DeleteObject(plan);
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.delEvent(int Id)
		{
			if (!
				(isUserAdmin ||
				currentUserId == db.Event.Where(ev=>ev.Id==Id).First().PlaningRef.Id ||
				(db.Planning.Any(p =>
					p.Type == (int)EventData.TypeEnum.Campus &&
					currentUserRoles.Any(r => r.Target == p.Id &&
						(p.Id == db.Event.Where(ev => ev.Id == Id).First().PlaningRef.Id ||
						p.ChildrenPlannings.Any(cla => cla.Id == db.Event.Where(ev => ev.Id == Id).First().PlaningRef.Id))))))
				)
				return "Vous devez être administrateur pour faire ça.";
			var eve=db.Event.Where(p=>p.Id==Id).First();
			eve.PlaningRef.LastChange = DateTime.Now;
			db.Event.DeleteObject(eve);
			db.SaveChanges();
			return "ok";
		}
		string IBusinessLayer.delCampus(int Id)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			db.Planning.DeleteObject(db.Planning.Where(camp => camp.Id == Id && camp.Type==(int)EventData.TypeEnum.Campus).First());
			return "ok";
		}
		string IBusinessLayer.delPromotion(int Id)
		{
			if (!isUserAdmin)
				return "Vous devez être administrateur pour faire ça.";
			var plan = db.Planning.Where(p => p.Id == Id).First();
			if (plan.ChildrenPlannings.Count > 0)
				return "D'autres objets en dépendent, merci de les suprimmer en premier lieu.";
			db.Planning.DeleteObject(plan);
			db.SaveChanges();
			return "ok";
		}
		UserData IBusinessLayer.getUser(int ID)
		{
			if(currentUserRoles.Count()>0)
				return db.User.Where(u => u.Id == ID)
					.Select(u => new
					{
						Class = new IdName()
							   {
								   Id = u.Planning.ParentPlanning ==null ?0: u.Planning.ParentPlanning.Id,
								   Name = u.Planning.ParentPlanning.Name
							   },
						Id = ID,
						Login=u.Login,
						Name = u.Planning.Name,
						Roles = u.Roles
					}).ToArray().Select(u=>new UserData() {Id=u.Id, Roles= u.Roles.Select(r => new RoleData()
						{
							Id = r.Id,
							Role = r.Target.HasValue ? (r.Planning.Type.Value == (int)EventData.TypeEnum.University ? RoleData.RoleType.Administrator : RoleData.RoleType.CampusManager) : RoleData.RoleType.Speaker,
							TargetId = r.Target
						}).ToArray(), Class=u.Class, Name= u.Name, Login=u.Login }).First();
			return null;
		}
		ClassData IBusinessLayer.getClass(int ID)
		{
			if (currentUserRoles.Count() > 0)
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
			if (currentUserRoles.Count() > 0)
				return db.Period.Where(p => p.Id == ID)
					.Select(p => new PeriodData()
					{
						Id = ID,
						Name = p.Planning.Name,
						Promotion = new IdName() { Id = p.Planning.ParentPlanning.Id, Name = p.Planning.ParentPlanning.Name },
						Start = p.Start,
						End = p.End
					}).First();
			return null;
		}
		#endregion

		#region IDisposable Members

		/// <summary>
		/// dispose the db properly
		/// </summary>
		public void Dispose()
		{
			this.db.Dispose();
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}