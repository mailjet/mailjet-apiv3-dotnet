using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactdataCreateDataContract 
    {
        [DataMember]
        public Int64 ContactID { get; set; }
        [DataMember]
        public IList<NameValueDataContract> Data { get; set; }
    }
}


