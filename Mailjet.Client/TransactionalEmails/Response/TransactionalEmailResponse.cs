namespace Mailjet.Client.TransactionalEmails.Response
{
    public class TransactionalEmailResponse
    {
        /// <summary>
        /// Array with information regarding each sent message
        /// </summary>
        public MessageResult[] Messages { get; set; }
    }
}