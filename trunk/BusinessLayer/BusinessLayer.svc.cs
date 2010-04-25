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
	public class BusinessLayer : IBusinessLayer, IDisposable
	{
		protected TteDataBase db;
		public BusinessLayer()
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
								 select new { evt = p, sub = p.Subject.Name, spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub, e.spk)).ToArray();
				}

				public EventData[] getEventsByUniversity(DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
									 p.Campus == null && p.Period == null && p.Class == null &&
									 db.University.First().LastChange > LastUpdate
								 select new { evt = p, sub = p.Subject.Name,spk=p.Speaker.Name  }).ToArray()
							select EventData.ED(e.evt, e.sub,e.spk)).ToArray(); 
				}

				public EventData[] getEventsByPeriod(string PeriodName, DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
									p.Period.Name == PeriodName &&
									p.Period.LastChange > LastUpdate
								 select new { evt = p, sub = p.Subject.Name, spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub, e.spk)).ToArray();			
				}

				public EventData[] getEventsByClass(string ClassName, DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Event
								 where p.Start < Stop && p.End > Start &&
									p.Class.Name ==ClassName &&
									p.Class.LastChange > LastUpdate
								 select new { evt = p, sub = p.Subject.Name, spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub, e.spk)).ToArray();
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
								 select new { evt = p, sub = p.Subject.Name, spk = p.Speaker.Name }).ToArray()
							select EventData.ED(e.evt, e.sub, e.spk)).ToArray();
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
			#endregion
			#region Ecriture
				public string addUser(string UserName, string UserPassword, string UserClassName)
				{
					if (db.User.Where(p=>p.Name==UserName).Count()>0)
					{
						return "L'User "+UserName+" existe déjà.";
					}
					//warn
					Class c = db.Class.FirstOrDefault(p => p.Name == UserClassName);
					if (c == null)
					{
						return "La Class "+UserClassName+" n'existe pas";
					}
					User u = new User();
					u.LastChange = DateTime.Now;
					u.Name = UserName;
					u.Password = UserPassword;
					u.Class=c;
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
				public string addSubject(string SubjectName, int SubjectHours)
				{
					if (db.Subject.Where(p => p.Name == SubjectName).Count() > 0)
						return "La Subject " + SubjectName + " existe déjà.";
					Subject m = new Subject();
					m.Name = SubjectName;
					m.Hours = SubjectHours;
					db.AddToSubject(m);
					db.SaveChanges();
					return "ok";
				}
				public string addPeriod(string PeriodName, string PromoName, DateTime PeriodStart, DateTime PeriodEnd)
				{
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
				public string grantNewRight(string UserName, int Type, string CampusName)
				{
					return "Toujours pas implementé";
				}
				public string addEvent(string EventName, DateTime Start, DateTime End, bool Obligatoire, string IntervenatName, string CampusName, string PeriodName, string SubjectName, string Type, string Lieu)
				{
//TODO:Need Sessions
					return "Toujours pas implementé";
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
