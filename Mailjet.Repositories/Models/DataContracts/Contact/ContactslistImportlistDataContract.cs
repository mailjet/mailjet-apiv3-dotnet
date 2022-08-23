using Mailjet.Client;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactslistImportlistDataContract
    {
        [DataMember]
        public string Action { get; set; }
        [DataMember]
        public string ListID { get; set; }
    }
}
