namespace Mailjet.Client.TransactionalEmails
{
    public class Attachment
    {
        /// <summary>
        /// Creates attachment object
        /// </summary>
        /// <param name="filename">The full name of the file (including the file extension).</param>
        /// <param name="contentType">Defines the type of content being sent out using a MIME type.
        /// See the official MIME type list for additional information. https://www.iana.org/assignments/media-types/media-types.xhtml </param>
        /// <param name="base64Content">Base64 encoded content of the attached file.</param>
        public Attachment(string filename, string contentType, string base64Content)
        {
            Filename = filename;
            ContentType = contentType;
            Base64Content = base64Content;
        }


        /// <remarks>
        /// If the attachment is inlined, you can set ContentId
        /// Then you can set Name of the cid to be inserted in the HTML content of the message.
        /// The value of ContentId must be unique across all inline attachments in the message.
        /// </remarks>
        public Attachment(string filename, string contentType, string base64Content, string contentId)
            : this(filename, contentType, base64Content)
        {
            ContentID = contentId;
        }

        //public static Attachment FromFile(string filePath)
        //{
        //    var contentType = MimeMapping.GetMimeType(filePath);
        //    var fileName = Path.GetFileName(filePath);
        //    var fileContent = File.ReadAllBytes(filePath);
        //    var base64FileContent = Convert.ToBase64String(fileContent);

        //    return new Attachment(fileName, contentType, base64FileContent);
        //}

        public string Filename { get; }
        public string ContentType { get; }
        public string Base64Content { get; }

        /// <summary>
        /// Name of the cid to be inserted in the HTML content of the message.
        /// The value must be unique across all inline attachments in the message.
        /// The value valid ONLY for InlinedAttachment
        /// </summary>
        public string ContentID { get; }
    }

    //public class MimeMapping
    //{
    //    public static string GetMimeType(string filePath)
    //    {
    //        var extension = Path.GetExtension(filePath).ToLowerInvariant();

    //        return KnownMimeTypes.TryGetValue(extension, out var mimeType)
    //            ? mimeType
    //            : "application/octet-stream";
    //    }

    //    private static readonly Dictionary<string, string> KnownMimeTypes = new Dictionary<string, string>
    //    {
    //        {".doc", "application/msword"},
    //        {".docx", "application/msword"},
    //        {".pdf", "application/pdf"},
    //        {".rtf", "application/rtf"},
    //        {".xls", "application/vnd.ms-excel"},
    //        {".xlsx", "application/vnd.ms-excel"},
    //        {".pps", "application/vnd.ms-powerpoint"},
    //        {".ppt", "application/vnd.ms-powerpoint"},
    //        {".pptx", "application/vnd.ms-powerpoint"},
    //        {".zip", "application/zip"},
    //        {".jpeg", "image/jpeg"},
    //        {".jpe", "image/jpeg"},
    //        {".jpg", "image/jpeg"},
    //        {".png", "application/png"},
    //        {".bmp", "image/bmp"},
    //        {".txt", "text/plain"},
    //    };
    //}
}