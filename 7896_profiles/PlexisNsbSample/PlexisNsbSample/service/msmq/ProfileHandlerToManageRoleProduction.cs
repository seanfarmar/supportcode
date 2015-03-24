namespace PlexisNsbSample
{
    using NServiceBus;
    using NServiceBus.Hosting.Profiles;

    public class ProfileHandlerToManageRoleProduction :
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