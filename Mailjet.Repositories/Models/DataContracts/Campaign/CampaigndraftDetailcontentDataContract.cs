using Mailjet.Client;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Campaign
{
    [DataContract]
    public class CampaigndraftDetailcontentDataContract
    {

        [DataMember(Name = "Text-part")]
        //[JsonProperty(PropertyName = "Text-part")]
        public string Textpart { get; set; } = "";
        [DataMember(Name = "Html-part")]
        //[JsonProperty(PropertyName = "Html-part")]
        public string Htmlpart { get; set; } = "";
        [DataMember]
        public string MJMLContent { get; set; }
        [DataMember]
        public Dictionary<String, String> Headers { get; set; } = new Dictionary<string, string>();
    }
}


