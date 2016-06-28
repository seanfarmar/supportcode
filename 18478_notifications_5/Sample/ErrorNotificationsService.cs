using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
//using Alogent.ApgReview.Contracts.Internal.Pnv;
using NServiceBus;
using NServiceBus.Faults;
using NServiceBus.Logging;

namespace Alogent.ApgReview.BackEnd.NServiceBus
{
	public interface IErrorNotificationsService
	{
		void ErrorEvent(FailedMessage failedMessage);
	}

	public class ErrorNotificationsService : IErrorNotificationsService, IWantToRunWhenBusStartsAndStops, IDisposable
	{
		private static ILog Log = LogManager.GetLogger<ErrorNotificationsService>();
		protected readonly BusNotifications _busNotifications;
		private readonly List<IDisposable> _unsubscribeStreams = new List<IDisposable>();
		protected static string ConversationIdKey = "NServiceBus.ConversationId";
		//private readonly IConversationService _conversationService;

		public IBus Bus { get; set; }

		public ErrorNotificationsService(IBus bus, BusNotifications busNotifications)//, IConversationService conversationService)
		{
			Bus = bus;
			this._busNotifications = busNotifications;
			// _conversationService = conversationService;
		}

		public void Start()
		{
			ErrorsNotifications errorNotifications = _busNotifications.Errors;
			DefaultScheduler defaultScheduler = Scheduler.Default;
			_unsubscribeStreams.Add(
				errorNotifications.MessageSentToErrorQueue
					.ObserveOn(defaultScheduler)
					.Subscribe(ErrorEvent)
				);
		}

		public void ErrorEvent(FailedMessage failedMessage)
		{
			Log.Fatal("ErrorEvent received when message went to error queue.  Putting the error message on the  bus.");
			var conversationId = failedMessage.Headers[ConversationIdKey];			
			Log.FatalFormat("Error ConversationId: {0}", conversationId);

			//Bus.Send(new PnvItemWentToErrorQueue
			//{
			//	ConversationId = conversationId,
			//	ExceptionMessage = string.Format("Item sent to the error queue. Error: {0}", failedMessage.Exception)
			//});
		}

		public void Stop()
		{
			foreach (var unsubscribeStream in _unsubscribeStreams)
			{
				unsubscribeStream.Dispose();
			}
		}

		public void Dispose()
		{
			Stop();
		}
	}
}