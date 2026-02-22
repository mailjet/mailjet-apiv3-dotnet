namespace Mailjet.Client.Exceptions
{
    public class MailjetServerException : MailjetException
    {
        public int HttpCode { get; set; }

        public MailjetServerException(string message) : base(message)
        {
        }

        public MailjetServerException(MailjetResponse mailjetResponse) : base(mailjetResponse.Content.ToString())
        {
            HttpCode = mailjetResponse.StatusCode;
        }
    }
}