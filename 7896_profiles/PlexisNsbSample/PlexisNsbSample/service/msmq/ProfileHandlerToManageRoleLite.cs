namespace PlexisNsbSample
{
    using NServiceBus;
    using NServiceBus.Hosting.Profiles;

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