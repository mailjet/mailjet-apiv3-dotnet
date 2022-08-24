using Mailjet.Client;
using Mailjet.Repositories.Models.MailJet.DataContracts.Base;
using System.Runtime.Serialization;

namespace Mailjet.Repositories.Models.DataContracts.Campaign
{
    [DataContract]
    public class CampaignDataContract : PeriodRequestBaseDataContract
    {
        [DataMember]
        public string CampaignType { get; set; }
        [DataMember]
        public string ClickTracked { get; set; }
        [DataMember]
        public string CreatedAt { get; set; }
        [DataMember]
        public string CustomValue { get; set; }
        [DataMember]
        public string FirstMessageID { get; set; }
        [DataMember]
        public string From { get; set; }
        [DataMember]
        public string FromEmail { get; set; }
        [DataMember]
        public string FromName { get; set; }
        [DataMember]
        public string HasHtmlCount { get; set; }
        [DataMember]
        public string HasTxtCount { get; set; }
        [DataMember]
        public string ID { get; set; }
        [DataMember]
        public string IsDeleted { get; set; }
        [DataMember]
        public string IsStarred { get; set; }
        [DataMember]
        public string List { get; set; }
        [DataMember]
        public string NewsLetterID { get; set; }
        [DataMember]
        public string OpenTracked { get; set; }
        [DataMember]
        public string Segmentation { get; set; }
        [DataMember]
        public string SendEndAt { get; set; }
        [DataMember]
        public string SendStartAt { get; set; }
        [DataMember]
        public string SpamassScore { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public string Subject { get; set; }
        [DataMember]
        public string UnsubscribeTrackedCount { get; set; }
        [DataMember]
        public string CampaignID { get; set; }
        [DataMember]
        public string CampaignStatus { get; set; }
        [DataMember]
        public string ContactsListID { get; set; }
        [DataMember]
        public string CustomCampaign { get; set; }
        [DataMember]
        public string FromDomain { get; set; }
        [DataMember]
        public string FromID { get; set; }
        [DataMember]
        public string FromType { get; set; }
        [DataMember]
        public string IsNewsletterTool { get; set; }
    }
}

