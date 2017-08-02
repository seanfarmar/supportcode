# bold-nservicebus

AzureServiceBusTransport is hardcoded to Serializable as IsolationLevel cause Azure Service Bus requires it to be.

How are we going to scale if all our database queries are Serializable?

Trying to prove the concept outlined here: https://docs.particular.net/nservicebus/azure-service-bus/understanding-transactions-and-delivery-guarantees?version=asb_6

Start in Endpoint2.Program.cs to see the Bus Configuration. 

In the Endpoint2.ShipOrderHandler we want the IsolationLevel to be Snapshot not Serializable.

I don't want to use EntityFramework, but at current assignment I am forced to, so it has to work with EF as well.

I also want it to work with our feature Inbox that is a IdempotencyEnforcer, injected in the NSB pipeline after OutboxDeduplication but before LoadHandlers. 