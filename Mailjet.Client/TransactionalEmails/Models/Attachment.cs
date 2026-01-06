using System.Text.Json.Serialization;

namespace Mailjet.Client.TransactionalEmails;

[method: JsonConstructor]
public class Attachment(string filename, string contentType, string base64Content)
{
    public Attachment(string filename, string contentType, string base64Content, string contentId)
        : this(filename, contentType, base64Content)
    {
        ContentID = contentId;
    }

    public string Filename { get; } = filename;
    public string ContentType { get; } = contentType;
    public string Base64Content { get; } = base64Content;
    public string? ContentID { get; }
}
