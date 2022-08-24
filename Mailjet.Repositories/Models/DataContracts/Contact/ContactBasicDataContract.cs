using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Contact
{
    [DataContract]
    public class ContactBasicDataContract<T>
        where T : class, new()
    {
        [DataMember]
        public string Email { get; set; } = "";
        [DataMember]
        public string Name { get; set; } = "";
        [DataMember]
        public Boolean IsExcludedFromCampaigns { get; set; }
        [DataMember]
        public T Properties { get; set; } = new ();
}
}
