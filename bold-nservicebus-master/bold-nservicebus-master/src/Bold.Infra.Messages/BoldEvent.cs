using System;

namespace Bold.Infra.Messages
{
    public class BoldEvent : BoldMessage, IBoldEvent
    {
        public BoldEvent(Guid contentId)
            : base(contentId.ToString())
        {
        }

        public BoldEvent(int contentId)
            : base(contentId)
        {
        }

        public BoldEvent(string contentId)
            : base(contentId)
        {
        }

        public BoldEvent(Guid contentId, long contentVersion)
            : base(contentId.ToString(), contentVersion)
        {
        }

        public BoldEvent(int contentId, long contentVersion)
            : base(contentId, contentVersion)
        {
        }

        public BoldEvent(string contentId, long contentVersion)
            : base(contentId, contentVersion)
        {
        }
    }
}
