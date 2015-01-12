namespace PortalWebAccess
{
    using System.ComponentModel;
    using System.Web.Services;

    /// <summary>
    ///     Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : WebService
    {
        [WebMethod]
        public bool InsideGracePeriode()
        {
            return true;
        }
    }
}