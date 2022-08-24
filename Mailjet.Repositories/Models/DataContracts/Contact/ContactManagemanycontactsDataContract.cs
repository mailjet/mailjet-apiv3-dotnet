using Mailjet.Client;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactManagemanycontactsDataContract
    {
        [DataMember]
        public string ContactsLists { get; set; }
        [DataMember]
        public string Contacts { get; set; }
    }
}


