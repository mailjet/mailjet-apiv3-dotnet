namespace Mailjet.Client.Resources
{
    public static class Contacts
    {
        /// <summary>
        /// Use only for GDPR Delete contact only
        /// For the regular CRU operations, use <see cref="Mailjet.Client.Resources.Contact"/>
        /// </summary>
        public static readonly ResourceInfo Resource = new ResourceInfo("contacts", null, ApiVersion.V4, ResourceType.V4);
    }
}