using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataModel;

namespace BusinessLayer
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BusinessLayer" in code, svc and config file together.
	public class BusinessService : IBusinessService, IDisposable
	{
		protected  TteDataBase db;
		public BusinessService()
		{
			db = new TteDataBase();
		}
		public void centerToSqlDate(ref DateTime d)
		{
			//The range of SQL's datetime starts at January 1st, 1753 and ends in 9999.
			DateTime min = new DateTime(1753, 1, 2);
			DateTime max = new DateTime(9998, 12, 31);
			if (d < min)
				d=min;
			else if (d > max)
				d=max;
		}

		#region IBusinessLayer Members
			#region Lecture d'évènements
				public EventData[] getEventsByCampus(int CampusId, DateTime Start, DateTime Stop)
				{
					centerToSqlDate(ref Start); centerToSqlDate(ref Stop);
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
									 p.Campus.Id == CampusId
								 select new { evt = p, sub = p.Subject.Name,mod=p.Subject.Modality , spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub,e.mod, e.spk)).ToArray();
				}
				public bool isUpToDateByCampus(int id,DateTime LastUpdate)
				{
					return db.Campus.Where(p=>p.Id==id).Single().LastChange.CompareTo(LastUpdate)>=0;
				}

				public EventData[] getEventsByUniversity(DateTime Start, DateTime Stop)
				{
					centerToSqlDate(ref Start); centerToSqlDate(ref Stop);
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
									 p.Campus == null && p.Period == null && p.Class == null
								 select new { evt = p, sub = p.Subject.Name, mod = p.Subject.Modality, spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub, e.mod, e.spk)).ToArray();
				}
				public bool isUpToDateByUniversity(DateTime LastUpdate)
				{
					return db.University.Single().LastChange.CompareTo(LastUpdate) >= 0;
				}


				public EventData[] getEventsByPeriod(int PeriodId, DateTime Start, DateTime Stop)
				{
					centerToSqlDate(ref Start); centerToSqlDate(ref Stop);
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
									p.Period.Id == PeriodId
								 select new { evt = p, sub = p.Subject.Name, mod = p.Subject.Modality, spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub, e.mod, e.spk)).ToArray();
				}
				public bool isUpToDateByPeriod(int id, DateTime LastUpdate)
				{
					return db.Period.Where(p => p.Id == id).Single().LastChange.CompareTo(LastUpdate) >= 0;
				}


				public EventData[] getEventsByClass(int ClassId, DateTime Start, DateTime Stop)
				{
					centerToSqlDate(ref Start); centerToSqlDate(ref Stop);
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
									p.Class.Id ==ClassId 
								 select new { evt = p, sub = p.Subject.Name, mod = p.Subject.Modality, spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub, e.mod, e.spk)).ToArray();
				}
				public bool isUpToDateByClass(int id, DateTime LastUpdate)
				{
					return db.Class.Where(p => p.Id == id).Single().LastChange.CompareTo(LastUpdate) >= 0;
				}

				public EventData[] getPrivateEvents(DateTime Start, DateTime Stop) 
				{
					centerToSqlDate(ref Start); centerToSqlDate(ref Stop);
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
//TODO:Modifier quand les sessions seront gérées
									p.Creator.Name == "christophe"  &&
									p.Speaker == null 
								 select new { evt = p, sub = p.Subject.Name, mod = p.Subject.Modality, spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub, e.mod, e.spk)).ToArray();
				}
				public bool isUpToDateByPrivate(DateTime LastUpdate)
				{
					//not implemented yet
					return false;
				}
			#endregion
			#region Completions
				public string[] getCampusNames()
				{
					return (from e in db.Campus
								select e.Name).Distinct().ToArray();
				}

				public string[] getClassesNames()
				{
					return (from e in db.Class
							select e.Name).Distinct().ToArray();
				}

				public string[] getPromotionsNames()
				{
					return (from e in db.Promotion
							select e.Name).Distinct().ToArray();
				}

				public string[] getPeriodsNames()
				{
					return (from p in db.Period
								select p.Name).Distinct().ToArray();
				}

				public string[] getSubjectsNames()
				{
					return db.Subject.Select(p => p.Name).ToArray();
				}

				public string[] getUsersNames()
				{
					return (from u in db.User
							select u.Name).Distinct().ToArray();
				}
				public string[] getModalities() 
				{
					return (from sub in db.Subject
								select sub.Modality).Distinct().ToArray();
				}
				public Dictionary<string, Dictionary<string, string[]>> getCampusPeriodClassTree()
				{
					return (from campus in db.Campus
								where campus.Classes.Any(p=>p.Period.Name!=null)
								select new {key=campus.Name, value=(
									from period in db.Period
									where period.Classes.Any(ca=>ca.Campus.Name==campus.Name)
									select new {periodName=period.Name, val=period.Classes.Where(p=>p.Campus==campus).Select(p=>p.Name)}
									)
								}
							).ToDictionary(p => p.key, p => p.value.ToDictionary(e => e.periodName, e => e.val.ToArray()));
				}
			#endregion
			#region Identified completion
				public IdName[] getIdCampusNames()
				{
 					return db.Campus.Select(p=>new {Id=p.Id, name=p.Name}).ToArray().Select(p=>IdName.IN(p.Id,p.name)).ToArray();
				}
				public IdName[] getIdClassesNames()
				{
					return db.Class.Select(p => new { Id = p.Id, name = p.Name }).ToArray().Select(p => IdName.IN(p.Id, p.name)).ToArray();
				}
				public IdName[] getIdPromotionsNames()
				{
					return db.Promotion.Select(p => new { Id = p.Id, name = p.Name }).ToArray().Select(p => IdName.IN(p.Id, p.name)).ToArray();
				}
				public IdName[] getIdPeriodsNames()
				{
					return db.Period.Select(p => new { Id = p.Id, name = p.Name }).ToArray().Select(p => IdName.IN(p.Id, p.name)).ToArray();
				}
				public Tuple<IdName, string>[] getIdSubjectsNamesModality()
				{
					return db.Subject.Select(p => new { Id = p.Id, name = p.Name, mod=p.Modality }).ToArray().Select(p => new Tuple<IdName,string>(IdName.IN(p.Id, p.name),p.mod)).ToArray();
				}
				public IdName[] getIdUsersNames()
				{
					return db.User.Select(p => new { Id = p.Id, name = p.Name }).ToArray().Select(p => IdName.IN(p.Id, p.name)).ToArray();
				}
				public Dictionary<IdName, Dictionary<IdName, IdName[]>> getIdCampusPeriodClassTree()
				{
					return (from campus in db.Campus
							where campus.Classes.Any(p => p.Period.Name != null)
							select new
							{
								campid = campus.Id,
								campname = campus.Name,
								value = (
									from period in db.Period
									where period.Classes.Any(ca => ca.Campus.Name == campus.Name)
									select new { periodid = period.Id, periodName = period.Name, 
												 val = period.Classes.Where(p => p.Campus == campus).Select(p => new { classid = p.Id, classname = p.Name }) 
											   }
									)
							}
							).ToDictionary(p => IdName.IN(p.campid, p.campname), 
								p => p.value.ToDictionary(
									e => IdName.IN(e.periodid, e.periodName),
									e => e.val.Select(f => IdName.IN(f.classid, f.classname)).ToArray()
								));
				}
			#endregion
			#region add
				public string addUser(string UserName, string UserPassword, string UserClassName)
				{
					if (db.User.Where(p=>p.Name==UserName).Count()>0)
					{
						return "L'User "+UserName+" existe déjà.";
					}
					if (db.Class.Where(p => p.Name == UserClassName).Count() < 1)
					{
						return "La Class "+UserClassName+" n'existe pas";
					}
					User u = new User();
					u.LastChange = DateTime.Now;
					u.Name = UserName;
					u.Password = UserPassword;
					u.Class = db.Class.Where(p => p.Name == UserClassName).First();
					db.AddToUser(u);
					db.SaveChanges();
					return "ok";
				}
				public string addCampus(string CampusName)
				{
					if (db.Campus.Where(p => p.Name == CampusName).Count() > 0)
						return "Le Campus " + CampusName + " existe déjà.";

					Campus c = new Campus();
					c.Name = CampusName;
					c.LastChange = DateTime.Now;
					c.Id = 2;
					db.AddToCampus(c);
					db.SaveChanges();
					return "ok";
				}
				public string addClass(string ClassName, string CampusName, string PeriodName)
				{
					if (db.Campus.FirstOrDefault(p => p.Name == CampusName) == null)
						return "Le Campus " + CampusName + " n'existe pas.";
					if (db.Period.FirstOrDefault(p => p.Name == PeriodName) == null)
						return "La Period " + PeriodName + " n'existe pas.";
					if (db.Class.Where(p => p.Name == ClassName).Count() > 0)
						return "La Class " + ClassName + " existe déjà.";

					Class c = new Class();
					c.Name = ClassName;
					c.LastChange = DateTime.Now;
					c.Period = db.Period.First(p => p.Name == PeriodName);
					c.Campus = db.Campus.First(p => p.Name == CampusName);
					db.AddToClass(c);
					db.SaveChanges();
					return "ok";
				}
				public string addPromotion(string PromotionName)
				{
					if (db.Promotion.Where(p => p.Name == PromotionName).Count() > 0)
						return "La Promotion " + PromotionName + " existe déjà.";
					Promotion prom = new Promotion();
					prom.LastChange = DateTime.Now;
					prom.Name = PromotionName;
					db.AddToPromotion(prom);
					db.SaveChanges();
					return "ok";
				}
				public string addSubject(string SubjectName, int SubjectHours, string Modality)
				{
					if (db.Subject.Where(p => p.Name == SubjectName).Count() > 0)
						return "La Subject " + SubjectName + " existe déjà.";
					Subject m = new Subject();
					m.Name = SubjectName;
					m.Hours = SubjectHours;
					m.Modality = Modality;
					db.AddToSubject(m);
					db.SaveChanges();
					return "ok";
				}
				public string addPeriod(string PeriodName, string PromoName, DateTime PeriodStart, DateTime PeriodEnd)
				{
					if (PeriodStart >= PeriodEnd)
						return "Dates incorrectes";
					if (db.Promotion.FirstOrDefault(p => p.Name == PromoName) == null)
						return "La Promo " + PromoName + " n'existe pas.";
					if (db.Period.Where(p => p.Name == PeriodName).Count() > 0)
						return "La Period " + PeriodName + " existe déjà.";
					Period per = new Period();
					per.Name = PeriodName;
					per.Start = PeriodStart;
					per.LastChange = DateTime.Now;
					per.End = PeriodEnd;
					per.Promotion = db.Promotion.First(p=>p.Name == PromoName);
					db.AddToPeriod(per);
					db.SaveChanges();
					return "ok";
				}
				public string grantNewRight(string UserName, string CampusName)
				{
					return "Toujours pas implementé";
				}
				public string addEventToCampus(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, IdName Campus, string Place)
				{
					if (Start >= End )
						return "Dates incorrectes";
					if ( db.Campus.FirstOrDefault(p => p.Id == Campus.Id) == null)
						return "Le Campus " + Campus.Name + " n'existe pas.";

					if (db.User.FirstOrDefault(p => p.Name == SpeakerName) == null)
						return "L'intervenant " + SpeakerName + " n'existe pas.";
					Event e = new Event();
					e.Name = EventName;
					e.Start = Start;
					e.End = End;
					e.Mandatory = Mandatory;
					e.Speaker= db.User.First(p=>p.Name==SpeakerName);
					e.Campus = db.Campus.First(p => p.Id == Campus.Id);
					e.Creator = db.User.FirstOrDefault(p => p.Name == "admin");
					e.Place = Place;
					db.AddToEvent(e);
					e.Campus.LastChange = DateTime.Now;
					db.SaveChanges();
					return "ok";
				}

				public string addEventToPeriode(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, IdName Period, string Place) 
				{
					if (Start >= End)
						return "Dates incorrectes";
					Period per = db.Period.FirstOrDefault(p => p.Id == Period.Id);
					if (per == null)
						return "La periode " + Period.Name + " n'existe pas.";

					if (db.User.FirstOrDefault(p => p.Name == SpeakerName) == null)
						return "L'intervenant " + SpeakerName + " n'existe pas.";
					Event e = new Event();
					e.Name = EventName;
					e.Start = Start;
					e.End = End;
					e.Mandatory = Mandatory;
					e.Speaker = db.User.First(p => p.Name == SpeakerName);
					e.Period = per;
					e.Creator = db.User.FirstOrDefault(p => p.Name == "admin");
					e.Place = Place;
					db.AddToEvent(e);
					per.LastChange = DateTime.Now;
					db.SaveChanges();
					return "ok";
				}

				public string addEventToClass(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, IdName Class, IdName Subject, string Place)
				{
					if (Start >= End)
						return "Dates incorrectes";

					Class c = db.Class.FirstOrDefault(p => p.Id == Class.Id);
					Subject s = db.Subject.FirstOrDefault(p=> p.Id == Subject.Id);
					if (c == null)
						return "Le Campus " + Class.Name + " n'existe pas.";

					if (db.User.FirstOrDefault(p => p.Name == SpeakerName) == null)
						return "L'intervenant " + SpeakerName + " n'existe pas.";
					Event e = new Event();
					e.Name = EventName;
					e.Start = Start;
					e.End = End;
					e.Mandatory = Mandatory;
					e.Speaker = db.User.First(p => p.Name == SpeakerName);
					e.Class = c;
					e.Subject = s;
					e.Creator = db.User.FirstOrDefault(p => p.Name == "admin");
					e.Place = Place;
					db.AddToEvent(e);
					c.LastChange = DateTime.Now;
					db.SaveChanges();
					return "ok";
				}

				public string addEventToUniversity(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName,  string Place)
				{
					if (Start >= End)
						return "Dates incorrectes";
					if (db.User.FirstOrDefault(p => p.Name == SpeakerName) == null)
						return "L'intervenant " + SpeakerName + " n'existe pas.";
					Event e = new Event();
					e.Name = EventName;
					e.Start = Start;
					e.End = End;
					e.Mandatory = Mandatory;
					e.Speaker = db.User.First(p => p.Name == SpeakerName);
					e.Creator = db.User.FirstOrDefault(p => p.Name == "admin");
					e.Place = Place;
					db.AddToEvent(e);
					db.University.First().LastChange = DateTime.Now;
					db.SaveChanges();
					return "ok";
				}

				public string addEventToUser(string EventName, DateTime Start, DateTime End, bool Mandatory, string Place)
				{ return "still not implemented"; }

			#endregion
			#region set
					public string setUser(UserData UD)
					{
						if (db.User.Any(p => p.Id != UD.Id != null))
							return "L'utilisateur n'existe pas";
						User u = db.User.Where(p => p.Id == UD.Id).Single();
						u.Name = UD.Name;
						if (UD.Password!=null)
							u.Password=UD.Password;
						u.Class = db.Class.Where(p => p.Id == UD.ClassId).FirstOrDefault();
						db.SaveChanges();
						return "ok";
					}
					public string setCampus(int Id, string CampusName)
					{
						if (db.Campus.Any(p => p.Id != Id != null))
							return "Le campus n'existe pas";
						Campus c = db.Campus.Where(p => p.Id == Id).Single();
						c.Name = CampusName;
						db.SaveChanges();
						return "ok";
					}
					public string setClass(ClassData CD)
					{
						if (db.Class.Any(p => p.Id != CD.Id) != null)
							return "La classe n'existe pas";
						if (db.Campus.Where(p => p.Name == CD.CampusName).FirstOrDefault() != null)
							return "Le Campus n'existe pas";
						if (db.Period.Where(p => p.Id == CD.Period).FirstOrDefault() != null)
							return "La Period n'existe pas";
						Class c = db.Class.Where(p => p.Id == CD.Id).Single();
						c.Name = CD.Name;
						c.Campus = db.Campus.Where(p => p.Name == CD.CampusName).FirstOrDefault();
						c.Period = db.Period.Where(p => p.Id == CD.Period).FirstOrDefault();
						db.SaveChanges();
						return "ok";
					}
					public string setPromotion(int Id, string PromotionName)
					{
						if (db.Promotion.Any(p => p.Id != Id))
							return "La Promotion n'existe pas";
						Promotion c = db.Promotion.Where(p => p.Id == Id).Single();
						c.Name = PromotionName;
						db.SaveChanges();
						return "ok";
					}
					public string setSubject(SubjectData SD)
					{
						if (db.Subject.Any(p => p.Id == SD.Id)!=null)
							return "Le sujet n'existe pas";;
						Subject s = db.Subject.Where(p => p.Id == SD.Id).Single();
						s.Name = SD.Name;
						s.Modality = SD.Modality;
						s.Hours = SD.Hours;
						db.SaveChanges();
						return "ok";
					}
					public string setPeriod(PeriodData PD)
					{
						if (db.Period.Any(p => p.Id == PD.Id) != null)
							return "Le period n'existe pas";
						if (db.Promotion.Where(p => p.Name == PD.PromotionName).FirstOrDefault() != null)
							return "La promotion n'existe pas";
						Period per = db.Period.Where(p => p.Id == PD.Id).Single();
						per.Name = PD.Name;
						per.Start = PD.Start;
						per.End = PD.End;
						per.Promotion=db.Promotion.Where(p => p.Name == PD.PromotionName).Single();
						db.SaveChanges();
						return "ok";
					}
					public string setEvent(EventData EditedEvent)
					{return "not implemented yet";}
			#endregion
			#region del
					public string delUser(int Id)
					{
						if(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name == "popi")
							return "test, tu es popi!";
						User u = db.User.Where(p => p.Id == Id).FirstOrDefault();
						if (u == null)
							return "l'utilisateur n'existe pas";
						db.DeleteObject(u);
						db.SaveChanges();
						return "ok";
					}
					public string delCampus(int Id)
					{
						Campus u = db.Campus.Where(p => p.Id == Id).FirstOrDefault();
						if (u == null)
							return "l'campus n'existe pas";
						db.DeleteObject(u);
						db.SaveChanges(); 
						return "ok";
					}
					public string delClass(int Id)
					{
						Class u = db.Class.Where(p => p.Id == Id).FirstOrDefault();
						if (u == null)
							return "la classe n'existe pas";
						db.DeleteObject(u);
						db.SaveChanges(); 
						return "ok";
					}
					public string delPromotion(int Id)
					{
						Promotion u = db.Promotion.Where(p => p.Id == Id).FirstOrDefault();
						if (u == null)
							return "la promotion n'existe pas";
						db.DeleteObject(u);
						db.SaveChanges(); 
						return "ok";
					}
					public string delSubject(int Id)
					{
						Subject u = db.Subject.Where(p => p.Id == Id).FirstOrDefault();
						if (u == null)
							return "la matière n'existe pas";
						db.DeleteObject(u);
						db.SaveChanges(); 
						return "ok";
					}
					public string delPeriod(int Id)
					{
						Period u = db.Period.Where(p => p.Id == Id).FirstOrDefault();
						if (u == null)
							return "la periode n'existe pas";
						db.DeleteObject(u);
						db.SaveChanges(); 
						return "ok";
					}
					public string delRight(string UserName, string CampusName)
					{return "not implemented yet";}
					public string delEvent(int Id)
					{
						Event u = db.Event.Where(p => p.Id == Id).FirstOrDefault();
						if (u == null)
							return "l' évennement n'existe pas";
						db.DeleteObject(u);
						db.SaveChanges();
						return "ok";
					}

			#endregion
			#region get Current

					public UserData getUser(int ID)
					{ 
						return db.User.Where(u => u.Id == ID).
						Select(p=> new {u=p, camp=p.Class.Campus.Name, clas=p.Class.Id }).ToArray().
						Select(u=>UserData.UD(u.u,u.camp,u.clas)).Single(); 
					}
					public ClassData getClass(int ID)
					{ 
						return db.Class.Where(c => c.Id == ID).
						Select(p=> new {u=p, camp=p.Campus.Name, per=p.Period.Id }).ToArray().
						Select(u=>ClassData.CD(u.u,u.camp,u.per)).Single(); 
					}
					public PeriodData getPeriod(int ID)
					{ 
						return db.Period.Where(u => u.Id == ID).
						Select(p=> new {u=p, prom=p.Promotion.Name}).ToArray().
						Select(u=>PeriodData.PD(u.u,u.prom)).Single(); 
					}
					public SubjectData getSubject(int ID)
					{ 
						return SubjectData.SD(db.Subject.Where(u => u.Id == ID).Single()); 
					}
			#endregion
		#endregion

		#region IDisposable Members
			public void Dispose()
			{
				this.Dispose(true);
			}

			protected virtual void Dispose(bool fullyDispose)
			{
				this.db.SaveChanges();
				this.db.Dispose();
				if (fullyDispose == true)
					GC.SuppressFinalize(this);
			}
		#endregion

	}
}
