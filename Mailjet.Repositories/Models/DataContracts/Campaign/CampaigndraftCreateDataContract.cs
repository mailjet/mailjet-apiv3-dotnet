using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Campaign
{
    [DataContract]
    public class CampaigndraftCreateDataContract : RequestBaseDataContract
    {

        [DataMember]
        public string Locale { get; set; }
        [DataMember]
        public Int64 Sender { get; set; }
        [DataMember]
        public string SenderEmail { get; set; }
        [DataMember]
        public string ReplyEmail { get; set; }
        [DataMember]
        public string SenderName { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public Int64 ContactsListID { get; set; }
        [DataMember]
        public string Title { get; set; }
    }
}


