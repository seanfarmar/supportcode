using System;

namespace Bold.Infra.Messages
{
    public interface IBoldMessage
    {
        string ContentId { get; set; }
        long ContentVersion { get; set; }
        DateTime CreatedAt { get; set; }
    }
}