using System.Text.Json.Serialization;
using Mailjet.Client.Exceptions;

namespace Mailjet.Client.TransactionalEmails;

public class SendContact
{
    [JsonConstructor]
    public SendContact(string email, string? name = null)
    {
        if (email == null || !email.Contains('@'))
            throw new MailjetClientConfigurationException("Valid email address is required");
        Email = email;
        Name = name;
    }

    public string? Name { get; }
    public string Email { get; }
}
