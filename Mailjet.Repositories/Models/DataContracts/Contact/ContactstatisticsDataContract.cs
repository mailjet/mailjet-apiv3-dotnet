using Mailjet.Client;
using System.Runtime.Serialization;
using System;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactstatisticsDataContract : PagingRequestBaseDataContract
    {
        [DataMember]
        public string BlockedCount { get; set; }
        [DataMember]
        public string BouncedCount { get; set; }
        [DataMember]
        public string ClickedCount { get; set; }
        [DataMember]
        public string Contact { get; set; }
        [DataMember]
        public string DeliveredCount { get; set; }
        [DataMember]
        public string LastActivityAt { get; set; }
        [DataMember]
        public string MarketingContacts { get; set; }
        [DataMember]
        public string OpenedCount { get; set; }
        [DataMember]
        public string ProcessedCount { get; set; }
        [DataMember]
        public string QueuedCount { get; set; }
        [DataMember]
        public string SpamComplaintCount { get; set; }
        [DataMember]
        public string UnsubscribedCount { get; set; }
        [DataMember]
        public string UserMarketingContacts { get; set; }
        [DataMember]
        public string Blocked { get; set; }
        [DataMember]
        public string Bounced { get; set; }
        [DataMember]
        public string Click { get; set; }
        [DataMember]
        public string MaxLastActivityAt { get; set; }
        [DataMember]
        public string MinLastActivityAt { get; set; }
        [DataMember]
        public string Open { get; set; }
        [DataMember]
        public string Queued { get; set; }

        [Obsolete]
        [DataMember]
        public string Recalculate { get; set; }

        [DataMember]
        public string Sent { get; set; }
        [DataMember]
        public string Spam { get; set; }
        [DataMember]
        public string Unsubscribed { get; set; }
        [DataMember]
        public string DeferredCount { get; set; }
        [DataMember]
        public string HardbouncedCount { get; set; }
        [DataMember]
        public string SoftbouncedCount { get; set; }
        [DataMember]
        public string WorkFlowExitedCount { get; set; }
    }
}


