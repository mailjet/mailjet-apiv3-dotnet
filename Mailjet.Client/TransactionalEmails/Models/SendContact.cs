using System;
using Mailjet.Client.Exceptions;

namespace Mailjet.Client.TransactionalEmails
{
    public class SendContact
    {
        public SendContact(string email, string name)
            : this(email)
        {
            Name = name;
        }

        public SendContact(string email)
        {
            if (email == null || email.IndexOf("@", StringComparison.Ordinal) == -1)
                throw new MailjetClientConfigurationException("Valid email address is required");

            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}