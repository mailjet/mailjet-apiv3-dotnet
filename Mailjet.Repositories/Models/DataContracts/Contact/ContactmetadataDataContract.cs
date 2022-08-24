using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactmetadataDataContract : PagingRequestBaseDataContract
    {
        [DataMember]
        public string Datatype { get; set; }
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string NameSpace { get; set; }
        [DataMember]
        public string DataType { get; set; }
        [DataMember]
        public string Namespace { get; set; }
    }
}


