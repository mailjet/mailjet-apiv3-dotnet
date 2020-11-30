using System.Collections.Generic;

namespace Mailjet.Client.TransactionalEmails.Response
{
    public class MessageResult
    {
        /// <summary>
        /// Indicates the sending status of the message.
        /// Possible values:
        /// success
        /// error
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// List containing information about any errors with the processing of this message.
        /// Will be displayed only if processing errors occur.
        /// Each error will generate a separate error object with the below properties.
        /// </summary>
        public List<SendEmailError> Errors { get; set; }

        /// <summary>
        /// A user-defined custom ID.
        /// </summary>
        public string CustomID { get; set; }

        /// <summary>
        /// List containing information about the messages sent to recipients in To.
        /// </summary>
        public List<EmailResult> To { get; set; }

        /// <summary>
        /// List containing information about the messages sent to recipients in Cc.
        /// </summary>
        public List<EmailResult> Cc { get; set; }

        /// <summary>
        /// List containing information about the messages sent to recipients in Bcc.
        /// </summary>
        public List<EmailResult> Bcc { get; set; }
    }
}