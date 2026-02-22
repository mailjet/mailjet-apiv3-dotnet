using Mailjet.Client.Exceptions;

namespace Mailjet.Client.TransactionalEmails;

public class TransactionalEmailBuilder
{
    private string? _subject;
    private string? _htmlPart;
    private string? _textPart;
    private SendContact? _sender;
    private SendContact? _from;
    private SendContact? _replyTo;
    private List<SendContact> _to = [];
    private List<SendContact>? _cc;
    private List<SendContact>? _bcc;
    private long? _templateId;
    private bool? _templateLanguage;
    private SendContact? _templateErrorReporting;
    private bool? _templateErrorDeliver;
    private List<Attachment>? _attachments;
    private List<Attachment>? _inlinedAttachments;
    private int? _priority;
    private string? _customCampaign;
    private bool? _deduplicateCampaign;
    private TrackOpens? _trackOpens;
    private TrackClicks? _trackClicks;
    private string? _customId;
    private string? _eventPayload;
    private string? _urlTags;
    private Dictionary<string, string>? _headers;
    private IDictionary<string, object>? _variables;

    public TransactionalEmailBuilder WithSubject(string subject) { _subject = subject; return this; }
    public TransactionalEmailBuilder WithHtmlPart(string htmlPart) { _htmlPart = htmlPart; return this; }
    public TransactionalEmailBuilder WithTextPart(string textPart) { _textPart = textPart; return this; }
    public TransactionalEmailBuilder WithSender(SendContact sender) { _sender = sender; return this; }
    public TransactionalEmailBuilder WithFrom(SendContact from) { _from = from; return this; }
    public TransactionalEmailBuilder WithReplyTo(SendContact replyTo) { _replyTo = replyTo; return this; }
    public TransactionalEmailBuilder WithTo(SendContact to) { _to.Add(to); return this; }
    public TransactionalEmailBuilder WithTo(IEnumerable<SendContact> toContacts) { _to.AddRange(toContacts); return this; }
    public TransactionalEmailBuilder WithCc(SendContact cc) { _cc ??= []; _cc.Add(cc); return this; }
    public TransactionalEmailBuilder WithCc(IEnumerable<SendContact> ccContacts) { _cc ??= []; _cc.AddRange(ccContacts); return this; }
    public TransactionalEmailBuilder WithBcc(SendContact bcc) { _bcc ??= []; _bcc.Add(bcc); return this; }
    public TransactionalEmailBuilder WithBcc(IEnumerable<SendContact> bccContacts) { _bcc ??= []; _bcc.AddRange(bccContacts); return this; }
    public TransactionalEmailBuilder WithTemplateId(long templateId) { _templateId = templateId; return this; }
    public TransactionalEmailBuilder WithTemplateLanguage(bool value) { _templateLanguage = value; return this; }
    public TransactionalEmailBuilder WithTemplateErrorReporting(SendContact contact) { _templateErrorReporting = contact; return this; }
    public TransactionalEmailBuilder WithTemplateErrorDeliver(bool value) { _templateErrorDeliver = value; return this; }
    public TransactionalEmailBuilder WithAttachments(IEnumerable<Attachment> attachments) { _attachments ??= []; _attachments.AddRange(attachments); return this; }
    public TransactionalEmailBuilder WithAttachment(Attachment attachment) { _attachments ??= []; _attachments.Add(attachment); return this; }
    public TransactionalEmailBuilder WithInlinedAttachments(IEnumerable<Attachment> inlinedAttachments) { _inlinedAttachments ??= []; _inlinedAttachments.AddRange(inlinedAttachments); return this; }
    public TransactionalEmailBuilder WithInlinedAttachment(Attachment inlinedAttachment) { _inlinedAttachments ??= []; _inlinedAttachments.Add(inlinedAttachment); return this; }
    public TransactionalEmailBuilder WithPriority(int priority) { _priority = priority; return this; }
    public TransactionalEmailBuilder WithCustomCampaign(string customCampaign) { _customCampaign = customCampaign; return this; }
    public TransactionalEmailBuilder WithDeduplicateCampaign(bool value) { _deduplicateCampaign = value; return this; }
    public TransactionalEmailBuilder WithTrackOpens(TrackOpens trackOpens) { _trackOpens = trackOpens; return this; }
    public TransactionalEmailBuilder WithTrackClicks(TrackClicks trackClicks) { _trackClicks = trackClicks; return this; }
    public TransactionalEmailBuilder WithCustomId(string customId) { _customId = customId; return this; }
    public TransactionalEmailBuilder WithEventPayload(string eventPayload) { _eventPayload = eventPayload; return this; }
    public TransactionalEmailBuilder WithUrlTags(string urlTags) { _urlTags = urlTags; return this; }
    public TransactionalEmailBuilder WithHeader(string headerName, string headerValue) { _headers ??= []; _headers.Add(headerName, headerValue); return this; }
    public TransactionalEmailBuilder WithVariable(string variableName, object variableValue) { _variables ??= new Dictionary<string, object>(); _variables.Add(variableName, variableValue); return this; }
    public TransactionalEmailBuilder WithVariables(IDictionary<string, object> variables) { _variables = variables; return this; }

    public TransactionalEmail Build()
    {
        Validate();
        return new TransactionalEmail
        {
            Subject = _subject, HTMLPart = _htmlPart, TextPart = _textPart, Sender = _sender, From = _from, ReplyTo = _replyTo,
            To = _to, Cc = _cc, Bcc = _bcc, TemplateID = _templateId, TemplateLanguage = _templateLanguage,
            TemplateErrorReporting = _templateErrorReporting, TemplateErrorDeliver = _templateErrorDeliver,
            Attachments = _attachments, InlinedAttachments = _inlinedAttachments, Priority = _priority,
            CustomCampaign = _customCampaign, DeduplicateCampaign = _deduplicateCampaign, TrackOpens = _trackOpens,
            TrackClicks = _trackClicks, CustomID = _customId, EventPayload = _eventPayload, URLTags = _urlTags,
            Headers = _headers, Variables = _variables
        };
    }

    public TransactionalEmailBuilder Clone()
    {
        var result = (TransactionalEmailBuilder)MemberwiseClone();
        result._to = [.. _to];
        if (_cc != null) result._cc = [.. _cc];
        if (_bcc != null) result._bcc = [.. _bcc];
        if (_headers != null) result._headers = _headers.ToDictionary(x => x.Key, x => x.Value);
        if (_variables != null) result._variables = _variables.ToDictionary(x => x.Key, x => x.Value);
        return result;
    }

    private void Validate()
    {
        if (_from == null && _templateId == null)
            throw new MailjetClientConfigurationException("From field should be specified");
        if (string.IsNullOrEmpty(_textPart) && string.IsNullOrEmpty(_htmlPart) && _templateId == null)
            throw new MailjetClientConfigurationException("TextPart or htmlPart or TemplateId should be set to send an email");
        if (_templateId.HasValue)
        {
            if (!string.IsNullOrEmpty(_textPart))
                throw new MailjetClientConfigurationException("TemplateId is set, so TextPart will be ignored");
            if (!string.IsNullOrEmpty(_htmlPart))
                throw new MailjetClientConfigurationException("TemplateId is set, so HtmlPart will be ignored");
        }
        else if (_templateErrorDeliver.HasValue || _templateErrorReporting != null)
            throw new MailjetClientConfigurationException("To use template options, template id should be set");
    }
}
