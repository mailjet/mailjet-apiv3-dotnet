using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.MailJet.DataContracts.Template
{

    [DataContract]
    public class TemplateDataContract : PagingRequestBaseDataContract
    {
 

        public enum EditModeValueTypes
        {
            DNDBuilder = 1,
            HTMLBuilder = 2,
            SavedSectionBuilder = 3,
            MJMLBuilder = 4
        }

        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string[] Categories { get; set; }
        [DataMember]
        public string Copyright { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public EditModeValueTypes EditMode { get; set; }
        [DataMember]
        public Int64 ID { get; set; }
        [DataMember]
        public bool IsStarred { get; set; }
        [DataMember]
        public string Locale { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string OwnerId { get; set; }
        [DataMember]
        public string OwnerType { get; set; }
        [DataMember]
        public string Presets { get; set; }
        [DataMember]
        public string Previews { get; set; }
        [DataMember]
        public string[] Purposes { get; set; }
        [DataMember]
        public string APIKey { get; set; }
        [DataMember]
        public string CategoriesSelectionMethod { get; set; }
        [DataMember]
        public string PurposesSelectionMethod { get; set; }
        [DataMember]
        public bool IsTextPartGenerationEnabled { get; set; }
        [DataMember]
        public string User { get; set; }

    }
}


