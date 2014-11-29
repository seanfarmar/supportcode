using Microsoft.WindowsAzure.ServiceRuntime;
using NServiceBus.Hosting.Azure;

namespace DataSync.HQ.HQStoreMessageProcessor
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly NServiceBusRoleEntrypoint _nsb = new NServiceBusRoleEntrypoint();

        public override bool OnStart()
        {
            _nsb.Start();
            bool result = base.OnStart();
            return result;
        }

        public override void OnStop()
        {
            _nsb.Stop();
            base.OnStop();
        }
    }
}