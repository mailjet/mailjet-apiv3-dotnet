using System;
using Mailjet.Client.Exceptions;

namespace Mailjet.Client.TransactionalEmails
{
    public class Contact
    {
        public Contact(string email, string name)
            : this(email)
        {
            Name = name;
        }

        public Contact(string email)
        {
            if (email == null || email.IndexOf("@", StringComparison.Ordinal) == -1)
                throw new MailjetClientConfigurationException("Valid email address is required");

            Email = email;
        }

        public string Name { get; }
        public string Email { get; }
    }
}