namespace Mailjet.Client.TransactionalEmails.Response;

public class MessageResult
{
    public string? Status { get; set; }
    public List<SendEmailError>? Errors { get; set; }
    public string? CustomID { get; set; }
    public List<EmailResult>? To { get; set; }
    public List<EmailResult>? Cc { get; set; }
    public List<EmailResult>? Bcc { get; set; }
}
