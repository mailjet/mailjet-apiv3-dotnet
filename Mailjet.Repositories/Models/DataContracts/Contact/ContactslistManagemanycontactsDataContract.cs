using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Collections;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactslistManagemanycontactsDataContract: RequestBaseDataContract
    {

        [DataMember]
        public string Action { get; set; }

        [DataMember]
        public IList Contacts { get; set; }

    }
}


