using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactfilterDataContract : PagingRequestBaseDataContract
    {
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Expression { get; set; }
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string ShowDeleted { get; set; }
    }
}


