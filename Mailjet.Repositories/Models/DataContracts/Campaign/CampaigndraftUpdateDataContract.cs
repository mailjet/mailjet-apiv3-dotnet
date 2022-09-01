using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Campaign
{
    [DataContract]
    public class CampaigndraftUpdateDataContract : RequestBaseDataContract
    {
        [DataMember]
        public Boolean IsStarred { get; set; }
        [DataMember]
        public Boolean IsDeleted { get; set; }
    }
}


