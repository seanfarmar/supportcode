namespace DocumentServices.Saga
{
    using System;
    using NServiceBus.Saga;

    public class DocumentSagaData : ContainSagaData
    {
        [Unique]
        public virtual Guid DocumentId { get; set; }
        public virtual string FilePath { get; set; }
        public virtual string ClientId { get; set; }
    }
}