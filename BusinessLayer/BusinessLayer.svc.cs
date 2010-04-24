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
				public EventData[] getEventsData_Campus(string CampusName, DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Evenement
								 where p.Debut < Stop && p.Fin > Start &&
									 p.Campus.Nom == CampusName &&
									 p.Createur.LastChange > LastUpdate
								 select new { e=p, m=p.Matiere.Nom }).ToArray()
							select EventData.ED(e.e, e.m)).ToArray();
				}

				public EventData[] getEventsData_University(DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Evenement
								 where p.Debut < Stop && p.Fin > Start &&
									 p.Campus == null && p.Periode == null && p.Classe == null &&
									 p.Createur.LastChange > LastUpdate
								 select new { e = p, m = p.Matiere.Nom }).ToArray()
							select EventData.ED(e.e, e.m)).ToArray(); 
				}

				public EventData[] getEventsData_Periode(string PromoPeriodeName, DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Evenement
								 where p.Debut < Stop && p.Fin > Start &&
									p.Periode.Nom == PromoPeriodeName.Split('-')[1] &&
									p.Periode.Promotion.Nom == PromoPeriodeName.Split('-')[0] &&
									p.Createur.LastChange > LastUpdate
								 select new { e = p, m = p.Matiere.Nom }).ToArray()
							select EventData.ED(e.e, e.m)).ToArray();
			
				}

				public EventData[] getEventsData_Class(string ClassName, string CampusName, string PromoPeriodeName, DateTime Start, DateTime Stop, DateTime LastUpdate)
				{
					return (from e in
								(from p in db.Evenement
								 where p.Debut < Stop && p.Fin > Start &&
									p.Classe ==
									(from c in db.Classe
										where c.Campus.Nom == CampusName &&
											c.nom == ClassName &&
											c.Periode.Nom == PromoPeriodeName.Split('-')[1]
										select c
											).First() &&
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
				public string addUser(string UserName, string UserPassword, string UserCampusName)
				{
					Utilisateur u = new Utilisateur();
					u.LastChange = DateTime.Now;
					u.Nom = UserName;
					u.Password = UserPassword;
					//!!!!!
					//TODO: modifier cette ligne de test!
					u.Classe=db.Classe.First();
					db.SaveChanges();
					return "ok";
				}
				public string addCampus(string CampusName)
				{
					throw new NotImplementedException();
				}
				public string addClasse(string ClassName, string ClassCampusName, string ClassPeriode)
				{
					throw new NotImplementedException();
				}
				public string addPromotion(string UserName, string UserPassword, string UserCampusName)
				{
					throw new NotImplementedException();
				}
				public string addMatiere(string UserName, string UserPassword, string UserCampusName)
				{
					throw new NotImplementedException();
				}
				public string addPeriode(string UserName, string UserPassword, string UserCampusName)
				{
					throw new NotImplementedException();
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
