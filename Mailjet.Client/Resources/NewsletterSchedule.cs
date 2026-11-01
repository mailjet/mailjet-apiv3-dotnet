using System;

namespace Mailjet.Client.Resources
{
    [Obsolete("The newsletter schedule resource is deprecated by the Mailjet API. Use CampaigndraftSchedule.")]
    public static class NewsletterSchedule
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("newsletter", "schedule");

        public const string Date = "Date";
    }
}


