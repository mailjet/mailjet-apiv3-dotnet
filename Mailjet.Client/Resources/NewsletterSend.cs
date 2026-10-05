using System;

namespace Mailjet.Client.Resources
{
    [Obsolete("The newsletter send resource is deprecated by the Mailjet API. Use CampaigndraftSend.")]
    public static class NewsletterSend
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("newsletter", "send");

    }
}


