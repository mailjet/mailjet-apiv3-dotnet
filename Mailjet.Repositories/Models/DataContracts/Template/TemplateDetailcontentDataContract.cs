using Mailjet.Client;
using System.Runtime.Serialization;
using System.Collections.Specialized;
using System.Text.Json.Nodes;
using Newtonsoft.Json;

namespace Mailjet.Repositories.Models.MailJet.DataContracts.Template
{
    [DataContract]
    public class TemplateDetailcontentDataContract
    {
        [DataMember(Name = "Text-part")]
        //[JsonProperty(PropertyName = "Text-part")]
        public string Textpart { get; set; } = "";
        [DataMember(Name = "Html-part")]
        //[JsonProperty(PropertyName = "Html-part")]
        public string Htmlpart { get; set; } = "";
        [DataMember]
        public dynamic MJMLContent { get; set; } = "";
        [DataMember]
        public Dictionary<String, String> Headers { get; set; } = new Dictionary<string, string>();
    }
}
