using System;

namespace Mailjet.Client.Resources
{
    public static class Contactstatistics
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("contactstatistics");

        public const string BlockedCount = "BlockedCount";
        public const string BouncedCount = "BouncedCount";
        public const string ClickedCount = "ClickedCount";
        public const string Contact = "Contact";
        public const string DeliveredCount = "DeliveredCount";
        public const string LastActivityAt = "LastActivityAt";
        public const string MarketingContacts = "MarketingContacts";
        public const string OpenedCount = "OpenedCount";
        public const string ProcessedCount = "ProcessedCount";
        public const string QueuedCount = "QueuedCount";
        public const string SpamComplaintCount = "SpamComplaintCount";
        public const string UnsubscribedCount = "UnsubscribedCount";
        public const string UserMarketingContacts = "UserMarketingContacts";
        public const string Blocked = "Blocked";
        public const string Bounced = "Bounced";
        public const string Click = "Click";
        public const string MaxLastActivityAt = "MaxLastActivityAt";
        public const string MinLastActivityAt = "MinLastActivityAt";
        public const string Open = "Open";
        public const string Queued = "Queued";

        [Obsolete]
        public const string Recalculate = "Recalculate";

        public const string Sent = "Sent";
        public const string Spam = "Spam";
        public const string Unsubscribed = "Unsubscribed";
        public const string Limit = "Limit";
        public const string Offset = "Offset";
        public const string Sort = "Sort";
        public const string CountOnly = "CountOnly";
        public const string DeferredCount = "DeferredCount";
        public const string HardbouncedCount = "HardbouncedCount";
        public const string SoftbouncedCount = "SoftbouncedCount";
        public const string WorkFlowExitedCount = "WorkFlowExitedCount";
    }
}


