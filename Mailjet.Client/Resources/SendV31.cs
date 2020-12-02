namespace Mailjet.Client.Resources
{
    /// <summary>
    /// Represents Send API v3.1
    /// </summary>
    public static class SendV31
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("send", null, ApiVersion.V3_1, ResourceType.Send);

        public const int MaxEmailsPerBatch = 50;
    }
}