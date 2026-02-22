namespace Mailjet.Client.TransactionalEmails.Response;

public class EmailResult
{
    public string? Email { get; set; }
    public string? MessageUUID { get; set; }
    public long MessageID { get; set; }
    public string? MessageHref { get; set; }
}
