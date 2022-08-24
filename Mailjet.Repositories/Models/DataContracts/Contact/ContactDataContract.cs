using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactDataContract: PagingRequestBaseDataContract
    {
        [DataMember]
        public string CreatedAt { get; set; }
        [DataMember]
        public string DeliveredCount { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string ExclusionFromCampaignsUpdatedAt { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string IsExcludedFromCampaigns { get; set; }
        [DataMember]
        public string IsOptInPending { get; set; }
        [DataMember]
        public string IsSpamComplaining { get; set; }
        [DataMember]
        public string LastActivityAt { get; set; }
        [DataMember]
        public string LastUpdateAt { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string UnsubscribedAt { get; set; }
        [DataMember]
        public string UnsubscribedBy { get; set; }
        [DataMember]
        public string Campaign { get; set; }
        [DataMember]
        public String ContactsList { get; set; }
        [DataMember]
        public string IsUnsubscribed { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}
