using Mailjet.Client;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Campaign
{
    [DataContract]
    public class CampaigndraftScheduleDataContract
    {
        [DataMember]
        public string Date { get; set; }
    }
}


