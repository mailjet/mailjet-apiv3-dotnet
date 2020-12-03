using System;

namespace Mailjet.Client.Exceptions
{
    public class MailjetClientConfigurationException : Exception
    {
        public MailjetClientConfigurationException(string message) : base(message)
        {
        }
    }
}