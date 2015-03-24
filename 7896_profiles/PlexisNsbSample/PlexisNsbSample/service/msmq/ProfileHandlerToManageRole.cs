using System;
using NServiceBus;
using NServiceBus.Hosting.Profiles;

namespace PlexisNsbSample
{
	public class ProfileHandlerToManageRole :
		IHandleProfile<Lite>,
		IHandleProfile<Production>,
		IWantTheEndpointConfig
	{
		public void ProfileActivated()
		{
			RoleManager.ConfigureRole(Config);
		}

		public IConfigureThisEndpoint Config { get; set; }
	}
}