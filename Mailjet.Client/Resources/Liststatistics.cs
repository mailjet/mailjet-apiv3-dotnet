using System;
namespace Mailjet.Client.Resources
{
    
    public static class Liststatistics
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("liststatistics");

        public const string ActiveCount = "ActiveCount";
        public const string ActiveExcludedSubscriberCount = "ActiveExcludedSubscriberCount";
        public const string ActiveUnsubscribedCount = "ActiveUnsubscribedCount";
        public const string Address = "Address";
        public const string BlockedCount = "BlockedCount";
        public const string BouncedCount = "BouncedCount";
        public const string ClickedCount = "ClickedCount";
        public const string CreatedAt = "CreatedAt";
        public const string DeliveredCount = "DeliveredCount";
        public const string ExcludedSubscriberCount = "ExcludedSubscriberCount";
        public const string ID = "ID";
        public const string IsDeleted = "IsDeleted";

        [Obsolete]
        public const string LastActivityAt = "LastActivityAt";

        public const string Name = "Name";
        public const string OpenedCount = "OpenedCount";
        public const string SpamComplaintCount = "SpamComplaintCount";
        public const string SubscriberCount = "SubscriberCount";
        public const string UnsubscribedCount = "UnsubscribedCount";
        public const string CalcActive = "CalcActive";
        public const string CalcActiveExcludedSubscriber = "CalcActiveExcludedSubscriber";
        public const string CalcActiveUnsub = "CalcActiveUnsub";
        public const string CalcExcludedSubscriber = "CalcExcludedSubscriber";
        public const string ContactsListID = "ContactsListID";
        public const string ExcludeID = "ExcludeID";
        public const string NameCI = "NameCI";
        public const string NameLike = "NameLike";
        public const string NameLikeCI = "NameLikeCI";
        public const string Limit = "Limit";
        public const string Offset = "Offset";
        public const string Sort = "Sort";
        public const string CountOnly = "CountOnly";
        public const string SoftbouncedCount = "SoftbouncedCount";
        public const string HardbouncedCount = "HardbouncedCount";
        public const string DeferredCount = "DeferredCount";
        public const string Blocked = "Blocked";
        public const string Bounced = "Bounced";
        public const string MaxLastActivityAt = "MaxLastActivityAt";
        public const string MinLastActivityAt = "MinLastActivityAt";
        public const string Open = "Open";
        public const string Queued = "Queued";
        public const string Sent = "Sent";
        public const string Spam = "Spam";
        public const string Unsubscribed = "Unsubscribed";
    }
}


