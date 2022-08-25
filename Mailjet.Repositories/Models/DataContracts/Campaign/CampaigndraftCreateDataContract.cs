using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Campaign
{
    [DataContract]
    public class CampaigndraftCreateDataContract : PagingRequestBaseDataContract
    {

        [DataMember]
        public string Locale { get; set; }
        [DataMember]
        public string Sender { get; set; }
        [DataMember]
        public string SenderEmail { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public Int64 ContactsListID { get; set; }
        [DataMember]
        public string Title { get; set; }
    }
}


