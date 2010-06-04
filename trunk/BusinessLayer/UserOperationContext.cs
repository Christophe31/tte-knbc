using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataAccessLayer;

namespace BusinessLayer
{
	public class UserOperationContext:IExtension<OperationContext>
	{
		public static UserOperationContext Current
		{
			get { return OperationContext.Current.Extensions.Find<UserOperationContext>(); }
		}	

		#region IExtension<OperationContext> Members
		void IExtension<OperationContext>.Attach(OperationContext owner)
		{
		}

		void IExtension<OperationContext>.Detach(OperationContext owner)
		{
		}
		#endregion

		public UserData UserProperty {get;set; }
	}
}