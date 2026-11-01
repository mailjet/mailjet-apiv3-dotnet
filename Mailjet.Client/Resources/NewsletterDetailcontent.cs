using System;

namespace Mailjet.Client.Resources
{
    [Obsolete("The newsletter detailcontent resource is deprecated by the Mailjet API. Use CampaigndraftDetailcontent.")]
    public static class NewsletterDetailcontent
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("newsletter", "detailcontent");

        public const string Textpart = "Text-part";
        public const string Htmlpart = "Html-part";
    }
}


