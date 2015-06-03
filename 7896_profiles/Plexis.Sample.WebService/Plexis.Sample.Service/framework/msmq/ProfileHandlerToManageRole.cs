using NServiceBus;
using NServiceBus.Hosting.Profiles;

namespace Plexis.Sample.Service
{
	public class ProfileHandlerToManageRoleLite :
		IHandleProfile<Lite>,
		IWantTheEndpointConfig
	{
		public void ProfileActivated()
		{
			RoleManager.ConfigureRole(Config);
		}

		public IConfigureThisEndpoint Config { get; set; }
	}

}