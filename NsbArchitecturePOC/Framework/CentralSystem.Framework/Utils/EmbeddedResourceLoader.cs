using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CentralSystem.Framework.Utils
{
    public static class EmbeddedResourceLoader
    {
        /// <summary>
        /// Load RDL file.
        /// </summary>
        /// <param name="fileResourceName"></param>
        /// <returns></returns>
        public static Stream LoadResource(string fileResourceName, Assembly executingAssembly)
        {
            string resourceFullName = GetResourceFullName(fileResourceName, executingAssembly);

            return executingAssembly.GetManifestResourceStream(resourceFullName);
        }

        /// <summary>
        /// Fetches RDL file from embedded resources
        /// </summary>
        /// <param name="fileResourceName"></param>
        /// <returns></returns>
        private static string GetResourceFullName(string fileResourceName, Assembly executingAssembly) 
        {
            foreach (var resourceFullName in executingAssembly.GetManifestResourceNames())
            {
                if (resourceFullName.EndsWith(string.Format(".{0}", fileResourceName), StringComparison.OrdinalIgnoreCase))
                {
                    return resourceFullName;
                }
            }

            throw new ArgumentException(string.Format("Can't find embedded resource {0}", fileResourceName));
        }
    }
}
