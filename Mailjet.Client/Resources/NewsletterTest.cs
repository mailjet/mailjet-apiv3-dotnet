using System;

namespace Mailjet.Client.Resources
{
    [Obsolete("The newsletter test resource is deprecated by the Mailjet API. Use CampaigndraftTest.")]
    public static class NewsletterTest
    {
        public static readonly ResourceInfo Resource = new ResourceInfo("newsletter", "test");

        public const string Recipients = "Recipients";
    }
}


