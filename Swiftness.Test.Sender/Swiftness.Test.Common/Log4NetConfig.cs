using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swiftness.Test.Common
{
    public class Log4NetConfig
    {

        public void Start()
        {
            init();
            log4net.Config.XmlConfigurator.Configure();

        }

        private void init()
        {
            string invokerName = string.Empty;
            string processName = Process.GetCurrentProcess().ProcessName;

            // specific case - add invoker name
            if (processName.Contains("Invoker"))
            {
                try
                {
                    string[] args = Environment.GetCommandLineArgs();
                    if (args != null && args.Length > 1 && args[1] != null)
                    {
                        invokerName = args[1].Replace(@"/", "").Replace(@"\", "");
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            if (!string.IsNullOrEmpty(invokerName))
            {
                log4net.GlobalContext.Properties["ProcName"] = string.Concat(invokerName);
            }
            else
            {
                log4net.GlobalContext.Properties["ProcName"] = processName;
            }
        }

        public void Start(string configFilePath)
        {
            init();
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(@configFilePath));

        }
    }

}
