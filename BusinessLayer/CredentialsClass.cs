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
	public class AuthValidator : UserNamePasswordValidator
	{
		DataAccessLayer.Entities db = new DataAccessLayer.Entities();
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

	}
}