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
		protected TteDataBase db;
		public BusinessService()
		{
			db = new TteDataBase();
		}

		#region IBusinessLayer Members
			#region Lecture d'évènements
				public EventData[] getEventsByCampus(string CampusName, DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
									 p.Campus.Name == CampusName &&
									 p.Campus.LastChange > LastUpdate
								 select new { evt = p, sub = p.Subject.Name,mod=p.Subject.Modality , spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub,e.mod, e.spk)).ToArray();
				}

				public EventData[] getEventsByUniversity(DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
									 p.Campus == null && p.Period == null && p.Class == null &&
									 db.University.First().LastChange > LastUpdate
								 select new { evt = p, sub = p.Subject.Name, mod = p.Subject.Modality, spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub, e.mod, e.spk)).ToArray();
				}

				public EventData[] getEventsByPeriod(string PeriodName, DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
									p.Period.Name == PeriodName &&
									p.Period.LastChange > LastUpdate
								 select new { evt = p, sub = p.Subject.Name, mod = p.Subject.Modality, spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub, e.mod, e.spk)).ToArray();
				}

				public EventData[] getEventsByClass(string ClassName, DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
									p.Class.Name ==ClassName &&
									p.Class.LastChange > LastUpdate
								 select new { evt = p, sub = p.Subject.Name, mod = p.Subject.Modality, spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub, e.mod, e.spk)).ToArray();
				}

				public EventData[] getPrivateEvents(DateTime Start, DateTime Stop, DateTime LastUpdate) 
				{
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
//TODO:Modifier quand les sessions seront gérées
									p.Creator.Name == "christophe"  &&
									p.Speaker == null &&
									p.Creator.LastChange > LastUpdate
								 select new { evt = p, sub = p.Subject.Name, mod = p.Subject.Modality, spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub, e.mod, e.spk)).ToArray();
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
					/// Dictionary<campus, Dictionary<period, classes[]>>
					return (from campus in db.Campus
								where campus.Classes.Any(p=>p.Period.Name!=null)
								select new {key=campus.Name, value=(
									from period in db.Period
									where period.Class.Any(ca=>ca.Campus.Name==campus.Name)
									select new {periodName=period.Name, val=period.Class.Where(p=>p.Campus==campus).Select(p=>p.Name)}
									)
								}
							).ToDictionary(p => p.key, p => p.value.ToDictionary(e => e.periodName, e => e.val.ToArray()));
				}
			#endregion
			#region Identified completion
				public Tuple<int, string>[] getIdCampusNames()
				{
 					return db.Campus.Select(p=>new Tuple<int,string>(p.Id,p.Name)).ToArray();
				}
				public Tuple<int, string>[] getIdClassesNames()
				{
 					return db.Class.Select(p=>new Tuple<int,string>(p.Id,p.Name)).ToArray();
				}
				public Tuple<int, string>[] getIdPromotionsNames()
				{
					return db.Promotion.Select(p => new Tuple<int, string>(p.Id, p.Name)).ToArray();
				}
				public Tuple<int, string>[] getIdPeriodsNames()
				{
 					return db.Period.Select(p=>new Tuple<int,string>(p.Id,p.Name)).ToArray();
				}
				public Tuple<int, string, string>[] getIdSubjectsNamesModality()
				{
 					return db.Subject.Select(p=>new Tuple<int,string,string>(p.Id,p.Name, p.Modality)).ToArray();
				}
				public Tuple<int, string>[] getIdUsersNames()
				{
 					return db.User.Select(p=>new Tuple<int,string>(p.Id,p.Name)).ToArray();
				}
				public Dictionary<Tuple<int, string>, Dictionary<Tuple<int, string>, Tuple<int, string>[]>> getIdCampusPeriodClassTree()
				{
					return (from campus in db.Campus
								where campus.Classes.Any(p=>p.Period.Name!=null)
								select new {key=new Tuple<int,string>(campus.Id,campus.Name), value=(
									from period in db.Period
									where period.Class.Any(ca=>ca.Campus.Name==campus.Name)
									select new {periodName=new Tuple<int,string>(period.Id, period.Name), val=period.Class.Where(p=>p.Campus==campus).Select(p=>new Tuple<int,string>(p.Id,p.Name))}
									)
								}
							).ToDictionary(p => p.key, p => p.value.ToDictionary(e => e.periodName, e => e.val.ToArray()));
				}
			#endregion
			#region add
                /// <summary>
                /// Ne fonctonne pas
                /// </summary>
                /// <param name="UserName"></param>
                /// <param name="UserPassword"></param>
                /// <param name="UserClassName"></param>
                /// <returns></returns>
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
				public string addEventToCampus(string EventName, DateTime Start, DateTime End, bool Mandatory, 
					string SpeakerName, string CampusName, string Place)
				{
					if (Start >= End )
						return "Dates incorrectes";
					if ( db.Campus.FirstOrDefault(p => p.Name == CampusName) == null)
						return "Le Campus " + CampusName + " n'existe pas.";

					if (db.User.FirstOrDefault(p => p.Name == SpeakerName) == null)
						return "L'intervenant " + SpeakerName + " n'existe pas.";
					Event e = new Event();
					e.Name = EventName;
					e.Start = Start;
					e.End = End;
					e.Mandatory = Mandatory;
					e.Speaker= db.User.First(p=>p.Name==SpeakerName);
					e.Campus = db.Campus.First(p => p.Name == CampusName);
					e.Creator = db.User.FirstOrDefault(p => p.Name == "admin");
					e.Place = Place;
					db.AddToEvent(e);
					e.Campus.LastChange = DateTime.Now;
					db.SaveChanges();
					return "ok";
				}

				public string addEventToPeriode(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string PeriodName, string Place) 
				{
					if (Start >= End)
						return "Dates incorrectes";
					Period per = db.Period.FirstOrDefault(pi => pi.Name == PeriodName);
					if (per == null)
						return "Le Campus " + PeriodName + " n'existe pas.";

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

				public string addEventToClass(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string ClassName,string Subject ,string Modality, string Place)
				{
					if (Start >= End)
						return "Dates incorrectes";

					Class c = db.Class.FirstOrDefault(p => p.Name == ClassName);
					Subject s = db.Subject.FirstOrDefault(p=> p.Name == Subject && p.Modality==Modality);
					if (c == null)
						return "Le Campus " + ClassName + " n'existe pas.";

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
					public Tuple<string> setUser(string UserName, string UserPassword, string UserClassName)
					{return new Tuple<string>("not implemented yet");}
					public string setCampus(string CampusName)
					{return "not implemented yet";}
					public string setClass(string ClassName, string CampusName, string PeriodeName)
					{return "not implemented yet";}
					public string setPromotion(string PromotionName)
					{return "not implemented yet";}
					public string setSubject(string SubjectName, int Hours)
					{return "not implemented yet";}
					public string setPeriod(string PeriodName, string PromotionName, DateTime PeriodStart, DateTime PeriodEnd)
					{return "not implemented yet";}
					public string setEventToCampus(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string CampusName, string Place)
					{return "not implemented yet";}
					public string setEventToPeriode(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string PeriodeName, string Place)
					{return "not implemented yet";}
					public string setEventToClass(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string ClassName, string Subject, string Modality, string Place)
					{return "not implemented yet";}
					public string setEventToUniversity(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string Place)
					{return "not implemented yet";}
					public string setEventToUser(string EventName, DateTime Start, DateTime End, bool Mandatory, string Place)
					{return "not implemented yet";}
				#endregion
			#region del
					public string delUser(string UserName)
					{return "not implemented yet";}
					public string delCampus(string CampusName)
					{return "not implemented yet";}
					public string delClass(string ClassName, string CampusName, string PeriodeName)
					{return "not implemented yet";}
					public string delPromotion(string PromotionName)
					{return "not implemented yet";}
					public string delSubject(string SubjectName, int Hours)
					{return "not implemented yet";}
					public string delPeriod(string PeriodName, string PromotionName, DateTime PeriodStart, DateTime PeriodEnd)
					{return "not implemented yet";}
					public string delRight(string UserName, string CampusName)
					{return "not implemented yet";}
					public string delEventToCampus(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string CampusName, string Place)
					{return "not implemented yet";}
					public string delEventToPeriode(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string PeriodeName, string Place)
					{return "not implemented yet";}
					public string delEventToClass(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string ClassName, string Subject, string Modality, string Place)
					{return "not implemented yet";}
					public string delEventToUniversity(string EventName, DateTime Start, DateTime End, bool Mandatory, string SpeakerName, string Place)
					{return "not implemented yet";}
					public string delEventToUser(string EventName, DateTime Start, DateTime End, bool Mandatory, string Place)
					{return "not implemented yet";}
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
