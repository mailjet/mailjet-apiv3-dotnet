using Mailjet.Client;
using Mailjet.Repositories.Interfaces.Models;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactslistManagecontactDataContract: IContactEmail
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Action { get; set; }
        [DataMember]
        public string Properties { get; set; }
    }
}


