namespace CustomErrorHandelingBehavior
{
    using System;
    using System.Threading.Tasks;
    using BaseException;
    using NServiceBus;
    using NServiceBus.Pipeline;

    public class CustomExceptionHandelingBehavior : Behavior<IIncomingLogicalMessageContext>
    {
        public override async Task Invoke(IIncomingLogicalMessageContext context, Func<Task> next)
        {
            // custom logic before calling the next step in the pipeline.

            try
            {
                await next();
            }
            catch (MyBusinessException)
            {
                // here we want to send the message to a configured queue  
                var sendOptions = new SendOptions();

                sendOptions.SetDestination("customErrorQueue");

                await context.Send(context.Message.Instance, sendOptions);
            }

            // custom logic after all inner steps in the pipeline completed.
        }
    }
}
