namespace Mailjet.Client.Resources.SMS
{
    public static class SMS
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("sms", null, ResourceType.V4);

        public const string From = "From";
        public const string To = "To";
        public const string Text = "Text";
        public const string MessageID = "MessageID";
        public const string SMSCount = "SMSCount";
        public const string CreationTS = "CreationTS";
        public const string SentTS = "SentTS";
        public const string Cost = "Cost";
        public const string Status = "Status";
        public const string FromTS = "FromTS";
        public const string ToTS = "ToTS";
        public const string StatusCode = "StatusCode";
        public const string Offset = "Offset";
        public const string Limit = "Limit";
    }
}
