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

			return db.Evenement.
				Where(p => p.Debut < Stop && p.Fin > Start).
				Where(p => p.Campus.Nom == CampusName).
				Where(p => p.Createur.LastChange > LastUpdate).
				Select(p => new EventData(p)).ToArray();
		}

		public EventData[] getEventsData_University(DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return db.Evenement.
				Where(p => p.Debut < Stop && p.Fin > Start).
				Where(p => p.Campus == null && p.Periode == null && p.Classe == null).
				Where(p => p.Createur.LastChange > LastUpdate).
				Select(p => new EventData(p)).ToArray();
		}

		public EventData[] getEventsData_Periode(string PromoPeriodeName, DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return db.Evenement.
				Where(p => p.Debut < Stop && p.Fin > Start).
				Where(p => (p.Periode.Nom == PromoPeriodeName.Split('-')[1] && p.Periode.Promotion.Nom == PromoPeriodeName.Split('-')[0])).
				Where(p => p.Createur.LastChange > LastUpdate).
				Select(p => new EventData(p)).ToArray();
		}

		public EventData[] getEventsData_Class(string ClassName, string CampusName, string PromoPeriodeName, DateTime Start, DateTime Stop, DateTime LastUpdate)
		{
			return db.Evenement.
				Where(p => p.Debut < Stop && p.Fin > Start).
				Where(p => (p.Classe ==
						db.Classe.
						Where(c => (c.Campus.Nom == CampusName &&
							  c.nom == ClassName &&
							  c.Periode.Nom == PromoPeriodeName.Split('-')[1])).First())
					  ).
				Where(p => p.Createur.LastChange > LastUpdate).
				Select(p => new EventData(p)).ToArray();
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
