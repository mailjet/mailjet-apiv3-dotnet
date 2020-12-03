namespace Mailjet.Client.TransactionalEmails.Response
{
    public class SendEmailError
    {
        /// <summary>
        /// A 128-bit universally unique identifier (UUID) for this error.
        /// </summary>
        public string ErrorIdentifier { get; set; }

        /// <summary>
        /// An internal Mailjet error code for this error. See Send API Errors for more information.
        /// https://dev.mailjet.com/email/guides/send-api-v31/#send-api-errors
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// The error status code. See Send API Errors for more information.
        /// https://dev.mailjet.com/email/guides/send-api-v31/#send-api-errors
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// An error message informing the user what the issue is.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Indicates which part of the payload this error is related to.
        /// </summary>
        public string[] ErrorRelatedTo { get; set; }
    }
}