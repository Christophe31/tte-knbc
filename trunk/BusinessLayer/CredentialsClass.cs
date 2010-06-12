using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Security.Principal;
using System.IdentityModel.Selectors;
using System.IdentityModel;
using DataAccessLayer;

namespace BusinessLayer
{
	/// <summary>
	/// Validation du login/mot de passe
	/// </summary>
	public sealed class AuthValidator : UserNamePasswordValidator, IDisposable
	{
		DataAccessLayer.Entities db = new DataAccessLayer.Entities();
		/// <summary>
		/// Validation du login mot de passe
		/// </summary>
		/// <param name="userName">nom de l'utilisateur</param>
		/// <param name="password">mot de passe.</param>
		public override void Validate(string userName, string password)
		{
			if (!db.User.Any(p=>p.Login==userName && p.Password == password))
			{
				// This throws an informative fault to the client.
				throw new FaultException("Unknown Username or Incorrect Password");
				// When you do not want to throw an infomative fault to the client,
				// throw the following exception.
				// throw new SecurityTokenException("Unknown Username or Incorrect Password");
			}
		}


		#region IDisposable Members

		/// <summary>
		/// Dispose the db properly
		/// </summary>
		public void Dispose()
		{
			db.Dispose();
			GC.SuppressFinalize(this);
		}

		#endregion
	}
}