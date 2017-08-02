using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CentralSystem.Framework.Utils
{
    public class HttpUtils
    {
        private const int HTTP_ADDRESS_ALIVE_STATUS = 200;

        public bool IsAddressAlive(Uri testUrl)
        {
            bool isAlive = false;

            try
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(testUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if ((int)response.StatusCode == HTTP_ADDRESS_ALIVE_STATUS)
                {
                    isAlive = true;
                }
            }
            catch
            {
                
            }

            return isAlive;
        } 
    }
}
