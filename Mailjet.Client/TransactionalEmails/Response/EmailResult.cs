namespace Mailjet.Client.TransactionalEmails.Response
{
    public class EmailResult
    {
        /// <summary>
        /// The email address of this recipient.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A 128-bit universally unique identifier (UUID) for this message.
        /// </summary>
        public string MessageUUID { get; set; }

        /// <summary>
        /// Unique numeric ID of this message.
        /// </summary>
        public long MessageID { get; set; }

        /// <summary>
        /// URL link that can be used for an API call to retrieve more information about this message.
        /// </summary>
        public string MessageHref { get; set; }
    }
}