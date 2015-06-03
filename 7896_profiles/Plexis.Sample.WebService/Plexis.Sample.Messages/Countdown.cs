using NServiceBus;

namespace Plexis.Sample.Messages
{
	public class Countdown : IMessage
	{
	}

	public enum ReturnCodes
	{
		None, Fail
	}
}
