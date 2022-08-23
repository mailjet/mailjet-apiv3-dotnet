using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactslistDataContract : PagingRequestBaseDataContract
    {
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string CreatedAt { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string IsDeleted { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string SubscriberCount { get; set; }
        [DataMember]
        public string ExcludeID { get; set; }
    }
}


