using System.Collections.Generic;

namespace Mailjet.Client.TransactionalEmails
{
    public class TransactionalEmail
    {
        public string Subject { get; set; }
        public string HTMLPart { get; set; }
        public string TextPart { get; set; }
        public Contact Sender { get; set; }
        public Contact From { get; set; }
        public Contact ReplyTo { get; set; }

        public List<Contact> To { get; set; }
        public List<Contact> Cc { get; set; }
        public List<Contact> Bcc { get; set; }

        public long? TemplateID { get; set; }
        public bool? TemplateLanguage { get; set; }
        public Contact TemplateErrorReporting { get; set; }
        public bool? TemplateErrorDelivery { get; set; }

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
        public Dictionary<string, string> Variables { get; set; }
    }
}