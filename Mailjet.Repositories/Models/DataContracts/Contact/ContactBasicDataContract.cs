using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactBasicDataContract<T>: ContactEmailDataContract
        where T : class, new()
    {
        [DataMember]
        public Boolean IsExcludedFromCampaigns { get; set; }
        [DataMember]
        public T Properties { get; set; } = new ();
}
}
