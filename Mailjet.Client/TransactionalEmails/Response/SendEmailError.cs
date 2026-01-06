namespace Mailjet.Client.TransactionalEmails.Response;

public class SendEmailError
{
    public string? ErrorIdentifier { get; set; }
    public string? ErrorCode { get; set; }
    public int StatusCode { get; set; }
    public string? ErrorMessage { get; set; }
    public string[]? ErrorRelatedTo { get; set; }
}
