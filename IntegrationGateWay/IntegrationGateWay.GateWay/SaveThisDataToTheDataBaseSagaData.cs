namespace IntegrationGateWay.GateWay
{
    using System;
    using NServiceBus.Saga;

    public class SaveThisDataToTheDataBaseSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }
    }
}
