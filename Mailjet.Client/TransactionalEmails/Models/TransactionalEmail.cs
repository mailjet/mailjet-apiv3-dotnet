using System.Collections.Generic;

namespace Mailjet.Client.TransactionalEmails
{
    public class TransactionalEmail
    {
        public string Subject { get; set; }
        public string HTMLPart { get; set; }
        public string TextPart { get; set; }
        public SendContact Sender { get; set; }
        public SendContact From { get; set; }
        public SendContact ReplyTo { get; set; }

        public List<SendContact> To { get; set; }
        public List<SendContact> Cc { get; set; }
        public List<SendContact> Bcc { get; set; }

        public long? TemplateID { get; set; }
        public bool? TemplateLanguage { get; set; }
        public SendContact TemplateErrorReporting { get; set; }
        public bool? TemplateErrorDeliver { get; set; }

        public List<Attachment> Attachments { get; set; }
        public List<Attachment> InlinedAttachments { get; set; }
        public int? Priority { get; set; }
        public string CustomCampaign { get; set; }
        public bool? DeduplicateCampaign { get; set; }
        public TrackOpens? TrackOpens { get; set; }
        public TrackClicks? TrackClicks { get; set; }
        public string CustomID { get; set; }
        public string EventPayload { get; set; }
        public string URLTags { get; set; }

        public Dictionary<string, string> Headers { get; set; }
        public IDictionary<string, object> Variables { get; set; }
    }
}