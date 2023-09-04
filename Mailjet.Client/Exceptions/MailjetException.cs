using System;

namespace Mailjet.Client.Exceptions
{
    public class MailjetException : Exception
    {
        public MailjetException(string message) : base(message)
        {
        }

        public MailjetException(string message, Exception exception): base(message, exception)
        {
        }
    }
}