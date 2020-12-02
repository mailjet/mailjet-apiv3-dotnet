namespace Mailjet.Client.Resources
{
    /// <summary>
    /// Represents Send API v3
    /// </summary>
    public static class Send
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("send", null, ApiVersion.V3, ResourceType.Send);

        public const string SandboxMode = "SandboxMode";

        public const string FromEmail = "FromEmail";
        public const string FromName = "FromName";
        public const string Subject = "Subject";
        public const string TextPart = "Text-Part";
        public const string HtmlPart = "Html-Part";
        public const string Recipients = "Recipients";
        public const string Vars = "Vars";
        public const string To = "To";
        public const string Cc = "cc";
        public const string Bcc = "bcc";
        public const string MjTemplateID = "Mj-TemplateID";
        public const string MjTemplateLanguage = "Mj-TemplateLanguage";
        public const string MjTemplateErrorReporting = "Mj-TemplateErrorReporting";
        public const string MjTemplateErrorDeliver = "Mj-TemplateErrorDeliver";
        public const string Attachments = "Attachments";
        public const string InlineAttachments = "Inline_Attachments";
        public const string Mjprio = "Mj-prio";
        public const string MjCustomID = "Mj-CustomID";
        public const string Mjcampaign = "Mj-campaign";
        public const string Mjdeduplicatecampaign = "Mj-deduplicatecampaign";
        public const string MjEventPayload = "Mj-EventPayload";
        public const string Headers = "Headers";
        public const string Messages = "Messages";
    }
}
