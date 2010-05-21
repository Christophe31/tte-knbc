using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using System.Security.Principal;
using System.IdentityModel.Selectors;
using System.IdentityModel;
using DataModel;

namespace BusinessLayer
{
	public class AuthValidator : UserNamePasswordValidator
	{

		TteDataBase db = new TteDataBase();
		public override void Validate(string userName, string password)
		{
			if (db.User.Where(p=>p.Name==userName && p.Password == password).FirstOrDefault()==null)
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