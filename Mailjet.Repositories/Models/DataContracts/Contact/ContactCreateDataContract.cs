using Mailjet.Client;
using Mailjet.Repositories.Interfaces.Models;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactCreateDataContract : RequestBaseDataContract, IContactEmail
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
