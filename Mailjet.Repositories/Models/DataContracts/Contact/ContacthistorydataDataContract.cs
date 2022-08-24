using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContacthistorydataDataContract : PagingRequestBaseDataContract
    {
        [DataMember]
        public string Contact { get; set; }
        [DataMember]
        public string CreatedAt { get; set; }
        [DataMember]
        public string Data { get; set; }
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Last { get; set; }
        [DataMember]
        public string MaxCreatedAt { get; set; }
        [DataMember]
        public string MinCreatedAt { get; set; }
    }
}


