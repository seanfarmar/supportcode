namespace Endpoint 
{
    using NServiceBus;

    /*
		This class configures this endpoint as a Server. More information about how to configure the NServiceBus host
		can be found here: http://particular.net/articles/profiles-for-nservicebus-host
	*/
	public class EndpointConfig : IConfigureThisEndpoint, AsA_Server, IWantCustomInitialization
    {
	    public void Init()
	    {
	        Configure.With()
                .DefaultBuilder()
                .DefiningCommandsAs(t => t.Namespace != null && 
                    !t.IsInterface && t.Name.EndsWith("Command"))
                .DefiningMessagesAs(t => t.Namespace != null && 
                    !t.IsInterface && t.Name.EndsWith("Notification"));
        }
    }

    public interface IDeepCopy<T>
    {
        T DeepCopy();
    }

    public class TestCommand
    {
        public IUserContext UserContext { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }

    }

    public interface IUserContext : IDeepCopy<IUserContext>
    {
        int Id { get; set; }
        string Description { get; set; }
    }

    public class UserContext : IUserContext
    {
        public IUserContext DeepCopy()
        {
            return new UserContext
            {
                Id = Id,
                Description = Description
            };
        }

        public int Id { get; set; }
        public string Description { get; set; }
    }
}