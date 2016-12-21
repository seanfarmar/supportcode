using NServiceBus;
using NServiceBus.Features;
using NServiceBus.Persistence;
using System;
using System.Threading.Tasks;

namespace Payveris.ScheduledPaymentsListener
{
    public class EndpointConfig : IConfigureThisEndpoint
    {
        public void Customize(EndpointConfiguration configuration)
        {
            configuration.UsePersistence<NHibernatePersistence>()
                //.ConnectionString(@"Data Source=CRBDB12;Initial Catalog=RLACHReturns;Integrated Security=True;MultipleActiveResultSets=True");
                .ConnectionString(System.Configuration.ConfigurationManager.ConnectionStrings["NSBPersistence"].ConnectionString);
            configuration.UseTransport<MsmqTransport>();
            configuration.UseSerialization<JsonSerializer>();
            configuration.DisableFeature<Audit>();
            configuration.DisableFeature<AutoSubscribe>();
            configuration.DisableFeature<Sagas>();
        }
    }

    public class Initialization : IWantToRunWhenEndpointStartsAndStops
    {
        public Initialization()
        {
        }

        public async Task Start(IMessageSession session)
        {
            await session.ScheduleEvery(
                timeSpan: TimeSpan.FromSeconds(30),
                task: pipelineContext =>
                {
                    var message = new CheckScheduledPayments();
                    return pipelineContext.Send(message);
                })
                .ConfigureAwait(false);
        }

        public Task Stop(IMessageSession session)
        {
            return Task.CompletedTask;
        }
    }
}
