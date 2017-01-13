namespace CentralSystem.Framework.ServiceModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel.Web;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Utilities according to WCF contexts
    /// </summary>
    public static class ServiceContextUtils
    {

        #region Methods

        /// <summary>
        /// Try header from web context
        /// </summary>
        /// <param name="headerName">Header name</param>
        /// <param name="headerValue">Result header value</param>
        /// <returns>True - header was found</returns>
        public static bool TryGetHeaderValue(string headerName, out string headerValue)
        {
            bool foundHeaderFlag = false;
            headerValue = null;

            if (WebOperationContext.Current != null
                && WebOperationContext.Current.IncomingRequest != null
                && WebOperationContext.Current.IncomingRequest.Headers != null)
            {
                headerValue = WebOperationContext.Current.IncomingRequest.Headers[headerName];
                foundHeaderFlag = !string.IsNullOrEmpty(headerValue);
            }

            return foundHeaderFlag;
        }

        #endregion

    }
}
