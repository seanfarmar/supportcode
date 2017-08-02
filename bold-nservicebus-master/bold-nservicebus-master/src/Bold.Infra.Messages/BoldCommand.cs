using System;

namespace Bold.Infra.Messages
{
    public class BoldCommand : BoldMessage, IBoldCommand
    {
        public BoldCommand(Guid contentId)
            : base(contentId.ToString())
        {
        }

        public BoldCommand(int contentId)
            : base(contentId)
        {
        }

        public BoldCommand(string contentId)
            : base(contentId)
        {
        }

        public BoldCommand(Guid contentId, long contentVersion)
            : base(contentId.ToString(), contentVersion)
        {
        }

        public BoldCommand(int contentId, long contentVersion)
            : base(contentId, contentVersion)
        {
        }

        public BoldCommand(string contentId, long contentVersion)
            : base(contentId, contentVersion)
        {
        }
    }
}