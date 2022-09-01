using Mailjet.Client;
using Mailjet.Repositories.Interfaces.Models;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactslistsignupDataContract : PagingRequestBaseDataContract
    {
        [DataMember]
        public string ConfirmAt { get; set; }
        [DataMember]
        public string ConfirmIp { get; set; }
        [DataMember]
        public string Contact { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public string List { get; set; }
        [DataMember]
        public string SignupAt { get; set; }
        [DataMember]
        public string SignupIp { get; set; }
        [DataMember]
        public string SignupKey { get; set; }
        [DataMember]
        public string Source { get; set; }
        [DataMember]
        public string SourceId { get; set; }
        [DataMember]
        public string APIKey { get; set; }
        [DataMember]
        public string ContactsList { get; set; }
        [DataMember]
        public string Domain { get; set; }
        [DataMember]
        public string LocalPart { get; set; }
        [DataMember]
        public string MaxConfirmAt { get; set; }
        [DataMember]
        public string MaxSignupAt { get; set; }
        [DataMember]
        public string MinConfirmAt { get; set; }
        [DataMember]
        public string MinSignupAt { get; set; }
        [DataMember]
        public string SourceID { get; set; }
    }
}


