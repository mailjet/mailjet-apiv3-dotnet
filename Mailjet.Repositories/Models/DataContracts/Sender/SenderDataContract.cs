using Mailjet.Client;
using Mailjet.Repositories.Interfaces.Models;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Sender
{
    [DataContract]
    public class SenderDataContract : PagingRequestBaseDataContract, IContactEmail
    {
        [DataMember]
        public string CreatedAt { get; set; }
        [DataMember]
        public string DNS { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string EmailType { get; set; }
        [DataMember]
        public string Filename { get; set; }
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public bool IsDefaultSender { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string DnsID { get; set; }
        [DataMember]
        public string Domain { get; set; }
        [DataMember]
        public string LocalPart { get; set; }
        [DataMember]
        public string ShowDeleted { get; set; }
    }
}


