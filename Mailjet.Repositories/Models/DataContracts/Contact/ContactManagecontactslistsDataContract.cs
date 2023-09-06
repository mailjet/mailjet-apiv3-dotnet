using Mailjet.Client;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactManagecontactslistsDataContract
    {
        [DataMember]
        public string ContactsLists { get; set; }
    }
}


