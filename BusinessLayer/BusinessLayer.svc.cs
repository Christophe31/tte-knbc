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
		protected DataModel.TteDB db;
		public BusinessLayer()
		{
			db = new TteDB();
		}

		#region IBusinessLayer Members

		public EventData[] getEventsData_Campus(string CampusName, DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return (from e in
						(from p in db.Evenement
						 where p.Debut < Stop && p.Fin > Start &&
							 p.Campus.Nom == CampusName &&
							 p.Createur.LastChange > LastUpdate
						 select new { e=p, m=p.Matiere }).ToArray()
					select EventData.ED(e.e, e.m)).ToArray();
		}

		public EventData[] getEventsData_University(DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return (from e in
						(from p in db.Evenement
						 where p.Debut < Stop && p.Fin > Start &&
							 p.Campus == null && p.Periode == null && p.Classe == null &&
							 p.Createur.LastChange > LastUpdate
						 select new { e = p, m = p.Matiere }).ToArray()
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
						 select new { e = p, m = p.Matiere }).ToArray()
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
						 select new { e = p, m = p.Matiere }).ToArray()
					select EventData.ED(e.e, e.m)).ToArray();
		}

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
