using System;

namespace Mailjet.Client.Resources
{
    [Obsolete("The newsletter status resource is deprecated by the Mailjet API. Use CampaigndraftStatus.")]
    public static class NewsletterStatus
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("newsletter", "status");

    }
}


