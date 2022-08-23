using Mailjet.Client;
using System.Runtime.Serialization;
using System.Collections.Specialized;
using System.Text.Json.Nodes;

namespace Mailjet.Repositories.Models.MailJet.DataContracts.Template
{
    [DataContract]
    public class TemplateDetailcontentDataContract
    {
        [DataMember(Name = "Text-part")]
        public string Textpart { get; set; }
        [DataMember(Name = "Html-part")]
        public string Htmlpart { get; set; }
        [DataMember]
        public dynamic MJMLContent { get; set; }
        [DataMember]
        public Dictionary<String,String> Headers { get; set; }
    }
}
