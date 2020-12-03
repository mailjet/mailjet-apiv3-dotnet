namespace Mailjet.Client.Resources.SMS
{
    public static class Count
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("sms/count", null, ApiVersion.V4, ResourceType.V4);

        public const string To = "To";
        public const string FromTS = "FromTS";
        public const string ToTS = "ToTS";
        public const string StatusCode = "StatusCode";
    }
}
