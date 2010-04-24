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
		protected TteDB db;
		public BusinessLayer()
		{
			db = new TteDB();
		}

		#region IBusinessLayer Members
			#region Lecture d'évènements
				public EventData[] getEventsByCampus(string CampusName, DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Evenement
								 where p.Debut < Stop && p.Fin > Start &&
									 p.Campus.Nom == CampusName &&
									 p.Createur.LastChange > LastUpdate
								 select new { e=p, m=p.Matiere.Nom }).ToArray()
							select EventData.ED(e.e, e.m)).ToArray();
				}

				public EventData[] getEventsByUniversity(DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Evenement
								 where p.Debut < Stop && p.Fin > Start &&
									 p.Campus == null && p.Periode == null && p.Classe == null &&
									 p.Createur.LastChange > LastUpdate
								 select new { e = p, m = p.Matiere.Nom }).ToArray()
							select EventData.ED(e.e, e.m)).ToArray(); 
				}

				public EventData[] getEventsByPeriode(string PeriodeName, DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Evenement
								 where p.Debut < Stop && p.Fin > Start &&
									p.Periode.Nom == PeriodeName &&
									p.Createur.LastChange > LastUpdate
								 select new { e = p, m = p.Matiere.Nom }).ToArray()
							select EventData.ED(e.e, e.m)).ToArray();
			
				}

				public EventData[] getEventsByClass(string ClassName, DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Evenement
								 where p.Debut < Stop && p.Fin > Start &&
									p.Classe.nom ==ClassName &&
									p.Createur.LastChange > LastUpdate
								 select new { e = p, m = p.Matiere.Nom }).ToArray()
							select EventData.ED(e.e, e.m)).ToArray();
				}
			#endregion
			#region Completions
				public string[] getCampusNames()
				{
					return (from e in db.Campus
								select e.Nom).Distinct().ToArray();
				}

				public string[] getClassesNames()
				{
					return (from e in db.Classe
							select e.nom).Distinct().ToArray();
				}

				public string[] getPromotionsNames()
				{
					return (from e in db.Promotion
							select e.Nom).Distinct().ToArray();
				}

				public string[] getPromoPeriodeNames()
				{
					return (from e in db.Periode
							select e.Nom +'-'+ e.Promotion.Nom).Distinct().ToArray();
				}
			#endregion
			#region Ecriture
				public string addUser(string UserName, string UserPassword, string UserClassName)
				{
					if (db.Utilisateur.Where(p=>p.Nom==UserName).Count()>0)
					{
						return "L'utilisateur "+UserName+" existe déjà.";
					}
					//warn
					Classe c = db.Classe.FirstOrDefault(p => p.nom == UserClassName);
					if (c == null)
					{
						return "La classe "+UserClassName+" n'existe pas";
					}
					Utilisateur u = new Utilisateur();
					u.LastChange = DateTime.Now;
					u.Nom = UserName;
					u.Password = UserPassword;
					u.Classe=c;
					db.SaveChanges();
					return "ok";
				}
				public string addCampus(string CampusName)
				{
					if (db.Campus.Where(p => p.Nom == CampusName).Count() > 0)
					{
						return "Le Campus " + CampusName + " existe déjà.";
					}
					Campus c = new Campus();
					c.Nom = CampusName;
					db.SaveChanges();
					return "ok";
				}
				public string addClasse(string ClassName, string CampusName, string PeriodeName)
				{
					if (db.Campus.FirstOrDefault(p => p.Nom == CampusName) == null)
						return "Le Campus " + CampusName + " n'existe pas.";
					if (db.Periode.FirstOrDefault(p => p.Nom == PeriodeName) == null)
						return "Le Campus " + PeriodeName + " n'existe pas.";
					if (db.Classe.Where(p => p.nom == ClassName).Count() > 0)
						return "La classe " + ClassName + " existe déjà.";

					Classe c = new Classe();
					c.nom = ClassName;
					c.Periode = db.Periode.First(p => p.Nom == PeriodeName);
					c.Campus = db.Campus.First(p => p.Nom == CampusName);
					db.SaveChanges();
					return "ok";
				}
				public string addPromotion(string PromotionName)
				{
					if (db.Promotion.Where(p => p.Nom == PromotionName).Count() > 0)
						return "La Promotion " + PromotionName + " existe déjà.";
					Promotion prom = new Promotion();
					prom.Nom = PromotionName;
					db.SaveChanges();
					return "ok";
				}
				public string addMatiere(string MatiereName, int MatiereHours)
				{
					return "Toujours pas implementé";
				}
				public string addPeriode(string PeriodeName, string PromoName, DateTime PeriodeStart, DateTime PeriodeEnd)
				{
					return "Toujours pas implementé";
				}
				public string grantNewRight(string UserName, int Type, string CampusName)
				{
					return "Toujours pas implementé";
				}
				public string addEvent(string EventName, DateTime Start, DateTime End, bool Obligatoire, string IntervenatName, string CampusName, string PeriodeName, string MatiereName, string Type, string Lieu)
				{
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
