using Mailjet.Client;
using System.Runtime.Serialization;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.MailJet.DataContracts.Template
{

    [DataContract]
    public class TemplateRequestDataContract
    {
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public int OwnerId { get; set; }
        [DataMember]
        public string OwnerType { get; set; }
        [DataMember]
        public string Presets { get; set; }
        [DataMember]
        public string[] Categories { get; set; }
        [DataMember]
        public string Copyright { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int EditMode { get; set; }
        [DataMember]
        public bool IsStarred { get; set; }
        [DataMember]
        public bool IsTextPartGenerationEnabled { get; set; }
        [DataMember]
        public string Locale { get; set; }
        [DataMember]
        public string[] LocaleList { get; set; }
        [DataMember]
        public int[] Previews { get; set; }
        [DataMember]
        public string[] Purposes { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }
        [DataMember]
        public DateTime LastUpdatedAt { get; set; }
    }
}


