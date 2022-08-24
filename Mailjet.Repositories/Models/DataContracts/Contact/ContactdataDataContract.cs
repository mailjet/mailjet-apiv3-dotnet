using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactdataDataContract : PagingRequestBaseDataContract
    {
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public Int64 ContactID { get; set; }
        [DataMember]
        public String MethodCollection { get; set; }
        [DataMember]
        public IList<NameValueDataContract> Data { get; set; }
        [DataMember]
        public string Campaign { get; set; }
        [DataMember]
        public string ContactEmail { get; set; }
        [DataMember]
        public string ContactsList { get; set; }
        [DataMember]
        public string Fields { get; set; }
        [DataMember]
        public string IsExcludedFromCampaigns { get; set; }
        [DataMember]
        public string IsUnsubscribed { get; set; }
        [DataMember]
        public string LastActivityAt { get; set; }
        [DataMember]
        public string Status { get; set; }
    }
}


