using Mailjet.Client;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Campaign
{
    [DataContract]
    public class CampaigndraftTestDataContract
    {
        [DataMember]
        public string Recipients { get; set; }
    }
}


