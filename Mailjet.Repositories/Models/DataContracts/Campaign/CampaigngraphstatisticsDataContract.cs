using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Campaign
{
    [DataContract]
    public class CampaigngraphstatisticsDataContract : PagingRequestBaseDataContract
    {

        [DataMember]
        public string Clickcount { get; set; }
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string Opencount { get; set; }
        [DataMember]
        public string Spamcount { get; set; }
        [DataMember]
        public string Tick { get; set; }
        [DataMember]
        public string Unsubcount { get; set; }
        [DataMember]
        public string Click { get; set; }
        [DataMember]
        public string IDS { get; set; }
        [DataMember]
        public string Open { get; set; }
        [DataMember]
        public string Range { get; set; }
        [DataMember]
        public string Spam { get; set; }
        [DataMember]
        public string Unsub { get; set; }
    }
}


