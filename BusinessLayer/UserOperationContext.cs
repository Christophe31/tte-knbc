using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
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

		UserData userProperty;
		public UserData UserProperty { get { return userProperty; } set { userProperty = value; } }
	}

	public class UserContextMessageInspector : IDispatchMessageInspector
	{
		public object AfterReceiveRequest(ref System.ServiceModel.Channels.Message request,
										  IClientChannel channel,
										  InstanceContext instanceContext)
		{
			OperationContext.Current.Extensions.Add(new UserOperationContext());
			return request.Headers.MessageId;
		}

		public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
		{
			OperationContext.Current.Extensions.Remove(UserOperationContext.Current);
		}
	}

	public class UserContextBehaviorAttribute : Attribute, IServiceBehavior
	{
		#region IServiceBehavior Members

		public void AddBindingParameters(ServiceDescription serviceDescription,
								ServiceHostBase serviceHostBase,
								System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints,
								BindingParameterCollection bindingParameters)
		{
			//no-op
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcher cd in serviceHostBase.ChannelDispatchers)
			{
				foreach (EndpointDispatcher ed in cd.Endpoints)
				{
					ed.DispatchRuntime.MessageInspectors.Add(new UserContextMessageInspector());
				}
			}
		}

		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			//no-op
		}

		
		#endregion
	}
}
