using System.Web.Services;
using NServiceBus;
using Plexis.Sample.Messages;
using log4net;


namespace Plexis.Sample.WebService
{
	/// <summary>
	/// Summary description for CountdownRequest
	/// </summary>
	[WebService(Namespace = "http://plexissample.com")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	public class CountdownRequest : Webservice<Countdown, ReturnCodes>
	{
	}
}
