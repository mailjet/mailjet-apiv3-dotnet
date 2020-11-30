using System;
using System.Collections.Generic;
using System.Linq;
using Mailjet.Client.Exceptions;

namespace Mailjet.Client.TransactionalEmails
{
    public class TransactionalEmailBuilder
    {
        private string _subject;
        private string _htmlPart;
        private string _textPart;
        private Contact _sender;
        private Contact _from;
        private Contact _replyTo;

        private List<Contact> _to = new List<Contact>();
        private List<Contact> _cc;
        private List<Contact> _bcc;

        private long? _templateId;
        private bool? _templateLanguage;
        private Contact _templateErrorReporting;
        private bool? _templateErrorDelivery;

        private List<Attachment> _attachments;
        private List<Attachment> _inlinedAttachments;
        private int? _priority;
        private string _customCampaign;
        private bool? _deduplicateCampaign;
        private TrackOpens? _trackOpens;
        private TrackClicks? _trackClicks;
        private string _customId;
        private string _eventPayload;
        private string _urlTags;

        private Dictionary<string, string> _headers;
        private Dictionary<string, string> _variables;

        /// <summary>
        /// The email subject line
        /// </summary>
        public TransactionalEmailBuilder WithSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        /// <summary>
        /// Provides the HTML part of the message. Mandatory, if the TextPart and TemplateID parameters are not specified.
        /// </summary>
        public TransactionalEmailBuilder WithHtmlPart(string htmlPart)
        {
            _htmlPart = htmlPart;
            return this;
        }

        /// <summary>
        /// Provides the Text part of the message. Mandatory, if the HTMLPart and TemplateID parameters are not specified.
        /// </summary>
        public TransactionalEmailBuilder WithTextPart(string textPart)
        {
            _textPart = textPart;
            return this;
        }

        /// <summary>
        /// Specifies the sender name and email address. Used when you want to send emails on behalf of a different email address. Must be a valid, activated sender for this account.
        /// See the /sender and /metasender API resources for more info.
        /// The Sender email address will be used to send the message, while the From email address will be displayed in the recipient's inbox.
        /// In such cases, it is not necessary for the From address to be verified. However, the information will be displayed in the inbox as From {from_email} via / sent on behalf of {sender_domain}.
        /// </summary>
        /// <remarks>
        /// Note: This option is disabled by default. Contact the Support team if you want us to enable this setting on a specific API Key.
        /// </remarks>
        public TransactionalEmailBuilder WithSender(Contact sender)
        {
            _sender = sender;
            return this;
        }

        /// <summary>
        /// When Sender is not specified, the email address will be used to send the message.
        /// In such cases, it must be a valid, activated sender for this account.
        /// See the /sender and /metasender API resources for more info.
        /// When Sender is specified, the Sender email address will be used to send the message, while the From email address will be displayed in the recipient's inbox.
        /// In such cases, it is not necessary for the From address to be validated.
        /// When the Sender and From domains differ, the information will be displayed in the inbox as From {from_email} via / sent on behalf of {sender_domain}.
        /// </summary>
        /// <remarks>
        /// If the From domain has a DMARC policy in effect(e.g.Yahoo, AOL), the message will not be delivered.Instead, it will either bounce or be considered as Spam.
        /// </remarks>
        public TransactionalEmailBuilder WithFrom(Contact from)
        {
            _from = from;
            return this;
        }

        /// <summary>
        /// the email address and name(optional), to which replies to this message will go.
        /// </summary>
        public TransactionalEmailBuilder WithReplyTo(Contact replyTo)
        {
            _replyTo = replyTo;
            return this;
        }

        /// <summary>
        /// To recipients
        /// </summary>
        public TransactionalEmailBuilder WithTo(Contact to)
        {
            _to.Add(to);
            return this;
        }

        /// <summary>
        /// To recipients
        /// </summary>
        public TransactionalEmailBuilder WithTo(IEnumerable<Contact> toContacts)
        {
            _to.AddRange(toContacts);
            return this;
        }

        /// <summary>
        /// Carbon copy recipients
        /// </summary>
        public TransactionalEmailBuilder WithCc(Contact cc)
        {
            if (_cc == null)
                _cc = new List<Contact>();

            _cc.Add(cc);
            return this;
        }

        /// <summary>
        /// Carbon copy recipients
        /// </summary>
        public TransactionalEmailBuilder WithCc(IEnumerable<Contact> ccContacts)
        {
            if (_cc == null)
                _cc = new List<Contact>();

            _cc.AddRange(ccContacts);
            return this;
        }

        /// <summary>
        /// Blind carbon copy recipients
        /// </summary>
        public TransactionalEmailBuilder WithBcc(Contact bcc)
        {
            if (_bcc == null)
                _bcc = new List<Contact>();

            _bcc.Add(bcc);
            return this;
        }

        /// <summary>
        /// Blind carbon copy recipients
        /// </summary>
        public TransactionalEmailBuilder WithBcc(IEnumerable<Contact> bccContacts)
        {
            if (_bcc == null)
                _bcc = new List<Contact>();

            _bcc.AddRange(bccContacts);
            return this;
        }

        /// <summary>
        /// Unique numeric ID of the template to be used as email content. If the HTML/Text parts are specified, TemplateID will overwrite them.
        /// </summary>
        /// <remarks>
        /// Equivalent to using the X-MJ-TemplateID header through SMTP.
        /// </remarks>
        public TransactionalEmailBuilder WithTemplateId(long templateId)
        {
            _templateId = templateId;
            return this;
        }

        /// <summary>
        /// When true, activates the template language processing. Equivalent to using the X-MJ-TemplateLanguage header through SMTP.
        /// Default value: false
        /// </summary>
        public TransactionalEmailBuilder WithTemplateLanguage(bool shouldUseTemplateLanguageProcessing)
        {
            _templateLanguage = shouldUseTemplateLanguageProcessing;
            return this;
        }

        /// <summary>
        /// An object containing the email address and name of the recipient,
        /// to whom a carbon copy with the error message is sent to in case of sending issues. Can only be used when TemplateLanguage=true.
        /// Equivalent to using the X-MJ-TemplateErrorReporting header through SMTP.
        /// </summary>
        public TransactionalEmailBuilder WithTemplateErrorReporting(Contact contactForErrorNotifications)
        {
            _templateErrorReporting = contactForErrorNotifications;
            return this;
        }

        /// <summary>
        /// When true, the message delivery will proceed if an error is discovered in the templating language.
        /// When false, the message delivery will be stopped.
        /// Can only be used when TemplateLanguage= true.
        /// Equivalent to using the X-MJ-TemplateErrorDeliver header through SMTP.
        /// Default value: false
        /// </summary>
        public TransactionalEmailBuilder WithTemplateErrorDeliver(bool shouldProceedWithMessageDeliveryInCaseOfErrors)
        {
            _templateErrorDelivery = shouldProceedWithMessageDeliveryInCaseOfErrors;
            return this;
        }

        /// <summary>
        /// The array of attachments to the email
        /// Each attachment contains the Filename, Content type and Base64 encoded content of the file.
        /// The total size of attachments (including inline) should not exceed 15 MB.
        /// </summary>
        public TransactionalEmailBuilder WithAttachments(IEnumerable<Attachment> attachments)
        {
            if (_attachments == null)
                _attachments = new List<Attachment>();

            _attachments.AddRange(attachments);
            return this;
        }

        /// <summary>
        /// The array of attachments to the email
        /// Each attachment contains the Filename, Content type and Base64 encoded content of the file.
        /// The total size of attachments (including inline) should not exceed 15 MB.
        /// </summary>
        public TransactionalEmailBuilder WithAttachment(Attachment attachment)
        {
            if (_attachments == null)
                _attachments = new List<Attachment>();

            _attachments.Add(attachment);
            return this;
        }
        /// <summary>
        /// The array of inlined attachments to the email that are available for inline use.
        /// Each attachment contains the Filename, Content type and Base64 encoded content of the file.
        /// Inline attachments can be visible directly in the body of the message, depending on the email client support.
        /// Insert the file into the HTML code of the email by using cid:Filename, or, if you have specified ContentID, by using cid:ContentID.
        /// The total size of attachments(including inline) should not exceed 15 MB.
        /// </summary>
        public TransactionalEmailBuilder WithInlinedAttachments(IEnumerable<Attachment> inlinedAttachments)
        {
            if (_inlinedAttachments == null)
                _inlinedAttachments = new List<Attachment>();

            _inlinedAttachments.AddRange(inlinedAttachments);
            return this;
        }

        /// <summary>
        /// The array of inlined attachments to the email that are available for inline use.
        /// Each attachment contains the Filename, Content type and Base64 encoded content of the file.
        /// Inline attachments can be visible directly in the body of the message, depending on the email client support.
        /// Insert the file into the HTML code of the email by using cid:Filename, or, if you have specified ContentID, by using cid:ContentID.
        /// The total size of attachments(including inline) should not exceed 15 MB.
        /// </summary>
        public TransactionalEmailBuilder WithInlinedAttachment(Attachment inlinedAttachment)
        {
            if (_inlinedAttachments == null)
                _inlinedAttachments = new List<Attachment>();

            _inlinedAttachments.Add(inlinedAttachment);
            return this;
        }

        /// <summary>
        /// Indicates the processing priority inside your account (API Key) scheduling queue.
        /// Equivalent of using X-Mailjet-Prio header through SMTP.
        /// Default value: 2
        /// </summary>
        public TransactionalEmailBuilder WithPriority(int priority)
        {
            _priority = priority;
            return this;
        }

        /// <summary>
        /// Specifies a custom campaign name, to which all messages with this property value will be assigned.
        /// If the campaign doesn't already exist it will be automatically created in the Mailjet system.
        /// API users are allowed to send multiple emails from the same campaign name to the same contacts.
        /// To block this feature, use the DeduplicateCampaign property.
        /// Equivalent of using X-Mailjet-Campaign header through SMTP.
        /// </summary>
        public TransactionalEmailBuilder WithCustomCampaign(string customCampaign)
        {
            _customCampaign = customCampaign;
            return this;
        }

        /// <summary>
        /// Enables or disables the option to send messages from the same campaign to the same contact multiple times.
        /// Equivalent of using X-Mailjet-DeduplicateCampaign header through SMTP.
        /// Default value: false
        /// </summary>
        public TransactionalEmailBuilder WithDeduplicateCampaign(bool shouldDeduplicateCampaign)
        {
            _deduplicateCampaign = shouldDeduplicateCampaign;
            return this;
        }

        /// <summary>
        /// Enable or disable open tracking on this message.
        /// This property will overwrite the preferences selected in the Mailjet account.
        /// Equivalent of using X-Mailjet-TrackOpen header through SMTP.
        /// </summary>
        public TransactionalEmailBuilder WithTrackOpens(TrackOpens trackOpens)
        {
            _trackOpens = trackOpens;
            return this;
        }

        /// <summary>
        /// Enable or disable open tracking on this message.
        /// This property will overwrite the preferences selected in the Mailjet account.
        /// Equivalent of using X-Mailjet-TrackClick header through SMTP.
        /// </summary>
        public TransactionalEmailBuilder WithTrackClicks(TrackClicks trackClicks)
        {
            _trackClicks = trackClicks;
            return this;
        }

        /// <summary>
        /// Select a user-defined custom ID.
        /// </summary>
        public TransactionalEmailBuilder WithCustomId(string customId)
        {
            _customId = customId;
            return this;
        }

        /// <summary>
        /// Defines a payload attached to the message.
        /// The Parse API will return the values in the payload.
        /// Any standard format can be used - XML, JSON, CSV etc.
        /// Equivalent of using X-MJ-EventPayload header through SMTP.
        /// </summary>
        public TransactionalEmailBuilder WithEventPayload(string eventPayload)
        {
            _eventPayload = eventPayload;
            return this;
        }

        /// <summary>
        /// URL tags to append every URL link in the message.
        /// The user needs to provide the query between the ? and # characters in the URL.
        /// The URLTags value needs to be URL-encoded.
        /// </summary>
        public TransactionalEmailBuilder WithUrlTags(string urlTags)
        {
            _urlTags = urlTags;
            return this;
        }

        /// <summary>
        /// Adds additional email headers.
        /// </summary>
        public TransactionalEmailBuilder WithHeader(string headerName, string headerValue)
        {
            if (_headers == null)
                _headers = new Dictionary<string, string>();

            _headers.Add(headerName, headerValue);

            return this;
        }

        /// <summary>
        /// Adds variable used to modify the content of your email.
        /// Specified as {var_name}:{var_value} pairs.
        /// Enter the information in the template text / HTML part by using the [[var:{var_name}]] format.
        /// Equivalent of using X-MJ-Vars header through SMTP.
        /// </summary>
        public TransactionalEmailBuilder WithVariable(string variableName, string variableValue)
        {
            if (_variables == null)
                _variables = new Dictionary<string, string>();

            _variables.Add(variableName, variableValue);

            return this;
        }

        /// <summary>
        /// Builds the mail message
        /// </summary>
        public TransactionalEmail Build()
        {
            Validate();

            return new TransactionalEmail
            {
                Subject = _subject,
                HTMLPart = _htmlPart,
                TextPart = _textPart,
                Sender = _sender,
                From = _from,
                ReplyTo = _replyTo,
                To = _to,
                Cc = _cc,
                Bcc = _bcc,
                TemplateID = _templateId,
                TemplateLanguage = _templateLanguage,
                TemplateErrorReporting = _templateErrorReporting,
                TemplateErrorDelivery = _templateErrorDelivery,
                Attachments = _attachments,
                InlinedAttachments = _inlinedAttachments,
                Priority = _priority,
                CustomCampaign = _customCampaign,
                DeduplicateCampaign = _deduplicateCampaign,
                TrackOpens = _trackOpens,
                TrackClicks = _trackClicks,
                CustomID = _customId,
                EventPayload = _eventPayload,
                URLTags = _urlTags,
                Headers = _headers,
                Variables = _variables
            };
        }

        /// <summary>
        /// Allows to create multiple messages with slightly different parameters
        /// with single preconfigured builder instance
        /// performs the deep clone of the current builder instance
        /// </summary>
        public TransactionalEmailBuilder Clone()
        {
            var result = (TransactionalEmailBuilder) MemberwiseClone();

            // create instances of collection to allow it's modifications in other builder
            result._to = _to.ToList();

            if (_cc != null)
                result._cc = _cc.ToList();

            if (_bcc != null)
                result._bcc = _bcc.ToList();

            if (_headers != null)
                result._headers = _headers.ToDictionary(x => x.Key, x => x.Value);

            if (_variables != null)
                result._variables = _variables.ToDictionary(x => x.Key, x => x.Value);

            return result;
        }

        private void Validate()
        {
            if (_from == null)
                throw new MailjetClientConfigurationException("From field should be specified");

            if (string.IsNullOrEmpty(_textPart) &&
                string.IsNullOrEmpty(_htmlPart) &&
                _templateId == null)
                throw new MailjetClientConfigurationException("TextPart or htmlPart or TemplateId should be set to send an email");

            if (_templateId.HasValue)
            {
                if (!string.IsNullOrEmpty(_textPart))
                    throw new MailjetClientConfigurationException("TemplateId is set, so TextPart will be ignored");
                
                if (!string.IsNullOrEmpty(_htmlPart))
                    throw new MailjetClientConfigurationException("TemplateId is set, so HtmlPart will be ignored");
            }
            else
            {
                if (_templateErrorDelivery.HasValue || _templateLanguage.HasValue || _templateErrorReporting != null)
                    throw new MailjetClientConfigurationException("To use template options, template id should be set");
            }
        }
    }
}