namespace Mailjet.Client.Resources.SMS
{
    public static class Export
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("sms/export", null, ApiVersion.V4, ResourceType.V4);

        public const string FromTS = "FromTS";
        public const string ToTS = "ToTS";
        public const string ID = "ID";
        public const string URL = "URL";
        public const string Status = "Status";
        public const string CreationTS = "CreationTS";
        public const string ExpirationTS = "ExpirationTS";
    }
}
