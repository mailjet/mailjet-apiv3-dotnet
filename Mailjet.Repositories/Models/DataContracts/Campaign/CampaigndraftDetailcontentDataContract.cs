using Mailjet.Client;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Campaign
{
    [DataContract]
    public class CampaigndraftDetailcontentDataContract
    {
        [DataMember]
        public string Textpart { get; set; }
        [DataMember]
        public string Htmlpart { get; set; }
        [DataMember]
        public string MJMLContent { get; set; }
        [DataMember]
        public string Headers { get; set; }
    }
}


